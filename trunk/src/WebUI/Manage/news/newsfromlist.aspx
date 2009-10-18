<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsfromlist.aspx.cs" Inherits="news_newsfromlist" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="Manage" namespace="TCG.Manage.Controls" assembly="TCG.Manage"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资源特性列表</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["ScriptsSite"]%>Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["ScriptsSite"]%>Common/AllNewsSpeciality.aspx"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/pager.js"></script>
	<script type="text/javascript" src="../js/CreateInput.js"></script>
	<script type="text/javascript" src="../js/newsfromlist.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckForm()">
    <div class="page_title">
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AddNewSAction();">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsSDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<Manage:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt bold">资讯来源</div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id bold l_rg">ID</span>
		<span class="l_classname bold l_rg">来源名称</span>
		<span class="l_classname bold l_rg list_info_w">来源联接</span>
		<span class="l_updatedate bold">更新时间</span>
	</div>
	<div class="list_title_c hid" id="AddNewS" style="height:25px; line-height:25px;">
		<span class="l_check"></span>
		<span class='l_id green bold'>新增:</span>
		<span class='l_classname' ><input type="text" id="inTitle" runat="server"  class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		</span>
		<span class='l_classname hidover list_info_w'><input type="text" id="inUrl" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'" style="width:320px;"/></span>
		<span class='l_updatedate dcolor' style="width:160px;"><input type="submit" class="btn2 bold addbtn" value="确定" /><input type="reset" class="btn2 addbtn" value="取消" onclick="CAdd();"/></span>
	</div>	
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id' id='sId' runat='server'/>
		<TCG:Span class='l_classname' id='sTitle' runat='server' onclick="MdyFeild(this,'Title')"/>
		<TCG:Span class='l_classname hidover list_info_w' id='sUrl' runat='server' onclick="MdyFeild(this,'Url')" />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		<TCG:Pager Id='pager' runat='server'/>
	</div>
	<input type="hidden" id="iFeildName" name="iFeildName" runat="server" />
	<input type="hidden" id="iAction" name="iAction"/>
	<input type="hidden" id="iMdyID" name="iMdyID"/>
	<input type="hidden" id="iIds" name="iIds"/>
    </form>
</body>
</html>
