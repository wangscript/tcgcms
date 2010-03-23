function list_bgchange(obj,num){
	if(obj==null)return;
	if(num==1){
		if(obj.className!="list_title_c lb_2")obj.className="list_title_c lb_1";
	}else if(num==0){
		if(obj.className!="list_title_c lb_2")obj.className="list_title_c";
	}
}

function list_click(obj){
	if(obj==null)return;
	var o=(document.all)?obj.children:obj.childNodes;
	for(var i=0;i<o.length;i++){
		if(o[i].tagName=="SPAN"&&o[i].className=="l_check"){
			var oc =(document.all)?o[i].children:o[i].childNodes;
			if(oc[0].checked!=true){
				oc[0].checked=true;
				obj.className="list_title_c lb_2";
			}else{
				oc[0].checked=false;
				obj.className="list_title_c";
			}
		}
	}
}

function SetCheckBoxBg(BoxName,Action){
	if(Action==null)Action="SELECT ALL";
	var boxs = document.getElementsByName(BoxName);
	if(boxs==null)return;
	if(typeof(Action) == "object"){
		if(typeof(Action.tagName)!="undefined"){
			if(Action.type=="checkbox"){
				for(var i =0;i<boxs.length;i++){
					if(boxs[i].type=="checkbox"){
						boxs[i].checked = Action.checked?false:true;
						boxs[i].click();
					}
				}
			}
		}
	}	
}

function ForBgCheck(obj){
	if(obj==null)return;
	if(obj.checked==true){
		obj.checked=false;
	}else{
		obj.checked=true;
	}	
}