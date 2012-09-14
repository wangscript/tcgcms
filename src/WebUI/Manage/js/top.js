//--------------


function LoginOut(){
	$.ajax({
	    type: "GET", url: "AjaxMethod/Admin_logout.aspx?temp=" + new Date().toString(), data: "",
	    errror: function() { },
	    success: function(data) {
	        top.location.href = "login.aspx";
	    },
	    dataType: "json"
	});
}
var t = null;
$(document).ready(function () {
    t = setInterval("LoadCreatingFile()", 4000);
});

function LoadCreatingFile() {
    $.ajax({
        type: "GET", url: "Common/CreatingFileStatus.aspx?temp=" + new Date().toString(), data: "",
        errror: function () { },
        success: function (data) {
            if (data.FilePath == '') {
                $("#tdCreateFlie").html("");
            } else {
                $("#tdCreateFlie").html('&nbsp;&nbsp;当前正在生成：<a href="' + data.FilePath + '">' + data.FilePath + '</a>');
            }
        },
        dataType: "json"
    });
}


function GoMyAccount(){
	top.menu.show();
}

function emuInit(){
	if(Mnum==null)return;if(SelecM==null)return;
	if(SelecM==0){
		$("#m_0")[0].className="top_title bold";
		$("#mm_0").className="top_title_m";
	}else{
		if($("#m_"+SelecM).length==0)return;
		$("#m_"+SelecM)[0].className="top_title bold";
		$("#mm_" + (SelecM - 1))[0].className = "top_title_m";
		$("#mm_" + SelecM)[0].className = "top_title_m";
	}
}

function SelEmu(Num) {
    if (Mnum == null) return; if (SelecM == null) return;
    if (Num == SelecM) return;
    var OldSel = $("#m_" + SelecM);
    if (OldSel.length == 0) return;
    var NewSel = $("#m_" + Num);
    if (NewSel.length == 0) return;
    OldSel[0].className = "top_title1 bold";
    var OldM_1 = $("#mm_" + SelecM); var OldM_2 = $("#mm_" + (SelecM - 1));
    if (OldM_1.length != 0) OldM_1[0].className = "top_title_m ttmbg";
    if (OldM_2.length != 0) OldM_2[0].className = "top_title_m ttmbg";
    NewSel[0].className = "top_title bold";
    var NewM_1 = $("#mm_" + Num); var NewM_2 = $("#mm_" + (Num - 1));
    if (NewM_1.length != 0) NewM_1[0].className = "top_title_m";
    if (NewM_2.length != 0) NewM_2[0].className = "top_title_m";
    SelecM = Num;
}