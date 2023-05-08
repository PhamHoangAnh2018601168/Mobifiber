$(function () {
    window.common = {
        init: function () {
            common.Show_Active_Menu();
            common.action();
        },
        action: function () {
            $('#HomeActive').click(function () {
                if ($(this).attr('data-disable') !="DisableMenu") {
                    location.href = "/Home"
                }
            });
            $('#DeviceActive').click(function () {
                if ($(this).attr('data-disable') != "DisableMenu") {
                    location.href = "/Device"
                }
            });
            $('#PackageActive').click(function () {
                if ($(this).attr('data-disable') != "DisableMenu") {
                    location.href = "/Package"
                }
            });
            $('#ContractActive').click(function () {
                if ($(this).attr('data-disable') != "DisableMenu") {
                    location.href = "/Contract"
                }
            });

            $(".modal").draggable();

        },
        Show_Active_Menu: function () {
            var _url = window.location.pathname;
            var _arr_url = _url.split('/');
            var pathname = _arr_url[1];
            if (_arr_url.length > 1) {
                var action_name = "";
                if (_arr_url.length > 2) {
                    action_name = _url.split('/')[2];
                }
                common.SetMenuChild(pathname, action_name);
            }
        },
        SetMenuChild: function (controllerName, actionName) {
            var strUrl = actionName !== "" ? actionName :
                (controllerName !== "" ? controllerName : "Home");
            var selector = $('a[href$="' + strUrl + '"]').parent();
            selector.addClass('active');
        },
        formatCurrentcy: function (value) {
            return new Intl.NumberFormat('en-US', { maximumSignificantDigits: 18 }).format(value);
        },
        StringToDate: function (dateStr) {
            var parts = dateStr.split("/")
            return new Date(parts[2], parts[1] - 1, parts[0])
        },
        ConvertDate: function (datetime) {
            var newdate = new Date(datetime);
            var month = newdate.getMonth() + 1;
            var day = newdate.getDate();
            var year = newdate.getFullYear();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            return day + "/" + month + "/" + year;
        },
        ConvertDateReport: function (day) {
            var month = day.slice(0, 2);
            var years = day.slice(3, 7);
            var fromDate = month + '/01/' + years;
            var date = new Date(fromDate);
            return date;
        },

        buttonLoader: function (btnId, action, textLoading) {
            var self = $('#' + btnId);
            if (action == 'start') {
                //if ($(self).attr("disabled") == "disabled") {
                //    e.preventDefault();
                //}
                $(self).attr("disabled", "disabled");
                $(self).attr('data-btn-text', $(self).text());
                $(self).html('<span class="spinner"><i class="fa fa-spinner fa-spin"></i></span>' + textLoading + '');
                $(self).addClass('active');
            }
            if (action == 'stop') {
                $(self).html($(self).attr('data-btn-text'));
                $(self).removeClass('active');
                $(self).removeAttr("disabled");
            }
        },

        CaculatorAlocationTime: function (datetime) {
            var newdate = new Date(datetime);
            var monthactive = newdate.getMonth() + 1;
            var dayactive = newdate.getDate();
            var yearactive = newdate.getFullYear();

            var datenow = moment().date();
            var monthnow = moment().month() + 1;
            var yearnow = moment().year();

            var date = parseInt((monthnow - monthactive) + 12 * (yearnow - yearactive));
            return date;
        },
        RemoveSpecialChar: function (sender) {
            sender.value = sender.value.replace(/[^0-9]/g, "");
        },
       
        SaveFileAs: function (uri, filename) {
            var link = document.createElement('a');
            if (typeof link.download === 'string') {
                link.href = uri;
                link.download = filename;

                //Firefox requires the link to be in the body
                document.body.appendChild(link);

                //simulate click
                link.click();

                //remove the link when done
                document.body.removeChild(link);
            } else {
                window.open(uri);
            }
        },
        checkResponseStatus: function (rp) {
            if (rp.code != undefined && rp.code != null && rp.code == 403) {
                toastr.error(rp.message);
                return false;
            }
            return true;
        }
    }
});
$(document).ready(function () {
    common.init();
});