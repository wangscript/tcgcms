<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl.ascx.cs" Inherits="htmleditor_WebUserControl" %>
<script language="JAVASCRIPT" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/editfunc.js" type="text/javascript"></script>
<script language="JAVASCRIPT" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/colorSelect.js" type="text/javascript"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<style type="text/css">
TD.icon {
	VERTICAL-ALIGN: middle; WIDTH: 24px; HEIGHT: 24px; TEXT-ALIGN: center
}
TD.sp {
	VERTICAL-ALIGN: middle; WIDTH: 8px; HEIGHT: 24px; TEXT-ALIGN: center
}
TD.xz {
	VERTICAL-ALIGN: middle; WIDTH: 47px; HEIGHT: 24px; TEXT-ALIGN: center
}
TD.bq {
	VERTICAL-ALIGN: middle; WIDTH: 49px; HEIGHT: 24px; TEXT-ALIGN: center
}
DIV A.n {
	PADDING-RIGHT: 2px; DISPLAY: block; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; COLOR: #000000; LINE-HEIGHT: 16px; PADDING-TOP: 2px; HEIGHT: 16px; TEXT-DECORATION: none
}
DIV A.n:hover {
	BACKGROUND: #e5e5e5
}
</style>
<style type="text/css">
.ico {
	VERTICAL-ALIGN: middle; WIDTH: 24px; HEIGHT: 24px; TEXT-ALIGN: center
}
.ico2 {
	VERTICAL-ALIGN: middle; WIDTH: 27px; HEIGHT: 24px; TEXT-ALIGN: center
}
.ico3 {
	VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 24px; TEXT-ALIGN: center
}
.ico4 {
	VERTICAL-ALIGN: middle; WIDTH: 8px; HEIGHT: 24px; TEXT-ALIGN: center
}
BODY {
	MARGIN: 0px
}
</style>
<table style="border-right: #abbdd1 1px solid; border-top: #abbdd1 1px solid; font-size: 12px;
    border-left: #abbdd1 1px solid; border-bottom: #abbdd1 1px solid" cellspacing="0"
    cellpadding="0" width="100%" background="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/bg.gif" border="0">
    <tbody>
        <tr>
            <td style="padding-left: 5px" height="30">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="format('Cut')" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/1.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="format('Copy')" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/2.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="粘" onclick="format('Paste')" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/3.gif" width="20"></td>
                            <td class="ico">
                                <img height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/line.gif" width="4"></td>
                            <td class="ico2">
                                <img onmousedown="fSetBorderMouseDown(this)" id="imgFontface" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="fGetEv(event);fDisplayElement('fontface','')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/4.gif" width="25"></td>
                            <td class="ico2">
                                <img onmousedown="fSetBorderMouseDown(this)" id="imgFontsize" onmouseover="fSetBorderMouseOver(this)"
                                    title="趾" onclick="fGetEv(event);fDisplayElement('fontsize','')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/5.gif" width="25"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="哟" onclick="format('Bold');" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/6.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="斜" onclick="format('Italic');" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/7.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="禄" onclick="format('Underline')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/8.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="format('Justifyleft')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/9.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="屑" onclick="format('Justifycenter')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/10.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="叶" onclick="format('Justifyright')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/11.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="直" onclick="format('Insertorderedlist')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/12.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="目" onclick="format('Insertunorderedlist')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/13.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="format('Outdent')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/14.gif" width="20"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="format('Indent')" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/15.gif" width="20"></td>
                            <td class="ico3">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="色" onclick="foreColor(event)" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/16.gif" width="23"></td>
                            <td class="ico2">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="色" onclick="backColor(event)" onmouseout="fSetBorderMouseOut(this)"
                                    height="20" src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/17.gif" width="24"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="" onclick="createLink()" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/18.gif" width="21"></td>
                            <td class="ico">
                                <img onmousedown="fSetBorderMouseDown(this)" onmouseover="fSetBorderMouseOver(this)"
                                    title="图片" onclick="createImg()" onmouseout="fSetBorderMouseOut(this)" height="20"
                                    src="<%=config["WebSite"]%>/<%=config["ManagePath"]%>htmleditor/Resources/19.gif" width="21"></td>
                            <td class="ico4">&nbsp;
                          </td>
                            <td style="font-size: 12px">&nbsp;
                          </td>
                            <td style="font-size: 12px">
                                <input language="javascript" onmouseover="fSetModeTip(this)" onclick="setMode(this.checked)"
                                    onmouseout="fHideTip()" type="checkbox" name="switchMode">
                                HTML</td>
                            <td style="font-size: 12px">&nbsp;
                          </td>
                        </tr>
                    </tbody>
                </table>
          </td>
        </tr>
    </tbody>
