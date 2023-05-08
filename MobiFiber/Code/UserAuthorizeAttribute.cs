using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Micro.Web.PartialViewModel;
using MobiFiber.DAO;
using MobiFiber.Models;
using Micro.Web.Code;
using MobiFiber.Code;

namespace Micro.Web
{
    public class UserAuthorizeAttribute : TypeFilterAttribute
    {
        public UserAuthorizeAttribute(ActionModule[] modules, ActionTypeCustom[] actionTypeCustoms) : base(typeof(UserAuthorizeFilter))
        {
            //Arguments = new object[] { new Claim(type, value) };
            Arguments = new object[] { modules, actionTypeCustoms };
        }
    }

    public class UserAuthorizeFilter : IAuthorizationFilter
    {
        //readonly Claim _claim;
        public ActionModule[] Modules;
        public ActionTypeCustom[] ActionTypeCustoms;
        private static string sessionManagerRole = Micro.Web.Code.SessionSystem.sessionManagerRole;
        private static string sessionName = Micro.Web.Code.SessionSystem.sessionName;

        public UserAuthorizeFilter(ActionModule[] modules, ActionTypeCustom[] actionTypeCustoms)
        {
            Modules = modules;
            ActionTypeCustoms = actionTypeCustoms;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookie = context.HttpContext.Request.Cookies[sessionName];
            if (!string.IsNullOrEmpty(cookie))
            {
                var controller = Convert.ToString(context.RouteData.Values["controller"]);
                var action = Convert.ToString(context.RouteData.Values["action"]);
                Modules = Modules ?? new ActionModule[] { };
                ActionTypeCustoms = ActionTypeCustoms ?? new ActionTypeCustom[] { };
                if (Modules.Length == 0 && ActionTypeCustoms.Length == 0)
                {
                    OnAuthorization(context);
                    return;
                }
                var user = JsonConvert.DeserializeObject<UserSession>(cookie);

                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }
                else
                {
                    SessionSystem.userSession = user;
                }

                var _context = new MobiFiberContext();
                Account acc = _context.Accounts.FirstOrDefault(o => o.UserName == user.UserName);

                if (!acc.Active.Value)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorised", action = "Index" }));
                }

                List<int> lstRoleGroup = _context.RoleGroups.Where(_o => _o.Id == user.Role && _o.Disable != (int)GroupStatus.Disable).Select(_o => _o.Id).ToList();

                List<Role> lstRole = _context.Roles.Where(_o => lstRoleGroup.Contains(_o.GroupId)).ToList();

                if (lstRole != null && lstRole.Count > 0)
                {
                    var checkAccess = AccessUser.checkAccess(lstRole, Modules, ActionTypeCustoms, user.Role);
                    if (!checkAccess)
                    {
                        if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        {
                            context.Result = new JsonResult(new
                            {
                                status = false,
                                code = 403,
                                message = Constant.MESSAGE_UNAUTHORISED //"Yêu cầu trái phép <br> Bạn không có quyền truy cập tài nguyên được yêu cầu do hạn chế về bảo mật.<br>Để được truy cập, vui lòng liên hệ với quản trị viên hệ thống của bạn."

                            });
                        }
                        else
                        {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorised", action = "Index" }));
                        }
                    }
                }
                else
                {
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        context.Result = new JsonResult(new
                        {
                            status = false,
                            code = 403,
                            message = Constant.MESSAGE_UNAUTHORISED //"Yêu cầu trái phép <br> Bạn không có quyền truy cập tài nguyên được yêu cầu do hạn chế về bảo mật.<br>Để được truy cập, vui lòng liên hệ với quản trị viên hệ thống của bạn."

                        });
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorised", action = "Index" }));
                    }
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            //var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            //if (!hasClaim)
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }

