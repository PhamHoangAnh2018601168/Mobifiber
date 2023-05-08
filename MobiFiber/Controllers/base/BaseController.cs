using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Micro.Web;

namespace WebApplication.Controllers
{
    public class BaseController : Controller
    {
        private IConfiguration config;
        public BaseController(IConfiguration iconfig)
        {
            config = iconfig;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                if (HttpContext.Session.GetString("SessionUser") != null)
                {
                    var user = JsonConvert.DeserializeObject<UserLogin>(HttpContext.Session.GetString("SessionUser"));
                    ViewBag.Session = user;
                }
            }
        }
    }
}