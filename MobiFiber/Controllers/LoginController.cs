using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Micro.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Micro.Web.PartialViewModel;
using Micro.Web.Code;
using MobiFiber.DAO;

namespace Micro.Web.Controllers
{
    public class LoginController : Controller
    {
        Account_DAO account_DAO = new Account_DAO();
        Role_DAO role_DAO = new Role_DAO();
        Module_DAO module_DAO = new Module_DAO();
        private static string sessionName = Micro.Web.Code.SessionSystem.sessionName;
        private static string sessionManagerRole = Micro.Web.Code.SessionSystem.sessionManagerRole;
        public IActionResult Index()
        {
            ViewData["Title"] = "Login";
            //var cookie = Request.Cookies[sessionName];
            //if (!string.IsNullOrEmpty(cookie))
            //{
            //    return Redirect("/CMS/Home");
            //}
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                if (cookieKey.Equals(sessionName) || cookieKey.Equals(sessionManagerRole))
                {
                    Response.Cookies.Delete(cookieKey);
                }
            }
            SessionSystem.userSession = null;
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(LoginModel model, string returnUrl = "")
        {
            ViewData["MsgLogin"] = "Tài khoản hoặc mật khẩu không hợp lệ !";

            if (ModelState.IsValid)
            {
                var session = HttpContext.Session.GetString(sessionName);
                string password = Micro.Web.Code.SHA.sha256_hash(model.Password);
                var userObj = account_DAO.Login(model.UserName);
                if (model.Remember)
                {
                    Response.Cookies.Append("UserName", model.UserName, new CookieOptions() { Expires = DateTime.Now.AddDays(30) });
                    Response.Cookies.Append("Password", model.Password, new CookieOptions() { Expires = DateTime.Now.AddDays(30) });
                }
                var retVal = LoginHandler(userObj, password, ref returnUrl);
                if (retVal)
                {
                    return Redirect(returnUrl);
                }
                return View("Index");
            }
            return View("Index");
        }

        private bool LoginHandler(UserObject userObj, string password, ref string returnUrl)
        {
            if (userObj.userInfo != null && userObj.userInfo.Id != Guid.Empty)
            {
                var userInfo = userObj.userInfo;
                if (!userInfo.Active.HasValue || (userInfo.Active.HasValue && !userInfo.Active.Value))
                {
                    //ViewData["MsgLogin"] = "Tài khoản chưa được kích hoạt !";
                }
                else
                {
                    if (userInfo.Password.Equals(password))
                    if (true)
                    {
                        var sessUser = new UserSession
                        {
                            UserId = userInfo.Id,
                            FirstName = userInfo.FirstName,
                            LastName = userInfo.LastName,
                            Password = userInfo.Password,
                            UserName = userInfo.UserName,
                            Role = userInfo.Role.HasValue ? userInfo.Role.Value : 0
                        };
                        SessionSystem.userSession = sessUser;
                        var roleID = userInfo.Role.HasValue ? userInfo.Role.Value : 0;
                        var roleList = role_DAO.GetByGroupID(roleID);
                        var allModule = module_DAO.ListAll();
                        var userRoles = new List<RoleEntity>();
                        for (int i = 0; i < roleList.Count; i++)
                        {
                            RoleEntity entity = new RoleEntity
                            {
                                Add = roleList[i].Add,
                                Delete = roleList[i].Delete,
                                Edit = roleList[i].Edit,
                                Export = roleList[i].Export,
                                GroupId = roleList[i].GroupId,
                                Import = roleList[i].Import,
                                ModuleId = roleList[i].ModuleId.Value,
                                Publish = roleList[i].Publish,
                                Upload = roleList[i].Upload,
                                View = roleList[i].View
                            };
                            for (int j = 0; j < allModule.Count; j++)
                            {
                                if (roleList[i].ModuleId == allModule[j].Id)
                                {
                                    entity.ModuleName = allModule[j].Name;
                                }
                            }
                            userRoles.Add(entity);
                        }

                        Response.Cookies.Append(sessionName, JsonConvert.SerializeObject(sessUser), new CookieOptions() { Expires = DateTime.Now.AddHours(1), IsEssential = true });
                        //Response.Cookies.Append(sessionManagerRole, JsonConvert.SerializeObject(userRoles), new CookieOptions() { Expires = DateTime.Now.AddHours(1), IsEssential = true });
                        returnUrl = "/Home";
                        return true;
                    }
                    else
                    {
                        ViewData["MsgLogin"] = "Tài khoản hoặc mật khẩu sai !";
                    }
                }
            }
            else
            {
                //ViewData["MsgLogin"] = "Tài khoản hoặc mật khẩu không hợp lệ !";
            }
            return false;
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove(sessionName);
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                if (cookieKey.Equals(sessionName) || cookieKey.Equals(sessionManagerRole))
                {
                    Response.Cookies.Delete(cookieKey);
                }
            }
            SessionSystem.userSession = null;
            return RedirectToAction("Index", "Login");
        }
    }
}