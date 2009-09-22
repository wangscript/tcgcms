//--------------
var ajax = new AJAXRequest();
var GroupDiv = new MenuDiv();
var UploadFile = new UploadFile();

function CAdd(){
	var AddFileClass=$("AddFileClass");
	AddFileClass.className="list_title_c hid";
}

function fileclassTitleInit(){
	var iClassId =$("iClassId");
	if(iClassId.value==0){
		$("classTitle").innerHTML = "<span class='bold'>文件系统根目录</span>";
	}else{
		$("classTitle").innerHTML = "<span class='bold'><a href='?'>文件系统根目录</a></span> >>" + GetTitleListByID(iClassId.value);
	}
}

function GetTitleListByID(ID){
	for(var i=0;i<AllFileClass.length;i++){
		if(AllFileClass[i][0]==ID){
			var t = GetTitleListByID(AllFileClass[i][1]);
			var tt =(t=="")?"":" >> ";
			return t + tt + "<span><a href='?iClassId="+AllFileClass[i][0]+"' >"+AllFileClass[i][2]+"</a></span>";
		}
	}
	return "";
}

function AddClassS(){
	var AddFileClass=$("AddFileClass");
	$("inTitle").value="";
	$("inInfo").value="";
	$("work").value="AddClass"
	AddFileClass.className="list_title_c";
	GroupDiv.HidMenuDiv();
}

function CheckFrom(){
	if($("inTitle").value==''||$("inInfo").value==''){
		alert("请输入完整信息！");
		return false;
	}
	ajax.postf($("form1"),function(obj) { AddClassBack(obj.responseText);});
	return false;
}

function AddClassBack(val){
	if(GetErrText(val))return;
	refinsh();
}

function AddFiles(){
	var c=$("iClassId").value;
	if(c==0){
		return;
	}
	GroupDiv.HidMenuDiv();
	UploadFile.Default={w:(document.all==null)?-189:-193,h:(document.all==null)?-197:-194}
	UploadFile.ClassId=c;
	UploadFile.CallBack="UpdateBack";
	UploadFile.MaxNum=5;
	UploadFile.Start();
}

function ShowFilesCreate(obj){
	var addlist = [{href:"javascript:GoTo();",onclick:"AddClassS();",Text:"增加新文件夹"},{href:"javascript:GoTo();",onclick:"AddFiles();",Text:"增加文件"}];
	GroupDiv.CreadDiv("AddS",obj,addlist,100,null,0,0);
}

function MoveFiles(obj){
	var addlist = [{href:"javascript:GoTo();",onclick:"AddClassS();",Text:"增加新文件夹"},{href:"javascript:GoTo();",onclick:"AddFiles();",Text:"增加文件"}];
	GroupDiv.CreadDiv("MoveS",obj,addlist,100,null,0,0);
}

function UpdateBack(obj){
	refinsh();
}