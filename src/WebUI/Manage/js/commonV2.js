/// <reference path="jquery-1.3.1-vsdoc.js" />

var ajaxiconRoot = ['images/ajax-loader1.gif', 'images/post_err.gif', 'images/post_ok.gif'];

function GoTo(){
	void(0);
}
function fGoBack(){
	window.history.back();
}

function ajaxClose(){
	var ajaxdiv = $("ajaxdiv");
	if(ajaxdiv==null)return;
	ajaxdiv.className = "ajaxdiv hid";
}



function SetInnerText(obj,value){
    if (obj == null) return;
    obj = $(obj);
    obj.text(value);
}

//获得Ceckbox选中的值
function GetCheckBoxValues(BoxName){
	var boxs = document.getElementsByName(BoxName);
	if(boxs==null)return "";
	var str = "";
	for(var i =0;i<boxs.length;i++){
		if(boxs[i].tagName=="INPUT"&&boxs[i].checked){
			str+=(str=="")?boxs[i].value:","+boxs[i].value;
		}
	}
	return str;
}

function GetCheckBoxValuesForSql(BoxName){
	var boxs = document.getElementsByName(BoxName);
	if(boxs==null)return "";
	var str = "";
	for(var i =0;i<boxs.length;i++){
		if(boxs[i].tagName=="INPUT"&&boxs[i].checked){
			str+=(str=="")?"'"+boxs[i].value+"'":",'"+boxs[i].value+"'";
		}
	}
	return str;
}

//获得checkbox的个数
function GetCheckBoxCount(BoxName){
	var boxs = document.getElementsByName(BoxName);
	if(boxs==null)return 0;
	return boxs.length;
}

function GetCheckedBoxSetting(vals){
	var os=new Array();
	if(vals==""||vals==null)return os;
	if(vals.indexOf(",")>-1){
		var v=vals.split(",");
		for(var i=0;i<v.length;i++){
			os[i]={Text:v[i],Value:true}
		}
	}else{
		os[0]={Text:vals,Value:true};
	}
	return os;
}

//设置checkbox选择属性
function SetCheckBox(BoxName,Action){
	if(Action==null)Action="SELECT ALL";
	var boxs = document.getElementsByName(BoxName);
	if(boxs==null)return;
	if(typeof(Action) == "object"){
		if(typeof(Action.tagName)=="undefined"){
			for(var i =0;i<boxs.length;i++){
				for(var n =0;n<Action.length;n++){
					if(Action[n]==null)return;
					if(boxs[i].type=="checkbox"&&boxs[i].value==Action[n].Text){
						boxs[i].checked = Action[n].Value;
					}
				}
			}
		}else{
			if(Action.type=="checkbox"){
				for(var i =0;i<boxs.length;i++){
					if(boxs[i].type=="checkbox"){
						boxs[i].checked = Action.checked?true:false;
					}
				}
			}
		}
	}
}

function SetAjaxDiv(action,IsHide,txt){
	var ajaxdiv = $("#ajaxdiv");
	var ajaximg = $("#ajaximg");
	var ajaxText = $("#ajaxText");
	if(ajaxdiv.length==0)return;
	
	ajaxdiv[0].className = IsHide?"ajaxdiv hid":"ajaxdiv";
	ajaxText.text(txt);
	switch(action){
		case "loader" :
			ajaximg[0].src = ajaxicon[0];
			ajaxText[0].className = "loader";
		break;
		case "err" :
			ajaximg[0].src = ajaxicon[1];
			ajaxText[0].className = "err red";
		break;
		case "ok" :
			ajaximg[0].src = ajaxicon[2];
			ajaxText[0].className = "ok bold";
		break;
	}
}

function SetAjaxDivRoot(action, IsHide, txt) {
    var ajaxdiv = $("#ajaxdiv");
    var ajaximg = $("#ajaximg");
    var ajaxText = $("#ajaxText");
    if (ajaxdiv.length == 0) return;

    ajaxdiv[0].className = IsHide ? "ajaxdiv hid" : "ajaxdiv";
    ajaxText.text(txt);
    switch (action) {
        case "loader":
            ajaximg[0].src = ajaxiconRoot[0];
            ajaxText[0].className = "loader";
            break;
        case "err":
            ajaximg[0].src = ajaxiconRoot[1];
            ajaxText[0].className = "err red";
            break;
        case "ok":
            ajaximg[0].src = ajaxiconRoot[2];
            ajaxText[0].className = "ok bold";
            break;
    }
}

function SetAjaxDivAdminMian(action,IsHide,txt){
	var ajaxdiv = window.parent.adminmain.$("#ajaxdiv");
	var ajaximg = window.parent.adminmain.$("#ajaximg");
	var ajaxText = window.parent.adminmain.$("#ajaxText");
	if(ajaxdiv==null)return;

	ajaxdiv[0].className = IsHide?"ajaxdiv hid":"ajaxdiv";
	SetInnerText(ajaxText,txt);
	switch(action){
		case "loader" :
			ajaximg[0].src = ajaxicon[0];
			ajaxText[0].className = "loader";
		break;
		case "err" :
			ajaximg[0].src = ajaxicon[1];
			ajaxText[0].className = "err red";
		break;
		case "ok" :
			ajaximg[0].src = ajaxicon[2];
			ajaxText[0].className = "ok bold";
		break;
	}
}

