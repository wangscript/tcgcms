//--------------


function CheckAddClassForm(){
	var iClassId=$("iClassId");
	if(iClassId.value=="0"){
		if(!(CheckValueIsNull('iClassName','cnamemsg')&&CheckValueIsNull('iName','inamemsg'))){
			return false;
		}
	}else{
		if(!(CheckValueIsNull('iClassName','cnamemsg')&&CheckValueIsNull('iName','inamemsg')
			&&CheckValueIsNull('iDirectory','dirmsg')&&CheckValueIsNull('iUrl','urlmsg')&&
			CheckTemplate('sTemplate','stdmsg')&&CheckTemplate('slTemplate','stsdmsg'))){
			return false;
		}
	}
	SetAjaxDiv("loader",false,"正在发送请求...");
	return false;
}

$(document).ready(function() {
    var options;
    if ($("#Work").length != 0) {
        options = {
            beforeSubmit: CheckEditClassForm,
            dataType: 'json',
            success: EditClassPostBack
        };

    } else {
        options = {
            beforeSubmit: CheckAddClassForm,
            dataType: 'json',
            success: AddClassPostBack
        };
    }
    $("#form1").ajaxForm(options);
});


function CheckEditClassForm(){
	var iClassId=$("iClassId");
	if(iClassId.value=="0"){
		if(!(CheckValueIsNull('iClassName','cnamemsg')&&CheckValueIsNull('iName','inamemsg'))){
			return false;
		}
	}else{
		if(!(CheckValueIsNull('iClassName','cnamemsg')&&CheckValueIsNull('iName','inamemsg')
			&&CheckValueIsNull('iDirectory','dirmsg')&&CheckValueIsNull('iUrl','urlmsg')&&
			CheckTemplate('sTemplate','stdmsg')&&CheckTemplate('slTemplate','stsdmsg'))){
			return false;
		}
	}
	SetAjaxDiv("loader", false, "正在发送请求...");
	return false;
}

function EditClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"资讯分类修改成功！");
}

function AddClassPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"资讯分类添加成功！");
}

function CheckTemplate(on,jn){
	var o=$(on);
	var j=$(jn);
	if(o.value=="-1"){
		j.className="info_err";
		return false;
	}else{
		j.className="info_ok";
		return true;
	}
}