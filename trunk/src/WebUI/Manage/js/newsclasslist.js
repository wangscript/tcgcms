//--------------

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
            $("#work").val("Create");
            $("#DelClassId").val(t);
            $("#form1").submit();
        }
    }
}

function PostClasses(ids){
	var o=ids.split(",");
	CreateDiv.set =1;
	CreateDiv.setcount=o.length;
	for(var i=0;i<o.length;i++){
		$("#work").val("Create");
		$("#DelClassId").val(o[i]);
		$("#form1").submit();
	}
}

function GetPostClassChild(id){
	var t=GetAllChildClassIdByClassId(id);
	if(t==""){
		return id
	}else{
		return id+","+t;
	}
}

function CreateBack(val){
	CreateDiv.SetSep(val);
}

function MdyFeild(obj, vname) {
    var iAction = $("iAction");
    iAction.value = "MDY";
    var o = $("iFeildName");
    o.value = vname;
    var iMdyID = $("iMdyID");
    var form = $("form1");
    iMdyID.value = GetCheckColumnCheckID(obj);
    var ci = new CreateInput();
    ci.obj = obj;
    ci.fobj = form;
    ci.Id = "KeyValue";
    bluraction = "CheckMdyFild()";
    imgaction = "ImgCheck();";
    ci.inputClassName = "itxt3";
    ci.Create();
    ci = null;
}

function GetCheckColumnCheckID(obj) {
    if (obj == null) return;
    var o = obj.parentNode;
    var os = (document.all) ? o.children : o.childNodes
    if (os == null) return "";

    for (var i = 0; i < os.length; i++) {
        if (os[i].className == "l_check") {
            var oss = (document.all) ? os[i].children : os[i].childNodes
            return oss[0].value;
        }
    }
    var oo = o.parentNode;
    if (oo == null) return "";
    os = (document.all) ? oo.children : oo.childNodes
    for (var i = 0; i < os.length; i++) {
        if (os[i].className == "l_check") {
            var oss = (document.all) ? os[i].children : os[i].childNodes
            return oss[0].value;
        }
    }
    return "";
}

function CheckMdyFild() {
    var KeyValue = $("KeyValue");
    if (KeyValue == null) return;
    if (KeyValue.value == "") {
        $("form1").removeChild(KeyValue);
        var CloseImg = $("CloseImg");
        if (CloseImg != null) document.body.removeChild(CloseImg);
    } else {
        ajax.postf($("form1"), function(obj) { NewsSMDYPostBack(obj.responseText); });
    }
}

function ImgCheck() {
}

function NewsSMDYPostBack(val) {
    var KeyValue = $("KeyValue");
    if (KeyValue == null) return;
    $("form1").removeChild(KeyValue);
    var aValue = KeyValue.value;
    var CloseImg = $("CloseImg");
    if (CloseImg != null) document.body.removeChild(CloseImg);
    if (GetErrText(val)) return;
    SetInnerText(CreateInputobj, aValue);
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