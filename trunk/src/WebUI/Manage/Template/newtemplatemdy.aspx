<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newtemplatemdy.aspx.cs" Inherits="Template_newtemplatemdy" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改资讯模版</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="../js/newstempadd.js"></script>
</head>
<body>
    <form id="TtempLateFromMdy" runat="server">
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
    <div class="Page_title">添加资讯模版<span class="info1">(添加资讯模版文件)</span><a href="javascript:fGoBack();" class="title_back bold">[返回]</a></div>
	<div class="Page_g">所属网站：<span class="info2" id="sSite"></span></div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">模板名称：</span><input id="vcTempName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckTempName();"/>
		<span class="info1" id="tnmsg">资讯模版的名称</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">模板类型：</span><select id="tType" runat="server" onchange="CheckType();" disabled="true">
		</select>
		<span class="info1" id="typemsg">模版的类型见下面的小提示！</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">页面地址：</span><input id="vcUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckUrl();" style="width:360px;"/>
		<span class="info1" id="urlmsg">单页时必须填写地址</span>
		<span class="info1" id="conmsg"></span>
	</div>
	<div class="Page_arrb arb_pr templateaddnew1">
		<textarea cols="140" rows="22" id="vcContent" name="vcContent" runat="server" onblur="CheckContent()"></textarea>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="green bold">小提示：</span>
		<span class="info1"><span class="red">单页</span>-对网站的某个页面进行维护，<span class="red">资讯内页</span>-资讯显示页，<span class="red">资讯里列表</span>-资讯文章列表显示页面模版</span>
	</div>
	<div class="dobtn arb_pr"><input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" /></div>
	<input type="hidden" id="iSiteId" name="iSiteId" runat="server" />
	<input type="hidden" id="iParentid" name="iParentid" runat="server" />
	<input type="hidden" id="SytemType" name="SytemType" value="0" runat="server"/>
	<script type="text/javascript">classTitleInit();</script>
    </form>
</body>
</html>
