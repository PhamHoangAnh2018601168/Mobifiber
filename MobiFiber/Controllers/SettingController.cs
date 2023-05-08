using Micro.Web;
using Micro.Web.Code;
using Microsoft.AspNetCore.Mvc;
using MobiFiber.Code;
using MobiFiber.DAO;
using MobiFiber.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MobiFiber.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [UserAuthorize(new ActionModule[] { ActionModule.GroupManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult GroupManager()
        {
            return View("GroupManager");
        }
        [UserAuthorize(new ActionModule[] { ActionModule.RoleGroup }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult RoleGroup(int? Id = 0)
        {
            Role_DAO role_DAO = new Role_DAO();
            MobifiberConfig obj = role_DAO.MobiConfig();
            ViewData["RoleConfig"] = obj;
            RoleGroup roleGroup = role_DAO.GetRoleGroupID((int)Group.Sale);
            ViewData["DisableSaler"] = roleGroup.Disable;
            var lstRole = RoleGroupList(Id.Value);
            return View("RoleGroup", lstRole);
        }

        [HttpPost]
        [UserAuthorize(new ActionModule[] { ActionModule.RoleGroup }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public IActionResult RoleGroup(List<Role> entity)
        {
            Role_DAO role_DAO = new Role_DAO();
            MobifiberConfig obj = role_DAO.MobiConfig();
            ViewData["RoleConfig"] = obj;
            int Id = entity.Select(p => p.GroupId).FirstOrDefault();
            RoleGroup roleGroup = role_DAO.GetRoleGroupID((int)Group.Sale);
            ViewData["DisableSaler"] = roleGroup.Disable;
            int result = role_DAO.UpdateRoleGroup(entity);
            var lstRole = RoleGroupList(Id);
            return View("RoleGroup", lstRole);
        }

        public List<Role> RoleGroupList(int Id)
        {
            Role_DAO role_DAO = new Role_DAO();
            Module_DAO module_DAO = new Module_DAO();
            List<Role> lstRole = role_DAO.GetByGroupID(Id);
            List<RoleGroup> lstRoleGroup = role_DAO.ListAllRoleGroup().Where(o => o.Disable != (int)GroupStatus.Disable).ToList();
            List<Module> lstModuleGroup = module_DAO.ListAll();
            ViewData["lstModuleGroup"] = lstModuleGroup;
            ViewData["lstRoleGroup"] = lstRoleGroup;
            ViewData["IdGroup"] = Id;
            return lstRole;
        }
        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult UserManager()
        {
            Role_DAO role_DAO = new Role_DAO();
            List<RoleGroup> roleGroup = role_DAO.ListAllGroup().Where(o => o.Disable != (int)GroupStatus.Disable).ToList();
            ViewData["roleGroup"] = roleGroup;
            return View("UserManager");
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetDataUser()
        {
            Account_DAO account_DAO = new Account_DAO();
            List<Account> lstAccount = account_DAO.ListAll();
            return Json(new { status = true, rows = lstAccount, mess = "Success" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Add })]
        public JsonResult CreateUser(string username)
        {
            return Json(new { status = true, mess = "Thành công" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult ResetPassword()
        {
            return Json(new { status = true, mess = "Thành công" });
        }
        public JsonResult GetDataUserManager(int offset, int limit, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            Account_DAO account_DAO = new Account_DAO();
            List<Account> lstaccounts = account_DAO.ListAll().Where(o => o.UserName.ToUpper().Contains(search.ToUpper()) || search == "").ToList();
            return Json(new { status = true, rows = lstaccounts.Skip(offset).Take(limit), mess = "Success", total = lstaccounts.Count() });
        }
        public JsonResult GetDataUserById(Guid id)
        {
            Account account = new Account();
            Account_DAO account_DAO = new Account_DAO();
            Role_DAO role_DAO = new Role_DAO();
            account = account_DAO.ListAll().FirstOrDefault(_o => _o.Id == id);
            RoleGroup roleGroup = role_DAO.ListAllGroup().FirstOrDefault(_o => _o.Id == account.Role);
            ViewData["roleGroup"] = roleGroup;
            return Json(new { status = true, rp = account, mess = "Success" });
        }
        [UserAuthorize(new ActionModule[] { ActionModule.UserHistory }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public IActionResult UserHistory()
        {
            Account_DAO account_DAO = new Account_DAO();
            List<Account> lstAccount = account_DAO.ListAll();
            ViewData["lstAccount"] = lstAccount;
            return View();
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserHistory }, new ActionTypeCustom[] { ActionTypeCustom.View })]
        public JsonResult GetHistoryUser(string Uid, string fromDate, string toDate, int moduleId, int actionId, int offset, int limit)
        {
            Uid = string.IsNullOrEmpty(Uid) == true ? "" : Uid.Trim();

            DateTime FromDate;
            DateTime ToDate;
            if (!DateTime.TryParseExact(fromDate, "dd/MM/yyyy", null, DateTimeStyles.None, out FromDate))
            {
                fromDate = string.Empty;
            }
            if (!DateTime.TryParseExact(toDate, "dd/MM/yyyy", null, DateTimeStyles.None, out ToDate))
            {
                toDate = string.Empty;
            }
            List<int> lstType = new List<int>();
            lstType.Add((int)ActionModule.DeviceManager);
            lstType.Add((int)ActionModule.PackageManager);
            lstType.Add((int)ActionModule.ContractManager);

            History_DAO history_DAO = new History_DAO();
            List<MobifiberHistory> LstMobifiberHistory = history_DAO.GetAllHistoryUsingQuery()
                .Where(o => (o.CreateBy.ToString().ToUpper() == Uid.ToUpper() || string.IsNullOrEmpty(Uid))
                && (o.DateCreate.Value.Date >= FromDate || string.IsNullOrEmpty(fromDate))
                && (o.DateCreate.Value.Date <= ToDate || string.IsNullOrEmpty(toDate))
                && (o.Type == moduleId || moduleId == -1)
                && (o.Action == actionId || actionId == -1)
                && lstType.Contains(o.Type.Value)).OrderByDescending(o => o.DateCreate).ToList();

            List<dynamic> lstDataBuild = new List<dynamic>();
            Account_DAO account_DAO = new Account_DAO();
            List<Account> lstAccount = account_DAO.ListAll();

            foreach (MobifiberHistory item in LstMobifiberHistory)
            {
                string Description = string.Empty;
                if (item.Type == (int)ActionModule.DeviceManager)
                {
                    Description = Common.GetDescription<MobifiberDevice>(item.FieldChange);
                }
                if (item.Type == (int)ActionModule.PackageManager)
                {
                    Description = Common.GetDescription<MobifiberPackage>(item.FieldChange);
                }
                if (item.Type == (int)ActionModule.ContractManager)
                {
                    Description = Common.GetDescription<MobifiberContract>(item.FieldChange);
                }

                List<string> optionListDevice = new List<string>(new string[] { "DeviceIdPay", "DeviceIdBorrow" });
                if (optionListDevice.Contains(item.FieldChange))
                {
                    Description = "Thiết bị";
                }

                List<string> optionListPackage = new List<string>(new string[] { "PackageIdPay", "PackageIdBorrow" });
                if (optionListPackage.Contains(item.FieldChange))
                {
                    Description = "Gói cước";
                }

                lstDataBuild.Add(new
                {
                    Id = item.Id,
                    UserName = lstAccount.FirstOrDefault(o => o.Id == item.CreateBy).UserName,
                    Type = item.Type,
                    TypeName = Common.GetEnumDescription((ActionModule)item.Type),
                    Description = item.Description,
                    CreateBy = item.CreateBy,
                    DateCreate = item.DateCreate,
                    DateCreateStr = item.DateCreate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    Action = item.Action,
                    ActionName = Common.GetEnumDescription((ActionTypeCustom)item.Action),
                    IdRefer = item.IdRefer,
                    FieldChange = item.FieldChange,
                    FieldChangeStr = Description,
                    NewValue = item.NewValue,
                    OldValue = item.OldValue,
                });
            }

            int total = lstDataBuild.Count();

            return Json(new { status = true, total = total, rows = lstDataBuild.Skip(offset).Take(limit), mess = "Success" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult AdminChangePass(SetPasswordViewModel PasswordViewModel)
        {
            Account account = new Account();
            Account_DAO account_DAO = new Account_DAO();
            if (string.IsNullOrEmpty(PasswordViewModel.NewPassword))
            {
                return Json(new { status = false, mess = "Mật khẩu mới không được để trống" });
            }
            if (string.IsNullOrEmpty(PasswordViewModel.ConfirmPassword))
            {
                return Json(new { status = false, mess = "Cần nhập lại mật khẩu" });
            }
            if (PasswordViewModel.ConfirmPassword != PasswordViewModel.NewPassword)
            {
                return Json(new { status = false, mess = "Nhập lại mật khẩu không đúng" });
            }
            account = account_DAO.ListAll().FirstOrDefault(_o => _o.Id == PasswordViewModel.Id);
            account.Password = SHA.sha256_hash(PasswordViewModel.NewPassword);

            account_DAO.Update(account);

            return Json(new { status = true, mess = "Mật khẩu được đổi thành công" });
        }
        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult ActiveUser(Guid id, bool active)
        {

            Account account = new Account();
            Account_DAO account_DAO = new Account_DAO();
            account = account_DAO.ListAll().FirstOrDefault(_o => _o.Id == id);
            if (active)
            {
                account.Active = false;
            }
            else
            {
                account.Active = true;
            }
            account_DAO.Update(account);
            return Json(new { status = true, mess = "Thành công" });
        }
        public IActionResult AgentsAndDevelopmentUnit()
        {
            return View("AgentsAndDevelopmentUnit");
        }

        public JsonResult GetDataAgents(int offset, int limit, string search)
        {
            Agents_DAO agents_DAO = new Agents_DAO();
            List<MobifiberAgent> lstAgent = new List<MobifiberAgent>();
            lstAgent = agents_DAO.ListAll();
            return Json(new { status = true, rows = lstAgent.Skip(offset).Take(limit), mess = "Success", total = lstAgent.Count() });
        }
        public JsonResult GetDataDevelopmentUnit(int offset, int limit, string search)
        {
            Developer_DAO developer_DAO = new Developer_DAO();
            List<MobifiberDevelopmentUnit> lstDevelopmentUnit = new List<MobifiberDevelopmentUnit>();
            lstDevelopmentUnit = developer_DAO.ListAll();
            return Json(new { status = true, rows = lstDevelopmentUnit.Skip(offset).Take(limit), mess = "Success", total = lstDevelopmentUnit.Count() });
        }

        public JsonResult CreateOrUpdateAgents(int Id, string AgentsName, string AgentsCode, string AgentsAddr, string AgentsTaxCode, string AgentsPhone, string DescriptionAgents)
        {
            Agents_DAO agents_DAO = new Agents_DAO();
            MobifiberAgent lstAgent = new MobifiberAgent();
            lstAgent.AmId = Id;
            lstAgent.AgentsName = AgentsName;
            lstAgent.AgentCode = AgentsCode;
            lstAgent.Address = AgentsAddr;
            lstAgent.TaxCode = AgentsTaxCode;
            lstAgent.PhoneNumber = AgentsPhone;
            lstAgent.Description = DescriptionAgents;
            string result = agents_DAO.SaveOrUpdate(lstAgent);

            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, mess = "Không thành công, mã đại lý đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công !" });
            }
        }

        public JsonResult CreateOrUpdateDevelopmentUnit(int Id, string DevelopmentUnitName, string DevelopmentUnitCode, string DevelopmentUnitAddr, string DevelopmentUnitTaxCode, string DevelopmentUnitPhone, string DescriptionDevelopmentUnit)
        {
            Developer_DAO developer_DAO = new Developer_DAO();
            MobifiberDevelopmentUnit lstDevelopmentUnit = new MobifiberDevelopmentUnit();
            lstDevelopmentUnit.DevelopId = Id;
            lstDevelopmentUnit.DevelopName = DevelopmentUnitName;
            lstDevelopmentUnit.DevelopCode = DevelopmentUnitCode;
            lstDevelopmentUnit.Address = DevelopmentUnitAddr;
            lstDevelopmentUnit.TaxCode = DevelopmentUnitTaxCode;
            lstDevelopmentUnit.PhoneNumber = DevelopmentUnitPhone;
            lstDevelopmentUnit.Description = DescriptionDevelopmentUnit;
            string result = developer_DAO.SaveOrUpdate(lstDevelopmentUnit);

            if (result == Constant.CODE_SUCCESS)
            {
                return Json(new { status = true, mess = "Thành công" });
            }
            else if (result == Constant.CODE_EXISTS)
            {
                return Json(new { status = false, mess = "Không thành công, mã đơn vị phát triển đã tồn tại !" });
            }
            else
            {
                return Json(new { status = false, mess = "Không thành công !" });
            }
        }

        public JsonResult DeleteDevelopmentUnit(int Id)
        {
            Developer_DAO developer_DAO = new Developer_DAO();
            var result = developer_DAO.Delete(Id);
            return Json(new { status = result, mess = "Thành công" });
        }
        public JsonResult DeleteAgents(int Id)
        {
            Agents_DAO agents_DAO = new Agents_DAO();
            var result = agents_DAO.Delete(Id);
            return Json(new { status = result, mess = "Thành công" });
        }
        [UserAuthorize(new ActionModule[] { ActionModule.RoleGroup }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public JsonResult DisableSaler(bool disable, string datelock)
        {

            //DateTime DateLock = DateTime.ParseExact(datelock, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Role_DAO role_DAO = new Role_DAO();
            List<Role> lstRole = role_DAO.ListAll().Where(_o => _o.GroupId == (int)Group.Sale).ToList();
            MobifiberConfig MBConfig = new MobifiberConfig();
            MBConfig.Key = "LockRole";
            if (disable)  // if true -> backup old role
            {
                MBConfig.Value = datelock;
                MBConfig.Status = "Active";

                //backup data role
                string json = JsonConvert.SerializeObject(lstRole);
                WriteLogToDatabase.AddLog(
                            Constant.DEFAULT,
                            json,
                            SessionSystem.userSession.UserId,
                            DateTime.Now,
                            (int)ActionTypeCustom.Add,
                            Constant.DEFAULT,
                            "",
                            "",
                            ""
                            );

                //disable role saler
                foreach (Role item in lstRole)
                {
                    item.Add = !disable;
                    item.Edit = !disable;
                    item.Delete = !disable;
                    item.Import = !disable;
                    item.Upload = !disable;
                    item.Publish = !disable;
                }
            }
            else
            {
                MBConfig.Value = datelock;
                MBConfig.Status = "Deactive";
                //get history role
                History_DAO history_DAO = new History_DAO();
                MobifiberHistory his = history_DAO.GetHistoryLastRole();
                if (his != null)
                {
                    lstRole = JsonConvert.DeserializeObject<List<Role>>(his.Description);  //list role backup
                }
            }
            role_DAO.UpdateAndCreateConfig(MBConfig);
            role_DAO.UpdateRoleGroup(lstRole);
            RoleGroup roleGroup = role_DAO.GetRoleGroupID((int)Group.Sale);
            roleGroup.Disable = disable == true ? (int)GroupStatus.Limit : (int)GroupStatus.Enable;
            role_DAO.UpdateGroup(roleGroup);

            return Json(new { status = true, mess = "Thành công" });
        }

        public static bool CheckLockRole(int Id, int type, string date)//type Device = 1 , Package = 2 , Contract = 3 
        {
            Role_DAO role_DAO = new Role_DAO();
            MobifiberConfig obj = role_DAO.MobiConfig();
            int monthLock = int.Parse(obj.Value.Substring(3, 2));
            int yearLock = int.Parse(obj.Value.Substring(6, 4));
            if (obj.Status == "Active")
            {
                if (Id == -1)
                {
                    return false;
                }
                if (date == "")
                {
                    switch (type)
                    {
                        case 1:
                            Device_DAO device_DAO = new Device_DAO();
                            MobifiberDevice lstDevice = device_DAO.GetById(Id);
                            if (lstDevice.DateInputWarehouse != null)
                            {
                                int monthDevice = int.Parse(lstDevice.DateInputWarehouse.Value.ToString("dd/MM/yyyy").Substring(3, 2));
                                int yaerDevice = int.Parse(lstDevice.DateInputWarehouse.Value.ToString("dd/MM/yyyy").Substring(6, 4));
                                if (yaerDevice < yearLock)
                                {
                                    return false;
                                }
                                else if (monthDevice < monthLock && yaerDevice == yearLock)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        case 2:
                            Package_DAO package_DAO = new Package_DAO();
                            MobifiberPackage lstPackage = package_DAO.GetById(Id);
                            if (lstPackage.DateCreate != null)
                            {
                                int monthPackage = int.Parse(lstPackage.DateCreate.Value.ToString("dd/MM/yyyy").Substring(3, 2));
                                int yearPackage = int.Parse(lstPackage.DateCreate.Value.ToString("dd/MM/yyyy").Substring(6, 4));
                                if (yearPackage < yearLock)
                                {
                                    return false;
                                }
                                else if (monthPackage < monthLock && yearPackage == yearLock)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        case 3:
                            Contract_DAO contract_DAO = new Contract_DAO();
                            MobifiberContract lstContract = contract_DAO.GetById(Id);
                            if (lstContract.SignDate != null)
                            {
                                int monthContract = int.Parse(lstContract.SignDate.Value.ToString("dd/MM/yyyy").Substring(3, 2));
                                int yearContract = int.Parse(lstContract.SignDate.Value.ToString("dd/MM/yyyy").Substring(6, 4));
                                if (yearContract < yearLock)
                                {
                                    return false;
                                }
                                else if (monthContract < monthLock && yearContract == yearLock)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case 1:

                            int monthDevice = int.Parse(date.Substring(3, 2));
                            int yaerDevice = int.Parse(date.Substring(6, 4));
                            if (yaerDevice < yearLock)
                            {
                                return false;
                            }
                            else if (monthDevice < monthLock && yaerDevice == yearLock)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        case 2:
                            int monthPackage = int.Parse(date.Substring(3, 2));
                            int yearPackage = int.Parse(date.Substring(6, 4));
                            if (yearPackage < yearLock)
                            {
                                return false;
                            }
                            else if (monthPackage < monthLock && yearPackage == yearLock)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        case 3:
                            int monthContract = int.Parse(date.Substring(3, 2));
                            int yearContract = int.Parse(date.Substring(6, 4));
                            if (yearContract < yearLock)
                            {
                                return false;
                            }
                            else if (monthContract < monthLock && yearContract == yearLock)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        default:
                            break;
                    }

                }
            }
            return true;
        }
    }
}
