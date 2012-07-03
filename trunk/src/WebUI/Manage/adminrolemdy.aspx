<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminrolemdy.aspx.cs" Inherits="TCG.CMS.WebUi.adminrolemdy" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>添加角色组</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
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
		编辑角色组<a href="javascript:fGoBack();" class="title_back bold">[返回]</a>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">角色名：</span><input id="vcRoleName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'"  onblur="CheckRoleName();"/>
		<span class="info1 red" id="rnmsg">*必填项目，角色名称</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">简&nbsp;&nbsp;&nbsp;&nbsp;介：</span><input id="vcContent" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
	</div>
	<div class="Page_arrb arb_pr poph nfl">
		<span class="p_a_t lfl"><p class="green bold">可分配的功能权限</p>
			<select size="12" multiple="true" id="vcPopedom" runat="server" class="popselect" onchange="SetPopValue('popedom',this);">
			</select>
			<input type="hidden" id="popedom" name="popedom" runat="server" />
			<script type="text/javascript">PopSelectInit();</script>
		</span>
		<span class="p_a_t lfl popml"><p class="green bold">可分配的文章分类权限</p>
			<select size="12" multiple="true" id="vcClassPopedom" runat="server" class="popselect" onchange="SetPopValue('classpopedom',this);">
			</select>
			<input type="hidden" id="classpopedom" name="classpopedom" runat="server" />
			<script type="text/javascript">NewsSelectInit();</script>
		</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="green bold">小提示：</span>
		<span class="info1">按住SHIFT键可以多选</span>
	</div>
	<div class="dobtn arb_pr">
	    
		<input type="submit" class="btn2 bold" value="确定" />　　　<input type="reset" class="btn2" value="取消" />
	</div>
    </form>
</body>
</html>