//--------------
var ajax = new AJAXRequest();

function CheckFrom(obj){
	if(obj==null)return false;
	if(obj.length==0)return false;
	var canpost=true;
	for(var i=0;i<obj.length;i++){
		var e=obj[i];
		if(e.name!=''){
			if(e.value==''){
				var err=$(e.name+"_msg");
				if(err!=null)err.className="info_err";
				canpost=false;
			}else{
				var err=$(e.name+"_msg");
				if(err!=null)err.className="info_ok";
			}
		}
	}
	if(canpost){
		SetAjaxDiv("loader",false,"���ڷ�������...");
		ajax.postf($("form1"),function(obj) { ChanagePostBack(obj.responseText);});
	}
	return false;
}

function ChanagePostBack(val){
	if(GetErrText(val))return;
	SetAjaxDiv("ok",false,"ϵͳ�����޸ĳɹ���");
}