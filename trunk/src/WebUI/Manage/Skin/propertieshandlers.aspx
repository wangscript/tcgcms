<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propertieshandlers.aspx.cs" Inherits="TCG.CMS.WebUi.Manage_Skin_propertieshandlers" %>

<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>添加资讯分类</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllSkin.aspx"></script>
	<script type="text/javascript" src="../js/propertieshandler.js"></script>
    <script type="text/javascript" src="../Common/CategorieProperties.aspx?cid=<asp:Literal ID='cid' runat='server'></asp:Literal>"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
	<input type="hidden" id="PropertiesCategorieId" name="PropertiesCategorieId" runat="server" />
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server" />
    <input type="hidden" id="iMaxPId" name="iMaxPId" runat="server" />
    <div class="page_title" style="margin-bottom:5px;">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="$('#form1').submit();">
			<img src="../images/icon/save.gif" /> 保存
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="javascript:window.history.back();">
			<img src="../images/icon/5.gif" />取消
		</a>
        <a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="CategoriePropertieHTMLAdd();">
			<img src="../images/add.png" />加属性
		</a>
        <a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
        
	</div>
    <TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
    <div class="g-tabnav">
        <a style="margin-left:-13px;"></a>
        <a id="a1" onclick="SetFromsByNum('a1')">基本信息</a>
        <a id="a2" onclick="SetFromsByNum('a2')">分类属性</a>
    </div>
    <div id="a1_from">
	    <div id="placemsg" class="Page_arrb arb_pr classaddline">
		    <span class="p_a_t">所 属 皮 肤：</span>
	    </div>
	    <script type="text/javascript">GetParentTitle();</script>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 名 称：</span>
	      <input id="iClassName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
		    <span id="cnamemsg" class="info1">分类名称，必须填写</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">是 否 可 用：</span>
	      <select id="iVisible" runat="server">
                <option value="Y">可用</option>
                <option value="N">不可用</option>
	      </select>
		    <span id="Span1" class="info1">如果悬在不可用，在发布的时候将无法选择该属性分类</span>
	    </div>
    </div>
    <div id="a2_from"></div>
	<div class="dobtn arb_pr">
		<input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" />
	</div>
    </form>
</body>
</html>
