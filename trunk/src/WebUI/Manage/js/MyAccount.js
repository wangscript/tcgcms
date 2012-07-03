/// <reference path="jquery-1.3.1-vsdoc.js" />


function CheckForm(){
	var iOldPWD = $("#iOldPWD");
	var iNewPWD = $("#iNewPWD")
	var iCPWD = $("#iCPWD");
	var iNickName = $("#iNickName");
	var nnmsg = $("#nnmsg");
	var post = $("#post");
	
	if(iNickName.val()==""){
		nnmsg.get(0).className = "info_err";
		SetInnerText(nnmsg,"昵称不能为空!");
	}
	
	if(!(CheckCPWD(iCPWD))){
		return false;
	}
	CheckPasswordFrom(iOldPWD);
	
}

function ChanagePostBack(val){
	if(GetErrTextRoot(val))return;
	SetAjaxDivRoot("ok",false,"用户登陆信息更改成功！");
}

function CheckPassword(obj) {
    obj = $(obj);
    if (obj == null && obj.length == 0) return false;
    
	obj.get(0).className='itxt1';
	var og = $("#pwdmsg");
	if(og==null)return false;
	if(obj.val()== ""){
	    og.get(0).className = "info_err";
		SetInnerText(og,"原始密码不能为空!");
		return false;
	}
	$.ajax({
	    type: "GET", url: "AjaxMethod/Admin_CheckPwd.aspx?temptime=" + new Date().toString(), data: "PWD=" + obj.val(),
	    errror: function() { alert("err"); },
	    success: function(data) {
	        //debugger;
	        if (!data.state) {
	            og.get(0).className = "info_err";
	            return false;
	        } else {
	            og.get(0).className = "info_ok";
	        }
	        SetInnerText(og, data.message);
	    },
	    dataType: "json"
	});

	return true;
}

function CheckPasswordFrom(obj) {
    obj = $(obj);
    if (obj == null && obj.length == 0) return false;

    obj.get(0).className = 'itxt1';
    var og = $("#pwdmsg");
    if (og == null) return false;
    if (obj.val() == "") {
        og.get(0).className = "info_err";
        SetInnerText(og, "原始密码不能为空!");
        return false;
    }

    $.ajax({
        type: "GET", url: "AjaxMethod/Admin_CheckPwd.aspx?temptime=" + new Date().toString(), data: "PWD=" + obj.val(),
        errror: function() { alert("err"); },
        success: function(data) {
            //debugger;
            if (!data.state) {
                og.get(0).className = "info_err";
                return false;
            } else {
                og.get(0).className = "info_ok";

                if (CheckNewPassword($("#iNewPWD")) && CheckCPWD($("#iCPWD"))) {
                    $("#form1").submit();
                }
            }
            SetInnerText(og, data.message);
        },
        dataType: "json"
    });
}


$(document).ready(function() {

    var form1 = $("#form1");
    var options;
    options = {
        beforeSubmit: function() { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

});
