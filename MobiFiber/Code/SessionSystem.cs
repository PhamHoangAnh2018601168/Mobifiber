using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Micro.Web.PartialViewModel;

namespace Micro.Web.Code
{
    public static class SessionSystem
    {
        public static string sessionName = "GlobalEnglish.Manager";
        public static string sessionFrontEnd = "GlobalEnglish.User";
        public static string sessionManagerRole = "GlobalEnglish.ManagerRole";
        public static UserSession userSession;

        public static UserSession GetUser(this ISession session, string sessionName)
        {
            var value = session.GetString(sessionName);
            return !string.IsNullOrEmpty(value) ? new UserSession() : JsonConvert.DeserializeObject<UserSession>(value);
        }

        public static bool SetUser(this ISession session, UserSession user, string sessionName)
        {
            var value = JsonConvert.SerializeObject(user);
            session.SetString(sessionName, value);
            return false;
        }

        public static void RemoveSession(this ISession session, string sessionName)
        {
            session.Remove(sessionName);
        }

        public static void ClearSession(this ISession session)
        {
            session.Clear();            
        }
    }
}
