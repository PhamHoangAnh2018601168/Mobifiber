using Micro.Web;
using Micro.Web.PartialViewModel;
using MobiFiber.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MobiFiber.Code
{
    public class Common
    {
        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (attributes != null && attributes.Any())
                {
                    return attributes.First().Description;
                }
                return value.ToString();
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public static bool isRole(ActionModule[] Modules, ActionTypeCustom[] ActionTypeCustoms, string cookie)
        {
            var _context = new MobiFiberContext();
            var user = JsonConvert.DeserializeObject<UserSession>(cookie);
            List<int> lstRoleGroup = _context.RoleGroups.Where(_o => _o.Id == user.Role && _o.Disable != (int)GroupStatus.Disable).Select(_o => _o.Id).ToList();

            List<Role> lstRole = _context.Roles.Where(_o => lstRoleGroup.Contains(_o.GroupId)).ToList();
            return AccessUser.checkAccess(lstRole, Modules, ActionTypeCustoms, user.Role);
        }

        public static T clone<T>(T B)
        {
            string output = JsonConvert.SerializeObject(B);
            return JsonConvert.DeserializeObject<T>(output);
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static string GetDescription<T>(string fieldName)
        {
            string result = string.Empty;
            var getAllInfor = typeof(T).GetProperties();
            var fi = getAllInfor.Where(_o => _o.Name == fieldName).FirstOrDefault();
            if (fi != null)
            {
                object[] descriptionAttrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttrs.Count() > 0)
                {
                    DescriptionAttribute description = (DescriptionAttribute)descriptionAttrs[0];
                    result = (description.Description);
                }
                else
                {
                    return result;
                }
            }
            return result;
        }
    }
}
