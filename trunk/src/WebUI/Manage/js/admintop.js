//--------------
/// <reference path="jquery-1.3.1-vsdoc.js" />

$(document).ready(function () {
    //添加提交方法
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
		window.parent.adminmain.SetAjaxDiv("err",false,"您还没选择需要编辑的管理员!");
		return;
	}
	if(admins.indexOf(",")!=-1){
		window.parent.adminmain.SetAjaxDiv("err",false,"一次只能对一个管理员进行编辑!");
		return;
	}
	window.parent.adminmain.location.href="adminmdy.aspx?adminname="+admins;
}

function AdminDel() {
    var admins = window.parent.adminmain.GetCheckBoxValuesForSql("CheckID");
    if (admins == "") {
        window.parent.adminmain.SetAjaxDiv("err", false, "您还没选择需要删除的管理员!");
        return;
    }
    $("#admins").val(admins);
    if (confirm("您确定删除管理员[" + admins + "]")) {
        SetAjaxDivAdminMian("loader", false, "正在发送删除[" + admins + "]的请求...");
        $("#form1").submit();
    }
    return false;
}

function DelAdminsBack(val){
	if(GetErrTextFrame(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	SetAjaxDivAdminMian("ok",false,"成功删除指定的管理员！");
}

