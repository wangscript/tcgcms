/// <reference path="../../../manage/js/jquery-1.3.1-vsdoc.js" />


function vcoderefash() {
    $('#vcodie').attr('src', $('#vcodie').attr('src') + "#temp=" + new Date().toString());
}


$(document).ready(function () {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: function () { return true; },
        dataType: 'json',
        success: function (data) {

            alert(data.message);

            if (data.state) {
                window.location.reload();
            }
        }
    };
    form1.ajaxForm(options);

});