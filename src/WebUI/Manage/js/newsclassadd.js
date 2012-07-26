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
    
    SetAjaxDiv("loader", false, "正在发送请求...");
    return true;
}

function GetParentTitle() {
    var o = $("#iClassId");
    if (o == null) return;
    var placemsg = $("#placemsg");
    if (o.length == 0) {
        placemsg.html(placemsg.html() + "作为网站分类");
    } else {
        a = "";
        if (o.val() != "") {
            GetNewsListTitleByClassIdW(o.val());
            a = "根类别>>" + a;
            placemsg.html(placemsg.html() + a);
        } 
    }
}


function GetNewsListTitleByClassIdW(classid) {
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        
        if (_Categories[i].Id == classid && _Categories[i].Skin.Id == $("#skinid").val()) {
            var t = (_Categories[i].ParentId == 0) ? "" : ">>";
            a = t + _Categories[i].ClassName + a;
            GetNewsListTitleByClassIdW(_Categories[i].ParentId);
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
    SetFromsByNum("a1");

    $.get("../Common/AllNewsSpeciality.aspx?skinid=" + $("#skinid").val() + "&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            data = data.substring(4, data.length);
            eval(data);

            var iSpeciality = $("#iSpeciality");
            var iSpeciality_t = $("#iSpeciality_t");

            var p = GetSpecialityById(iSpeciality.val());
            
            if (p == null) {
                iSpeciality_t.val("请选择资讯特性...");
            } else {
                iSpeciality_t.val(p.vcTitle);
            }

            GetSpecialityEnmu($("#iSpeciality_cc"), $("#skinid").val(), "0");

            Menu.init("iSpeciality_c");
        });

    $("#form1").ajaxForm(options);

});


function CheckEditClassForm() {
    var iClassId = $("#iClassId");

    if (!(CheckValueIsNull('iClassName', 'cnamemsg') && CheckValueIsNull('iName', 'inamemsg')
			&& CheckValueIsNull('iDirectory', 'dirmsg') &&
			CheckTemplate('sTemplate', 'stdmsg') && CheckTemplate('slTemplate', 'stsdmsg'))) {
        return false;
    }
    SetAjaxDiv("loader", false, "正在发送请求...");
    return true;
}

function EditClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"资讯分类修改成功！");
}

function AddClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"资讯分类添加成功！");
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


var objss = ["a1", "a2", "a3", "a4"];
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