/// <reference path="jquery-1.3.1-vsdoc.js" />

function SaveNewsInfo() {
    setContent();
    $("#form1").submit();
}

function CheckAdd(){
	if(!(CheckValueIsNull('iTitle','titlemsg')&&CheckValueIsNull('iClassName','classmsg')&&CheckValueIsNull('iAuthor','autmsg')&&CheckValueIsNull('iKeyWords','keymsg'))){
		return false;
	}
	SetAjaxDiv("loader", false, "正在发送请求...");
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
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetSmallImge('"+RegExp.$1+"')\" />设置</li></ul>";
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
			imgPace.innerHTML +="<ul><img src=\""+RegExp.$1 +"\" /><li><input name=\"CheckImg\" "+c+" type=\"radio\" onclick=\"SetBigImge('"+RegExp.$1+"')\" />设置</li></ul>";
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
	SetAjaxDiv("ok",false,"资讯成功修改并生成文件！");
}

function NewsAddPostBack() {
	var iTitle=$("#iTitle");
	var iAuthor=$("#iAuthor");
	var iKeyWords=$("#iKeyWords");
	var msg = "资讯["+iTitle.val()+"]已经成功添加，不继续添加请点取消！";
	SetAjaxDiv("ok",false,msg);
	iTitle.val("");
	iAuthor.val("");
	iKeyWords.val("");
}

function SelectClassValue(val, txt) {
	var iClassName=$("#iClassName");
	var iClassId=$("#iClassId");
	iClassName.val(txt);
	iClassId.val(val);
}

var tempWork;
function ClassInit() {

    ColorInit(); //初始化颜色选择
    var iClassName = $("#iClassName");
    var iClassId = $("#iClassId");
    var SelectDivW = $("#SelectDivW");

    var o = GetCategorieById(iClassId.val());
    if (o == null) {
        iClassName.val("请选择资讯分类...");
    } else {
        iClassName.val(o.ClassName);
    }

}


//颜色选择初始化
function ColorInit() {
    var sTitleColor = $("#sTitleColor");
    var iTitleColor = $("#iTitleColor");
    sTitleColor.append("<option value=''>请选择颜色</option>");

    var o = { "White": "#FFFFFF", "Green": "#008000", "Silver": "#C0C0C0", "Lime": "#00FF00", "Gray": "#808080", "Olive": "#808000", "Black": "#000000", "Yellow": "#FFFF00", "Maroon": "#800000", "Navy": "#000080", "Red": "#FF0000", "Blue": "#0000FF", "Purple": "#800080", "Teal": "#008080", "Fuchsia": "#FF00FF", "Aqua": "#00FFFF" };
    for (key in o) {

        sTitleColor.append("<option value='" + o[key] + "' style='background-color:" + o[key] + ";'>" + key + "</option>");

        if (o[key] == iTitleColor.val()) {
            sTitleColor.val(o[key]);
        }
    }

    SetAjaxDiv("ok", false, "小提示：发布完以后可以当页接着发布！");
}

