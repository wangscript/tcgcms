<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigSetting.aspx.cs" Inherits="TCG.CMS.WebUi.ConfigSetting" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>ϵͳ��������</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/configsetting.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="js/common.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/AJAXRequest.js"></script>
	<script type="text/javascript" src="js/configsetting.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFrom(this);">
    <div class="Page_title">ϵͳ��������<span class="info1">(ϵͳ���е����ò������������޸ģ�)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g">ϵͳ�������<span class="info2">(���в���Ϊ��)</span></div>
	<TCG:Span id='items' runat='server' />
	<div class="dobtn"><input type="submit" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" /></div>
    </form>
</body>
</html>