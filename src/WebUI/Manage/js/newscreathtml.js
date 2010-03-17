/// <reference path="jquery-1.3.1-vsdoc.js" />

var CreateDiv=new CreateDiv();


var iId ;
var work;
var tClassId ;
var iFilePath ;
var tCreated ;


$(document).ready(function() {

    iId = $("iId");
    work = $("work");
    tClassId = $("tClassId");
    iFilePath = $("iFilePath");
    tCreated = $("tCreated");

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

//    var iClassName = $("iClassName");
//    var iClassId = $("iClassId");
//    var SelectDivW = $("SelectDivW");
//    var pos = getAbsolutePositionXY(iClassName);
//    SelectDivW.style.top = (pos.y + 1) + "px";
//    SelectDivW.style.left = (pos.x + iClassName.offsetWidth - 20) + "px";
//    var o = GetNewsItemById(iClassId.value);
//    if (o == null) {
//        iClassName.value = "请选择资讯分类...";
//    } else {
//        iClassName.value = o[2];
//    }
//    var work = $("work");
//    work.value = "KeyWordLoad";

});

function KeyWordLoadBack(val){
    if(GetErrText(val))return;
     SetAjaxDiv("ok",false,"词库加载成功！");
}

function selebg1(){
	var iClassName=$("iClassName");
	var SelectDivW=$("SelectDivW");
	SelectDivW.className="selectDiv sl_bg2";
	iClassName.className="itxt2";
}

function selebg2(){
	var iClassName=$("iClassName");
	var SelectDivW=$("SelectDivW");
	SelectDivW.className="selectDiv sl_bg1";
	iClassName.className="itxt1";
}

function ShowNewsClassSl(){
	var iClassName=$("iClassName");
	var alist =GetClassItems(0);
	if(alist.length>0){
		GroupDiv.HidAuto = false;
		GroupDiv.CreadDiv("ClassDiv",iClassName,alist,iClassName.offsetWidth,null,6,0);
	}
}

function GetClassItems(v){
	var items =[];
	var n=0;
	var val=$("iClassId").value;
	var sels=false;
	for(var i=0;i<NewsLis.length;i++){
		if(v==NewsLis[i][1]){
			if(NewsLis[i][0]==val){
				sels=true;
			}else{
				sels=false;
			}
			items[n]={href:"javascript:GoTo();",onclick:"SelectClassValue("+NewsLis[i][0]+",'"+NewsLis[i][2]+"');",Text:NewsLis[i][2],onmouseover:"ShowChild(this,"+NewsLis[i][0]+")",onmouseout:"HidChild("+NewsLis[i][0]+")",Sel:sels};
			n++;
		}
	}
	return items
}

function SelectClassValue(val,txt){
    var iClassName = $("#iClassName");
    var iClassId = $("#iClassId");
    iClassName.val(txt);
    iClassId.val(val);

}

function ShowChild(obj,classid){
	var id="ClassDiv_"+classid;
	var o=obj.parentNode;
	o.className="topmenu";
	GroupDiv.DefZIndex++;
	var al = GetClassItems(classid);
	if(al.length>0){
		GroupDiv.HidAuto = false;
		GroupDiv.CreadDiv(id,obj,al,obj.offsetWidth,null,0,0,"right");
	}
}


function HidChild(classid){
	var id="ClassDiv_"+classid;
	GroupDiv.HidMenuItem(id);
}

function CheckForm(){
	var iClassId=$("iClassId");
	var classmsg=$("classmsg");
	var orderClass=$("orderClass");
	var work=$("work");
	
	if(orderClass.checked==true){
		
		if(iClassId.value=="-1"){
			classmsg.className="info_err";
			return false;
		}else{
			classmsg.className="info_ok";
		}
	}else{
		if(!(CheckValueIsNull('iTimeFeild','timefmsg')&&CheckValueIsDateTime('iStartTime','starttimemsg')&&CheckValueIsDateTime('iEndTime','endtimemsg'))){
			return false;																												
		}
	}
	o=null;
    newsnum=0;
	work.value="Search";
	ajax.postf($("form1"),function(obj) { SearchBack(obj.responseText);});
	return false;
}

var o=null;
var newsnum=0;
function SearchBack(val){
	if(val=="")return;
	eval("o=["+val+"]");
	if(o!=null){
		CreateDiv.Start("批量生成资源...第"+$("page").value+"页");
		CreateDiv.set =1;
		CreateDiv.setcount=o.length;
		newsnum = o.length -1;
		if(o[newsnum].Id){
			iId.value =o[newsnum].Id;
			work.value="Create";
			tClassId.value =o[newsnum].ClassId;
			iFilePath.value =o[newsnum].FilePath;
			tCreated.value =o[newsnum].Created;
			ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
		}
	}
}

function CreateBack(val){
	CreateDiv.SetSep(val);
	newsnum--;
	if(newsnum==-1){
		$("page").value = parseInt($("page").value) + 1;
		CheckForm();
	}
	if(newsnum>-1){
		iId.value =o[newsnum].Id;
		work.value="Create";
		tClassId.value =o[newsnum].ClassId;
		iFilePath.value =o[newsnum].FilePath;
		tCreated.value =o[newsnum].Created;
		ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
	}
}
