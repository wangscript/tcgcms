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


    /** ʾ��1 **/
    function test1() {
        jQuery.jMessageBox.show({
            width: 600,
            title: 'ϵͳ��Ϣ',
            message: $("#divCategoriesManage").html(),
            yesButton: {
                text: 'ȷ��',
                click: function() {
                    jQuery.jMessageBox.hide();
                }
            },
            cancelButton: {
                text: 'ȡ��',
                click: function() {
                    jQuery.jMessageBox.hide();
                }
            },
            bottom: {
                text: '˵��: ��������������,����"ȷ��"!',
                click: function() { }
            }
        });
    }
    
 $().ready(function() {
        var option = { width: 150, items: [
                        { text: "��ӷ���", icon: "../images/icons/chart_organisation_add.png", alias: "1-1", action: test1 }], onShow: applyrule,
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
        <h1>������Ϣ</h1>
        <ul id="ulCategories" class="lightTreeview"></ul>
        
    </div>
    <div id="divCategoriesManage" style="display:none;">
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� �ƣ�</span>
	      <input id="iClassName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iClassName','cnamemsg');"/>
		    <span id="cnamemsg" class="info1">�������ƣ�������д</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� ����</span>
	      <input id="iName" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iName','inamemsg');"/>
		    <span id="inamemsg" class="info1">�����������ʾ��ǰ̨������</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">ָ �� Ŀ ¼��</span>
	      <input id="iDirectory" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iDirectory','dirmsg');"/>
		    <span id="dirmsg" class="info1">��Թ�����վ�ĵ�Ŀ¼��������ɵľ�̬�ļ�</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">���� �׵�ַ��</span>
	      <input id="iUrl" type="text" runat="server" class="itxt1" onfocus="this.className='itxt2'" onblur="CheckValueIsNull('iUrl','urlmsg');"/>
		    <span id="urlmsg" class="info1">��������վǰ̨����ҳ��ַ���������ɵ���</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� ϸ ģ �壺</span>
	      <select id="sTemplate" runat="server"  onchange="CheckTemplate('sTemplate','stdmsg')">
	  	    <option value="-1">��ѡ��</option>
	      </select>
		    <span id="stdmsg" class="info1">������Ѷ��ϸҳģ��</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� ģ �壺</span>
	      <select id="slTemplate" runat="server" onchange="CheckTemplate('slTemplate','stsdmsg')">
	  	    <option value="-1">��ѡ��</option>
	      </select>
		    <span id="stsdmsg" class="info1">������Ѷ�б�ҳģ��</span>
	    </div>
	    <div class="Page_arrb arb_pr classaddline"><span class="p_a_t">�� �� �� ��</span>
	      <input id="iOrder" type="text" runat="server" value="0" class="itxt1" onfocus="this.className='itxt2'" onblur="this.className='itxt1'"/>
		    <span id="urlmsg" class="info1">���б��е���ʾ˳��</span>
	    </div>
        </div>
        <input type="hidden" id="work" name="work"/>
    </form>
</body>
</html>
