var lstImport = [];
var lstExport = [];
$(function () {
    window.Contract = {
        init: function () {
            $('#LiquidationDate,#DateChangeDevice,#DateChangePackage,.ChangeDevice,.DateChange,.DateSign, .DateRegister, .DateBill').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });

            $('#Status,#filterstatus,#DeviceId, #DeveloperName ,#Agentcode,#PackageId,#filterpackage').select2();

            Contract.action();
            Contract.GetContract();
        },
        action: function () {
            $('.cssliquidationdate').css('display', 'none');

            $('#btnAddContract').click(function () {
                var Id = $('#ContractId').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn sửa lại thông tin hợp đồng ?')
                    if (r == true) {
                        var url = "/Contract/Update";
                        Contract.AddOrUpdateNewContract(Id, url, true);
                    }
                }
                else {
                    var r = confirm('Bạn có muốn tạo mới hợp đồng ?')
                    if (r == true) {
                        var url = "/Contract/Create"
                        Id = 0; //0 là thêm mới
                        Contract.AddOrUpdateNewContract(Id, url, true);
                    }
                }
            });
            $('#btnsearch').click(function () {
                Contract.GetContract();
            });
            $('#exportcontract').click(function () {
                var status = $('#filterstatus').val();
                var search = $('#txtsearch').val();
                var package = $('#filterpackage').val();
                $.ajax({
                    url: '/Contract/ExportContract',
                    type: "POST",
                    data: {
                        status: status,
                        package: package,
                        search: search
                    },
                    success: function (result) {
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
            $('#btnCloseContract').click(function () {
                var r = confirm('Bạn có muốn làm mới lại dữ liệu !')
                if (r == true) {
                    Contract.Clear();
                }
            });
            $('#Status').on('change', function () {
                var Status = $('#Status').val();
                if (Status != 2) {
                    $('.cssliquidationdate').css('display', 'none');
                }
                else {
                    $('.cssliquidationdate').css('display', 'block');
                }
            });
            $('#PackageId').on('change', function () {
                var CurrentPackage = $('#CurrentPackage').val();
                if (CurrentPackage != -1 && CurrentPackage != $('#PackageId').val()) {
                    $('.csschangedate').css('display', 'block');
                    $('.cssassigndate').css('display', 'none');
                }
                else if (CurrentPackage != -1 && CurrentPackage == $('#PackageId').val()) {
                    $('.csschangedate').css('display', 'none');
                    $('.cssassigndate').css('display', 'block');
                }
            });
            $('.cssChangeDevice').css('display', 'none');
            $('#DeviceId').on('change', function () {
                var CurrentDevice = $('#CurrentDevice').val();
                if (CurrentDevice != -1 && CurrentDevice != $('#DeviceId').val()) {
                    $('.cssChangeDevice').css('display', 'block');
                }
                else if (CurrentDevice != -1 && CurrentDevice == $('#DeviceId').val()) {
                    $('.cssChangeDevice').css('display', 'none');
                }
            });
            $('#btnCloseContract').click(function () {
                var r = confirm('Bạn có muốn làm mới lại dữ liệu !')
                if (r == true) {
                    Contract.Clear();
                }
            });
            $('#btnforwardcccd').click(function () {
                var cccd = $('#CustomerID').val();
                if (cccd.length > 12) {
                    toastr.error("Sai định dạng !");
                    return;
                }
                else {
                    $('#IdentityCard').val(cccd);
                }
            });
            $('#btnupload').click(function () {
                Contract.FileUpload('/Contract/Import');
            });
            $('#btnSaveData').click(function () {
                var fdata = new FormData();
                var fileUpload = $("#fileupload").get(0);
                var files = fileUpload.files;
                fdata.append(files[0].name, files[0]);
                common.buttonLoader('btnSaveData', 'start', 'Lưu');
                $('#loadingpage').addClass('loader-wrapper loader');
                $('#overlay').css({ 'display': 'block' });
                $.ajax({
                    url: '/Contract/SaveData',
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
                            toastr.error(rp.mess);
                        }
                    },
                });
            });
            $('#btnDownloadError').click(function () {
                var fdata = new FormData();
                var fileUpload = $("#fileupload").get(0);
                var files = fileUpload.files;
                fdata.append(files[0].name, files[0]);
                $('#loadingpage').addClass('loader-wrapper loader');
                $('#overlay').css({ 'display': 'block' });
                $.ajax({
                    url: '/Contract/ExportContractError',
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
                alert("Please select a file.");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) == -1) {
                    alert("Please select only excel files.");
                    return false;
                }
            }
            var fdata = new FormData();
            var fileUpload = $("#fileupload").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
            common.buttonLoader('btnupload', 'start', 'tải lên');
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
                    $('#loadingpage').removeClass('loader-wrapper loader');
                    $('#overlay').css({ 'display': 'none' });
                    if (!common.checkResponseStatus(response)) {
                        return;
                    }
                    common.buttonLoader('btnupload', 'stop', 'tải lên');
                    $('#loadingpage').removeClass('loader-wrapper loader');
                    if (response.status == true) {
                        $("#divUpload").css('display', 'none')
                        $("#divPreview").css('display', 'block')
                        lstImport = response.lstValid;
                        lstExport = response.lstInValid;
                        if (response.checkdatedevice == 1 && response.CountDone) {
                            toastr.warning("<span> Cảnh báo ngày nhập kho thiết bị đang không hợp lệ !</span> "
                                , response.mess,
                                {
                                    closeButton: true,
                                    allowHtml: true,
                                    onShown: function (toast) {
                                        $("#confirmationRevertYes").click(function () {
                                        });
                                    }
                                });
                        }
                        else {
                            toastr.success(response.mess);
                        }
                        $("#tblCustomerValid").bootstrapTable('destroy');
                        $("#tblCustomerValid").bootstrapTable({
                            data: response.lstValid,
                            columns: [{
                                field: 'customerName',
                                title: 'Tên khách hàng',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssCustomerName'
                            },
                            {
                                field: 'customerIdvm',
                                title: 'Mã khách hàng (VM)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'identityCard',
                                title: 'CCCD/CMND',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'address',
                                title: 'Địa chỉ',
                                align: 'left',
                                valign: 'middle',
                                formatter: function (value, row, index) {
                                    if (value != null) {
                                        if (value.length > 30) {
                                            return '<span title="' + value + '">' + value.substring(0, 29) + '...</span>';
                                        }
                                        else {
                                            return value;
                                        }
                                    }
                                    else {
                                        return "";
                                    }
                                }
                            },
                            {
                                field: 'phone',
                                title: 'Số điện thoại',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'contractNumber',
                                title: 'Số hợp đồng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'signDate',
                                title: 'Ngày ký hợp đồng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'packageName',
                                title: 'Gói cước đăng ký',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'registerDate',
                                title: 'Ngày đăng ký gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'deviceName',
                                title: 'Thiết bị',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssDeviceName',
                            },
                            {
                                field: 'serial',
                                title: 'Serial thiết bị',
                                align: 'left',
                                valign: 'middle',
                            },
                            {
                                field: 'agentName',
                                title: 'Tên đại lý (AM)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billNumber',
                                title: 'Số hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billDate',
                                title: 'Ngày hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billPrice',
                                title: 'Số tiền trên hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'developerName',
                                title: 'Tên đơn vị phát triển',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'infrastructurePartners',
                                title: 'Đối tác hạ tầng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'typeOfCooperation',
                                title: 'Loại hình hợp tác',
                                align: 'left',
                                valign: 'middle'
                            }
                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblCustomerValid").bootstrapTable('refresh');
                                }
                            },
                        });

                        $("#tblCustomerInValid").bootstrapTable('destroy');
                        $("#tblCustomerInValid").bootstrapTable({
                            data: response.lstInValid,
                            columns: [{
                                field: 'customerName',
                                title: 'Tên khách hàng',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssCustomerName',
                                formatter: function (value, row, index) {
                                    return '<p>' + value + '</p>' + '<small class="text-danger">' + row.errStr + '</small>';
                                }

                            },
                            {
                                field: 'customerIdvm',
                                title: 'Mã khách hàng (VM)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'identityCard',
                                title: 'CCCD/CMND',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'address',
                                title: 'Địa chỉ',
                                align: 'left',
                                valign: 'middle',
                                formatter: function (value, row, index) {
                                    if (value != null) {
                                        if (value.length > 30) {
                                            return '<span title="' + value + '">' + value.substring(0, 29) + '...</span>';
                                        }
                                        else {
                                            return value;
                                        }
                                    }
                                    else {
                                        return "";
                                    }
                                }
                            },
                            {
                                field: 'phone',
                                title: 'Số điện thoại',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'contractNumber',
                                title: 'Số hợp đồng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'signDate',
                                title: 'Ngày ký hợp đồng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'packageName',
                                title: 'Gói cước đăng ký',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'registerDate',
                                title: 'Ngày đăng ký gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'deviceName',
                                title: 'Thiết bị',
                                align: 'left',
                                valign: 'middle',
                                class: 'cssDeviceName'
                            },
                            {
                                field: 'serial',
                                title: 'Serial thiết bị',
                                align: 'left',
                                valign: 'middle',
                            },
                            {
                                field: 'agentName',
                                title: 'Tên đại lý (AM)',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billNumber',
                                title: 'Số hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billDate',
                                title: 'Ngày hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'billPrice',
                                title: 'Số tiền trên hóa đơn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'developerName',
                                title: 'Tên đơn vị phát triển',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'infrastructurePartners',
                                title: 'Đối tác hạ tầng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'typeOfCooperation',
                                title: 'Loại hình hợp tác',
                                align: 'left',
                                valign: 'middle'
                            },

                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblCustomerInValid").bootstrapTable('refresh');
                                }
                            },
                        });
                    }
                    else {
                        toastr.error(response.mess);
                        //location.reload();
                    }
                },
                error: function (e) {
                    $('#divPrint').html(e.responseText);
                }
            });
        },
        Clear: function () {
            $('#CustomerName').val('');
            $('#CustomerID').val('');
            $('#AdressCustomer').val('');
            $('#IdentityCard').val('');
            $('#Phone').val('');
            $('#ContractNumber').val('');
            $('#SignDate').val('');
            $('#PackageId').val(0).trigger('change');
            $('#DeviceId').val(0).trigger('change');
            $('#RegisterDate').val('');
            $('#Agentcode').val('');
            $('#BillNumber').val('');
            $('#BillDate').val('');
            $('#SignDate').val('');
            $('#BillPrice').val('');
            $('#DeveloperName').val('');
            $('#InfrastructurePartners').val('');
            $('#TypeOfCooperation').val('');

            $('#fileupload').val('');
            $('#step_1').addClass('active');
            $('#step_2').removeClass('active');
            $('#btn-step1').addClass('active');
            $('#btn-step2').removeClass('active');
            lstImport = [];
            lstExport = [];
            $("#divUpload").css('display', 'block')
            $("#divPreview").css('display', 'none')
        },
        AddOrUpdateNewContract: function (Id, url, isConfirm) {
            var obj = new Object();
            obj.ContractId = $('#ContractId').val();
            obj.CustomerName = $('#CustomerName').val();
            obj.CustomerIDVM = $('#CustomerID').val();
            obj.Address = $('#AdressCustomer').val();
            obj.Phone = $('#Phone').val();
            obj.ContractNumber = $('#ContractNumber').val();
            obj.SignDate = moment(common.StringToDate($('#SignDate').val())).format('MM/DD/YYYY');
            obj.PackageId = $('#PackageId').val();
            obj.DeviceId = $('#DeviceId').val();
            obj.RegisterDate = moment(common.StringToDate($('#RegisterDate').val())).format('MM/DD/YYYY');
            obj.AgentcodeAm = $('#Agentcode').val();
            obj.BillNumber = $('#BillNumber').val();
            obj.BillDate = moment(common.StringToDate($('#BillDate').val())).format('MM/DD/YYYY');
            obj.BillPrice = $('#BillPrice').val();
            obj.DeveloperName = $('#DeveloperName').val();
            obj.InfrastructurePartners = $('#InfrastructurePartners').val();
            obj.TypeOfCooperation = $('#TypeOfCooperation').val();
            obj.IdentityCard = $('#IdentityCard').val();
            obj.CurrentPackage = $('#CurrentPackage').val();
            obj.Status = $('#Status').val();
            obj.CurrentDevice = $('#CurrentDevice').val();
            obj.DateChangePackage = null;
            obj.DateChangeDevice = null;
            obj.LiquidationDate = null;
            if (obj.Status == 2) {
                obj.LiquidationDate = $('#LiquidationDate').val();
                if (obj.LiquidationDate == null || obj.LiquidationDate == "") {
                    toastr.error("Vui lòng nhập ngày thanh lý hợp đồng !");
                    return;
                }
            }
            if (obj.CurrentPackage != -1 && obj.CurrentPackage != $('#PackageId').val() && obj.CurrentPackage != undefined) {
                obj.DateChangePackage = $('#DateChangePackage').val();
                if (DateChangePackage == null || DateChangePackage == "") {
                    toastr.error("Vui lòng nhập ngày chuyển đổi gói cước !");
                    return;
                }
            }
            if (obj.CurrentDevice != -1 && obj.CurrentDevice != $('#DeviceId').val() && obj.CurrentDevice != undefined) {
                obj.DateChangeDevice = $('#DateChangeDevice').val();
                if (obj.DateChangeDevice == null || obj.DateChangeDevice == "") {
                    toastr.error("Vui lòng nhập ngày đổi thiết bị !");
                    return;
                }
            }

            if (obj.CustomerName.trim() == "") {
                toastr.error("Vui lòng nhập tên khách hàng !");
                return;
            }
            if (obj.CustomerIDVM.trim() == "") {
                toastr.error("Vui lòng nhập mã khách hàng !");
                return;
            }
            if (obj.IdentityCard.trim() != "") {
                if (obj.IdentityCard.length != 9 && obj.IdentityCard.length != 12) {
                    toastr.error("Sai định dạng CMND hoặc CCCD !");
                    return;
                }
            }

            if (obj.ContractNumber.trim() == "") {
                toastr.error("Vui lòng nhập số hợp đồng !");
                return;
            }
            if (obj.SignDate.trim() == "") {
                toastr.error("Vui lòng nhập ngày ký hợp đồng !");
                return;
            }
            if (obj.PackageId.trim() == "0") {
                toastr.error("Vui lòng chọn gói cước !");
                return;
            }
            if (obj.RegisterDate.trim() == "") {
                toastr.error("Vui lòng nhập ngày đăng ký gói cước !");
                return;
            }
            if (obj.ContractId == 0) {
                common.buttonLoader('btnAddContract', 'start', 'Tạo');
            }
            else {
                common.buttonLoader('btnAddContract', 'start', 'Cập nhật');
            }
            $.ajax({
                type: 'post',
                url: url,
                data: {
                    obj: obj
                },
                success: function (rp) {
                    if (obj.ContractId == 0) {
                        common.buttonLoader('btnAddContract', 'stop', 'Tạo');
                    }
                    else {
                        common.buttonLoader('btnAddContract', 'stop', 'Cập nhật');
                    }
                    if (!common.checkResponseStatus(rp)) {
                        return;
                    }
                    if (rp.status) {
                        toastr.success(rp.mess);
                        setTimeout(function () {
                            location.reload();
                        }, 1500);
                    } else {
                        if (rp.code == "003") {
                            toastr.warning("<span> Bạn có muốn tiếp tục </span>  \
                        <button type='button' id='confirmationRevertYes' class='btn btn-primary bg-white clear float-right' style='padding:5px 7px;color:black'>Yes</button>", rp.mess,
                                {
                                    closeButton: true,
                                    allowHtml: true,
                                    onShown: function (toast) {
                                        $("#confirmationRevertYes").click(function () {
                                            Contract.AddOrUpdateNewContract(Id, url, false);
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
        GetContract: function () {
            var status = $('#filterstatus').val();
            var search = $('#txtsearch').val();
            var package = $('#filterpackage').val();
            $('#tblContract').bootstrapTable('destroy')
            $('#tblContract').bootstrapTable({
                url: '/Contract/GetDataContract',
                method: "get",
                queryParams: function (p) {
                    return {
                        status: status,
                        package: package,
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
                            var ViewDetail = '<a href="javascript:void(0)" data-id = ' + row.contractId + ' class="btnViewDetailContract" title="Lịch sử hợp đồng" style="margin-left: 3px;" data-toggle="modal" data-target="#ViewDetailContract"><i class="fa fa-eye"></i></a>';
                            return '<a href="javascript:void(0)" data-id = ' + row.contractId + ' class="btnEditContract" title="Cập nhật hợp đồng"  ><i class="fa fa-edit"></i></a>' + ViewDetail;
                        },
                        events: {
                            'click .btnViewDetailContract': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Contract/GetHistoriesContractById',
                                    data: {
                                        Id: row.contractId
                                    },
                                    success: function (rp) {
                                        if (rp.status) {

                                            $('#tabledevicecontract').bootstrapTable('destroy');
                                            $('#tablepackagecontract').bootstrapTable('destroy');

                                            $('#CustomerNameDetail').text(rp.lstContract.customerName);
                                            $('#CustomerNumberDetail').text(rp.lstContract.customerIdvm);
                                            $('#ContractNumberDetail').text(rp.lstContract.contractNumber);
                                            $('#RegisterContractDetail').text(moment(rp.lstContract.signDate).format('DD/MM/YYYY'));
                                            // binding data 
                                            // todo
                                            var columns_1 = [
                                                {
                                                    field: 'deviceName',
                                                    title: 'Tên thiết bị',
                                                    align: 'left',
                                                    valign: 'middle'
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
                                                    field: 'fieldChange',
                                                    title: 'Trạng thái',
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
                                                    title: 'Ngày thay đổi',
                                                    align: 'left',
                                                    valign: 'middle',
                                                    formatter: function (value, row, index) {
                                                        var time = "";
                                                        if (row.action == 1) {
                                                            time = moment(rp.lstContract.signDate).format('DD/MM/YYYY');
                                                        }
                                                        else {
                                                            time = moment(row.dateCreate).format('DD/MM/YYYY');
                                                        }
                                                        return time;
                                                    }
                                                }
                                            ];
                                            var columns_2 = [
                                                {
                                                    field: 'packageName',
                                                    title: 'Tên gói cước',
                                                    align: 'left',
                                                    valign: 'middle'
                                                },
                                                {
                                                    field: 'priceVAT',
                                                    title: 'Giá gói cước',
                                                    align: 'left',
                                                    valign: 'middle',
                                                    formatter: function (value, row, index) {
                                                        return common.formatCurrentcy(value);
                                                    }
                                                },
                                                {
                                                    field: 'timeUsed',
                                                    title: 'Thời gian sử dụng',
                                                    align: 'left',
                                                    valign: 'middle'
                                                },
                                                {
                                                    field: "fieldChange",
                                                    title: "Trạng thái",
                                                    align: 'left',
                                                    valign: 'middle',
                                                    formatter: function (value, row, index) {
                                                        var status = "";
                                                        if (value == "PackageId" || value == "PackageIdBorrow") {
                                                            status = "Đăng ký gói cước";
                                                        }
                                                        else {
                                                            status = "Hủy gói cước";
                                                        }
                                                        return status;
                                                    }
                                                },
                                                {
                                                    title: "Ngày Thay đổi",
                                                    align: 'left',
                                                    valign: 'middle',
                                                    formatter: function (value, row, index) {
                                                        var time = ""
                                                        if (row.action == 1) {
                                                            time = moment(rp.lstContract.signDate).format('DD/MM/YYYY');
                                                        }
                                                        else {
                                                            time = moment(row.dateCreate).format('DD/MM/YYYY');
                                                        }
                                                        return time;
                                                    }
                                                },
                                            ];

                                            Contract.buildataTable('tabledevicecontract', columns_1, rp.lstdatadevice);
                                            Contract.buildataTable('tablepackagecontract', columns_2, rp.lstdatapackage);
                                        } else {
                                            toastr.error(rp.mess);
                                        }
                                    }
                                });
                            },
                            'click .btnEditContract': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Contract/GetDataContractById',
                                    data: {
                                        Id: row.contractId
                                    },
                                    success: function (rp) {
                                        if (rp.status) {
                                            $('#CreateContract').modal('show');
                                            $('#PackageId').empty();
                                            $('#DeviceId').empty();
                                            var htmlDevice = "";
                                            for (var i = 0; i < rp.lstDevice.length; i++) {
                                                var SelectedDevice = "";
                                                if (rp.lstDevice[i].deviceId == rp.contract.deviceId) {
                                                    SelectedDevice = "selected"
                                                }
                                                var devicename = "";
                                                var DevID = 0;
                                                if (rp.lstDevice[i].deviceName != null) {
                                                    devicename = rp.lstDevice[i].deviceName + " - Serial : " + rp.lstDevice[i].serial;
                                                    DevID = rp.lstDevice[i].deviceId;
                                                }
                                                else {
                                                    devicename = "Không mượn thiết bị";
                                                }
                                                htmlDevice += '<option value="' + DevID + '"' + SelectedDevice + '>' + devicename + '</option>'
                                            };
                                            $('#DeviceId').html(htmlDevice);

                                            var htmlPackage = "";

                                            for (var i = 0; i < rp.lstPackage.length; i++) {
                                                var SelectedPackage = "";
                                                if (rp.lstPackage[i].packageId == rp.contract.packageId) {
                                                    SelectedPackage = "selected"
                                                }
                                                htmlPackage += '<option value="' + rp.lstPackage[i].packageId + '"' + SelectedPackage + '>' + rp.lstPackage[i].packageName + " - " + rp.lstPackage[i].priceVat + '</option>'
                                            };
                                            $('#PackageId').html(htmlPackage);
                                            $('#SignDate,#BillDate,#RegisterDate').datepicker({
                                                format: 'dd/mm/yyyy',
                                                todayHighlight: true,
                                                todayBtn: 'linked',
                                                autoclose: true,
                                            });
                                            $('.DateSign').datepicker('setDate', moment(rp.contract.signDate).format('DD/MM/YYYY'));
                                            $('.DateRegister').datepicker('setDate', moment(rp.contract.registerDate).format('DD/MM/YYYY'));
                                            $('#ContractId').val(rp.contract.contractId);
                                            $('#CustomerName').val(rp.contract.customerName);
                                            $('#CustomerID').val(rp.contract.customerIdvm);
                                            $('#AdressCustomer').val(rp.contract.address);
                                            $('#Phone').val(rp.contract.phone);
                                            $('#ContractNumber').val(rp.contract.contractNumber);
                                            $('#SignDate').datepicker('setDate', moment(rp.contract.signDate).format('DD/MM/YYYY'));
                                            $('#RegisterDate').datepicker('setDate', moment(rp.contract.registerDate).format('DD/MM/YYYY'));
                                            $('#Agentcode').val(rp.contract.agentcodeAm).trigger('change');
                                            $('#BillNumber').val(rp.contract.billNumber);
                                            $('#BillDate').datepicker('setDate', moment(rp.contract.billDate).format('DD/MM/YYYY'));
                                            $('#BillPrice').val(rp.contract.billPrice);
                                            $('#DeveloperName').val(rp.contract.developerName).change();
                                            $('#InfrastructurePartners').val(rp.contract.infrastructurePartners);
                                            $('#TypeOfCooperation').val(rp.contract.typeOfCooperation);
                                            $('#IdentityCard').val(rp.contract.identityCard);
                                            $('#CurrentPackage').val(rp.contract.packageId);
                                            $('#CurrentDevice').val(rp.contract.deviceId);
                                            $('#Status').val(rp.contract.status).trigger('change');
                                            $('.csschangedate').css('display', 'none');
                                            $('.cssChangeDevice').css('display', 'none');
                                            $('.cssassigndate').css('display', 'block');
                                            if (rp.contract.status == 2) {
                                                $('.cssliquidationdate').css('display', 'block');
                                                $('#LiquidationDate').datepicker('setDate', moment(rp.contract.liquidationDate).format('DD/MM/YYYY'));
                                            }
                                            else {
                                                $('.cssliquidationdate').css('display', 'none');
                                                $('#LiquidationDate').datepicker('setDate', moment(new Date()).format('DD/MM/YYYY'));
                                            }
                                        } else {
                                            toastr.error(rp.mess);

                                        }
                                    }
                                });
                            },

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
                        field: "customerIdvm",
                        title: "Mã khách hàng",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "identityCard",
                        title: "CCCD/CMND",
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
                        field: "phone",
                        title: "Số điện thoại",
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
                        field: "signDate",
                        title: "Ngày ký hợp đồng",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.ConvertDate(value)
                        }
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
                        field: "priceVat",
                        title: "Giá gói cước",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "signDate",
                        title: "Ngày bắt đầu dịch vụ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.ConvertDate(value)
                        }
                    },
                    {
                        field: "endService",
                        title: "Ngày kết thúc dịch vụ",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.ConvertDate(value)
                        }
                    },
                    {
                        field: "dayEndService",
                        title: "Số ngày sử dụng còn lại",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value > 0) {
                                return value;
                            }
                            else {
                                return 'Đã hết hạn';
                            }
                        }
                    },
                    {
                        field: "deviceName",
                        title: "Tên thiết bị",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssDeviceName',
                        formatter: function (value, row, index) {
                            if (value != 0) {
                                return value;
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "devicePrice",
                        title: "Giá thiết bị",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return common.formatCurrentcy(value);
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "serial",
                        title: "Serial thiết bị",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return value;
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "billNumber",
                        title: "Số hóa đơn",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "billDate",
                        title: "Ngày hóa đơn",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return common.ConvertDate(value)
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "billPrice",
                        title: "Số tiền trên hóa đơn",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != 0) {
                                return common.formatCurrentcy(value);
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "agentcodeAm",
                        title: "AM/Đại lý",
                        align: 'left',
                        valign: 'middle',
                        class: 'cssAgentsCode',
                        formatter: function (value, row, index) {
                            if (value != 0) {
                                return value;
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "developerName",
                        title: "Tên đơn vị phát triển",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != 0) {
                                return value;
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "infrastructurePartners",
                        title: "Đối tác hạ tầng",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "typeOfCooperation",
                        title: "Loại hình hợp tác",
                        align: 'left',
                        valign: 'middle',
                    },
                    {
                        field: "liquidationDate",
                        title: "Ngày thanh lý hợp đồng",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value != null) {
                                return common.ConvertDate(value)
                            }
                            else {
                                return;
                            }
                        }
                    },
                    {
                        field: "statusName",
                        title: "Trạng thái",
                        align: 'left',
                        valign: 'middle',
                    },

                ],
                onLoadSuccess: function (data) {
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        buildataTable: function (elment_table, columns, data) {
            $("#" + elment_table).bootstrapTable({
                columns: columns,
                pageSize: 5,
                pagination: true,
                data: data,
            });
        }
    }
});
$(document).ready(function () {
    Contract.init();
});

