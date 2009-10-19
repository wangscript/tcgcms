//--------------
var ajax = new AJAXRequest();

function onPop2(obj,action){
	if(obj==null)return;
	if(obj.className=="pop1 popbg")return;
	if(action==0){
		obj.className='pop2 popbg'
	}else if(action==1){
		obj.className='pop2'
	}
}

function onPop(obj){
	if(obj==null)return;
	obj.className = "pop1 popbg";
	for(var i=1;i<emunum+1;i++){
		var o = $("l_" + i);
		if(o==null)continue;
		if(o.id!=obj.id){
			o.className = "pop2";
		}
	}
}

var GroupDiv = new MenuDiv();
function GetAddGroupDiv(){
	var sa = {href:"javascript:GoTo();",onclick:"window.parent.adminmain.location.href='adminroleadd.aspx';",Text:"增加新角色组"};
	GroupDiv.CreadDiv("addgroup",null,alist,150,sa,0,6);
}

function GetAddDiv(){
	var addlist = [{href:"javascript:GoTo();",onclick:"window.parent.adminmain.location.href='adminadd.aspx';",Text:"增加新管理员"},{href:"javascript:GoTo();",onclick:"window.parent.adminmain.location.href='adminroleadd.aspx';",Text:"增加新角色组"}];
	GroupDiv.CreadDiv("add",null,addlist,150,null,0,6);
}

function AddGroup(iRole){
	var admins = window.parent.adminmain.GetCheckBoxValuesForSql("CheckID");
	if(admins==""){
		window.parent.adminmain.SetAjaxDiv("err",false,"您还没选择需要移动的管理员!");
		return;
	}
	$("admins").value=admins;
	$("iRole").value=iRole;
	SetAjaxDivAdminMian("loader",false,"正在发送移动请求...");
	ajax.postf($("form1"),function(obj) { AddGroupBack(obj.responseText);});
}

function AddGroupBack(val){
	if(GetErrTextFrameRoot(val))return;
	window.parent.adminpop.location.href=window.parent.adminpop.location.href;
	SetAjaxDivAdminMian("ok",false,"成功移动管理员到指定的角色组！");
}