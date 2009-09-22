//--------------
var ajax = new AJAXRequest();

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

function AdminDel(){
	var admins = window.parent.adminmain.GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		window.parent.adminmain.SetAjaxDiv("err",false,"您还没选择需要删除的管理员!");
		return;
	}
	$("admins").value=admins;
	if(confirm("您确定删除管理员["+admins+"]")){
		SetAjaxDivAdminMian("loader",false,"正在发送删除["+admins+"]的请求...");
		ajax.postf($("form1"),function(obj) { DelAdminsBack(obj.responseText);});
	}
}

function DelAdminsBack(val){
	if(GetErrTextFrame(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	SetAjaxDivAdminMian("ok",false,"成功删除指定的管理员！");
}

function refinsh(){
	window.parent.adminmain.location.href=window.parent.adminmain.location.href;	
}