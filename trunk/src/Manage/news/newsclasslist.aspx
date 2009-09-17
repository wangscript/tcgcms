<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsclasslist.aspx.cs" Inherits="news_newsclasslist" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="Manage" namespace="TCG.Manage.Controls" assembly="TCG.Manage"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/newscommon.aspx"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/pager.js"></script>
	<script type="text/javascript" src="../js/DivMove.js"></script>
	<script type="text/javascript" src="../js/CreateDiv.js"></script>
	<script type="text/javascript" src="../js/newsclasslist.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreatClass(this);">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditClass(this);">
			<img src="../images/icon/05.gif" />编辑
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsClassDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsClassCreateHtml();">
			<img src="../images/icon/8.gif" />生成
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<Manage:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id bold l_rg">ID</span>
		<span class="l_classname bold l_rg">资源分类名称</span>
		<span class="l_classname bold l_rg">分类别名</span>
		<span class="l_Directory bold l_rg">生成目录</span>
		<span class="l_updatedate bold">更新时间</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id' id='sId' runat='server'/>
		<TCG:Span class='l_classname' id='classname' runat='server' />
		<TCG:Span class='l_classname hidover' id='lname' runat='server' />
		<TCG:Anchor class='l_Directory' id='directory' runat='server' />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		
		<TCG:Pager Id='pager' runat='server'/>
	</div>
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
	<input type="hidden" id="DelClassId" name="DelClassId" runat="server" />
	<input type="hidden" id="work" name="work"/>
	<div id="CreateClassDiv" class="CreateClassDiv">
		
		<iframe id="ifCreateAdd" width="0" height="0" frameborder="0"></iframe>
		<div class="CreateClassDivClose hid" id="CreateClassDivClose"><a href="javascript:GoTo();" onclick="CreatClassClose();"><img src="../images/icon/10.gif"  /></a></div>
	</div>
	<div id="CBackg" class="CBackg"></div>
	<script type="text/javascript">classTitleInit();</script>
    </form>
</body>
</html>