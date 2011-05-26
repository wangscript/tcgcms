/// <reference path="jquery-1.3.1-vsdoc.js" />


$(document).ready(function () {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: function () { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

});



function classTitleInit() {
    var m = $("#classTitle");
    var iSkinId = $("#iSkinId");

    var a = "";
    if (_Skin != null) {
        for (var i = 0; i < _Skin.length; i++) {
            if (iSkinId.val() == _Skin[i].Id) {
                a = _Skin[i].Name;
            }
        }
    }
    m.html("<span class='txt bold'>" + a + "留言列表</span>");
}