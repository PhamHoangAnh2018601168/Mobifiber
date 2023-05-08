using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace MobiFiber.Code
{
    public static class Logger
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(Logger));
        public static void LogError(string controllerAction, string function, string msg, Exception ex)
        {
            _log4net.Error(controllerAction + " - " + function + " - "+ msg, ex);
        }
        public static void LogInfo(string controllerAction,string function, string msg)
        {
            _log4net.Info(controllerAction + " - "+ function + " - " + msg);
        }
        public static void LogInfo(string msg)
        {
            _log4net.Info(msg);
        }
        public static void LogError(string msg, Exception ex)
        {
            _log4net.Info(msg, ex);
        }
    }
}
