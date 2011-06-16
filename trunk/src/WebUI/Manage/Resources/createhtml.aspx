<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createhtml.aspx.cs" Inherits="TCG.CMS.WebUi.news_createhtml" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>��Ѷ����</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<link href="../css/enmu.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
    <script type="text/javascript" src="../js/newscommon.js"></script>
	<script type="text/javascript" src="../js/listcommon.js"></script>
    <script type="text/javascript" src="../js/enmu.js"></script>
	<script type="text/javascript" src="../js/CreateDivV2.js"></script>
	<script type="text/javascript" src="../js/calendar.js"></script>
	<script type="text/javascript" src="../js/layer.js"></script>
	<script type="text/javascript" src="../js/newscreathtml.js"></script>
</head>
<body>
    <form id="form1" runat="server">
	<div class="Page_title">��Ѷ����<span class="info1">(����������Ѷ�ľ�̬�ļ�)</span></div>
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g"><input name="StypeCheck" type="radio" id="orderTime" value="1" />
	����һ��ʱ���ڵ�������Դ<span class="info2">(����ʱ�����������Դ)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">�����ֶΣ�</span><select id="iTimeFeild" name="iTimeFeild" onchange="CheckValueIsNull('iTimeFeild','timefmsg');">
			<option value="-1">��ѡ��...</option>
			<option value="1">��Ѷ���ʱ��</option>
			<option value="2">����޸�ʱ��</option>
		</select>
		<span class="info1" id="timefmsg">������Ѷ���ʱ�䣬����޸�ʱ��</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">��ʼʱ�䣺</span><input id="iStartTime" readonly="true" name="iStartTime" type="text"  class="itxt1" onfocus="this.className='itxt2';setDayHM(this);" onblur="CheckValueIsDateTime('iStartTime','starttimemsg');"/>
		<span class="info1" id="starttimemsg">ʱ��εĿ�ʼ��</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">����ʱ�䣺</span><input id="iEndTime" readonly="true" name="iEndTime" type="text"  class="itxt1" onfocus="this.className='itxt2';setDayHM(this);" onblur="CheckValueIsDateTime('iEndTime','endtimemsg');"/>
		<span class="info1" id="endtimemsg">ʱ��εĽ���ʱ�䣡</span>
	</div>
	<div class="Page_g"><input id="orderClass" name="StypeCheck" type="radio" checked="checked" value="2" />ָ�����༰���ӷ����µ���ѯ���б�����е�ҳģ��<span class="info2">(������Ѷ�����������,�������)</span></div>
	<div class="Page_arrb ">
		<span class="p_a_t1 lfl">��Ѷ���</span>
		<span class="p_a_t1 lfl">
		    <div class="cagegoriesSelect" style=" margin-top:10px;">
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
	<div class="Page_g">����ѡ��<span class="info2">(���������ͣ���Ŀ��������)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">��Դ��Χ��</span><label for="iCreated"><input name="Creat" type="radio" id="iCreated" value="1" checked="checked" />
		δ�����������ɵ���Դ</label><label for="iCreat"><input id="iCreat" name="Creat" type="radio" value="2" />��δ����������Դ</label>
		<span class="info1" id="createmsg">������Ѷ������״̬��</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">����������</span><input id="iCondition"  name="iCondition" type="text"  class="itxt1" onfocus="this.className='itxt2';"  onblur="" style="width:280px;"/>
		<span class="info1" id="numsmsg"> </span>
	</div>
	<div class="dobtn" style="margin-top:5px;"><input type="button"  onclick="StartCreate();" id="btnok" class="btn2 bold" value="ȷ��" />������<input type="reset" class="btn2" value="ȡ��" /></div>
	<input type="hidden" id="iClassId" name="iClassId" value="-1"/>
	<input type="hidden" id="tClassId" name="tClassId" />
	<input type="hidden" id="iId" name="iId" />
	<input type="hidden" id="tCreated" name="tCreated" />
	<input type="hidden" id="work" name="work"/>
	<input type="hidden" id="iFilePath" name="iFilePath"/>
	<input type="hidden" id="page" name="page" value="1"/>
	<input type="hidden" id="iTopicWebPath" name="iTopicWebPath" value=""/>
	<input type="hidden" id="iSkinId" name="iSkinId" runat="server"/>
    </form>
</body>
</html>