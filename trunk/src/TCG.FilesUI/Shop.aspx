<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Shop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>遍历文件夹下所有图片</title>
    <style type="text/css">
    .Folders{ float:left; width:100%;}
    .Folders a{ margin-left:12px; float:left; height:40px; line-height:40px;}
    .ImageDiv{float:left;}
    .ImageDiv img { width:200px;}
    .ImageDiv .Item{ width:230px; float:left; margin-bottom:50px;}
    .ImageDiv .Item span{ width:80%; float:left; height:40px; line-height:40px; font-size:12px; text-align:center; font-weight:bold; overflow:hidden;}
    </style>
    
    <script type="text/javascript">
    function copy(obj){
        var rng = document.body.createTextRange(); 
        rng.moveToElementText(obj); 
        rng.scrollIntoView(); 
        rng.select(); 
        rng.execCommand("Copy"); 
        rng.collapse(false);
        alert("复制成功!"); 

    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Folders">
        <asp:Literal ID="ltFileNames" runat="server"></asp:Literal>
    </div>
    <div class="ImageDiv">
        <asp:Repeater id="ItemRepeater" runat="server" onitemdatabound="ItemRepeater_ItemDataBound" EnableViewState="False">
		<ItemTemplate>
		    <div class="Item">
                <a href="<asp:Literal ID='Literal1' runat='server'></asp:Literal>" target="_blank"><asp:image ID="Image1" runat="server" /></a>
                <span onclick="copy(this)"><asp:Literal ID="ltImagePath" runat="server"></asp:Literal></span>
            </div>
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>