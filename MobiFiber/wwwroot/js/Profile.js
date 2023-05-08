$(function () {
    window.Profile = {
        init: function () {
            $('#birthday').datepicker({
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                todayBtn: 'linked',
                autoclose: true,
                endDate: new Date,
            });
            debugger
            $('#birthday').format('DD/MM/YYYY');
            Profile.action();
        },
        action: function () {
            
        }
    }
});
$(document).ready(function () {
    Profile.init();
});
