//--------------
var ajax = new AJAXRequest();
var GroupDiv = new MenuDiv();
var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};

var a="";
function classTitleInit(){
	var m=$("classTitle");
	var classobj=$("iClassId");
	if(m==null||classobj==null)return;
	//if(classobj.value=="0"){
		//m.innerHTML="<span class='txt bold lfl'>网站名称</span><span class='info1'>(资讯站点根目录)</span>";
	//}else{
	    a = "";
	   
		GetNewsListTitleByClassId(classobj.value);
		m.innerHTML = "<span class='txt bold lfl'><a href='newslist.aspx'>所有资讯</a>>>"+a+"</span>" + m.innerHTML;
	//}
}

function ShowClassTitle(obj){
	var m=$("ChildclassTitle");
	var pos = getAbsolutePositionXY(obj);
	m.style.top = (pos.y) + "px";
	m.style.left = (pos.x+22) + "px";
	m.className="ChildclassTitle";
}

function HidClassTitle(){
	var m=$("ChildclassTitle");
	m.className="ChildclassTitle hid";
}

function childClassTitleInit(){
	var m=$("ChildclassTitle");
	var classobj=$("iClassId");
	a="";
	for(var i=0;i<NewsLis.length;i++){
		if(NewsLis[i][1]==classobj.value){
			a+=" <a href='?iClassId="+NewsLis[i][0]+"' class='childnewstitle bold'>"+NewsLis[i][2]+"</a>";
		}
	}
	m.innerHTML=a;
}

function GetNewsListTitleByClassId(classid){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(NewsLis[i][0]==classid){
			var t=(NewsLis[i][1]==0)?"":" >>";
			a =t+" <a href='?iClassId="+NewsLis[i][0]+"'>"+NewsLis[i][2]+"</a>"+a;
			GetNewsListTitleByClassId(NewsLis[i][1]);
		}
	}
}

function EditNewsInfo(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要编辑的新闻！");
		return;
	}
	if(temps.indexOf(",")>-1){
		SetAjaxDiv("err",false,"请保证您只选择了一个资源！");
		return;
	}
	window.location.href="newsadd.aspx?newsid="+temps;
}

function ShowClassNameByClassID(id){
	if(NewsLis==null)return;
	var o = GetNewsItemById(id);
	if(o==null)return;
	document.write("<a href=\"newslist.aspx?iclassid="+o[0]+"\">" + o[2] + "</a>");	
}

function AddNewsInfo(){
	window.location.href="newsAdd.aspx?iClassId="+$("iClassId").value;
}

function CreateNews(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要生成的资讯！");
		return;
	}
	CreateDiv.Start("批量生成文件");
	if(temps.indexOf(",")>-1){
		var ts = temps.split(",");
		CreateDiv.set =1;
		CreateDiv.setcount=ts.length;
		for(var i=0;i<ts.length;i++){
			if(ts[i]=="")continue;
			$("iAction").value="CREATE";
			$("DelClassId").value=ts[i];
			SetAjaxDiv("loader",false,"正在发送删除请求...");
			ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
		}
	}else{
		CreateDiv.set =1;
		CreateDiv.setcount=1;
		$("iAction").value="CREATE";
		$("DelClassId").value=temps;
		SetAjaxDiv("loader",false,"正在发送删除请求...");
		ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
	}
	SetAjaxDiv("ok",true,"请求发送成功");
}

function CreateBack(val){
	if(GetErrText(val))return;
	CreateDiv.SetSep(val);
}


function SearchAll(){
	var iClassId = $("iClassId");
	window.location.href="?iClassId="+iClassId.value;
}

function SearchChecked(num){
	var iClassId = $("iClassId");
	window.location.href="?iClassId="+iClassId.value+"&check="+num;
}

function SearchCreated(num){
	var iClassId = $("iClassId");
	window.location.href="?iClassId="+iClassId.value+"&create="+num;
}

function SearchBQ(obj){
	var iClassId=$("iClassId");
	var op =GetNewsSiteByClassId(iClassId.value);
	if(op==null)return;
	var o=GetSpecialItems(op[0]);
	GroupDiv.HidAuto = false;
	GroupDiv.CreadDiv("Speciality",obj,o,150,null,0,0);
	var Speciality = $("Speciality");
	var pos = getAbsolutePositionXY(obj);
	Speciality.style.top = (pos.y - Speciality.offsetHeight) + "px";
}

function HidSChild(classid){
	var id="Speciality_"+classid;
	GroupDiv.HidMenuItem(id);
}

function ShowSChild(obj,id){
	var sid="Speciality_"+id;
	var o=obj.parentNode;
	o.className="topmenu";
	GroupDiv.DefZIndex++;
	var al = GetSpecialItemsW(id);
	if(al.length>0){
		GroupDiv.HidAuto = false;
		GroupDiv.CreadDiv(sid,obj,al,150,null,0,0,"right");
	}
}

function SelSpeciality(id){
	var iClassId = $("iClassId");
	window.location.href="?iClassId="+iClassId.value+"&Speciality="+id;
}

function GetSpecialItems(id){
	var iSpeciality=$("iSpeciality");
	var sels=false;
	var o =[];
	var n=0;
	for(var i=0;i<NewsSpeciality.length;i++){
		sels=CharInStr(iSpeciality.value,NewsSpeciality[i][0])?true:false;
		if(id==NewsSpeciality[i][1]&&NewsSpeciality[i][2]==0){
			o[n]={href:"javascript:GoTo();",onclick:"SelSpeciality("+NewsSpeciality[i][0]+")",Text:NewsSpeciality[i][3],onmouseover:"ShowSChild(this,"+NewsSpeciality[i][0]+")",onmouseout:"HidSChild("+NewsSpeciality[i][0]+")",Sel:sels};
			n++;
		}
	}
	return o
}

function GetSpecialItemsW(id){
	var iSpeciality=$("iSpeciality");
	var sels=false;
	var o =[];
	var n=0;
	for(var i=0;i<NewsSpeciality.length;i++){
		sels=CharInStr(iSpeciality.value,NewsSpeciality[i][0])?true:false;
		if(id==NewsSpeciality[i][2]){
			o[n]={href:"javascript:GoTo();",onclick:"SelSpeciality("+NewsSpeciality[i][0]+")",Text:NewsSpeciality[i][3],onmouseover:"ShowSChild(this,"+NewsSpeciality[i][0]+")",onmouseout:"HidSChild("+NewsSpeciality[i][0]+")",Sel:sels};
			n++;
		}
	}
	return o
}

function NewsDel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要删除的资讯！");
		return;
	}
	$("iAction").value="DEL";
	$("DelClassId").value=temps;
	if(confirm("您确定删除资讯["+temps+"]")){
		SetAjaxDiv("loader",false,"正在发送删除请求...");
		ajax.postf($("form1"),function(obj) { FalsDelBack(obj.responseText);});
	}
}

function SaveNews(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要救回的资讯！");
		return;
	}
	$("iAction").value="SAVE";
	$("DelClassId").value=temps;
	SetAjaxDiv("loader",false,"正在发送删除请求...");
	ajax.postf($("form1"),function(obj) { FalsDelBack(obj.responseText);});
}
function realdel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要彻底删除的资讯！");
		return;
	}
	$("iAction").value="DEL";
	$("DelClassId").value=temps;
	SetAjaxDiv("loader",false,"正在发送删除请求...");
	ajax.postf($("form1"),function(obj) { FalsDelBack(obj.responseText);});
}

function FalsDelBack(val){
	if(GetErrText(val))return;
	refinsh();
}