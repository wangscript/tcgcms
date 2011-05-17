<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourceshandlers.aspx.cs" Inherits="resources_resourceshandlers" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<%@ Register src="../Ctrl/Editor.ascx" tagname="WebUserControl" tagprefix="TCG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>�����Ѷ</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<link href="../css/enmu.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../Common/AllNewsSpeciality.aspx"></script>
	<script type="text/javascript" src="../js/enmu.js"></script>
    <script type="text/javascript" src="../js/layer.js"></script>
	<script type="text/javascript" src="../js/MenuDiv.js"></script>
	<script type="text/javascript" src="../js/newsadd.js"></script>
	
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var fileclassid = <asp:Literal ID="ltfileclassid" runat="server"></asp:Literal> ;
    </script>
    <div class="page_title" style="margin-bottom:5px;">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="SaveNewsInfo();">
			<img src="../images/icon/save.gif" /> ����
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="ShowFromDiv(this);">
			<img src="../images/icon/from.gif" />��Դ
		</a>
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="SpecialitySel(this);">
			<img src="../images/icon/6.gif" />����
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="javascript:window.history.back();">
			<img src="../images/icon/5.gif" />ȡ��
		</a>
	</div>
		

	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
    <div class="g-tabnav">
        <a style="margin-left:-13px;"></a>
        <a>������Ϣ</a>
        <a onmouseover="this.className='moson';" onmouseout="this.className ='';">��ϸ����</a>
        <a>��Դ����</a>
    </div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1">��Ѷ���⣺</span><input id="iTitle" name="iTitle" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" onblur="CheckValueIsNull('iTitle','titlemsg');" runat="server"/>
		<span class="info1" id="titlemsg">��Ѷ�������ݣ�����Ϊ��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1">����Ч����</span>
		��ɫ��
		<select name="sTitleColor" id="sTitleColor" runat="server">
		</select>
		�Ӵ�:<input type="checkbox" id="iStrong" name="iStrong" value="Y" runat="server"/>
		
		<span class="info1" id="Span1">��Ѷ�������ݣ�����Ϊ��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1">��ת��ַ��</span><input id="iUrl" name="iUrl" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" onblur="this.className='itxt1'" runat="server"/>
		<span class="info1" id="urlmsg">�������ת��õ�ַ��û�п�Ϊ��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1 lfl">��Ѷ��� </span>
		<span class="p_a_t1 lfl">
		<div class="cagegoriesSelect">
		    <input id="iClassName" name="iClassName" type="text"  class="itxt1" onblur="CheckValueIsNull('iClassName','classmsg');" />
		    <a id="SelectDivW" href="javascript:GoTo();" class="selectDiv sl_bg1"></a>
		
		    <div id="gamelist_c" class="enmu addselect">
                <div  class="c_box" id="gamelist">
		            <ul id="Cagetorie_c" class="one"></ul>
                </div>
            </div>
		</div>
		</span>
		<span class="info1" id="classmsg" style=" margin-left:210px;">��Ѷ�����ķ��࣬����Ϊ��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1">��Ѷ���ߣ�</span><input id="iAuthor" name="iAuthor" type="text"  class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iAuthor','autmsg');" runat="server"/>
		<span class="info1" id="autmsg">��Ѷ���ߣ���Ϊ��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<span class="p_a_t1">�� �� �֣�</span><input id="iKeyWords" name="iKeyWords" type="text"  class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iKeyWords','keymsg');" style="width:400px;" runat="server"/>
		<span class="info1" id="keymsg">���������Դ�ı��</span>
	</div>
	<div class="Page_arrb arb_pr1">
		<input id="iBigImg" name="iBigImg" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" runat="server"/><img src="../images/icon/fj.gif" /> <a href="javascript:GoTo();"  onclick="SelectBigImg(this)">���ô�ͼƬ</a>
	</div>
	<div class="Page_arrb arb_pr1">
	    <input id="iSmallImg" name="iSmallImg" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" runat="server"/><img src="../images/icon/fj.gif" /> <a href="javascript:GoTo();"  onclick="SelectSmallImg(this)">����СͼƬ</a>
	</div>
	<div class="Page_arrb arb_pr1 templateaddnew1">
		<TCG:WebUserControl ID="iContent" runat="server" />
	</div>
	<div class="Page_arrb arb_pr1 templateaddnew2">
		<textarea id="iShortContent" name="iShortContent" type="text"  class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iKeyWords','keymsg');" style="width:800px; height:80px; margin-top:5px;" runat="server"/>
	</div>
	<div class="imgPace hid" id="imgPace"></div>
	<div class="dobtn arb_pr" style="margin-top:5px;clear:left;"><input type="button" onclick="SaveNewsInfo()" id="btnok" class="btn2 bold" value="ȷ��"/>������<input type="reset" class="btn2" value="ȡ��" /></div>
	<input type="hidden" id="iClassId" name="iClassId" runat="server"/>
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server"/>
	<input type="hidden" id="iFrom" name="iFrom" value="1" runat="server"/>
	<input type="hidden" id="iNewsId" name="iNewsId" runat="server"/>
	<input type="hidden" id="work" name="work" value="AddNew" runat="server"/>
	<input type="hidden" id="iSpeciality" name="iSpeciality" value="" runat="server"/>
	<input type="hidden" id="iTitleColor" name="iTitleColor" value="" runat="server"/>
    </form>
	
</body>
</html>