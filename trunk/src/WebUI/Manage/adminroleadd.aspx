<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminroleadd.aspx.cs" Inherits="TCG.CMS.WebUi.adminroleadd" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>��ӽ�ɫ��</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="js/admincommon.js"></script>
	<script type="text/javascript" src="js/adminroleadd.js"></script>
	<script type="text/javascript" src="Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="Common/AllPop.aspx"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="DefaultSkinId" name="DefaultSkinId" runat="server" />
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_title bold">
		<a class="title_back bold" href="adminadd.aspx">�½�����Ա</a> | �½���ɫ��<a href="javascript:fGoBack();" class="title_back bold">[����]</a>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">��ɫ����</span><input id="vcRoleName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'"  onblur="CheckRoleName();"/>
		<span class="info1 red" id="rnmsg">*������Ŀ����ɫ����</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">��&nbsp;&nbsp;&nbsp;&nbsp;�飺</span><input id="vcContent" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
	</div>
	<div class="Page_arrb arb_pr poph nfl">
		<span class="p_a_t lfl"><p class="green bold">�ɷ���Ĺ���Ȩ��</p>
			<select size="12" multiple="true" id="vcPopedom" runat="server" class="popselect" onchange="SetPopValue('popedom',this);">
			</select>
			<input type="hidden" id="popedom" name="popedom" />
			<script type="text/javascript">PopSelectInit();</script>
		</span>
		<span class="p_a_t lfl popml"><p class="green bold">�ɷ�������·���Ȩ��</p>
			<select size="12" multiple="true" id="vcClassPopedom" runat="server" class="popselect" onchange="SetPopValue('classpopedom',this);">
			</select>
			<input type="hidden" id="classpopedom" name="classpopedom" />
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
</body>
</html>
