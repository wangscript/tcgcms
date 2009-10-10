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
	    + "<td width='1%'><img src='/manage/images/ajax-loadersmall.gif' /></td>"
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

	$.ajax({
	    type: "GET", url: "/manage/Version/Ver.aspx?temptime=" + new Date().toString(), data: "action=IsHaveHigherVer",
	    errror: function() { top.location.href = "main.aspx"; },
	    success: function(data) {
	        if (data.state) {
	            var errText = $("#errText");
	            $("#tblogin").css({ "display": "none" });
	            var tbupdate = $("#tbupdate");
	            var btn_ok = $("#btn_ok");
	            var btn_ret = $("#btn_ret");

	            errText.html("检测到新的版本 TCG CMS System Version " + data.version + "<br/> <span class='STYLE1'><a class='STYLE1 bold' href='"
	                            + data.logurl + "' target='_blank'>查看详细信息</a></span>");
	            errText[0].className = "errTextW";
	            btn_ret.val("跳过");
	            btn_ok.val("升级");
	            tbupdate.css({ "display": "block" });

	            //设置表单提交
	            var options = {
	                beforeSubmit: updatePost,
	                dataType: 'json',
	                success: updateback
	            };
	            $("#form1").ajaxForm(options);

	            btn_ok.click(function() {
	                var SqlSep = $("#SqlSep");
	                if (confirm("更新所造成的数据丢失，概不负责，继续更新，请确认！")) {
	                    $("#tbupdate").html("");
	                    $("#Work").val("UPDATE");
	                    SqlSep.val("1");
	                    return true;
	                }
	                return false;
	            });
	            btn_ret.click(function() { top.location.href = "main.aspx"; });
	            return;
	        }
	        top.location.href = "main.aspx";
	    },
	    dataType: "json"
	});
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