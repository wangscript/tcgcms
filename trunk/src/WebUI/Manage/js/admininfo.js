//--------------
/// <reference path="jquery-1.3.1-vsdoc.js" />

function DeleteRole(){
	if(iRoleId==0)return false;
	var count = GetCheckBoxCount("CheckID");
	if(count>0){
	    SetAjaxDiv("err", false, "Ҫɾ������ϵ�飬�����Ƴ���ɾ�������еĹ���Ա");
		return false;
	}
    SetAjaxDiv("loader", false, "���ڷ�������...");
	return true;
}

var iRoleId = 0;
$(document).ready(function () {
    //����ύ����
    iRoleId = $("#iRoleId").val();
    var options = {
        beforeSubmit: DeleteRole,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    $("#form1")[0].action = "AjaxMethod/Admin_DelAdminRole.aspx?iRole=" +$("#iRoleId").val();
    $("#form1").ajaxForm(options);
});
