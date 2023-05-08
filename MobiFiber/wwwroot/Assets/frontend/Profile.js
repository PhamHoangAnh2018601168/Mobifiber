$(document).ready(function () {
    $(document).keypress(function (e) {
        if (e.which == 13) {
            $('#btnSave').click();
        }
    })
})