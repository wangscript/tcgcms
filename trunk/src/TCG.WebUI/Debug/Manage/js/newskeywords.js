var ajax = new AJAXRequest();
var GroupDiv = new MenuDiv();
var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};



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
    sheifnum=0;
	work.value="Search";
	ajax.postf($("form1"),function(obj) { SearchBack(obj.responseText);});
	return false;
}

var o=null;
var sheifnum=0;
var work;
var iTopicId;
var page;
var tClassId;
var tCreated;
var iClassId;
var vcTitle;
var vcShorContent;
function SearchBack(val){
	if(GetErrText(val))return;
	eval("o=["+val+"]");
	if(o!=null){	
		CreateDiv.Start("批量格式关键字...第"+$("page").value+"页");
		CreateDiv.set =1;
		CreateDiv.setcount=o.length;
		sheifnum = o.length-1;
		if(o[sheifnum].iTopicId!=""){
			work.value="UpdateKeyWords";
            iTopicId.value=o[sheifnum].iTopicId;
            vcTitle.value=o[sheifnum].vcTitle;
            vcShorContent.value=o[sheifnum].vcShorContent;
			ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
		}
	}
}

function CreateBack(val){
	CreateDiv.SetSep(val);
	sheifnum--;
	if(sheifnum==-1){
		page.value = parseInt(page.value) + 1;
		CheckForm();
	}
	if(sheifnum>-1){
		work.value="UpdateKeyWords";
		iTopicId.value=o[sheifnum].iTopicId;
        vcTitle.value=o[sheifnum].vcTitle;
        vcShorContent.value=o[sheifnum].vcShorContent;
		ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
	}
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

function SelectClassValue(val,txt){
	var iClassName=$("iClassName");
	var iClassId=$("iClassId");
	iClassName.value=txt;
	iClassId.value=val;
	GroupDiv.HidMenuDiv();
}

function ClassInit(){
    SetAjaxDiv("loader",false,"正在加载中文词库...");
    
	var iClassName=$("iClassName");
	var iClassId=$("iClassId");
	var SelectDivW=$("SelectDivW");
	var pos = getAbsolutePositionXY(iClassName);
	SelectDivW.style.top=(pos.y+1)+"px";
	SelectDivW.style.left=(pos.x+iClassName.offsetWidth-20)+"px";
	var o=GetNewsItemById(iClassId.value);
	if(o==null){
		iClassName.value="请选择资讯分类...";
	}else{
		iClassName.value=o[2];
	}
	
    work=$("work");
    iTopicId=$("iTopicId");
    page=$("page");
    tClassId=$("tClassId");
    tCreated=$("tCreated");
    iClassId=$("iClassId");
    vcTitle=$("vcTitle");
    vcShorContent=$("vcShorContent");
    
    
    work.value="LoadKeyWords";
	ajax.postf($("form1"),function(obj) { LoadBack(obj.responseText);});
}


function LoadBack(val){
    if(GetErrText(val))return;
    SetAjaxDiv("ok",false,"中文词库已经加载完成！");
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