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
	ajax.postf($("form1"),function(obj) { AddClassPostBack(obj.responseText);});
	return false;
}
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
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.postf($("form1"),function(obj) { EditClassPostBack(obj.responseText);});
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