
$(function () {
    window.Report = {
        init: function () {

            $('.startdate,.enddate').datepicker({
                format: "mm/yyyy",
                startView: "months",
                minViewMode: "months",
                autoclose: true,
                defaultDate: '0m'
            });
            $('#ActiveStatus,#IsActiveStatus').select2();
            $('.startdate,.enddate').datepicker('setDate', new Date());
            var date = new Date();
            var datenowstart = new Date(date.getFullYear(), date.getMonth(), 1);
            var datenowend = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            var fromDate = moment(datenowstart).format('DD/MM/YYYY');
            var toDate = moment(datenowend).format('DD/MM/YYYY');
            Report.action();
            Report.GetDeviceStatus(fromDate, toDate, 0, "");
            Report.GetDeviceAllocation(fromDate, toDate, "", -1);
            Report.GetRegisteredCustomers(fromDate, toDate, "");
            Report.GetServiceRevenueAllocation(fromDate, toDate, "");
        },
        action: function () {
            $('#btnRegistredCustormersFilter').click(function () {
                var type = 3;
                Report.GetDataFilter(type)
            });
            $('#btnDeviceCostAllocationFilter').click(function () {
                var type = 2;
                Report.GetDataFilter(type)
            });
            $('#btnServiceRevenueAllocationFilter').click(function () {
                var type = 4;
                Report.GetDataFilter(type)
            });
            $('#btnDeviceStatusFilter').click(function () {
                var type = 1;
                Report.GetDataFilter(type)
            });

            $('#btnRegistredCustormersExport').click(function () {

                var day = $('#startDateFilter').val();
                var endday = $('#startDateFilter').val();
                var enddate = common.ConvertDateReport(endday);
                var ToEndDatecustormer = new Date(enddate.getFullYear(), enddate.getMonth() + 1, 0);
                var toDate = moment(ToEndDatecustormer).format('DD/MM/YYYY');
                var date = common.ConvertDateReport(day);
                var ToEndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var fromDate = moment(date).format('DD/MM/YYYY');
                var TextSearch = $('#TextSearch').val();
                $.ajax({
                    url: '/Report/ExportRegisteredCustomers',
                    type: "GET",
                    data: {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch
                    },
                    success: function (result) {
                        if (!common.checkResponseStatus(result)) {
                            return;
                        }
                        if (result.status) {
                            common.SaveFileAs(result.fileLink, result.fileName);
                            toastr.success("Xuât excel thành công!");
                        }
                        else {
                            toastr.warning(result.mess);
                        }
                    }
                });
            });
            $('#btnDeviceCostAllocationExport').click(function () {
                var day = $('#startDateFilter').val();
                var date = common.ConvertDateReport(day);
                var ToEndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var fromDate = moment(date).format('DD/MM/YYYY');
                var endday = $('#startDateFilter').val();
                var enddate = common.ConvertDateReport(endday);
                var ToEndDatecustormer = new Date(enddate.getFullYear(), enddate.getMonth() + 1, 0);
                var toDate = moment(ToEndDatecustormer).format('DD/MM/YYYY');
                var TextSearch = $('#TextSearch').val();
                var IsActiveStatus = $('#IsActiveStatus').val();
                $.ajax({
                    url: '/Report/ExportDeviceCostAllocation',
                    type: "GET",
                    data: {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch,
                        IsActiveStatus: IsActiveStatus
                    },
                    success: function (result) {
                        if (!common.checkResponseStatus(result)) {
                            return;
                        }
                        if (result.status) {
                            common.SaveFileAs(result.fileLink, result.fileName);
                            toastr.success("Xuât excel thành công!");
                        }
                        else {
                            toastr.warning(result.mess);
                        }
                    }
                });
            });
            $('#btnServiceRevenueAllocationExport').click(function () {
                var day = $('#startDateFilter').val();
                var date = common.ConvertDateReport(day);
                var ToEndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var fromDate = moment(date).format('DD/MM/YYYY');
                var endday = $('#startDateFilter').val();
                var enddate = common.ConvertDateReport(endday);
                var ToEndDatecustormer = new Date(enddate.getFullYear(), enddate.getMonth() + 1, 0);
                var toDate = moment(ToEndDatecustormer).format('DD/MM/YYYY');
                var TextSearch = $('#TextSearch').val();
                $.ajax({
                    url: '/Report/ExportServiceRevenueAllocation',
                    type: "GET",
                    data: {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch
                    },
                    success: function (result) {
                        if (!common.checkResponseStatus(result)) {
                            return;
                        }
                        if (result.status) {
                            common.SaveFileAs(result.fileLink, result.fileName);
                            toastr.success("Xuât excel thành công!");
                        }
                        else {
                            toastr.warning(result.mess);
                        }
                    }
                });
            });
            $('#btnDeviceStatusExport').click(function () {
                var day = $('#startDateFilter').val();
                var date = common.ConvertDateReport(day);
                var ToEndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var fromDate = moment(date).format('DD/MM/YYYY');
                var endday = $('#startDateFilter').val();
                var enddate = common.ConvertDateReport(endday);
                var ToEndDatecustormer = new Date(enddate.getFullYear(), enddate.getMonth() + 1, 0);
                var toDate = moment(ToEndDatecustormer).format('DD/MM/YYYY');
                var TextSearch = $('#TextSearch').val();
                $.ajax({
                    url: '/Report/ExportDeviceStatus',
                    type: "GET",
                    data: {

                        fromDate: fromDate,
                        toDate: toDate,
                        ActiveStatus: $('#ActiveStatus').val(),
                        keySearch: TextSearch
                    },
                    success: function (result) {
                        if (!common.checkResponseStatus(result)) {
                            return;
                        }
                        if (result.status) {
                            common.SaveFileAs(result.fileLink, result.fileName);
                            toastr.success("Xuât excel thành công!");
                        }
                        else {
                            toastr.warning(result.mess);
                        }
                    }
                });
            });
        },
        GetDataFilter: function (type) {
            var day = $('#startDateFilter').val();
            var endday = $('#startDateFilter').val();
            var date = common.ConvertDateReport(day);
            var ActiveStatus = $('#ActiveStatus').val();
            var todatecustomer = '';
            if (endday != undefined) {
                var enddate = common.ConvertDateReport(endday);
                if (enddate < date) {
                    toastr.error("Ngày kết thúc không thể bé hơn bắt đàu !");
                    return;
                }
                var ToEndDatecustormer = new Date(enddate.getFullYear(), enddate.getMonth() + 1, 0);
                todatecustomer = moment(ToEndDatecustormer).format('DD/MM/YYYY');
            }
            var ToEndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            var fromDate = moment(date).format('DD/MM/YYYY');
            var toDate = moment(ToEndDate).format('DD/MM/YYYY');
            var TextSearch = $('#TextSearch').val();
            switch (type) {
                case 1:
                    Report.GetDeviceStatus(fromDate, todatecustomer, ActiveStatus, TextSearch);
                    break;
                case 2:
                    Report.GetDeviceAllocation(fromDate, todatecustomer, TextSearch, $('#IsActiveStatus').val());
                    break;
                case 3:
                    Report.GetRegisteredCustomers(fromDate, todatecustomer, TextSearch);
                    break;
                case 4:
                    Report.GetServiceRevenueAllocation(fromDate, todatecustomer, TextSearch);
                    break;
                default:
                    break;
            }
        },

        Clear: function () {
            $('#AddDeviceName').val('');
            $('#DeviceCode').val('');
            $('#SerialDevice').val('');
            $('#DevicePrice').val('');
            $('#AllocationTime').val('');
            $('#DateInputWarehouse').val('');
        },

        GetDeviceStatus: function (fromDate, toDate, ActiveStatus, TextSearch) {
            var column = "";
            if (ActiveStatus == 0) {
                column = [
                    {
                        field: "deviceName",
                        title: "Tên thiết bị",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssDeviceName',
                    },
                    {
                        field: "serial",
                        title: "Serial thiết bị",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        title: "Địa điểm thiết bị",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {

                            if (row.status == 0) {
                                return '<div>Trong kho</div>';
                            }
                            else if (row.status == 1) {
                                return '<div>Thực hiện hợp đồng</div>';
                            }
                        }
                    },
                    {
                        field: "contractNumber",
                        title: "Mã hợp đồng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "customerName",
                        title: "Tên khách hàng",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssCustomerName'
                    },
                    {
                        field: "customerIDVM",
                        title: "Mã khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "address",
                        title: "Địa chỉ khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "packageName",
                        title: "Loại gói cước",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "signDate",
                        title: "Ngày cho mượn",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.status == 0) {
                                return;
                            }
                            else {
                                return common.ConvertDate(value);
                            }
                        }
                    },
                    {
                        field: "dateUndo",
                        title: "Ngày thu hồi",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var time = moment(value).format('DD/MM/YYYY');
                            if (row.status == 0) {
                                return;
                            }
                            else {
                                return time;
                            }
                        }
                    },
                ];
            }
            else {
                column = [
                    {
                        field: "deviceName",
                        title: "Tên thiết bị",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssDeviceName'

                    },
                    {
                        field: "deviceCode",
                        title: "Mã thiết bị",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "serial",
                        title: "Serial thiết bị",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "devicePrice",
                        title: "Giá thiết bị",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {

                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "dateInputWarehouse",
                        title: "Ngày nhập kho",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return '<div>' + common.ConvertDate(value) + '</div>'
                        }
                    },
                    {
                        field: "allocationTime",
                        title: "Thời gian phân bổ (tháng)",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssallocationTime',
                    },
                    {
                        field: "isActive",
                        title: "Trạng thái",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == 7) {
                                return '<div> Chưa kích hoạt</div>';
                            }
                            else if (value == 8) {
                                ;
                                return '<div> Đã kích hoạt</div>';
                            }
                            else if (value == 9) {
                                return '<div> Bảo hành</div>';
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        title: "Tình trạng",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.status == 0) {
                                return '<div>Trong kho</div>'
                            }
                            else if (row.status == 1) {
                                return '<div>Thực hiện hợp đồng</div>'
                            }
                            else if (row.status == 2) {
                                return '<div>Đã xóa</div>'
                            }
                            else if (row.status == 3) {
                                return '<div>Đã bán</div>'
                            }
                            else {
                                return;
                            };
                        }
                    },
                ];
            }
            $('#tblDeviceStatus').bootstrapTable('destroy');
            $('#tblDeviceStatus').bootstrapTable({
                url: '/Report/GetDeviceStatus',
                method: "get",
                queryParams: function (p) {
                    return {
                        fromDate: fromDate,
                        toDate: toDate,
                        ActiveStatus: ActiveStatus,
                        keySearch: TextSearch,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,
                columns: column,
                onLoadSuccess: function (data) {
                    if (!common.checkResponseStatus(data)) {
                        return;
                    }
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        GetDeviceAllocation: function (fromDate, toDate, TextSearch, IsActiveStatus) {
            $('#tblDeviceAllocation').bootstrapTable('destroy');
            $('#tblDeviceAllocation').bootstrapTable({
                url: '/Report/GetDeviceCostAllocation',
                method: "get",
                queryParams: function (p) {
                    return {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch,
                        IsActiveStatus: IsActiveStatus,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,

                columns: [
                    {
                        field: "deviceName",
                        title: "Tên thiết bị",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssDeviceName',
                    },
                    {
                        field: "serial",
                        title: "Serial thiết bị",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "devicePrice",
                        title: "Giá thiết bị",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "customerName",
                        title: "Tên khách hàng",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssCustomerName'
                    },
                    {
                        field: "contractNumber",
                        title: "Số hợp đồng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "dateActive",
                        title: "Ngày đưa thiết bị vào sử dụng",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.isActive == 8 || row.isActive == 9) {
                                if (value == '0001-01-01T00:00:00') {
                                    return;
                                }
                                else {
                                    return common.ConvertDate(value);
                                }
                            }
                            else {
                                return 'Thiết bị chưa kích hoạt';
                            }
                        }
                    },
                    {
                        field: "allocationTime",
                        title: "Số tháng còn phải phân bổ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.isActive == 8 || row.isActive == 9) {
                                var time = row.allocationTime - common.CaculatorAlocationTime(row.dateActive);
                                return time + ' tháng';
                            }
                            else {
                                return 'Thiết bị chưa kích hoạt';
                            }
                        }
                    },
                    {
                        title: "Ngày kết thúc phân bổ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.isActive == 8 || row.isActive == 9) {
                                if (row.status == 4) { // thiết bị dừng phân bổ
                                    var time = moment(row.stopAllocation).format('DD/MM/YYYY');
                                    return time;
                                }
                                else {
                                    var timeused = row.allocationTime;
                                    var time = moment(row.dateActive).add(timeused, 'months').format('DD/MM/YYYY');
                                    return time;
                                }
                            }
                            else {
                                return 'Thiết bị chưa kích hoạt';

                            }
                        }
                    },
                    {
                        field: "soDuThangTruocChuyenSang",
                        title: "Số dư tháng trước chuyển sang",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "soPhatSinhMoiTrongKy",
                        title: "Số phát sinh mới trong kỳ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "chiPhiPhanBo",
                        title: "Chi phí phân bổ trong kỳ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "soDuConPhaiPhanBo",
                        title: "Số dư chi phí còn phải phân bổ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "statusDevice",
                        title: "Trạng thái thiết bị",
                        align: 'left',
                        valign: 'middle',
                    },
                ],
                onLoadSuccess: function (data) {
                    if (!common.checkResponseStatus(data)) {
                        return;
                    }
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        GetRegisteredCustomers: function (fromDate, toDate, TextSearch) {
            $('#tblRegisteredCustomers').bootstrapTable('destroy');
            $('#tblRegisteredCustomers').bootstrapTable({
                url: '/Report/GetRegisteredCustomers',
                method: "get",
                queryParams: function (p) {
                    return {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,
                //onLoadError: function (xhr, textStatus, errorThrown) {
                //    alert("An error occured: " + xhr.status + " " + xhr.statusText);

                //},
                columns: [
                    {
                        field: "customerName",
                        title: "Khách hàng",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssCustomerName'
                    },
                    {
                        field: "customerIDVM",
                        title: "Mã khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "contractNumber",
                        title: "Số hợp đồng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "deviceName",
                        title: "Tên thiết bị cho mượn",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "serial",
                        title: "Serial",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "packageName",
                        title: "Gói cước",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "timeUsed",
                        title: "Chu kỳ sử dụng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "signDate",
                        title: "Ngày bắt đầu dịch vụ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.ConvertDate(value);
                        }
                    },
                    {
                        title: "Ngày kết thúc dịch vụ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var timeused = row.timeUsed;
                            var time = moment(row.signDate).add(timeused, 'months').format('DD/MM/YYYY');
                            return time;
                        }
                    },
                    {
                        field: "priceVAT",
                        title: "Tổng phí dịch vụ (bao gồm VAT)",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                ],
                onLoadSuccess: function (data) {
                    if (!common.checkResponseStatus(data)) {
                        return;
                    }
                    Report.GenChart(data);
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        GenChart: function (data) {
            Highcharts.chart('charMonth', {
                chart: {
                    type: 'line'
                },
                title: {
                    text: 'Biểu đồ khách hàng đăng ký'
                },
                xAxis: {
                    categories: data.lstMonth
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Số lượng khách hàng đăng ký'
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: false
                    }
                },
                series: [{
                    name: 'Khách hàng đăng ký',
                    data: data.lstDataMonth,
                    color: '#2e6fa1'
                }]
            });
        },
        GetServiceRevenueAllocation: function (fromDate, toDate, TextSearch) {
            $('#tblServiceRevenueAllocation').bootstrapTable('destroy');
            $('#tblServiceRevenueAllocation').bootstrapTable({
                url: '/Report/GetServiceRevenueAllocation',
                method: "get",
                queryParams: function (p) {
                    return {
                        fromDate: fromDate,
                        toDate: toDate,
                        keySearch: TextSearch,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,

                columns: [
                    {
                        field: "customerName",
                        title: "Khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "customerIDVM",
                        title: "Mã khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "contractNumber",
                        title: "Số hợp đồng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "address",
                        title: "Địa chỉ khách hàng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "deviceName",
                        title: "Tên thiết bị cho mượn",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "serial",
                        title: "Serial",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "packageName",
                        title: "Gói cước",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "timeUsed",
                        title: "Chu kỳ sử dụng",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "price",
                        title: "Giá gói (chưa VAT)",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "priceVAT",
                        title: "Giá gói (Đã có VAT)",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "signDate",
                        title: "Ngày đăng ký gói",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.ConvertDate(value);
                        }
                    },
                    {
                        title: "Ngày kết thúc gói",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var timeused = row.timeUsed;
                            var time = moment(row.signDate).add(timeused, 'months').format('DD/MM/YYYY');
                            return time;
                        }
                    },
                    {
                        field: "soDuThangTruocChuyenSang",
                        title: "Số dư tháng trước chuyển sang",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "soPhatSinhMoiTrongKy",
                        title: "Số phát sinh mới trong kỳ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "doanhThuPhanBoTrongKy",
                        title: "Doanh thu phân bổ trong kỳ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "soDuConPhaiPhanBo",
                        title: "Số dư doanh thu chưa thực hiện còn phải phân bổ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                ],
                onLoadSuccess: function (data) {
                    if (!common.checkResponseStatus(data)) {
                        return;
                    }
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
    }
});
$(document).ready(function () {
    Report.init();
});


