using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class CustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerIdvm { get; set; }
        public string IdentityCard { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Phone { get; set; }
        public string ContractNumber { get; set; }
        public string Serial { get; set; }
        public DateTime? SignDate { get; set; }
        public string PackageName { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string DeviceName { get; set; }
        public string AgentName { get; set; }
        public string BillNumber { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal BillPrice { get; set; }
        public string DeveloperName { get; set; }
        public string InfrastructurePartners { get; set; }
        public string TypeOfCooperation { get; set; }

        public bool IsValid { get; set; }
        public int ErrorCustomerName { get; set; }
        public int ErrorCustomerIdvm { get; set; }
        public int ErrorIdentityCard { get; set; }
        public int ErrorAddress { get; set; }
        public int ErrorTaxCode { get; set; }
        public int ErrorPhone { get; set; }
        public int ErrorContractNumber { get; set; }
        public int ErrorSignDate { get; set; }
        public int ErrorPackageName { get; set; }
        public int ErrorRegisterDate { get; set; }
        public int ErrorDeviceName { get; set; }
        public int ErrorAgentcodeAm { get; set; }
        public int ErrorBillNumber { get; set; }
        public int ErrorBillDate { get; set; }
        public int ErrorBillPrice { get; set; }
        public int ErrorDeveloperName { get; set; }
        public int ErrorInfrastructurePartners { get; set; }
        public int ErrorTypeOfCooperation { get; set; }
        public string ErrStr { get; set; }
    }
}
