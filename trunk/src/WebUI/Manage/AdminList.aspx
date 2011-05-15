<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminList.aspx.cs" Inherits="AdminList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>帐户列表</title>
    <meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="js/common.js"></script>
</head>
<div style=" margin-top:12px;">
<frameset rows="37,*" cols="*" framespacing="0" frameborder="yes" border="0" bordercolor="#CCCCCC">
  <frame src="Admin_Top.aspx" name="adminframeTop" frameborder="no" scrolling="NO" bordercolor="#CCCCCC">
  <frameset rows="*" cols="160,*" framespacing="1" frameborder="yes" border="0" id="fram1">
    <frame src="admin_pop.aspx" name="adminpop" frameborder="no">
    <frame src="adminInfo.aspx" name="adminmain" frameborder="no" bordercolor="#CCCCCC">
  </frameset>
</frameset>
</div>
</html>