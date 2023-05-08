using Micro.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MobiFiber.Code;
using MobiFiber.DAO;
using MobiFiber.PartialViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using MobiFiber.Models;

namespace MobiFiber.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Index
        [UserAuthorize(new ActionModule[] { ActionModule.ReprotRegisteredCustomers }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult RegisteredCustomers()
        {
            return View("RegisteredCustomers");
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceCostAllocation }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult DeviceCostAllocation()
        {
            return View("DeviceCostAllocation");
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ServiceRevenueAllocation }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult ServiceRevenueAllocation()
        {
            return View("ServiceRevenueAllocation");
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceStatus }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult DeviceStatus()
        {
            return View("DeviceStatus");
        }
        #endregion

        #region Search
        [UserAuthorize(new ActionModule[] { ActionModule.ReprotRegisteredCustomers }, new ActionTypeCustom[] { ActionTypeCustom.Report })]
        public JsonResult GetRegisteredCustomers(int offset, int limit, string fromDate, string toDate, string keySearch)  // fromDate ->signdate , keySearch -> ten khach hang
        {
            Report_DAO report_DAO = new Report_DAO();
            List<RegisteredCustomers> registeredCustomers = report_DAO.GetRegisteredCustomers(fromDate, toDate, keySearch).OrderBy(o => o.SignDate).ToList();

            List<string> lstSignDate = registeredCustomers.Select(o => o.SignDate.ToString("MM/yyyy")).ToList();

            var dataChart = from p in lstSignDate
                            group p by p into g
                            select new { Month = g.Key, Counter = g.Count() };
            DateTime date;
            DateTime.TryParseExact(fromDate, "dd/MM/yyyy", null, DateTimeStyles.None, out date);
            DateTime StartMonth = date;
            DateTime.TryParseExact(toDate, "dd/MM/yyyy", null, DateTimeStyles.None, out date);
            DateTime EndMonth = date;

            List<DateTime> lstMonthGenarate = GetMonthsBetween(StartMonth, EndMonth);

            List<string> lstMonth = dataChart.Select(o => o.Month).ToList();

            //foreach (DateTime item in lstMonthGenarate)
            //{
            //    if (!lstMonth.Contains(item.ToString("MM/yyyy")))
            //    {
            //        dataChart.Append(new { Month = item.ToString("MM/yyyy"), Counter = 0 });
            //    }
            //}


            lstMonth = dataChart.Select(o => o.Month).ToList();
            List<int> lstDataMonth = dataChart.Select(o => o.Counter).ToList();

            return Json(new { status = true, dataChart = dataChart, lstMonth = lstMonth, lstDataMonth = lstDataMonth, rows = registeredCustomers.Skip(offset).Take(limit), mess = "Thành công", total = registeredCustomers.Count() });
        }

        public static List<DateTime> GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                monthDiff -= 1;
            }

            List<DateTime> results = new List<DateTime>();
            for (int i = monthDiff; i >= 1; i--)
            {
                results.Add(to.AddMonths(-i));
            }

            return results;
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceCostAllocation }, new ActionTypeCustom[] { ActionTypeCustom.Report })]
        public JsonResult GetDeviceCostAllocation(int offset, int limit, string fromDate, string toDate, string keySearch, int IsActiveStatus)  //keySearch ten thiet bi,  date -> date active
        {
            Report_DAO report_DAO = new Report_DAO();
            List<DeviceCostAllocation> deviceCostAllocation = report_DAO.GetDeviceCostAllocation(fromDate, toDate, keySearch, IsActiveStatus);
            return Json(new { status = true, rows = deviceCostAllocation.Skip(offset).Take(limit), total = deviceCostAllocation.Count(), mess = "Thành công" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ServiceRevenueAllocation }, new ActionTypeCustom[] { ActionTypeCustom.Report })]
        public JsonResult GetServiceRevenueAllocation(int offset, int limit, string fromDate, string toDate, string keySearch) //keysearch -> so hop dong, toDate -> signdate
        {
            Report_DAO report_DAO = new Report_DAO();
            List<ServiceRevenueAllocation> serviceRevenueAllocation = report_DAO.GetServiceRevenueAllocation(fromDate, toDate, keySearch);
            return Json(new { status = true, rows = serviceRevenueAllocation.Skip(offset).Take(limit), total = serviceRevenueAllocation.Count(), mess = "Thành công" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceStatus }, new ActionTypeCustom[] { ActionTypeCustom.Report })]
        public JsonResult GetDeviceStatus(int offset, int limit, string fromDate, string toDate, int ActiveStatus, string keySearch) // date-> signdate, keySearch -> ten thiet bi
        {
            if (ActiveStatus == 0)
            {
                Report_DAO report_DAO = new Report_DAO();
                List<PartialViewModel.DeviceStatus> deviceStatus = report_DAO.GetDeviceStatus(fromDate, toDate, keySearch);
                return Json(new { status = true, rows = deviceStatus.Skip(offset).Take(limit), total = deviceStatus.Count(), mess = "Thành công" });
            }
            else
            {
                Report_DAO report_DAO = new Report_DAO();
                List<DeviceStatusNoActive> deviceStatus = report_DAO.GetDeviceNoActive(fromDate, toDate, keySearch, ActiveStatus);
                return Json(new { status = true, rows = deviceStatus.Skip(offset).Take(limit), total = deviceStatus.Count(), mess = "Thành công" });
            }

        }
        #endregion

        #region export data
        [UserAuthorize(new ActionModule[] { ActionModule.ReprotRegisteredCustomers }, new ActionTypeCustom[] { ActionTypeCustom.Export })]
        public JsonResult ExportRegisteredCustomers(string fromDate, string toDate, string keySearch)  // fromDate ->signdate , keySearch -> ten khach hang
        {
            Report_DAO report_DAO = new Report_DAO();
            List<RegisteredCustomers> registeredCustomers = report_DAO.GetRegisteredCustomers(fromDate, toDate, keySearch);
            RegisteredCustomers total = new RegisteredCustomers();
            if (registeredCustomers == null || registeredCustomers.Count == 0)
            {
                return Json(new { status = false, mess = "Không có dữ liệu" });
            }
            decimal totalprice = 0; // tổng tiền
            for (int i = 0; i < registeredCustomers.Count; i++)
            {
                totalprice += registeredCustomers[i].PriceVAT;
            }
            total.CustomerName = "Tổng";
            total.PriceVAT = totalprice;

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\RegisteredCustomers.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "BC_KH_DK_DV_Mobifiber_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";

            try
            {
                MemoryStream st = new MemoryStream();
                using (ExcelTemplateHelper helper = new ExcelTemplateHelper(urlTemplate, st))
                {
                    helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                    helper.CurrentSheetName = "Sheet2";
                    helper.TempSheetName = "Sheet1";
                    helper.CurrentPosition = new CellPosition("A1");

                    var temp_top = helper.CreateTemplate("top");
                    var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");
                    var temp_footer = helper.CreateTemplate("footer");

                    helper.InsertData(temp_top, "");
                    helper.InsertData(temp_info, new { info = "Từ ngày " + fromDate + " - đến ngày " + toDate });
                    helper.Insert(temp_column);

                    helper.InsertDatas(temp_item, registeredCustomers);
                    helper.InsertData(temp_footer, total);
                }
                FileStream fileStream = new FileStream($@"{pathSave}\{fileName}", FileMode.Create, System.IO.FileAccess.Write);
                st.WriteTo(fileStream);
                fileStream.Close();
                return Json(new
                {
                    status = true,
                    IsCreateExel = true,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mess = "Lỗi!" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceCostAllocation }, new ActionTypeCustom[] { ActionTypeCustom.Export })]
        public JsonResult ExportDeviceCostAllocation(string fromDate, string toDate, string keySearch, int IsActiveStatus)  //keySearch ten thiet bi,  date -> date active
        {
            Report_DAO report_DAO = new Report_DAO();
            List<DeviceCostAllocation> deviceCostAllocation = report_DAO.GetDeviceCostAllocation(fromDate, toDate, keySearch, IsActiveStatus);
            DeviceCostAllocation total = new DeviceCostAllocation();

            if (deviceCostAllocation == null || deviceCostAllocation.Count == 0)
            {
                return Json(new { status = false, mess = "Không có dữ liệu" });
            }
            decimal totalprice = 0; // tổng tiền thiết bị
            decimal totalallocation = 0; //tổng số dư còn phải phân bổ
            decimal totalalloca = 0;// Tổng số chi phí phân bổ
            decimal totalinit = 0; // tổng phát sinh
            decimal totalsd = 0;//Tổng số dư chuyển sang
            for (int i = 0; i < deviceCostAllocation.Count; i++)
            {
                totalprice += deviceCostAllocation[i].DevicePrice;
                totalallocation += deviceCostAllocation[i].SoDuConPhaiPhanBo;
                totalinit += deviceCostAllocation[i].SoPhatSinhMoiTrongKy;
                totalalloca += deviceCostAllocation[i].ChiPhiPhanBo;
                totalsd += deviceCostAllocation[i].SoDuThangTruocChuyenSang;
            }
            total.DeviceName = "Tổng";
            total.DevicePrice = totalprice;
            total.ChiPhiPhanBo = totalalloca;
            total.SoPhatSinhMoiTrongKy = totalinit;
            total.SoDuConPhaiPhanBo = totalallocation;
            total.SoDuThangTruocChuyenSang = totalsd;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\DeviceCostAllocation.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "BC_TD_PB_CP_TB_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";

            try
            {
                MemoryStream st = new MemoryStream();
                using (ExcelTemplateHelper helper = new ExcelTemplateHelper(urlTemplate, st))
                {
                    helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                    helper.CurrentSheetName = "Sheet2";
                    helper.TempSheetName = "Sheet1";
                    helper.CurrentPosition = new CellPosition("A1");
                    var temp_top = helper.CreateTemplate("top");
                    var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");
                    var temp_footer = helper.CreateTemplate("footer");

                    helper.InsertData(temp_top, "");
                    helper.InsertData(temp_info, new { info = "Từ ngày " + fromDate + " - đến ngày " + toDate });
                    helper.Insert(temp_column);

                    helper.InsertDatas(temp_item, deviceCostAllocation);
                    helper.InsertData(temp_footer, total);
                }
                FileStream fileStream = new FileStream($@"{pathSave}\{fileName}", FileMode.Create, System.IO.FileAccess.Write);
                st.WriteTo(fileStream);
                fileStream.Close();
                return Json(new
                {
                    status = true,
                    IsCreateExel = true,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mess = "Lỗi!" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ServiceRevenueAllocation }, new ActionTypeCustom[] { ActionTypeCustom.Export })]
        public JsonResult ExportServiceRevenueAllocation(string fromDate, string toDate, string keySearch) //keysearch -> so hop dong, toDate -> signdate
        {
            Report_DAO report_DAO = new Report_DAO();
            List<ServiceRevenueAllocation> serviceRevenueAllocation = report_DAO.GetServiceRevenueAllocation(fromDate, toDate, keySearch);
            ServiceRevenueAllocation total = new ServiceRevenueAllocation();
            if (serviceRevenueAllocation == null || serviceRevenueAllocation.Count == 0)
            {
                return Json(new { status = false, mess = "Không có dữ liệu" });
            }
            decimal totalprice = 0; // tổng tiền 
            decimal totalallocation = 0; //tổng số dư còn phải phân bổ
            decimal totalalloca = 0;// Tổng số chi phí phân bổ
            decimal totalinit = 0; // tổng phát sinh
            decimal totalsd = 0;//Tổng số dư chuyển sang
            for (int i = 0; i < serviceRevenueAllocation.Count; i++)
            {
                totalprice += serviceRevenueAllocation[i].Price;
                totalallocation += serviceRevenueAllocation[i].SoDuConPhaiPhanBo;
                totalinit += serviceRevenueAllocation[i].SoPhatSinhMoiTrongKy;
                totalalloca += serviceRevenueAllocation[i].DoanhThuPhanBoTrongKy;
                totalsd += serviceRevenueAllocation[i].SoDuThangTruocChuyenSang;
            }
            total.ContractNumber = "Tổng";
            total.Price = totalprice;
            total.DoanhThuPhanBoTrongKy = totalalloca;
            total.SoPhatSinhMoiTrongKy = totalinit;
            total.SoDuConPhaiPhanBo = totalallocation;
            total.SoDuThangTruocChuyenSang = totalsd;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\ServiceRevenueAllocation.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "BC_TD_PB_DT_DV_FTTH_Mobifiber_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";

            try
            {
                MemoryStream st = new MemoryStream();
                using (ExcelTemplateHelper helper = new ExcelTemplateHelper(urlTemplate, st))
                {
                    helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                    helper.CurrentSheetName = "Sheet2";
                    helper.TempSheetName = "Sheet1";
                    helper.CurrentPosition = new CellPosition("A1");

                    var temp_top = helper.CreateTemplate("top");
                    var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");
                    var temp_footer = helper.CreateTemplate("footer");

                    helper.InsertData(temp_top, "");
                    helper.InsertData(temp_info, new { info = "Từ ngày " + fromDate + " - đến ngày " + toDate });
                    helper.Insert(temp_column);

                    helper.InsertDatas(temp_item, serviceRevenueAllocation);
                    helper.InsertData(temp_footer, total);
                }
                FileStream fileStream = new FileStream($@"{pathSave}\{fileName}", FileMode.Create, System.IO.FileAccess.Write);
                st.WriteTo(fileStream);
                fileStream.Close();
                return Json(new
                {
                    status = true,
                    IsCreateExel = true,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mess = "Lỗi!" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceStatus }, new ActionTypeCustom[] { ActionTypeCustom.Export })]
        public JsonResult ExportDeviceStatus(string fromDate, string toDate, int ActiveStatus, string keySearch) // date-> signdate, keySearch -> ten thiet bi
        {
            Report_DAO report_DAO = new Report_DAO();
            if (ActiveStatus == 0)
            {
                List<PartialViewModel.DeviceStatus> deviceStatus = report_DAO.GetDeviceStatus(fromDate, toDate, keySearch);
                if (deviceStatus == null || deviceStatus.Count == 0)
                {
                    return Json(new { status = false, mess = "Không có dữ liệu" });
                }

                foreach (PartialViewModel.DeviceStatus item in deviceStatus)
                {
                    item.StatusName = Common.GetEnumDescription((Code.DeviceStatus)item.Status);
                }
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\DeviceStatus.xlsx");
                string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string fileName = "BC_TinhTrang_TB_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";
                MemoryStream st = new MemoryStream();
                using (ExcelTemplateHelper helper = new ExcelTemplateHelper(urlTemplate, st))
                {
                    helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                    helper.CurrentSheetName = "Sheet2";
                    helper.TempSheetName = "Sheet1";
                    helper.CurrentPosition = new CellPosition("A1");

                    var temp_top = helper.CreateTemplate("top");
                    var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");

                    helper.InsertData(temp_top, "");
                    helper.InsertData(temp_info, new { info = "Từ ngày " + fromDate + " - đến ngày " + toDate });
                    helper.Insert(temp_column);
                    helper.InsertDatas(temp_item, deviceStatus);
                }
                FileStream fileStream = new FileStream($@"{pathSave}\{fileName}", FileMode.Create, System.IO.FileAccess.Write);
                st.WriteTo(fileStream);
                fileStream.Close();
                return Json(new
                {
                    status = true,
                    IsCreateExel = true,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
            else
            {
                List<DeviceStatusNoActive> deviceStatus = report_DAO.GetDeviceNoActive(fromDate, toDate, keySearch, ActiveStatus);
                if (deviceStatus == null || deviceStatus.Count == 0)
                {
                    return Json(new { status = false, mess = "Không có dữ liệu" });
                }
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\DeviceStatusNoActive.xlsx");
                string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string fileName = "BC_TinhTrang_TB_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";
                MemoryStream st = new MemoryStream();
                using (ExcelTemplateHelper helper = new ExcelTemplateHelper(urlTemplate, st))
                {
                    helper.Direction = ExcelTemplateHelper.DirectionType.TOP_TO_DOWN;
                    helper.CurrentSheetName = "Sheet2";
                    helper.TempSheetName = "Sheet1";
                    helper.CurrentPosition = new CellPosition("A1");

                    var temp_top = helper.CreateTemplate("top");
                    var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");

                    helper.InsertData(temp_top, "");
                    helper.InsertData(temp_info, new { info = "Từ ngày " + fromDate + " - đến ngày " + toDate });
                    helper.Insert(temp_column);
                    helper.InsertDatas(temp_item, deviceStatus);
                }
                FileStream fileStream = new FileStream($@"{pathSave}\{fileName}", FileMode.Create, System.IO.FileAccess.Write);
                st.WriteTo(fileStream);
                fileStream.Close();
                return Json(new
                {
                    status = true,
                    IsCreateExel = true,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
        }
        #endregion
    }
}
