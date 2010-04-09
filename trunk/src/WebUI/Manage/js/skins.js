/// <reference path="jquery-1.3.1-vsdoc.js" />

function SetDefaultSkin(skinid) {
    $("#Work").val("SetDefalutSkinId");
    $("#SkinId").val(skinid);
    $("#form1").submit();
}

function LoginCkBack() { refinsh(); }


$(document).ready(function() {
    var options = {
        beforeSubmit: function() { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    $("#form1").ajaxForm(options);
});


function CreateSql(skinid) {
    $("#Work").val("CreateSkinSql");
    $("#SkinId").val(skinid);
    SetAjaxDiv("loader", false, "正在生成模板脚本...");
    $("#form1").submit();
}