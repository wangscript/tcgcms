//--------------
/// <reference path="jquery-1.3.1-vsdoc.js" />

function CheckAddClassForm() {
    var iClassId = $("#iClassId");

    if (!(CheckValueIsNull('iClassName', 'cnamemsg') && CheckValueIsNull('iName', 'inamemsg')
			&& CheckValueIsNull('iDirectory', 'dirmsg') &&
			CheckTemplate('sTemplate', 'stdmsg') && CheckTemplate('slTemplate', 'stsdmsg'))) {
        SetFromsByNum("a1");
        return false;
    }
    
    SetAjaxDiv("loader", false, "���ڷ�������...");
    return true;
}

function GetParentTitle() {
    var o = $("#iClassId");
    if (o == null) return;
    var placemsg = $("#placemsg");
    if (o.length == 0) {
        placemsg.html(placemsg.html() + "��Ϊ��վ����");
    } else {
        a = "";
        GetNewsListTitleByClassIdW(o.val());
        a = "�����>>" + a;
        placemsg.html(placemsg.html() + a);
    }
}


function GetNewsListTitleByClassIdW(classid) {
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories.ParentId == classid) {
            var t = (_Categories.ParentId == 0) ? "" : ">>";
            a = t + _Categories.ClassName + a;
            GetNewsListTitleByClassIdW(_Categories.Name);
        }
    }
}

$(document).ready(function () {
    var options;
    if ($("#Work").length != 0) {
        options = {
            beforeSubmit: CheckEditClassForm,
            dataType: 'json',
            success: AjaxPostFormBack
        };

    } else {
        options = {
            beforeSubmit: CheckAddClassForm,
            dataType: 'json',
            success: AjaxPostFormBack
        };
    }
    $("#form1").ajaxForm(options);

    SetFromsByNum("a1");

    //��������
    var classid = $("#iClassId");
    var a2from = $("#a2_from");
    if (classid.length != 0) {

        if (_CategorieProperties != null) {
            for (var i = 0; i < _CategorieProperties.length; i++) {
                var cp = _CategorieProperties[i];
                var chtml = CategoriePropertieMdyHtml(cp.Id, cp.ProertieName, cp.Type, cp.Values, cp.width, cp.height);
                var ohtml = a2from.html();
                var line = ohtml.toString().length == 0 ? "" : "<div id=\"line_" + cp.Id + "\" class=\"ln-c-mid ln-thin\"></div>";
                a2from.html(ohtml + line + chtml);
            }
        }
    }

    SetAjaxDiv("ok", false, "С��ʾ�����ݷ������ԵĶ��壬����ʵ����������������ת�䣬�磺��Ʒ");
});


function CategoriePropertieHTMLAdd() {
    var ohtml = $("#a2_from").html();
    var MaxPid = $("#iMaxPId");
    var n = parseInt(MaxPid.val()) + 1;
    MaxPid.val(n);
    var chtml = CategoriePropertieMdyHtml(n,"",null,"",0,0);
    var line = "<div id=\"line_" + n + "\" class=\"ln-c-mid ln-thin\"></div>";
    $("#a2_from").html(ohtml + line + chtml);
}

function CategoriePropertieHTMLDel(id) {

    var classid = $("#iClassId");

    if (classid.length != 0) {

        if (!confirm("��ȷ�����׸÷�������ô?")) return;
        $.post("../AjaxMethod/CategoriePropertiesDel.aspx?id=" + id, { Action: "post", Id: id },
    function (data, textStatus) {
        // data ������ xmlDoc, jsonObj, html, text, �ȵ�.
        //this; // ���Ajax�����ѡ��������Ϣ����ο�jQuery.get()˵����this
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


function CheckEditClassForm() {
    var iClassId = $("#iClassId");

    if (!(CheckValueIsNull('iClassName', 'cnamemsg') && CheckValueIsNull('iName', 'inamemsg')
			&& CheckValueIsNull('iDirectory', 'dirmsg') &&
			CheckTemplate('sTemplate', 'stdmsg') && CheckTemplate('slTemplate', 'stsdmsg'))) {
        return false;
    }
    SetAjaxDiv("loader", false, "���ڷ�������...");
    return true;
}

function EditClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"��Ѷ�����޸ĳɹ���");
}

function AddClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"��Ѷ������ӳɹ���");
}

function CheckTemplate(on, jn) {
    var o = $("#" + on);
    var j = $("#" + jn);
    if (o.val() == "-1") {
        j.addClass("info_err").removeClass('info_ok');
        return false;
    } else {
        j.addClass("info_ok").removeClass('info_err');
        return true;
    }
}