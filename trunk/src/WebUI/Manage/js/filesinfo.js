/// <reference path="jquery-1.3.1-vsdoc.js" />

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

function GetTitleListByID(ID) {
    if (_FileCategories == null) return;
    for (var i = 0; i < _FileCategories.length; i++) {
        if (_FileCategories[i].Id == ID) {
            var t = GetTitleListByID(_FileCategories[i].iParentId);
			var tt =(t=="")?"":" >> ";
			return t + tt + "<span><a href='?iClassId=" + _FileCategories[i].Id + "' >" + _FileCategories[i].vcFileName + "</a></span>";
		}
	}
	return "";
}

function CreateCatge() {
    if ($("#inTitle").val() == '' || $("#inInfo").val() == '' || $("#iSize").val() == "") {
        alert("请输入完整信息！");
        return false;
    }
    $("#form1").submit();
}

function AddClassBack(val){
	if(GetErrText(val))return;
	refinsh();
}

function AddFiles() {
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
    var AddFileClass = $("#AddFileClass");
    $("#inTitle").val("");
    $("#inInfo").val("");
    $("#work").val("AddClass");
    AddFileClass.addClass("list_title_c").show();
}

function MoveFiles(obj){
	var addlist = [{href:"javascript:GoTo();",onclick:"AddClassS();",Text:"增加新文件夹"},{href:"javascript:GoTo();",onclick:"AddFiles();",Text:"增加文件"}];
	GroupDiv.CreadDiv("MoveS",obj,addlist,100,null,0,0);
}

function UpdateBack(obj){
	refinsh();
}


$(document).ready(function() {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: function() { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);
});