﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileinfos.aspx.cs" Inherits="files_fileinfos" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="Manage" namespace="TCG.Manage.Controls" assembly="TCG.Manage"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>文件管理列表</title>
	<link href="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>css/base.css" rel="stylesheet" type="text/css" />
	<link href="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>css/filesinfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/common.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/listcommon.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/MenuDiv.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>Common/newscommon.aspx"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>Common/AllFileClass.aspx"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/pager.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/uploadfiles.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>js/filesinfo.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFrom();">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="ShowFilesCreate(this);">
			<img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/24.gif" />新建
		</a>
		<a href="#" class="tnew1" onmouseover="this.className='tnew1 nbg1'" onmouseout="this.className='tnew1'" onclick="MoveFiles(this);">
			<img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/move.gif" />移动
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="FilesDel();">
			<img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/08.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/07.gif" />刷新
		</a>
	</div>
	<div class="newslistt" id="classTitle"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id bold l_rg">ID</span>
		<span class="l_id bold l_rg"><img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/9.gif"  class="fileico"/></span>
		<span class="l_classname bold l_rg ">标题</span>
		<span class="l_classname bold l_rg ">备注</span>
		<span class="l_classname bold l_rg newsclass">大小</span>
		<span class="l_updatedate bold">创建时间</span>
	</div>
	<div class="list_title_c hid" id="AddFileClass" style="height:25px; line-height:25px;" >
		<span class="l_check "></span>
		<span class="l_id bold "></span>
		<span class="l_id bold  green">新建:</span>
		<span class="l_classname"><input type="text" id="inTitle" runat="server"  class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/></span>
		<span class="l_classname"><input type="text" id="inInfo" runat="server"  class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/> </span>
		<span class="l_classname bold  newsclass"></span>
		<span class='l_updatedate dcolor' style="width:160px;"><input type="submit" class="btn2 bold addbtn" value="确定" /><input type="reset" class="btn2 addbtn" value="取消" onclick="CAdd();"/></span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="FileClassCheckID" type="checkbox" value="<TCG:Span id='FileClassID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class="l_id" id='sFileClassId' runat='server' />
		<span class="l_id"><img src="<%=base.configService.baseConfig["WebSite"]%><%=base.configService.baseConfig["ManagePath"]%>images/icon/24.gif" class="fileico"/></span>
		<TCG:Span class='l_classname  hidover' id='sTitle' runat='server' />
		<TCG:Span class="l_classname " id='sInfo' runat='server'  />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<asp:Repeater id="ItemRepeaterFile" runat="server" onitemdatabound="ItemRepeaterFile_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="FileCheckID" type="checkbox" value="<TCG:Span id='FileClassID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class="l_id" id='sFileClassId' runat='server' />
		<span class="l_id"><TCG:Img class="fileico" id='sIco' runat='server'/></span>
		<TCG:Span class='l_classname  hidover' id='sTitle' runat='server' />
		<TCG:Span class="l_classname " id='sInfo' runat='server'  />
		<TCG:Span class="l_classname newsclass" id='ssize'  runat='server' />
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		<TCG:Pager Id='pager' runat='server'/>
	</div>
	<input type="hidden" id="work" name="work" />
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
	<script type="text/javascript">fileclassTitleInit();</script>
    </form>
</body>
</html>