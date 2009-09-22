<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="aMenu" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>功能列表</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/menu.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="js/common.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />	
</head>
<script type="text/javascript">var stNums = new Array();</script>
<body>
<form name="form1" id="form1" runat="server">
<table width="186" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:3px;">
  <tr>
    <td class="menu_t2 t_b1" onmouseover="this.className='menu_t1 t_b1'" onmouseout="this.className='menu_t2 t_b1'">
		<a href="javascript:show();">展开</a>
	</td>
    <td class="menu_t2 t_b2" onmouseover="this.className='menu_t1 t_b2'" onmouseout="this.className='menu_t2 t_b2'">
		<a href="javascript:hidden();">收拢</a>
	</td>
  </tr>
</table>
<TCG:Span id='emu' runat='server' />
<TCG:Span id='temp1' runat='server'>
<div class="title_bg" id="menu_{0}"><a class="title_icon" onclick="ChangeIcon({0})" href="javascript:GoTo();" id="menu_{0}_icon">&nbsp;</a><a href="{1}" target="main" onclick="ChangeIcon({0})">{2}</a></div>
</tcg:span><TCG:Span id='temp2' runat='server'>
<div class="stitle sbg2" id="menu_{0}_{1}"><img src="images/icon/{4}.gif" /><a href="{2}" onmouseover="$('menu_{0}_{1}').className='stitle sbg1'" onmouseout="$('menu_{0}_{1}').className='stitle sbg2'" target="main">{3}</a></div>
</tcg:span>
</form>
</body>
</html>