    public class AccessUser
    {
        private static Role_DAO role_DAO = new Role_DAO();
        public static bool checkAccess(List<Role> AllRoleModuleUser, ActionModule[] Modules, ActionTypeCustom[] ActionTypeCustoms, int roleID)
        {
            if (roleID <= 0)
            {
                return false;
            }
            AllRoleModuleUser = AllRoleModuleUser ?? new List<Role>();
            Modules = Modules ?? new ActionModule[] { };
            ActionTypeCustoms = ActionTypeCustoms ?? new ActionTypeCustom[] { };
            var ModulesList = Modules.Select(e => (int)e).ToList();
            if (ModulesList.Contains(-1))
            {
                return true;
            }
            var CheckUserModule = AllRoleModuleUser.Where(e => ModulesList.Contains(e.ModuleId.Value)).ToList();
            var checkAccess = false;
            foreach (var actionType in ActionTypeCustoms)
            {
                if (actionType == ActionTypeCustom.All)
                {
                    return true;
                }
                foreach (var module in CheckUserModule)
                {
                    switch (actionType)
                    {
                        case ActionTypeCustom.Add:
                            checkAccess = module.Add ? module.Add : false;
                            break;
                        case ActionTypeCustom.Edit:
                            checkAccess = module.Edit ? module.Edit : false;
                            break;
                        case ActionTypeCustom.View:
                            checkAccess = module.View ? module.View : false;
                            break;
                        case ActionTypeCustom.Delete:
                            checkAccess = module.Delete ? module.Delete : false;
                            break;
                        case ActionTypeCustom.Import:
                            checkAccess = module.Import ? module.Import : false;
                            break;
                        case ActionTypeCustom.Export:
                            checkAccess = module.Export ? module.Export : false;
                            break;
                        case ActionTypeCustom.Upload:
                            checkAccess = module.Upload ? module.Upload : false;
                            break;
                        case ActionTypeCustom.Pushlish:
                            checkAccess = module.Publish ? module.Publish : false;
                            break;
                        case ActionTypeCustom.Report:
                            checkAccess = module.Report ? module.Report : false;
                            break;
                        default:
                            checkAccess = false;
                            break;
                    }
                    if (checkAccess == true)
                    {
                        break;
                    }
                }
            }
            return checkAccess;
        }
    }

    public static class GetUserRole
    {
        public static Dictionary<int, bool> GetRoleByUser()
        {
            Guid userId = SessionSystem.userSession.UserId;
            var _context = new MobiFiberContext();
            Account acc = _context.Accounts.FirstOrDefault(o => o.Id == userId);
            List<Role> lstRole = _context.Roles.Where(o => o.GroupId == acc.Role).ToList();

            Dictionary<int, bool> dicRole = new Dictionary<int, bool>();

            foreach (Role item in lstRole)
            {
                dicRole.Add(item.ModuleId.Value, item.View);
            }
            return dicRole;
        }
        public static Dictionary<int, bool> GetRoleAddByUser()
        {
            Guid userId = SessionSystem.userSession.UserId;
            var _context = new MobiFiberContext();
            Account acc = _context.Accounts.FirstOrDefault(o => o.Id == userId);
            List<Role> lstRole = _context.Roles.Where(o => o.GroupId == acc.Role).ToList();

            Dictionary<int, bool> dicRole = new Dictionary<int, bool>();

            foreach (Role item in lstRole)
            {
                dicRole.Add(item.ModuleId.Value, item.Add);
            }
            return dicRole;
        }
        public static Dictionary<int, bool> GetRoleExportByUser()
        {
            Guid userId = SessionSystem.userSession.UserId;
            var _context = new MobiFiberContext();
            Account acc = _context.Accounts.FirstOrDefault(o => o.Id == userId);
            List<Role> lstRole = _context.Roles.Where(o => o.GroupId == acc.Role).ToList();

            Dictionary<int, bool> dicRole = new Dictionary<int, bool>();

            foreach (Role item in lstRole)
            {
                dicRole.Add(item.ModuleId.Value, item.Export);
            }
            return dicRole;
        }
    }
}
