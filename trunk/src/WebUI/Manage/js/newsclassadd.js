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

});


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