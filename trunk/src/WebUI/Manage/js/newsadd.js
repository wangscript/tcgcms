//--------------
var ajax = new AJAXRequest();
var GroupDiv = new MenuDiv();

function SaveNewsInfo(){
	$("btnok").click();
}

function CheckAdd(){
	if(!(CheckValueIsNull('iTitle','titlemsg')&&CheckValueIsNull('iClassName','classmsg')&&CheckValueIsNull('iAuthor','autmsg')&&CheckValueIsNull('iKeyWords','keymsg'))){
		return false;
	}

	SetAjaxDiv("loader", false, "正在发送请求...");
	
    //输入框数据赋值
	setContent();
	$("form1").action = "../files/ConentImgCheck.aspx";
	ajax.postf($("form1"), function(obj) { CheckImgBack(obj.responseText); });

	return false;
}

function CheckImgBack(val) {
    alert(val);
    $("iContent_content").value = val;
    $("form1").action = "newsadd.aspx";
	var work = $("work");
	if(work.value=="AddNew"){
		ajax.postf($("form1"),function(obj) { NewsAddPostBack(obj.responseText);});
	}else{
		ajax.postf($("form1"),function(obj) { NewsMdyPostBack(obj.responseText);})
	}
}

function SelectSmallImg(obj){
	var imgPace=$("imgPace");
	var iSmallImg=$("iSmallImg");
	var pos = getAbsolutePositionXY(obj);
	imgPace.className="imgPace";
	imgPace.style.top=(pos.y+obj.offsetHeight)+"px";
	imgPace.style.left=pos.x+"px";
	imgPace.innerHTML = "";
	setContent();
	var iContent = $("iContent_content").value;
	var reg = /<(img|IMG|input type=\"image\")[^>]+src="[^"]+"[^>]*>/g;
	var result = iContent.match(reg);
	if (result != null) {
		for (var i=0;i<result.length; i++){
			var reg2 = /src="([^"]+)"/;
			var result2 = reg2.exec(result[i]);
			var c=(iSmallImg.value==RegExp.$1)?"checked":"";
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetSmallImge('"+RegExp.$1+"')\" />设置</li></ul>";
		}
	}
}

function SelectBigImg(obj){
	var imgPace=$("imgPace");
	var iBigImg=$("iBigImg");
	var pos = getAbsolutePositionXY(obj);
	imgPace.className="imgPace";
	imgPace.style.top=(pos.y+obj.offsetHeight)+"px";
	imgPace.style.left=pos.x+"px";
	imgPace.innerHTML = "";
	setContent();
	var iContent = $("iContent_content").value;
	
	var reg = /<(img|IMG|input type=\"image\")[^>]+src="[^"]+"[^>]*>/g;
	var result = iContent.match(reg);
	if(result!=null){
		for (var i=0;i<result.length; i++){
			var reg2 = /src="([^"]+)"/;
			var result2 = reg2.exec(result[i]);
			var c=(iBigImg.value==RegExp.$1)?"checked":"";
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetBigImge('"+RegExp.$1+"')\" />设置</li></ul>";
		}
	}
}

function SetSmallImge(src){
	var imgPace=$("imgPace");
	var iSmallImg=$("iSmallImg");
	imgPace.className="imgPace hid";
	iSmallImg.value=src;
}

function SetBigImge(src){
	var imgPace=$("imgPace");
	var iBigImg=$("iBigImg");
	imgPace.className="imgPace hid";
	iBigImg.value=src;
}

function NewsMdyPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"资讯成功修改并生成文件！");
}

function NewsAddPostBack(val) {
	if(GetErrText(val))return;
	var iTitle=$("iTitle");
	var iAuthor=$("iAuthor");
	var iKeyWords=$("iKeyWords");
	var msg = "资讯["+iTitle.value+"]已经成功添加，不继续添加请点取消！";
	SetAjaxDiv("ok",false,msg);
	iTitle.value="";
	iAuthor.value="";
	iKeyWords.value="";
}


function ShowNewsClassSl() {
    var iClassName = $("iClassName");
    var alist = GetClassItems("0");
    if (alist.length > 0) {
        GroupDiv.HidAuto = false;
        GroupDiv.CreadDiv("ClassDiv", iClassName, alist, iClassName.offsetWidth, null, 6, 0);
    }
}

function SelectClassValue(val,txt){
	var iClassName=$("iClassName");
	var iClassId=$("iClassId");
	iClassName.value=txt;
	iClassId.value=val;
	GroupDiv.HidMenuDiv();
}

function ShowFromDiv(obj){
	var froms=$("froms");
	var iFrom=$("iFrom");
	var ot =[];
	var vals=iFrom.value;
	var sels=false;
	for(var i=0;i<NewsFromItems.length;i++){
		if(NewsFromItems[i][0]==vals){
			sels=true;
		}else{
			sels=false;
		}
		ot[i]={href:"javascript:GoTo();",onclick:"SelectFromValue("+NewsFromItems[i][0]+");",Text:NewsFromItems[i][1],Sel:sels};
	}
	GroupDiv.HidAuto = true;
	GroupDiv.CreadDiv("froms",obj,ot,200,null,0,0);
}

