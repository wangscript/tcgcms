<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Top.aspx.cs" Inherits="TCG.CMS.WebUi.Admin_Top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>帐户列表-头部</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/adminlist.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/admintop.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="window.parent.adminpop.GetAddDiv();">
			<img src="images/icon/03.gif" />新建
		</a>
		<a href="#" class="tnew1" onmouseover="this.className='tnew1 nbg1'" onmouseout="this.className='tnew1'" onclick="window.parent.adminpop.GetAddGroupDiv();">
			<img src="images/icon/04.gif" />加入组
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AdminEdit();">
			<img src="images/icon/05.gif" />编辑
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="AdminDel();">
			<img src="images/icon/06.gif" />删除
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="images/icon/07.gif" />刷新
		</a>
	</div>
	<input type="hidden" id="admins" name="admins" />
    </form>
</body>
</html>