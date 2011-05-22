/// <reference path="jquery-1.3.1-vsdoc.js" />


function classTitleInit(){
	var m=$("#sSite");
	var classobj=$("#iSiteId");
	if(m.lenght==0||classobj.lenght==0)return;
	if(classobj.val()!="0"){
	    m.text(classobj.val());
	}
}

function GetClassTitleById(id){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(id==NewsLis[i][0]){
			return NewsLis[i][2]+"("+NewsLis[i][3]+")";
		}
	}
	return "";
}

$(document).ready(function() {
    var TtempLateFromMdy = $("#TtempLateFromMdy");
	var TtempLateFromAdd = $("#TtempLateFromAdd");
	var options;
	if(TtempLateFromMdy.length!=0){
		options = {
			beforeSubmit: CheckMdyForm,
			dataType: 'json',
			success: AjaxPostFormBack
		};
		TtempLateFromMdy.ajaxForm(options);
	}
	if(TtempLateFromAdd.length!=0){
		options = {
			beforeSubmit: CheckForm,
			dataType: 'json',
			success: AjaxPostFormBack
		};
		TtempLateFromAdd.ajaxForm(options);
	}
});


function CheckForm(){
	if(!(CheckContent()&&CheckUrl()&&CheckTempName()&&CheckType())){
		return false;
	}
	SetAjaxDiv("loader",false,"���ڷ�������...");
	return true;
}

function CheckMdyForm(){
	if(!(CheckContent()&&CheckUrl()&&CheckTempName()&&CheckType())){
		return false;
	}
	SetAjaxDiv("loader",false,"���ڷ�������...");
	return true;
}

function AddPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"ģ����ӳɹ���");
}

function CheckContent(){
	var obj=$("#vcContent");
	var n=$("#conmsg");
	if(obj.val()==""){
	    n.removeClass("info_ok").addClass("info_err");
	    n.text("ģ�����ݲ���Ϊ��");
		return false;
	}else{
		n.removeClass("info_err").addClass("info_ok");
		n.text("");
		return true;
	}
}

function CheckUrl() {
    var obj = $("#vcUrl");
    var n = $("#urlmsg");
    var o = $("#tType");
    obj.removeClass("itxt1").addClass("itxt1");
    var ov1 = parseInt(o[0].value);
    if (ov1 == 0) {
        if (obj.val() == "") {
            alert(ov1);
            n.removeClass("info_ok").addClass("info_err");
            n.text("��ҳʱ��ַ����Ϊ��");
            return false;
        } else {
            n.removeClass("info_err").addClass("info_ok");
            n.text("����ʹ�ã�");
            return true;
        }
    } else {
        obj.val("");
        n.removeClass("info_err").addClass("info_ok");
        n.text("�ǵ�ҳ��ַ����Ҫ��");
    }
    return true;
}

function CheckTempName(){
	var obj=$("#vcTempName");
	var n=$("#tnmsg");
	obj.removeClass("itxt1").addClass("itxt1");
	if(obj.val()==""){
	    n.removeClass("info_ok").addClass("info_err");
		n.text("ģ�����Ʋ���Ϊ��");
		return false;
	}else{
		n.removeClass("info_err").addClass("info_ok");
		n.text("��Ѷģ�������,����ʹ�ã�");
		return true;
	}
}


function TempnageChange() {
    var obj = $("#vcTempName");
    var url = $("#vcUrl");
    var parentPath = $("#parentPath");

    url.val(parentPath.val() + obj.val());
    
}

function CheckType() {
    var obj = $("#tType");
    var n = $("#typemsg");
    if (obj[0].value == "-1") {
        n.removeClass("info_ok").addClass("info_err");
        n.text("��ѡ��ģ�����");
        return false;
    } else {
        n.removeClass("info_err").addClass("info_ok");
        n.text("ģ�����,����ʹ�ã�");
        return true;
    }
}