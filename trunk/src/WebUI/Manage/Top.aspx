<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="TCG.CMS.WebUi.Top" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>ͷ��</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/top.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/top.js"></script>
</head>
<body>
<table width="100%" height="52" border="0" cellpadding="0" cellspacing="0">
	<form id="form1" runat="server">
	<tr>
		<td width="13%" rowspan="2"><img src="images/logo.gif" width="160" height="44" /></td>
		<td width="66%" align="left" class="logininf">
			&nbsp;&nbsp;���ã�<strong>
			<TCG:Span id='adminName' runat='server' /></strong> [
			<a onclick="LoginOut()" href="#">�˳�</a>&nbsp;&nbsp;
          	<a onclick="GoMyAccount();" href="MyAccount.aspx" target="main">�����ʻ�</a>]
	  </td>
		<td width="21%" rowspan="2">&nbsp;</td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  </tr>
	</form>
</table>
<TCG:Span id='emu' runat="server"></TCG:Span>
<script type="text/javascript">emuInit();</script>
</body>
</html>