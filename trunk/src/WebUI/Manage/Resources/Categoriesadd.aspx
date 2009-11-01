<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categoriesadd.aspx.cs" Inherits="resources_categoriesadd" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>添加资讯分类</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="../Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="../js/CreateDiv.js"></script>
	<script type="text/javascript" src="../js/newsclasslist.js"></script>
	<script type="text/javascript" src="../js/newsclassadd.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckAddClassForm();">
	<input type="hidden" id="iClassId" name="iClassId" runat="server" />
    <TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_title bold">
		添加资讯分类　<a href="javascript:GoTo();" class="title_back bold" onclick="window.parent.CreatClassClose();">[关闭]</a>
	</div>
	<div id="placemsg" class="Page_arrb arb_pr classaddline">
		<span class="p_a_t">详 细 位 置：</span>
	</div>
	<script type="text/javascript">GetParentTitle();</script>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 名 称：</span>
	  <input id="iClassName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
		<span id="cnamemsg" class="info1">分类名称，必须填写</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 别 名：</span>
	  <input id="iName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iName','inamemsg');"/>
		<span id="inamemsg" class="info1">分类别名，显示在前台的名称</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">指 定 目 录：</span>
	  <input id="iDirectory" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iDirectory','dirmsg');"/>
		<span id="dirmsg" class="info1">相对管理网站的的目录，存放生成的静态文件</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分类 首地址：</span>
	  <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iUrl','urlmsg');"/>
		<span id="urlmsg" class="info1">分类在网站前台的首页地址，用于生成导航</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">详 细 模 板：</span>
	  <select id="sTemplate" runat="server"  onchange="CheckTemplate('sTemplate','stdmsg')">
	  	<option value="-1">请选择</option>
	  </select>
		<span id="stdmsg" class="info1">分类资讯详细页模板</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">列 表 模 板：</span>
	  <select id="slTemplate" runat="server" onchange="CheckTemplate('slTemplate','stsdmsg')">
	  	<option value="-1">请选择</option>
	  </select>
		<span id="stsdmsg" class="info1">分类资讯列表页模板</span>
	</div>
	<div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 排 序：</span>
	  <input id="iOrder" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		<span id="urlmsg" class="info1">在列表中的显示顺序</span>
	</div>
	<div class="dobtn arb_pr">
		<input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" />
	</div>
    </form>
</body>
</html>
