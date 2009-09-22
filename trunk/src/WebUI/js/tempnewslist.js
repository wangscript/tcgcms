//--------------
var ajax = new AJAXRequest();
var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};

function classTitleInit(){
	var m=$("classTitle");
	var classobj=$("iSiteId");
	var SytemType=$("SytemType");
	var iParentid=$("iParentid");
	if(m==null||classobj==null)return;
	if(classobj.value!="0"){
		m.innerHTML=GetClassTitleById(classobj.value)+ GetTempTitleBy(classobj.value,SytemType.value,iParentid.value);
	}
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

function TempDel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要删除的模版！");
		return;
	}
	var t=CheckTempsUsed(temps);
	if(t==""){
		SetAjaxDiv("err",false,"您选择的模版，正在使用中，无法删除！");
		return;
	}
	if(t!=temps){
		var CheckBoxMain=$("CheckBoxMain");
		CheckBoxMain.checked=false;
		SetCheckBoxBg('CheckID',CheckBoxMain);
		SetCheckBox("CheckID",GetCheckedBoxSetting(t));
		SetAjaxDiv("err",false,"您选择的模版中含有正在使用的模版，系统已经自动选择可以删除的部分！");
		return;
	}
	$("temps").value=t;
	$("work").value="DEL";
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.postf($("form1"),function(obj) { DelPostBack(obj.responseText);});
}

function DelPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"模版已经删除成功！");
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
		if(parseInt(temp)==parseInt(NewsLis[i][4]))return true;
	}
	return false;
}

function AddTemplate(){
	var iSiteId=$("iSiteId");
	var SytemType=$("SytemType");
	var iParentid=$("iParentid");
	window.location.href="newtemplateadd.aspx?iSiteId="+iSiteId.value+"&SytemType="+SytemType.value+"&iParentid="+iParentid.value;
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
		var iParentid=$("iParentid");
		var iSiteId=$("iSiteId");
		window.location.href="?iSiteId="+iSiteId.value+"&iParentid="+iParentid.value;
	}else{
		var iParentid=$("iParentid");
		var iSiteId=$("iSiteId");
		window.location.href="?iSiteId="+iSiteId.value+"&iType="+obj.value+"&iParentid="+iParentid.value;
	}
}

function PageCreat(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要生成的模版！");
		return;
	}
	var work=$("work");
	var iTemplateId=$("iTemplateId");
	if(temps.indexOf(",")>-1){
		var o=temps.split(",");
		CreateDiv.Start("生成单页模版文件");
		CreateDiv.set =1;
		for(var i=0;i<o.length;i++){
			var t=GetTemplateInfoById(o[i]);
			if(t==null)continue;
			if(t[5]!=0)continue;
			work.value="Create";
			iTemplateId.value=o[i];
			CreateDiv.setcount++;
			ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
			
		}
	}else{
		var t2=GetTemplateInfoById(temps);
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
		work.value="Create";
		iTemplateId.value=temps;
		ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
	}
}

function CreateBack(val){
	CreateDiv.SetSep(val);
}