function GetErrText(val) {
    if (Number(val) < 0) {
        ajax.get("../AjaxMethod/Text_GetErrText.aspx?ErrCode=" + val,
				function(obj) { ErrBack(obj.responseText); }
			)
        return true;
    }
    return false;
}

function GetErrTextRoot(val) {
    if (Number(val) < 0) {
        ajax.get("AjaxMethod/Text_GetErrText.aspx?ErrCode=" + val,
				function(obj) { ErrBack(obj.responseText); }
			)
        return true;
    }
    return false;
}

function GetErrTextFrame(val){
	if(Number(val)<0){
	ajax.get("../AjaxMethod/Text_GetErrText.aspx?ErrCode="+val,
			function(obj) { ErrBackFrom(obj.responseText);}
		)
	return true;
	}
	return false;
}

function GetErrTextFrameRoot(val) {
    if (Number(val) < 0) {
        ajax.get("AjaxMethod/Text_GetErrText.aspx?ErrCode=" + val,
			function(obj) { ErrBackFrom(obj.responseText); }
		)
        return true;
    }
    return false;
}

function ErrBack(val){
	SetAjaxDiv("err",false,val);
}

function ErrBackFrom(val){
	SetAjaxDivAdminMian("err",false,val);
}

function GetColor(r,g,b){ 
	var red=r; 
	var green=g; 
	var blue=b; 
	
	var hR=red.toString(16); 
	var hG=green.toString(16); 
	var hB=blue.toString(16); 
	return "#"+(red<16?("0"+hR):hR)+(green<16?("0"+hG):hG)+(blue<16?("0"+hB):hB); 
}; 

function IntInList(int,intlist,sp){
	if(intlist.indexOf(sp)==-1)return false;
	var la = intlist.split(sp);
	for(var i=0;i<la.length;i++){
		if(la[i]==String(int))return true;
	}
	return false;
}

function GetMSelectValue(obj) {
    var Value = "";
    for (var i = 0; i < obj.length; i++) {
        if (obj.options[i].selected == true) {
            Value += ((Value == "") ? "" : ",") + obj.options[i].value;
        }
    }
    return Value;
}

function SetPopValue(objname,obj){
	if(objname==null)return;
	if(obj==null)return;
	var o = $("#"+objname);
	if (o.length == 0) return;
	o.val(GetMSelectValue(obj));
}

function refinsh() {
    window.location.reload();
}

function getAbsolutePositionXY(obj){
	if(obj==null)return {x:0,y:0};
	var position={x:0,y:0};
	while(obj!=null && obj!=document.body){
		position.x += obj.offsetLeft;
		position.y += obj.offsetTop;
		obj = obj.offsetParent
	 }
	return position;
}

function CheckValueIsNull(obj, msgobj) {
    var iClassName = $("#" + obj);
    var cnamemsg = $("#" + msgobj);
    if (iClassName.attr("tagName") == "INPUT") {
        iClassName.addClass("itxt1").removeClass('info_err').removeClass('info_ok');
        if (iClassName.val() == "") {
            cnamemsg.addClass("info_err").removeClass('itxt1').removeClass('info_ok');
            return false;
        } else {
            cnamemsg.addClass("info_ok").removeClass('itxt1').removeClass('info_err');
            return true;
        }
    } else if (iClassName.attr("tagName") == "SELECT") {
        if (iClassName.val() == "-1") {
            cnamemsg.addClass("info_err").removeClass('itxt1').removeClass('info_ok');
            return false;
        } else {
            cnamemsg.addClass("info_ok").removeClass('itxt1').removeClass('info_err');
            return true;
        }
    }
}

function CharInStr(str,chr){
	if(str==null||str==0)return false;
	if(str.indexOf(",")>-1){
		var sa = str.split(",");
		for(var i=0;i<sa.length;i++){
			if(sa[i]==chr)return true;
		}
	}else{
		if(str==chr)return true;
	}
	return false;
}

function SetCharInStr(str,chr){
	if(str==0)str="";
	return str+=(str=="")?chr:","+chr;
}

function MoveCharOutStr(str,chr){
	if(str==null)return "";
	if(str.indexOf(",")>-1){
		var sa = str.split(",");
		str="";
		for(var i=0;i<sa.length;i++){
			if(sa[i]!=chr){
				str+=(str=="")?sa[i]:","+sa[i];
			}
		}
	}else{
		if(str==chr)return "";
	}
	return str;
}

function CheckValueIsDateTime(obj, msgobj) {
    var iClassName = $("#" + obj);
    var cnamemsg = $("#" + msgobj);
    iClassName.addClass("itxt1");
    if (strDateTime(iClassName.val())) {
        cnamemsg.addClass("info_ok").removeClass("info_err");
        return true;
    } else {
        cnamemsg.addClass("info_err").removeClass("info_ok");
        return false;
    }
}

