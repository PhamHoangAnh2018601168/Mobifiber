using System.Collections.Generic;
using System.ComponentModel;

namespace MobiFiber.Code
{
    public class Constant
    {
        public static string CODE_SUCCESS = "000";
        public static string CODE_EXISTS = "001";
        public static string CODE_NOT_EXISTS = "002";
        public static string CODE_EXCEPTION = "999";
        public static string CODE_CONFIRM = "003";

        public static int ID_EVICTION = 0;
        public static int DEFAULT = 0;
        public static string ID_EVICTION_STR = "Thu hồi";
        public static string MESSAGE_UNAUTHORISED = "Yêu cầu trái phép <br> Bạn không có quyền truy cập tài nguyên được yêu cầu do hạn chế về bảo mật.<br>Để được truy cập, vui lòng liên hệ với quản trị viên hệ thống của bạn.";
        public static List<string> LIST_PROPERTY_IGNOR_WRITELOG = new List<string>(new string[] { "UserCreate", "DateCreate", "UserLastUpdate", "DateLastUpdate" });
        public static string UPDATE_DEVICE_PAY = "Thu hồi thiết bị";
        public static string UPDATE_DEVICE_BORROW = "Thiết bị đổi";
        public static string UPDATE_PACKAGE_PAY = "Đổi gói cước";
        public static string UPDATE_PACKAGE_BORROW = "Đổi gói cước";
        public static string CREATE = "Thêm mới";
        public static string UPDATE = "Sửa";
        public static string DELETE = "Xoá";
    }

    #region Enum
    public enum ActionModule
    {
        All = -1,
        Home = 1,
        [Description("Thiết bị")]
        DeviceManager = 2,
        [Description("Gói cước")]
        PackageManager = 3,
        [Description("Khách hàng")]
        ContractManager = 4,

        UserManager = 5,
        GroupManager = 6,
        RoleGroup = 7,

        ReprotRegisteredCustomers = 8,
        DeviceCostAllocation = 9,
        ServiceRevenueAllocation = 10,
        DeviceStatus = 11,
        UserHistory = 12,
    }
    public enum ActionTypeCustom
    {
        [Description("All")]
        All = -1,
        [Description("Thêm mới")]
        Add = 1,
        [Description("Sửa")]
        Edit = 2,
        [Description("Xem")]
        View = 3,
        [Description("Xoá")]
        Delete = 4,
        [Description("Nhập")]
        Import = 5,
        [Description("Xuất")]
        Export = 6,
        [Description("Tải lên")]
        Upload = 7,
        [Description("Pushlish")]
        Pushlish = 8,
        [Description("Báo cáo")]
        Report = 9,
        [Description("In")]
        Print = 10,
        [Description("Accept")]
        Accept = 11,
        [Description("Cancel")]
        Cancel = 12,
        [Description("Record")]
        Record = 13,
        [Description("Sync")]
        Sync = 14,
        [Description("WarehouseAccept")]
        WarehouseAccept = 15,
        [Description("Save")]
        Save = 16
    }

    public enum PakageStatus
    {
        [Description("Còn hiệu lực")]
        Active = 0,
        [Description("Hết hiệu lực")]
        DeActive = 1,
        [Description("Xoá")]
        Delete = 2
    }
    public enum DeviceStatus
    {
        [Description("Trong kho")]
        NotLinked = 0,
        [Description("Thực hiện hợp đồng")]
        Linked = 1,
        [Description("Xoá")]
        Delete = 2,
        [Description("Bán")]
        Buy = 3,
        [Description("Dừng phân bổ")]
        Stop = 4
    }
    public enum DeviceActiveStatus
    {
        [Description("Chưa kích hoạt")]
        New = 7,
        [Description("Đã kích hoạt")]
        NotNew = 8,
        [Description("Bảo hành")]
        Guarantee = 9,
    }

