<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminmdy.aspx.cs" Inherits="TCG.CMS.WebUi.adminmdy" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<%@ Register src="Ctrl/AjaxDiv.ascx" tagname="AjaxDiv" tagprefix="TCG"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改管理员</title>
	<link href="css/base.css" rel="stylesheet" type="text/css" />
	<link href="css/admininfo.css" rel="stylesheet" type="text/css" />
	<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
	<script type="text/javascript" src="js/commonV2.js"></script>
	<script type="text/javascript" src="Common/common.aspx"></script>
	<script type="text/javascript" src="js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="js/jquery.form.js"></script>
	<script type="text/javascript" src="js/admincommon.js"></script>
	<script type="text/javascript" src="js/adminmdy.js"></script>
	<script type="text/javascript" src="Common/AllCategories.aspx"></script>
	<script type="text/javascript" src="Common/AllPop.aspx"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckForm(1);">
    <input type="hidden" id="DefaultSkinId" name="DefaultSkinId" runat="server" />
    <input type="hidden" id="adminname" name="adminname" runat="server" />
	<TCG:AjaxDiv ID="AjaxDiv1" runat="server" />
	<div class="Page_title bold">
		修改管理员 <a href="javascript:fGoBack();" class="title_back bold">[返回]</a>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">登&nbsp;&nbsp;陆&nbsp;&nbsp;名：</span>
		<input id="vcAdminName" disabled="disabled" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2';$('adminmsg').text('登陆名称');"  onblur="CheckAdminName();"/>
		<span class="info1" id="adminmsg">登陆名称</span>
	</div>
	<div class="Page_arrb arb_pr">
		<span class="p_a_t">昵&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称：</span>
		 <input id="iNickName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		<span class="info1" id="nnmsg">登陆时的显示名，将会出现在资源发布的编辑者</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">登陆密码：</span>
	  <input id="iNewPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" />
		<span id="npwdmsg" class="info1">登陆密码,不修改请留空！</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">确认密码：</span>
	 <input id="iCPWD" type="password" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckCPWD(this);"/>
		<span id="cpwdmsg" class="info1">确认您的新密码！</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">所属角色：</span>
	 <select id="sRole" runat="server" onchange="CheckRole();">
	 	<option value="0" selected="selected">请选择角色</option>
	 </select>
		<span id="rolemsg" class="info1">请给管理员分配角色！</span>
	</div>
	<div class="Page_arrb arb_pr"><span class="p_a_t">是否锁定：</span>
	  <input type="radio" id="iLockY" name="iLock" value="Y"  runat="server"/> 锁定
	  <input type="radio" id="iLockN" name="iLock" value="N"  runat="server"/> 未锁
	  <span id="lockmsg" class="info1">请设置管理员状态！</span>	</div>
	<div class="Page_arrb arb_pr poph nfl">
		<span class="p_a_t lfl"><p class="green bold">可分配的功能权限</p>
			<select size="12" multiple="true" id="vcPopedom"  class="popselect" onchange="SetPopValue('popedom',this);">
			</select>
			<input type="hidden" id="popedom" name="popedom" runat="server"/>
			<script type="text/javascript">PopSelectInit();</script>
		</span>
		<span class="p_a_t lfl popml"><p class="green bold">可分配的文章分类权限</p>
			<select size="12" multiple="true" id="vcClassPopedom" class="popselect" onchange="SetPopValue('classpopedom',this);">
			</select>
			<input type="hidden" id="classpopedom" name="classpopedom" runat="server"/>
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
    </form>
</body>
</html>