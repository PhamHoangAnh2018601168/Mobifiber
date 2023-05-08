using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Micro.Web;
using MobiFiber.DAO;
using MobiFiber.Models;
using System.Globalization;
using MobiFiber.Code;
using Micro.Web.Code;
using Microsoft.AspNetCore.Http;
using MobiFiber.PartialViewModel;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using MobiFiber.Controllers;
using AutoMapper;

namespace TemplateNetCore.Controllers
{
    public class DeviceController : Controller
    {
        private static int IMPORT_ERROR_FILE_NOT_SUPPORT = -1;
        private static int IMPORT_SUCCESS = 0;
        private static int IMPORT_ERROR_UNKNOW = 1;
        private static int IMPORT_ERROR_FILE_NOT_CHOOSE = 2;
        private static int IMPORT_ERROR_FILE_NO_DATA = 3;
        private static int IMPORT_STEP_VERIFY_DATA = 1;

        private string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd/MMM/yy", "dd/MMM/yyyy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy", "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy", "dd-mmm-yy", "dd-mmm-yyyy", "dd-MMM-yy", "dd-MMM-yyyy" };

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper mapper;
        public DeviceController(IWebHostEnvironment webHostEnvironment, IMapper _mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            mapper = _mapper;
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult Index()
        {
            //var cookieKey = HttpContext.Request.Cookies[Micro.Web.Code.SessionSystem.sessionName];
            //bool isxxx = Common.isRole(new ActionModule[] { ActionModule.PackageManager }, new ActionTypeCustom[] { ActionTypeCustom.View }, cookieKey);

            return View();
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetDataDevice(string search, int type, int offset, int limit)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            Device_DAO dal = new Device_DAO();

            List<int> lstStatusOfDevice = new List<int>();
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.NotLinked);
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.Linked);
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.Buy);
            lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.Stop);

            List<MobifiberDevice> lstAllData = dal.GetAllDeviceUsingQuery().Where(_o => lstStatusOfDevice.Contains(_o.Status)).ToList();
            List<MobifiberDevice> lstDataSrarch = lstAllData.Where(_o => _o.DeviceName.ToUpper().Contains(search.ToUpper()) || search == "" || _o.DeviceCode.ToUpper().Contains(search.ToUpper()) || _o.Serial.ToUpper().Contains(search.ToUpper())).ToList();
            List<MobifiberDevice> lstDataFilter = new List<MobifiberDevice>();
            switch (type)
            {
                case -1:
                    lstDataFilter = lstDataSrarch;
                    break;
                case 0:
                    lstDataFilter = lstDataSrarch.Where(o => o.Status == (int)MobiFiber.Code.DeviceStatus.NotLinked).ToList();
                    break;
                case 1:
                    lstDataFilter = lstDataSrarch.Where(o => o.Status == (int)MobiFiber.Code.DeviceStatus.Linked).ToList();
                    break;
                case 3:
                    lstDataFilter = lstDataSrarch.Where(o => o.Status == (int)MobiFiber.Code.DeviceStatus.Buy).ToList();
                    break;
                case 4:
                    lstDataFilter = lstDataSrarch.Where(o => o.Status == (int)MobiFiber.Code.DeviceStatus.Stop).ToList();
                    break;
                case 5:
                    lstDataFilter = lstDataSrarch.Where(o => o.Status == (int)MobiFiber.Code.DeviceStatus.NotLinked && o.IsActive == (int)DeviceActiveStatus.NotNew).ToList();
                    break;
                default:
                    lstDataFilter = lstDataSrarch;
                    break;
            }

            var total = lstDataFilter.Count;

            return Json(new { status = true, rows = lstDataFilter.Skip(offset).Take(limit), total = total, mess = "Success" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Add })]
        public JsonResult Create(string DeviceName, string DeviceCode, string SerialDevice, decimal DevicePrice, int AllocationTime, string DateInputWarehouse, string DateStopAllocation, int DeviceStatus)
        {
            //var checkrole = SettingController.CheckLockRole(0, 1, DateInputWarehouse);
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}

            var sessUser = SessionSystem.userSession;
            Device_DAO dal = new Device_DAO();
            MobifiberDevice obj = new MobifiberDevice();

            int isVal = ValidateDevice(0, DeviceName, DeviceCode, SerialDevice, DevicePrice, AllocationTime, DateInputWarehouse, 0);
            if (isVal != (int)ERR_VALIDATE_DEVICE.ERR_0)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)isVal) });
            }

            DateTime date;
            if (!string.IsNullOrEmpty(DateInputWarehouse) && DateTime.TryParseExact(DateInputWarehouse, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                obj.DateInputWarehouse = date;
            }
            else
            {
                obj.DateInputWarehouse = DateTime.Now;
            }

            obj.DeviceName = DeviceName;
            obj.DeviceCode = DeviceCode;
            obj.Serial = SerialDevice;
            obj.DevicePrice = DevicePrice;
            obj.AllocationTime = AllocationTime;

            obj.UserCreate = sessUser.UserId;
            obj.DateCreate = DateTime.Now;
            string result = dal.SaveOrUpdate(obj);
            //Add device write log
            WriteLogToDatabase.AddLog(
                        (int)ActionModule.DeviceManager,
                        Common.GetEnumDescription((ActionTypeCustom)(int)ActionTypeCustom.Add),
                        sessUser.UserId,
                        DateTime.Now,
                        (int)ActionTypeCustom.Add,
                        obj.DeviceId,
                        "",
                        "",
                        ""
                        );
            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, mess = "Không thành công, Mã thiết bị và Serial thiết bị đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công !" });
            }
        }
        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult Update(int Id, string DeviceName, string DeviceCode, string SerialDevice, decimal DevicePrice, int AllocationTime, string DateInputWarehouse, string DateStopAllocation, int DeviceStatus, bool isConfirm = false)
        {
            //var checkrole = SettingController.CheckLockRole(Id, 1, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}
            if (Id <= 0)
            {
                return Json(new { status = false, code = Constant.CODE_NOT_EXISTS, mess = "Thiết bị không tồn tại !" });
            }

            int isVal = ValidateDevice(Id, DeviceName, DeviceCode, SerialDevice, DevicePrice, AllocationTime, DateInputWarehouse, DeviceStatus);
            if (isVal != (int)ERR_VALIDATE_DEVICE.ERR_0)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)isVal) });
            }

            var sessUser = SessionSystem.userSession;
            Device_DAO dal = new Device_DAO();
            MobifiberDevice obj = dal.GetById(Id);
            MobifiberDevice current = Common.clone<MobifiberDevice>(obj);

            DateTime date;
            if (!string.IsNullOrEmpty(DateInputWarehouse) && DateTime.TryParseExact(DateInputWarehouse, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                obj.DateInputWarehouse = date;
            }
            else
            {
                obj.DateInputWarehouse = DateTime.Now;
            }
            DateTime DateStop;
            if (!string.IsNullOrEmpty(DateStopAllocation) && DateTime.TryParseExact(DateStopAllocation, "dd/MM/yyyy", null, DateTimeStyles.None, out DateStop))
            {
                obj.StopAllocation = DateStop;
            }
            else
            {
                obj.StopAllocation = null;
            }
            if (isConfirm)
            {
                Contract_DAO contract_DAO = new Contract_DAO();
                MobifiberContract mobifiberContract = contract_DAO.GetAllContractUsingQuery().FirstOrDefault(o => o.DeviceId == Id);
                if (mobifiberContract != null && obj.DateInputWarehouse > mobifiberContract.SignDate)
                {
                    return Json(new { status = false, code = Constant.CODE_CONFIRM, mess = "Ngày nhập kho thiết bị không hợp lệ !" });
                }
            }

            obj.DeviceId = Id;
            obj.DeviceName = DeviceName;
            obj.DeviceCode = DeviceCode;
            obj.Serial = SerialDevice;
            obj.DevicePrice = DevicePrice;
            obj.AllocationTime = AllocationTime;
            obj.UserLastUpdate = sessUser.UserId;
            obj.DateLastUpdate = DateTime.Now;
            if (DeviceStatus == 0 || DeviceStatus == 1 || DeviceStatus == 3 || DeviceStatus == 4)
            {
                obj.Status = DeviceStatus;
            }
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
                            isChange = (string)_oldValue != (string)_newValue;
                        }

                        if (prop.Name != DateInputWarehouse)
                        {
                            if (isChange)
                            {
                                // todo insert.
                                string strFieldChange = prop.Name;
                                string oldValue = Common.GetPropValue(current, prop.Name).ToString();
                                string newValue = Common.GetPropValue(obj, prop.Name).ToString();
                                if (newValue == "0")
                                {
                                    newValue = DeviceStatus.ToString();
                                }
                                WriteLogToDatabase.AddLog(
                                        (int)ActionModule.DeviceManager,
                                        Common.GetEnumDescription((ActionTypeCustom)(int)ActionTypeCustom.Edit),
                                        sessUser.UserId,
                                        DateTime.Now,
                                        (int)ActionTypeCustom.Edit,
                                        obj.DeviceId,
                                        strFieldChange,
                                        newValue,
                                        oldValue
                                        );
                            }
                        }
                    }
                }

            }
            if (DeviceStatus == (int)DeviceActiveStatus.Guarantee)
            {
                obj.IsActive = (int)DeviceActiveStatus.Guarantee;
            }

            string result = dal.SaveOrUpdate(obj);
            //WriteLogToDatabase.AddLog((int)ActionModule.DeviceManager, sessUser.UserName + " Update", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Edit, obj.DeviceId);
            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, code = Constant.CODE_SUCCESS, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, code = Constant.CODE_EXISTS, mess = "Không thành công, Mã thiết bị và Serial thiết bị đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, code = Constant.CODE_EXCEPTION, mess = "Không thành công !" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Delete })]
        public JsonResult Delete(int Id)
        {
            //var checkrole = SettingController.CheckLockRole(Id, 1, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}
            var sessUser = SessionSystem.userSession;
            Device_DAO dal = new Device_DAO();
            MobifiberDevice obj = new MobifiberDevice();
            obj = dal.GetById(Id);
            obj.Status = (int)MobiFiber.Code.DeviceStatus.Delete;
            obj.UserLastUpdate = sessUser.UserId;
            obj.DateLastUpdate = DateTime.Now;
            WriteLogToDatabase.AddLog(
                        (int)ActionModule.DeviceManager,
                        Common.GetEnumDescription((ActionTypeCustom)(int)ActionTypeCustom.Delete),
                        sessUser.UserId,
                        DateTime.Now,
                        (int)ActionTypeCustom.Delete,
                        obj.DeviceId,
                        "",
                        "",
                        ""
                        );
            dal.SaveOrUpdate(obj);
            //WriteLogToDatabase.AddLog((int)ActionModule.DeviceManager, sessUser.UserName + " Delete", sessUser.UserId, DateTime.Now, (int)ActionTypeCustom.Delete, obj.DeviceId);
            return Json(new { status = true, mess = "Thành công" });
        }
        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetHistoriesDeviceById(int Id, int offset, int limit)
        {

            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Mã Thiết bị không tồn tại !" });
            }
            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(Id);

            History_DAO history_DAO = new History_DAO();
            List<HistoryDeviceView> lstdata = history_DAO.GetDeviceHistoryUsingStoreProcedure(Id);
            var total = lstdata.Count;

            if (mobifiberDevice != null)
            {
                return Json(new { status = true, rows = lstdata.Skip(offset).Take(limit), deviceinfo = mobifiberDevice, total = total, mess = "Thành công" });
            }
            return Json(new { status = false, rows = "", total = 0, mess = "lỗi" });

        }

        public JsonResult GetDataDeviceById(int Id)
        {
            //var checkrole = SettingController.CheckLockRole(Id, 1, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Thiết bị không tồn tại !" });
            }

            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(Id);

            List<dynamic> ListStatus = new List<dynamic>();
            ListStatus.Add(new { IdStatus = (int)MobiFiber.Code.DeviceStatus.NotLinked, NameStatus = "Trong kho" });
            ListStatus.Add(new { IdStatus = (int)DeviceActiveStatus.Guarantee, NameStatus = "Bảo hành" });

            if (mobifiberDevice != null)
            {

                return Json(new { status = true, data = mobifiberDevice, lstStatus = ListStatus, mess = "Thành công !" });
            }
            else
            {
                return Json(new { status = false, mess = "Thiết bị không tồn tại !" });
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult UpdateDevice(int OldId, int NewId, string reason, string DateWithDraw, string DateVouchers)
        {
            //var checkrole = SettingController.CheckLockRole(OldId, 1, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}
            var sessUser = SessionSystem.userSession;
            DateTime Datereinput = DateTime.ParseExact(DateWithDraw, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (OldId <= 0)
            {
                return Json(new { status = false, mess = "Thiết bị không tồn tại !" });
            }

            if (OldId == NewId)
            {
                return Json(new { status = true, mess = "Thành công !" });  // todo
            }
            int isVal = ValidateDeviceUpdate(NewId);
            if (isVal != (int)ERR_VALIDATE_DEVICE.ERR_0)
            {
                return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_CONTRACT)isVal) });
            }
            Contract_DAO contract_DAO = new Contract_DAO();
            MobifiberContract mobifiberContract = contract_DAO.GetAllContractUsingQuery().FirstOrDefault(_o => _o.DeviceId == OldId);

            //update thiet bi cu ve kho

            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(OldId);
            if (mobifiberDevice != null)
            {
                mobifiberDevice.Status = (int)MobiFiber.Code.DeviceStatus.NotLinked;
                if (NewId != 0)
                {
                    mobifiberDevice.IsActive = (int)MobiFiber.Code.DeviceActiveStatus.Guarantee;
                }
                mobifiberDevice.DateReinputWarehouse = Datereinput;  //ngay nhap lai
                mobifiberDevice.UserLastUpdate = SessionSystem.userSession.UserId;
                mobifiberDevice.DateLastUpdate = DateTime.Now;

                device_DAO.Update(mobifiberDevice);

                WriteLogToDatabase.AddLog(
                       (int)ActionModule.DeviceManager,
                       reason,
                       sessUser.UserId,
                       Datereinput,
                       (int)ActionTypeCustom.Edit,
                       mobifiberDevice.DeviceId,
                       "UpdateDeviceStatus",
                       MobiFiber.Code.DeviceStatus.Linked.ToString(),
                       MobiFiber.Code.DeviceStatus.NotLinked.ToString()
                       );
                if (NewId != 0)
                {
                    WriteLogToDatabase.AddLog(
                      (int)ActionModule.DeviceManager,
                      reason,
                      sessUser.UserId,
                      Datereinput,
                      (int)ActionTypeCustom.Edit,
                      mobifiberDevice.DeviceId,
                      "UpdateDeviceIsActive",
                      MobiFiber.Code.DeviceActiveStatus.NotNew.ToString(),
                      MobiFiber.Code.DeviceActiveStatus.Guarantee.ToString()
                      );
                }
            }

            //update hop dong gan thiet bi ID = 0
            if (mobifiberContract != null)
            {
                mobifiberContract.DeviceId = Constant.ID_EVICTION;
                mobifiberContract.UserLastUpdate = SessionSystem.userSession.UserId;
                mobifiberContract.DateLastUpdate = DateTime.Now;
                contract_DAO.Update(mobifiberContract);
                //ghi log trả thiết bị
                WriteLogToDatabase.AddLog(
                               (int)ActionModule.ContractManager,
                               reason,
                               sessUser.UserId,
                               Datereinput,
                               (int)ActionTypeCustom.Edit,
                               mobifiberContract.ContractId,
                               "DeviceIdPay",
                               OldId.ToString(),
                               NewId.ToString()
                               );
                //ghi log chứng từ
                if (NewId != 0 && DateVouchers != null)
                {
                    WriteLogToDatabase.AddLog(
                              (int)ActionModule.ContractManager,
                              reason,
                              sessUser.UserId,
                              DateTime.ParseExact(DateVouchers, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                              (int)ActionTypeCustom.Edit,
                              mobifiberContract.ContractId,
                              "PayDateVouchers",
                              OldId.ToString(),
                              NewId.ToString()
                              );
                }
            }
            if (mobifiberContract != null && NewId != 0)
            {
                mobifiberContract.DeviceId = Constant.ID_EVICTION;
                mobifiberContract.UserLastUpdate = SessionSystem.userSession.UserId;
                mobifiberContract.DateLastUpdate = DateTime.Now;
                contract_DAO.Update(mobifiberContract);
                WriteLogToDatabase.AddLog(
                               (int)ActionModule.ContractManager,
                               reason,
                               sessUser.UserId,
                               Datereinput,
                               (int)ActionTypeCustom.Edit,
                               mobifiberContract.ContractId,
                               "DeviceIdBorrow",
                               NewId.ToString(),
                               OldId.ToString()
                               );
            }

            if (OldId != NewId && NewId != Constant.ID_EVICTION) // doi thiet bi moi
            {
                //update hop dong gan thiet bi ID = new device id
                mobifiberContract.DeviceId = NewId;
                mobifiberContract.UserLastUpdate = SessionSystem.userSession.UserId;
                mobifiberContract.DateLastUpdate = DateTime.Now;
                contract_DAO.Update(mobifiberContract);

                MobifiberDevice mobifiberDeviceNew = device_DAO.GetById(NewId);
                mobifiberDeviceNew.Status = (int)MobiFiber.Code.DeviceStatus.Linked;
                if (mobifiberDeviceNew.IsActive == (int)DeviceActiveStatus.New)
                {
                    mobifiberDeviceNew.IsActive = (int)DeviceActiveStatus.NotNew;
                    mobifiberDeviceNew.DateActive = Datereinput;
                    WriteLogToDatabase.AddLog(
                       (int)ActionModule.DeviceManager,
                       reason,
                       sessUser.UserId,
                       Datereinput,
                       (int)ActionTypeCustom.Edit,
                       mobifiberDevice.DeviceId,
                       "UpdateDeviceStatus",
                       MobiFiber.Code.DeviceStatus.NotLinked.ToString(),
                       MobiFiber.Code.DeviceStatus.Linked.ToString()
                       );
                }
                device_DAO.Update(mobifiberDeviceNew);

                return Json(new { status = true, mess = "Thành công !" });
            }

            return Json(new { status = true, mess = "Thu hồi thành công !" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetInforDevice(int Id)
        {
            //var checkrole = SettingController.CheckLockRole(Id, 1, "");
            //if (checkrole == false)
            //{
            //    return Json(new { status = false, mess = Common.GetEnumDescription((ERR_VALIDATE_DEVICE)20) });
            //}
            if (Id <= 0)
            {
                return Json(new { status = false, mess = "Thiết bị không tồn tại !" });
            }

            Contract_DAO contract_DAO = new Contract_DAO();
            Device_DAO device_DAO = new Device_DAO();
            Package_DAO package_DAO = new Package_DAO();

            MobifiberContract mobifiberContract = contract_DAO.GetAllContractUsingQuery().FirstOrDefault(_o => _o.DeviceId == Id);

            if (mobifiberContract != null)
            {
                List<int> lstStatusOfDevice = new List<int>();
                lstStatusOfDevice.Add((int)MobiFiber.Code.DeviceStatus.NotLinked);
                List<MobifiberDevice> lstDevice = device_DAO.GetDeviceAvailability().Where(_o => lstStatusOfDevice.Contains(_o.Status)).ToList();

                MobifiberDevice CurrentDevice = device_DAO.GetById(Id);
                lstDevice.Add(CurrentDevice);

                return Json(new { status = true, Contract = mobifiberContract, lstDevice = lstDevice, mess = "Thành công !" });
            }
            else
            {
                return Json(new { status = false, mess = "Hợp đồng không tồn tại !" });
            }
        }

        private int ValidateDevice(int Id, string DeviceName, string DeviceCode, string SerialDevice, decimal DevicePrice, int AllocationTime, string DateInputWarehouse, int DeviceStatus)
        {
            DateTime date;
            if (string.IsNullOrEmpty(DeviceName))
            {
                return (int)ERR_VALIDATE_DEVICE.ERR_1;
            }
            if (string.IsNullOrEmpty(DeviceCode))
            {
                return (int)ERR_VALIDATE_DEVICE.ERR_2;
            }
            if (string.IsNullOrEmpty(SerialDevice))
            {
                return (int)ERR_VALIDATE_DEVICE.ERR_3;
            }
            if (string.IsNullOrEmpty(DateInputWarehouse))
            {
                return (int)ERR_VALIDATE_DEVICE.ERR_6;
            }
            if (!DateTime.TryParseExact(DateInputWarehouse, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                return (int)ERR_VALIDATE_CONTRACT.ERR_7;
            }
            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(Id);
            if (mobifiberDevice == null || (mobifiberDevice.Status != (int)MobiFiber.Code.DeviceStatus.NotLinked && Id <= 0))  // check when add new
            {
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Linked)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_9;
                }
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Delete)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_13;
                }
            }
            //Contract_DAO contract_DAO = new Contract_DAO();
            //MobifiberContract mobifiberContract = contract_DAO.GetById(Id);

            return (int)ERR_VALIDATE_DEVICE.ERR_0;
        }
        private int ValidateDeviceUpdate(int Id)
        {
            Device_DAO device_DAO = new Device_DAO();
            MobifiberDevice mobifiberDevice = device_DAO.GetById(Id);
            if (mobifiberDevice == null || mobifiberDevice.Status != (int)MobiFiber.Code.DeviceStatus.NotLinked)  // check when add new
            {
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Linked)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_9;
                }
                if (mobifiberDevice.Status == (int)MobiFiber.Code.DeviceStatus.Delete)
                {
                    return (int)ERR_VALIDATE_CONTRACT.ERR_13;
                }
            }
            return (int)ERR_VALIDATE_DEVICE.ERR_0;
        }

        #region Import Device
        //[HttpPost]
        //public ActionResult ImportFile(IFormFile files)
        //{
        //    List<object> lstResult = new List<object>();
        //    List<object> lstIgnore = new List<object>();
        //    //List<string> lstErrorMessage = new List<string>();
        //    Dictionary<string, string> dic_mapping = new Dictionary<string, string>()
        //                {
        //                    {"A", "DeviceName" },
        //                    {"B", "DeviceCode" },
        //                    {"C", "Serial"},
        //                    {"D", "DevicePrice"},
        //                    {"E", "AllocationTime"},
        //                    {"F", "DateReinputWarehouse"},
        //                };
        //    var fileBases = Request.Form.Files;
        //    //if (fileBase.ContentLength > 0)
        //    if (fileBases.Count > 0)
        //    {
        //        Stream stream = fileBases[0].OpenReadStream();
        //        var validFile = true;//IssueAttachmentManager.IsValidFile(fileBase.FileName, out inValidReason);
        //        if (validFile)
        //        {
        //            ImportExcelHelper.ReadExcelFileStream(stream, lstResult, lstIgnore, dic_mapping);

        //            ViewData["lstResult"] = lstResult;
        //            ViewData["lstIgnore"] = lstIgnore;
        //            //ViewData["lstError"] = lstErrorMessage;

        //            if (lstResult.Count > 0)
        //            {
        //                return Json(new { status = true, message = "", code = IMPORT_SUCCESS, lstResult = lstResult, lstIgnore = lstIgnore, stepId = IMPORT_STEP_VERIFY_DATA }); ;
        //            }
        //            else
        //            {
        //                return Json(new { status = false, message = "File no data.", code = IMPORT_ERROR_FILE_NO_DATA });
        //            }
        //        }
        //        else
        //        {
        //            // khong dung dinh dang
        //            return Json(new { status = false, message = "File is not support.", code = IMPORT_ERROR_FILE_NOT_SUPPORT });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { status = false, message = "please choose file data", code = IMPORT_ERROR_FILE_NOT_CHOOSE });
        //    }
        //    return Json(new { status = false, message = "Unknow", code = IMPORT_ERROR_UNKNOW });
        //}
        #endregion
        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        public JsonResult Import()
        {

            List<DeviceViewModel> lstDevice = new List<DeviceViewModel>();
            List<DeviceViewModel> lstDeviceInvalid = new List<DeviceViewModel>();
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
                    //Process item in file
                    lstDevice = processFile(sheet, ref lstDeviceInvalid, true);
                }

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }


            return Json(new { status = true, mess = "Upload thành công. Kiểm tra dữ liệu trước khi lưu", CountDone = lstDevice.Count(), CountError = lstDeviceInvalid.Count(), lstValid = lstDevice, lstInValid = GetErrStr(lstDeviceInvalid) });
        }
        private List<DeviceViewModel> processFile(ISheet sheet, ref List<DeviceViewModel> lstfail, bool checkInvalid = true)
        {
            Device_DAO device_DAO = new Device_DAO();
            List<MobifiberDevice> listAll = device_DAO.GetAllDeviceUsingQuery();
            List<DeviceViewModel> lstDone = new List<DeviceViewModel>();
            IRow headerRow = sheet.GetRow(0); //Get Header Row
            int cellCount = headerRow.LastCellNum;
            int startPoint = 2;
            for (int i = (sheet.FirstRowNum + startPoint); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                DeviceViewModel obj = new DeviceViewModel();
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
                                obj.DeviceName = row.GetCell(j).ToString().Trim();
                                break;
                            case 1:
                                obj.DeviceCode = row.GetCell(j).ToString().Trim();
                                break;
                            case 2:
                                obj.Serial = row.GetCell(j).ToString().Trim();
                                break;
                            case 3:
                                decimal price;
                                if (decimal.TryParse(row.GetCell(j).ToString().Trim(), out price))
                                {
                                    obj.DevicePrice = price;
                                }
                                else
                                {
                                    obj.DevicePrice = decimal.Parse(row.GetCell(j).ToString().Trim());
                                    obj.IsValid = false;
                                    obj.ErrorDevicePrice = (int)Error.Wrongformat;
                                }
                                break;
                            case 4:
                                int AllocationTime;
                                if (int.TryParse(row.GetCell(j).ToString().Trim(), out AllocationTime))
                                {
                                    obj.AllocationTime = AllocationTime;
                                }
                                else
                                {
                                    obj.AllocationTime = int.Parse(row.GetCell(j).ToString().Trim());
                                    obj.IsValid = false;
                                    obj.ErrorAllocationTime = (int)Error.Wrongformat;
                                }
                                break;
                            case 5:
                                DateTime DateInputWarehouse;

                                if (DateTime.TryParseExact(row.GetCell(j).ToString().Trim(), formats, null, DateTimeStyles.None, out DateInputWarehouse))
                                {
                                    obj.DateInputWarehouse = DateInputWarehouse;
                                }
                                else
                                {
                                    obj.DateInputWarehouse = DateInputWarehouse;
                                    obj.IsValid = false;
                                    obj.ErrorDateInputWarehouse = (int)Error.Wrongformat;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        if (checkInvalid)
                        {
                            switch (j)
                            {
                                case 0:
                                    obj.ErrorDeviceName = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 1:
                                    obj.ErrorDeviceCode = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 2:
                                    obj.ErrorSerial = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 3:
                                    obj.ErrorDevicePrice = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 4:
                                    obj.ErrorAllocationTime = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                case 5:
                                    obj.ErrorDateInputWarehouse = (int)Error.Empty;
                                    obj.IsValid = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                if (listAll.FirstOrDefault(_o => _o.DeviceCode == obj.DeviceCode && _o.Serial == obj.Serial) != null)
                {
                    obj.IsValid = false;
                    obj.ErrorDeviceCode = (int)Error.Exist;
                    obj.ErrorSerial = (int)Error.Exist;
                }
                else
                {
                    listAll.Add(mapper.Map<MobifiberDevice>(obj));
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
        private List<DeviceViewModel> GetErrStr(List<DeviceViewModel> lst)
        {
            foreach (DeviceViewModel item in lst)
            {
                string str = string.Empty;
                if (item.ErrorDeviceName > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Tên thiết bị " + Common.GetEnumDescription((Error)item.ErrorDeviceName);
                }
                if (item.ErrorDeviceCode > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Mã thiết bị " + Common.GetEnumDescription((Error)item.ErrorDeviceCode);
                }
                if (item.ErrorSerial > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Serial thiết bị " + Common.GetEnumDescription((Error)item.ErrorSerial);
                }
                if (item.ErrorDevicePrice > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Giá thiết bị " + Common.GetEnumDescription((Error)item.ErrorDevicePrice);
                }
                if (item.ErrorAllocationTime > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Thời gian phân bổ " + Common.GetEnumDescription((Error)item.ErrorAllocationTime);
                }
                if (item.ErrorDateInputWarehouse > 0)
                {
                    str += str != "" ? ", " : "";
                    str += "Ngày nhập kho " + Common.GetEnumDescription((Error)item.ErrorDateInputWarehouse);
                }
                item.ErrStr = str;
            }
            return lst;
        }

        [UserAuthorize(new ActionModule[] { ActionModule.DeviceManager }, new ActionTypeCustom[] { ActionTypeCustom.Import })]
        [HttpPost]
        public JsonResult SaveData()
        {
            List<DeviceViewModel> lstDevice = new List<DeviceViewModel>();
            List<DeviceViewModel> lstDeviceInvalid = new List<DeviceViewModel>();
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
                    //Process item in file
                    lstDevice = processFile(sheet, ref lstDeviceInvalid, false);
                }

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            List<MobifiberDevice> lstDeviceAdd = new List<MobifiberDevice>();
            for (int i = 0; i < lstDevice.Count; i++)
            {
                lstDeviceAdd.Add(mapper.Map<MobifiberDevice>(lstDevice[i]));

            }
            Device_DAO device_DAO = new Device_DAO();
            bool result = device_DAO.AddRange(lstDeviceAdd);
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
        public JsonResult ExportDeviceError()
        {
            List<DeviceViewModel> lstDevice = new List<DeviceViewModel>();
            List<DeviceViewModel> lstDeviceInvalid = new List<DeviceViewModel>();
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
                    //Process item in file
                    lstDevice = processFile(sheet, ref lstDeviceInvalid, false);
                }

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string urlTemplate = Path.Combine(contentRootPath, @"TemplateExcel\DeviceTemplate.xlsx");
            string pathSave = Path.Combine(_webHostEnvironment.WebRootPath, @"Output");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string fileName = "Danh_Sach_Thiet_Bi.xlsx";

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

                    helper.InsertDatas(temp_item, lstDeviceInvalid);
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
            string path = _webHostEnvironment.ContentRootPath + "\\TemplateExcel\\Danh_Sach_Thiet_Bi.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Template_ThietBi.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
