﻿/// <reference path="jquery-1.3.1-vsdoc.js" />

var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};

var a="";
function classTitleInit() {
    var m = $("#classTitle");
    var classobj = $("#iClassId");
    if (m == null || classobj == null) return;
    a = "";

    GetNewsListTitleByClassId(classobj.val());
    m.html("<span class='txt bold lfl'><a href='?iClassId=0&skinid=" + $("#iSkinId").val() + "'>所有资讯</a>>>" + a + "</span>" + m.html());

}

function ShowClassTitle(obj) {
	var m=$("#ChildclassTitle");
	var pos = getAbsolutePositionXY(obj);
	m.css({"top":(pos.y) + "px"});
	m.css({ "left": (pos.x + 22) + "px" });
	m.addClass("ChildclassTitle").removeClass("hid");
}

function HidClassTitle() {
    var m = $("#ChildclassTitle");
    m.addClass("hid");
}

function childClassTitleInit() {
    var skinid = $("#iSkinId").val();
    var m = $("#ChildclassTitle");
    var classobj = $("#iClassId");
    a = "";
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].ParentId == classobj.val() && _Categories[i].Skin.Id == skinid) {
            a += " <a href='?iClassId=" + _Categories[i].Id + "&skinid=" + $("#iSkinId").val() + "' class='childnewstitle bold'>" + _Categories[i].ClassName + "</a>";
        }
    }
    m.html(a);
}

function GetNewsListTitleByClassId(classid) {
    var skinid = $("#iSkinId").val();
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].Id == classid && _Categories[i].Skin.Id == skinid) {
            var t = (_Categories[i].ParentId == 0) ? "" : " >>";
            a = t + " <a href='?iClassId=" + _Categories[i].Id + "&skinid=" + $("#iSkinId").val() + "'>" + _Categories[i].ClassName + "</a>" + a;
            GetNewsListTitleByClassId(_Categories[i].ParentId);
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
	window.location.href = "resourceshandlers.aspx?newsid=" + temps;
}

function ShowClassNameByClassID(id){
	var o = GetCategorieById(id);
	if(o==null)return;
	document.write("<a href=\"?iclassid="+o.Id+"\">" + o.ClassName + "</a>");	
}

function AddNewsInfo(){
    window.location.href = "resourceshandlers.aspx?iClassId=" + $("#iClassId").val();
}

function CreateNews(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要生成的资讯！");
		return;
	}
	CreateDiv.Start("批量生成文件");
	layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: function() { } });
	
	if(temps.indexOf(",")>-1){
		var ts = temps.split(",");
		CreateDiv.set =1;
		CreateDiv.setcount=ts.length;
		for(var i=0;i<ts.length;i++){
			if(ts[i]=="")continue;
			$("#iAction").val("CREATE");
			$("#DelClassId").val(ts[i]);
			$('#form1').submit();
		}
	}else{
		CreateDiv.set =1;
		CreateDiv.setcount=1;
		$("#iAction").val("CREATE");
		$("#DelClassId").val(temps);
		$('#form1').submit();
	}
}

function CheckNews() {
    var temps = GetCheckBoxValues("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "您没选择需要审核的资讯！");
        return;
    }
    CreateDiv.Start("批量审核文件");
    layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: function () { } });

    if (temps.indexOf(",") > -1) {
        var ts = temps.split(",");
        CreateDiv.set = 1;
        CreateDiv.setcount = ts.length;
        for (var i = 0; i < ts.length; i++) {
            if (ts[i] == "") continue;
            $("#iAction").val("CHECK");
            $("#DelClassId").val(ts[i]);
            $('#form1').submit();
        }
    } else {
        CreateDiv.set = 1;
        CreateDiv.setcount = 1;
        $("#iAction").val("CHECK");
        $("#DelClassId").val(temps);
        $('#form1').submit();
    }
}

function CreateBack(val) {
    CreateDiv.SetSep(val);
    if (!CreateDiv.runing) {
        CreateDiv.Start("正在生成分类列表...");
        CreateDiv.set = 1;
        CreateDiv.setcount = 1;
        $("#iAction").val("CREATECATEGRI");
        $("#DelClassId").val($("#iClassId").val());
        $('#form1').submit();
    }
}

function CreateBack1(val) {
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
	$("#iAction").val("DEL");
	$("#DelClassId").val(temps);
	if(confirm("您确定删除资讯["+temps+"]")){
	    SetAjaxDiv("loader", false, "正在发送删除请求...");
	    $("#form1").submit();
	}
}

function SaveNews(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要救回的资讯！");
		return;
	}
	$("#iAction").val("SAVE");
	$("#DelClassId").val(temps);
	SetAjaxDiv("loader", false, "正在发送删除请求...");
	$("#form1").submit();
}

function realdel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要彻底删除的资讯！");
		return;
	}
	$("#iAction").val("DEL");
	$("#DelClassId").val(temps);
	SetAjaxDiv("loader",false,"正在发送删除请求...");
	$("#form1").submit();
}

function FalsDelBack(val){
	if(GetErrText(val))return;
	refinsh();
}


$(document).ready(function() {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: BeforSubmit,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);
});