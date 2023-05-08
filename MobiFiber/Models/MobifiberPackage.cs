using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberPackage
    {
        [Description("ID gói cước")]
        public int PackageId { get; set; }
        [Description("Tên gói cước")]
        public string PackageName { get; set; }
        [Description("Mã gói cước")]
        public string PackageNumber { get; set; }
        [Description("Giá gói cước VAT")]
        public decimal PriceVat { get; set; }
        [Description("Giá gói cước")]
        public decimal Price { get; set; }
        [Description("Thời gian sử dụng")]
        public int TimeUsed { get; set; }
        [Description("Thời gian Khuyến mãi")]
        public int? PromotionTime { get; set; }
        [Description("Số công văn")]
        public string Decision { get; set; }
        [Description("Người tạo")]
        public Guid? UserCreate { get; set; }
        [Description("Ngày tạo")]
        public DateTime? DateCreate { get; set; }
        [Description("Trạng thái")]
        public int Status { get; set; }
        [Description("Người update cuối cùng")]
        public Guid? UserLastUpdate { get; set; }
        [Description("Ngày update cuối cùng")]
        public DateTime? DateLastUpdate { get; set; }
    }
}
