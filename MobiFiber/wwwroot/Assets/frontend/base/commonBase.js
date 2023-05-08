function activeMenu() {
    var type = $('#Type').val();
    $(".menuzord-menu li").each(function () {
        if ($(this).attr('id') === type) {
            $(this).addClass('active');
        }
    });
    $("#btn-mascot").click(function () {
        $("#ApplyModal").modal('show');
        $('body').removeClass('modal-open');
    });
    $("#register_form").validate({
        submitHandler: function (form) {
            var form_btn = $(form).find('button[type="submit"]');
            var form_result_div = '#form-result';
            $(form_result_div).remove();
            form_btn.before('<div id="form-result" class="alert alert-success" role="alert" style="display: none;"></div>');
            var form_btn_old_msg = form_btn.html();
            form_btn.html(form_btn.prop('disabled', true).data("loading-text"));
            $(form).ajaxSubmit({
                dataType: 'json',
                success: function (data) {
                    if (data.status === true) {
                        //$(form).find('.form-control').val('');
                        addProductNotice('', '', 'Request successful', 'success');
                        $("#ApplyModal form").trigger('reset');
                        $("#ApplyModal").modal('hide');
                    }
                    else {
                        addProductNotice('', '', data.mess, 'danger');
                    }
                    form_btn.prop('disabled', false).html(form_btn_old_msg);
                    //$(form_result_div).html(data.message).fadeIn('slow');
                    //setTimeout(function () { $(form_result_div).fadeOut('slow') }, 6000);
                }
            });
        }
    });
}
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
function goBack() {
    window.history.back();
}
function addProductNotice(title, thumb, text, type) {

    //$.jGrowl.defaults.closer = false;
    //Stop jGrowl
    //$.jGrowl.defaults.sticky = true;
    var tpl = text;
    //$.jGrowl(tpl, {
    //	life: 4000,
    //	header: title,
    //	speed: 'slow',
    //	theme: type
    //});
    //alert('a');

    $.notify({
        icon: 'ti-user',
        message: tpl

    }, {
            element: 'body',
            type: type,
            timer: 1000,
            template: '<div data-notify="container" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert">' +
                '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                '<span data-notify="icon"></span> ' +
                '<span data-notify="title">{1}</span> ' +
                '<span data-notify="message">{2}</span>' +
                '<div class="progress" data-notify="progressbar">' +
                '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                '</div>' +
                '<a href="{3}" target="{4}" data-notify="url"></a>' +
                '</div>'
        });

}
function smalldateFormatJsonDMY(datetime) {
    if (datetime === '' || datetime === undefined || datetime === null) {
        return '';
    } else {
        var newdate = new Date(parseInt(datetime.substr(6)));
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        return day + "/" + month + "/" + year;
    }
}
function smalldateFormatJsonYMD(datetime) {
    if (datetime === '' || datetime === undefined || datetime === null) {
        return '';
    } else {
        var newdate = new Date(parseInt(datetime.substr(6)));
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        return year + "-" + month + "-" + day;
    }
}
function smalldateFormatJsonDMYDateOnly(datetime) {
    if (datetime === '' || datetime === undefined || datetime === null) {
        return '';
    } else {
        var parts = datetime.substring(0, 10).split('-');
        var newdate = new Date(parts[0], parts[1] - 1, parts[2]);
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        return day + "/" + month + "/" + year;
    }
}
function stringToDate(_date, _format, _delimiter) {
    var formatLowerCase = _format.toLowerCase();
    var formatItems = formatLowerCase.split(_delimiter);
    var dateItems = _date.split(_delimiter);
    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var month = parseInt(dateItems[monthIndex]);
    month -= 1;
    var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
    return formatedDate;
}
function convertTimeStringTo12Hours(timeString) {
    if (timeString === '' || timeString === undefined || timeString === null) {
        return '';
    } else {
        var parts = timeString.split(':');
        if (parts[0] <= 12) {
            return parts[0] + ":" + parts[1] + " AM";
        }
        else {
            return (parts[0] - 12) + ":" + parts[1] + " PM";
        }
    }
}
function DayOfWeek() {
    var d = new Date();
    var n = d.getDay();
    var day = 1;
    switch (n) {
        case 0:
            day = 5;
            break;
        case 1:
            day = 1;
            break;
        case 2:
            day = 1;
            break;
        case 3:
            day = 2;
            break;
        case 4:
            day = 2;
            break;
        case 5:
            day = 3;
            break;
        case 6:
            day = 4;
            break;
    }
    var div = document.getElementById(day);
    div.classList.add('text-yellow');
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageUpload')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
function headerSearch() {
    window.location.href = "/Program/Search/" + $('#Courses').val();
}
function UrlExists(url) {
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status != 404;
}
function ClearSession() {
    $.ajax({
        url: '/Login/Logout',
        type: 'POST',
        success: function (rs) {
            if (rs) {
                window.location.href = "/login";
            }
            else {
                toastr.error('Err');
            }
        }
    })
}