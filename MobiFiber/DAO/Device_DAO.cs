using MobiFiber.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System;
using System.Data;
using MobiFiber.Code;
using Micro.Web;

namespace MobiFiber.DAO
{
    public class Device_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Device_DAO()
        {
            _context = new MobiFiberContext();
        }

        public bool Add(MobifiberDevice obj)
        {
            MobifiberDevice mobifiberDevice = _context.MobifiberDevices.FirstOrDefault(_o => _o.DeviceCode == obj.DeviceCode && _o.Serial == obj.Serial && _o.Status != (int)DeviceStatus.Delete);

            if (mobifiberDevice != null)
            {
                return false;
            }

            _context.MobifiberDevices.Add(obj);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool AddRange(List<MobifiberDevice> rangeDevices)
        {
            _context.MobifiberDevices.AddRange(rangeDevices);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool Update(MobifiberDevice obj)
        {
            _context.MobifiberDevices.Update(obj);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public List<MobifiberDevice> GetAllDeviceUsingQuery()
        {
            List<MobifiberDevice> lstObj = new List<MobifiberDevice>();
            try
            {
                string sql = "SELECT * FROM Mobifiber_Device";
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberDevice>(sql).ToList();

                    //FiddleHelper.WriteTable(orderDetails);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<MobifiberDevice> GetDeviceAvailability()
        {
            List<MobifiberDevice> lstObj = new List<MobifiberDevice>();
            try
            {
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberDevice>("MobiFiber_GetDeviceAvailability", commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public string SaveOrUpdate(MobifiberDevice obj)
        {
            try
            {
                MobifiberDevice objTemp = _context.MobifiberDevices.FirstOrDefault(_o => _o.DeviceId == obj.DeviceId);
                if (objTemp == null)
                {
                    MobifiberDevice objCheck = _context.MobifiberDevices.FirstOrDefault(_o => _o.DeviceCode == obj.DeviceCode && _o.Serial == obj.Serial && _o.Status != (int)DeviceStatus.Delete);

                    if (objCheck != null)
                    {
                        return Constant.CODE_EXISTS;
                    }

                    obj.Status = (int)DeviceStatus.NotLinked;
                    obj.IsActive = (int)DeviceActiveStatus.New;
                    _context.MobifiberDevices.Add(obj);
                }
                else
                {
                    MobifiberDevice objCheck = _context.MobifiberDevices.FirstOrDefault(_o => _o.DeviceCode == obj.DeviceCode && _o.Serial == obj.Serial && _o.DeviceId != obj.DeviceId && _o.Status != (int)DeviceStatus.Delete);

                    if (objCheck != null)
                    {
                        return Constant.CODE_EXISTS;
                    }

                    objTemp.DeviceName = obj.DeviceName;
                    objTemp.DeviceCode = obj.DeviceCode;
                    objTemp.Serial = obj.Serial;
                    objTemp.DevicePrice = obj.DevicePrice;
                    objTemp.AllocationTime = obj.AllocationTime;
                    objTemp.DateInputWarehouse = obj.DateInputWarehouse;
                    objTemp.DateReinputWarehouse = obj.DateReinputWarehouse;
                    objTemp.Status = obj.Status;
                    objTemp.IsActive = obj.IsActive;
                    objTemp.DateActive = obj.DateActive;
                    _context.MobifiberDevices.Update(objTemp);
                }
                var result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Constant.CODE_EXCEPTION;
            }

            return Constant.CODE_SUCCESS;
        }
        public MobifiberDevice GetById(int id)
        {
            MobifiberDevice obj = new MobifiberDevice();
            try
            {
                obj = _context.MobifiberDevices.First(_o => _o.DeviceId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return obj;
        }
    }
}
