/// <reference path="jquery-1.3.1-vsdoc.js" />

function SaveNewsInfo() {
    setContent();
    $("#form1").submit();
}

function CheckAdd(){
	if(!(CheckValueIsNull('iTitle','titlemsg')&&CheckValueIsNull('iClassName','classmsg')&&CheckValueIsNull('iAuthor','autmsg')&&CheckValueIsNull('iKeyWords','keymsg'))){
		return false;
	}
	SetAjaxDiv("loader", false, "���ڷ�������...");
	return true;
}

function CheckImgBack(val) {
    alert(val);
    $("#taContent").val(val);
    $("#form1")[0].action = "newsadd.aspx";
	var work = $("#work");
	if(work.val()=="AddNew"){
		ajax.postf($("form1"),function(obj) { NewsAddPostBack(obj.responseText);});
	}else{
		ajax.postf($("form1"),function(obj) { NewsMdyPostBack(obj.responseText);})
	}
}

function setContent() {
    $("#taContent").val(KE.html("taContent"));
}

function SelectSmallImg(obj) {
    var imgPace = $("#imgPace").get(0);
    var iSmallImg = $("#iSmallImg").get(0);
	var pos = getAbsolutePositionXY(obj);
	imgPace.className="imgPace";
	imgPace.style.top=(pos.y+obj.offsetHeight)+"px";
	imgPace.style.left=pos.x+"px";
	imgPace.innerHTML = "";
	setContent();
	var iContent = $("#taContent").val();
	var reg = /<(img|IMG|input type=\"image\")[^>]+src="[^"]+"[^>]*>/g;
	var result = iContent.match(reg);
	if (result != null) {
		for (var i=0;i<result.length; i++){
			var reg2 = /src="([^"]+)"/;
			var result2 = reg2.exec(result[i]);
			var c=(iSmallImg.value==RegExp.$1)?"checked":"";
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetSmallImge('"+RegExp.$1+"')\" />����</li></ul>";
		}
	}
}

function SelectBigImg(obj) {
	var imgPace=$("#imgPace").get(0);
	var iBigImg = $("#iBigImg").get(0);
	var pos = getAbsolutePositionXY(obj);
	imgPace.className="imgPace";
	imgPace.style.top=(pos.y+obj.offsetHeight)+"px";
	imgPace.style.left=pos.x+"px";
	imgPace.innerHTML = "";
	setContent();
	var iContent = $("#taContent").val();
	
	var reg = /<(img|IMG|input type=\"image\")[^>]+src="[^"]+"[^>]*>/g;
	var result = iContent.match(reg);
	if(result!=null){
		for (var i=0;i<result.length; i++){
			var reg2 = /src="([^"]+)"/;
			var result2 = reg2.exec(result[i]);
			var c=(iBigImg.value==RegExp.$1)?"checked":"";
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetBigImge('"+RegExp.$1+"')\" />����</li></ul>";
		}
	}
}

function SetSmallImge(src){
	var imgPace=$("#imgPace");
	var iSmallImg=$("#iSmallImg");
	imgPace.get(0).className = "#imgPace hid";
	iSmallImg.val(src);
	$("#imgPace").get(0).className = "imgPace hid";
}

function SetBigImge(src){
	var imgPace=$("#imgPace");
	var iBigImg = $("#iBigImg");
	imgPace.addClass("imgPace hid");
	iBigImg.val(src);
	$("#imgPace").get(0).className = "imgPace hid";
}

function NewsMdyPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"��Ѷ�ɹ��޸Ĳ������ļ���");
}

function NewsAddPostBack() {
	var iTitle=$("#iTitle");
	var iAuthor=$("#iAuthor");
	var iKeyWords=$("#iKeyWords");
	var msg = "��Ѷ["+iTitle.val()+"]�Ѿ��ɹ���ӣ�������������ȡ����";
	SetAjaxDiv("ok",false,msg);
	iTitle.val("");
	iAuthor.val("");
	iKeyWords.val("");
}

function SelectClassValue(val, txt) {

    var iClassName = $("#iClassName");
    var iClassId = $("#iClassId");
    iClassName.val(txt);
    iClassId.val(val);
}

function SelectSpecialityValue(val, txt) {
    $("#iSpeciality_t").val(txt);
    $("#iSpeciality").val(val);

}


var tempWork;
function ClassInit() {

    ColorInit(); //��ʼ����ɫѡ��
    var iClassName = $("#iClassName");
    var iClassId = $("#iClassId");
    var SelectDivW = $("#SelectDivW");

    var o = GetCategorieById(iClassId.val());
    if (o == null) {
        iClassName.val("��ѡ����Ѷ����...");
    } else {
        iClassName.val(o.ClassName);
    }

}


