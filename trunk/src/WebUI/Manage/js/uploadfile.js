var ajax = new AJAXRequest();

var itemidstr="filesitems_";
function SeBack(val){
	var tFiles=$("file_"+Items.length);
	tFiles.className="uploaddiv_info_title_emu hid";
	Items[Items.length]={title:GetFileName(val),id:Items.length,stat:"等待处理",obj:tFiles,Timer:null,Sep:170};
	AddItemToInfo(Items[Items.length-1]);
	if(Items.length<FileCount){
		CreateFileInput(Items.length);
	}
	uploaddiv_info_title.innerHTML = "您选择了<span class='bold'>"+GetFileNum()+"</span>个文件！";
}

function CheckFileType(str){
	var filename = GetFileName(str);
	var exps = filename.substring(filename.lastIndexOf(".")+1,filename.length)
	for(var i=0;i<FileType.length;i++){
		if(exps==FileType[i])return true;
	}
	return false;
}

function StartUpload(){
	setps=0;
	for(var i=0;i<Items.length;i++){
		if(Items[i]!=null){
			Items[i].Sep=170;
			StartUpdateById(Items[i].id);
			return;
		}
	}
}

function GetFileNum(){
	var num=0;
	for(var i=0;i<Items.length;i++){
		if(Items[i]!=null){
			num++;
		}
	}
	return num;
}

function DelAllUpload(){
	var tFiles=$("file_"+Items.length);
	uploaddiv_info.removeChild(tFiles);
	CreateFileInput(0);
	for(var i=0;i<Items.length;i++){
		if(Items!=null){
			var fItem =$(itemidstr+Items[i].id);
			uploaddiv_info.removeChild(fItem);
			uploaddiv_info.removeChild(Items[i].obj);
		}
	}
	Items.splice(0,Items.length);
	uploaddiv_info_title.innerHTML = "您选择了<span class='bold'>"+GetFileNum()+"</span>个文件！";
}
function CFileById(id){
	var o =Items[id];
	if(o!=null){
		var fItem =$(itemidstr+id);
		uploaddiv_info.removeChild(fItem);
		uploaddiv_info.removeChild(Items[id].obj);
		delete Items[id];
		var tFiles=$("file_"+id);
	}
	uploaddiv_info_title.innerHTML = "您选择了<span class='bold'>"+GetFileNum()+"</span>个文件！";
}

//根据ID启动上传
function StartUpdateById(id){
	var sid="sep_"+id;
	var o=$(sid);
	o.innerHTML = "";
	o.className="emu_state2";
	Items[id].obj.contentWindow.Save();
	Items[id].Timer=setInterval("changesepbg('"+id+"')",10);
}

var setps=0;
var Backvalue=[];
function UpdateBack(id, val) {
	Items[id].Sep=2;
	var sid="sep_"+id;
	var o=$(sid);
	eval("Backvalue["+setps+"]={"+val+"};");
	ChangeStatc(id,Backvalue[setps]);
	setps++;
	o.style.backgroundPosition="0px 0px";
	clearInterval(Items[id].Timer);
	if(setps>=GetFileNum()){
		$("uploadfileFinsh").className="uploadfileFinsh";
		TimeClear=setInterval("ClearUpdateFile()",1800);
	}
	for(var i=(id+1);i<Items.length;i++){
		if(Items[i]!=null){
			Items[i].Sep=170;
			StartUpdateById(Items[i].id);
			return;
		}
	}
}

var TimeClear=null;
function ClearUpdateFile() {
	eval("window.parent."+CallBack+"(Backvalue)");
	window.parent.document.body.removeChild(window.parent.$("uploadfilebg"));
	window.parent.document.body.removeChild(window.parent.$("uploaddiv"));
	clearInterval(TimeClear)
}

function ChangeStatc(id,obj){
	if(obj.Err!=null){
		if(obj.Err!=""){
			var sid="sep_"+id;
			var o=$(sid);
			o.innerHTML =obj.Err;
			o.className="emu_state";
		}
	}
}

function changesepbg(id){
	var sid="sep_"+id;
	var o=$(sid);

	o.style.backgroundPosition="-"+Items[id].Sep+"px 0px";
	Items[id].Sep=Items[id].Sep-2;
	if(Items[id].Sep<-1)clearInterval(Items[id].Timer);
}

var Items=[];

var filelistItem = "<span class=\"emu_tittle\">$title$</span><div id=\"sep_$sid$\" class=\"emu_state\">$stat$</div><span class=\"emu_manage\"><a href=\"javascript:GoTo();\" onclick=\"CFileById($id$)\" class=\"emu_manage_a\"></a></span>";

var uploaddiv_info=$("uploaddiv_info");
var uploaddiv_info_title=$("uploaddiv_info_title");

function AddItemToInfo(obj){
	var fItem = document.createElement("DIV");
	fItem.innerHTML = filelistItem.replace("$title$",obj.title).replace("$stat$",obj.stat).replace("$id$",obj.id).replace("$sid$",obj.id);
	fItem.className="uploaddiv_info_title_emu";
	fItem.setAttribute("id",itemidstr+obj.id);
	uploaddiv_info.appendChild(fItem);
}

function CreateFileInput(id){
	var sid="file_"+id;
	var fItem = document.createElement("IFRAME");
	fItem.name=sid;
	fItem.setAttribute("id",sid);
	fItem.className="fileitem";
	fItem.style.top="8px";
	fItem.style.left="-33px";
	fItem.cssText ="z-index:"+(30+id);
	fItem.src="uploadSave.aspx?ifid="+id+"&classid="+$("iClassId").value;
	uploaddiv_info.appendChild(fItem);
	return fItem;
}

CreateFileInput(0);

function GetFileName(str){
	return str.substring(str.lastIndexOf("\\")+1,str.length);
}