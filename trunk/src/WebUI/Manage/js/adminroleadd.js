//--------------

function CheckForm() { 
	if(CheckRoleName()){
	    SetAjaxDivRoot("loader", false, "���ڷ�������...");
	    return true;
	}
	return false;
}

function AddRoleBack(data) {
    if (!data.state) {

        SetAjaxDivRoot("err", false, data.message);
    } else {
        window.parent.adminpop.refinsh();
        SetAjaxDivRoot("ok", false, data.message);
    }
	
}

$(document).ready(function() {
    //����ύ����
    var options = {
    beforeSubmit: CheckForm,
        dataType: 'json',
        success: AddRoleBack
    };
    $("#form1").ajaxForm(options);
});

function CheckRoleName() {
	var vcRoleName = $("#vcRoleName");
	var rnmsg = $("#rnmsg");
	vcRoleName[0].className='itxt1';
	if(vcRoleName.val().length==0){
		rnmsg[0].className="info_err";
		rnmsg.text("��ɫ������Ϊ�գ�");
		return false;
	}
	rnmsg[0].className="info_ok";
	rnmsg.text("��ɫ������ʹ�ã�");
	return true;
}