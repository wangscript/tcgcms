/// <reference path="jquery-1.3.1-vsdoc.js" />

var a = "";
function classTitleInit() {
    var m = $("#classTitle");
    var iParentID = $("#iParentID");
    if (m.length == 0 || iParentID.length == 0) return;

    a = "";
    GetNewsListTitleByClassId(iParentID.val());
    a = "<a href='?iParentId=0&skinid=" + $("#iSkinId").val() + "'>站点根目录</a>>>" + a;
    m.html("<span class='txt bold'>" + a + "</span>");

}

function GetNewsListTitleByClassId(classid) {
    if (_Speciality == null) return;
    for (var i = 0; i < _Speciality.length; i++) {
        if (_Speciality[i].Id == classid) {
            var t = (_Speciality[i].iParent == 0) ? "" : ">>";
            a = t + "<a href='?iParentId=" + _Speciality[i].Id + "&skinid=" + $("#iSkinId").val() + "'>" + _Speciality[i].vcTitle + "</a>" + a;
            GetNewsListTitleByClassId(_Speciality[i].iParent);
        }
    }
}

function AddNewSAction(){
	var iAction=$("#iAction");
	var iParentID=$("#iParentID");
	var AddNewS=$("#AddNewS");
	iAction[0].value="ADD";
	var inTitle=$("#inTitle");
	var inExplain=$("#inExplain");
	var inParentId=$("#inParentId");
	AddNewS[0].className = "list_title_c";
	inTitle[0].value="";
	inExplain[0].value="";
	inParentId[0].value=iParentID[0].value;
}

function CAdd(){
	var AddNewS=$("#AddNewS");
	AddNewS[0].className = "list_title_c hid";
}

var _Speciality = [];
$(document).ready(function () {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: function () { return true; },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

    $.get("../Common/AllNewsSpeciality.aspx?skinid=" + $("#iSkinId").val() + "&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            data = data.substring(4, data.length);
            eval(data);
            classTitleInit();
        });


    SetAjaxDiv("ok", false, "小提示：特性的修改，在列表中就可以完成！");
});


function NewsSADDPostBack(val){
	if(GetErrText(val))return;
	refinsh();
}

function NewsSMDYPostBack(){
    var KeyValue = $("#KeyValue");
    if (KeyValue.length == 0) return;
    $("#form1")[0].removeChild(KeyValue[0]);
    var aValue = KeyValue[0].value;
    var CloseImg = $("#CloseImg");
    if (CloseImg.length != 0) document.body.removeChild(CloseImg[0]);
	SetInnerText(CreateInputobj,aValue);
}

function MdyFeild(obj,vname){
	var iAction=$("#iAction");
	iAction[0].value="MDY";
	var o = $("#iFeildName");
	o[0].value=vname;
	var iMdyID=$("#iMdyID");
	var form=$("#form1");
	iMdyID[0].value=GetCheckColumnCheckID(obj);
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

function NewsSDel() {
    var iAction = $("#iAction");
    var iIds = $("#iIds");
    iAction[0].value = "DEL";
    var temps = GetCheckBoxValues("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "您没选择需要删除的特性！");
        return;
    }
    iIds[0].value = temps;
    $("#form1").submit();
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

function CheckMdyFild() {
    var KeyValue = $("#KeyValue");
    if (KeyValue == null) return;
    if (KeyValue[0].value == "") {
        $("#form1")[0].removeChild(KeyValue);
        var CloseImg = $("#CloseImg");
        if (CloseImg.length != 0) document.body.removeChild(CloseImg[0]);
    } else {
        $("#form1").submit();
    }
}