using MobiFiber.Models;
using System;
using System.Collections.Generic;

namespace Micro.Web.PartialViewModel
{
    public class UserObject
    {
        public UserObject()
        {
            userInfo = new Account();
            lstRole = new List<Role>();
        }
        public Account userInfo { get; set; }
        public List<Role> lstRole { get; set; }
    }
}