    public enum ERR_VALIDATE_CONTRACT
    {
        [Description("Success")]
        ERR_0 = 0,
        [Description("Tên khách hàng không được để trống !")]
        ERR_1 = 1,
        [Description("Mã khách hàng không được để trống !")]
        ERR_2 = 2,
        [Description("Số hợp đồng không được để trống !")]
        ERR_3 = 3,
        [Description("Ngày ký hợp đồng không được để trống !")]
        ERR_4 = 4,
        [Description("Gói cước đăng ký không được để trống !")]
        ERR_5 = 5,
        [Description("Ngày đăng ký gói cước không được để trống !")]
        ERR_6 = 6,
        [Description("Thiết bị không được để trống !")]
        ERR_7 = 7,
        [Description("Gói cước đã hết hiệu lực, vui lòng chọn lại !")]
        ERR_8 = 8,
        [Description("Thiết bị đã được gán với hợp đồng khác, vui lòng chọn lại !")]
        ERR_9 = 9,
        [Description("Sai định dạng Ngày ký hợp đồng !")]
        ERR_10 = 10,
        [Description("Sai định dạng Ngày đăng ký gói cước !")]
        ERR_11 = 11,
        [Description("Sai định dạng Ngày hoá đơn !")]
        ERR_12 = 12,
        [Description("Thiết bị đã bị xoá, vui lòng chọn lại !")]
        ERR_13 = 13,
        [Description("Mã khách hàng đồng đã tồn tại !")]
        ERR_14 = 14,
        [Description("Thiết bị chưa gán cho hợp đồng nào !")]
        ERR_15 = 15,
        [Description("Ngày nhập kho thiết bị không hợp lệ !")]
        ERR_16 = 16,
        [Description("Quyền phân kỳ đã khóa không thể thay đổi dữ liệu của tháng trước đó !")]
        ERR_20 = 20,
        [Description("Quyền phân kỳ đã khóa không thể import dữ liệu !")]
        ERR_21 = 21,
        [Description("Ngày thanh lý hợp đồng không hợp lệ !")]
        ERR_22 = 22,
    }

    public enum ERR_VALIDATE_DEVICE
    {
        [Description("Success")]
        ERR_0 = 0,
        [Description("Tên thiết bị không được để trống !")]
        ERR_1 = 1,
        [Description("Mã thiết bị không được để trống !")]
        ERR_2 = 2,
        [Description("Serial thiết bị không được để trống !")]
        ERR_3 = 3,
        [Description("Giá thiết bị không được để trống !")]
        ERR_4 = 4,
        [Description("Thời gian phân bổ không được để trống !")]
        ERR_5 = 5,
        [Description("Ngày nhập kho không được để trống !")]
        ERR_6 = 6,
        [Description("Sai định dạng Ngày nhập kho !")]
        ERR_7 = 7,
        [Description("Quyền phân kỳ đã khóa không thể thay đổi dữ liệu của tháng trước đó !")]
        ERR_20 = 20,
        [Description("Quyền phân kỳ đã khóa không thể import dữ liệu !")]
        ERR_21 = 21,
    }

    public enum ERR_VALIDATE_PACKAGE
    {
        [Description("Success")]
        ERR_0 = 0,
        [Description("Tên gói cước không được để trống !")]
        ERR_1 = 1,
        [Description("Thời gian sử dụng không được để trống !")]
        ERR_2 = 2,
        [Description("Giá gói cước không được để trống !")]
        ERR_3 = 3,
        [Description("Thuế VAT không được để trống !")]
        ERR_4 = 4,
        [Description("Giá gói cước không được để trống !")]
        ERR_5 = 5,
        [Description("Trạng thái không được để trống !")]
        ERR_6 = 6,
        [Description("Thời gian sử dụng phải lớn hơn 0 !")]
        ERR_7 = 7,
        [Description("Giá gói cước phải lớn hơn 0 !")]
        ERR_8 = 8,
        [Description("Giá gói cước (VAT) phải lớn hơn 0 !")]
        ERR_9 = 9,
        [Description("Mã gói cước đã tồn tại !")]
        ERR_10 = 10,
        [Description("Quyền phân kỳ đã khóa không thể thay đổi dữ liệu của tháng trước đó !")]
        ERR_20 = 20,
        [Description("Quyền phân kỳ đã khóa không thể import dữ liệu !")]
        ERR_21 = 21,
    }
    public enum Group
    {
        [Description("Sale")]
        Sale = 1,
        [Description("Accountant")]
        Accountant = 2,
        [Description("Adminstrator")]
        Adminstrator = 5,
    }

