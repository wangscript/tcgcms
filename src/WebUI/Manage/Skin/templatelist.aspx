<%@ Page Language="C#" AutoEventWireup="true" CodeFile="templatelist.aspx.cs" Inherits="TCG.CMS.WebUi.Template_templatelist" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>站点模版列表</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../Common/AllTemplates.aspx"></script>
	<script type="text/javascript" src="../Common/AllSkin.aspx"></script>
	<script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../js/pager.js"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/CreateDivV2.js"></script>
	<script type="text/javascript" src="../js/tempnewslist.js"></script>
	<script type="text/javascript" src="../js/layer.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="page_title">
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AddTemplate();">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditTemplate();">
			<img src="../images/icon/05.gif" />编辑
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="TempDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="PageCreat();">
			<img src="../images/icon/8.gif" />生成
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle" runat="server"></div>
	<div class="list_title">
	  <span class="l_check l_rg"><input name="" id="CheckBoxMain" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id1 bold l_rg">ID</span>
		<span class="l_classname newstitle bold l_rg">模版名称</span><span class="l_updatedate bold">更新时间</span>
	</div>

    <div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);" id="systemlisttemplateDiv" runat="server">
		<span class="l_check"><input name="CheckID" type="checkbox" value="" onclick="ForBgCheck(this)" /></span>
		<Span class='l_id1'>1</Span>
		<TCG:Span class='l_classname newstitle' id='systemlisttemplate' runat='server' />
		<Span class='l_updatedate dcolor'></Span>
	</div>

    <div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);" id="systempagetemplateDiv" runat="server">
		<span class="l_check"><input name="CheckID" type="checkbox" value="" onclick="ForBgCheck(this)" /></span>
		<Span class='l_id1'>2</Span>
		<TCG:Span class='l_classname newstitle' id='systempagetemplate' runat='server' />
		<Span class='l_updatedate dcolor'></Span>
	</div>

    <div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);" id="systemctrltemplteDiv" runat="server">
		<span class="l_check"><input name="CheckID" type="checkbox" value="" onclick="ForBgCheck(this)" /></span>
		<Span class='l_id1'>3</Span>
		<TCG:Span class='l_classname newstitle' id='systemctrltemplte' runat='server' />
		<Span class='l_updatedate dcolor'></Span>
	</div>

    <asp:Repeater id="ItemRepeater1" runat="server" onitemdatabound="ItemRepeater1_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id1' id='sId' runat='server'/>
		<TCG:Span class='l_classname newstitle' id='classname' runat='server' />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>

	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id1' id='sId' runat='server'/>
		<TCG:Span class='l_classname newstitle' id='classname' runat='server' />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		
	</div>
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server" />
	<input type="hidden" id="iParentid" name="iParentid" runat="server" />
	<input type="hidden" id="temps" name="temps"/>
	<input type="hidden" id="work" name="work"/>
	<input type="hidden" id="iTemplateId" name="iTemplateId"/>
	<input type="hidden" id="SytemType" name="SytemType" value="0"/>
	<script type="text/javascript">classTitleInit();</script>
    </form>
</body>
</html>
