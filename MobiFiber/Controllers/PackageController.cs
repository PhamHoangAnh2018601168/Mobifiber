using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Micro.Web;
using MobiFiber.DAO;
using MobiFiber.Models;
using MobiFiber.Code;
using Micro.Web.Code;
using System;
using Newtonsoft.Json;
using MobiFiber.PartialViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using MobiFiber.Controllers;
using AutoMapper;


namespace TemplateNetCore.Controllers
{
    public class PackageController : Controller
    {
        private string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd/MMM/yy", "dd/MMM/yyyy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy", "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy", "dd-mmm-yy", "dd-mmm-yyyy", "dd-MMM-yy", "dd-MMM-yyyy" };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper mapper;
        public PackageController(IWebHostEnvironment webHostEnvironment, IMapper _mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            mapper = _mapper;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult Index()
        {
            return View();
        }

        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetDataPackage(int status, string search, int offset, int limit)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            Package_DAO dal = new Package_DAO();
            var total = 0;
            List<MobifiberPackage> lstAllData = dal.GetAllPackageUsingQuery(ref total, status, search, offset, limit).ToList();
            return Json(new { status = true, rows = lstAllData, total = total, mess = "Success" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.Add })]
        public JsonResult Create(string PackageName, string PackageNumber, string Decision, int? TimeUsed = 0, int? PromotionTime = 0, decimal? PricePackage = 0, decimal? PricePackageVAT = 0, int? PackageStatus = 0)
        {
            //var checkrole = SettingController.CheckLockRole(0, 2, (DateTime.Now.ToString("dd/MM/yyyy")));
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_PACKAGE)20) });
            //}
            int isVal = ValidatePackage(0, PackageName, PackageNumber, TimeUsed.Value, PricePackage.Value, PricePackageVAT.Value, PackageStatus.Value);
            if (isVal != (int)ERR_VALIDATE_PACKAGE.ERR_0)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_PACKAGE)isVal) });
            }

            var sessUser = SessionSystem.userSession;
            Package_DAO dal = new Package_DAO();
            MobifiberPackage obj = new MobifiberPackage();
            obj.PackageName = PackageName;
            obj.PackageNumber = PackageNumber;
            obj.Decision = Decision;
            obj.TimeUsed = TimeUsed.Value;
            obj.PromotionTime = PromotionTime.Value;
            obj.Price = PricePackage.Value;
            obj.PriceVat = PricePackageVAT.Value;
            obj.Status = PackageStatus.Value;
            obj.UserCreate = sessUser.UserId;
            obj.DateCreate = DateTime.Now;
            string result = dal.SaveOrUpdate(obj);
            WriteLogToDatabase.AddLog(
                        (int)ActionModule.PackageManager,
                        Constant.CREATE,
                        sessUser.UserId,
                        DateTime.Now,
                        (int)ActionTypeCustom.Add,
                        obj.PackageId,
                        "",
                        "",
                        ""
                        );
            //WriteLogToDatabase.AddLog((int)ActionModule.PackageManager, sessUser.UserName + " Create", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Add, obj.PackageId);
            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, mess = "Không thành công, Tên gói cước đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công !" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult Update(int? Id = 0, string? PackageName = "", string? PackageNumber = "", string? Decision = "", int? TimeUsed = 0, int? PromotionTime = 0, decimal? PricePackage = 0, decimal? PricePackageVAT = 0, int? PackageStatus = 0)
        {
            //var checkrole = SettingController.CheckLockRole(Id.Value, 2, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_PACKAGE)20) });
            //}
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Gói cước không tồn tại !" });
            }
            int isVal = ValidatePackage(Id.Value, PackageName, PackageNumber, TimeUsed.Value, PricePackage.Value, PricePackageVAT.Value, PackageStatus.Value);
            if (isVal != (int)ERR_VALIDATE_PACKAGE.ERR_0)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_PACKAGE)isVal) });
            }
            var sessUser = SessionSystem.userSession;
            Package_DAO dal = new Package_DAO();
            MobifiberPackage obj = dal.GetById(Id.Value);
            MobifiberPackage current = Common.clone<MobifiberPackage>(obj);
            obj.PackageId = Id.Value;
            obj.PackageName = PackageName;
            obj.PackageNumber = PackageNumber;
            obj.Decision = Decision;
            obj.TimeUsed = TimeUsed.Value;
            obj.PromotionTime = PromotionTime.Value;
            obj.Price = PricePackage.Value;
            obj.PriceVat = PricePackageVAT.Value;
            obj.Status = PackageStatus.Value;
            obj.UserLastUpdate = sessUser.UserId;
            obj.DateLastUpdate = DateTime.Now;
            foreach (var prop in current.GetType().GetProperties())
            {
                if (Constant.LIST_PROPERTY_IGNOR_WRITELOG.FirstOrDefault(o => o.ToUpper() == prop.Name.ToUpper()) != null)
                {
                    continue;
                }

                object _oldValue = Common.GetPropValue(current, prop.Name);
                object _newValue = Common.GetPropValue(obj, prop.Name);
                //typeof(current) == typeof(MobifiberDevice)
                //    type = (int)
                if (_newValue != null)
                {
                    if (_oldValue != null)
                    {
                        bool isChange = false;
                        if (_oldValue.GetType() == typeof(float))
                        {
                            isChange = (float)_oldValue != (float)_newValue;
                        }
                        else if (_oldValue.GetType() == typeof(DateTime))
                        {
                            // Convert datetime
                            isChange = (DateTime)_oldValue != (DateTime)_newValue;
                        }
                        else if (_oldValue.GetType() == typeof(Decimal))
                        {
                            isChange = (decimal)_oldValue != (decimal)_newValue;
                        }
                        else if (_oldValue.GetType() == typeof(int))
                        {
                            isChange = (int)_oldValue != (int)_newValue;
                        }
                        else if (_oldValue.GetType() == typeof(string))
                        {
                            // Convert datetime
                            isChange = (string)_oldValue != (string)_newValue;
                        }

                        if (isChange)
                        {

                            // todo insert.
                            string strFieldChange = prop.Name;
                            string oldValue = Common.GetPropValue(current, prop.Name).ToString();
                            string newValue = Common.GetPropValue(obj, prop.Name).ToString();
                            if (newValue == "0")
                            {
                                newValue = PackageStatus.ToString();
                            }
                            WriteLogToDatabase.AddLog(
                                    (int)ActionModule.PackageManager,
                                    Constant.UPDATE,
                                    sessUser.UserId,
                                    DateTime.Now,
                                    (int)ActionTypeCustom.Edit,
                                    obj.PackageId,
                                    strFieldChange,
                                    newValue,
                                    oldValue
                                    );
                        }
                    }
                }

            }
            string result = dal.SaveOrUpdate(obj);
            //WriteLogToDatabase.AddLog((int)ActionModule.PackageManager, sessUser.UserName + " Update", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Edit, obj.PackageId);
            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, mess = "Không thành công, Tên gói cước đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công !" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.Delete })]
        public JsonResult Delete(int Id)
        {
            var sessUser = SessionSystem.userSession;
            Package_DAO dal = new Package_DAO();
            MobifiberPackage obj = new MobifiberPackage();
            obj = dal.GetById(Id);
            obj.Status = (int)PakageStatus.Delete;
            obj.UserLastUpdate = sessUser.UserId;
            obj.DateLastUpdate = DateTime.Now;
            WriteLogToDatabase.AddLog(
                        (int)ActionModule.PackageManager,
                        Constant.DELETE,
                        sessUser.UserId,
                        DateTime.Now,
                        (int)ActionTypeCustom.Delete,
                        obj.PackageId,
                        "",
                        "",
                        ""
                        );
            dal.SaveOrUpdate(obj);
            //WriteLogToDatabase.AddLog((int)ActionModule.PackageManager, sessUser.UserName + " Delete", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Delete, obj.PackageId);
            return Json(new { status = true, mess = "Thành công" });
        }

        public JsonResult GetDataPackageById(int Id)
        {
            //var checkrole = SettingController.CheckLockRole(Id, 2, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_PACKAGE)20) });
            //}
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Gói cước không tồn tại !" });
            }

            Package_DAO package_DAO = new Package_DAO();
            MobifiberPackage mobifiberPackage = package_DAO.GetById(Id);

            List<dynamic> ListStatus = new List<dynamic>();
            ListStatus.Add(new { IdStatus = (int)PakageStatus.Active, NameStatus = "Còn hiệu Lực" });
            ListStatus.Add(new { IdStatus = (int)PakageStatus.DeActive, NameStatus = "Hết hiệu lực" });

            if (mobifiberPackage != null)
            {

                return Json(new { status = true, data = mobifiberPackage, lstStatus = ListStatus, mess = "Thành công !" });
            }
            else
            {
                return Json(new { status = false, mess = "Gói cước không tồn tại !" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetHistoriesPackageById(int Id, int offset, int limit)
        {
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Mã Gói cước không tồn tại !" });
            }
            Package_DAO package_DAO = new Package_DAO();
            MobifiberPackage mobifiberPackage = package_DAO.GetById(Id);
            History_DAO history_DAO = new History_DAO();
            List<HistoryPackageView> lstdata = history_DAO.GetPackageHistoryUsingStoreProcedure(Id);
            var total = lstdata.Count;
            return Json(new { status = true, rows = lstdata.Skip(offset).Take(limit), Packageinfo = mobifiberPackage, total = total, mess = "Success" });
        }
        private int ValidatePackage(int Id, string PackageName, string PackageNumber, int TimeUsed, decimal PricePackage, decimal PricePackageVAT, int PackageStatus)
        {
            if (string.IsNullOrEmpty(PackageName))
            {
                return (int)ERR_VALIDATE_PACKAGE.ERR_1;
            }
            if (TimeUsed <= 0)
            {
                return (int)ERR_VALIDATE_PACKAGE.ERR_7;
            }
            if (PricePackage <= 0)
            {
                return (int)ERR_VALIDATE_PACKAGE.ERR_8;
            }
            if (PricePackageVAT <= 0)
            {
                return (int)ERR_VALIDATE_PACKAGE.ERR_9;
            }
            Package_DAO package_DAO = new Package_DAO();
            List<MobifiberPackage> mobifiberPackage = package_DAO.GetAllPackageUsingStoreProcedure();
            foreach (var item in mobifiberPackage)
            {
                if (item.PackageId != Id && item.PackageNumber == PackageNumber)
                {
                    return (int)ERR_VALIDATE_PACKAGE.ERR_10;
                }
            }
            return (int)ERR_VALIDATE_PACKAGE.ERR_0;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        public JsonResult Import()
        {

            List<PackageViewModel> lstPackage = new List<PackageViewModel>();
            List<PackageViewModel> lstPackageInvalid = new List<PackageViewModel>();
            IFormFile file = Request.Form.Files[0];
            string folderName = "Output";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                }
                lstPackage = processFile(sheet, ref lstPackageInvalid, true);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return Json(new { status = true, mess = "Upload thành công. Kiểm tra dữ liệu trước khi lưu", CountDone = lstPackage.Count(), CountError = lstPackageInvalid.Count(), lstValid = lstPackage, lstInValid = GetErrStr(lstPackageInvalid) });
        }
        private List<PackageViewModel> processFile(ISheet sheet, ref List<PackageViewModel> lstfail, bool checkInvalid = true)
        {
            Package_DAO package_DAO = new Package_DAO();
            List<MobifiberPackage> ListAllPAckage = package_DAO.GetAllPackageUsingStoreProcedure();
            List<PackageViewModel> lstDone = new List<PackageViewModel>();
            IRow headerRow = sheet.GetRow(0); //Get Header Row
            int cellCount = headerRow.LastCellNum;
            int startPoint = 2;
            for (int i = (sheet.FirstRowNum + startPoint); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                PackageViewModel obj = new PackageViewModel();
                obj.IsValid = true;
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null && row.GetCell(j).ToString().Trim() != "")
                    {
                        switch (j)
                        {
                            case 0:
                                obj.PackageName = row.GetCell(j).ToString().Trim();
                                break;
                            case 1:
                                obj.PackageNumber = row.GetCell(j).ToString().Trim();
                                break;
                            case 2:
                                obj.Decision = row.GetCell(j).ToString().Trim();
                                break;
                            case 3:
                                int TimeUsed;
                                if (int.TryParse(row.GetCell(j).ToString().Trim(), out TimeUsed))
                                {
                                    obj.TimeUsed = TimeUsed;
                                }
                                else
                                {
                                    obj.TimeUsed = TimeUsed;
                                    obj.IsValid = false;
                                    obj.ErrorTimeUsed = (int)Error.Wrongformat;
                                }
                                break;
                            case 4:
                                int PromotionTime;
                                if (int.TryParse(row.GetCell(j).ToString().Trim(), out PromotionTime))
                                {
                                    obj.PromotionTime = PromotionTime;
                                }
                                else
                                {
                                    obj.PromotionTime = PromotionTime;
                                    obj.IsValid = false;
                                    obj.ErrorPromotionTime = (int)Error.Wrongformat;
                                }
                                break;
                            case 5:
                                decimal Price;
                                if (decimal.TryParse(row.GetCell(j).ToString().Trim(), out Price))
                                {
                                    obj.Price = Price;
                                }
                                else
                                {
                                    obj.Price = Price;
                                    obj.IsValid = false;
                                    obj.ErrorPrice = (int)Error.Wrongformat;
                                }
                                break;
                            case 6:
                                decimal PriceVAT;
                                if (decimal.TryParse(row.GetCell(j).ToString().Trim(), out PriceVAT))
                                {
                                    obj.PriceVat = PriceVAT;
                                }
                                else
                                {
                                    obj.PriceVat = PriceVAT;
                                    obj.IsValid = false;
                                    obj.ErrorPriceVat = (int)Error.Wrongformat;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0:
                                obj.ErrorPackageName = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 1:
                                obj.ErrorPackageNumber = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 2:
                                obj.ErrorDecision = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 3:
                                obj.ErrorTimeUsed = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 4:
                                obj.ErrorPromotionTime = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 5:
                                obj.ErrorPrice = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            case 6:
                                obj.ErrorPriceVat = (int)Error.Empty;
                                obj.IsValid = false;
                                break;
                            default:
                                break;
                        }
                        //break;
                    }
                }

                if (ListAllPAckage.FirstOrDefault(_o => _o.PackageNumber == obj.PackageNumber) != null)
                {
                    obj.IsValid = false;
                    obj.ErrorPackageNumber = (int)Error.Exist;
                }
                else
                {
                    ListAllPAckage.Add(mapper.Map<MobifiberPackage>(obj));
                }

                if (obj.IsValid)
                {
                    lstDone.Add(obj);
                }
                else
                {
                    lstfail.Add(obj);
                }
            }
            return lstDone;
        }

        private List<PackageViewModel> GetErrStr(List<PackageViewModel> lst)
        {
            foreach (PackageViewModel item in lst)
            {
                string str = string.Empty;
                if (item.ErrorPackageName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Tên gói cước " + Common.GetEnumDescription((Error)item.ErrorPackageName);
                }
                if (item.ErrorPackageNumber > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Mã gói cước " + Common.GetEnumDescription((Error)item.ErrorPackageNumber);
                }
                if (item.ErrorDecision > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Số công văn quyết định " + Common.GetEnumDescription((Error)item.ErrorDecision);
                }
                if (item.ErrorTimeUsed > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Thời gian sử dụng " + Common.GetEnumDescription((Error)item.ErrorTimeUsed);
                }
                if (item.ErrorPromotionTime > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Thời gian khuyến mãi " + Common.GetEnumDescription((Error)item.ErrorPromotionTime);
                }
                if (item.ErrorPrice > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Giá gói cước " + Common.GetEnumDescription((Error)item.ErrorPrice);
                }
                if (item.ErrorPriceVat > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Giá gói cước VAT " + Common.GetEnumDescription((Error)item.ErrorPriceVat);
                }
                item.ErrStr = str;
            }
            return lst;
        }

        [UserAuthorize(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        [HttpPost]
        public JsonResult SaveData()
        {
            List<PackageViewModel> lstPackage = new List<PackageViewModel>();
            List<PackageViewModel> lstPackageInvalid = new List<PackageViewModel>();
            IFormFile file = Request.Form.Files[0];
            string folderName = "Output";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                }
                lstPackage = processFile(sheet, ref lstPackageInvalid, false);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            List<MobifiberPackage> lstPackageAdd = new List<MobifiberPackage>();
            for (int i = 0; i < lstPackage.Count; i++)
            {
                lstPackageAdd.Add(mapper.Map<MobifiberPackage>(lstPackage[i]));

            }
            Package_DAO package_DAO = new Package_DAO();
            bool result = package_DAO.AddRange(lstPackageAdd);
            if (result)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công" });
            }
        }

        [HttpPost]
        public JsonResult ExportPackageError()
        {
            List<PackageViewModel> lstPackage = new List<PackageViewModel>();
            List<PackageViewModel> lstPackageInvalid = new List<PackageViewModel>();
            IFormFile file = Request.Form.Files[0];
            string folderName = "Output";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                }
                lstPackage = processFile(sheet, ref lstPackageInvalid, false);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\PackageTemplate.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "Danh_Sach_Goi_Cuoc.xlsx";

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
                    //var temp_info = helper.CreateTemplate("info");
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");

                    helper.InsertData(temp_top, "");
                    helper.Insert(temp_column);

                    helper.InsertDatas(temp_item, lstPackageInvalid);
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
                return Json(new
                {
                    status = false,
                    IsCreateExel = false,
                    fileLink = "/Output/" + fileName,
                    fileName = fileName
                });
            }
        }
        public ActionResult DownloadTemplateDevice()
        {
            string path = _webHostEnvironment.ContentRootPath + "\\TemplateExcel\\Danh_Sach_Goi_Cuoc.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Template_GoiCuoc.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
