//--------------
var ajax = new AJAXRequest();
var CreateDiv=new CreateDiv();
CreateDiv.Default={w:-230,h:-455};


var a="";
function classTitleInit(){
	var m=$("classTitle");
	var classobj=$("iClassId");
	if(m==null||classobj==null)return;
	if(classobj.value=="0"){
		m.innerHTML="<span class='txt bold'>网站名称</span><span class='info1'>(资讯站点根目录)</span>";
	}else{
		a="";
		GetNewsListTitleByClassId(classobj.value);
		a="<a href='?'>站点根目录</a>>>"+a;
		m.innerHTML="<span class='txt bold'>"+a+"</span>";
	}
}

function GetParentTitle(){
	var o=$("iClassId");
	if(o==null)return;
	var placemsg=$("placemsg");
	if(o.value=="0"){
		placemsg.innerHTML += "作为网站分类";
	}else{
		a="";
		GetNewsListTitleByClassIdW(o.value);
		a="根类别>>"+a;
		placemsg.innerHTML += a;
	}
}

function GetNewsListTitleByClassId(classid){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(NewsLis[i][0]==classid){
			var t=(NewsLis[i][1]==0)?"":">>";
			a =t+"<a href='?iParentId="+NewsLis[i][0]+"'>"+NewsLis[i][2]+"</a>"
				+"<span class='info2'>("+NewsLis[i][3]+")</span>"+a;
			GetNewsListTitleByClassId(NewsLis[i][1]);
		}
	}
}

function GetNewsListTitleByClassIdW(classid){
	if(NewsLis==null)return;
	for(var i=0;i<NewsLis.length;i++){
		if(NewsLis[i][0]==classid){
			var t=(NewsLis[i][1]==0)?"":">>";
			a =t+NewsLis[i][2]+a;
			GetNewsListTitleByClassIdW(NewsLis[i][1]);
		}
	}
}


function CreatClass(obj){
	var co=$("CreateClassDiv");
	if(co==null)return;
	var movediv=null;
	var CBackg=$("CBackg");
	if(DivMoves.length==0){
		movediv = new DivMove();
		movediv.obj=obj;
		movediv.Mobj=co;
		movediv.DefaultPos={w:186,h:88};
		movediv.newwid={w:600,h:369};
		movediv.times=20;
		movediv.a=3;
		movediv.bAction=true;
		var w=(document.all==null)?(movediv.Screen.w-189):(movediv.Screen.w-193);
		var h=(document.all==null)?(movediv.Screen.h-197):(movediv.Screen.h-194);
		CBackg.style.width=w+"px";
		CBackg.style.height=h+"px";
		CBackg.className="CBackg";
		DivMoves[DivMoves.length]=movediv;
		movediv.MoveToSrceenM("SetCreateInnerHTML()");
	}else{
		movediv=DivMoves[0];
	}
}

function EditClass(obj){
	var co=$("CreateClassDiv");
	if(co==null)return;
	var movediv=null;
	var CBackg=$("CBackg");
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要编辑的分类！");
		return;
	}
	if(temps.indexOf(",")>-1){
		SetAjaxDiv("err",false,"您选择了多个资讯类别！");
		return;
	}
	if(DivMoves.length==0){
		movediv = new DivMove();
		movediv.obj=obj;
		movediv.Mobj=co;
		movediv.DefaultPos={w:186,h:88};
		movediv.newwid={w:600,h:369};
		movediv.times=20;
		movediv.a=3;
		movediv.bAction=true;
		var w=(document.all==null)?(movediv.Screen.w-189):(movediv.Screen.w-193);
		var h=(document.all==null)?(movediv.Screen.h-197):(movediv.Screen.h-194);
		CBackg.style.width=w+"px";
		CBackg.style.height=h+"px";
		CBackg.className="CBackg";
		DivMoves[DivMoves.length]=movediv;
		movediv.MoveToSrceenM("SetMdyInnerHTML()");
	}else{
		movediv=DivMoves[0];
	}
}

function SetMdyInnerHTML(){
	var ifCreateAdd=$("ifCreateAdd");
	var CreateClassDivClose=$("CreateClassDivClose");
	ifCreateAdd.style.width="100%";
	ifCreateAdd.style.height="100%";
	ifCreateAdd.src="classmdy.aspx?iClassId="+GetCheckBoxValues("CheckID");
	CreateClassDivClose.className="CreateClassDivClose";
}

function SetCreateInnerHTML(){
	var ifCreateAdd=$("ifCreateAdd");
	var CreateClassDivClose=$("CreateClassDivClose");
	var par=$("iClassId");
	ifCreateAdd.style.width="100%";
	ifCreateAdd.style.height="100%";
	ifCreateAdd.src="classadd.aspx?iParentId="+par.value;
	CreateClassDivClose.className="CreateClassDivClose";
}

function CreatClassClose(){
	var movediv=null;
	var CBackg=$("CBackg");
	if(DivMoves.length==0)return;
	CBackg.className="CBackg hid";
	movediv=DivMoves[0];
	movediv.CloseDiv("SetCloseBack()");
}

function SetCloseBack(){
	var co=$("CreateClassDiv");
	var CreateClassDivClose=$("CreateClassDivClose");
	co.style.borderWidth="0px";
	CreateClassDivClose.className="CreateClassDivClose hid";
	var ifCreateAdd=$("ifCreateAdd");
	ifCreateAdd.style.width="0px";
	ifCreateAdd.style.height="0px";
	if(DivMoves.length==0)return;
	DivMoves[0]=null;
	DivMoves.length=0;
	refinsh();
}

function NewsClassDel(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没选择需要删除的分类！");
		return;
	}
	if(temps.indexOf(",")>-1){
		SetAjaxDiv("err",false,"一次只能删除一个模版！");
		return;
	}
	$("DelClassId").value=temps;
	$("work").value="DEL";
	SetAjaxDiv("loader",false,"正在发送请求...");
	ajax.postf($("form1"),function(obj) { DelPostBack(obj.responseText);});
}

function DelPostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"分类已经删除成功！");
}

function NewsClassCreateHtml(){
	var temps=GetCheckBoxValues("CheckID");
	if(temps==""){
		SetAjaxDiv("err",false,"您没有选择需要生成的分类！");
		return;
	}
	if(temps.indexOf(",")>-1){
		var ts=temps.split(",");
		var ss="";
		for(var i=0;i<ts.length;i++){
			var tt=GetPostClassChild(ts[i]);
			ss +=(ss=="")?tt:","+tt;
		}
		PostClasses(ss);
	}else{
		var t =GetPostClassChild(temps);
		if(t.indexOf(",")>-1){
			PostClasses(t);
		}else{
			CreateDiv.Start("批量生成分类列表");
			CreateDiv.set =1;
			CreateDiv.setcount=1;
			$("work").value="Create";
			$("DelClassId").value=t;
			ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
		}
	}
}
function PostClasses(ids){
	var o=ids.split(",");
	CreateDiv.Start("批量生成分类列表");
	CreateDiv.set =1;
	CreateDiv.setcount=o.length;
	for(var i=0;i<o.length;i++){
		$("work").value="Create";
		$("DelClassId").value=o[i];
		ajax.postf($("form1"),function(obj) { CreateBack(obj.responseText);});
	}
}

function GetPostClassChild(id){
	var t=GetAllChildClassIdByClassId(id);
	if(t==""){
		return id
	}else{
		return id+","+t;
	}
}

function CreateBack(val){
	CreateDiv.SetSep(val);
}