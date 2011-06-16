<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminInfo.aspx.cs" Inherits="TCG.CMS.WebUi.adminInfo" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>����Ա�б�</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/admininfo.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="page_title1 bold"><TCG:Span id='srolename' runat='server' /></div>
    <div class="listtitle">
		<span class="txt">����
            <TCG:Span class='green bold' id='sAdmincount' runat='server' />������Ա</span>
		<span class="txt">������<TCG:Span class='green bold' id='sRolecount' runat='server' />��ɫ�� | </span>
		<span id="iRoleAll" runat="server"> <a href="adminadd.aspx">�½�����Ա</a> - <a href="adminroleadd.aspx">�½���ɫ��</a></span>
		<span id="iRoleOther" runat="server"><a href="adminrolemdy.aspx?roleid=$iRoleId$">�༭��ɫ��</a> - <a href="javascript:GoTo();" onclick="DeleteRole($iRoleId$)">ɾ����ɫ��</a></span>
	</div>
	<div class="listtitle1 bold">
		<span class="CheckID"><input name="" type="checkbox" value="" onclick="SetCheckBox('CheckID',this);"/></span>
		<span class="loginName lnpl">��½��</span>
		<span class="nickname">�ǳ�</span>
		<span class="adminrole">��ɫ</span>
		<span class="updatedate">����ʱ��</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="listtitle1">
		<span class="CheckID"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" /></span>
		<TCG:Img class='iLock' id='iLock' runat='server' />
		<TCG:Anchor class='loginName' id='loginName' runat='server' />
		<TCG:Span class='nickname' id='nickname' runat='server' />
		<TCG:Anchor class='adminrole' id='adminrole' runat='server' />
		<TCG:Span class='updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>	
    <asp:HiddenField ID="iRoleId" runat="server" />
    </form>
</body>
</html>
