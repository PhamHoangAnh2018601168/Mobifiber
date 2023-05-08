using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class RegisteredCustomers
    {
        public int RowNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerIDVM { get; set; }
        public string ContractNumber { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public string PackageName { get; set; }
        public int TimeUsed { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime EndService { get; set; }
        public decimal PriceVAT { get; set; }
    }
}
