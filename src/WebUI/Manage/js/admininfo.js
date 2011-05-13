//--------------
/// <reference path="jquery-1.3.1-vsdoc.js" />

function DeleteRole(){
	if(iRoleId==0)return false;
	var count = GetCheckBoxCount("CheckID");
	if(count>0){
	    SetAjaxDiv("err", false, "要删除此联系组，请先移出或删除此组中的管理员");
		return false;
	}
    SetAjaxDiv("loader", false, "正在发送请求...");
	return true;
}

var iRoleId = 0;
$(document).ready(function () {
    //添加提交方法
    iRoleId = $("#iRoleId").val();
    var options = {
        beforeSubmit: DeleteRole,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    $("#form1")[0].action = "AjaxMethod/Admin_DelAdminRole.aspx?iRole=" +$("#iRoleId").val();
    $("#form1").ajaxForm(options);
});
