using Micro.Web;
using Micro.Web.Code;
using Microsoft.AspNetCore.Mvc;
using MobiFiber.Code;
using MobiFiber.DAO;
using MobiFiber.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MobiFiber.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            Account account = new Account();
            Account_DAO account_DAO = new Account_DAO();
            Role_DAO role_DAO = new Role_DAO();
            var sessUser = SessionSystem.userSession;

            if (sessUser != null)
            {
                account = account_DAO.ListAll().FirstOrDefault(_o => _o.UserName == sessUser.UserName);
            }
            RoleGroup roleGroup = role_DAO.ListAllGroup().FirstOrDefault(_o => _o.Id == account.Role);
            ViewData["roleGroup"] = roleGroup;
            return View(account);
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public ActionResult EditProfile(AccountProfile accountProfile)
        {
            Account_DAO account_DAO = new Account_DAO();
            Role_DAO role_DAO = new Role_DAO();
            Account Obj = account_DAO.ListAll().FirstOrDefault(_o => _o.Id == accountProfile.Id);

            Obj.FirstName = accountProfile.FirstName;
            Obj.LastName = accountProfile.LastName;
            Obj.Email = accountProfile.Email;
            Obj.Phone = accountProfile.Phone;
            Obj.Birthday = accountProfile.Birthday;

            var valiDatePhone = ValidatePhone(accountProfile.Phone);
            if (!valiDatePhone)
            {
                ViewData["MessageProfile"] = "Số điện thoại không đúng dịnh dạng";
                return View("Profile", Obj);
            }
            var validateEmail = ValidateEmail(accountProfile.Email);
            if (!validateEmail)
            {
                ViewData["MessageProfile"] = "Email không đúng dịnh dạng";
                return View("Profile", Obj);
            }
            RoleGroup roleGroup = role_DAO.ListAllGroup().FirstOrDefault(_o => _o.Id == Obj.Role);
            ViewData["MessageProfile"] = "Cập nhật thành công";
            ViewData["roleGroup"] = roleGroup;
            account_DAO.UpdateProfile(Obj);
            return View("Profile", Obj);
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public ActionResult ChangePass(SetPasswordViewModel setPasswordViewModel)
        {
            Account account = new Account();
            Account_DAO account_DAO = new Account_DAO();
            if (string.IsNullOrEmpty(setPasswordViewModel.NewPassword))
            {
                ViewData["Message"] = "Mật khẩu mới không được để trống";

                return View("Profile", account);
            }
            if (string.IsNullOrEmpty(setPasswordViewModel.ConfirmPassword))
            {
                ViewData["Message"] = "Cần nhập lại mật khẩu";

                return View("Profile", account);
            }
            if (setPasswordViewModel.ConfirmPassword != setPasswordViewModel.NewPassword)
            {
                ViewData["Message"] = "Nhập lại mật khẩu không đúng";

                return View("Profile", account);
            }
            account = account_DAO.ListAll().FirstOrDefault(_o => _o.Id == setPasswordViewModel.Id);
            var oldPasswordSHA = account.Password;
            var oldPasswordSHACheck = SHA.sha256_hash(setPasswordViewModel.OldPassword);

            if (oldPasswordSHA == oldPasswordSHACheck)
            {
                account.Password = SHA.sha256_hash(setPasswordViewModel.NewPassword);
                account_DAO.Update(account);
                ViewData["Message"] = "Mật khẩu được đổi thành công";
            }
            else
            {
                ViewData["Message"] = "Mật khẩu cũ không đúng";
            }
            return View("Profile", account);
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
        public static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Add })]
        public JsonResult CreateProfile(Account account)
        {
            Account_DAO account_DAO = new Account_DAO();
            Account acc = account_DAO.ListAll().FirstOrDefault(_o => _o.UserName.ToUpper() == account.UserName.ToUpper());

            if(acc != null)
            {
                return Json(new { status = true, mess = "User đã tồn tại !" });
            }

             Account obj = new Account();
            obj.Id = Guid.NewGuid();
            obj.UserName = account.UserName;
            obj.Password = SHA.sha256_hash(account.Password);
            obj.Email = account.Email;
            obj.Active = true;
            obj.Role = account.Role;
            account_DAO.Create(obj);
            return Json(new { status = true, mess = "Tạo tài khoản thành công" });
        }

        [UserAuthorize(new ActionModule[] { ActionModule.UserManager }, new ActionTypeCustom[] { ActionTypeCustom.Edit })]
        public ActionResult EditRole(Account account)
        {
            Account_DAO account_DAO = new Account_DAO();
            Account obj = account_DAO.GetById(account.Id);
            if (obj.Role!=null)
            {
                obj.Email = account.Email;
                obj.LastName = account.LastName;
                obj.FirstName = account.FirstName;
                obj.Role = account.Role;
                account_DAO.Update(obj);
            }
            else
            {
                return null;
            }
            return RedirectToAction("UserManager", "Setting");
        }
    }

    public class SetPasswordViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
    public class AccountProfile
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Birthday { get; set; }
        public string Skype { get; set; }
        public string Gmail { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public int? StaffTypeId { get; set; }
        public bool? IsHome { get; set; }
    }
}
