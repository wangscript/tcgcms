<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsInfo.aspx.cs" Inherits="Template_NewsInfo" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资讯模版管理</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/template.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
	<div class="Page_title">资讯模版管理<span class="info1">(资讯模版管理里导航页面)</span></div>
	
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
    <div class="TempItem" onmouseover="this.className='TempItem tempitembg';" onmouseout="this.className='TempItem';">
		<img src="../images/icon/1.gif" />
		<TCG:Anchor id='sitename' runat='server' />
		<TCG:Span id='info' class="info2" runat='server' />
	</div>
	</ItemTemplate>
	</asp:Repeater>
	<div class="Page_arrb arb_pr">
		<span class="green bold">小提示：</span>
		<span class="info1">各站点内的单篇文章管理，在模板里面！</span>
	</div>
    </form>
</body>
</html>
