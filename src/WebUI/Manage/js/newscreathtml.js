/// <reference path="jquery-1.3.1-vsdoc.js" />

var CreateDiv=new CreateDiv();


var iId ;
var work;
var tClassId ;
var iFilePath ;
var tCreated ;


$(document).ready(function() {

    iId = $("#iId");
    work = $("#work");
    tClassId = $("#tClassId");
    iFilePath = $("#iFilePath");
    tCreated = $("#tCreated");


    var form1 = $("#form1");
    var options;
    options = {
        beforeSubmit: function () {
            return true; 
        },
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

});


function SelectClassValue(val,txt){
    var iClassName = $("#iClassName");
    var iClassId = $("#iClassId");
    iClassName.val(txt);
    iClassId.val(val);
}

function StartCreate(){
	var iClassId=$("#iClassId");
	var classmsg=$("#classmsg");
	var orderClass=$("#orderClass");
	var work=$("#work");
	
	if(orderClass.attr("checked")==true){

	    if (iClassId.val() == "-1") {
	        classmsg.addClass("info_err").removeClass("info_ok");
			return false;
		}else{
			classmsg.addClass("info_ok").removeClass("info_err");
		}
	}else{
		if(!(CheckValueIsNull('iTimeFeild','timefmsg')&&CheckValueIsDateTime('iStartTime','starttimemsg')&&CheckValueIsDateTime('iEndTime','endtimemsg'))){
			return false;																												
		}
	}
	o=null;
    newsnum=0;
    work.val("Search");
    $("#form1").submit();
}

var o=null;
var newsnum=0;
function SearchBack(val) {
    var work = $("#work");
	if(val=="")return;
	eval("o=["+val+"]");
	if(o!=null){
	    CreateDiv.Start("批量生成资源...第" + $("#page").val() + "页");
	    layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: function() { } 
	    });
		CreateDiv.set =1;
		CreateDiv.setcount=o.length;
		newsnum = o.length -1;
		if (o[newsnum].Id) {
			iId.val(o[newsnum].Id);
			work.val("Create");
			tClassId.val(o[newsnum].ClassId);
			iFilePath.val(o[newsnum].FilePath);
			tCreated.val(o[newsnum].Created);
			$("#form1").submit();
		}
	}
}

function CreateBack(val) {
    CreateDiv.SetSep(val);
    newsnum--;
    if (newsnum == -1) {
        $("#page").val(parseInt($("#page").val()) + 1);
        StartCreate();
    }
    if (newsnum > -1) {
        iId.val(o[newsnum].Id);
        work.val("Create");
        tClassId.val(o[newsnum].ClassId);
        iFilePath.val(o[newsnum].FilePath);
        tCreated.val(o[newsnum].Created);
        $("#form1").submit();
    }
}

function PageInit() {
    $("#page").val("1");

    CreateDiv.Start("批量生成列表...");
    layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: function () { }
    });

    var t = GetAllClassIds();
    if (t.indexOf(",") > -1) {
        PostClasses(t);
    } else {
        var t_classinfo = GetCategorieById(t);
        if (t_classinfo.Url.indexOf(".") == -1) {
            CreateDiv.set = 1;
            CreateDiv.setcount = 1;
            $("#work").val("CreateClassList");
            $("#tClassId").val(t);
            $("#form1").submit();
        }
    }
}

var classids;
var classcreatesep = 0;
function PostClasses(ids) {
    classids = ids.split(",");
    CreateDiv.set = 1;
    CreateDiv.setcount = classids.length;

    var t_classinfo = GetCategorieById(classids[i]);
    if (t_classinfo.Url.indexOf(".") == -1) {
        $("#work").val("CreateClassList");
        $("#tClassId").val(classids[classcreatesep]);
        $("#form1").submit();
    } else {
        CreateDiv.setcount--;
    }
}

//生成列表
function CreateBack3(val) {

    CreateDiv.SetSep(val.message);
    if (classcreatesep < classids.length) {

        if (parseInt($("#page").val()) >= val.page) {
            classcreatesep = classcreatesep + 1;
            $("#page").val("1");
        } else {
            $("#page").val(parseInt($("#page").val()) + 1);
            CreateDiv.setcount++;
        }

        var t_classinfo = GetCategorieById(classids[classcreatesep]);
        if (t_classinfo!=null&&t_classinfo.Url.indexOf(".") == -1) {
            $("#work").val("CreateClassList");
            $("#tClassId").val(classids[classcreatesep]);
            $("#form1").submit();
        } else {
            CreateDiv.setcount--;
        }
    }

    if (classcreatesep == classids.length) {

        CreateDiv.Start("批量生成单页...");
        layer.openLayer({ id: 'layerbox', width: 426, height: 332, callBack: function () { }
        });
        debugger;
        var ot = GetSingTemplate();
        CreateDiv.set = 1;
        CreateDiv.setcount = ot.length;
        for (var i = 0; i < ot.length; i++) {
            $("#work").val("CreateSingeTemplate");
            $("#tClassId").val(ot[i].Id);
            $("#form1").submit();
        }
    }
}


function CreateBack1(val) {
    CreateDiv.SetSep(val);
    if (CreateDiv.set == (CreateDiv.setcount + 1)) {
        var ot = GetSingTemplate();
        CreateDiv.set = 1;
        CreateDiv.setcount = ot.length;
        for (var i = 0; i < ot.length; i++) {
            $("#work").val("CreateSingeTemplate");
            $("#tClassId").val(ot[i].Id);
            $("#form1").submit();
        }
    }
}

function CreateBack2(val) {
    CreateDiv.SetSep(val);
}