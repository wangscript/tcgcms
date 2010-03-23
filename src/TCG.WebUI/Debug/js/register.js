
var EmailEx = null;
var PassWordEx = null;
var EmailMsgEx = null;
var PassWordMsgEx = null;
var RePassWordEx=null;
var RePassWordMsgEx = null;
var ValidateCodeMsgEx = null;
var ValidateCodeEx = null;

$(document).ready(function() {
	var RegisterFormEx = $("#RegisterEx");
	RegisterFormEx[0].action = "/interface/userpost.aspx?temptime=" + new Date().toString() + "&action=USER_REGISTER";
	RegFromInitEx();
    /*添加提交方法*/
    var options = {
        beforeSubmit: FromPostEx,
        dataType: 'json',
        success: RegisterBackEx
    };
    RegisterFormEx.ajaxForm(options);
});

/*检测邮箱*/
function CheckEmailEx(){
	var pt = /^([a-zA-Z0-9\@\.]{2,50})|[\u4e00-\u9fffa-zA-Z0-9\@\.]{2,25}$/;
	var EmailValue = EmailEx.val();
	if(!utils.IsRegex(EmailValue,pt)){
		EmailMsgEx.html("会员名只能由3到50个英文和数字或2到25个中文组成，不能含空格或特殊符号！");
		EmailMsgEx[0].className = "fl info1 err";
		return false;
	}
	
	$.ajax({
	    type: "GET", url: "/interface/userget.aspx?temptime=" + new Date().toString(), data: "action=CHECK_USER_NAME&name="+EmailValue,
	    errror: function(){ 
			EmailMsgEx.html("无法检测用户名，请联系系统管理员！");
			EmailMsgEx[0].className = "fl info1 err";
			return false;
		},
	    success: function(data) {
	        if (!data.state) {
				var text = (data.message=='')?"很抱歉，[<span class='blue'>"+EmailValue+"</span>] 已经被其他会员占用，推荐使用您常用的邮箱！":data.message;
				EmailMsgEx.html(text);
				EmailMsgEx[0].className = "fl info1 err";
				return;
	        }
			
			EmailMsgEx.html("恭喜您，[<span class='blue'>"+EmailValue+"</span>] 可以使用！");
			EmailMsgEx[0].className = "fl info1 ok";
	    },
	    dataType: "json"
	});
	
	EmailMsgEx[0].className = "fl info1 ok";
	return true;
}

/*检测密码*/
function CheckPassWordEx(){
	var pt = /^[a-zA-Z0-9]{6,16}$/;
	var PassWordVale = PassWordEx.val();
	if(!utils.IsRegex(PassWordVale,pt)){
		PassWordMsgEx[0].className = "fl info1 err";
		return false;
	}
	PassWordMsgEx[0].className = "fl info1 ok";
	return true;
}


/*检测重复输入密码*/
function CheckRePassWordEx(){
	
	if(!CheckPassWordEx()){
		PassWordEx.focus();
		return false;
	}
	if(PassWordEx.val()!=RePassWordEx.val()){
		RePassWordMsgEx.html('两次输入的密码不一致！');
		RePassWordMsgEx[0].className = "fl info1 err";
		return false;
	}
	RePassWordMsgEx.html('');
	RePassWordMsgEx[0].className = "fl info1 ok";
	return true;
}

function RegFromInitEx(){
	EmailEx = $('#EmailEx');
	EmailMsgEx = $("#EmailMsgEx");
	PassWordEx = $('#PassWordEx');
	PassWordMsgEx = $("#PassWordMsgEx");
	RePassWordEx = $("#RePassWordEx");
	RePassWordMsgEx = $("#RePassWordMsgEx");
	ValidateCodeEx = $("#Validate_CodeEx");
	ValidateCodeMsgEx = $("#ValidateCodeMsgEx");
	
	/*设置邮箱离开事件*/
	EmailEx.blur(CheckEmailEx);
	PassWordEx.blur(CheckPassWordEx);
	RePassWordEx.blur(CheckRePassWordEx);
	ValidateCodeEx.blur(CheckValidateCodeEx);
}

function CheckValidateCodeEx(){
	var ValidateCodeValue = ValidateCodeEx.val();
	if(!utils.IsRegex(ValidateCodeValue,/^[a-zA-Z0-9]{4}$/)){
		ValidateCodeMsgEx.html('请输入4位验证码！');
		ValidateCodeMsgEx[0].className = "fl info1 err";
		return false;
	}
	
	$.ajax({
	    type: "GET", url: "/interface/userget.aspx?temptime=" + new Date().toString(), data: "action=CHECK_USER_VALIDATECODE&code="+ValidateCodeValue,
	    errror: function(){ 
			EmailMsgEx.html("无法检测验证码，请联系系统管理员！");
			EmailMsgEx[0].className = "fl info1 err";
			return false;
		},
	    success: function(data) {
	        if (!data.state) {
				ValidateCodeMsgEx.html('您输入的验证码不正确！');
				ValidateCodeMsgEx[0].className = "fl info1 err";
				return;
	        }else{
				ValidateCodeMsgEx.html('验证码输入正确！');
				ValidateCodeMsgEx[0].className = "fl info1 ok";
			}
	    },
	    dataType: "json"
	});
	
	ValidateCodeMsgEx.html('');
	return true;
}

/*表单提交的处理方法*/
function FromPostEx(){
	var pt = /^([a-zA-Z0-9\@\.]{2,50})|[\u4e00-\u9fffa-zA-Z0-9\@\.]{2,25}$/;
	var EmailValue = EmailEx.val();
	if(!utils.IsRegex(EmailValue,pt)){
		EmailMsgEx.html("会员名只能由3到50个英文和数字或2到25个中文组成，不能含空格或特殊符号！");
		EmailMsgEx[0].className = "fl info1 err";
		return false;
	}
	
	var ValidateCodeValue = ValidateCodeEx.val();
	if(!utils.IsRegex(ValidateCodeValue,/^[a-zA-Z0-9]{4}$/)){
		ValidateCodeMsgEx.html('请输入4位验证码！');
		ValidateCodeMsgEx[0].className = "fl info1 err";
		return false;
	}
	
	if(!(CheckPassWordEx()&&CheckRePassWordEx())){
		return false;
	}
	
	utils.SystemDo("do","正在发送注册请求...");
	return true;
	
}

//注册会送记录
function RegisterBackEx(data){
	utils.SystemDo("text","正在分析返回数据...");
	
	if(data.state){
		utils.SystemDo("text","您已经成功注册！",function(){
			utils.SystemDo("out","返回首页",function(){
				window.location.href = "/index.aspx";
			});
		});
		
	}else{
		utils.SystemDo("err",data.message);
	}
}