function strDateTime(str){
	var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2})$/; 
	var r = str.match(reg);
	if(r==null)return false; 
	var d= new Date(r[1], r[3]-1,r[4],r[5],r[6]); 
	return (d.getFullYear()==r[1]&&(d.getMonth()+1)==r[3]&&d.getDate()==r[4]&&d.getHours()==r[5]&&d.getMinutes()==r[6]);
}


/*----------------------------*/
function AjaxPostFormBack(data) {
    if (data.state) {
        if (data.callback != null) {
            if (data.callback.indexOf("(") > -1) {
                try {eval(data.callback); } catch (err) {}
                return;
            } else {
                try { eval(data.callback + "(\"" + data.message + "\")"); } catch (err) { /*alert(err)*/ }
                return;
            }
        }
        SetAjaxDiv("ok", false, data.message);
    } else {
        SetAjaxDiv("err", false, data.message);
    }
}

function AjaxPostFormBack2(data) {
    if (data.state) {
        if (data.callback != null) {
            if (data.callback.indexOf("(") > -1) {
                try { eval(data.callback); } catch (err) { }
            } else {
                try { eval(data.callback + "(\"" + data.message + "\")"); } catch (err) { /*alert(err)*/ }
                return;
            }
        }
        SetAjaxDivAdminMian("ok", false, data.message);
    } else {
        SetAjaxDivAdminMian("err", false, data.message);
    }
}

function BeforSubmit() {
    return true;
}

function refash() {

    window.parent.adminpop.location.href = window.parent.adminpop.location.href;
    window.location.href = window.location.href;
    window.parent.adminmain.location.href = window.parent.adminmain.location.href;
}


function CategoriePropertieMdyHtml(id, name, type, Values, width, height, iOrder) {

    var type1Check = type == "01" ? "checked" : "";
    var type2Check = type == "02" ? "checked" : "";
    var type3Check = type == "03" ? "checked" : "";

    return "<div id=\"cp_" + id + "\" style=\"height:100px;\"><a class=\"fn-bg Bopned\" href=\"javascript:GoTo()\" onclick=\"CategoriePropertieHTMLDel(" + id
        + ")\"></a><div class=\"Page_arrb arb_pr classaddline\"><span class=\"p_a_t\">属性名称：</span>"
	    + "<input id=\"name_" + id + "\" name=\"name_" + id + "\" type=\"text\" value=\"" + name + "\" class=\"itxt1\" onfocus=\"this.className='itxt2'\" onblur=\"this.className='itxt1'\"/>"
		+ "<input type=\"radio\" name=\"type_" + id + "\" id=\"type_" + id + "_1\" " + type1Check + " value=\"01\" /><label for=\"type_" + id + "_1\">输入</label>"
        + "<input type=\"radio\" name=\"type_" + id + "\" id=\"type_" + id + "_2\" " + type2Check + " value=\"02\" /><label for=\"type_" + id + "_2\">单选</label>"
        + "<input type=\"radio\" name=\"type_" + id + "\" id=\"type_" + id + "_3\" " + type3Check + " value=\"03\" /><label for=\"type_" + id + "_3\">复选</label>"
        + "<span id=\"Span1\" class=\"info1\">可选项目请用\"|\"间隔</span>"
	    + "</div>"
        + "<div class=\"Page_arrb arb_pr classaddline\"><span class=\"p_a_t\">宽&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：</span>"
	    + "<input id=\"pwidth_" + id + "\" name=\"pwidth_" + id + "\" type=\"text\" value=\"" + (width == 0 ? "" : width) + "\" style=\"width:80px;\" class=\"itxt1\" onfocus=\"this.className='itxt2'\" onblur=\"this.className='itxt1'\"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	    + "<span class=\"p_a_t\">高&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：</span><input id=\"pheight_" + id + "\" name=\"pheight_" + id + "\" type=\"text\" value=\"" + (height == 0 ? "" : height) + "\" style=\"width:80px;\" class=\"itxt1\" onfocus=\"this.className='itxt2'\" onblur=\"this.className='itxt1'\"/>"
        +"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	    + "<span class=\"p_a_t\">排&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;序：</span><input id=\"porder_" + id + "\" name=\"porder_" + id + "\" type=\"text\" value=\"" + (iOrder == 0 ? "0" : iOrder) + "\" style=\"width:80px;\" class=\"itxt1\" onfocus=\"this.className='itxt2'\" onblur=\"this.className='itxt1'\"/>"
        + "</div>"
        + "<div class=\"Page_arrb arb_pr classaddline\"><span class=\"p_a_t\">可选项目：</span>"
	    + "<input id=\"pttext_" + id + "\" name=\"pttext_" + id + "\" type=\"text\" value=\"" + Values + "\" style=\"width:480px;\" class=\"itxt1\" onfocus=\"this.className='itxt2'\" onblur=\"this.className='itxt1'\"/>"
	    + "</div>"
        + "<input name=\"cpid_" + id + "\" type=\"hidden\" id=\"cpid_" + id + "\" value=\"" + id + "\" /></div>";
}