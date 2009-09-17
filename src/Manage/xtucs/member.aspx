<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member.aspx.cs" Inherits="xtucs_member" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="Manage" namespace="TCG.Manage.Controls" assembly="TCG.Manage"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资讯内容列表</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/newscommon.aspx"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/AllNewsSpeciality.aspx"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/pager.js"></script>
	<script type="text/javascript" src="../js/MenuDiv.js"></script>
	<script type="text/javascript" src="../js/CreateDiv.js"></script>
	<script type="text/javascript" src="../js/xtucs_member.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<!--a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AddNewsInfo();">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditNewsInfo();">
			<img src="../images/icon/05.gif" />编辑
		</a-->
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreateNews();">
			<img src="../images/icon/8.gif" />通过
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreateNews1();">
			<img src="../images/icon/8.gif" />不通过
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<Manage:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle"> <a href="member.aspx">校友收录</a></div>
	<div class="ChildclassTitle hid" id="ChildclassTitle" onmouseout="HidClassTitle()" onmousemove="ShowClassTitle($('ClassTitleA'));"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id bold l_rg">ID</span>
		<span class="l_classname bold l_rg newsclass">姓名</span>
		<span class="l_classname bold l_rg newsclass">专业</span>
		<span class="l_classname bold l_rg newsclass">年级</span>
		<span class="l_classname bold l_rg newsclass">工作单位</span>
		<span class="l_classname bold l_rg newsclass">手机号</span>
		<span class="l_updatedate bold">审核</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id' id='sId' runat='server'/>
		
		<TCG:Span class="l_classname newsclass" id='sXinMing' runat='server'>姓名</TCG:Span>
		<TCG:Span class="l_classname newsclass" id='sZhuanYe' runat='server'>专业</TCG:Span>
		<TCG:Span class="l_classname newsclass" id='sNianJI' runat='server'>年级</TCG:Span>
		<TCG:Span class="l_classname newsclass" id='sDanWei' runat='server'>工作单位</TCG:Span>
		<TCG:Span class="l_classname newsclass" id='sShouJi' runat='server'>手机号</TCG:Span>
		<TCG:Span class="l_updatedate" id='cCheck' runat='server'>审核</TCG:Span>
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
		
		<TCG:Pager Id='pager' runat='server'/>
	</div>
	<div class="Manage list_b_bg>
	<span style=" float:left;">姓名：<input type="text" name="XinMing" /><input type="submit" value="查询" /></span>

	</div>
	<input type="hidden" id="iAction" name="iAction"/>
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
	<input type="hidden" id="iSpeciality" name="iSpeciality" runat="server" />
	<input type="hidden" id="DelClassId" name="DelClassId"/>
    </form>
</body>
</html>