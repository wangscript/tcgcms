<%@ Page Language="C#" AutoEventWireup="true" CodeFile="templateadd.aspx.cs" Inherits="Template_templateadd" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>�����Ѷģ��</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../js/newstempadd.js"></script>
</head>
<body>
    <form id="TtempLateFromAdd" runat="server">
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
    <div class="Page_title">�����Ѷģ��<span class="info1">(�����Ѷģ���ļ�)</span><a href="javascript:fGoBack();" class="title_back bold">[����]</a></div>
	<div class="Page_g">������վ��<span class="info2" id="sSite"></span></div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">ģ�����ƣ�</span><input id="vcTempName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckTempName();" onkeyup="TempnageChange();"/>
		<span class="info1" id="tnmsg">��Ѷģ�������</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">ģ�����ͣ�</span><select id="tType" runat="server" onchange="CheckType();">
			<option value="-1" selected="selected">��ѡ��</option>
		</select>
		<span class="info1" id="typemsg">ģ������ͼ������С��ʾ��</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">ҳ���ַ��</span><input id="vcUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckUrl();" style="width:360px;"/>
		<span class="info1" id="urlmsg">��ҳʱ������д��ַ</span>
		<span class="info1" id="conmsg"></span>
	</div>
	<div class="Page_arrb arb_pr templateaddnew1">
		<textarea cols="140" rows="13" id="vcContent" name="vcContent" onblur="CheckContent()"></textarea>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="green bold">С��ʾ��</span>
		<span class="info1"><span class="red">��ҳ</span>-����վ��ĳ��ҳ�����ά����<span class="red">��Ѷ��ҳ</span>-��Ѷ��ʾҳ��<span class="red">��Ѷ���б�</span>-��Ѷ�����б���ʾҳ��ģ��</span>
	</div>
	<div class="dobtn arb_pr"><input type="submit" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" /></div>
	<input type="hidden" id="iSiteId" name="iSiteId" runat="server" />
	<input type="hidden" id="iParentid" name="iParentid" runat="server" />
    <input type="hidden" id="parentPath" name="parentPath" value="" runat="server"/>
	<input type="hidden" id="SytemType" name="SytemType" value="0" runat="server"/>
	
    </form>
</body>
</html>
