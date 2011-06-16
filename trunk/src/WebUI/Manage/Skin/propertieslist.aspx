<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propertieslist.aspx.cs" Inherits="TCG.CMS.WebUi.Manage_Skin_propertieslist" %>

<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllSkin.aspx"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
    <script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../js/propertieslist.js"></script>
	<script type="text/javascript" src="../js/CreateInputV2.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreatpropertiesCategorie(this);">
			<img src="../images/icon/24.gif" /> 新建
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditpropertiesCategorie(this);">
			<img src="../images/icon/05.gif" />编辑
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsClassDel();">
			<img src="../images/icon/08.gif" />删除
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_id l_rg">编号</span>
		<span class="l_classname bold l_rg">属性名称</span>
		<span class="l_id bold l_rg">可用</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_id' id='ipid' runat='server' />
		<TCG:Span class='l_classname hidover' id='lname' runat='server' />
		<TCG:Span class='l_id' id='sVisible' onclick="MdyFeild(this,'Order')" runat='server'/>
	</div>	
		</ItemTemplate>
	</asp:Repeater>
	<div class="list_bottom">
	</div>
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
    <input type="hidden" id="iPage" name="iPage" runat="server" />
	<input type="hidden" id="DelClassId" name="DelClassId" runat="server" />
	<input type="hidden" id="work" name="work"/>
	<input type="hidden" id="iFeildName" name="iFeildName" runat="server" />
	<input type="hidden" id="iAction" name="iAction"/>
	<input type="hidden" id="iMdyID" name="iMdyID"/>
	<input type="hidden" id="iSkinId" name="iSkinId"  runat="server"/>
	<!-- class="layerbox"-->
	<div id="layerbox1" class="layerbox">
	    <div class="CreateClassDiv">
		    <iframe id="ifCreateAdd" width="600" height="379" frameborder="0"></iframe>
	    </div>
	</div>
	<script type="text/javascript">	    classTitleInit();</script>
    </form>
</body>
</html>