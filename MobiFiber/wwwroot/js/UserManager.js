$(function () {
    window.UserManager = {
        init: function () {
            $('.DateInput, .DateReinput, .date').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });
            UserManager.action();
            UserManager.GetUserManager();
        },
        action: function () {
            $('#showpassword').click(function () {
                if ($('#Password').attr('type') == 'password') {
                    $('#Password').attr('type', 'text');
                    $('#showpassword').removeClass('fa-eye');
                    $('#showpassword').addClass('fa-eye-slash');
                }
                else {
                    $('#Password').attr('type', 'password');
                    $('#showpassword').removeClass('fa-eye-slash');
                    $('#showpassword').addClass('fa-eye');
                }
            });
            $('#btnOpenModalUser').click(function () {
                $('#CreateUser').modal();
                UserManager.ClearData();
            });
            $('#RoleUserView, #Role, #RoleCreate, #SelectUserName, #SelectModule, #SelectAction').select2();
            $('#btnresetpassword').click(function () {
                var obj = new Object;
                obj.Id = $('#IdResetPassword').val();
                obj.UserName = $('#UserNameResetPassword').val();
                obj.NewPassword = $('#NewPassword').val();
                obj.ConfirmPassword = $('#ConfirmPassword').val();
                $.ajax({
                    type: 'post',
                    url: '/Setting/AdminChangePass',
                    data: {
                        PasswordViewModel: obj
                    },
                    success: function (rp) {
                        if (!common.checkResponseStatus(rp)) {
                            return;
                        }
                        if (rp.status) {
                            toastr.success(rp.mess);
                            $('#ChangePassword').modal("hide");
                        }
                        else {
                            toastr.error(rp.mess);
                        }
                    }
                });
            });
            $('#btnAddUser').click(function () {
                var email_regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
                if ($('#UserName').val() == "") {
                    toastr.error("Tên đăng nhập không được để trống");
                    return;
                }
                if ($('#Password').val() == "") {
                    toastr.error("Mật khẩu không được để trống");
                    return;
                }
                if ($('#Email').val() == "") {
                    toastr.error("Email không được để trống");
                    return;
                }
                if (!email_regex.test($('#Email').val())) {
                    toastr.error("Email không đúng định dạng");
                    e.preventDefault();
                    return;
                }
                if ($('#RoleCreate').val() == 0) {
                    toastr.error("Cần chọn quyền cho người dùng");
                    return;
                }
                var obj = new Object;
                obj.UserName = $('#UserName').val();
                obj.Password = $('#Password').val();
                obj.Email = $('#Email').val();
                obj.Role = $('#RoleCreate').val();
                $.ajax({
                    type: 'post',
                    url: '/Account/CreateProfile',
                    data: {
                        Account: obj
                    },
                    success: function (rp) {
                        if (!common.checkResponseStatus(rp)) {
                            return;
                        }
                        if (rp.status) {
                            toastr.success(rp.mess);
                            $('#CreateUser').modal('hide');
                            $('#tblUserManager').bootstrapTable('refresh');
                        }
                        else {
                            toastr.error(rp.mess);
                        }
                    }
                });
            });
            $('#btnSearchUserHis').click(function () {
                UserManager.GetUserHistory();
            });
        },
        ClearData: function () {
            $('#UserName').val('');
            $('#Password').val('');
            $('#Email').val('');
            $('#RoleCreate').val(0).trigger('change');
        },

        GetUserManager: function () {
            $('#tblUserManager').bootstrapTable({
                url: '/Setting/GetDataUserManager',
                method: "get",
                queryParams: function (p) {
                    return {
                        search: p.search,
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
                search: true,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,
                columns: [
                    {
                        field: "userName",
                        title: "Tên tài khoản",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        title: "Họ và tên",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var ho = row.lastName != null ? row.lastName : "";
                            var ten = row.firstName != null ? row.firstName : "";
                            return ho + " " + ten ;
                        }
                    },
                   
                    {
                        field: "active",
                        title: "Trạng thái tài khoản",
                        align: 'left',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var title = "";
                            if (value) {
                                var active = 'Đang kích hoạt <i class="fa fa-check-circle-o text-success"></i>';
                                title = " Ấn để khóa tài khoản ";
                            }
                            else {
                                var active = 'Đã khóa <i class="fa fa-ban text-danger"></i>';
                                title = " Ấn để mở khóa tài khoản ";
                            }
                            return '<a class="btnLockUsers" title="' + title + '">' + active + '</i></a>';
                        },
                        events: {
                            'click .btnLockUsers': function (e, value, row, index) {
                                if (row.active) {
                                    var r = confirm('Bạn muốn khóa tài khoản này !')
                                }
                                else {
                                    var r = confirm('Bạn muốn kích hoạt tài khoản này !')
                                }
                                if (r == true) {
                                    $.ajax({
                                        type: 'post',
                                        url: '/Setting/ActiveUser',
                                        data: {
                                            active: row.active,
                                            id: row.id
                                        },
                                        success: function (rp) {
                                            if (rp.status) {
                                                toastr.success(rp.mess);
                                                $('#tblUserManager').bootstrapTable('refresh');
                                            }
                                            else {
                                                toastr.error(rp.mess);
                                            }
                                        }
                                    });
                                }
                            },
                        }
                    },
                    {
                        title: "Thao tác",
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var deleteDevice = "";
                            if (row.status == 0) {
                                deleteDevice = '<a href="javascript:void(0)" data-id = ' + row.DeviceId + ' class="btnDeleteDevice" title="Xóa thiết bị" style="margin-left: 10px;"><i class="fa fa-trash-o"></i></a>';
                            }
                            return '<a href="javascript:void(0)" data-id = ' + row.id + ' class="btnEditUser mr-1" title="Sửa" data-toggle="modal" data-target="#EditUser1"><i class="fa fa-edit"></i></a> | <a href="javascript:void(0)" data-id = ' + row.id + ' class="btnResetPassword ml-1" title="Reset mật khẩu" data-toggle="modal" data-target="#EditUser1"><i class="fa fa-key"></i></a>'
                                + deleteDevice;
                        },
                        events: {
                            'click .btnEditUser': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Setting/GetDataUserById',
                                    data: {
                                        id: row.id
                                    },
                                    success: function (rp) {
                                        $('#EditUser').modal();
                                        $('#Id').val(rp.rp.id);
                                        $('#EditUserName').val(rp.rp.userName);
                                        $('#EditEmail').val(rp.rp.email);
                                        $('#EditLastName').val(rp.rp.lastName);
                                        $('#EditFirstName').val(rp.rp.firstName);
                                        $('#Role').val(rp.rp.role).trigger('change');
                                    }
                                });
                            },
                            'click .btnResetPassword': function (e, value, row, index) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Setting/GetDataUserById',
                                    data: {
                                        id: row.id
                                    },
                                    success: function (rp) {
                                        $('#ChangePassword').modal();
                                        $('#IdResetPassword').val(row.id);
                                        $('#UserNameResetPassword').val(row.userName);
                                        $('#NewPassword').val("");
                                        $('#ConfirmPassword').val("");
                                    }
                                });
                            }
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
        GetUserHistory: function () {
            $('#tblUserHistory').bootstrapTable('destroy');
            $('#tblUserHistory').bootstrapTable({
                url: '/Setting/GetHistoryUser',
                method: "get",
                queryParams: function (p) {
                    return {
                        Uid: $('#SelectUserName').val(),
                        fromDate: $('#FromDateHis').val(),
                        toDate: $('#ToDateHis').val(),
                        moduleId: $('#SelectModule').val(),
                        actionId: $('#SelectAction').val(),
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
                //showColumns: false,
                //showRefresh: false,
                minimumCountColumns: 0,
                columns: [
                    {
                        field: "userName",
                        title: "Tài khoản",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        field: "typeName",
                        title: "Mô đun",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        field: "actionName",
                        title: "Hành động",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        field: "fieldChangeStr",
                        title: "Trường dữ liệu thay đổi",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        field: "description",
                        title: "Ghi chú",
                        align: 'left',
                        valign: 'middle'
                    },
                    {
                        field: "dateCreateStr",
                        title: "Ngày thực thi",
                        align: 'left',
                        valign: 'middle'
                    }
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
    UserManager.init();
});
