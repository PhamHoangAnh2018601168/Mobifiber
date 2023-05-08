using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateNetCore.Controllers
{
    public class UnauthorisedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
