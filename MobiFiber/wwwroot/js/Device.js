
var lstImport = [];
var lstExport = [];
$(function () {
    window.Device = {
        init: function () {
            $('#filterdevicestatus,#SelectWithdraw,#EditDeviceStatus,#UpdateDeviceWithdraw').select2();
            $('.StopAllocation,#stopallocation,.dateVou ,.DateInput, .DateReinput').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });
            $('.dateDraw').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });
            Device.action();
            Device.GetDevice();
        },
        action: function () {
            $('#EditDeviceStatus').on('change', function () {
                if ($('#EditDeviceStatus').val() != 4) {
                    $('#cssStopAllocation').css('display', 'none');
                }
                else {
                    $('#cssStopAllocation').css('display', 'block');
                }
            });
            $('#btnsearch').click(function () {
                Device.GetDevice();
            })
            $('#SelectWithdraw').on('change', function () {
                if ($(this).val() == 2) {
                    $('.dateDraw').datepicker("destroy");
                    $('.dateDraw').datepicker({
                        format: 'dd/mm/yyyy',
                        todayHighlight: true,
                        todayBtn: 'linked',
                        autoclose: true,
                    });
                    $("#UpdateDeviceWithdraw").prop('disabled', false);
                    $('.baohanhdevice').css({ 'display': 'block' });
                    $('.ChangeDate').text('Ngày đổi thiết bị ');
                    $('.thuhoiDevice').text('Đổi thiết bị ');
                    $('.resondevicewithdraw').text('Lý do đổi thiết bị :')
                }
                else if ($(this).val() == 1) {
                    $('.dateDraw').datepicker("destroy");
                    $('.dateDraw').datepicker({
                        format: 'dd/mm/yyyy',
                        todayHighlight: true,
                        todayBtn: 'linked',
                        autoclose: true,
                        endDate: new Date()
                    });
                    $('.dateDraw').datepicker('setDate', moment(new Date()).format("DD/MM/YYYY"));
                    $("#UpdateDeviceWithdraw").prop('disabled', true);
                    $('.baohanhdevice').css({ 'display': 'block' });
                    $('.ChangeDate').text('Ngày thu hồi ');
                    $('.thuhoiDevice').text('Thiết bị thu hồi ');
                    $('.resondevicewithdraw').text('Lý do thu hồi :')
                }
                else {
                    $("#UpdateDeviceWithdraw").prop('disabled', false);
                    $('.baohanhdevice').css({ 'display': 'none' });
                    $('.ChangeDate').text('Ngày thu hồi ');
                    $('.resondevicewithdraw').text('Lý do thu hồi :')
                }
            });

            $('#btnAddDevice').click(function () {
                var Id = $('#Deviceid').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn cập nhật lại thông tin thiết bị !')
                    if (r == true) {
                        var url = "/Device/Update";
                        Device.AddOrUpdateNewDevice(Id, url, true);
                    }
                }
                else {
                    var url = "/Device/Create"
                    Device.AddOrUpdateNewDevice(Id, url, false); //0 là thêm mới
                }
            });
            $('#btnUpdateDevice').click(function () {
                Device.WithdrawDevice();
            });
            $('#btnCloseDevice').click(function () {
                Device.Clear();
            });
            $('#btnOpenModalDevice').click(function () {
                Device.Clear();
                $('#exampleModalLabel').text("Tạo mới thiết bị");
                $('#btnAddDevice').text('Tạo');
                $('#Deviceid').attr('value', 0);
            });
            $('#btnImport').click(function () {
                Device.import_file();
            });
            $('#btnupload').click(function () {
                Device.FileUpload('/Device/Import');
            });

            $('#btnSaveData').click(function () {
                var fdata = new FormData();
                var fileUpload = $("#fileupload").get(0);
                var files = fileUpload.files;
                common.buttonLoader('btnSaveData', 'start', 'Lưu');
                $('#loadingpage').addClass('loader-wrapper loader');
                $('#overlay').css({ 'display': 'block' });
                fdata.append(files[0].name, files[0]);
                $.ajax({
                    url: '/Device/SaveData',
                    type: "POST",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    data: fdata,
                    contentType: false,
                    processData: false,
                    success: function (rp) {
                        common.buttonLoader('btnSaveData', 'stop', 'Lưu');
                        $('#loadingpage').removeClass('loader-wrapper loader');
                        $('#overlay').css({ 'display': 'none' });
                        //$('#overlay').css({ 'display': 'none' });
                        if (!common.checkResponseStatus(rp)) {
                            return;
                        }
                        if (rp.status == true) {
                            toastr.success(rp.mess);
                            setTimeout(function () {
                                location.reload();
                            }, 1500);
                        }
                        else {
                            toastr.error('Error!');
                        }
                    },
                });
            });
            $('#btnDownloadError').click(function () {
                var fdata = new FormData();
                var fileUpload = $("#fileupload").get(0);
                var files = fileUpload.files;
                $('#loadingpage').addClass('loader-wrapper loader');
                $('#overlay').css({ 'display': 'block' });
                fdata.append(files[0].name, files[0]);
                $.ajax({
                    url: '/Device/ExportDeviceError',
                    type: "POST",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    data: fdata,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        $('#loadingpage').removeClass('loader-wrapper loader');
                        $('#overlay').css({ 'display': 'none' });
                        if (!common.checkResponseStatus(result)) {
                            return;
                        }
                        if (result.status) {
                            common.SaveFileAs(result.fileLink, result.fileName);
                            toastr.success("Xuât excel thành công!");
                        }
                        else {
                            toastr.error("Có lỗi khi export file.");
                        }
                    },
                });
            });
        },
        FileUpload: function (url) {
            var fileExtension = ['xls', 'xlsx'];
            var filename = $('#fileupload').val();
            if (filename.length == 0) {
                alert("Vui lòng chọn file.");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) == -1) {
                    alert("Vui lòng chỉ chọn các file excel.");
                    return false;
                }
            }
            var fdata = new FormData();
            var fileUpload = $("#fileupload").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
            common.buttonLoader('btnupload', 'start', 'Tải lên');
            $('#loadingpage').addClass('loader-wrapper loader');
            $('#overlay').css({ 'display': 'block' });
            $.ajax({
                type: "POST",
                url: url,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());

                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#overlay').css({ 'display': 'none' });
                    $('#loadingpage').removeClass('loader-wrapper loader');
                    if (!common.checkResponseStatus(response)) {
                        return;
                    }
                    if (response.status == true) {
                        toastr.success(response.mess);
                        common.buttonLoader('btnupload', 'stop', 'tải lên');
                        $("#divUpload").css('display', 'none')
                        $("#divPreview").css('display', 'block')
                        lstImport = response.lstValid;
                        lstExport = response.lstInValid;
                        $("#tblDeviceValid").bootstrapTable('destroy');
                        $("#tblDeviceValid").bootstrapTable({
                            data: response.lstValid,
                            columns: [{
                                field: 'deviceName',
                                title: 'Tên thiết bị',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssDeviceName'
                            },
                            {
                                field: 'deviceCode',
                                title: 'Mã thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'serial',
                                title: 'Serial thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'devicePrice',
                                title: 'Giá thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'allocationTime',
                                title: 'Thời gian phân bổ (tháng)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'dateInputWarehouse',
                                title: 'Ngày nhập kho',
                                align: 'left',
                                valign: 'middle'
                            }
                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblDeviceValid").bootstrapTable('refresh');
                                }
                            },
                        });

                        $("#tblDeviceInValid").bootstrapTable('destroy');
                        $("#tblDeviceInValid").bootstrapTable({
                            data: response.lstInValid,
                            columns: [{
                                field: 'deviceName',
                                title: 'Tên thiết bị',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssDeviceName',
                                formatter: function (value, row, index) {
                                    return '<p>' + value + '</p>' + '<small class="text-danger">' + row.errStr + '</small>';
                                }
                            },
                            {
                                field: 'deviceCode',
                                title: 'Mã thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'serial',
                                title: 'Serial thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'devicePrice',
                                title: 'Giá thiết bị',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'allocationTime',
                                title: 'Thời gian phân bổ (tháng)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'dateInputWarehouse',
                                title: 'Ngày nhập kho',
                                align: 'left',
                                valign: 'middle'
                            },

                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblDeviceInValid").bootstrapTable('refresh');
                                }
                            },
                        });
                    }
                    else {
                        toastr.error(response.mess);
                    }
                },
                error: function (e) {
                    toastr.error(response.mess);
                }
            });
        },
        Clear: function () {
            $('#AddDeviceName').val('');
            $('#DeviceCode').val('');
            $('#SerialDevice').val('');
            $('#DevicePrice').val('');
            $('#AllocationTime').val('');
            $('#DateInputWarehouse').val('');
            $('#fileupload').val('');
            $('#step_1').addClass('active');
            $('#step_2').removeClass('active');
            $('#btn-step1').addClass('active');
            $('#btn-step2').removeClass('active');
            lstImport = [];
            lstExport = [];
            $('#tabsDevice').css({ 'display': 'flex' });
            $("#divUpload").css('display', 'block')
            $("#divPreview").css('display', 'none')
            //$('#editstatusdevice').css({ 'display': 'none' });
            var check = $('#SelectWithdraw').val();
            //$('#DeviceIdWithDraw').val(0).trigger("change");
            $('#SelectWithdraw').val(0).trigger("change");
            $('#EditDeviceStatus').val(0).trigger("change");
            $('#ResonWithDraw').val('');
            $('#DateWithDraw').val('');
            $('#DateVouchers').val('');
            //$('#CreateDevice').modal({ backdrop: '', keyboard: true }) 

        },
        WithdrawDevice: function () {
            var check = $('#SelectWithdraw').val();
            var DeviceIdOld = $('#DeviceIdWithDraw').val();
            var DeviceIdNew = 0;
            if (check == 0) {
                toastr.error("Vui lòng chọn thu hồi hoặc bảo hành thiết bị !");
                return;
            }
            if (check == 2) {
                DeviceIdNew = $('#UpdateDeviceWithdraw').val();
            }
            var ResonWithDraw = $('#ResonWithDraw').val();
            var DateWithDraw = $('#DateWithDraw').val();
            var DateVouchers = $('#DateVouchers').val();
            if (DeviceIdNew == DeviceIdOld) {
                toastr.error("Vui lòng chọn thiết bị dổi !");
                return;
            }
            if (DateWithDraw == "") {
                toastr.error("Vui lòng chọn ngày thu hồi thiết bị !");
                return;
            }

            $.ajax({
                type: 'post',
                url: '/Device/UpdateDevice',
                data: {
                    OldId: DeviceIdOld,
                    NewId: DeviceIdNew,
                    reason: ResonWithDraw,
                    DateWithDraw: DateWithDraw,
                    DateVouchers: DateVouchers,
                },
                success: function (rp) {
                    if (!common.checkResponseStatus(rp)) {
                        return;
                    }
                    if (rp.status) {
                        toastr.success(rp.mess);
                        Device.Clear();
                        $('#UpdateDevice').modal('hide');
                        $("#tblDevice").bootstrapTable('refresh');
                    } else {
                        toastr.error(rp.mess);
                    }
                },

            });
        },
        AddOrUpdateNewDevice: function (Id, url, isConfirm) {
            var DeviceName = $('#AddDeviceName').val();
            var DeviceCode = $('#DeviceCode').val();
            var SerialDevice = $('#SerialDevice').val();
            var DevicePrice = $('#DevicePrice').val();
            var AllocationTime = parseInt($('#AllocationTime').val());
            var DateInputWarehouse = $('#DateInputWarehouse').val();
            var DeviceStatus = $('#EditDeviceStatus').val();
            var DateStopAllocation = $('#stopallocation').val();

            if (DeviceName == "") {
                toastr.error("Vui lòng nhập tên thiết bị !");
                return;
            }
            if (DeviceCode == "") {
                toastr.error("Vui lòng nhập mã thiết bị !");
                return;
            }
            if (SerialDevice == "") {
                toastr.error("Vui lòng nhập serial thiết bị !");
                return;
            }
            if (DevicePrice == "") {
                toastr.error("Vui lòng nhập giá thiết bị !");
                return;
            }
            if (DevicePrice <= 0) {
                toastr.error("Giá thiết bị không hợp lệ !");
                return;
            }
            if (AllocationTime == "") {
                toastr.error("Vui lòng nhập thời gian phân bổ thiết bị !");
                return;
            }
            if (AllocationTime <= 0) {
                toastr.error("Thời gian phân bổ thiết bị không hợp lệ !");
                return;
            }
            if (DateInputWarehouse == "") {
                toastr.error("Vui lòng nhập ngày thiết bị nhập kho !");
                return;
            }
            if (DeviceStatus == 4) {
                if (DateStopAllocation == "") {
                    toastr.error("Vui lòng nhập ngày dừng phân bổ thiết bị !");
                    return;
                }
            }
            if (Id == 0) {
                common.buttonLoader('btnAddDevice', 'start', 'Tạo');
            }
            else {
                common.buttonLoader('btnAddDevice', 'start', 'Cập nhật');
            }

            $.ajax({
                type: 'post',
                url: url,
                data: {
                    Id: Id,
                    DeviceName: DeviceName,
                    DeviceCode: DeviceCode,
                    SerialDevice: SerialDevice,
                    DevicePrice: DevicePrice,
                    AllocationTime: AllocationTime,
                    DateInputWarehouse: DateInputWarehouse,
                    DateStopAllocation: DateStopAllocation,
                    DeviceStatus: DeviceStatus,
                    isConfirm: isConfirm
                },
                success: function (rp) {
                    if (Id == 0) {
                        common.buttonLoader('btnAddDevice', 'stop', 'Tạo');
                    }
                    else {
                        common.buttonLoader('btnAddDevice', 'stop', 'Cập nhật');
                    }

                    if (!common.checkResponseStatus(rp)) {
                        return;
                    }

                    if (rp.status) {
                        toastr.success(rp.mess);
                        Device.Clear();
                        $('#CreateDevice').modal('hide');
                        $("#tblDevice").bootstrapTable('refresh');
                    } else {
                        if (rp.code == "003") {
                            toastr.warning("<span> Bạn có muốn tiếp tục </span>  \
                        <button type='button' id='confirmationRevertYes' class='btn btn-primary bg-white clear float-right' style='padding:5px 7px;color:black'>Yes</button>", rp.mess,
                                {
                                    closeButton: true,
                                    allowHtml: true,
                                    onShown: function (toast) {
                                        $("#confirmationRevertYes").click(function () {
                                            Device.AddOrUpdateNewDevice(Id, url, false);
                                        });
                                    }
                                });
                        } else {
                            toastr.error(rp.mess);
                        }
                    }
                },

            });
        },
        GetDevice: function (type) {
            var type = $('#filterdevicestatus').val();
            var search = $('#txtsearch').val();
            $('#tblDevice').bootstrapTable('destroy');
            $('#tblDevice').bootstrapTable({
                url: '/Device/GetDataDevice',
                method: "get",
                queryParams: function (p) {
                    return {
                        type: type,
                        search: search,
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
                        title: "Thao tác",
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var ViewDetail = "";
                            var deleteDevice = "";
                            var WithdrawDevice = "";
                            if (row.isActive == 8 || row.isActive == 9) {
                                ViewDetail = '<a href="javascript:void(0)" data-id = ' + row.deviceId + ' class="btnViewDetailDevice" title="Lịch sử thiết bị" style="margin-left: 3px;" data-toggle="modal" data-target="#ViewDetailDevice"><i class="fa fa-eye"></i></a>';
                            }
                            if (row.status == 1) {
                                WithdrawDevice = '<a href="javascript:void(0)" data-id = ' + row.deviceId + ' class="btnUpdateDevice" title="Thu hồi thiết bị" style="margin-left: 3px;"><i class="fa fa-undo"></i></a>';
                            }
                            if (row.status == 0) {
                                deleteDevice = '<a href="javascript:void(0)" data-id = ' + row.deviceId + ' class="btnDeleteDevice" title="Xóa thiết bị" style="margin-left: 3px;"><i class="fa fa-trash-o"></i></a>';
                            }
                            return '<a href="javascript:void(0)" data-id = ' + row.deviceId + ' class="btnEditDevice" title="Cập nhật thiết bị" ><i class="fa fa-edit"></i></a>'
                                + deleteDevice + WithdrawDevice + ViewDetail;
                        },
                        events: {
                            'click .btnViewDetailDevice': function (e, value, row, index) {
                                $('#tblDeviceDetail').bootstrapTable('destroy');
                                $('#tblDeviceDetail').bootstrapTable({
                                    url: '/Device/GetHistoriesDeviceById',
                                    method: "get",
                                    queryParams: function (p) {
                                        return {
                                            Id: row.deviceId,
                                            offset: p.offset,
                                            limit: p.limit
                                        };
                                    },
                                    pageSize: 5,
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
                                            title: "Tên khách hàng",
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
                                            field: "fieldChange",
                                            title: "Thay đổi",
                                            align: 'left',
                                            valign: 'middle',
                                            formatter: function (value, row, index) {
                                                var status = "";
                                                if (value == "DeviceId" || value == "DeviceIdBorrow") {
                                                    status = "Cho mượn thiết bị";
                                                }
                                                else {
                                                    status = "Trả thiết bị";
                                                }
                                                return status;
                                            }
                                        },
                                        {
                                            title: "Ngày thực hiện",
                                            align: 'left',
                                            valign: 'middle',
                                            formatter: function (value, row, index) {
                                                var time = ""
                                                if (row.action == 1) {
                                                    time = moment(row.signDate).format('DD/MM/YYYY');
                                                }
                                                else {
                                                    time = moment(row.dateCreate).format('DD/MM/YYYY');
                                                }
                                                return time;
                                            }
                                        },
                                        {
                                            title: "Ghi Chú",
                                            align: 'left',
                                            valign: 'middle',
                                            formatter: function (value, row, index) {
                                                if (row.fieldChange == "DeviceIdPay") {
                                                    return row.description;
                                                }
                                            }
                                        },
                                    ],
                                    onLoadSuccess: function (data) {
                                        $('#NamDeviceHistory').text(data.deviceinfo.deviceName);
                                        $('#DeviceNumberHistory').text(data.deviceinfo.deviceCode);
                                        $('#DateWareHouseHistory').text(moment(data.deviceinfo.dateInputWarehouse).format('DD/MM/YYYY'));
                                        $('#DeviceReAllocaHistory').text(data.deviceinfo.deviceCode);
                                        $('#SerialDeviceHistory').text(data.deviceinfo.serial);
                                        $('#DeviceAllocationHistory').text(data.deviceinfo.allocationTime + " tháng");
                                        $(this).bootstrapTable('load', data.rows);
                                    },
                                });
                            },
                            'click .btnUpdateDevice': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Device/GetInforDevice',
                                    data: {
                                        Id: row.deviceId
                                    },
                                    success: function (rp) {
                                        if (rp.status) {
                                            Device.Clear();
                                            $('#UpdateDeviceWithdraw').empty();
                                            var htmlDevice = "";
                                            for (var i = 0; i < rp.lstDevice.length; i++) {
                                                var SelectedDevice = "";
                                                if (rp.lstDevice[i].deviceId == rp.contract.deviceId) {
                                                    SelectedDevice = "selected"
                                                }
                                                htmlDevice += '<option value="' + rp.lstDevice[i].deviceId + '"' + SelectedDevice + '>' + rp.lstDevice[i].deviceName + " - Serial : " + rp.lstDevice[i].serial + '</option>'
                                            };
                                            $('#UpdateDevice').modal('show');
                                            $('#ResonWithDraw').val('');
                                            $('#UpdateDeviceWithdraw').html(htmlDevice);
                                            $('#ContractIdWithDraw').val(rp.contract.contractId);
                                            $('#DeviceIdWithDraw').val(rp.contract.deviceId);
                                            $('#CustomerNameDeviceUpdate').text(rp.contract.customerName);
                                            $('#CustomerNumberDeviceUpdate').text(rp.contract.customerIdvm);
                                            $('#ContractNumberDeviceUpdate').text(rp.contract.contractNumber);
                                            $('#SignDateDeviceUpdate').text(moment(rp.contract.signDate).format('DD/MM/YYYY'));
                                            $('.baohanhdevice').css({ 'display': 'none' });

                                        } else {

                                            toastr.error(rp.mess);
                                            //setTimeout(function () {
                                            //    $('#UpdateDevice').modal('hide');
                                            //}, 100);
                                        }
                                    }
                                });
                            },
                            'click .btnEditDevice': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Device/GetDataDeviceById',
                                    data: {
                                        Id: row.deviceId
                                    },
                                    success: function (rp) {
                                        if (rp.status) {
                                            Device.Clear();
                                            var row = rp.data;
                                            $('#DateInputWarehouse').datepicker({
                                                format: 'dd/mm/yyyy',
                                                todayHighlight: true,
                                                todayBtn: 'linked',
                                                autoclose: true,
                                            });
                                            $('#CreateDevice').modal('show');

                                            $('#tabsDevice').css({ 'display': 'none' });
                                            $('#exampleModalLabel').text("Cập nhật thông tin thiết bị");
                                            $('#btnAddDevice').text('Cập nhật');
                                            $('#Deviceid').attr('value', row.deviceId);
                                            $('#AddDeviceName').val(row.deviceName);
                                            $('#DeviceCode').val(row.deviceCode);
                                            $('#SerialDevice').val(row.serial);
                                            $('#DevicePrice').val(row.devicePrice);
                                            $('#AllocationTime').val(row.allocationTime);
                                            var dateinput = moment(row.dateInputWarehouse).format('DD/MM/YYYY');
                                            $('#DateInputWarehouse').datepicker('setDate', dateinput);
                                            $('#stopallocation').datepicker('setDate', moment(row.stopAllocation).format('DD/MM/YYYY'));
                                            $('#EditDeviceStatus').val(row.status).trigger('change');
                                        }
                                        else {
                                            toastr.error(rp.mess);
                                        }
                                    }
                                });
                            },
                            'click .btnDeleteDevice': function (e, value, row, index) {
                                var r = confirm('Bạn có muốn xóa thiết bị này !')
                                if (r == true) {
                                    $.ajax({
                                        type: 'post',
                                        url: '/Device/Delete',
                                        data: {
                                            Id: row.deviceId
                                        },
                                        success: function (rp) {
                                            if (!common.checkResponseStatus(rp)) {
                                                return;
                                            }
                                            if (rp.status) {
                                                toastr.success(rp.mess);
                                                $("#tblDevice").bootstrapTable('refresh');
                                            } else {
                                                toastr.error(rp.mess);
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    },

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
                        title: "Thời gian còn phải phân bổ (tháng)",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssallocationTime',
                        formatter: function (value, row, index) {
                            if (row.isActive == 8 || row.isActive == 9) {
                                var time = row.allocationTime - common.CaculatorAlocationTime(row.dateActive);
                                if (time >= 0) {
                                    return time;
                                }
                                return '0';
                            }
                            else {
                                return row.allocationTime;
                            }
                        },
                        events: {
                        }
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
                        field: "dateActive",
                        title: "Ngày kích hoạt",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return '<div>' + common.ConvertDate(value) + '</div>'
                            }
                        }
                    },
                    {
                        field: "dateReinputWarehouse",
                        title: "Ngày nhập lại",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return common.ConvertDate(value)
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
                            else if (row.status == 4) {
                                return '<div>Dừng phân bổ</div>'
                            }
                            else {
                                return;
                            };
                        }
                    },
                ],
                onLoadSuccess: function (data) {
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        import_file: function () {
            if (window.FormData !== undefined) {
                var fileUpload = $("#fileAttach").get(0);
                var files = fileUpload.files;
                // Create FormData object  
                var fileData = new FormData();
                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                    if (files[i].type != "application/vnd.ms-excel" && files[i].type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
                        toastr.error('Không đúng định dạng file !');
                        return;
                    }
                }
                // Adding one more key to FormData object  
                $.ajax({
                    url: '/Device/ImportFile',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (rp) {
                        if (rp.status == true) {
                            // Todo
                        }
                        else {
                            toastr.error(rp.message + ' !');
                        }
                    },

                });
            }
        },
    }
});
$(document).ready(function () {
    Device.init();
});
