//--------------

function CheckForm(){
	if(CheckRoleName()){
	    SetAjaxDivRoot("loader", false, "正在发送请求...");
		ajax.postf($("form1"),function(obj) { AddRoleBack(obj.responseText);});
	}
	return false;
}

function AddRoleBack(val){
    if (GetErrTextRoot(val)) return;
	window.parent.adminpop.refinsh();
	SetAjaxDivRoot("ok", false, "您已经成功完成对角色组的操作！");
}

function CheckRoleName(){
	var vcRoleName = $("vcRoleName");
	var rnmsg = $("rnmsg");
	vcRoleName.className='itxt1';
	if(vcRoleName.value.length==0){
		rnmsg.className="info_err";
		SetInnerText(rnmsg,"角色名不能为空！");
		return false;
	}
	rnmsg.className="info_ok";
	SetInnerText(rnmsg,"角色名可以使用！");
	return true;
}