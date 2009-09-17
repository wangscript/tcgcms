var Pstr = "";
var Plever = 0;
var Pico = "└ ";
var level =0;
function PopSelectInit(){
	if(PopLis.length==0)return;
	var vcPopedom = $("vcPopedom");
	if(vcPopedom==null)return;
	var popedom = $("popedom"); 
	var r=110,g=187,b=88;
	var oOption = document.createElement("OPTION");
	oOption.text = "不选择任何权限...";
	oOption.value = 0;
	oOption.style.cssText="background-color:"+GetColor(r,g,b)+";"
	vcPopedom.options.add(oOption);
	oOption=null;
	PopLisFind(0,vcPopedom,PopLis,popedom.value,0,110,180,50);
}

function PopLisFind(iParentId,obj,Lis,sList,level,r,g,b){
	var olevel=1;
	var lastParentId=-1;
	for(var i =0;i<Lis.length;i++){
		if(Lis[i][1]==iParentId){
			if(lastParentId!=iParentId){
				level++;lastParentId=iParentId;
				r+=12;g+=12;b+=12;
			}
			var oOption = document.createElement("OPTION");
			oOption.text = GerPsrt(level) + ((level!=1)?Pico:"") + Lis[i][2];
			oOption.value = Lis[i][0];
			if(IntInList(Lis[i][0],sList,","))oOption.selected=true;
			oOption.style.cssText="background-color:"+GetColor(r,g,b)+";"
			obj.options.add(oOption);
			oOption=null;
			PopLisFind(Lis[i][0],obj,Lis,sList,level,r,g,b);
		}
	}
	
}

function GerPsrt(num){
	var str="";
	for(var i=0;i<(num-1);i++){
		str+="　";
	}
	return str;
}
function NewsSelectInit(){
	if(NewsLis.length==0)return;
	var vcClassPopedom = $("vcClassPopedom");
	if(vcClassPopedom==null)return;
	var classpopedom = $("classpopedom"); 
	var r=110,g=187,b=88;
	var oOption = document.createElement("OPTION");
	oOption.text = "不选择任何权限...";
	oOption.value = 0;
	oOption.style.cssText="background-color:"+GetColor(r,g,b)+";"
	vcClassPopedom.options.add(oOption);
	oOption=null;
	PopLisFind(0,vcClassPopedom,NewsLis,classpopedom.value,0,110,187,88);
}

function CheckNewPassword(obj){
	obj.className="itxt1";
	var cp = $("iCPWD");
	var og = $("npwdmsg");
	if(og==null)return false;
	if(obj.value == ""){
		og.className = "info_err";
		SetInnerText(og,"新密码不能为空!");
		return false;
	}else{
		if(cp.value!=obj.value){
			og.className = "info1";
			SetInnerText(og,"请保证确认密码和新密码一样!");
			return false;
		}else{
			og.className = "info_ok";
			SetInnerText(og,"输入正确!");
			return true;
		}
	}
}

function CheckCPWD(obj){
	obj.className="itxt1";
	var cp = $("iNewPWD");
	var og = $("cpwdmsg");
	if(og==null)return false;
	if(cp.value!=obj.value){
		og.className = "info_err";
		SetInnerText(og,"请保证确认密码和新密码一样!");
		return false;
	}else{
		og.className = "info_ok";
		SetInnerText(og,"输入正确!");
		return true;
	}
}