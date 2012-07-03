<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_pop.aspx.cs" Inherits="TCG.CMS.WebUi.admin_pop" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>角色组</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admin_pop.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/MenuDiv.js"></script>
	<script type="text/javascript" src="js/adminpop.js"></script>
	<script language="javascript">
	var emunum = <TCG:Span id='emunum' runat='server' /> + 2;
	var alist =[<TCG:Span id='alist' runat='server' />];
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="poptitle">角色组</div>
	<input type="hidden" name="admins" id="admins" />
	<input type="hidden" name="iRole" id="iRole" />
	<a id="l_1" href="adminInfo.aspx" target="adminmain" class="pop1 popbg" onmouseover="onPop2(this,0)" onmouseout="onPop2(this,1)" onclick="onPop(this);">
		所有管理员(<TCG:Span id='admincount' runat='server' />)
	</a>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<a id="l_<TCG:Span id='num' runat='server' />" href="adminInfo.aspx?roleid=<TCG:Span id='roleid' runat='server' />" class="pop2" onmouseover="onPop2(this,0)" onmouseout="onPop2(this,1)" onclick="onPop(this);" target="adminmain" ><TCG:Span id='rolename' runat='server' />(<TCG:Span id='count' runat='server' />)
	</a>
		</ItemTemplate>
	</asp:Repeater>	
	<a id="l_2" href="adminRecovery.aspx" target="adminmain" class="pop2" onmouseover="onPop2(this,0)" onmouseout="onPop2(this,1)" onclick="onPop(this);">
		管理员回收站(<TCG:Span id='deladmincount' runat='server' />)
	</a>
	<a class="pop3" href="adminroleadd.aspx" target="adminmain">添加角色组</a>
    </form>
</body>
</html>
