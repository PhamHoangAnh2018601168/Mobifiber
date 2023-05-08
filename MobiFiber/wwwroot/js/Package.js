var lstImport = [];
var lstExport = [];
$(function () {
    window.package = {
        init: function () {
            $('#PackageStatus').select2({
                minimumResultsForSearch: -1
            });
            $('#filterstatus').select2();
            package.action();
            package.GetPackage();
        },
        action: function () {
            $('#PackageVAT').keyup(function () {
                var PricePackage = parseFloat($('#PricePackage').val());
                var PackageVAT = parseFloat($(this).val());
                var Price = parseFloat(PricePackage + (PricePackage * PackageVAT) / 100);
                $('#PricePackageVAT').val(Price);
            });
            $('#PricePackage').keyup(function () {
                var PricePackage = parseInt($(this).val());
                var PackageVAT = parseFloat($('#PackageVAT').val());
                var Price = parseFloat(PricePackage + (PricePackage * PackageVAT) / 100);
                $('#PricePackageVAT').val(Price);
            });

            $('#btnAddPackage').click(function () {
                var Id = $('#Packageid').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn cập nhật lại thông tin gói cước !')
                    if (r == true) {
                        var url = "/Package/Update";
                        package.AddOrUpdateNewPackage(Id, url);
                    }
                }
                else {
                    var r = confirm('Bạn có muốn tạo mới gói cước !')
                    if (r == true) {
                        var url = "/Package/Create"
                        package.AddOrUpdateNewPackage(Id, url); //0 là thêm mới
                    }

                }
            });

            $('#btnClosePackage').click(function () {
                package.Clear();
            });
            $('#btnsearch').click(function () {
                package.GetPackage();
            });
            $('#btnOpenModalPackage').click(function () {
                package.Clear();
                $('#exampleModalLabel').text("Tạo mới gói cước");
                $('#btnAddPackage').text('Tạo');
                $('#Packageid').attr('value', 0);
                $('#PackageVAT').val('0');
            });
            $('#btnupload').click(function () {
                package.FileUpload('/Package/Import');
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
                    url: '/Package/SaveData',
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
                            toastr.success('Thành công!');
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
                fdata.append(files[0].name, files[0]);
                $('#loadingpage').addClass('loader-wrapper loader');
                $('#overlay').css({ 'display': 'block' });
                $.ajax({
                    url: '/Package/ExportPackageError',
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
                    if (response.status == true) {
                        common.buttonLoader('btnupload', 'stop', 'tải lên');

                        $("#divUpload").css('display', 'none')
                        $("#divPreview").css('display', 'block')
                        lstImport = response.lstValid;
                        lstExport = response.lstInValid;
                        toastr.success(response.mess);
                        $("#tblPackageValid").bootstrapTable('destroy');
                        $("#tblPackageValid").bootstrapTable({
                            data: response.lstValid,
                            columns: [{
                                field: 'packageName',
                                title: 'Tên Gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'packageNumber',
                                title: 'Mã gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'decision',
                                title: 'Số công văn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'timeUsed',
                                title: 'Thời gian sử dụng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'promotionTime',
                                title: 'Thời gian Khuyến mại',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'price',
                                title: 'Giá Gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'priceVat',
                                title: 'Giá Gói cước VAT',
                                align: 'left',
                                valign: 'middle'
                            }
                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblPackageValid").bootstrapTable('refresh');
                                }
                            },
                        });

                        $("#tblPackageInValid").bootstrapTable('destroy');
                        $("#tblPackageInValid").bootstrapTable({
                            data: response.lstInValid,
                            columns: [{
                                field: 'packageName',
                                title: 'Tên Gói cước',
                                align: 'left',
                                valign: 'middle',
                                formatter: function (value, row, index) {
                                    return '<p>' + value + '</p>' + '<small class="text-danger">' + row.errStr + '</small>';
                                }
                            },
                            {
                                field: 'packageNumber',
                                title: 'Mã gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'decision',
                                title: 'Số công văn',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'timeUsed',
                                title: 'Thời gian sử dụng',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'promotionTime',
                                title: 'Thời gian Khuyến mại',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'price',
                                title: 'Giá Gói cước',
                                align: 'left',
                                valign: 'middle'
                            },
                            {
                                field: 'priceVat',
                                title: 'Giá Gói cước VAT',
                                align: 'left',
                                valign: 'middle'
                            },
                            ],
                            onLoadSuccess: function (data) {
                                if (data.total > 0 && data.rows.length === 0) {
                                    $("#tblPackageInValid").bootstrapTable('refresh');
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
            $('#AddPackageName').val('');
            $('#TimeUsed').val('');
            $('#PricePackage').val('');
            $('#Decision').val('');
            $('#PackageNumber').val('');
            $('#PromotionTime').val('');
            $('#PricePackageVAT').val('');
            $('#PackageVAT').val('');
            $('#PackageStatus').val(0).trigger('change');
            $('#tabsPackage').css({ 'display': 'flex' });
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

        AddOrUpdateNewPackage: function (Id, url) {
            var PackageName = $('#AddPackageName').val();
            var TimeUsed = parseInt($('#TimeUsed').val());
            var PricePackage = $('#PricePackage').val();
            var Decision = $('#Decision').val();
            var PromotionTime = parseInt($('#PromotionTime').val());
            var PackageNumber = $('#PackageNumber').val();
            var PricePackageVAT = $('#PricePackageVAT').val();
            var PackageVAT = $('#PackageVAT').val();
            var PackageStatus = $('#PackageStatus').val();
            if (PackageName == "") {
                toastr.error("Vui lòng nhập tên gói cước !");
                return;
            }
            if (PackageNumber == "") {
                toastr.error("Vui lòng nhập mã gói cước !");
                return;
            }
            if (Decision == "") {
                toastr.error("Vui lòng nhập số công văn quyết định !");
                return;
            }
            if (TimeUsed == "") {
                toastr.error("Vui lòng nhập thời gian sử dung gói cước !");
                return;
            }
            //if (PromotionTime == "") {
            //    toastr.error("Vui lòng nhập thời gian khuyến mãi gói cước !");
            //    return;
            //}
            if (PricePackage == "") {
                toastr.error("Vui lòng nhập giá gói cước ( Chưa bao gồm VAT ) !");
                return;
            }
            if (PricePackage < 0) {
                toastr.error("Giá gói cước không hợp lệ !");
                return;
            }
            if (PackageVAT < 0) {
                toastr.error("Giá trị thuế không hợp lệ !");
                return;
            }
            if (PackageVAT == "") {
                toastr.error("Vui lòng giá trị thuế VAT !");
                return;
            }
            if (Id == 0) {
                common.buttonLoader('btnAddPackage', 'start', 'Tạo');
            }
            else {
                common.buttonLoader('btnAddPackage', 'start', 'Cập nhật');
            }

            $.ajax({
                type: 'post',
                url: url,
                data: {
                    Id: Id,
                    PackageName: PackageName,
                    TimeUsed: TimeUsed,
                    PackageNumber: PackageNumber,
                    Decision: Decision,
                    PromotionTime: PromotionTime,
                    PricePackage: PricePackage,
                    PricePackageVAT: PricePackageVAT,
                    PackageStatus: PackageStatus,
                },
                success: function (rp) {
                    if (Id == 0) {
                        common.buttonLoader('btnAddPackage', 'stop', 'Tạo');
                    }
                    else {
                        common.buttonLoader('btnAddPackage', 'stop', 'Cập nhật');
                    }
                    if (!common.checkResponseStatus(rp)) {
                        return;
                    }
                    if (rp.status) {
                        toastr.success(rp.mess);
                        package.Clear();
                        $('#CreatePackage').modal('hide');
                        $("#tblPackage").bootstrapTable('refresh');
                    } else {
                        toastr.error(rp.mess);
                    }
                },

            });
        },
        GetPackage: function () {
            var status = $('#filterstatus').val();
            var search = $('#txtsearch').val();
            $('#tblPackage').bootstrapTable('destroy');
            $('#tblPackage').bootstrapTable({
                url: '/Package/GetDataPackage',
                method: "get",
                queryParams: function (p) {
                    return {
                        status: status,
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
                            var ViewDetail = '<a href="javascript:void(0)" data-id = ' + row.packageId + ' class="btnViewDetailPackage" title="Lịch sử đăng ký gói cước" style="margin-left: 3px;" data-toggle="modal" data-target="#ViewDetailPackage"><i class="fa fa-eye"></i></a>';
                            return '<a href="javascript:void(0)" data-id = ' + row.packageId + ' class="btnEditPackage" title="Cập nhật gói cước"><i class="fa fa-edit"></i></a>' + ViewDetail;
                        },
                        events: {
                            'click .btnViewDetailPackage': function (e, value, row, index) {
                                $('#tblPackageDetail').bootstrapTable('destroy')
                                $('#tblPackageDetail').bootstrapTable({
                                    url: '/Package/GetHistoriesPackageById',
                                    method: "get",
                                    queryParams: function (p) {
                                        return {
                                            Id: row.packageId,
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
                                                    time = moment(row.signDate).format('DD/MM/YYYY');
                                                }
                                                else {
                                                    time = moment(row.dateCreate).format('DD/MM/YYYY');
                                                }
                                                return time;
                                            }
                                        },
                                    ],
                                    onLoadSuccess: function (data) {

                                        $('#PackageNameDetail').text(data.packageinfo.packageName);
                                        $('#PriceDetail').text(common.formatCurrentcy(data.packageinfo.price));
                                        $('#PriceVATDetail').text(common.formatCurrentcy(data.packageinfo.priceVat));
                                        $('#TmeUsedDetail').text(data.packageinfo.timeUsed + " tháng");
                                        $(this).bootstrapTable('load', data.rows);
                                    },
                                });
                            },
                            'click .btnEditPackage': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Package/GetDataPackageById',
                                    data: {
                                        Id: row.packageId
                                    },
                                    success: function (rp) {
                                        
                                        if (rp.status) {
                                            package.Clear();
                                            $('#CreatePackage').modal('show');
                                            $('#tabsPackage').css({ 'display': 'none' });
                                            $('#exampleModalLabel').text("Cập nhật gói cước");
                                            $('#btnAddPackage').text('Cập nhật');
                                            $('#Packageid').attr('value', row.packageId);
                                            $('#AddPackageName').val(row.packageName);
                                            $('#TimeUsed').val(row.timeUsed);
                                            $('#PricePackage').val(row.price);
                                            $('#PricePackageVAT').val(row.priceVat);
                                            $('#Decision').val(row.decision);
                                            $('#PromotionTime').val(row.promotionTime);
                                            $('#PackageNumber').val(row.packageNumber);
                                            var VAT = parseFloat(((row.priceVat - row.price) * 100) / row.price).toFixed(2);
                                            $('#PackageVAT').val(VAT);
                                            $('#PackageStatus').val(row.status).trigger('change');
                                        }
                                        else {
                                            toastr.error(rp.mess);
                                        }
                                    }
                                });
                            },                            //'click .btnDeletePackage': function (e, value, row, index) {
                            //    var r = confirm('Bạn có muốn xóa gói cước này !')
                            //    if (r == true) {
                            //        $.ajax({
                            //            type: 'post',
                            //            url: '/Package/Delete',
                            //            data: {
                            //                Id: row.packageId
                            //            },
                            //            success: function (rp) {
                            //                if (rp.status) {
                            //                    toastr.success(rp.mess);
                            //                    $("#tblPackage").bootstrapTable('refresh');
                            //                } else {
                            //                    toastr.error(rp.mess);
                            //                }
                            //            }
                            //        });
                            //    }
                            //}
                        }
                    },
                    {
                        field: "packageName",
                        title: "Tên gói cước",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "packageNumber",
                        title: "Mã gói cước",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "decision",
                        title: "Công văn",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "price",
                        title: "Giá gói cước ( Chưa bao gồm VAT )",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);
                        }
                    },
                    {
                        field: "priceVat",
                        title: "Giá gói cước ( Bao gồm VAT )",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return common.formatCurrentcy(value);

                            //return '<div>' + common.formatCurrentcy(value, "", 0, ",", ",", "%v%s") + '</div>'
                        }
                    },
                    {
                        field: "timeUsed",
                        title: "Thời gian sử dụng",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return value + ' tháng'
                        }
                    },
                    {
                        field: "promotionTime",
                        title: "Thời gian khuyến mãi",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return value + ' tháng'
                        }
                    },
                    {
                        title: "Hiệu lực",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.status == 0) {
                                return 'Còn hiệu lực'
                            }
                            else if (row.status == 1) {
                                return 'Hết hiệu lực'
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
    }
});
$(document).ready(function () {
    package.init();
});