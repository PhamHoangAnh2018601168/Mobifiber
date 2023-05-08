using MobiFiber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.DAO
{
    public class Setting_DAO
    {
        private MobiFiberContext _context = null;
        public Setting_DAO()
        {
            _context = new MobiFiberContext();
        }
    }
}