//��ɫѡ���ʼ��
function ColorInit() {
    var sTitleColor = $("#sTitleColor");
    var iTitleColor = $("#iTitleColor");
    sTitleColor.append("<option value=''>��ѡ����ɫ</option>");

    var o = { "White": "#FFFFFF", "Green": "#008000", "Silver": "#C0C0C0", "Lime": "#00FF00", "Gray": "#808080", "Olive": "#808000", "Black": "#000000", "Yellow": "#FFFF00", "Maroon": "#800000", "Navy": "#000080", "Red": "#FF0000", "Blue": "#0000FF", "Purple": "#800080", "Teal": "#008080", "Fuchsia": "#FF00FF", "Aqua": "#00FFFF" };
    for (key in o) {

        sTitleColor.append("<option value='" + o[key] + "' style='background-color:" + o[key] + ";'>" + key + "</option>");

        if (o[key] == iTitleColor.val()) {
            sTitleColor.val(o[key]);
        }
    }

    SetAjaxDiv("ok", false, "С��ʾ���������Ժ���Ե�ҳ���ŷ�����");
}


var _Speciality = [];
$(document).ready(function () {


    $.get("../Common/AllNewsSpeciality.aspx?skinid=" + $("#iSkinId").val() + "&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            data = data.substring(4, data.length);
            eval(data);

            GetSpecialityEnmu($("#iSpeciality_cc"), $("#iSkinId").val(), "0");

            Menu.init("iSpeciality_c");
        });

    //��ʼ������ѡ��ؼ�
    GetCagetegoriesEnmu($("#Cagetorie_c"), $("#iSkinId").val(), "0");

    Menu.init("gamelist");
    $("#SelectDivW").bind('click', function (e) {
        if ($("#gamelist_c").css('display') == 'block') {
            $("#gamelist_c").hide();
        } else {
            $("#gamelist_c").show();
        }
        e.stopPropagation();

    });
    
    $("#SelectDivWW").bind('click', function (e) {
        if ($("#iSpeciality_1").css('display') == 'block') {
            $("#iSpeciality_1").hide();
        } else {
            $("#iSpeciality_1").show();
        }
        e.stopPropagation();

    });

    $(document).bind('click', function (e) {

        if ($("#gamelist_c").css('display') == 'block') {
            $("#gamelist_c").hide();
        }

        if ($("#iSpeciality_1").css('display') == 'block') {
            $("#iSpeciality_1").hide();
        }
    });

    var form1 = $("#form1");
    var options;
    options = {
        beforeSubmit: CheckAdd,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

    ClassInit();

    SetFromsByNum("a1");

    PropertieInit();

});

//��ʼ������
function PropertieInit() {
    var a2from = $("#a3_form_p");
    a2from.html("");
    if (_Properties != null) {
   
        for (var i = 0; i < _Properties.length; i++) {
            var cp = _Properties[i];
            var po = GetResourceByCpid(cp.Id);
            var oth = ResourcePropertiesHTMLADD(cp, po);
            var ohtml = a2from.html();
            a2from.html(ohtml + oth);
        }
    }
}

function GetResourceByCpid(cpid) {
    if (_ResourceProperties == null || _ResourceProperties.length == 0) return null;
    for (var i = 0; i < _ResourceProperties.length; i++) {
        var cp = _ResourceProperties[i];
        if (cp.PropertieId == cpid) {
            return cp;
        }
    }
    return null;
}

function ChangePCtype(obj) {

    $.get("../Common/CategorieProperties.aspx?cid=" + $(obj).val() + "&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            data = data.substring(4, data.length);
            eval(data);
            PropertieInit();

        });
}

var objss = ["a1","a2","a3"];
function SetFromsByNum(lb) {

    for (var i = 0; i < objss.length; i++) {
        var obj = $("#" + objss[i]);
        if (lb == objss[i]) {
            obj.attr("class", "ln-c-mid on");
            obj.unbind("mouseover");
            obj.unbind("mouseout");
            obj.unbind("click");
            $("#" + objss[i] + "_from").show();
        } else {
            obj.attr("class", "");
            obj.unbind("mouseover");
            obj.unbind("mouseout");
            obj.unbind("click");
            obj.mouseover(function () { this.className = "moson"; });
            obj.mouseout(function () { this.className = ""; });
            obj.attr("href", "javascript:GoTo();");
            $("#" + objss[i] + "_from").hide();
        }
    }
}

function UpLodatFileBack() {

}


function ProperDivShowChange(id) {
    var bonck = $("#B2_" + id);
    var pat = $("#pat_" + id);
    if (bonck.attr("class") == "fn-bg2 Bopned") {
        bonck.attr("class", "").attr("class", "fn-bg2 Bclded");
        pat.hide();
    } else {
        bonck.attr("class", "").attr("class", "fn-bg2 Bopned");
        pat.show();
    }
}

