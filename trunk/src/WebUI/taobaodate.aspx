<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taobaodate.aspx.cs" Inherits="TCG.CMS.WebUi.taobaodate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>淘宝数据导入工具</title>
    <script type="text/javascript" src="manage/js/commonV2.js"></script>
	<script type="text/javascript" src="manage/Common/common.aspx"></script>
	<script type="text/javascript" src="manage/js/jquery.1.3.2.js"></script>
	<script type="text/javascript" src="manage/js/jquery.form.js"></script>
    <style type="text/css">
    body{ font-size:12px; text-align:center;}
    .table td{ width:20px; overflow:hidden;}
    </style>
</head>
<script type="text/javascript">
    $(document).ready(function () {
        var form1 = $("#form1");
        var options;
        options = {
            beforeSubmit: CheckPost,
            dataType: 'json',
            success: PostFormBack
        };
        form1.ajaxForm(options);
    });


    function CheckPost() {

    }

    function PostFormBack() {

    }

</script>
<body>
    <form id="form1" runat="server">
    <div id="dataveiw" runat = "server"></div>
    <input type="hidden" id="work" runat="server" />
    </form>
</body>
</html>
