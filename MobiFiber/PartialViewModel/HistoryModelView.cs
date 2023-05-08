using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class HistoryDeviceView
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string ContractNumber { get; set; }
        public int DeviceId { get; set; }
        public int Action { get; set; }
        public string FieldChange { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public DateTime DateCreate { get; set; }
        public string Description { get; set; }
    }
    public class HistoryContractDeviceView
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public int AllocationTime { get; set; }
        public string DeviceCode { get; set; }
        public int Action { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public string FieldChange { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime SignDate { get; set; }

    }
    public class HistoryContractPackageView
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public Decimal PriceVAT { get; set; }
        public int TimeUsed { get; set; }
        public int Action { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayHuy { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime SignDate { get; set; }

        public string FieldChange { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }

    }
    public class HistoryPackageView
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string ContractNumber { get; set; }
        public int PackageId { get; set; }
        public int Action { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayHuy { get; set; }
        public string FieldChange { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
