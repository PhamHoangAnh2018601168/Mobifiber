using Dapper;
using MobiFiber.Code;
using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.DAO
{
    public class Dashboard_DAO
    {
        private MobiFiberContext _context = null;
        private string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Dashboard_DAO()
        {
            _context = new MobiFiberContext();
        }

        public List<dynamic> GetDataDashboard()
        {
            List<dynamic> lstObj = new List<dynamic>();
            try
            {
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<dynamic>("Mobifiber_GetDataDashboard", commandType: CommandType.StoredProcedure).ToList();
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
