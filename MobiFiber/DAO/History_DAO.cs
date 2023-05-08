using Dapper;
using MobiFiber.Code;
using MobiFiber.Models;
using MobiFiber.PartialViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.DAO
{
    public class History_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public History_DAO()
        {
            _context = new MobiFiberContext();
        }
        public List<MobifiberHistory> GetAllHistoryUsingQuery()
        {
            string sql = "SELECT * FROM Mobifiber_History";
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();

            using (var connection = new SqlConnection(connectString))
            {
                lstObj = connection.Query<MobifiberHistory>(sql).ToList();

                //FiddleHelper.WriteTable(orderDetails);

                return lstObj;
            }
        }

        public MobifiberHistory GetHistoryLastRole()
        {
            string sql = "SELECT TOP 1 * FROM Mobifiber_History where [Type] = 0 order by [Id] DESC";
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();

            using (var connection = new SqlConnection(connectString))
            {
                lstObj = connection.Query<MobifiberHistory>(sql).ToList();
            }
            if(lstObj != null && lstObj.Count > 0)
            {
                return lstObj[0];
            }
            else
            {
                return null;
            }
        }
        public List<HistoryDeviceView> GetDeviceHistoryUsingStoreProcedure(int Id)
        {
            List<HistoryDeviceView> lstObj = new List<HistoryDeviceView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@DeviceId", Id);


                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<HistoryDeviceView>("MobiFiber_History_GetDataHistoryDevice", p, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<HistoryPackageView> GetPackageHistoryUsingStoreProcedure(int Id)
        {
            List<HistoryPackageView> lstObj = new List<HistoryPackageView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@PackageId", Id);

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<HistoryPackageView>("MobiFiber_History_GetDataHistoryPackage", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<HistoryContractPackageView> GetContractPackageHistoryUsingStoreProcedure(int Id)
        {
            List<HistoryContractPackageView> lstObj = new List<HistoryContractPackageView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@ContractId", Id);

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<HistoryContractPackageView>("MobiFiber_History_GetDataHistoryContract_Package", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<HistoryContractDeviceView> GetContractDeviceHistoryUsingStoreProcedure(int Id)
        {
            List<HistoryContractDeviceView> lstObj = new List<HistoryContractDeviceView>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@ContractId", Id);

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<HistoryContractDeviceView>("MobiFiber_History_GetDataHistoryContract_Device", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }


        public string Save(MobifiberHistory obj)
        {
            try
            {
                _context.MobifiberHistories.Add(obj);
                var result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Constant.CODE_EXCEPTION;
            }

            return Constant.CODE_SUCCESS;
        }

        public List<MobifiberHistory> GetHistoriesDeviceById(int Id)
        {
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();
            lstObj = _context.MobifiberHistories.Where(_o => _o.Type == (int)ActionModule.DeviceManager && _o.IdRefer == Id).ToList();
            return lstObj;
        }
        public List<MobifiberHistory> GetHistoriesPackageById(int Id)
        {
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();
            lstObj = _context.MobifiberHistories.Where(_o => _o.Type == (int)ActionModule.PackageManager && _o.IdRefer == Id).ToList();
            return lstObj;
        }
        public List<MobifiberHistory> GetHistoriesContractById(int Id)
        {
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();
            lstObj = _context.MobifiberHistories.Where(_o => _o.Type == (int)ActionModule.ContractManager && _o.IdRefer == Id).ToList();
            return lstObj;
        }
    }
}