</table>
<div id="dvForeColor" style="display: none; left: -500px; width: 100px; position: absolute;
    top: -500px; height: 100px">
    <table style="border-right: #888888 1px solid; border-top: #888888 1px solid; border-left: #888888 1px solid;
        border-bottom: #888888 1px solid" height="25" cellspacing="0" cellpadding="0"
        width="218">
        <tbody>
            <tr>
                <td id="tdView" width="110">&nbsp;
                    </td>
                <td id="tdColorCode" align="middle" bgcolor="#ffffff">
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div id="dvPortrait" style="display: none; left: -500px; width: 100px; position: absolute;
    top: -500px; height: 100px">
</div>
<div id="fontface" style="border-right: #838383 1px solid; padding-right: 1px; border-top: #838383 1px solid;
    display: none; padding-left: 1px; z-index: 99; background: #ffffff; left: 2px;
    padding-bottom: 1px; border-left: #838383 1px solid; width: 110px; padding-top: 1px;
    border-bottom: #838383 1px solid; position: absolute; top: 35px; height: 270px">
    <a class="n" style="font: 12px ''" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
        href="javascript:void(0)"></a><a class="n" style="font: 12px ''" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
            href="javascript:void(0)"></a><a class="n" style="font: 12px ''" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                href="javascript:void(0)"></a><a class="n" style="font: 12px ''" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                    href="javascript:void(0)"></a><a class="n" style="font: 12px '圆'" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                        href="##">圆</a><a class="n" style="font: 12px Arial" onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                            href="##">Arial</a><a class="n" style="font: 12px 'Arial Narrow'"
                                onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                href="##">Arial Narrow</a><a class="n" style="font: 12px 'Arial Black'"
                                    onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                    href="##">Arial Black</a><a class="n" style="font: 12px 'Comic Sans MS'"
                                        onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                        href="##">Comic Sans MS</a><a class="n" style="font: 12px Courier"
                                            onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                            href="##">Courier</a><a class="n" style="font: 12px System"
                                                onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                                href="##">System</a><a class="n" style="font: 12px 'Times New Roman'"
                                                    onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                                    href="javascript:void(0)">Times New Roman</a><a class="n" style="font: 12px Verdana"
                                                        onclick="format('fontname',this.innerHTML);this.parentNode.style.display='none'"
                                                        href="javascript:void(0)">Verdana</a></div>
<div id="fontsize" style="border-right: #838383 1px solid; padding-right: 1px; border-top: #838383 1px solid;
    display: none; padding-left: 1px; background: #ffffff; left: 26px; padding-bottom: 1px;
    border-left: #838383 1px solid; width: 115px; padding-top: 1px; border-bottom: #838383 1px solid;
    position: absolute; top: 35px; height: 160px">
    <a class="n" style="font-size: xx-small; line-height: 120%" onclick="format('fontsize',1);this.parentNode.style.display='none'"
        href="javascript:void(0)">小</a><a class="n" style="font-size: x-small; line-height: 120%"
            onclick="format('fontsize',2);this.parentNode.style.display='none'" href="javascript:void(0)">小</a><a
                class="n" style="font-size: small; line-height: 120%" onclick="format('fontsize',3);this.parentNode.style.display='none'"
                href="javascript:void(0)">小</a><a class="n" style="font-size: medium; line-height: 120%"
                    onclick="format('fontsize',4);this.parentNode.style.display='none'" href="#"></a><a
                        class="n" style="font-size: large; line-height: 120%" onclick="format('fontsize',5);this.parentNode.style.display='none'"
                        href="javascript:void(0)"></a><a class="n" style="font-size: x-large; line-height: 120%"
                            onclick="format('fontsize',6);this.parentNode.style.display='none'" href="javascript:void(0)">卮</a><a
                                class="n" style="font-size: xx-large; line-height: 140%" onclick="format('fontsize',7);this.parentNode.style.display='none'"
                                href="javascript:void(0)"></a></div>
                                
                                
<div id="divEditor">
    <table width="100%" height="287px" border="0" cellspacing="0" cellpadding="0" ><tr><td style="border:1px solid #abbdd1; border-top:0;"><IFRAME class="HtmlEditor" ID="HtmlEditor" name="HtmlEditor" style=" height:286px;width:100%" frameBorder="0" marginHeight=0 marginWidth=0 ></IFRAME></td></tr></table>
</div>
<textarea ID="sourceEditor" style="height:280px;width:100%;display:none" ></textarea>
<input id="HtmlEditorContent" type="hidden" name="HtmlEditorContent" runat="server"  />
<script language="javascript" type="text/javascript">
function setContent(){
	if(gMode==true){
		setMode(false);
	}
	var f = window.frames["HtmlEditor"];
	var body = f.document.getElementsByTagName("BODY")[0];
	document.getElementById("<%=HtmlEditorContent.ClientID %>").value = body.innerHTML ;
}
InitDocument("<%=HtmlEditorContent.ClientID %>");
</script>
