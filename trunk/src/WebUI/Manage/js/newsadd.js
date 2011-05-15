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
    $("iContent_content").value = val;
    $("form1").action = "newsadd.aspx";
	var work = $("work");
	if(work.value=="AddNew"){
		ajax.postf($("form1"),function(obj) { NewsAddPostBack(obj.responseText);});
	}else{
		ajax.postf($("form1"),function(obj) { NewsMdyPostBack(obj.responseText);})
	}
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
	var iContent = $("#iContent_content").val();
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
	var iContent = $("#iContent_content").val();
	
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
	var iClassName=$("#iClassName");
	var iClassId=$("#iClassId");
	iClassName.val(txt);
	iClassId.val(val);
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

$(document).ready(function() {

    //��ʼ������ѡ��ؼ�
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
        beforeSubmit: CheckAdd,
        dataType: 'json',
        success: AjaxPostFormBack
    };
    form1.ajaxForm(options);

    ClassInit();

});

function UpLodatFileBack() {

}