/// <reference path="jquery-1.3.1-vsdoc.js" />

$(document).ready(function () {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: PCheckForm,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

    SetFromsByNum("a1");

    //设置属性
    var PropertiesCategorieId = $("#PropertiesCategorieId");
    var a2from = $("#a2_from");
    if (PropertiesCategorieId.val() != "0") {

        if (_Properties != null) {
            for (var i = 0; i < _Properties.length; i++) {
                var cp = _Properties[i];
                var chtml = CategoriePropertieMdyHtml(cp.Id, cp.ProertieName, cp.Type, cp.Values, cp.width, cp.height);
                var ohtml = a2from.html();
                var line = ohtml.toString().length == 0 ? "" : "<div id=\"line_" + cp.Id + "\" class=\"ln-c-mid ln-thin\"></div>";
                a2from.html(ohtml + line + chtml);
            }
        }
    }

    SetAjaxDiv("ok", false, "小提示：属性设置好以后，在资讯发布的时候，就可以使用！");

});

function GetParentTitle() {
    var placemsg = $("#placemsg");
    var iSkinId = $("#iSkinId");

    var a = "";
    if (_Skin != null) {
        for (var i = 0; i < _Skin.length; i++) {
            if (iSkinId.val() == _Skin[i].Id) {
                a = _Skin[i].Name;
            }
        }
    }
    placemsg.html(placemsg.html() + a );
}

function PCheckForm() {
    if (CheckValueIsNull('iClassName', 'cnamemsg')) {
        return true;
    }
    return false;
}


var objss = ["a1", "a2"];
function SetFromsByNum(lb) {

    for (var i = 0; i < objss.length; i++) {
        var obj = $("#" + objss[i]);
        if (lb == objss[i]) {
            obj.attr("class", "ln-c-mid on");
            obj.unbind("mouseover");
            obj.unbind("mouseout");
            obj.unbind("click");
            $("#" + objss[i] + "_from").show();
        } else {
            obj.attr("class", "");
            obj.unbind("mouseover");
            obj.unbind("mouseout");
            obj.unbind("click");
            obj.mouseover(function () { this.className = "moson"; });
            obj.mouseout(function () { this.className = ""; });
            obj.attr("href", "javascript:GoTo();");
            $("#" + objss[i] + "_from").hide();
        }
    }
}


function CategoriePropertieHTMLAdd() {
    var ohtml = $("#a2_from").html();
    var MaxPid = $("#iMaxPId");
    var n = parseInt(MaxPid.val()) + 1;
    MaxPid.val(n);
    var chtml = CategoriePropertieMdyHtml(n, "", null, "", 0, 0);
    var line = "<div id=\"line_" + n + "\" class=\"ln-c-mid ln-thin\"></div>";
    $("#a2_from").html(ohtml + line + chtml);
}

function CategoriePropertieHTMLDel(id) {

    var classid = $("#PropertiesCategorieId");

    if (classid.length != 0) {

        if (!confirm("您确定彻底该分类属性么?")) return;
        $.post("../AjaxMethod/PropertiesDel.aspx?id=" + id, { Action: "post", Id: id },
            function (data, textStatus) {
                // data 可以是 xmlDoc, jsonObj, html, text, 等等.
                //this; // 这个Ajax请求的选项配置信息，请参考jQuery.get()说到的this
                if (data.state) {
                    $("#cp_" + id).remove();
                    $("#line_" + id).remove();
                }
                AjaxPostFormBack(data);

            }, "json");
    } else {
        $("#cp_" + id).remove();
        $("#line_" + id).remove();
    }
}