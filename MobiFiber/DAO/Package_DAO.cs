using Dapper;
using Micro.Web;
using MobiFiber.Code;
using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MobiFiber.DAO
{
    public class Package_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Package_DAO()
        {
            _context = new MobiFiberContext();
        }
        public bool Add(MobifiberPackage obj)
        {
            MobifiberPackage mobifiberPackage = _context.MobifiberPackages.FirstOrDefault(_o => _o.PackageNumber == obj.PackageNumber && _o.Status != (int)PakageStatus.Delete);

            if (mobifiberPackage != null)
            {
                return false;
            }

            _context.MobifiberPackages.Add(obj);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool AddRange(List<MobifiberPackage> rangeDevices)
        {
            _context.MobifiberPackages.AddRange(rangeDevices);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public List<MobifiberPackage> GetAllPackageUsingQuery(ref int total, int status, string search , int PageIndex ,int PageSize)
        {
            List<MobifiberPackage> lstObj = new List<MobifiberPackage>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Status", status);
                p.Add("@search", search);
                p.Add("@PageIndex", PageIndex);
                p.Add("@PageSize", PageSize);
                p.Add("@Total", total, System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberPackage>("MobiFiber_GetFilterPackage", p, commandType: CommandType.StoredProcedure).ToList();
                    total = p.Get<int>("@Total");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return lstObj;
        }

        public List<MobifiberPackage> GetAllPackageUsingStoreProcedure()
        {
            List<MobifiberPackage> lstObj = new List<MobifiberPackage>();
            try
            {
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberPackage>("MobiFiber_GetAllPackage", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            return lstObj;
        }

        public MobifiberPackage GetById(int id)
        {
            MobifiberPackage obj = new MobifiberPackage();
            try
            {
                obj = _context.MobifiberPackages.First(_o => _o.PackageId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return obj;
        }

        public string SaveOrUpdate(MobifiberPackage obj)
        {
            try
            {
                MobifiberPackage objTemp = _context.MobifiberPackages.FirstOrDefault(_o => _o.PackageId == obj.PackageId);
                if (objTemp == null)
                {
                    MobifiberPackage objCheck = _context.MobifiberPackages.FirstOrDefault(_o => _o.PackageName == obj.PackageName && _o.Status != (int)PakageStatus.Delete);

                    if (objCheck != null)
                    {
                        return Constant.CODE_EXISTS;
                    }
                    _context.MobifiberPackages.Add(obj);
                }
                else
                {
                    //MobifiberPackage objCheck = _context.MobifiberPackages.FirstOrDefault(_o => _o.PackageId != obj.PackageId && _o.Status != (int)PakageStatus.Delete);

                    //if(objCheck != null)
                    //{
                    //    return Constant.CODE_EXISTS;
                    //}
                    objTemp.PackageName = obj.PackageName;
                    objTemp.PackageNumber = obj.PackageNumber;
                    objTemp.Decision = obj.Decision;
                    objTemp.TimeUsed = obj.TimeUsed;
                    objTemp.PromotionTime = obj.PromotionTime;
                    objTemp.Price = obj.Price;
                    objTemp.PriceVat = obj.PriceVat;
                    objTemp.Status = obj.Status;
                    _context.MobifiberPackages.Update(objTemp);
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
    }
}
