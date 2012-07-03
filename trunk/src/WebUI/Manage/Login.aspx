<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="TCG.CMS.WebUi.aLogin" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title><TCG:Span id='title' runat='server' /></title>
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/login.css" rel="stylesheet" type="text/css" />
	<link href="css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/login.js"></script>
</head>
<body>
<div class="login">
	<form id="form1" runat="server">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
	  <tr>
		<td><ul class="title">
				<li><TCG:Span id='ltitle' runat='server' /></li>
			</ul>
			<ul class="month">
				<li><TCG:Span id='month' runat='server' /></li>
			</ul></td>
	  </tr>
	</table>

	<table width="100%" height="111" border="0" cellpadding="0" cellspacing="0" id="tblogin" >
	  <tr>
	    <td width="23%" align="right"><span class="txt1">用户名：</span></td>
        <td width="77%" align="left"><span>
        <input type="text" class="btn" id="username" onfocus="this.className='btn1';" onblur="this.className='btn';" runat="server"/></span></td>
	  </tr>
	  <tr>
	    <td align="right"><span class="txt1">密　码：</span></td>
        <td align="left">
		<span>
		<input type="password" class="btn" id="password" onfocus="this.className='btn1';" onblur="this.className='btn';" runat="server"/></span></td>
	  </tr>
	  <tr>
	    <td align="right">&nbsp;</td>
        <td align="left"><input type="checkbox" id="rUsername" value="checkbox" runat="server" />
        <span class="txt1">记住登录名</span></td>
	  </tr>
	</table>
	<div id="tbupdate" style=" display:none;"></div>
	<table width="100%" height="44" border="0" cellpadding="0" cellspacing="0">
	  <tr>
		<td align="center">
		<input type="hidden" id="SqlSep" name="SqlSep" value="1" />
		<input type="hidden" id="Work" name="Work" value="LOGIN" />
	    <input type="submit" class="btn_submit" value="登陆" id="btn_ok" />
		<input name="btn_ret" type="reset" class="btn_c" value="重置" onclick="return oncunle();" id="btn_ret" />
		</td>
	  </tr>
  </table>
  <div id="errText" class="errText"></div>
  <table width="100%" height="77" border="0" cellpadding="0" cellspacing="0">
	  <tr>
		<td>&nbsp;</td>
      </tr>
	  <tr>
	    <td align="left" class="STYLE1">　　<span class="txt2">·</span>退出系统前先注销用户,同名管理员无法从另一IP登录</td>
    </tr>
	  <tr>
	    <td align="left" class="STYLE1">　　<span class="txt2">·</span>本系统要求服务器端及客户端均安装有IE5.5以上版本。</td>
    </tr>
  </table>
  </form>
</div>
</body>
</html>