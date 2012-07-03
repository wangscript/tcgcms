var Pstr = "";
var Plever = 0;
var Pico = "└ ";
var level = 0;

function PopSelectInit(){
    if (_Popedom.length == 0) return;
	var vcPopedom = $("#vcPopedom");
	if(vcPopedom.length==0)return;
	var popedom = $("#popedom"); 
	var r=110,g=187,b=88;
	var oOption = document.createElement("OPTION");
	oOption.text = "不选择任何权限...";
	oOption.value = 0;
	oOption.style.cssText="background-color:"+GetColor(r,g,b)+";"
	vcPopedom[0].options.add(oOption);
	oOption=null;
	PopLisFind(0, vcPopedom, _Popedom, popedom.val(), 0, 110, 180, 50);
}

function PopLisFind(iParentId, obj, Lis, sList, level, r, g, b) {
    var olevel = 1;
    var lastParentId = -1;

    for (var i = 0; i < Lis.length; i++) {
    
        if (Lis[i].ParentId == iParentId) {
            
            if (lastParentId != iParentId) {
                level++; lastParentId = iParentId;
                r += 12; g += 12; b += 12;
            }
            var oOption = document.createElement("OPTION");
        
            oOption.text = GerPsrt(level) + ((level != 1) ? Pico : "") + Lis[i].Name;

            oOption.value = Lis[i].Id;
            if (IntInList(Lis[i].Id, sList, ",")) oOption.selected = true;
            oOption.style.cssText = "background-color:" + GetColor(r, g, b) + ";"
            obj[0].options.add(oOption);
            oOption = null;
            PopLisFind(Lis[i].Id, obj, Lis, sList, level, r, g, b);
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
    if (_Categories.length == 0) return;
	var vcClassPopedom = $("#vcClassPopedom");
	if(vcClassPopedom.length==0)return;
	var classpopedom = $("#classpopedom"); 
	var r=110,g=187,b=88;
	var oOption = document.createElement("OPTION");
	oOption.text = "不选择任何权限...";
	oOption.value = 0;
	oOption.style.cssText="background-color:"+GetColor(r,g,b)+";"
	vcClassPopedom[0].options.add(oOption);
	oOption = null;
	
	PopLisFind($("#DefaultSkinId").val(), vcClassPopedom, _Categories, classpopedom.val(), 0, 110, 187, 88);
}

function CheckNewPassword(obj){
	obj = $(obj);
	obj.get(0).className="itxt1";
	var cp = $("#iCPWD");
	var og = $("#npwdmsg");
	if(og==null)return false;
	if(obj.val() == ""){
		og.get(0).className = "info_err";
		SetInnerText(og,"新密码不能为空!");
		return false;
	}else{
		if(cp.val()!=obj.val()){
			og.get(0).className = "info1";
			SetInnerText(og,"请保证确认密码和新密码一样!");
			return false;
		}else{
			og.get(0).className = "info_ok";
			SetInnerText(og,"输入正确!");
			return true;
		}
	}
}

function CheckCPWD(obj) {
    obj = $(obj);
    if (obj == null && obj.length == 0) return false;
	obj.get(0).className="itxt1";
	var cp = $("#iNewPWD");
	var og = $("#cpwdmsg");
	if(og==null)return false;
	if(cp.val()!=obj.val()){
		og.get(0).className = "info_err";
		SetInnerText(og,"请保证确认密码和新密码一样!");
		return false;
	}else{
		og.get(0).className = "info_ok";
		SetInnerText(og,"输入正确!");
		return true;
	}
}