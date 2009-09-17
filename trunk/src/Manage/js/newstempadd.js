//--------------
var ajax = new AJAXRequest();

function classTitleInit(){
	var m=$("sSite");
	var classobj=$("iSiteId");
	if(m==null||classobj==null)return;
	if(classobj.value!="0"){
		SetInnerText(m,GetClassTitleById(classobj.value));
	}
}

function GetClassTitleById(id){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(id==NewsLis[i][0]){
			return NewsLis[i][2]+"("+NewsLis[i][3]+")";
		}
	}
	return "";
}

function CheckForm(){
	if(!(CheckContent()&&CheckUrl()&&CheckTempName()&&CheckType())){
		return false;
	}
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.postf($("form1"),function(obj) { AddPostBack(obj.responseText);});
	return false;
}

function CheckMdyForm(){
	if(!(CheckContent()&&CheckUrl()&&CheckTempName()&&CheckType())){
		return false;
	}
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.postf($("form1"),function(obj) { MdyPostBack(obj.responseText);});
	return false;
}

function MdyPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"模板修改成功！");
}

function AddPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"模板添加成功！");
}

function CheckContent(){
	var obj=$("vcContent");
	var n=$("conmsg");
	if(obj.value==""){
		n.className="info_err";
		SetInnerText(n,"模板内容不能为空");
		return false;
	}else{
		n.className="info_ok";
		SetInnerText(n,"");
		return true;
	}
}

function CheckUrl(){
	var obj=$("vcUrl");
	var n=$("urlmsg");
	var o=$("tType");
	obj.className='itxt1';
	if(o.value=="0"){
		if(obj.value==""){
			n.className="info_err";
			SetInnerText(n,"单页时地址不能为空");
			return false;
		}else{
			n.className="info_ok";
			SetInnerText(n,"可以使用！");
			return true;
		}
	}else{
		obj.value="";
		n.className="info_ok";
		SetInnerText(n,"非单页地址不需要！");
	}
	return true;
}
function CheckTempName(){
	var obj=$("vcTempName");
	var n=$("tnmsg");
	obj.className='itxt1';
	if(obj.value==""){
		n.className="info_err";
		SetInnerText(n,"模板名称不能为空");
		return false;
	}else{
		n.className="info_ok";
		SetInnerText(n,"资讯模版的名称,可以使用！");
		return true;
	}
}

function CheckType(){
	var obj=$("tType");
	var n=$("typemsg");
	if(obj.value=="-1"){
		n.className="info_err";
		SetInnerText(n,"请选择模版类别！");
		return false;
	}else{
		n.className="info_ok";
		SetInnerText(n,"模版类别,可以使用！");
		return true;
	}
}