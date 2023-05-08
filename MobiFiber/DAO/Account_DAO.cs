using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Micro.Web.PartialViewModel;
using MobiFiber.Models;
using MobiFiber.Code;

namespace MobiFiber.DAO
{
    public class Account_DAO
    {
        private MobiFiberContext _context = null;
        public Account_DAO()
        {
            _context = new MobiFiberContext();
        }

        public UserObject Login(string userName)
        {
            var userObject = new UserObject();
            var account = _context.Accounts.Where(s => s.UserName == userName).FirstOrDefault();
            userObject.userInfo = account;
            return userObject;
        }
        public List<Account> ListAll()
        {
            var list = _context.Accounts.ToList();
            return list;
        }
        public IPagedList<Account> ListAllbyTypeAndPage(string type, int pageIndex, int pageSize)
        {
            var list = _context.Accounts.Where(m => m.Active == true).ToList();
            //if (type != "" && type != null)
            //{
            //    list = _context.StaffTypes.Where(m => m.TypeName == type).Join(list, d => d.Id, k => k.StaffTypeId, (d, k) => k).ToList();
            //}
            return list.ToPagedList(pageIndex, pageSize);
        }
        public List<Account> ListAllByStaffType(int type)
        {
            var list = _context.Accounts.Where(m => m.StaffTypeId == type).ToList();
            return list;
        }
        public List<Account> ListAllByText(string search)
        {
            var list = _context.Accounts.ToList();
            if (search != null && search != "")
            {
                list = list.Where(m => m.FirstName.Contains(search) || m.LastName.Contains(search)).ToList();
            }
            return list;
        }
        public List<Account> ListAllByTypeAndText(string search, string type)
        {
            var list = _context.Accounts.ToList();
            //if (type != "" && type != null)
            //{
            //    list = _context.StaffTypes.Where(m => m.TypeName == type).Join(list, d => d.Id, k => k.StaffTypeId, (d, k) => k).ToList();
            //}
            if (search != null && search != "")
            {
                list = list.Where(m => m.FirstName.Contains(search) || m.LastName.Contains(search)).ToList();
            }
            return list;
        }
        public Guid? Create(Account p)
        {
            try
            {
                _context.Accounts.Add(p);
                _context.SaveChanges();
                return p.Id;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return null;
            }
        }
        public bool Update(Account type)
        {
            try
            {
                _context.Accounts.Update(type);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }
        public bool UpdateProfile(Account Obj)
        {
            try
            {
                _context.Accounts.Update(Obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }
        public Account GetById(Guid Id)
        {
            return _context.Accounts.Find(Id);
        }
        public bool Delete(Guid Id)
        {
            try
            {
                var res = _context.Accounts.Find(Id);
                _context.Accounts.Remove(res);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }
        public bool CheckAccount(string name, string email)
        {
            try
            {
                var res = _context.Accounts.Where(m => m.UserName.ToLower() == name.ToLower() || m.Email.ToLower() == email.ToLower());
                if (res.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
