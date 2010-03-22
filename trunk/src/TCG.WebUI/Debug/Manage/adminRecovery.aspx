<%@ page language="C#" autoeventwireup="true" inherits="adminRecovery, TCG.WebUI" enableviewstatemac="false" enableEventValidation="false" viewStateEncryptionMode="Never" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>管理员回收站</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="js/common.js"></script>
	<script type="text/javascript" src="js/AJAXRequest.js"></script>
	<script type="text/javascript" src="js/adminRecovery.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="page_title1 bold"><TCG:Span id='srolename' runat='server' /></div>
    <div class="listtitle">
		<span class="txt">共有
            <TCG:Span class='green bold' id='sAdmincount' runat='server' />个管理员</span>
		<span class="txt">，共有<TCG:Span class='green bold' id='sRolecount' runat='server' />角色组 | </span>
		<span  class="txt"> <a href="javascript:GoTo();" onclick="RealDel();">彻底删除</a> - <a href="javascript:GoTo();" onclick="SaveAdmins();">救回所选</a></span>
	</div>
	<div class="listtitle1 bold">
		<span class="CheckID"><input name="SelAll" type="checkbox" value="" onclick="SetCheckBox('CheckID',this);"/></span>
		<span class="loginName">登陆名</span>
		<span class="nickname">昵称</span>
		<span class="adminrole">角色</span>
		<span class="updatedate">更新时间</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="listtitle1">
		<span class="CheckID"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" /></span>
		<TCG:Anchor class='loginName' id='loginName' runat='server' />
		<TCG:Span class='nickname' id='nickname' runat='server' />
		<TCG:Anchor class='adminrole' id='adminrole' runat='server' />
		<TCG:Span class='updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<input type="hidden" id="saction" name="saction" />
	<input type="hidden" id="admins" name="admins" />	
    </form>
</body>
</html>