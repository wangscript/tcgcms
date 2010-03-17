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

    GetCagetegoriesEnmu($("#Cagetorie_c"), $("#iSkinId").val(), "0");

    Menu.init("gamelist");

    $("#SelectDivW").bind('click', function(e) {
        if ($("#gamelist_c").css('display') == 'block') {
            $("#gamelist_c").hide();
        } else {
            $("#gamelist_c").show();
        }
        e.stopPropagation();

    });

    $(document).bind('click', function(e) {

        if ($("#gamelist_c").css('display') == 'block') {
            $("#gamelist_c").hide();
        }
    });

    var form1 = $("#form1");
    var options;
    options = {
        beforeSubmit: function() { return true; },
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
	if(newsnum>-1){
		iId.val(o[newsnum].Id);
		work.val("Create");
		tClassId.val(o[newsnum].ClassId);
		iFilePath.val(o[newsnum].FilePath);
		tCreated.val(o[newsnum].Created);
		$("#form1").submit();
	}
}
