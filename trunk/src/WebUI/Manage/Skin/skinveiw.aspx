<%@ Page Language="C#" AutoEventWireup="true" CodeFile="skinveiw.aspx.cs" Inherits="TCG.CMS.WebUi.Manage_Skin_skinveiw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<title></title>

<link href="../css/base.css" rel="stylesheet" type="text/css" />
<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />

<link href="../css/jquery.lightTreeview.css" rel="stylesheet" type="text/css" />
<link href="../css/main.css" rel="stylesheet" type="text/css" />
<link href="../css/contextmenu.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/commonV2.js"></script>
<script type="text/javascript" src="../Common/common.aspx"></script>
<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
<script type="text/javascript" src="../js/jquery.form.js"></script>
<script src="../js/jquery.contextmenu.js" type="text/javascript"></script>
<script language="JavaScript" src="../js/jmessagebox-1.0.0.js" charset="utf-8"></script>
<link rel="stylesheet" type="text/css" href="../images/messagebox/blue/messagebox.css" />

<style type="text/css">
#divCategories
{
	overflow:auto;
	width:200px; height:500px;
	border:blue solid 1px;
}
</style>

<script type="text/javascript">


    /** 示例1 **/
    function test1() {
        jQuery.jMessageBox.show({
            width: 600,
            title: '系统消息',
            message: $("#divCategoriesManage").html(),
            yesButton: {
                text: '确定',
                click: function() {
                    jQuery.jMessageBox.hide();
                }
            },
            cancelButton: {
                text: '取消',
                click: function() {
                    jQuery.jMessageBox.hide();
                }
            },
            bottom: {
                text: '说明: 如果你想继续操作,请点击"确定"!',
                click: function() { }
            }
        });
    }
    
 $().ready(function() {
        var option = { width: 150, items: [
                        { text: "添加分类", icon: "../images/icons/chart_organisation_add.png", alias: "1-1", action: test1 }], onShow: applyrule,
            onContextMenu: BeforeContextMenu
        };
        function menuAction() {
            alert(this.data.alias);
        }
        function applyrule(menu) {               
            if (this.id == "target2") {
                menu.applyrule({ name: "target2",
                    disable: true,
                    items: ["1-2", "2-3", "2-4", "1-6"]
                });
            }
            else {
                menu.applyrule({ name: "all",
                    disable: true,
                    items: []
                });
            }
        }
        function BeforeContextMenu() {
            return this.id != "target3";
        }
        $("#divCategories").contextmenu(option);
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divCategories" class="target">
        <h1>分类信息</h1>
        <ul id="ulCategories" class="lightTreeview"></ul>
        
    </div>
    <div id="divCategoriesManage" style="display:none;">
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
	      <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iUrl','urlmsg');"/>
		    <span id="urlmsg" class="info1">分类在网站前台的首页地址，用于生成导航</span>
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
        <input type="hidden" id="work" name="work"/>
    </form>
</body>
</html>
