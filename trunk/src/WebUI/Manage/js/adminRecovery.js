
function RealDel(){
	var admins = GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		SetAjaxDiv("err",false,"����ûѡ����Ҫɾ���Ĺ���Ա!");
		return;
	}
	$("#admins").val(admins);
	$("#saction").val("02");
	if (!confirm("��ȷ������ɾ������Ա[" + admins + "]?")) return;

	SetAjaxDiv("loader", false, "���ڷ���ɾ��[" + admins + "]������...");
	$("#form1").submit();

}

$(document).ready(function () {
    //����ύ����
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
		SetAjaxDiv("err",false,"����ûѡ����Ҫ�ȻصĹ���Ա!");
		return;
	}
	$("#admins").val(admins);
	$("#saction").val("03");
	if(!confirm("��ȷ���Ȼع���Ա["+admins+"]?"))return;
	SetAjaxDiv("loader",false,"���ڷ��ͻ���["+admins+"]������...");
	$("#form1").submit();
}

