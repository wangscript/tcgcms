<%@ page language="C#" autoeventwireup="true" inherits="resources_resourceslist, TCG.WebUI" enableviewstatemac="false" enableEventValidation="false" viewStateEncryptionMode="Never" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资讯内容列表</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../Common/AllTemplates.aspx"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../js/pager.js"></script>
    <script type="text/javascript" src="../js/CreateDivV2.js"></script>
	<script type="text/javascript" src="../js/newslist.js"></script>
	<script type="text/javascript" src="../js/layer.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AddNewsInfo();">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditNewsInfo();">
			<img src="../images/icon/05.gif" />编辑
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreateNews();">
			<img src="../images/icon/8.gif" />生成
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle"> <a href="javascript:GoTo();" onmousemove="ShowClassTitle(this);" onmouseout="HidClassTitle()" class="ClassTitleA" id="ClassTitleA" title="显示子分类"><img src="../images/icon/2.gif" /></a></div>
	<div class="ChildclassTitle hid" id="ChildclassTitle" onmouseout="HidClassTitle()" onmousemove="ShowClassTitle($('#ClassTitleA').get(0));"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id bold l_rg">ID</span>
		<span class="l_classname bold l_rg newstitle hidover">资讯标题</span>
		<span class="l_classname bold l_rg newsclass">所属主类别</span>
		<span class="l_id bold l_rg">审核</span>
		<span class="l_id bold l_rg">生成</span>
		<span class="l_updatedate bold">更新时间</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id' id='sId' runat='server'/>
		<TCG:Span class='l_classname newstitle hidover' id='sTitle' runat='server' />
		<TCG:Span class='l_classname newsclass' id='sClassName' runat='server' />
		<TCG:Span class='l_id' id='sChecked' runat='server' />
		<TCG:Span class='l_id' id='sCreated' runat='server' />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		<span class="bold lfl">查看：</span>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchAll();">全部</a>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchChecked('Y');">- 已审</a>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchChecked('N');">- 未审</a>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchCreated('Y');">- 已生成</a>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchCreated('N');">- 未生成</a>
		<a href="javascript:GoTo();" class="gray lfl ck" onclick="SearchBQ(this);">- 按标签</a>
		<TCG:Pager Id='pager' runat='server'/>
	</div>
	<div class="Manage list_b_bg">
	</div>
	<input type="hidden" id="iAction" name="iAction"/>
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server" />
	<input type="hidden" id="iSpeciality" name="iSpeciality" runat="server" />
	<input type="hidden" id="DelClassId" name="DelClassId"/>
	<script type="text/javascript">classTitleInit();childClassTitleInit();</script>
    </form>
</body>
</html>