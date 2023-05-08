using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobiFiber.DAO
{
    public class Module_DAO
    {
        private MobiFiberContext _context = null;
        public Module_DAO()
        {
            _context = new MobiFiberContext();
        }

        public List<Module> ListAll()
        {
            return _context.Modules.ToList();
        }
    }
}
