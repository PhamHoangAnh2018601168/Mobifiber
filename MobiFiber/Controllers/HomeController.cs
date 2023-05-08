using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobiFiber.Code;
using MobiFiber.DAO;
using MobiFiber.Models;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers;

namespace Micro.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IConfiguration config;
        public HomeController(IConfiguration iconfig) : base(iconfig)
        {
            config = iconfig;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.Home }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult Index()
        {
            Logger.LogInfo("HomeController", "Index","Go to Home/Index");
            return View();
        }

        #region JsonResult
        public JsonResult GetDashboard()
        {
            Dashboard_DAO _d = new Dashboard_DAO();
            Device_DAO _device = new Device_DAO();
            Package_DAO _package = new Package_DAO();
            Contract_DAO _contract = new Contract_DAO();
            List<dynamic> lst =  _d.GetDataDashboard();

            List<MobifiberDevice>   getAllDevice        = _device.GetAllDeviceUsingQuery();
            List<MobifiberPackage>  getAllPackage       = _package.GetAllPackageUsingStoreProcedure();
            List<MobifiberContract>  getAllContract     = _contract.GetAllContractUsingQuery();

            #region Device
            List<dynamic> lstDevice = new List<dynamic>();
            lstDevice.Add(new
            {
                Name = "Thực hiện hợp đồng",
                Value = getAllDevice.Count(_o => _o.Status == (int)DeviceStatus.Linked)
            });
            lstDevice.Add(new
            {
                Name = "Trong kho",
                Value = getAllDevice.Count(_o => _o.Status == (int)DeviceStatus.NotLinked)
            });
            lstDevice.Add(new
            {
                Name = "Đã bán",
                Value = getAllDevice.Count(_o => _o.Status == (int)DeviceStatus.Buy)
            });
            #endregion

            #region Package
            List<dynamic> lstPackage = new List<dynamic>();
            lstPackage.Add(new
            {
                Name = "Còn hiệu lực",
                Value = getAllPackage.Count(_o => _o.Status == (int)PakageStatus.Active)
            });
            lstPackage.Add(new
            {
                Name = "Hết hiệu lực",
                Value = getAllPackage.Count(_o => _o.Status == (int)PakageStatus.DeActive)
            });
            #endregion

            #region Contract
            List<dynamic> lstContract = new List<dynamic>();
            lstContract.Add(new
            {
                Name = "Còn hiệu lực",
                Value = getAllContract.Count(_o => _o.Status == 0)
            });
            lstContract.Add(new
            {
                Name = "Hết hiệu lực",
                Value = getAllContract.Count(_o => _o.Status == 1)
            });
            #endregion

            dynamic dataResult = new
            {
                Stats = lst,
                Device = lstDevice,
                Package = lstPackage,
                Contract = lstContract
            };
            return Json(new { status = true, data = dataResult, mess = "Không thành công !" });
        }
        #endregion
    }
}
