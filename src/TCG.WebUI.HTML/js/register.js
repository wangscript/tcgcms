
$(document).ready(function() {
	var RegisterForm = $("#Register");
	RegFromInit();
    //添加提交方法
    var options = {
        beforeSubmit: FromPost,
        dataType: 'json',
        success: RegisterBack
    };
    RegisterForm.ajaxForm(options);
});

function RegFromInit(){
	
	
	var obj = $("<div class=\"systemdo\" id=\"systemdo\"></div>")
	var mybody = $('body');
	mybody.append(obj);
	obj.html("正在提交注册请求.....")
	obj.css({top:(window.screen.availHeight-obj.height())/2+"px",left:(window.screen.availWidth-obj.width())/2+"px"})	
}

//表单提交的处理方法
function FromPost(){
	
	return false;
}

//注册会送记录
function RegisterBack(data){
	
}