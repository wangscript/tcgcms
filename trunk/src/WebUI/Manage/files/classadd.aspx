<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classadd.aspx.cs" Inherits="TCG.CMS.WebUi.files_classadd" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>�ޱ���ҳ</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="../js/filseclassadd.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Page_title">�����µ��ļ�����<span class="info1">(���������ǳƣ���½����)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g">�������<span class="info2">*Ϊ������</span></div>
    </form>
</body>
</html>
