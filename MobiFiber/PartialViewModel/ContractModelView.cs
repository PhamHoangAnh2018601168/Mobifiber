using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class ContractModelView
    {
        public int RowNumber { get; set; }
        public int ContractId { get; set; }
        public string CustomerIdvm { get; set; }
        public string IdentityCard { get; set; }
        public string CustomerName { get; set; }
        public string TaxCode { get; set; }
        public string Phone { get; set; }
        public string ContractNumber { get; set; }
        public string Address { get; set; }
        public int? PackageId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? EndService { get; set; }
        public string DayEndService { get; set; }
        public int? DeviceId { get; set; }
        public string DeviceCode { get; set; }
        public int TimeUsed { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? LiquidationDate { get; set; }
        public string BillNumber { get; set; }
        public decimal? BillPrice { get; set; }
        public Guid? UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public string AgentcodeAm { get; set; }
        public string InfrastructurePartners { get; set; }
        public string DeveloperName { get; set; }
        public string StatusName { get; set; }
        public string TypeOfCooperation { get; set; }
        public int? Status { get; set; }
        public Guid? UserLastUpdate { get; set; }
        public DateTime? DateLastUpdate { get; set; }


        public string DeviceName { get; set; }
        public decimal? DevicePrice { get; set; }
        public string Serial { get; set; }
        public string PackageName { get; set; }
        public decimal? PriceVat { get; set; }
    }
}
