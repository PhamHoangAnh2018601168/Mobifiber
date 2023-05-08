using Dapper;
using Micro.Web.Code;
using MobiFiber.Code;
using MobiFiber.Models;
using MobiFiber.PartialViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace MobiFiber.DAO
{
    public class Contract_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Contract_DAO()
        {
            _context = new MobiFiberContext();
        }
        public bool Add(MobifiberContract obj)
        {
            MobifiberContract mobifiberContract = _context.MobifiberContracts.FirstOrDefault(_o => _o.CustomerIdvm == obj.CustomerIdvm);
            MobifiberDevice mobifiberDevice = _context.MobifiberDevices.FirstOrDefault(o => o.DeviceId == obj.DeviceId && o.Status == (int)Code.DeviceStatus.NotLinked);
            MobifiberPackage mobifiberPackage = _context.MobifiberPackages.FirstOrDefault(o => o.PackageId == obj.PackageId && o.Status == (int)Code.PakageStatus.Active);

            if (mobifiberContract != null || (mobifiberDevice == null && obj.DeviceId != 0) || mobifiberPackage == null)
            {
                return false;
            }

            if (obj.DeviceId != Constant.ID_EVICTION)
            {
                Device_DAO dal_device = new Device_DAO();
                if (mobifiberDevice != null && mobifiberDevice.IsActive == (int)DeviceActiveStatus.New && mobifiberDevice.Status == (int)Code.DeviceStatus.NotLinked)
                {
                    mobifiberDevice.IsActive = (int)DeviceActiveStatus.NotNew;
                    mobifiberDevice.DateActive = obj.SignDate;
                }
                mobifiberDevice.Status = (int)Code.DeviceStatus.Linked;
                _context.MobifiberDevices.Update(mobifiberDevice);
            }

            _context.MobifiberContracts.Add(obj);
            var result = _context.SaveChanges();

            MobifiberContract newContract = new MobifiberContract();
            MobifiberContract current = Common.clone<MobifiberContract>(newContract);

            foreach (var prop in current.GetType().GetProperties())
            {

                object _oldValue = Common.GetPropValue(current, prop.Name);
                object _newValue = Common.GetPropValue(obj, prop.Name);

                string oldValue = "";
                string newValue = "";

                oldValue = _oldValue == null ? "" : _oldValue.ToString();
                newValue = _newValue == null ? "" : _newValue.ToString();


                if (_oldValue != _newValue)
                {
                    WriteLogToDatabase.AddLog(
                                (int)ActionModule.ContractManager,
                                Constant.CREATE,
                                SessionSystem.userSession.UserId,
                                DateTime.Now,
                                (int)ActionTypeCustom.Add,
                                obj.ContractId,
                                prop.Name,
                                newValue,
                                oldValue
                                );
                }

            }

            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool AddRange(List<MobifiberContract> rangeDevices)
        {
            _context.MobifiberContracts.AddRange(rangeDevices);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                List<MobifiberDevice> lstDevice = new List<MobifiberDevice>();
                List<MobifiberHistory> lstHistory = new List<MobifiberHistory>();

                for (int i = 0; i < rangeDevices.Count; i++)
                {
                    var obj = rangeDevices[i];
                    if (obj.DeviceId != Constant.ID_EVICTION)
                    {
                        MobifiberDevice mobifiberDevice = _context.MobifiberDevices.FirstOrDefault(o => o.DeviceId == obj.DeviceId && o.Status == (int)Code.DeviceStatus.NotLinked);
                        if (mobifiberDevice != null && mobifiberDevice.IsActive == (int)DeviceActiveStatus.New && mobifiberDevice.Status == (int)Code.DeviceStatus.NotLinked)
                        {
                            mobifiberDevice.IsActive = (int)DeviceActiveStatus.NotNew;
                            mobifiberDevice.DateActive = obj.SignDate;
                        }
                        mobifiberDevice.Status = (int)Code.DeviceStatus.Linked;
                        lstDevice.Add(mobifiberDevice);
                    }
                    lstHistory.Add(new MobifiberHistory()
                    {
                        Type = (int)ActionModule.ContractManager,
                        Description = Constant.CREATE,
                        Action = (int)ActionTypeCustom.Add,
                        CreateBy = SessionSystem.userSession.UserId,
                        DateCreate = DateTime.Now,
                        FieldChange = "DeviceId",
                        IdRefer = obj.ContractId,
                        NewValue = obj.DeviceId.ToString(),
                        OldValue = "0"
                    });

                    lstHistory.Add(new MobifiberHistory()
                    {
                        Type = (int)ActionModule.ContractManager,
                        Description = Constant.CREATE,
                        Action = (int)ActionTypeCustom.Add,
                        CreateBy = SessionSystem.userSession.UserId,
                        DateCreate = DateTime.Now,
                        FieldChange = "PackageId",
                        IdRefer = obj.ContractId,
                        NewValue = obj.PackageId.ToString(),
                        OldValue = "0"
                    });
                }
                _context.MobifiberDevices.UpdateRange(lstDevice);
                _context.MobifiberHistories.AddRange(lstHistory);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
        public bool Update(MobifiberContract obj)
        {
            _context.MobifiberContracts.Update(obj);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public List<MobifiberContract> GetAllContractUsingQuery()
        {
            string sql = "SELECT * FROM Mobifiber_Contract";
            List<MobifiberContract> lstObj = new List<MobifiberContract>();
            try
            {
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberContract>(sql).ToList();

                    //FiddleHelper.WriteTable(orderDetails);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return lstObj;
        }

        public MobifiberContract GetById(int id)
        {
            MobifiberContract obj = new MobifiberContract();
            try
            {
                obj = _context.MobifiberContracts.First(_o => _o.ContractId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return obj;
        }
        public List<ContractModelView> GetContractUsingStoreProcedue(ref int total, int status, int package, string search, int PageIndex, int PageSize)
        {
            List<ContractModelView> lstObj = new List<ContractModelView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Status", status);
                p.Add("@search", search);
                p.Add("@PageIndex", PageIndex);
                p.Add("@PageSize", PageSize);
                p.Add("@package", package);
                p.Add("@Total", total, System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<ContractModelView>("MobiFiber_GetContract", p, commandType: CommandType.StoredProcedure).ToList();
                    total = p.Get<int>("@Total");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<ContractModelView> GetContractExport(int status, int package, string search)
        {
            List<ContractModelView> lstObj = new List<ContractModelView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Status", status);
                p.Add("@search", search);
                p.Add("@package", package);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<ContractModelView>("MobiFiber_Export_GetContract", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }

        public bool SaveOrUpdate(MobifiberContract obj)
        {
            using (MobiFiberContext _context = new MobiFiberContext())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        MobifiberContract objTemp = _context.MobifiberContracts.FirstOrDefault(_o => _o.ContractId == obj.ContractId);
                        if (objTemp == null)
                        {
                            _context.MobifiberContracts.Add(obj);
                        }
                        else
                        {
                            int DeviceIdOld = objTemp.DeviceId;
                            objTemp.ContractId = obj.ContractId;
                            objTemp.CustomerName = obj.CustomerName;
                            objTemp.CustomerIdvm = obj.CustomerIdvm;
                            objTemp.IdentityCard = obj.IdentityCard;
                            objTemp.Address = obj.Address;
                            objTemp.Phone = obj.Phone;
                            objTemp.ContractNumber = obj.ContractNumber;
                            objTemp.SignDate = obj.SignDate;
                            objTemp.PackageId = obj.PackageId;
                            objTemp.DeviceId = obj.DeviceId;
                            objTemp.RegisterDate = obj.RegisterDate;
                            objTemp.AgentcodeAm = obj.AgentcodeAm;
                            objTemp.BillNumber = obj.BillNumber;
                            objTemp.BillDate = obj.BillDate;
                            objTemp.BillPrice = obj.BillPrice;
                            objTemp.DeveloperName = obj.DeveloperName;
                            objTemp.InfrastructurePartners = obj.InfrastructurePartners;
                            objTemp.TypeOfCooperation = obj.TypeOfCooperation;
                            objTemp.Status = obj.Status;
                            objTemp.LiquidationDate = obj.LiquidationDate;
                            _context.MobifiberContracts.Update(objTemp);

                            //update device
                            if (DeviceIdOld != objTemp.DeviceId && DeviceIdOld != Constant.ID_EVICTION)
                            {
                                Device_DAO device_DAO = new Device_DAO();
                                MobifiberDevice DeviceOld = device_DAO.GetById(DeviceIdOld);
                                DeviceOld.Status = (int)Code.DeviceStatus.NotLinked;
                                DeviceOld.DateReinputWarehouse = DateTime.Now;  //ngay nhap lai

                                _context.MobifiberDevices.Update(DeviceOld);
                                //todo neu het khau hao thi xoa luon
                            }
                        }

                        //active device when New
                        if (obj.DeviceId != Constant.ID_EVICTION)
                        {
                            Device_DAO dal_device = new Device_DAO();
                            MobifiberDevice DeviceNew = dal_device.GetById(obj.DeviceId);
                            if (DeviceNew != null && DeviceNew.IsActive == (int)DeviceActiveStatus.New && DeviceNew.Status == (int)Code.DeviceStatus.NotLinked)
                            {
                                DeviceNew.IsActive = (int)DeviceActiveStatus.NotNew;
                                if (obj.DateChangeDevice != null)
                                {
                                    DateTime DateChange_Device = DateTime.ParseExact(obj.DateChangeDevice, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DeviceNew.DateActive = DateChange_Device;
                                }
                                else
                                {
                                    DeviceNew.DateActive = obj.SignDate;
                                }
                            }
                            DeviceNew.Status = (int)Code.DeviceStatus.Linked;
                            _context.MobifiberDevices.Update(DeviceNew);
                        }

                        var result = _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.Message, ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
