$(function () {
    window.Role = {
        init: function () {
            $('#RoleUser,#SelectRoleGroup').select2();
            $('.DateInput, .DateReinput').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });
            var datenow = moment(new Date()).format('DD/MM/YYYY');
            $('.LockRoleDate').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
            });
            if ($('#DataConfigRole').attr('data-status') == 'Active') {
                var dateactive = $('#DataConfigRole').attr('value'); 
                $('.LockRoleDate').datepicker('setDate', dateactive);
            }
            else {
                $('.LockRoleDate').datepicker('setDate', datenow);
            }
            if ($('#DisableSaler').prop('checked') == true) {
                $('#LockRoleDate').prop('disabled', true);
            }
            else {
                $('#LockRoleDate').prop('disabled', false);
            }
            Role.action();
        },
        action: function () {
            $('#SelectRoleGroup').on('change', function () {
                location.href = "/Setting/RoleGroup?Id=" + $('#SelectRoleGroup').val();
            });
            $("#chk_All_New").click(function () {
                $('input:checkbox[data-id=chk_Add_New_Item]').not(this).prop('checked', this.checked);
            });

            $("#chk_All_Edit").click(function () {
                $('input:checkbox[data-id=chk_Edit_Item]').not(this).prop('checked', this.checked);
            });

            $("#chk_All_View").click(function () {
                $('input:checkbox[data-id=chk_View_Item]').not(this).prop('checked', this.checked);
            });

            $("#chk_All_Delete").click(function () {
                $('input:checkbox[data-id=chk_Delete_Item]').not(this).prop('checked', this.checked);
            });

            $("#chk_All_Upload").click(function () {
                $('input:checkbox[data-id=chk_Upload_Item]').not(this).prop('checked', this.checked);
            });

            $("#chk_All_Export").click(function () {
                $('input:checkbox[data-id=chk_Export_Item]').not(this).prop('checked', this.checked);
            });
            $("#chk_All_Report").click(function () {
                $('input:checkbox[data-id=chk_Report_Item]').not(this).prop('checked', this.checked);
            });

            $('#btnAddDevice').click(function () {
                var Id = $('#Deviceid').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn sửa lại thông tin thiết bị !')
                    if (r == true) {
                        var url = "/Device/Update";
                        Device.AddOrUpdateNewDevice(Id, url);
                    }
                }
                else {
                    var url = "/Device/Create"
                    Device.AddOrUpdateNewDevice(Id, url); //0 là thêm mới
                }
            });
            //$('#showpassword').click(function () {
            //    if ($('#CreatePassword').attr('type') == 'password') {
            //        $('#CreatePassword').attr('type', 'text');
            //        $('#showpassword').removeClass('fa-eye');
            //        $('#showpassword').addClass('fa-eye-slash');
            //    }
            //    else {
            //        $('#CreatePassword').attr('type', 'password');
            //        $('#showpassword').removeClass('fa-eye-slash');
            //        $('#showpassword').addClass('fa-eye');
            //    }
            //});
            $("#DisableSaler").click(function () {
                var val = $('#DisableSaler').prop('checked');
                var datelock = $('#LockRoleDate').val();
                if (datelock == "") {
                    toastr.error("Vui lòng chọn ngày khóa quyền phân kỳ !");
                    return;
                }
                $.ajax({
                    url: '/Setting/DisableSaler',
                    type: "Get",
                    data: {
                        disable: val,
                        datelock: datelock
                    },
                    success: function (rp) {
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
                            setTimeout(function () {
                                location.reload();
                            }, 1500);
                        }
                    },
                });
            });
        },

    }
});
$(document).ready(function () {
    Role.init();
});
