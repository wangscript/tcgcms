<%@ page language="C#" autoeventwireup="true" inherits="ConfigSetting, TCG.WebUI" enableviewstatemac="false" enableEventValidation="false" viewStateEncryptionMode="Never" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>系统参数设置</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/configsetting.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="js/common.js"></script>
	<script type="text/javascript" src="js/AJAXRequest.js"></script>
	<script type="text/javascript" src="js/configsetting.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFrom(this);">
    <div class="Page_title">系统参数设置<span class="info1">(系统运行的配置参数，请慎重修改！)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g">系统参数相关<span class="info2">(所有不能为空)</span></div>
	<TCG:Span id='items' runat='server' />
	<div class="dobtn"><input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" /></div>
    </form>
</body>
</html>