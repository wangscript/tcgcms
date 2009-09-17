//--------------
var ajax = new AJAXRequest();

function CheckForm(){
	var iOldPWD = $("iOldPWD");
	var iNewPWD = $("iNewPWD")
	var iCPWD = $("iCPWD");
	var iNickName = $("iNickName");
	var nnmsg = $("nnmsg");
	var post = $("post");
	
	if(iNickName.value==""){
		nnmsg.className = "info_err";
		SetInnerText(nnmsg,"昵称不能为空!");
	}
	
	if(!(CheckCPWD(iCPWD))){
		return false;
	}
	CheckPasswordFrom(iOldPWD);
	return false;
}

function ChanagePostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"用户登陆信息更改成功！");
}

function CheckPassword(obj){
	obj.className='itxt1';
	var og = $("pwdmsg");
	if(og==null)return false;
	if(obj.value == ""){
		og.className = "info_err";
		SetInnerText(og,"原始密码不能为空!");
		return false;
	}
	ajax.get("AjaxMethod/Admin_CheckPwd.aspx?PWD="+obj.value,
			function(obj) { CheckPwdBack(obj.responseText);}
		);
}

function CheckPasswordFrom(obj){
	obj.className='itxt1';
	var og = $("pwdmsg");
	if(og==null)return false;
	if(obj.value == ""){
		og.className = "info_err";
		SetInnerText(og,"原始密码不能为空!");
		return false;
	}
	ajax.get("AjaxMethod/Admin_CheckPwd.aspx?PWD="+obj.value,
			function(obj) { CheckPwdBack1(obj.responseText);}
		);
}


function CheckPwdBack1(obj){
	var og = $("pwdmsg");
	if(obj=="true"){
		SetAjaxDiv("loader",false,"正在发送请求...");
		ajax.postf($("form1"),function(obj) { ChanagePostBack(obj.responseText);});
	}
	else if(obj=="false"){
		og.className = "info_err";
		SetInnerText(og,"请正确输入原始密码!");
	}
}


function CheckPwdBack(obj){
	var og = $("pwdmsg");
	if(obj=="true"){
		og.className = "info_ok";
		SetInnerText(og,"原始密码输入正确!");
		return true;
	}
	else if(obj=="false"){
		og.className = "info_err";
		SetInnerText(og,"请正确输入原始密码!");
		return false;
	}
}