using Dapper;
using Microsoft.AspNetCore.Mvc;
using MobiFiber.DAO;
using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.Code
{
    public static class WriteLogToDatabase
    {
        private static string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static JsonResult AddLog(int Type, string Description, Guid CreateBy, DateTime DateCreate,int Action, int IdRefer, string FieldChange, string NewValue, string OldValue)
        {
            MobifiberHistory logToDatabase = new MobifiberHistory();
            History_DAO history_DAO = new History_DAO();
            logToDatabase.Type = Type;
            logToDatabase.Description = Description;
            logToDatabase.CreateBy = CreateBy;
            logToDatabase.DateCreate = DateCreate;
            logToDatabase.Action = Action;
            logToDatabase.IdRefer = IdRefer;
            logToDatabase.FieldChange = FieldChange;
            logToDatabase.NewValue = NewValue;
            logToDatabase.OldValue = OldValue;
            string result = history_DAO.Save(logToDatabase);
            return new JsonResult( result);
        }
        public static List<MobifiberHistory> GetLogDatabase(int Type, int IdRefer)
        {
            List<MobifiberHistory> lstObj = new List<MobifiberHistory>();
            try
            {
                var dt = new DataTable();
                using (var connection = new SqlConnection(connectString))
                {
                    lstObj = connection.Query<MobifiberHistory>("MobiFiber_GetLogHistory",
                        new []{ 
                            new { 
                                Type = dt.AsTableValuedParameter("Type") 
                            } 
                        },
                        commandType: CommandType.StoredProcedure).ToList();
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