    public enum GroupStatus
    {
        [Description("Enable")]
        Enable = 0,
        [Description("Disable")]
        Disable = 1,
        [Description("Limit")]
        Limit = 2,
    }

    public enum Error
    {
        [Description("Trống")]
        Empty = 1,
        [Description("Sai định dạng")]
        Wrongformat = 2,
        [Description("Trùng")]
        Exist = 3,
        [Description("Không hợp lệ")]
        NoMatch = 4,
        [Description("không hợp lệ (đã khóa quyền phân kỳ) !")]
        Rolelock = 5,
        [Description(" nhập thiếu tên !")]
        NoName = 6,
        [Description("nhập thiếu serial !")]
        NoSerial = 7,
    }

    public enum FieldMapping
    {
        [Description("ID thiết bị")]
        DeviceId = 1,
        [Description("Mã thiết bị")]
        DeviceCode = 2,
        [Description("Tên thiết bị")]
        DeviceName = 3,
        [Description("Serial")]
        Serial = 4,
        [Description("Ngày nhập kho")]
        DateInputWarehouse = 5,
        [Description("Giá thiết bị")]
        DevicePrice = 6,
        [Description("Thời gian phân bổ")]
        AllocationTime = 7,
        [Description("Ngày nhập lại kho")]
        DateReinputWarehouse = 8,
        [Description("Người tạo")]
        UserCreate = 9,
        [Description("Ngày tạo")]
        DateCreate = 10,
        [Description("Trạng thái Kích hoạt")]
        IsActive = 11,
        [Description("Ngày kích hoạt")]
        DateActive = 12,
        [Description("Trạng thái")]
        Status = 13,
        [Description("Người update cuối cùng")]
        UserLastUpdate = 14,
        [Description("Ngày update cuối cùng")]
        DateLastUpdate = 15,


        [Description("ID gói cước")]
        PackageId = 16,
        [Description("Tên gói cước")]
        PackageName = 17,
        [Description("Mã gói cước")]
        PackageNumber = 18,
        [Description("Giá gói cước VAT")]
        PriceVat = 19,
        [Description("Giá gói cước")]
        Price = 20,
        [Description("Thời gian sử dụng")]
        TimeUsed = 21,
        [Description("Thời gian Khuyến mãi")]
        PromotionTime = 22,
        [Description("Số công văn")]
        Decision = 23,


        [Description("ID Hợp đồng")]
        ContractId = 24,
        [Description("Mã khách hàng (VM)")]
        CustomerIdvm = 25,
        [Description("CCCD/CMND")]
        IdentityCard = 26,
        [Description("Tên khách hàng")]
        CustomerName = 27,
        [Description("Mã số thuế")]
        TaxCode = 28,
        [Description("Số hợp đồng")]
        ContractNumber = 29,
        [Description("Địa chỉ")]
        Address = 30,
        [Description("Ngày đăng ký gói cước")]
        RegisterDate = 31,
        [Description("Ngày ký hợp đồng")]
        SignDate = 32,
        [Description("Ngày hóa đơn")]
        BillDate = 33,
        [Description("Số hóa đơn")]
        BillNumber = 34,
        [Description("Số tiền trên hóa đơn")]
        BillPrice = 35,
        [Description("Đại lý (AM)")]
        AgentcodeAm = 36,
        [Description("Đối tác hạ tầng")]
        InfrastructurePartners = 37,
        [Description("Đơn vị phát triển")]
        DeveloperName = 38,
        [Description("Loại hình hợp tác")]
        TypeOfCooperation = 39,
    }
    #endregion
}
