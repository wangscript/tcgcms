
function RealDel(){
	var admins = GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		SetAjaxDiv("err",false,"您还没选择需要删除的管理员!");
		return;
	}
	$("#admins").val(admins);
	$("#saction").val("02");
	if (!confirm("您确定彻底删除管理员[" + admins + "]?")) return;

	SetAjaxDiv("loader", false, "正在发送删除[" + admins + "]的请求...");
	$("#form1").submit();

}

$(document).ready(function () {
    //添加提交方法
    var options = {
        beforeSubmit: function () { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    $("#form1").ajaxForm(options);
});

function SaveAdmins(){
	var admins = GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		SetAjaxDiv("err",false,"您还没选择需要救回的管理员!");
		return;
	}
	$("#admins").val(admins);
	$("#saction").val("03");
	if(!confirm("您确定救回管理员["+admins+"]?"))return;
	SetAjaxDiv("loader",false,"正在发送回收["+admins+"]的请求...");
	$("#form1").submit();
}

