using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class DeviceCostAllocation
    {
        public int RowNumber { get; set; }
        public int IsActive { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public string StatusDevice { get; set; }
        public decimal DevicePrice { get; set; }
        public string CustomerName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime DateActive { get; set; }
        public DateTime SignDate { get; set; }
        public int AllocationTime { get; set; }
        public DateTime EndAllocationTime { get; set; }
        public DateTime StopAllocation { get; set; }
        public int Status { get; set; }

        public decimal SoDuThangTruocChuyenSang { get; set; }
        public decimal SoPhatSinhMoiTrongKy { get; set; }
        public decimal ChiPhiPhanBo { get; set; }
        public decimal SoDuConPhaiPhanBo { get; set; }
    }
}
