using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Micro.Web;
using MobiFiber.Models;
using MobiFiber.DAO;
using System.Globalization;
using MobiFiber.PartialViewModel;
using MobiFiber.Code;
using Micro.Web.Code;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using MobiFiber.Controllers;
using AutoMapper;
using System.Text.RegularExpressions;

namespace TemplateNetCore.Controllers
{
    public class ContractController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly int DeviceIdDefault = 0;
        private string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd/MMM/yy", "dd/MMM/yyyy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy", "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy", "dd-mmm-yy", "dd-mmm-yyyy", "dd-MMM-yy", "dd-MMM-yyyy" };
        private readonly IMapper mapper;

        public ContractController(IWebHostEnvironment webHostEnvironment, IMapper _mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            mapper = _mapper;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult Index()
        {
            Device_DAO dalDevice = new Device_DAO();
            Developer_DAO developer_DAO = new Developer_DAO();
            Agents_DAO agents_DAO = new Agents_DAO();

            List<MobifiberAgent> lstAgent = agents_DAO.ListAll();
            List<MobifiberDevelopmentUnit> lstDevelopmentUnit = developer_DAO.ListAll();

            List<int> lstStatusOfDevice = new List<int>();
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.NotLinked);

            List<int> lstStatusOfPackage = new List<int>();
            lstStatusOfPackage.Add((int)PakageStatus.Active);

            List<MobifiberDevice> lstdevice = dalDevice.GetDeviceAvailability().Where(_o => lstStatusOfDevice.Contains(_o.Status)).ToList();

            Package_DAO dalPAckage = new Package_DAO();
            List<MobifiberPackage> lstpackage = dalPAckage.GetAllPackageUsingStoreProcedure().Where(_o => lstStatusOfPackage.Contains(_o.Status)).ToList();
            ViewData["lstdevice"] = lstdevice;
            ViewData["lstpackage"] = lstpackage;
            ViewData["lstAgent"] = lstAgent;
            ViewData["lstDevelopmentUnit"] = lstDevelopmentUnit;
            return View();
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult CreateInit()
        {
            Developer_DAO developer_DAO = new Developer_DAO();
            Agents_DAO agents_DAO = new Agents_DAO();
            List<MobifiberAgent> lstAgent = agents_DAO.ListAll();
            List<MobifiberDevelopmentUnit> lstDevelopmentUnit = developer_DAO.ListAll();
            ViewData["lstAgent"] = lstAgent;
            ViewData["lstDevelopmentUnit"] = lstDevelopmentUnit;
            ViewData["lstdevice"] = GetListDevice();
            ViewData["lstpackage"] = GetListPackage();
            return View("Create");
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Add })]
        public JsonResult Create(MobifiberContract obj)
        {
            var checkrole = SettingController.CheckLockRole(0, 3, obj.SignDate.Value.ToString("dd/MM/yyyy"));
            if (checkrole == false)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_CONTRACT)20) });
            }
            Contract_DAO dal = new Contract_DAO();
            var sessUser = SessionSystem.userSession;
            MobifiberContract current = Common.clone<MobifiberContract>(obj);
            obj.UserCreate = sessUser.UserId;
            obj.DateCreate = DateTime.Now;
            if (obj.DeviceId > 0)
            {
                Device_DAO device_DAO = new Device_DAO();
                MobifiberDevice mobifiberDevice = device_DAO.GetById(obj.DeviceId);
                if (mobifiberDevice != null && mobifiberDevice.DateInputWarehouse > obj.SignDate)
                {
                    return Json(new { status = false, code = Constant.CODE_CONFIRM, mess = "Ngày nhập kho thiết bị không hợp lệ !" });
                }
            }

            bool result = dal.SaveOrUpdate(obj);
            if (result)
            {
                foreach (var prop in current.GetType().GetProperties())
                {
                    if (Constant.LIST_PROPERTY_IGNOR_WRITELOG.FirstOrDefault(o => o.ToUpper() == prop.Name.ToUpper()) != null)
                    {
                        continue;
                    }

                    object _oldValue = Common.GetPropValue(current, prop.Name);
                    object _newValue = Common.GetPropValue(obj, prop.Name);

                    string oldValue = "";
                    string newValue = "";

                    oldValue = _oldValue == null ? "" : _oldValue.ToString();
                    newValue = _newValue == null ? "" : _newValue.ToString();


                    if (_oldValue != _newValue)
                    {
                        WriteLogToDatabase.AddLog(
                                    (int)ActionModule.ContractManager,
                                    Constant.CREATE,
                                    sessUser.UserId,
                                    DateTime.Now,
                                    (int)ActionTypeCustom.Add,
                                    obj.ContractId,
                                    prop.Name,
                                    newValue,
                                    oldValue
                                    );
                    }

                }
                return Json(new { status = true, mess = "Thành công" });
            }
            else
            {
                return Json(new { status = false, mess = "Lỗi" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult Update(MobifiberContract obj)
        {
            var checkrole = SettingController.CheckLockRole(obj.ContractId, 3, "");
            if (checkrole == false)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_CONTRACT)20) });
            }
            if (obj.ContractId <= 0)
            {
                return Json(new { status = false, mess = "Hợp đồng không tồn tại !" });
            }

            var sessUser = SessionSystem.userSession;
            Contract_DAO dal = new Contract_DAO();
            MobifiberContract current = dal.GetById(obj.ContractId);

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
                                newValue = obj.Status.ToString();
                            }

                            if (strFieldChange == "DeviceId")
                            {
                                DateTime DateChange_Device = DateTime.ParseExact(obj.DateChangeDevice, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                strFieldChange = prop.Name + "Pay";
                                WriteLogToDatabase.AddLog(
                                   (int)ActionModule.ContractManager,
                                   Constant.UPDATE_DEVICE_PAY,
                                   sessUser.UserId,
                                   DateChange_Device,
                                   (int)ActionTypeCustom.Edit,
                                   obj.ContractId,
                                   strFieldChange,
                                   oldValue,
                                   newValue
                                   );

                                strFieldChange = prop.Name + "Borrow";
                                WriteLogToDatabase.AddLog(
                                   (int)ActionModule.ContractManager,
                                   Constant.UPDATE_DEVICE_BORROW,
                                   sessUser.UserId,
                                   DateChange_Device,
                                   (int)ActionTypeCustom.Edit,
                                   obj.ContractId,
                                   strFieldChange,
                                   newValue,
                                   oldValue
                                   );
                            }
                            else if (strFieldChange == "PackageId")
                            {
                                DateTime DateChange = DateTime.ParseExact(obj.DateChangePackage, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                strFieldChange = prop.Name + "Pay";
                                WriteLogToDatabase.AddLog(
                                   (int)ActionModule.ContractManager,
                                   Constant.UPDATE_PACKAGE_PAY,
                                   sessUser.UserId,
                                   DateChange,
                                   (int)ActionTypeCustom.Edit,
                                   obj.ContractId,
                                   strFieldChange,
                                   oldValue,
                                   newValue
                                   );

                                strFieldChange = prop.Name + "Borrow";
                                WriteLogToDatabase.AddLog(
                                   (int)ActionModule.ContractManager,
                                   Constant.UPDATE_PACKAGE_BORROW,
                                   sessUser.UserId,
                                   DateChange,
                                   (int)ActionTypeCustom.Edit,
                                   obj.ContractId,
                                   strFieldChange,
                                   newValue,
                                   oldValue
                                   );
                            }
                            else
                            {
                                WriteLogToDatabase.AddLog(
                                   (int)ActionModule.ContractManager,
                                   Constant.UPDATE,
                                   sessUser.UserId,
                                   DateTime.Now,
                                   (int)ActionTypeCustom.Edit,
                                   obj.ContractId,
                                   strFieldChange,
                                   newValue,
                                   oldValue
                                   );
                            }

                        }
                    }
                }

            }

            bool result = dal.SaveOrUpdate(obj);
            //WriteLogToDatabase.AddLog((int)ActionModule.ContractManager, sessUser.UserName + " Update", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Edit, obj.ContractId);
            if (result)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else
            {

                return Json(new { status = false, mess = "Lỗi" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetDataContract(int status, int package, string search, int offset, int limit)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            var total = 0;
            Contract_DAO dal = new Contract_DAO();
            List<ContractModelView> lstdata = dal.GetContractUsingStoreProcedue(ref total, status, package, search, offset, limit).ToList();

            return Json(new { status = true, rows = lstdata, total = total, mess = "Thành công" });
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Export })]
        public JsonResult ExportContract(int status, int package, string search)
        {
            Contract_DAO dal = new Contract_DAO();
            List<ContractModelView> lstdata = dal.GetContractExport(status, package, search).ToList();
            if (lstdata == null || lstdata.Count == 0)
            {
                return Json(new { status = false, mess = "Không có dữ liệu" });
            }

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\ExportContract.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "DS_Contract_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";

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
                    var temp_column = helper.CreateTemplate("column");
                    var temp_item = helper.CreateTemplate("item");

                    helper.InsertData(temp_top, "");
                    helper.Insert(temp_column);

                    helper.InsertDatas(temp_item, lstdata);
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
        public JsonResult GetDataContractById(int Id)
        {
            var checkrole = SettingController.CheckLockRole(Id, 3, "");
            if (checkrole == false)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_CONTRACT)20) });
            }
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Hợp đồng không tồn tại !" });
            }
            Contract_DAO contract_DAO = new Contract_DAO();
            Device_DAO device_DAO = new Device_DAO();
            Package_DAO package_DAO = new Package_DAO();

            MobifiberContract mobifiberContract = contract_DAO.GetById(Id);
            if (mobifiberContract != null)
            {
                List<MobifiberDevice> lstDevice = GetListDevice();
                MobifiberDevice device = device_DAO.GetById(mobifiberContract.DeviceId);
                lstDevice.Add(device);

                List<MobifiberPackage> lstPackage = GetListPackage();
                MobifiberPackage package = package_DAO.GetById(mobifiberContract.PackageId);
                if (lstPackage.FirstOrDefault(_o => _o.PackageId == package.PackageId) == null)
                {
                    lstPackage.Add(package);
                }

                return Json(new { status = true, Contract = mobifiberContract, lstDevice = lstDevice, lstPackage = lstPackage, mess = "Thành công !" });
            }
            else
            {
                return Json(new { status = false, mess = "Hợp đồng không tồn tại !" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetHistoriesContractById(int Id)
        {
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Mã hợp đồng không tồn tại !" });
            }
            Contract_DAO contract_DAO = new Contract_DAO();
            MobifiberContract mobifiberContract = contract_DAO.GetById(Id);
            History_DAO history_DAO = new History_DAO();
            List<HistoryContractDeviceView> lstdatadevice = history_DAO.GetContractDeviceHistoryUsingStoreProcedure(Id);
            List<HistoryContractPackageView> lstdatapackage = history_DAO.GetContractPackageHistoryUsingStoreProcedure(Id);
            //var totaldevice = lstdatadevice.Count;
            //var totalpackage = lstdatapackage.Count;
            return Json(new { status = true, lstContract = mobifiberContract, lstdatadevice = lstdatadevice, lstdatapackage = lstdatapackage, mess = "Thành công" });
        }

        private List<MobifiberPackage> GetListPackage()
        {
            List<int> lstStatusOfPackage = new List<int>();
            lstStatusOfPackage.Add((int)PakageStatus.Active);
            Package_DAO dalPAckage = new Package_DAO();
            List<MobifiberPackage> lstpackage = dalPAckage.GetAllPackageUsingStoreProcedure().Where(_o => lstStatusOfPackage.Contains(_o.Status)).ToList();
            return lstpackage;
        }

        private List<MobifiberDevice> GetListDevice()
        {
            Device_DAO dalDevice = new Device_DAO();
            List<int> lstStatusOfDevice = new List<int>();
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.NotLinked);
            List<MobifiberDevice> lstdevice = dalDevice.GetDeviceAvailability().Where(_o => lstStatusOfDevice.Contains(_o.Status)).ToList();
            return lstdevice;
        }

        private int ValidateContract(int Id, string CustomerName, string CustomerID, string AdressCustomer, string TaxCode, string ContractNumber
            , string SignDate, int PackageId, int DeviceId, string RegisterDate, int Agentcode, string BillNumber, string BillDate
            , decimal BillPrice, int DeveloperName, string InfrastructurePartners, string TypeOfCooperation)
        {
            Contract_DAO contract_DAO = new Contract_DAO();
            DateTime date;
            MobifiberContract mobifiberContract = new MobifiberContract();
            List<MobifiberContract> lstContract = contract_DAO.GetAllContractUsingQuery();

            if (Id > 0)
            {
                mobifiberContract = contract_DAO.GetById(Id);
            }

            if (string.IsNullOrEmpty(CustomerName))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_1;
            }
            if (string.IsNullOrEmpty(CustomerID))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_2;
            }

            if (string.IsNullOrEmpty(ContractNumber))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_3;
            }
            if (string.IsNullOrEmpty(SignDate))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_4;
            }

            DateTime SignDateConverted;
            if (!DateTime.TryParseExact(SignDate, "dd/MM/yyyy", null, DateTimeStyles.None, out SignDateConverted))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_10;
            }

            if (PackageId <= 0)
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_5;
            }
            if (string.IsNullOrEmpty(RegisterDate))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_6;
            }
            if (!DateTime.TryParseExact(RegisterDate, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_11;
            }

            Package_DAO package_DAO = new Package_DAO();
            MobifiberPackage mobifiberPackage = package_DAO.GetById(PackageId);
            if (mobifiberPackage == null || (mobifiberPackage.Status != (int)MobiFiber.Code.PakageStatus.Active && Id <= 0 && mobifiberContract.PackageId != PackageId))  // check when add new
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_8;
            }

            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(DeviceId);
            if (mobifiberDevice == null || (mobifiberDevice.Status != (int)MobiFiber.Code.DeviceStatus.NotLinked && Id <= 0 && mobifiberContract.DeviceId != DeviceId))  // check when add new
            {
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Linked)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_9;
                }
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Delete)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_13;
                }
                //if(mobifiberDevice.DateInputWarehouse > SignDateConverted)
                //{
                //    return (int)ERR_VALIDATE_CONTRACT.ERR_16;
                //}
            }

            //if(mobifiberDevice != null)
            //{
            //    if (mobifiberDevice.DateInputWarehouse > SignDateConverted)
            //    {
            //        return (int)ERR_VALIDATE_CONTRACT.ERR_16;
            //    }
            //}


            if (!string.IsNullOrEmpty(BillDate) && !DateTime.TryParseExact(BillDate, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_12;
            }
            foreach (var item in lstContract)
            {
                if (item.ContractId != Id && item.CustomerIdvm == CustomerID)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_14;
                }
            }

            return (int)ERR_VALIDATE_CONTRACT.ERR_0;
        }

        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        public JsonResult Import()
        {
            try
            {
                Contract_DAO contract_DAO = new Contract_DAO();
                List<MobifiberContract> lstContracts = new List<MobifiberContract>();
                List<CustomerViewModel> lstContract = new List<CustomerViewModel>();
                List<CustomerViewModel> lstContractInvalid = new List<CustomerViewModel>();
                IFormFile file = Request.Form.Files[0];
                int checkdatedevice = 0;
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
                    lstContract = processFile(sheet, ref lstContractInvalid, ref lstContracts, checkdatedevice, true);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                if (checkdatedevice == 1)
                {
                    return Json(new { status = true, mess = "Upload thành công. Kiểm tra dữ liệu trước khi lưu", CountDone = lstContract.Count(), CountError = lstContractInvalid.Count(), lstValid = lstContract, lstInValid = GetErrStr(lstContractInvalid), checkdatedevice = checkdatedevice });
                }
                return Json(new { status = true, mess = "Upload thành công. Kiểm tra dữ liệu trước khi lưu", CountDone = lstContract.Count(), CountError = lstContractInvalid.Count(), lstValid = lstContract, lstInValid = GetErrStr(lstContractInvalid) });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mess = "Có lỗi xảy ra, Vui lòng kiểm tra lại file import !" });
            }

        }

        private List<CustomerViewModel> processFile(ISheet sheet, ref List<CustomerViewModel> lstfail, ref List<MobifiberContract> lstContracts, int checkdatedevice, bool checkInvalid = true)
        {
            try
            {
                Contract_DAO contract_DAO = new Contract_DAO();
                Device_DAO device_DAO = new Device_DAO();
                Package_DAO package_DAO = new Package_DAO();
                Developer_DAO developer_DAO = new Developer_DAO();
                Agents_DAO agents_DAO = new Agents_DAO();

                List<MobifiberContract> lstAllContract = contract_DAO.GetAllContractUsingQuery();
                List<MobifiberDevice> lstAllDevice = device_DAO.GetAllDeviceUsingQuery();
                List<MobifiberPackage> lstAllPackage = package_DAO.GetAllPackageUsingStoreProcedure();
                List<MobifiberAgent> lstAllAgent = agents_DAO.ListAll();

                List<MobifiberDevelopmentUnit> lstAllDevelopmentUnit = developer_DAO.ListAll();
                List<CustomerViewModel> lstContract = new List<CustomerViewModel>();
                List<CustomerViewModel> lstContractInvalid = new List<CustomerViewModel>();

                List<CustomerViewModel> lstDone = new List<CustomerViewModel>();
                IRow headerRow = sheet.GetRow(0); //Get Header Row
                int cellCount = headerRow.LastCellNum;
                int startPoint = 2;
                for (int i = (sheet.FirstRowNum + startPoint); i <= sheet.LastRowNum; i++) //Read Excel File
                {
                    CustomerViewModel obj = new CustomerViewModel();
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
                                    obj.CustomerName = row.GetCell(j).ToString().Trim();
                                    break;
                                case 1:
                                    obj.CustomerIdvm = row.GetCell(j).ToString().Trim();
                                    break;
                                case 2:
                                    obj.IdentityCard = row.GetCell(j).ToString().Trim();
                                    break;
                                case 3:
                                    obj.Address = row.GetCell(j).ToString().Trim();
                                    break;
                                case 4:
                                    obj.Phone = row.GetCell(j).ToString().Trim();
                                    break;
                                case 5:
                                    obj.ContractNumber = row.GetCell(j).ToString().Trim();
                                    break;
                                case 6:
                                    DateTime SignDate;

                                    if (!DateTime.TryParseExact(row.GetCell(j).ToString().Trim(), formats, null, DateTimeStyles.None, out SignDate))
                                    {
                                        obj.IsValid = false;
                                        obj.ErrorSignDate = (int)Error.Wrongformat;
                                    }
                                    var checkrole = SettingController.CheckLockRole(0, 3, SignDate.ToString("dd/MM/yyyy"));
                                    if (checkrole == false)
                                    {
                                        obj.IsValid = false;
                                        obj.ErrorSignDate = (int)Error.Rolelock;
                                    }
                                    obj.SignDate = SignDate;

                                    break;
                                case 7:
                                    obj.PackageName = row.GetCell(j).ToString().Trim();
                                    break;
                                case 8:
                                    DateTime RegisterDate;
                                    if (!DateTime.TryParseExact(row.GetCell(j).ToString().Trim(), formats, null, DateTimeStyles.None, out RegisterDate))
                                    {
                                        obj.IsValid = false;
                                        obj.ErrorRegisterDate = (int)Error.Wrongformat;
                                    }
                                    obj.RegisterDate = RegisterDate;

                                    break;
                                case 9:
                                    obj.DeviceName = row.GetCell(j).ToString().Trim();
                                    break;
                                case 10:
                                    obj.Serial = row.GetCell(j).ToString().Trim();
                                    break;
                                case 11:
                                    obj.AgentName = row.GetCell(j).ToString().Trim();
                                    break;
                                case 12:
                                    obj.BillNumber = row.GetCell(j).ToString().Trim();
                                    break;
                                case 13:
                                    DateTime BillDate;
                                    if (!DateTime.TryParseExact(row.GetCell(j).ToString().Trim(), formats, null, DateTimeStyles.None, out BillDate))
                                    {
                                        obj.IsValid = false;
                                        obj.ErrorBillDate = (int)Error.Wrongformat;
                                    }
                                    obj.BillDate = BillDate;

                                    break;
                                case 14:
                                    decimal BillPrice;
                                    if (!decimal.TryParse(row.GetCell(j).ToString().Trim(), out BillPrice))
                                    {
                                        obj.IsValid = false;
                                        obj.ErrorBillDate = (int)Error.Wrongformat;
                                    }
                                    obj.BillPrice = BillPrice;

                                    break;
                                case 15:
                                    obj.DeveloperName = row.GetCell(j).ToString().Trim();
                                    break;
                                case 16:
                                    obj.InfrastructurePartners = row.GetCell(j).ToString().Trim();
                                    break;
                                case 17:
                                    obj.TypeOfCooperation = row.GetCell(j).ToString().Trim();
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
                                    obj.ErrorCustomerName = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 1:
                                    obj.ErrorCustomerIdvm = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 5:
                                    obj.ErrorContractNumber = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 6:
                                    obj.ErrorSignDate = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 7:
                                    obj.ErrorPackageName = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                //case 8:
                                //    obj.ErrorRegisterDate = (int)Error.Empty;
                                //    obj.IsValid = false;
                                //    break;

                                default:
                                    break;
                            }
                        }
                    }
                    if (obj.IsValid)
                    {
                        lstContract.Add(obj);
                    }
                    else
                    {
                        lstfail.Add(obj);
                    }
                }
                List<int> LstDeviceSelected = new List<int>();
                foreach (CustomerViewModel item in lstContract)
                {
                    MobifiberContract mobifiberContract = new MobifiberContract();
                    mobifiberContract.CustomerName = item.CustomerName;
                    mobifiberContract.IdentityCard = item.IdentityCard;
                    mobifiberContract.CustomerIdvm = item.CustomerIdvm;
                    mobifiberContract.Address = item.Address;
                    if (!string.IsNullOrEmpty(item.Phone))
                    {
                        if (ValidatePhone(item.Phone))
                        {
                            mobifiberContract.Phone = item.Phone;
                        }
                        else
                        {
                            item.IsValid = false;
                            item.ErrorPhone = (int)Error.NoMatch;
                        }
                    }
                    else
                    {
                        mobifiberContract.Phone = item.Phone;
                    }
                    mobifiberContract.ContractNumber = item.ContractNumber;
                    mobifiberContract.UserCreate = SessionSystem.userSession.UserId;
                    mobifiberContract.DateCreate = DateTime.Now;
                    mobifiberContract.SignDate = item.SignDate;
                    mobifiberContract.RegisterDate = item.RegisterDate;
                    mobifiberContract.BillNumber = item.BillNumber;
                    mobifiberContract.BillDate = item.BillDate;
                    mobifiberContract.BillPrice = item.BillPrice;
                    mobifiberContract.InfrastructurePartners = item.InfrastructurePartners;
                    mobifiberContract.TypeOfCooperation = item.TypeOfCooperation;

                    if (string.IsNullOrEmpty(item.DeviceName) && string.IsNullOrEmpty(item.Serial))
                    {
                        mobifiberContract.DeviceId = DeviceIdDefault;
                    }
                    else if (!string.IsNullOrEmpty(item.DeviceName) && string.IsNullOrEmpty(item.Serial))
                    {
                        item.IsValid = false;
                        item.ErrorDeviceName = (int)Error.NoSerial;
                    }
                    else if (string.IsNullOrEmpty(item.DeviceName) && !string.IsNullOrEmpty(item.Serial))
                    {
                        item.IsValid = false;
                        item.ErrorDeviceName = (int)Error.NoName;
                    }
                    else
                    {
                        MobifiberDevice device = lstAllDevice.FirstOrDefault(o => o.DeviceName.Trim().ToUpper() == item.DeviceName.Trim().ToUpper() && o.Serial == item.Serial && o.Status == (int)MobiFiber.Code.DeviceStatus.NotLinked && !LstDeviceSelected.Contains(o.DeviceId));
                        // && device.DateInputWarehouse.Value < mobifiberContract.SignDate
                        if (device != null)
                        {
                            mobifiberContract.DeviceId = device.DeviceId;
                            LstDeviceSelected.Add(device.DeviceId);
                            if (device.DateInputWarehouse > item.SignDate)
                            {
                                checkdatedevice = 1;
                            }
                        }
                        else
                        {
                            item.IsValid = false;
                            item.ErrorDeviceName = (int)Error.NoMatch;
                        }
                    }
                    if (!string.IsNullOrEmpty(item.PackageName))
                    {
                        MobifiberPackage package = lstAllPackage.FirstOrDefault(o => o.PackageName.ToUpper().Trim() == item.PackageName.Trim().ToUpper() && o.Status == (int)MobiFiber.Code.PakageStatus.Active);
                        if (package != null)
                        {
                            mobifiberContract.PackageId = package.PackageId;
                        }
                        else
                        {
                            item.IsValid = false;
                            item.ErrorPackageName = (int)Error.NoMatch;
                        }
                    }
                    else
                    {
                        item.ErrorPackageName = (int)Error.Empty;
                        item.IsValid = false;
                    }
                  
                    if (string.IsNullOrEmpty(item.AgentName))
                    {
                        mobifiberContract.AgentcodeAm = 0;
                    }
                    else
                    {
                        MobifiberAgent agent = lstAllAgent.FirstOrDefault(o => o.AgentsName.Trim().ToUpper() == item.AgentName.Trim().ToUpper());
                        if (agent != null)
                        {
                            mobifiberContract.AgentcodeAm = agent.AmId;
                        }
                        else
                        {
                            item.IsValid = false;
                            item.ErrorAgentcodeAm = (int)Error.NoMatch;
                        }
                    }
                    if (string.IsNullOrEmpty(item.DeveloperName))
                    {
                        mobifiberContract.DeveloperName = 0;
                    }
                    else
                    {
                        MobifiberDevelopmentUnit DevelopmentUnit = lstAllDevelopmentUnit.FirstOrDefault(o => o.DevelopName.Trim().ToUpper() == item.DeveloperName.Trim().ToUpper());
                        if (DevelopmentUnit != null)
                        {
                            mobifiberContract.DeveloperName = DevelopmentUnit.DevelopId;
                        }
                        else
                        {
                            item.IsValid = false;
                            item.ErrorDeveloperName = (int)Error.NoMatch;
                        }
                    }
                    if (item.IsValid)
                    {
                        lstDone.Add(item);
                        lstContracts.Add(mobifiberContract);
                    }
                    else
                    {
                        lstfail.Add(item);
                    }
                }
                return lstDone;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private List<CustomerViewModel> GetErrStr(List<CustomerViewModel> lst)
        {
            foreach (CustomerViewModel item in lst)
            {
                string str = string.Empty;
                if (item.ErrorCustomerName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Tên khách hàng " + Common.GetEnumDescription((Error)item.ErrorCustomerName);
                }
                if (item.ErrorCustomerIdvm > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Mã khách hàng (VM) " + Common.GetEnumDescription((Error)item.ErrorCustomerIdvm);
                }
                if (item.ErrorIdentityCard > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "CCCD/CMND " + Common.GetEnumDescription((Error)item.ErrorIdentityCard);
                }
                if (item.ErrorAddress > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Địa chỉ " + Common.GetEnumDescription((Error)item.ErrorAddress);
                }
                if (item.ErrorPhone > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Số điện thoại " + Common.GetEnumDescription((Error)item.ErrorPhone);
                }
                if (item.ErrorContractNumber > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Số hợp đồng " + Common.GetEnumDescription((Error)item.ErrorContractNumber);
                }
                if (item.ErrorSignDate > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Ngày ký hợp đồng " + Common.GetEnumDescription((Error)item.ErrorSignDate);
                }
                if (item.ErrorPackageName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Gói cước đăng ký " + Common.GetEnumDescription((Error)item.ErrorPackageName);
                }
                if (item.ErrorRegisterDate > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Ngày đăng ký gói cước " + Common.GetEnumDescription((Error)item.ErrorRegisterDate);
                }
                if (item.ErrorDeviceName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Thiết bị " + Common.GetEnumDescription((Error)item.ErrorDeviceName);
                }
                if (item.ErrorAgentcodeAm > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Tên đại lý (AM) " + Common.GetEnumDescription((Error)item.ErrorAgentcodeAm);
                }
                if (item.ErrorBillNumber > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Số hóa đơn " + Common.GetEnumDescription((Error)item.ErrorBillNumber);
                }
                if (item.ErrorBillDate > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Ngày hóa đơn " + Common.GetEnumDescription((Error)item.ErrorBillDate);
                }
                if (item.ErrorBillPrice > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Số tiền trên hóa đơn " + Common.GetEnumDescription((Error)item.ErrorBillPrice);
                }
                if (item.ErrorDeveloperName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Tên đơn vị phát triển " + Common.GetEnumDescription((Error)item.ErrorDeveloperName);
                }
                if (item.ErrorInfrastructurePartners > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Đối tác hạ tầng " + Common.GetEnumDescription((Error)item.ErrorInfrastructurePartners);
                }
                if (item.ErrorTypeOfCooperation > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Loại hình hợp tác " + Common.GetEnumDescription((Error)item.ErrorTypeOfCooperation);
                }
                item.ErrStr = str;
            }
            return lst;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        [HttpPost]
        public JsonResult SaveData()
        {
            Contract_DAO contract_DAO = new Contract_DAO();
            List<MobifiberContract> lstContracts = new List<MobifiberContract>();
            List<CustomerViewModel> lstContract = new List<CustomerViewModel>();
            List<CustomerViewModel> lstContractInvalid = new List<CustomerViewModel>();
            IFormFile file = Request.Form.Files[0];
            int checkdatedevice = 0;
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
                lstContract = processFile(sheet, ref lstContractInvalid, ref lstContracts, checkdatedevice, true);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            bool result = contract_DAO.AddRange(lstContracts);
            if (result)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.ContractManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        [HttpPost]
        public JsonResult ExportContractError()
        {
            Contract_DAO contract_DAO = new Contract_DAO();
            List<MobifiberContract> lstContracts = new List<MobifiberContract>();
            List<CustomerViewModel> lstContract = new List<CustomerViewModel>();
            List<CustomerViewModel> lstContractInvalid = new List<CustomerViewModel>();
            IFormFile file = Request.Form.Files[0];
            int checkdatedevice = 0;
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
                lstContract = processFile(sheet, ref lstContractInvalid, ref lstContracts, checkdatedevice, true);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\CustomerTemplate.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "Danh_Sach_Khach_Hang.xlsx";

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

                    helper.InsertDatas(temp_item, lstContractInvalid);
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
            string path = _webHostEnvironment.ContentRootPath + "\\TemplateExcel\\Danh_Sach_Khach_Hang.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Template_KhachHang.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public static bool ValidatePhone(string phone)
        {
            try
            {
                var phoneRegex = new Regex(@"(0[3|5|7|8|9]{1})+([0-9]{8})\b");
                var flag = phoneRegex.IsMatch(phone);
                return flag;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
