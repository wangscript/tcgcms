//--------------
var ajax = new AJAXRequest();

function DeleteRole(iRoleId){
	if(iRoleId==0)return false;
	var count = GetCheckBoxCount("CheckID");
	if(count>0){
		SetAjaxDiv("err",false,"要删除此联系组，请先移出或删除此组中的管理员");
		return false;
	}
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.post("AjaxMethod/Admin_DelAdminRole.aspx","iRole="+iRoleId,function(obj){DelRoleBack(obj.responseText);});
	return false;
}

function DelRoleBack(val){
	if(GetErrText(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	window.location.href="adminInfo.aspx";
}