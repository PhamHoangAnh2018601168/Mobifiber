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
    public class Agents_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Agents_DAO()
        {
            _context = new MobiFiberContext();
        }
        public bool Delete(int Id)
        {
            MobifiberAgent objTemp = _context.MobifiberAgents.First(_o => _o.AmId == Id);
            objTemp.Status = 1;
            _context.MobifiberAgents.Update(objTemp);
            _context.SaveChanges();
            return true;
        }
        public List<MobifiberAgent> ListAll()
        {
            List<MobifiberAgent> lstObj = new List<MobifiberAgent>();
            try
            {
                string sql = "SELECT * FROM Mobifiber_Agents WHERE Status = 0 ";

                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberAgent>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
            return lstObj;
        }
        public string SaveOrUpdate(MobifiberAgent obj)
        {
            try
            {
                MobifiberAgent objTemp = _context.MobifiberAgents.FirstOrDefault(_o => _o.AmId == obj.AmId);
                if (objTemp == null)
                {
                    MobifiberAgent objCheck = _context.MobifiberAgents.FirstOrDefault(_o => _o.AgentCode == obj.AgentCode );

                    if (objCheck != null)
                    {
                        return Constant.CODE_EXISTS;
                    }
                    _context.MobifiberAgents.Add(obj);
                }
                else
                {
                    var check = 0;
                    List<MobifiberAgent> objCheck = _context.MobifiberAgents.ToList();
                    foreach (var item in objCheck)
                    {
                        if (item.AmId != obj.AmId && item.AgentCode == obj.AgentCode)
                        {
                            check++;
                        }
                    }

                    if (check > 0)
                    {
                        return Constant.CODE_EXISTS;
                    }

                    objTemp.AgentsName = obj.AgentsName;
                    objTemp.AgentCode = obj.AgentCode;
                    objTemp.Address = obj.Address;
                    objTemp.TaxCode = obj.TaxCode;
                    objTemp.PhoneNumber = obj.PhoneNumber;
                    objTemp.Description = obj.Description;

                    _context.MobifiberAgents.Update(objTemp);
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
