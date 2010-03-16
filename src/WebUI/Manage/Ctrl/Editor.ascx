<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Editor.ascx.cs" Inherits="Manage_Ctrl_Editor" %>
<div id="ContentWin" style="width:800px;height:400px">
<iframe src="../QzoneEditor/edit.html?I=<% =content.ClientID %>&B=<% =ContentBg.ClientID %>" 
    frameBorder="0" 
    marginHeight="0" 
    marginWidth="0" 
    scrolling="No" 
    style="height:100%;width:100%" id="EditerFrame" name="EditerFrame">
 </iframe>
 </div>
<input type="text" value="" name="ContentBg" id="ContentBg" size=106 class="hid" runat="server"/><br>
<textarea name="content" id="content" style="width:800px;height:100px" class="hid"  runat="server"></textarea>
<script language="javascript" type="text/javascript">
function setContent(){
    var f = window.frames["EditerFrame"];
    var ff = f.document.frames["HtmlEditor"];
    var body = ff.document.getElementsByTagName("BODY")[0];
	document.getElementById("<%=content.ClientID %>").value = body.innerHTML;
}
</script>