function SelectFromValue(val){
	var iFrom=$("iFrom");
	iFrom.value=val;
	GroupDiv.HidMenuItem("froms");
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
			items[n]={href:"javascript:GoTo();",onclick:"SelectClassValue(\""+NewsLis[i][0]+"\",\""+NewsLis[i][2]+"\");",Text:NewsLis[i][2],onmouseover:"ShowChild(this,\""+NewsLis[i][0]+"\")",onmouseout:"HidChild(\""+NewsLis[i][0]+"\")",Sel:sels};
			n++;
		}
	}
	return items
}
var tempWork;
function ClassInit() {
    SetAjaxDiv("loader", false, "正在加载关键字词库...");
    tempWork = $("work").value;
    var work = $("work");
    work.value = "KeyWordLoad";
    ajax.postf($("form1"), function(obj) { KeyWordLoadBack(obj.responseText); });

}

function KeyWordLoadBack(val) {
    if (GetErrText(val)) return;
    SetAjaxDiv("ok", false, "词库加载成功！");

    ColorInit(); //初始化颜色选择
    var iClassName = $("iClassName");
    var iClassId = $("iClassId");
    var SelectDivW = $("SelectDivW");
    var pos = getAbsolutePositionXY(iClassName);
    SelectDivW.style.top = (pos.y + 1) + "px";
    SelectDivW.style.left = (pos.x + iClassName.offsetWidth - 20) + "px";
    var o = GetNewsItemById(iClassId.value);
    if (o == null) {
        iClassName.value = "请选择资讯分类...";
    } else {
        iClassName.value = o[2];
    }
    $("work").value = tempWork;
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

function SpecialitySel(obj){
	var iClassId=$("iClassId");
	//var op =GetNewsSiteByClassId(iClassId.value);
	//if (op == null) return;
	var o = GetSpecialItems(0, true);
	o[o.length] = { href: "javascript:GoTo();", onclick: "GroupDiv.HidMenuDiv()",
	 Text: "<img src='../images/close.gif' style='float:left;'><span style='float:left;'><b>关闭</b></span>", onmouseover: "void 0;", onmouseout: "void 0", Sel: false };
	GroupDiv.HidAuto = false;
	GroupDiv.IsMut=true;
	GroupDiv.CreadDiv("Speciality",obj,o,150,null,0,0);
	GroupDiv.IsMut=false;
}
function ShowSChild(obj,id){
	var sid="Speciality_"+id;
	var o=obj.parentNode;
	o.className="topmenu";
	GroupDiv.DefZIndex++;
	var al = GetSpecialItems(id,false);
	if(al.length>0){
		GroupDiv.HidAuto = false;
		GroupDiv.IsMut=true;
		GroupDiv.CreadDiv(sid,obj,al,150,null,0,0,"right");
		GroupDiv.IsMut=false;
	}
}

function HidSChild(classid){
	var id="Speciality_"+classid;
	GroupDiv.HidMenuItem(id);
}

function SelSpeciality(obj,id){
	var iSpeciality=$("iSpeciality");
	GroupDiv.MutClick(obj);
	if(!CharInStr(iSpeciality.value,id)){
		iSpeciality.value = SetCharInStr(iSpeciality.value,id)
	}else{
		iSpeciality.value = MoveCharOutStr(iSpeciality.value,id)
	}
}

function GetSpecialItems(id,b){
	var iSpeciality=$("iSpeciality");
	var sels=false;
	var isdo =false;
	var o =[];
	var n=0;
	for(var i=0;i<NewsSpeciality.length;i++){
		if(b){
			if(id==NewsSpeciality[i][1]&&NewsSpeciality[i][2]==0){
				isdo=true;
			}else{
				isdo=false;
			}
		}else{
			if(id==NewsSpeciality[i][2]){
				isdo=true;
			}else{
				isdo=false;
			}
		}
		sels=CharInStr(iSpeciality.value,NewsSpeciality[i][0])?true:false;
		if(isdo){
			o[n]={href:"javascript:GoTo();",onclick:"SelSpeciality(this,"+NewsSpeciality[i][0]+")",Text:NewsSpeciality[i][3],onmouseover:"ShowSChild(this,"+NewsSpeciality[i][0]+")",onmouseout:"HidSChild("+NewsSpeciality[i][0]+")",Sel:sels};
			n++;
		}
	}
	return o
}

//颜色选择初始化
function ColorInit() {
    var sTitleColor = $("sTitleColor");
    var iTitleColor = $("iTitleColor");

    var oOption = document.createElement("OPTION");
    oOption.text = "请选择颜色";
    oOption.value = "";
    sTitleColor.options.add(oOption);

    var o = { "White": "#FFFFFF", "Green": "#008000", "Silver": "#C0C0C0", "Lime": "#00FF00", "Gray": "#808080", "Olive": "#808000", "Black": "#000000", "Yellow": "#FFFF00", "Maroon": "#800000", "Navy": "#000080", "Red": "#FF0000", "Blue": "#0000FF", "Purple": "#800080", "Teal": "#008080", "Fuchsia": "#FF00FF", "Aqua": "#00FFFF" };
    for (key in o) {
        var oOption = document.createElement("OPTION");
        oOption.text = key;
        oOption.value = o[key];
        oOption.style.cssText = "background-color:" + o[key] + ";"
        sTitleColor.options.add(oOption);
        if (o[key] == iTitleColor.value) {
            sTitleColor.value = o[key];
        }
    }
}