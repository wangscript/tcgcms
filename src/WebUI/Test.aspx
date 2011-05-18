<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<script type="text/javascript" charset="gb2312" src="manage/KindEditer/kindeditor-min.js"></script>
    <script type="text/javascript">
        KE.show({
            id: 'content1',
            imageUploadJson: '../../asp.net/upload_json.ashx',
            fileManagerJson: '../../asp.net/file_manager_json.ashx',
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
<textarea id="content1" cols="100" rows="8" style="width:700px;height:200px;visibility:hidden;" runat="server"></textarea>