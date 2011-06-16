<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourceshandlers.aspx.cs" Inherits="TCG.CMS.WebUi.resources_resourceshandlers" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="../Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>添加资讯</title>
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
	<script type="text/javascript" id="cptid" src="../Common/CategorieProperties.aspx?cid=<asp:Literal ID='cid' runat='server'></asp:Literal>"></script>
    <script type="text/javascript" src="../Common/ResourcesProperties.aspx?nid=<asp:Literal ID='nid' runat='server'></asp:Literal>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var fileclassid = <asp:Literal ID="ltfileclassid" runat="server"></asp:Literal> ;
    </script>
    <div class="page_title" style="margin-bottom:5px;">
		<a href="#" class="tnew" onmouseover="this.className='tnew nbg'" onmouseout="this.className='tnew'" onclick="SaveNewsInfo();">
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
        <a id="a2" onclick="SetFromsByNum('a2')">详细内容</a>
        <a id="a3" onclick="SetFromsByNum('a3')">资源属性</a>
    </div>
    <div id="a1_from">
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1">资讯标题：</span><input id="iTitle" name="iTitle" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" onblur="CheckValueIsNull('iTitle','titlemsg');" runat="server"/>
		    <span class="info1" id="titlemsg">资讯标题内容，不能为空</span>
	    </div>
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1">标题效果：</span>
		    颜色：
		    <select name="sTitleColor" id="sTitleColor" runat="server">
		    </select>
		    加粗:<input type="checkbox" id="iStrong" name="iStrong" value="Y" runat="server"/>
		
		    <span class="info1" id="Span1">资讯标题内容，不能为空</span>
	    </div>
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1">跳转地址：</span><input id="iUrl" name="iUrl" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" onblur="this.className='itxt1'" runat="server"/>
		    <span class="info1" id="urlmsg">点击标题转向该地址，没有可为空</span>
	    </div>
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1 lfl">资讯类别： </span>
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
		    <span class="info1" id="classmsg" style=" margin-left:210px;">资讯所属的分类，不能为空</span>
	    </div>

        <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1 lfl">特新选择： </span>
		    <span class="p_a_t1 lfl">
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
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1">资讯作者：</span><input id="iAuthor" name="iAuthor" type="text"  class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iAuthor','autmsg');" runat="server"/>
		    <span class="info1" id="autmsg">资讯作者，能为空</span>
	    </div>
	    <div class="Page_arrb arb_pr1">
		    <span class="p_a_t1">关 键 字：</span><input id="iKeyWords" name="iKeyWords" type="text"  class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iKeyWords','keymsg');" style="width:400px;" runat="server"/>
		    <span class="info1" id="keymsg">生成相关资源的标记</span>
	    </div>
	    <div class="Page_arrb arb_pr1">
		    <input id="iBigImg" name="iBigImg" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" runat="server"/><img src="../images/icon/fj.gif" /> <a href="javascript:GoTo();"  onclick="SelectBigImg(this)">设置大图片</a>
	    </div>
	    <div class="Page_arrb arb_pr1">
	        <input id="iSmallImg" name="iSmallImg" type="text"  class="itxt1" onfocus="this.className='itxt2'"  style="width:400px;" runat="server"/><img src="../images/icon/fj.gif" /> <a href="javascript:GoTo();"  onclick="SelectSmallImg(this)">设置小图片</a>
	    </div>
        <div class="Page_arrb arb_pr1">
		    <textarea id="iShortContent" name="iShortContent" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iKeyWords','keymsg');" style="width:800px; height:80px; margin-top:5px;" runat="server"/>
	    </div>
    </div>
    <div id="a2_from">
	    <div class="Page_arrb arb_pr1">
		    <script type="text/javascript" charset="gb2312" src="../KindEditer/kindeditor-min.js"></script>
                <script type="text/javascript">
                    KE.show({
                        id: 'taContent',
                        imageUploadJson: '/manage/upload/editUploadfile.aspx',
                        fileManagerJson: '/manage/upload/filemanager.aspx',
                        allowFileManager: true,
                        afterCreate: function (id) {
                            KE.event.ctrl(document, 13, function () {
                                KE.util.setData(id);
                                document.forms['form1'].submit();
                            });
                            KE.event.ctrl(KE.g[id].iframeDoc, 13, function () {
                                KE.util.setData(id);
                                document.forms['form1'].submit();
                            });
                        }
                    });
            </script>
            <textarea id="taContent" cols="100" rows="8" style="width:800px;height:300px;visibility:hidden;" runat="server"></textarea>
	    </div>
    </div>
    <div id="a3_from">
        <div class="Page_g">资讯属性：<select id="scp" name="scp" runat="server" onchange="ChangePCtype(this)">
        </select></div>
        <div id="a3_form_p">
        </div>
    </div>
	<div class="dobtn arb_pr1 Page_arrb" style="margin-top:5px;">
        <input type="button" onclick="SaveNewsInfo()" id="btnok" class="btn2 bold" value="确定"/>　　　<input type="reset" class="btn2" value="取消" />
    </div>
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