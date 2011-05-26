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

function FeedBackDel() {

    var temps = GetCheckBoxValues("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "您没选择需要删除的留言！");
        return;
    }

    if (!confirm("您确定删除选定的留言么?")) {
        return;
    }

    SetAjaxDiv("loader", false, "正在发送请求...");
    $("#work").val("DEL");
    $("#DelClassId").val(temps);
    $("#form1").submit();
}

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


function ShowContent(id) {
    var cs = $(".list_content");
    if (cs != null && cs.length > 0) {
        cs.each(function (i) {
            var o = cs.eq(i);
            if (o.attr("id") == ("content_" + id)) {
                $(o).toggle();
            } else {
                $(o).hide();
            }
        });
    }
}