<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="TCG.CMS.WebUi.MyAccount" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>�����ʻ�</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
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
    <div class="Page_title">�����ʻ�<span class="info1">(���������ǳƣ���½����)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g">��½���<span class="info2">*Ϊ������</span></div>
	<div class="Page_arrb">
		<span class="p_a_t">�ǡ����ƣ�</span><input id="iNickName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		<span class="info1" id="nnmsg">��½ʱ����ʾ���������������Դ�����ı༭��</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">ԭʼ���룺</span><input id="iOldPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckPassword(this);"/>
		<span id="pwdmsg" class="info1">��֤���ĵ�½��Ϣ��</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">�������룺</span><input id="iNewPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckNewPassword(this);"/>
		<span id="npwdmsg" class="info1">���ĵ�������,���޸������գ�</span>
	</div>
	<div class="Page_arrb"><span class="p_a_t">ȷ�����룺</span><input id="iCPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckCPWD(this);"/>
		<span id="cpwdmsg" class="info1">ȷ�����������룡</span>
	</div>
	<div class="dobtn"><input type="button" onclick="return CheckForm();" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" /></div>
    </form>
</body>
</html>