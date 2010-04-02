//--------------


function FromPost() {
    var errText = $("#errText");
    errText[0].className = "errText";
    SetAjaxLoad(errText[0],"正在提交数据...");

    var adminname = $("#username");
    var password = $("#password");
    var radminName = $("#rUsername");

    if (adminname.val() == "") {
        errText.html("请输入用户名！");
        errText[0].className = "errTextW";
        adminname.focus();
        return false;
    }

    if (password.val() == "") {
        errText.html("请输入登陆密码！");
        errText[0].className = "errTextW";
        password.focus();
        return false;
    }
    return true;
}

function SetAjaxLoad(obj, txt) {
    obj.innerHTML = "<table width=\"98%\" height=\"16\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">"
	  			+ "<tr>"
	    + "<td width='1%'><img src='images/ajax-loadersmall.gif' /></td>"
        + "<td width='99%' align=\"left\" class=\"lodertxt\">&nbsp;&nbsp;" + txt + "</tr>"
	+ "</table>";
}

function LoginCkBack(data) {
    
    if(!data.state){
        var errText = $("#errText");
        errText[0].className ="errTextW";
        errText.html(data.message);
	    return;
	}
	top.location.href = "main.aspx";
}

$(document).ready(function() {
    //添加提交方法
    var options = {
        beforeSubmit: FromPost,
        dataType: 'json',
        success: LoginCkBack
    };
    $("#form1").ajaxForm(options);
});

//表单重置
function oncunle() {
    var errText = $("#errText");
    errText.html("");
    errText[0].className = "errText";
    return true;
}



function updatePost() {
    var SqlSep = $("#SqlSep");
    var st = parseInt(SqlSep.val()) + 1;
    SqlSep.val(st);
    return true;
}

function updateback(data) {
    var tbupdate = $("#tbupdate");
    var tx = (tbupdate.html() == "") ? "" : "<br/>";
    if (!data.state) {
        tbupdate.html(tbupdate.html() + tx + data.message);
        $("#form1").submit();
    } else {
        tbupdate.html(tbupdate.html() + tx + data.message);
    }
}