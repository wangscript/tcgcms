//--------------
var ajax = new AJAXRequest();

function LoginOut(){
	ajax.get(
		"AjaxMethod/Admin_logout.aspx",
		function(obj){LogoutBack(obj.responseText)}
	);
}

function LogoutBack(val){
	top.location.href = "login.aspx";
}

function GoMyAccount(){
	top.menu.show();
}

function emuInit(){
	if(Mnum==null)return;if(SelecM==null)return;
	if(SelecM==0){
		$("m_0").className="top_title bold";
		$("mm_0").className="top_title_m";
	}else{
		if($("m_"+SelecM)==null)return;
		$("m_"+SelecM).className="top_title bold";
		$("mm_"+(SelecM-1)).className="top_title_m";
		$("mm_"+SelecM).className="top_title_m";
	}
}

function SelEmu(Num){
	if(Mnum==null)return;if(SelecM==null)return;
	if(Num==SelecM)return;
	var OldSel = $("m_"+SelecM);
	if(OldSel==null)return;
	var NewSel = $("m_"+Num);
	if(NewSel==null)return;
	OldSel.className="top_title1 bold";
	var OldM_1=$("mm_"+SelecM);var OldM_2=$("mm_"+(SelecM-1));
	if(OldM_1!=null)OldM_1.className="top_title_m ttmbg";
	if(OldM_2!=null)OldM_2.className="top_title_m ttmbg";
	NewSel.className="top_title bold";
	var NewM_1=$("mm_"+Num);var NewM_2=$("mm_"+(Num-1));
	if(NewM_1!=null)NewM_1.className="top_title_m";
	if(NewM_2!=null)NewM_2.className="top_title_m";
	SelecM=Num;
}