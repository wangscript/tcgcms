
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
	var options = {
	    beforeSubmit: function () { return true; },
	    dataType: 'json',
	    success: AjaxPostFormBack
	};
	$("#form1").ajaxForm(options);
	$("#form1").submit();

}

function SaveAdmins(){
	var admins = GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		SetAjaxDiv("err",false,"您还没选择需要救回的管理员!");
		return;
	}
	$("admins").value=admins;
	$("saction").value="03";
	if(!confirm("您确定救回管理员["+admins+"]?"))return;
	SetAjaxDiv("loader",false,"正在发送回收["+admins+"]的请求...");
	ajax.postf($("form1"),function(obj) { SaveAdminsBack(obj.responseText);});
}

function DelAdminsBack(val) {
    if (!data.state) {
        var errText = $("#errText");
        errText[0].className = "errTextW";
        errText.html(data.message);
        return;
    }

	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	window.location.href=window.location.href;
}

function SaveAdminsBack(val){
	if(GetErrText(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	window.location.href=window.location.href;
}