function ResourcePropertiesHTMLADD(cpobj,rpobj) {

    if (cpobj == null) return;

    var resourceid = rpobj == null ? "" : rpobj.ResourceId;
    var resourcepid = rpobj == null ? "" : rpobj.Id;

    var height = cpobj.height < 80 ? 80 : cpobj.height;
    var text = "<div id=\"" + cpobj.Id + "\" >"
    text += "<input name=\"ptname_" + cpobj.Id + "\" type=\"hidden\" id=\"ptname_" + cpobj.Id + "\" value=\"" + cpobj.ProertieName + "\" />";
    text += "<input name=\"cpid_" + cpobj.Id + "\" type=\"hidden\" id=\"cpid_" + cpobj.Id + "\" value=\"" + cpobj.Id + "\" />";
    text += "<input name=\"rpid_" + cpobj.Id + "\" type=\"hidden\" id=\"rpid_" + cpobj.Id + "\" value=\"" + resourcepid + "\" />";

    text += "<div onclick=\"ProperDivShowChange('" + cpobj.Id + "');\" class=\"g-title-2 fn-hand\">"
            + "<b class=\"fn-bg2 Bopned\" id=\"B2_" + cpobj.Id + "\"></b><h3>" + cpobj.ProertieName + "</h3>"
            + "</div><div class=\"ln-c-mid ln-thin\"></div><div id=\"pat_" + cpobj.Id + "\" style=\"text-align:left;\">"

    var revalue = rpobj == null ? "" : rpobj.PropertieValue;
    if (cpobj.Type == "01") {
        if (cpobj.height < 80) {

            text += "<input id=\"rpvalue_" + cpobj.Id + "\" name=\"rpvalue_" + cpobj.Id + "\" "
                + "type=\"text\"  class=\"itxt1\" onfocus=\"this.className='itxt2'\" "
                + "onblur=\"CheckValueIsNull('iKeyWords','keymsg');\" "
                + "style=\"width:800px; margin-top:15px;margin-left:35px;margin-bottom:10px;\" value=\"" + revalue + "\" />";
        } else {
            text += "<textarea rows=\"10\" cols=\"1\" id=\"rpvalue_" + cpobj.Id + "\" name=\"rpvalue_" + cpobj.Id + "\" "
                + "class=\"itxt1\" onfocus=\"this.className='itxt2'\" "
                + "onblur=\"CheckValueIsNull('iKeyWords','keymsg');\" "
                + "style=\"width:800px; height:" + cpobj.height + "px; margin-top:10px;margin-left:35px;margin-bottom:10px;\">" + revalue + "</textarea>";
        }
    } else if (cpobj.Type == "02") {
        text += "<select style=\"margin-top:5px;margin-left:35px;margin-bottom:10px;\" id=\"rpvalue_" + cpobj.Id + "\" name=\"rpvalue_" + cpobj.Id + "\">";
        text += "<option value=\"\">��ѡ��" + cpobj.ProertieName + "</option>";
        if (cpobj.Values.indexOf("|") > -1) {
            var vales = cpobj.Values.toString().split("|");
            for (var i = 0; i < vales.length; i++) {
                var checkh = revalue.indexOf(vales[i]) > -1 ? "selected" : "";
                text += "<option value=\"" + vales[i] + "\" " + checkh + ">" + vales[i] + "</option>";
            }
        } else {
            var checkh = revalue.indexOf(cpobj.Values) > -1 ? "selected" : "";
            text += "<option value=\"" + cpobj.Values + "\"  " + checkh + ">" + cpobj.Values + "</option>";
        }

        text += "</select>";
    } else if (cpobj.Type == "03") {
        text += "<div style=\"margin-top:5px;margin-left:35px;margin-bottom:10px;\">";
        if (cpobj.Values.indexOf("|") > -1) {
            var vales = cpobj.Values.toString().split("|");
            for (var i = 0; i < vales.length; i++) {
                var checkh = revalue.indexOf(vales[i]) > -1 ? "checked" : "";
                text += "<label><input type=\"checkbox\" name=\"rpvalue_" + cpobj.Id + "\" value=\"" + vales[i] + "\"  " + checkh + "/>" + vales[i] + "</label>";
            }
        } else {
            var checkh = revalue.indexOf(cpobj.Values) > -1 ? "checked" : "";
            text += "<label><input type=\"checkbox\" name=\"rpvalue_" + cpobj.Id + "\" value=\"" + cpobj.Values + "\"  " + checkh + "/>" + cpobj.Values + "</label>";
        }
        text += "</div>";
         
    }
    text += "</div></div>";
    return text;
}