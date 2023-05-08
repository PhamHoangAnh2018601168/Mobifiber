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
    public class Developer_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Developer_DAO()
        {
            _context = new MobiFiberContext();
        }

        public List<MobifiberDevelopmentUnit> ListAll()
        {
            List<MobifiberDevelopmentUnit> lstObj = new List<MobifiberDevelopmentUnit>();
            try
            {
                string sql = "SELECT * FROM Mobifiber_DevelopmentUnit WHERE Status = 0 ";

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberDevelopmentUnit>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return lstObj;
        }
        public bool Delete(int Id)
        {
            MobifiberDevelopmentUnit objTemp = _context.MobifiberDevelopmentUnits.First(_o => _o.DevelopId == Id);
            objTemp.Status = 1;
            _context.MobifiberDevelopmentUnits.Update(objTemp);
            _context.SaveChanges();
            return true;
        }
        public string SaveOrUpdate(MobifiberDevelopmentUnit obj)
        {
            try
            {
                MobifiberDevelopmentUnit objTemp = _context.MobifiberDevelopmentUnits.FirstOrDefault(_o => _o.DevelopId == obj.DevelopId);
                if (objTemp == null)
                {
                    MobifiberDevelopmentUnit objCheck = _context.MobifiberDevelopmentUnits.FirstOrDefault(_o => _o.DevelopCode == obj.DevelopCode);

                    if (objCheck != null)
                    {
                        return Constant.CODE_EXISTS;
                    }
                    _context.MobifiberDevelopmentUnits.Add(obj);
                }
                else
                {
                    var check = 0;
                    List<MobifiberDevelopmentUnit> objCheck = _context.MobifiberDevelopmentUnits.ToList();
                    foreach (var item in objCheck)
                    {
                        if (item.DevelopId != obj.DevelopId && item.DevelopCode == obj.DevelopCode)
                        {
                            check++;
                        }
                    }

                    if (check > 0)
                    {
                        return Constant.CODE_EXISTS;
                    }

                    objTemp.DevelopName = obj.DevelopName;
                    objTemp.DevelopCode = obj.DevelopCode;
                    objTemp.Address = obj.Address;
                    objTemp.TaxCode = obj.TaxCode;
                    objTemp.PhoneNumber = obj.PhoneNumber;
                    objTemp.Description = obj.Description;

                    _context.MobifiberDevelopmentUnits.Update(objTemp);
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
