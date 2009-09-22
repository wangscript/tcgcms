//--------------
var ajax = new AJAXRequest();

var a="";
function classTitleInit(){
	var m=$("classTitle");
	var iSiteId=$("iSiteId");
	if(m==null||iSiteId==null)return;
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(NewsLis[i][0]==iSiteId.value){
			m.innerHTML="<span class='txt bold'>"+NewsLis[i][2]+"</span><span class='info1'>("+NewsLis[i][3]+")</span>";
		}
	}
}

function classTitleInitW(){
	var m=$("classTitleW");
	var iParentID=$("iParentID");
	var iSiteId=$("iSiteId");
	if(m==null||iParentID==null)return;
	if(iParentID.value=="0"){
		m.className="Page_g hid";
	}else{
		a="";
		GetNewsSpecialityTitleByClassId(iSiteId.value,iParentID.value);
		a="<a href='?iSiteId="+iSiteId.value+"'>根特性</a>>>"+a;
		m.innerHTML="<span class='txt'>"+a+"</span>";
	}
}

function GetNewsSpecialityTitleByClassId(siteid,classid){
	if(NewsSpeciality==null)return;
	for(var i=0;i<NewsSpeciality.length;i++){
		if(NewsSpeciality[i][0]==classid&&NewsSpeciality[i][1]==siteid){
			var t=(NewsSpeciality[i][2]==0)?"":">>";
			a =t+"<a href='?iParentId="+NewsSpeciality[i][0]+"&iSiteId="
				+siteid+"'>"+NewsSpeciality[i][3]+"</a>"+a;
			GetNewsSpecialityTitleByClassId(siteid,NewsSpeciality[i][2]);
		}
	}
}

function AddNewSAction(){
	var iAction=$("iAction");
	var iParentID=$("iParentID");
	var AddNewS=$("AddNewS");
	iAction.value="ADD";
	var inTitle=$("inTitle");
	var inExplain=$("inExplain");
	var inParentId=$("inParentId");
	AddNewS.className = "list_title_c";
	inTitle.value="";
	inExplain.value="";
	inParentId.value=iParentID.value;
}

function CAdd(){
	var AddNewS=$("AddNewS");
	AddNewS.className = "list_title_c hid";
}

function CheckForm(){
	ajax.postf($("form1"),function(obj) { NewsSADDPostBack(obj.responseText);});
	return false;
}

function NewsSADDPostBack(val){
	if(GetErrText(val))return;
	refinsh();
}

function NewsSMDYPostBack(val){
	var KeyValue = $("KeyValue");
	if(KeyValue==null)return;
	$("form1").removeChild(KeyValue);
	var aValue=KeyValue.value;
	var CloseImg=$("CloseImg");
	if(CloseImg!=null)document.body.removeChild(CloseImg);
	if(GetErrText(val))return;
	SetInnerText(CreateInputobj,aValue);
}

function MdyFeild(obj,vname){
	var iAction=$("iAction");
	iAction.value="MDY";
	var o = $("iFeildName");
	o.value=vname;
	var iMdyID=$("iMdyID");
	var form=$("form1");
	iMdyID.value=GetCheckColumnCheckID(obj);
	var ci = new CreateInput();
	ci.obj=obj;
	ci.fobj=form;
	ci.Id = "KeyValue";
	bluraction="CheckMdyFild()";
	imgaction="ImgCheck();";
	ci.inputClassName="itxt3";
	ci.Create();
	ci=null;
}

function ImgCheck(){
}

function NewsSDel(){
	var iAction=$("iAction");
	var iIds=$("iIds");
	iAction.value="DEL";
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要删除的特性！");
		return;
	}
	iIds.value=temps;
	ajax.postf($("form1"),function(obj) { NewsSADDPostBack(obj.responseText);});
}

function GetCheckColumnCheckID(obj){
	if(obj==null)return;
	var o = obj.parentNode;
	var os=(document.all)?o.children:o.childNodes
	if(os==null)return "";

	for(var i=0;i<os.length;i++){
		if(os[i].className=="l_check"){
			var oss=(document.all)?os[i].children:os[i].childNodes
			return oss[0].value;
		}
	}
	var oo=o.parentNode;
	if(oo==null)return "";
	os=(document.all)?oo.children:oo.childNodes
	for(var i=0;i<os.length;i++){
		if(os[i].className=="l_check"){
			var oss=(document.all)?os[i].children:os[i].childNodes
			return oss[0].value;
		}
	}
	return "";
}

function CheckMdyFild(){
	var KeyValue = $("KeyValue");
	if(KeyValue==null)return;
	if(KeyValue.value==""){
		$("form1").removeChild(KeyValue);
		var CloseImg=$("CloseImg");
		if(CloseImg!=null)document.body.removeChild(CloseImg);
	}else{
		ajax.postf($("form1"),function(obj) { NewsSMDYPostBack(obj.responseText);});
	}
}