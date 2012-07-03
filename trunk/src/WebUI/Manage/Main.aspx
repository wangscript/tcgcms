<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="TCG.CMS.WebUi.Main" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><TCG:Span id='title' runat='server' /></title>
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" /></head>
<frameset rows="91,*" cols="*" framespacing="0" frameborder="yes" border="0" bordercolor="#CCCCCC">
  <frame src="Top.aspx" name="frameTop" frameborder="no" scrolling="NO" bordercolor="#CCCCCC">
  <frameset rows="*" cols="188,*" framespacing="1" frameborder="yes" border="0" id="fram1">
    <frame src="Menu.aspx" name="menu" frameborder="no">
    <frame src="MainInfo.aspx" name="main" frameborder="no" bordercolor="#CCCCCC">
  </frameset>
</frameset>
<noframes> 
<body bgcolor="#FFFFFF" text="#000000">
</body>
</noframes> 
</html>
