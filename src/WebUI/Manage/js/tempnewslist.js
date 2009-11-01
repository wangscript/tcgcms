//--------------

var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};

function classTitleInit(){
	var m=$("#classTitle");
	var classobj=$("#iSiteId");
	var SytemType=$("#SytemType");
	var iParentid=$("#iParentid");
	if (m.lenght == 0 || classobj.lenght == 0) return;

	if (classobj.val() == "0") {
	    m.innerHTML = GetClassTitleById(classobj.val()) + GetTempTitleBy(classobj.val(), SytemType.val(), iParentid.val());
	}
}


function DoSubmit(callback) {
    var form1 = $("#form1");
    if (form1.lenght == 0) return;
    var options;

    options = {
        beforeSubmit: BeforSubmit,
        dataType: 'json',
        success: callback
    };
    form1.ajaxForm(options);
}

function GetTempTitleBy(site,systemid,parentid){
	for(var i=0;i<AllTemplates.length;i++){
		if(AllTemplates[i][1]==site&&AllTemplates[i][3]==systemid&&AllTemplates[i][0]==parentid){
			return  GetTempTitleBy(site,systemid,AllTemplates[i][2])+">><a href='?iParentid="+
			parentid+"&iSiteId="+site+"'>"+ AllTemplates[i][4]+"</a>";
		}
	}
	return "";
}

function GetClassTitleById(id){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(id==NewsLis[i][0]){
			return "<span class='txt bold'><a href='?iSiteId="+id+"'>"+NewsLis[i][2]+"</a></span><span class='info1'>("+NewsLis[i][3]+")</span>";
		}
	}
	return "";
}

function TempDel() {
    DoSubmit(AjaxPostFormBack);
    var temps = GetCheckBoxValuesForSql("CheckID");
    if (temps == "") {
        SetAjaxDiv("err", false, "您没选择需要删除的模版！");
        return false;
    }
    var t = CheckTempsUsed(temps);
    if (t == "") {
        SetAjaxDiv("err", false, "您选择的模版，正在使用中，无法删除！");
        return false;
    }

    if (t != temps) {
        var CheckBoxMain = $("CheckBoxMain");
        CheckBoxMain.checked = false;
        SetCheckBoxBg('CheckID', CheckBoxMain);
        SetCheckBox("CheckID", GetCheckedBoxSetting(t));
        SetAjaxDiv("err", false, "您选择的模版中含有正在使用的模版，系统已经自动选择可以删除的部分！");
        return false;
    }
    $("#temps").val(t);
    $("#work").val("DEL");
    SetAjaxDiv("loader", false, "正在发送请求...");
    $('#form1').submit();
}

function BeforSubmit() {
    return true;
}


function CheckTempsUsed(temps){
	if(temps=="")return false;
	var str="";
	if(temps.indexOf(",")>-1){
		var t = temps.split(",");
		for(var i=0;i<t.length;i++){
			if(!CheckTempUsed(t[i])){
				var x=(str=="")?"":",";
				str+=x+t[i];
			}
		}
	}else{
		if(!CheckTempUsed(temps))str=temps;
	}
	return str;
}

function CheckTempUsed(temp){
	if(temp=="")return false;
	if(NewsLis==null)return false;
	for(var i=0;i<NewsLis.length;i++){
		if(temp==NewsLis[i][4])return true;
	}
	return false;
}

function AddTemplate(){
	var iSiteId=$("#iSiteId");
	var SytemType=$("#SytemType");
	var iParentid=$("#iParentid");
	window.location.href = "newtemplateadd.aspx?iSiteId=" + iSiteId.value + "&SytemType=" + SytemType.val() + "&iParentid=" + iParentid.val();
}
function EditTemplate(){
	var vs=GetCheckBoxValues("CheckID")
	if(vs==""){
		SetAjaxDiv("err",false,"您没选择需要编辑的模版！");
		return;
	}else{
		if(vs.indexOf(",")>-1){
			SetAjaxDiv("err",false,"一次只能编辑一个模版！");
			return;
		}
	}
	window.location.href="newtemplatemdy.aspx?templateid="+vs;
}

function sTypeChange(obj){
	if(obj.value=="-1"){
		var iParentid=$("#iParentid");
		var iSiteId=$("#iSiteId");
		window.location.href = "?iSiteId=" + iSiteId.val() + "&iParentid=" + iParentid.val();
	}else{
		var iParentid=$("#iParentid");
		var iSiteId=$("#iSiteId");
		window.location.href = "?iSiteId=" + iSiteId.val() + "&iType=" + obj.val() + "&iParentid=" + iParentid.val();
	}
}

function PageCreat() {
    DoSubmit(CreateBack); 
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要生成的模版！");
		return;
	}
	var work=$("#work");
	var iTemplateId=$("#iTemplateId");
	if(temps.indexOf(",")>-1){
		var o=temps.split(",");
		CreateDiv.Start("生成单页模版文件");
		CreateDiv.set =1;
		for(var i=0;i<o.length;i++){
			var t=GetTemplateById(o[i]);
			if(t==null)continue;
			if(t[5]!=0)continue;
			work.val("Create");
			iTemplateId.val(o[i]);
			CreateDiv.setcount++;
			$('#form1').submit();
			
		}
	}else{
		var t2=GetTemplateById(temps);
		var s=true;
		if(t2==null){
		    s=false;
		}else{
		    if(t2[5]!=0)s=false;
		}
		
		if(!s){
			SetAjaxDiv("err",false,"您选择的模版不是单页模版！");
			return;
		}
		CreateDiv.Start("生成单页模版文件");
		CreateDiv.set =1;
		CreateDiv.setcount=1;
		work.val("Create");
		iTemplateId.value=temps;
		$('#form1').submit();
	}
}

function CreateBack(data) {
	CreateDiv.SetSep(data.message);
}