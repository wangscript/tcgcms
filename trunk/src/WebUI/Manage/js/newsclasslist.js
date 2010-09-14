/// <reference path="jquery-1.3.1-vsdoc.js" />

var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};


var a="";
function classTitleInit() {
    var m = $("#classTitle");
    var classobj = $("#iClassId");
    if (m == null || classobj == null) return;
   
        a = "";
        GetNewsListTitleByClassId(classobj.val());
        a = "<a href='?iParentId=0&skinid=" + $("#iSkinId").val() + "'>站点根目录</a>>>" + a;
        m.html("<span class='txt bold'>" + a + "</span>");
   
}

function GetNewsListTitleByClassId(classid) {
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].Id == classid) {
            var t = (_Categories[i].ParentId == "0") ? "" : ">>";
            a = t + "<a href='?iParentId=" + _Categories[i].Id + "&skinid=" + $("#iSkinId").val() + "'>" + _Categories[i].ClassName + "</a>"
				+ "<span class='info2'>(" + _Categories[i].Name + ")</span>" + a;
            GetNewsListTitleByClassId(_Categories[i].ParentId);
		}
	}
}

function GetNewsListTitleByClassIdW(classid){
    if (_Categories == null) return;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories.ParentId == classid) {
            var t = (_Categories.ParentId == 0) ? "" : ">>";
            a = t + _Categories.ClassName + a;
            GetNewsListTitleByClassIdW(_Categories.Name);
		}
	}
}


function CreatClass(obj){
    layer.openLayer({ id: 'layerbox1', width: 600, height: 369, callBack: operback });
    SetCreateInnerHTML();
}

function operback() { 
    
}

function EditClass(obj) {
    layer.openLayer({ id: 'layerbox1', width: 600, height: 369, callBack: operback });
    SetMdyInnerHTML();
}

function SetMdyInnerHTML(){
	var ifCreateAdd=$("#ifCreateAdd");
	ifCreateAdd.css({ "width": "100%" });
	ifCreateAdd.css({ "height": "100%" });
	ifCreateAdd.get(0).src = "categoriesmdy.aspx?iClassId=" + GetCheckBoxValues("CheckID") + "&skinid=" + $("#iSkinId").val();
}

function SetCreateInnerHTML() {
	var ifCreateAdd=$("#ifCreateAdd");
	var CreateClassDivClose=$("#CreateClassDivClose");
	var par=$("#iClassId");
	ifCreateAdd.css({"width":"100%"});
	ifCreateAdd.css({ "height": "100%" });
	ifCreateAdd.get(0).src = "Categoriesadd.aspx?iParentId=" + par.val() + "&skinid=" + $("#iSkinId").val();
}

function CreatClassClose(){
	var movediv=null;
	var CBackg=$("CBackg");
	if(DivMoves.length==0)return;
	CBackg.className="CBackg hid";
	movediv=DivMoves[0];
	movediv.CloseDiv("SetCloseBack()");
}

function SetCloseBack(){
	var co=$("CreateClassDiv");
	var CreateClassDivClose=$("CreateClassDivClose");
	co.style.borderWidth="0px";
	CreateClassDivClose.className="CreateClassDivClose hid";
	var ifCreateAdd=$("ifCreateAdd");
	ifCreateAdd.style.width="0px";
	ifCreateAdd.style.height="0px";
	if(DivMoves.length==0)return;
	DivMoves[0]=null;
	DivMoves.length=0;
	refinsh();
}

function NewsClassDel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要删除的分类！");
		return;
	}
	if(temps.indexOf(",")>-1){
		SetAjaxDiv("err",false,"一次只能删除一个模版！");
		return;
	}
	$("#DelClassId").val(temps);
	$("#work").val("DEL");
	SetAjaxDiv("loader",false,"正在发送请求...");
	$("#form1").submit();
}

function DelPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"分类已经删除成功！");
}

function NewsClassCreateHtml() {
    var temps = GetCheckBoxValues("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "您没有选择需要生成的分类！");
        return;
    }

    CreateDiv.Start("批量生成分类列表");
    layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: operback });

    if (temps.indexOf(",") > -1) {
        var ts = temps.split(",");
        var ss = "";
        for (var i = 0; i < ts.length; i++) {
            var tt = GetPostClassChild(ts[i]);
            ss += (ss == "") ? tt : "," + tt;
        }
        PostClasses(ss);
    } else {
        var t = GetPostClassChild(temps);
        if (t.indexOf(",") > -1) {
            PostClasses(t);
        } else {
            CreateDiv.set = 1;
            CreateDiv.setcount = 1;
            o[0] = t;
            osep = 1;
            $("#iPage").val("0");
            DoCreatClassHtml();
        }
    }
}

var o = new Array();
var osep = 1;
function PostClasses(ids) {
    o = ids.split(",");
    osep = 1;
    CreateDiv.set = 1;
    CreateDiv.setcount = o.length;
    $("#iPage").val("0");
    DoCreatClassHtml();
}

function DoCreatClassHtml() {
    $("#work").val("Create");
    $("#DelClassId").val(o[osep-1]);
    $("#form1").submit();
}


function GetPostClassChild(id){
    var t = GetAllChildClassIdByClassId(id);
	if(t==""){
		return id
	}else{
		return id+","+t;
	}
}

function CreateBack(val) {
    CreateDiv.SetSep(val);
    if (osep < o.length) {
        DoCreatClassHtml();
        osep++;
        $("#iPage").val("0");
    }
}

function CreateBack1(val) {
    CreateDiv.setcount = CreateDiv.setcount + 1;
    CreateDiv.SetSep(val);
    $("#iPage").val(parseInt($("#iPage").val()) + 1);
    DoCreatClassHtml();
}

function MdyFeild(obj, vname) {
    var iAction = $("#iAction");
    iAction.val("MDY");
    var o = $("#iFeildName");
    o.val(vname);
    var iMdyID = $("#iMdyID");
    var form = $("#form1");
    iMdyID.val(GetCheckColumnCheckID(obj));
    var ci = new CreateInput();
    ci.obj = $(obj);
    ci.fobj = form;
    ci.Id = "KeyValue";
    bluraction = "CheckMdyFild()";
    imgaction = "ImgCheck();";
    ci.inputClassName = "itxt3";
    ci.Create();
    ci = null;
}

function GetCheckColumnCheckID(obj) {
    obj = $(obj);
    if (obj == null) return "";
    var o = obj.parent().children().eq(0).children(0);
  
    return $(o).val();
}

function CheckMdyFild() {
    var KeyValue = $("#KeyValue");
    if (KeyValue == null || KeyValue.length == 0) return;
    if (KeyValue.val() == "") {
        KeyValue.remove();
        var CloseImg = $("#CloseImg");
        if (CloseImg != null || CloseImg.length == 0) CloseImg.remove();
    } else {
        $("#form1").submit();
    }
}

function ImgCheck() {
}

function NewsSMDYPostBack() {
    var KeyValue = $("#KeyValue");
    var aValue = KeyValue.val();
    var CloseImg = $("#CloseImg");

    $(CreateInputobj).text(aValue);
    KeyValue.remove();
    CloseImg.remove();
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