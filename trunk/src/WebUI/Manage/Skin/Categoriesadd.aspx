<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categoriesadd.aspx.cs" Inherits="skin_categoriesadd" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>�����Ѷ����</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../Common/AllTemplates.aspx"></script>
	<script type="text/javascript" src="../js/CreateDivV2.js"></script>
	<script type="text/javascript" src="../js/newsclasslist.js"></script>
	<script type="text/javascript" src="../js/newsclassadd.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
	<input type="hidden" id="iParentId" name="iParentId" runat="server" />
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server" />
    <input type="hidden" id="iMaxPId" name="iMaxPId" runat="server" />
    <div class="page_title" style="margin-bottom:5px;">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="$('#form1').submit();">
			<img src="../images/icon/save.gif" /> ����
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="javascript:window.history.back();">
			<img src="../images/icon/5.gif" />ȡ��
		</a>
        <a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />ˢ��
		</a>
        
	</div>
    <TCG:AjaxDiv ID="AjaxDiv1" runat="server" />

 
	    <div id="placemsg" class="Page_arrb arb_pr classaddline">
		    <span class="p_a_t">�� ϸ λ �ã�</span>
	    </div>
	    <script type="text/javascript">GetParentTitle();</script>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� �ƣ�</span>
	      <input id="iClassName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
		    <span id="cnamemsg" class="info1">�������ƣ�������д</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� ����</span>
	      <input id="iName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iName','inamemsg');"/>
		    <span id="inamemsg" class="info1">�����������ʾ��ǰ̨������</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">ָ �� Ŀ ¼��</span>
	      <input id="iDirectory" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iDirectory','dirmsg');"/>
		    <span id="dirmsg" class="info1">��Թ�����վ�ĵ�Ŀ¼��������ɵľ�̬�ļ�</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� ַ��</span>
	      <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" />
		    <span id="urlmsg" class="info1">��������վǰ̨����ҳ��ַ���������ɵ���</span>
	    </div>
        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�Ƿ�Ϊ��ҳ��</span>
          <input type="checkbox" name="iIsSinglePage" id="iIsSinglePage" runat="server" />
		    <span id="iIsSinglePagemsg" class="info1">���Ϊ��ҳ�������ӵ��÷����µ�����һƪ����</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� ϸ ģ �壺</span>
	      <select id="sTemplate" runat="server"  onchange="CheckTemplate('sTemplate','stdmsg')">
	  	    <option value="-1">��ѡ��</option>
	      </select>
		    <span id="stdmsg" class="info1">������Ѷ��ϸҳģ��</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� ģ �壺</span>
	      <select id="slTemplate" runat="server" onchange="CheckTemplate('slTemplate','stsdmsg')">
	  	    <option value="-1">��ѡ��</option>
	      </select>
		    <span id="stsdmsg" class="info1">������Ѷ�б�ҳģ��</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� ��</span>
	      <input id="iOrder" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		    <span id="urlmsg" class="info1">���б��е���ʾ˳��</span>
	    </div>
        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� չ ͼ��</span>
	      <input id="iPic" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		    <span id="iPicmsg" class="info1">���б��е���ʾ˳��</span>
	    </div>
   
	<div class="dobtn arb_pr">
		<input type="submit" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" />
	</div>
    </form>
</body>
</html>
