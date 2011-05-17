/// <reference path="jquery-1.3.1-vsdoc.js" />

var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};

function classTitleInit() {
    var m = $("#classTitle");
    var classobj = $("#iSkinId");
    var SytemType = $("#SytemType");
    var iParentid = $("#iParentid");
    if (m.lenght == 0 || classobj.lenght == 0) return;
    if (iParentid == "0") return;

    m.html(m.html() + GetTempTitleBy(classobj.val(), iParentid.val()));
}


$(document).ready(function() {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: BeforSubmit,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);
});

function GetTempTitleBy(site, parentid) {

    if (parentid == "1") {
        return ">> <a href='?iParentid=1&SkinId=" + site + "'>��ϸҳģ��</a>"
    } else if (parentid == "2"){
        return ">> <a href='?iParentid=2&SkinId=" + site + "'>�б�ģ��</a>"
    } else if (parentid == "3") {
        return ">> <a href='?iParentid=3&SkinId=" + site + "'>ԭ��ģ��</a>"
    }
    for (var i = 0; i < _Template.length; i++) {
        if (_Template[i].SkinId == site && _Template[i].Id == parentid) {
            return GetTempTitleBy(site, _Template[i].ParentId) + " >> <a href='?iParentid=" +
			parentid + "&SkinId=" + site + "'>" + _Template[i].TempName + "</a>";
		}
	}
	return "";
}

function TempDel() {
    var temps = GetCheckBoxValuesForSql("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "��ûѡ����Ҫɾ����ģ�棡");
        return false;
    }
    var t = CheckTempsUsed(temps);
   
    if (t == "") {
        SetAjaxDiv("err", false, "��ѡ���ģ�棬����ʹ���У��޷�ɾ����");
        return false;
    }

    if (t != temps) {
        var CheckBoxMain = $("CheckBoxMain");
        CheckBoxMain.checked = false;
        SetCheckBoxBg('CheckID', CheckBoxMain);
        SetCheckBox("CheckID", GetCheckedBoxSetting(t));
        SetAjaxDiv("err", false, "��ѡ���ģ���к�������ʹ�õ�ģ�棬ϵͳ�Ѿ��Զ�ѡ�����ɾ���Ĳ��֣�");
        return false;
    }
    $("#temps").val(t);
    $("#work").val("DEL");
    SetAjaxDiv("loader", false, "���ڷ�������...");
    $('#form1').submit();
}



function CheckTempsUsed(temps) {
	if(temps=="")return false;
	var str="";
	if(temps.indexOf(",")>-1){
		var t = temps.split(",");
		for(var i=0;i<t.length;i++){
			if(!CheckTempUsed(t[i])){
				var x=(str=="")?"":",";
				str+=x+t[i];
			}
		}
	}else{
		if(!CheckTempUsed(temps))str=temps;
	}
	return str;
}

function CheckTempUsed(temp) {
    if (temp == "") return false;
    temp = temp.replace("'", "").replace("'", "");
	if (_Categories == null) return false;
	for (var i = 0; i < _Categories.length; i++) {
	    if (temp == _Categories[i].ResourceListTemplate.Id || temp == _Categories[i].ResourceTemplate.Id) return true;
	}
	if (_Template == null) return false;
	for (var i = 0; i < _Template.length; i++) {
	    if (temp == _Template[i].ParentId) return true;
	}
	return false;
}

function AddTemplate(){
    var iSiteId = $("#iSkinId");
	var SytemType=$("#SytemType");
	var iParentid=$("#iParentid");
	window.location.href = "templateadd.aspx?iSkinId=" + iSiteId.val() + "&SytemType=" + SytemType.val() + "&iParentid=" + iParentid.val();
}
function EditTemplate(){
	var vs=GetCheckBoxValues("CheckID")
	if(vs==""){
		SetAjaxDiv("err",false,"��ûѡ����Ҫ�༭��ģ�棡");
		return;
	}else{
		if(vs.indexOf(",")>-1){
			SetAjaxDiv("err",false,"һ��ֻ�ܱ༭һ��ģ�棡");
			return;
		}
	}
	window.location.href="templatemdy.aspx?templateid="+vs;
}

function sTypeChange(obj) {
    var obj = $(obj);
    if (obj.val() == "-1") {
        var iParentid = $("#iParentid");
        var iSkinId = $("#iSkinId");
        window.location.href = "?SkinId=" + iSkinId.val() + "&iParentid=" + iParentid.val();
    } else {
        var iParentid = $("#iParentid");
        var iSkinId = $("#iSkinId");
        window.location.href = "?SkinId=" + iSkinId.val() + "&iType=" + obj.val() + "&iParentid=" + iParentid.val();
    }
}

//����ģ��
function PageCreat() {
    var temps = GetCheckBoxValues("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "��ûѡ����Ҫ���ɵ�ģ�棡");
        return;
    }
    
    CreateDiv.Start("���ɵ�ҳģ���ļ�");
    layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: operback });
    var work = $("#work");
    var iTemplateId = $("#iTemplateId");
    if (temps.indexOf(",") > -1) {
        var o = temps.split(",");

        o = CheckCreateTemplate(o);
        
        CreateDiv.set = 1;
        CreateDiv.setcount = o.length;
        for (var i = 0; i < o.length; i++) {
            var t = GetTemplateById(o[i]);

            work.val("Create");
            iTemplateId.val(o[i]);
            $('#form1').submit();

        }
    } else {
        var t2 = GetTemplateById(temps);
        var s = true;
        if (t2 == null) {
            s = false;
        } else {
            if (t2.SystemType != 0) s = false;
        }

        if (!s) {
            SetAjaxDiv("err", false, "��ѡ���ģ�治�ǵ�ҳģ�棡");
            return;
        }
     
        CreateDiv.set = 1;
        CreateDiv.setcount = 1;
        work.val("Create");
        iTemplateId.val(temps);
        $('#form1').submit();
    }
}


function CheckCreateTemplate(temps) {
    var tempstr = "";
    for (var i = 0; i < temps.length; i++) {
        var t = GetTemplateById(temps[i]);
        if (t != null && t.TemplateType == 0) {
            tempstr += (tempstr == "" ? "" : ",") + temps[i];
        }
    }

    return tempstr.split(",");
}

function CreateBack(data) {
    CreateDiv.SetSep(data);
}

function operback() {
}