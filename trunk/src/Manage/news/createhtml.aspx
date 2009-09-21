﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createhtml.aspx.cs" Inherits="news_createhtml" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register tagPrefix="Manage" namespace="TCG.Manage.Controls" assembly="TCG.Manage"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>资讯生成</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/AllNewsClass.aspx"></script>
	<script type="text/javascript" src="<%=base.config["ScriptsSite"]%>Common/newscommon.aspx"></script>
	<script type="text/javascript" src="../js/MenuDiv.js"></script>
	<script type="text/javascript" src="../js/CreateDiv.js"></script>
	<script type="text/javascript" src="../js/calendar.js"></script>
	<script type="text/javascript" src="../js/newscreathtml.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckForm()">
	<div class="Page_title">资讯生成<span class="info1">(批量生成资讯的静态文件)</span></div>
	<Manage:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_g"><input name="StypeCheck" type="radio" id="orderTime" value="1" checked="checked"/>
	生成一段时间内的所有资源<span class="info2">(根据时间段来生成资源)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">根据字段：</span><select id="iTimeFeild" name="iTimeFeild" onchange="CheckValueIsNull('iTimeFeild','timefmsg');">
			<option value="-1">请选择...</option>
			<option value="1">资讯添加时间</option>
			<option value="2">最后修改时间</option>
		</select>
		<span class="info1" id="timefmsg">根据资讯添加时间，最后修改时间</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">开始时间：</span><input id="iStartTime" readonly="true" name="iStartTime" type="text"  class="itxt1" onfocus="this.className='itxt2';setDayHM(this);" onblur="CheckValueIsDateTime('iStartTime','starttimemsg');"/>
		<span class="info1" id="starttimemsg">时间段的开始！</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">结束时间：</span><input id="iEndTime" readonly="true" name="iEndTime" type="text"  class="itxt1" onfocus="this.className='itxt2';setDayHM(this);" onblur="CheckValueIsDateTime('iEndTime','endtimemsg');"/>
		<span class="info1" id="endtimemsg">时间段的结束时间！</span>
	</div>
	<div class="Page_g"><input id="orderClass" name="StypeCheck" type="radio" value="2" />生成一段时间内的所有资源<span class="info2">(根据资讯的类别来生成,含子类别！)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">资讯类别：</span><input id="iClassName" name="iClassName" type="text"  class="itxt1" onmouseover="selebg1();" onmouseout="selebg2();" onblur="CheckValueIsNull('iClassName','classmsg');" /><a id="SelectDivW" href="javascript:GoTo();" class="selectDiv sl_bg1" onmouseover="selebg1();" onmouseout="selebg2();" onclick="ShowNewsClassSl();"></a>
		<span class="info1" id="classmsg">资讯所属的分类，不能为空</span>
	</div>
	<div class="Page_g">公共选项<span class="info2">(对生成类型，数目进行限制)</span></div>
	<div class="Page_arrb">
		<span class="p_a_t1">资源范围：</span><label for="iCreated"><input name="Creat" type="radio" id="iCreated" value="1" checked="checked" />
		未生成与已生成的资源</label><label for="iCreat"><input id="iCreat" name="Creat" type="radio" value="2" />仅未成生过的资源</label>
		<span class="info1" id="createmsg">限制资讯的生成状态！</span>
	</div>
	<div class="Page_arrb">
		<span class="p_a_t1">附加条件：</span><input id="iCondition"  name="iCondition" type="text"  class="itxt1" onfocus="this.className='itxt2';"  onblur="" style="width:280px;"/>
		<span class="info1" id="numsmsg"> </span>
	</div>
	<div class="dobtn" style="margin-top:5px;"><input type="submit" id="btnok" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" /></div>
	<input type="hidden" id="iClassId" name="iClassId" value="-1"/>
	<input type="hidden" id="tClassId" name="tClassId" />
	<input type="hidden" id="iId" name="iId" />
	<input type="hidden" id="tCreated" name="tCreated" />
	<input type="hidden" id="work" name="work"/>
	<input type="hidden" id="iFilePath" name="iFilePath"/>
	<input type="hidden" id="page" name="page" value="1"/>
	<input type="hidden" id="iTopicWebPath" name="iTopicWebPath" value=""/>
	<script type="text/javascript">
		ClassInit();
		var iId=$("iId");
		var work=$("work");
		var tClassId=$("tClassId");
		var iFilePath=$("iFilePath");
		var tCreated=$("tCreated");
	</script>
    </form>
</body>
</html>