$(document).ready(function () {

    //初始化分类选择控件
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

    $(document).bind('click', function (e) {

        if ($("#gamelist_c").css('display') == 'block') {
            $("#gamelist_c").hide();
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

    var a2from = $("#a3_from");

    if (_CategorieProperties != null) {
        for (var i = 0; i < _CategorieProperties.length; i++) {
            var cp = _CategorieProperties[i];
            var po = GetResourceByCpid(cp.Id);
            var oth = ResourcePropertiesHTMLADD(cp, po);
            var ohtml = a2from.html();
            a2from.html(ohtml + oth);
        }
    }

});

function GetResourceByCpid(cpid) {
    if (_ResourceProperties == null || _ResourceProperties.length == 0) return null;
    for (var i = 0; i < _ResourceProperties.length; i++) {
        var cp = _ResourceProperties[i];
        if (cp.CategoriePropertieId == cpid) {
            return cp;
        }
    }
    return null;
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

    var height = cpobj.height < 80 ? 80 : cpobj.height;
    var text = "<div id=\"" + cpobj.Id + "\" >"
    text += "<input name=\"ptname_" + cpobj.Id + "\" type=\"hidden\" id=\"ptname_" + cpobj.Id + "\" value=\"" + cpobj.ProertieName + "\" />";
    text += "<input name=\"cpid_" + cpobj.Id + "\" type=\"hidden\" id=\"cpid_" + cpobj.Id + "\" value=\"" + cpobj.Id + "\" />";
    text += "<input name=\"rpid_" + cpobj.Id + "\" type=\"hidden\" id=\"rpid_" + cpobj.Id + "\" value=\"" + resourceid + "\" />";

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
        text += "<option value=\"\">请选择" + cpobj.ProertieName + "</option>";
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
    /*
    <div id="cp_" style="height:100px;">
            <div onclick="MM.personal.switchFolder(0)" class="g-title-2 fn-hand">
                <b class="fn-bg2 Bopned" id="psnB0"></b><h3>基本信息</h3>
            </div>
            <div id="line_" class="ln-c-mid ln-thin"></div>

            <div class="Page_arrb arb_pr1">
                <input id="Text1" name="iKeyWords" 
                type="text"  class="itxt1" onfocus="this.className='itxt2'" 
                onblur="CheckValueIsNull('iKeyWords','keymsg');" 
                style="width:400px; margin-top:5px;margin-left:15px;" />
	        </div>
        </div>

         <div id="Div1" style="height:100px;">
            <div onclick="MM.personal.switchFolder(0)" class="g-title-2 fn-hand">
                <b class="fn-bg2 Bopned" id="B1"></b><h3>基本信息</h3>
            </div>
            <div id="Div2" class="ln-c-mid ln-thin"></div>

            <div class="Page_arrb arb_pr1">
               <textarea rows="10" cols="1" id="Textarea1" name="iShortContent" 
                class="itxt1" onfocus="this.className='itxt2'" 
                onblur="CheckValueIsNull('iKeyWords','keymsg');" 
                style="width:800px; height:80px; margin-top:5px;margin-left:15px;"></textarea>
	        </div>
        </div>

        <div id="Div3" style="height:100px;">
            <div onclick="ProperDivShowChange('Div3');" class="g-title-2 fn-hand">
                <b class="fn-bg2 Bopned" id="B2_Div3"></b><h3>基本信息</h3>
            </div>
            <div id="Div4" class="ln-c-mid ln-thin"></div>

            <div class="Page_arrb arb_pr1" id="pat_Div3">
               <select style="margin-top:5px;margin-left:15px;">
                    <option>测试测是个</option>
                     <option>测试测是个</option>
                      <option>测试测是个</option>
                       <option>测试测是个</option>
                        <option>测试测是个</option>
                         <option>测试测是个</option>
                          <option>测试测是个</option>
               </select>
	        </div>
        </div>

        <div id="Div5" style="height:100px;">
            <div onclick="MM.personal.switchFolder(0)" class="g-title-2 fn-hand">
                <b class="fn-bg2 Bopned" id="B3"></b><h3>基本信息</h3>
            </div>
            <div id="Div6" class="ln-c-mid ln-thin"></div>

            <div class="Page_arrb arb_pr1">
               <label><input type="checkbox" name="checkbox2" value="checkbox" />测测是个</label>
                <label><input type="checkbox" name="checkbox2" value="checkbox" />测试是个</label>
                 <label><input type="checkbox" name="checkbox2" value="checkbox" />测试测是个</label>
                  <label><input type="checkbox" name="checkbox2" value="checkbox" />测试测个</label>
                   <label><input type="checkbox" name="checkbox2" value="checkbox" />试测是个</label>
	        </div>
        </div>
        */
    text += "</div></div>";
    return text;
}