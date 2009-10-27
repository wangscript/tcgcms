
var Email = null;
var PassWord = null;
var EmailMsg = null;
var PassWordMsg = null;
var RePassWord=null;
var RePassWordMsg = null;
var ValidateCodeMsg = null;
var ValidateCode = null;

$(document).ready(function() {
	var RegisterForm = $("#Register");
	RegisterForm[0].action = "/interface/userpost.aspx?temptime=" + new Date().toString() + "&action=USER_REGISTER";
	RegFromInit();
    /*添加提交方法*/
    var options = {
        beforeSubmit: FromPost,
        dataType: 'json',
        success: RegisterBack
    };
    RegisterForm.ajaxForm(options);
});

/*检测邮箱*/
function CheckEmail(){
	var pt = /^([a-zA-Z0-9\@\.]{2,50})|[\u4e00-\u9fffa-zA-Z0-9\@\.]{2,25}$/;
	var EmailValue = Email.val();
	if(!utils.IsRegex(EmailValue,pt)){
		EmailMsg.html("会员名只能由3到50个英文和数字或2到25个中文组成，不能含空格或特殊符号！");
		EmailMsg[0].className = "fl info1 err";
		return false;
	}
	
	$.ajax({
	    type: "GET", url: "/interface/userget.aspx?temptime=" + new Date().toString(), data: "action=CHECK_USER_NAME&name="+EmailValue,
	    errror: function(){ 
			EmailMsg.html("无法检测用户名，请联系系统管理员！");
			EmailMsg[0].className = "fl info1 err";
			return false;
		},
	    success: function(data) {
	        if (!data.state) {
				var text = (data.message=='')?"很抱歉，[<span class='blue'>"+EmailValue+"</span>] 已经被其他会员占用，推荐使用您常用的邮箱！":data.message;
				EmailMsg.html(text);
				EmailMsg[0].className = "fl info1 err";
				return;
	        }
			
			EmailMsg.html("恭喜您，[<span class='blue'>"+EmailValue+"</span>] 可以使用！");
			EmailMsg[0].className = "fl info1 ok";
	    },
	    dataType: "json"
	});
	
	EmailMsg[0].className = "fl info1 ok";
	return true;
}

/*检测密码*/
function CheckPassWord(){
	var pt = /^[a-zA-Z0-9]{6,16}$/;
	var PassWordVale = PassWord.val();
	if(!utils.IsRegex(PassWordVale,pt)){
		PassWordMsg[0].className = "fl info1 err";
		return false;
	}
	PassWordMsg[0].className = "fl info1 ok";
	return true;
}


/*检测重复输入密码*/
function CheckRePassWord(){
	
	if(!CheckPassWord()){
		PassWord.focus();
		return false;
	}
	if(PassWord.val()!=RePassWord.val()){
		RePassWordMsg.html('两次输入的密码不一致！');
		RePassWordMsg[0].className = "fl info1 err";
		return false;
	}
	RePassWordMsg.html('');
	RePassWordMsg[0].className = "fl info1 ok";
	return true;
}

function RegFromInit(){
	Email = $('#Email');
	EmailMsg = $("#EmailMsg");
	PassWord = $('#PassWord');
	PassWordMsg = $("#PassWordMsg");
	RePassWord = $("#RePassWord");
	RePassWordMsg = $("#RePassWordMsg");
	ValidateCode = $("#Validate_Code");
	ValidateCodeMsg = $("#ValidateCodeMsg");
	
	/*设置邮箱离开事件*/
	Email.blur(CheckEmail);
	PassWord.blur(CheckPassWord);
	RePassWord.blur(CheckRePassWord);
	ValidateCode.blur(CheckValidateCode);
}

function CheckValidateCode(){
	var ValidateCodeValue = ValidateCode.val();
	if(!utils.IsRegex(ValidateCodeValue,/^[a-zA-Z0-9]{4}$/)){
		ValidateCodeMsg.html('请输入4位验证码！');
		ValidateCodeMsg[0].className = "fl info1 err";
		return false;
	}
	
	$.ajax({
	    type: "GET", url: "/interface/userget.aspx?temptime=" + new Date().toString(), data: "action=CHECK_USER_VALIDATECODE&code="+ValidateCodeValue,
	    errror: function(){ 
			EmailMsg.html("无法检测验证码，请联系系统管理员！");
			EmailMsg[0].className = "fl info1 err";
			return false;
		},
	    success: function(data) {
	        if (!data.state) {
				ValidateCodeMsg.html('您输入的验证码不正确！');
				ValidateCodeMsg[0].className = "fl info1 err";
				return;
	        }else{
				ValidateCodeMsg.html('验证码输入正确！');
				ValidateCodeMsg[0].className = "fl info1 ok";
			}
	    },
	    dataType: "json"
	});
	
	ValidateCodeMsg.html('');
	return true;
}

/*表单提交的处理方法*/
function FromPost(){
	var pt = /^([a-zA-Z0-9\@\.]{2,50})|[\u4e00-\u9fffa-zA-Z0-9\@\.]{2,25}$/;
	var EmailValue = Email.val();
	if(!utils.IsRegex(EmailValue,pt)){
		EmailMsg.html("会员名只能由3到50个英文和数字或2到25个中文组成，不能含空格或特殊符号！");
		EmailMsg[0].className = "fl info1 err";
		return false;
	}
	
	var ValidateCodeValue = ValidateCode.val();
	if(!utils.IsRegex(ValidateCodeValue,/^[a-zA-Z0-9]{4}$/)){
		ValidateCodeMsg.html('请输入4位验证码！');
		ValidateCodeMsg[0].className = "fl info1 err";
		return false;
	}
	
	if(!(CheckPassWord()&&CheckRePassWord())){
		return false;
	}
	
	utils.SystemDo("do","正在发送注册请求...");
	return true;
	
}

//注册会送记录
function RegisterBack(data){
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