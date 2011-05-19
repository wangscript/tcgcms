<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categoriesmdy.aspx.cs" Inherits="skin_categoriesmdy" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改资讯分类</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../Common/AllTemplates.aspx"></script>
	<script type="text/javascript" src="../js/newsclassadd.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title" style="margin-bottom:5px;">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="$('#form1').submit();">
			<img src="../images/icon/save.gif" /> 保存
		</a>
		<a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="javascript:window.history.back();">
			<img src="../images/icon/5.gif" />取消
		</a>
        <a href="javascript:GoTo();" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="refinsh();">
			<img src="../images/icon/07.gif" />刷新
		</a>
	</div>
    <TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
    <div class="g-tabnav">
        <a style="margin-left:-13px;"></a>
        <a id="a1" onclick="SetFromsByNum('a1')">基本信息</a>
        <a id="a2" onclick="SetFromsByNum('a2')">分类属性</a>
        <a id="a3" onclick="SetFromsByNum('a3')">数据源</a>
    </div>
    <div id="a1_from">
	    <div id="placemsg" class="Page_arrb arb_pr classaddline">
		    <span class="p_a_t">详 细 位 置：</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">父 类 编 号：</span>
	      <input id="iClassId" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
	      <script type="text/javascript">GetParentTitle();</script>
		    <span id="cnamemsg" class="info1">分类名称，必须填写</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 名 称：</span>
	      <input id="iClassName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
		    <span id="cnamemsg" class="info1">分类名称，必须填写</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 别 名：</span>
	      <input id="iName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iName','inamemsg');"/>
		    <span id="inamemsg" class="info1">分类别名，显示在前台的名称</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">指 定 目 录：</span>
	      <input id="iDirectory" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iDirectory','dirmsg');"/>
		    <span id="dirmsg" class="info1">相对管理网站的的目录，存放生成的静态文件</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分类 首地址：</span>
	      <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" />
		    <span id="urlmsg" class="info1">分类在网站前台的首页地址，用于生成导航</span>
	    </div>
        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">是否为单页：</span>
          <input type="checkbox" name="iIsSinglePage" id="iIsSinglePage" runat="server" />
		    <span id="iIsSinglePagemsg" class="info1">如果为单页，则链接到该分类下的最新一篇文章</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">详 细 模 板：</span>
	      <select id="sTemplate" runat="server"  onchange="CheckTemplate('sTemplate','stdmsg')">
	  	    <option value="-1">请选择</option>
	      </select>
		    <span id="stdmsg" class="info1">分类资讯详细页模板</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">列 表 模 板：</span>
	      <select id="slTemplate" runat="server" onchange="CheckTemplate('slTemplate','stsdmsg')">
	  	    <option value="-1">请选择</option>
	      </select>
		    <span id="stsdmsg" class="info1">分类资讯列表页模板</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 排 序：</span>
	      <input id="iOrder" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		    <span id="urlmsg" class="info1">在列表中的显示顺序</span>
	    </div>
    </div>
    <div id="a2_from"></div>
        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">属性名称：</span>
	      <input id="Text1" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		    <input type="radio" name="radio" id="raInPut" value="01" /><label for="raInPut">输入</label>
            <input type="radio" name="radio" id="raSSelect" value="02" /><label for="raSSelect">单选</label>
            <input type="radio" name="radio" id="raMSelect" value="03" /><label for="raMSelect">复选</label>
            <span id="Span1" class="info1">可选项目请用"|"间隔</span>
	    </div>
        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">可选项目：</span>
	      <input id="Text2" type="text" runat="server" value="0" style="width:480px;" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
	    </div>
    <div id="a3_from"></div>
	<div class="dobtn arb_pr">
	    <input type="hidden" id="Work" value="Mdy" />
	    <input type="hidden" id="skinid" runat="server" value="" />
		<input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" />
	</div>
    </form>
</body>
</html>