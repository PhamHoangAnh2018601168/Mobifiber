using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class PackageViewModel
    {
        public string PackageName { get; set; }
        public string PackageNumber { get; set; }
        public string Decision { get; set; }
        public int TimeUsed { get; set; }
        public int? PromotionTime { get; set; }
        public decimal Price { get; set; }
        public decimal PriceVat { get; set; }

        public bool IsValid { get; set; }
        public int ErrorPackageName { get; set; }
        public int ErrorPackageNumber { get; set; }
        public int ErrorDecision { get; set; }
        public int ErrorTimeUsed { get; set; }
        public int ErrorPromotionTime { get; set; }
        public int ErrorPrice { get; set; }
        public int ErrorPriceVat { get; set; }
        public string ErrStr { get; set; }
    }
}
