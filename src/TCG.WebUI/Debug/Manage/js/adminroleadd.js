//--------------

function CheckForm() { 
	if(CheckRoleName()){
	    SetAjaxDivRoot("loader", false, "正在发送请求...");
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
    //添加提交方法
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
		rnmsg.text("角色名不能为空！");
		return false;
	}
	rnmsg[0].className="info_ok";
	rnmsg.text("角色名可以使用！");
	return true;
}