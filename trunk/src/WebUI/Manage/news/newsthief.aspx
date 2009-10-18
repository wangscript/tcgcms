<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsthief.aspx.cs" Inherits="news_newsthief" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>文章抓取程序</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["ScriptsSite"]%>Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="<%=base.configService.baseConfig["ScriptsSite"]%>Common/newscommon.aspx"></script>
	<script type="text/javascript" src="../js/MenuDiv.js"></script>
	<script type="text/javascript" src="../js/CreateDiv.js"></script>
	<script type="text/javascript" src="../js/newshief.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckForm()">
    <div class="Page_title">抓取资讯<span class="info1">(批量抓取网络资讯资料)</span></div>
	<div class="Page_g">列表规则<span class="info2">(获得列表中间资讯的连接规则)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">列表地址：</span><input id="iListPage" name="iListPage" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iListPage','iListPagemsg');" style="width:380px;"/>
		<span class="info1" id="iListPagemsg">页数用{0}表示</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">起　始　页：</span><input id="iListPageStart" name="iListPageStart" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iListPageStart','PageStartmsg');" style="width:30px;"/>-<input id="iListPageEnd" name="iListPageEnd" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iListPageEnd','PageStartmsg');" style="width:30px;"/>
	    <span class="info1" id="PageStartmsg">列表页面的开始和结束页面，请填写数字</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">编码规则：</span><input id="iCharSet" name="iCharSet" type="text"  class="itxt1" onfocus="this.className='itxt2';" style="width:130px;"/>
	    <span class="info1" id="iCharSetmsg">不填写对默认为GB2312</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">资讯类别：</span><input id="iClassName" name="iClassName" type="text"  class="itxt1" onmouseover="selebg1();" onmouseout="selebg2();" onblur="CheckValueIsNull('iClassName','classmsg');" /><a id="SelectDivW" href="javascript:GoTo();" class="selectDiv sl_bg1" onmouseover="selebg1();" onmouseout="selebg2();" onclick="ShowNewsClassSl();"></a>
		<span class="info1" id="classmsg">资讯所属的分类，不能为空</span>
	</div>
	<div class="Page_arrb" style="height:80px;"> <span class="p_a_t1">列区规则：</span>
		<textarea id="iListArea" name="iListArea" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iListArea','iListAreamsge');" style="width:380px; height:60px;"></textarea>
		<span class="info1" id="iListAreamsge">找到列表区域的正则</span>	
	</div>
	<div class="Page_arrb" style="height:80px;"> <span class="p_a_t1">链接规则：</span>
		<textarea id="iListLink" name="iListLink" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iListLink','iListLinkmsg');" style="width:380px; height:60px;"></textarea>
		<span class="info1" id="iListLinkmsg">找到列表区域的正则</span>
	</div>
	<div class="Page_g" style="clear:both;">文章规则<span class="info2">(获得资讯具体内容的规则)</span></div>
	<div class="Page_arrb" style="height:80px;"> <span class="p_a_t1">列区规则：</span>
		<textarea id="iTopicArea" name="iTopicArea" type="text"  class="itxt1" onfocus="this.className='itxt2';" onblur="CheckValueIsNull('iTopicArea','iListLinkmsgs');" style="width:380px; height:60px;"></textarea>
		<span class="info1" id="iListLinkmsgs">找到文章的具体内容，标题$1 内容$2</span>	
	</div>
	<div class="dobtn" style="margin-top:5px;"><input type="submit" id="btnok" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" /></div>
	<input type="hidden" id="iClassId" name="iClassId" value="-1"/>
	<input type="hidden" id="work" name="work" value="-1"/>
	<input type="hidden" id="iTopicWebPath" name="iTopicWebPath"/>
	<script type="text/javascript">ClassInit();</script>
    </form>
</body>
</html>