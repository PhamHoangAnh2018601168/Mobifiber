using System;
using System.ComponentModel;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberDevice
    {
        [Description("ID thiết bị")]
        public int DeviceId { get; set; }
        [Description("Mã thiết bị")]
        public string DeviceCode { get; set; }
        [Description("Tên thiết bị")]
        public string DeviceName { get; set; }
        [Description("Serial")]
        public string Serial { get; set; }
        [Description("Ngày nhập kho")]
        public DateTime? DateInputWarehouse { get; set; }
        [Description("Giá thiết bị")]
        public decimal DevicePrice { get; set; }
        [Description("Thời gian phân bổ")]
        public int AllocationTime { get; set; }
        [Description("Ngày dừng phân bổ")]
        public DateTime? StopAllocation { get; set; }
        [Description("Ngày nhập lại kho")]
        public DateTime? DateReinputWarehouse { get; set; }
        [Description("Người tạo")]
        public Guid? UserCreate { get; set; }
        [Description("Ngày tạo")]
        public DateTime? DateCreate { get; set; }
        [Description("Trạng thái Kích hoạt")]
        public int IsActive { get; set; }
        [Description("Ngày kích hoạt")]
        public DateTime? DateActive { get; set; }
        [Description("Trạng thái")]
        public int Status { get; set; }
        [Description("Người update cuối cùng")]
        public Guid? UserLastUpdate { get; set; }
        [Description("Ngày update cuối cùng")]
        public DateTime? DateLastUpdate { get; set; }
    }
}
