
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
	var Email = $('#Email');
	var PassWord = $('#PassWord');
	Email.blur(function(){
		
	});
	
	PassWord.blur(function(){
						   
	});

}

//表单提交的处理方法
function FromPost(){
	
	return false;
}

//注册会送记录
function RegisterBack(data){
	
}