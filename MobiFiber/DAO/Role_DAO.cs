using Micro.Web.Code;
using Microsoft.EntityFrameworkCore;
using MobiFiber.Code;
using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobiFiber.DAO
{
    public class Role_DAO
    {
        private MobiFiberContext _context = null;
        public Role_DAO()
        {
            _context = new MobiFiberContext();
        }
        /// <summary>
        /// Thêm 1 object condition cho đk.
        /// </summary>
        /// <returns></returns>
        public List<Role> ListAll()
        {
            return _context.Roles.AsNoTracking().ToList();
        }
        public List<RoleGroup> ListAllRoleGroup()
        {
            return _context.RoleGroups.ToList();
        }

        public List<RoleGroup> ListAllGroup()
        {
            return _context.RoleGroups.ToList();
        }

        public List<Role> GetByGroupID(int groupID)
        {
            List<Role> list = new List<Role>();
            if (groupID > 0)
            {
                list = _context.Roles.Where(s => s.GroupId == groupID).ToList();
            }
            return list;
        }

        public int UpdateRoleGroup(List<Role> lstRole)
        {
            _context.Roles.UpdateRange(lstRole);
            return _context.SaveChanges();
        }

        public int UpdateGroup(RoleGroup roleGroup)
        {
            _context.RoleGroups.Update(roleGroup);
            return _context.SaveChanges();
        }
        public RoleGroup GetRoleGroupID(int groupID)
        {
            return _context.RoleGroups.AsNoTracking().FirstOrDefault(o => o.Id == groupID);
        }
        public MobifiberConfig MobiConfig()
        {
            MobifiberConfig obj = _context.MobifiberConfigs.FirstOrDefault(o => o.Key == "LockRole");
            return obj;
        }
        public int UpdateAndCreateConfig(MobifiberConfig Config)
        {
            var lst = _context.MobifiberConfigs.FirstOrDefault(o => o.Key == Config.Key);
            if (lst != null)
            {
                lst.UserLastUpdate = SessionSystem.userSession.UserId;
                lst.DateLastUpdate = DateTime.Now;
                lst.Value = Config.Value;
                lst.Status = Config.Status;
                _context.MobifiberConfigs.Update(lst);
                WriteLogToDatabase.AddLog(
                            (int)ActionModule.RoleGroup,
                           "Cập nhật quyền phân kỳ",
                           SessionSystem.userSession.UserId,
                           DateTime.Now,
                           (int)ActionTypeCustom.Add,
                           Constant.DEFAULT,
                           "Khóa quyền",
                           Config.Value,
                           lst.Value
                           );
            }
            else
            {
                Config.UserCreate = SessionSystem.userSession.UserId;
                Config.DateCreate = DateTime.Now;
                _context.MobifiberConfigs.Update(Config);
                WriteLogToDatabase.AddLog(
                           (int)ActionModule.RoleGroup,
                          "Tạo khóa quyền phân kỳ",
                          SessionSystem.userSession.UserId,
                          DateTime.Now,
                          (int)ActionTypeCustom.Add,
                          Constant.DEFAULT,
                          "Khóa quyền",
                          Config.Value,
                          ""
                          );

            }


            return _context.SaveChanges();
        }

    }
}
