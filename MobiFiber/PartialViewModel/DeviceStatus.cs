using System;

namespace MobiFiber.PartialViewModel
{
    public class DeviceStatus
    {
        public int RowNumber { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerIDVM { get; set; }
        public string Address { get; set; }
        public string PackageName { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime DateUndo { get; set; }
    }
    public class DeviceStatusNoActive
    {
        public int RowNumber { get; set; }
        public int DeviceId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public DateTime? DateInputWarehouse { get; set; }
        public decimal DevicePrice { get; set; }
        public int AllocationTime { get; set; }
        public DateTime? DateReinputWarehouse { get; set; }
        public Guid? UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public int IsActive { get; set; }
        public DateTime? DateActive { get; set; }
        public int Status { get; set; }
        public Guid? UserLastUpdate { get; set; }
        public DateTime? DateLastUpdate { get; set; }
        public string Active { get; set; }  
        public string DeviceStatus { get; set; }

    }
}
