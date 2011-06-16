<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminmdy.aspx.cs" Inherits="TCG.CMS.WebUi.adminmdy" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>�޸Ĺ���Ա</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/admincommon.js"></script>
	<script type="text/javascript" src="js/adminmdy.js"></script>
	<script type="text/javascript" src="Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="Common/AllPop.aspx"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckForm(1);">
    <input type="hidden" id="DefaultSkinId" name="DefaultSkinId" runat="server" />
    <input type="hidden" id="adminname" name="adminname" runat="server" />
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_title bold">
		�޸Ĺ���Ա <a href="javascript:fGoBack();" class="title_back bold">[����]</a>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">��&nbsp;&nbsp;½&nbsp;&nbsp;����</span>
		<input id="vcAdminName" disabled="disabled" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2';$('adminmsg').text('��½����');"  onblur="CheckAdminName();"/>
		<span class="info1" id="adminmsg">��½����</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ƣ�</span>
		 <input id="iNickName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		<span class="info1" id="nnmsg">��½ʱ����ʾ���������������Դ�����ı༭��</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">��½���룺</span>
	  <input id="iNewPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" />
		<span id="npwdmsg" class="info1">��½����,���޸������գ�</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">ȷ�����룺</span>
	 <input id="iCPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckCPWD(this);"/>
		<span id="cpwdmsg" class="info1">ȷ�����������룡</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">������ɫ��</span>
	 <select id="sRole" runat="server" onchange="CheckRole();">
	 	<option value="0" selected="selected">��ѡ���ɫ</option>
	 </select>
		<span id="rolemsg" class="info1">�������Ա�����ɫ��</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">�Ƿ�������</span>
	  <input type="radio" id="iLockY" name="iLock" value="Y"  runat="server"/> ����
	  <input type="radio" id="iLockN" name="iLock" value="N"  runat="server"/> δ��
	  <span id="lockmsg" class="info1">�����ù���Ա״̬��</span>	</div>
	<div class="Page_arrb arb_pr poph nfl">
		<span class="p_a_t lfl"><p class="green bold">�ɷ���Ĺ���Ȩ��</p>
			<select size="12" multiple="true" id="vcPopedom"  class="popselect" onchange="SetPopValue('popedom',this);">
			</select>
			<input type="hidden" id="popedom" name="popedom" runat="server"/>
			<script type="text/javascript">PopSelectInit();</script>
		</span>
		<span class="p_a_t lfl popml"><p class="green bold">�ɷ�������·���Ȩ��</p>
			<select size="12" multiple="true" id="vcClassPopedom" class="popselect" onchange="SetPopValue('classpopedom',this);">
			</select>
			<input type="hidden" id="classpopedom" name="classpopedom" runat="server"/>
			<script type="text/javascript">NewsSelectInit();</script>
		</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="green bold">С��ʾ��</span>
		<span class="info1">��סSHIFT�����Զ�ѡ</span>
	</div>
	<div class="dobtn arb_pr">
		<input type="submit" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" />
	</div>
    </form>
    </form>
</body>
</html>