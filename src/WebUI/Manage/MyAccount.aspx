<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="TCG.CMS.WebUi.MyAccount" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>个人帐户</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/admincommon.js"></script>
	<script type="text/javascript" src="js/MyAccount.js"></script>
</head>
<body>
    <form id="form1" runat="server">
	<input type="hidden" id="adminname" runat="server" />
    <div class="Page_title">个人帐户<span class="info1">(更改您的昵称，登陆密码)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g">登陆相关<span class="info2">*为必填项</span></div>
	<div class="Page_arrb">
		<span class="p_a_t">昵　　称：</span><input id="iNickName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		<span class="info1" id="nnmsg">登陆时的显示名，将会出现在资源发布的编辑者</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">原始密码：</span><input id="iOldPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckPassword(this);"/>
		<span id="pwdmsg" class="info1">验证您的登陆信息！</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">新设密码：</span><input id="iNewPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckNewPassword(this);"/>
		<span id="npwdmsg" class="info1">更改的新密码,不修改请留空！</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">确认密码：</span><input id="iCPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckCPWD(this);"/>
		<span id="cpwdmsg" class="info1">确认您的新密码！</span>
	</div>
	<div class="dobtn"><input type="button" onclick="return CheckForm();" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" /></div>
    </form>
</body>
</html>