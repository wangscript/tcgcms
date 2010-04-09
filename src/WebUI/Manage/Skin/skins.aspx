<%@ Page Language="C#" AutoEventWireup="true" CodeFile="skins.aspx.cs" Inherits="Skin_skins" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资讯模版管理</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/template.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../js/skins.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body>
    <form id="form1" runat="server">
	<div class="Page_title">站点皮肤管理<span class="info1">(设置,修改站点皮肤的导航页)</span></div>
    <TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
    <div class="TempItem" onmouseover="this.className='TempItem tempitembg';" onmouseout="this.className='TempItem';">
		<TCG:Img ID="pic" src="../images/icon/1.gif" runat='server' />
		<TCG:Anchor id='sitename' runat='server' />
		<TCG:Span id='info' class="info2" runat='server' />
		<span class="info2">
		    <a href="javascript:void 0;" onclick="CreateSql('<TCG:Span id='sid1' runat='server' />')">导出</a>
		    <a href="javascript:void 0;" onclick="SetDefaultSkin('<TCG:Span id='sid' runat='server' />')">启用</a>
		</span>
		<span class="info3">
		    <a href="templatelist.aspx?skinid=<TCG:Span id='vsid' runat='server' />">模板</a>
		    <a href="categorieslist.aspx?skinid=<TCG:Span id='vsid1' runat='server' />">分类</a>
		    <a href="speciality.aspx?skinid=<TCG:Span id='vsid2' runat='server' />">特性</a>
		</span>
		<div class="used" id="IsDefault" runat='server'><img src="../images/icon/used_skin.png" /></div>
	</div>
	</ItemTemplate>
	</asp:Repeater>
	<input type="hidden" id="Work" name="Work" value="SetDefalutSkinId" />
	<input type="hidden" id="SkinId" name="SkinId" value="" />
	<div class="Page_arrb arb_pr">
		<span class="green bold">小提示：</span>
		<span class="info1">各站点内的单篇文章管理，在模板里面！</span>
	</div>
    </form>
</body>
</html>