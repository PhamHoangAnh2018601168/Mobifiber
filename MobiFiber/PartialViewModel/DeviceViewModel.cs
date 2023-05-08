using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.PartialViewModel
{
    public class DeviceViewModel
    {
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string Serial { get; set; }
        public DateTime? DateInputWarehouse { get; set; }
        public decimal DevicePrice { get; set; }
        public int AllocationTime { get; set; }

        public bool IsValid { get; set; }
        public int ErrorDeviceName { get; set; }
        public int ErrorDeviceCode { get; set; }
        public int ErrorSerial { get; set; }
        public int ErrorDevicePrice { get; set; }
        public int ErrorAllocationTime { get; set; }
        public int ErrorDateInputWarehouse { get; set; }
        public string ErrStr { get; set; }
    }
}
