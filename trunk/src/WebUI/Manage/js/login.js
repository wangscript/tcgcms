//--------------
var ajax = new AJAXRequest();

function FromPost(){
	var errText = $("errText");
	errText.className = "errText";
	SetAjaxLoad(errText,"正在提交数据...");
	
	var adminname = $("username");
	var password = $("password");
	var radminName = $("rUsername");
	
	if(adminname.value==""){
		errText.innerHTML = "请输入用户名！";
		errText.className = "errTextW";
		adminname.focus();
		return false;
	}
	
	if(password.value==""){
		errText.innerHTML = "请输入登陆密码！";
		errText.className = "errTextW";
		password.focus();
		return false;
	}
	ajax.postf($("form1"),function(obj){LoginCkBack(obj.responseText);});
	return false;
}

function LoginCkBack(val){
	if(Number(val)<0){
		ajax.get("AjaxMethod/Text_GetErrText.aspx?ErrCode="+val,
			function(obj) { Back2(obj.responseText);}
		)
		return;
	}
	top.location.href = "main.aspx";
}

function Back2(val){
	var errText = $("errText");
	errText.className = "errTextW";
	errText.innerHTML = val;
}

function oncunle(){
	var errText = $("errText");
	errText.innerHTML = "";
	errText.className = "errText";
	return true;
}