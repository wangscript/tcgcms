<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categorieslist.aspx.cs" Inherits="skin_categorieslist" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.PageControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>�ޱ���ҳ</title>
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
	<script type="text/javascript" src="../js/listcommon.js"></script>
	<script type="text/javascript" src="../js/layer.js"></script>
	<script type="text/javascript" src="../js/CreateDivV2.js"></script>
	<script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../js/newsclasslist.js"></script>
	<script type="text/javascript" src="../js/CreateInputV2.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CreatClass(this);">
			<img src="../images/icon/24.gif" /> �½�
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="EditClass(this);">
			<img src="../images/icon/05.gif" />�༭
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsClassDel();">
			<img src="../images/icon/08.gif" />ɾ��
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="NewsClassCreateHtml();">
			<img src="../images/icon/8.gif" />����
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />ˢ��
		</a>
	</div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="newslistt" id="classTitle"></div>
	<div class="list_title">
		<span class="l_check l_rg"><input name="" type="checkbox" value="" onclick="SetCheckBoxBg('CheckID',this);"/></span>
		<span class="l_classname bold l_rg">��Դ��������</span>
		<span class="l_classname bold l_rg">�������</span>
		<span class="l_classname bold l_rg">����Ŀ¼</span>
		<span class="l_id bold l_rg">����</span>
		<span class="l_updatedate bold">����ʱ��</span>
	</div>
	<asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
	<div class="list_title_c" onmousemove="list_bgchange(this,1);" onmouseout="list_bgchange(this,0);" onclick="list_click(this);">
		<span class="l_check"><input name="CheckID" type="checkbox" value="<TCG:Span id='CheckID' runat='server' />" onclick="ForBgCheck(this)" /></span>
		<TCG:Span class='l_classname' id='classname' runat='server' />
		<TCG:Span class='l_classname hidover' id='lname' runat='server' />
		<TCG:Anchor class='l_classname' id='directory' runat='server' />
		<TCG:Span class='l_id' id='sOrder' onclick="MdyFeild(this,'Order')" runat='server'/>
		<TCG:Span class='l_updatedate dcolor' id='updatedate' runat='server' />
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
	<script type="text/javascript">classTitleInit();</script>
    </form>
</body>
</html>