<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categorieshandlers.aspx.cs" Inherits="TCG.CMS.WebUi.skin_categorieshandlers" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改资讯分类</title>
	<link href="../css/base.css" rel="stylesheet" type="text/css" />
	<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
	<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/enmu.css" rel="stylesheet" type="text/css" />
    <link href="../css/layer.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/commonV2.js"></script>
	<script type="text/javascript" src="../Common/common.aspx"></script>
	<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="../js/jquery.form.js"></script>
	<script type="text/javascript" src="../Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="../Common/AllTemplates.aspx"></script>
	<script type="text/javascript" src="../js/newsclassadd.js"></script>
    <script type="text/javascript" src="../js/newscommon.js"></script>
    <script type="text/javascript" src="../js/enmu.js"></script>
	<script type="text/javascript" src="../js/MenuDiv.js"></script>
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="skinid" runat="server" value="" />
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
            <a id="a2" onclick="SetFromsByNum('a2')">模版配置</a>
            <a id="a3" onclick="SetFromsByNum('a3')">特殊配置</a>
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
            <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 排 序：</span>
	          <input id="iOrder" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		        <span id="Span1" class="info1">在列表中的显示顺序</span>
	        </div>
        </div>
        <div id="a2_from">
	        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">详情页生成目录：</span>
	          <input id="iDirectory" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iDirectory','dirmsg');"/>
		        <span id="dirmsg" class="info1">相对管理网站的的目录，存放生成的静态文件</span>
	        </div>
	        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">列表页生成目录：</span>
	            <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" />
		        <span id="urlmsg" class="info1">分类在网站前台的首页地址，用于生成导航</span>
	        </div>
            <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">是 否 为 单 页：</span>
              <input type="checkbox" name="iIsSinglePage" id="iIsSinglePage" runat="server" />
		        <span id="iIsSinglePagemsg" class="info1">如果为单页，则链接到该分类下的最新一篇文章</span>
	        </div>
	        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">详 情 页 模 板：</span>
	          <select id="sTemplate" runat="server"  onchange="CheckTemplate('sTemplate','stdmsg')">
	  	        <option value="-1">请选择</option>
	          </select>
		        <span id="stdmsg" class="info1">分类资讯详细页模板</span>
	        </div>
	        <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">列 表 页 模 板：</span>
	          <select id="slTemplate" runat="server" onchange="CheckTemplate('slTemplate','stsdmsg')">
	  	        <option value="-1">请选择</option>
	          </select>
		        <span id="stsdmsg" class="info1">分类资讯列表页模板</span>
	        </div>
        </div>
        
	    <div id="a3_from">
            <div class="Page_arrb arb_pr classaddline">
		        <span class="p_a_t lfl">特 性 选 择： </span>
		        <span class="p_a_t lfl">
		        <div class="cagegoriesSelect">
		            <input id="iSpeciality_t" name="iSpeciality_t" type="text"  class="itxt1" />
		            <a id="SelectDivWW" href="javascript:GoTo();" class="selectDiv sl_bg1"></a>
		
		            <div id="iSpeciality_1" class="enmu addselect">
                        <div  class="c_box" id="iSpeciality_c">
		                    <ul id="iSpeciality_cc" class="one"></ul>
                        </div>
                    </div>
		        </div>
		        </span>
		        <span class="info1" id="iSpeciality_msg" style=" margin-left:210px;">设置资讯特性，可以让文章显示在特殊的位置!</span>
	        </div>
            <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">分 类 展 图：</span>
	          <input id="iPic" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		        <span id="iPicmsg" class="info1">在列表中的显示顺序</span>
	        </div>

            <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">导 航 显 示：</span>
	          <select id="sVisite" runat="server"  onchange="CheckTemplate('sVisite','sVisitemsg')">
	  	        <option value="Y">显示</option>
                <option value="N">不显示</option>
	          </select>
		        <span id="sVisitemsg" class="info1">分类资讯详细页模板</span>
	        </div>
        </div>

	<div class="dobtn arb_pr">
	    <input type="hidden" id="Work" value="Mdy" />
        <input type="hidden" id="iMaxPId" name="iMaxPId" runat="server" />
	    
        <input type="hidden" id="iSpeciality" name="iSpeciality" value="" runat="server"/>
		<input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" />
	</div>
    </form>
</body>
</html>