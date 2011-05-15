//--------------
/// <reference path="jquery-1.3.1-vsdoc.js" />

$(document).ready(function () {
    //����ύ����
    var options = {
        beforeSubmit: function () { return true; },
        dataType: 'json',
        success: AjaxPostFormBack2
    };
    $("#form1").ajaxForm(options);
});

function AdminEdit(){
	var admins = window.parent.adminmain.GetCheckBoxValues("CheckID");
	if(admins==""){
		window.parent.adminmain.SetAjaxDiv("err",false,"����ûѡ����Ҫ�༭�Ĺ���Ա!");
		return;
	}
	if(admins.indexOf(",")!=-1){
		window.parent.adminmain.SetAjaxDiv("err",false,"һ��ֻ�ܶ�һ������Ա���б༭!");
		return;
	}
	window.parent.adminmain.location.href="adminmdy.aspx?adminname="+admins;
}

function AdminDel() {
    var admins = window.parent.adminmain.GetCheckBoxValuesForSql("CheckID");
    if (admins == "") {
        window.parent.adminmain.SetAjaxDiv("err", false, "����ûѡ����Ҫɾ���Ĺ���Ա!");
        return;
    }
    $("#admins").val(admins);
    if (confirm("��ȷ��ɾ������Ա[" + admins + "]")) {
        SetAjaxDivAdminMian("loader", false, "���ڷ���ɾ��[" + admins + "]������...");
        $("#form1").submit();
    }
    return false;
}

function DelAdminsBack(val){
	if(GetErrTextFrame(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	SetAjaxDivAdminMian("ok",false,"�ɹ�ɾ��ָ���Ĺ���Ա��");
}

