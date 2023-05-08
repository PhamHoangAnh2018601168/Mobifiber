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

namespace MobiFiber.DAO
{
    public class Report_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<RegisteredCustomers> GetRegisteredCustomers(string fromDate, string toDate, string keySearch)
        {
            List<RegisteredCustomers> lstObj = new List<RegisteredCustomers>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@keySearch", keySearch);

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<RegisteredCustomers>("Mobifiber_Report_RegisteredCustomers", p, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }

        public List<DeviceCostAllocation> GetDeviceCostAllocation(string fromDate, string toDate, string keySearch, int IsActiveStatus)
        {
            List<DeviceCostAllocation> lstObj = new List<DeviceCostAllocation>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@keySearch", keySearch);
                p.Add("@IsActiveStatus", IsActiveStatus);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<DeviceCostAllocation>("Mobifiber_Report_DeviceCostAllocation", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<DeviceStatusNoActive> GetDeviceNoActive(string fromDate, string toDate, string keySearch, int ActiveStatus)
        {
            List<DeviceStatusNoActive> lstObj = new List<DeviceStatusNoActive>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@keySearch", keySearch);
                p.Add("@Type", ActiveStatus);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<DeviceStatusNoActive>("Mobifiber_Report_DeviceStatusNoActive", p, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
        public List<ServiceRevenueAllocation> GetServiceRevenueAllocation(string fromDate, string toDate, string keySearch)
        {
            List<ServiceRevenueAllocation> lstObj = new List<ServiceRevenueAllocation>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@keySearch", keySearch);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<ServiceRevenueAllocation>("Mobifiber_Report_ServiceRevenueAllocation", p, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }

        public List<PartialViewModel.DeviceStatus> GetDeviceStatus(string fromDate, string toDate, string keySearch)
        {
            List<PartialViewModel.DeviceStatus> lstObj = new List<PartialViewModel.DeviceStatus>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);
                p.Add("@keySearch", keySearch);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<PartialViewModel.DeviceStatus>("Mobifiber_Report_DeviceStatus", p, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }
    }
}
