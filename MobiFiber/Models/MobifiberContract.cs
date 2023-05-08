using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberContract
    {
        [Description("ID Hợp đồng")]
        public int ContractId { get; set; }
        [Description("Mã khách hàng (VM)")]
        public string CustomerIdvm { get; set; }
        [Description("CCCD/CMND")]
        public string IdentityCard { get; set; }
        [Description("Tên khách hàng")]
        public string CustomerName { get; set; }
        [Description("Số điện thoại")]
        public string Phone { get; set; }
        [Description("Số hợp đồng")]
        public string ContractNumber { get; set; }
        [Description("Địa chỉ")]
        public string Address { get; set; }
        [Description("Gói cước")]
        public int PackageId { get; set; }
        [Description("Ngày đăng ký gói cước")]
        public DateTime? RegisterDate { get; set; }
        [Description("Thiết bị")]
        public int DeviceId { get; set; }
        [Description("Ngày ký hợp đồng")]
        public DateTime? SignDate { get; set; }
        [Description("Ngày hóa đơn")]
        public DateTime? BillDate { get; set; }
        [Description("Số hóa đơn")]
        public string BillNumber { get; set; }
        [Description("Số tiền trên hóa đơn")]
        public decimal BillPrice { get; set; }
        [Description("Người tạo")]
        public Guid? UserCreate { get; set; }
        [Description("Ngày tạo")]
        public DateTime? DateCreate { get; set; }
        [Description("Đại lý (AM)")]
        public int? AgentcodeAm { get; set; }
        [Description("Đối tác hạ tầng")]
        public string InfrastructurePartners { get; set; }
        [Description("Đơn vị phát triển")]
        public int? DeveloperName { get; set; }
        [Description("Loại hình hợp tác")]
        public string TypeOfCooperation { get; set; }
        [Description("Trạng thái")]
        public int Status { get; set; }
        [Description("Người update cuối cùng")]
        public Guid? UserLastUpdate { get; set; }
        [Description("Ngày update cuối cùng")]
        public DateTime? DateLastUpdate { get; set; }
        [Description("Ngày thanh lý hợp đồng")]
        public DateTime? LiquidationDate { get; set; }
        [NotMapped]
        public string DateChangeDevice { get; set; }
        [NotMapped]
        public string DateChangePackage { get; set; }
    }
}
