//--------------
var ajax = new AJAXRequest();

function CheckForm(num){
	var pwd=$("iNewPWD");
	var cpwd=$("iCPWD");
	var o=$("iLockY");
	var p=$("iLockN");
	var lockmsg=$("lockmsg");
	if(p.checked==false&&o.checked==false){
		lockmsg.className="info_err";
		SetInnerText(lockmsg,"还未设定管理员的状态");
	}else{
		lockmsg.className="info_ok";
		SetInnerText(lockmsg,"成功设置管理员状态!");
	}
	
	var nnmsg = $("nnmsg");
	var iNickName=$("iNickName");
	if(iNickName.value==""){
		nnmsg.className = "info_err";
		SetInnerText(nnmsg,"昵称不能为空!");
	}
	if(num!=1){
		if(!(CheckAdminName()&&CheckRole()&&CheckNewPassword(pwd)&&CheckCPWD(cpwd)))return false;
	}else{
		if(!(CheckRole()&&CheckCPWD(cpwd)))return false;
	}
	ajax.postf($("form1"),function(obj) { AddadminPostBack(obj.responseText);});
	return false;
}

function AddadminPostBack(val){
	if(GetErrText(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	SetAjaxDiv("ok",false,"您对管理员的操作已经成功执行！");
}

function CheckAdminName(){
	var adminname = $("vcAdminName");
	adminname.className="itxt1";
	var adminmsg = $("adminmsg");
	if(adminname==null)return;
 	if(adminname.value==""||(adminname.value.length<3||adminname.value>18)){
		adminmsg.className="info_err";
		SetInnerText(adminmsg,"登陆名不能为空或长度不在3-18个字符之间!");
		adminmsg.focus();
	}
	ajax.get("AjaxMethod/Admin_CheckAdminName.aspx?admin="+adminname.value,function(obj) { CheckadminBack(obj.responseText);});
	return true;
}

function CheckadminBack(val){
	var adminmsg = $("adminmsg");
	if(Number(val)<0){
		adminmsg.className="info_err";
		SetInnerText(adminmsg,"登陆名不能为空!");
		adminmsg.focus();
	}else if(Number(val)==0){
		adminmsg.className="info_ok";
		SetInnerText(adminmsg,"登陆名输入正确,可以使用!");
	}else if(Number(val)>0){
		adminmsg.className="info_err";
		SetInnerText(adminmsg,"登陆名已经存在,请更换后重试!");
	}
}

function CheckRole(){
	var sRole = $("sRole");
	var rolemsg = $("rolemsg");
	if(sRole.value=="0"){
		rolemsg.className="info_err";
		SetInnerText(rolemsg,"还未给管理员分配角色!");
		return false;
	}
	rolemsg.className="info_ok";
	SetInnerText(rolemsg,"管理员角色分配成功!");
	return true;
}