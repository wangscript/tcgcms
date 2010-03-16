<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Editor.ascx.cs" Inherits="Manage_Ctrl_Editor" %>
<div id="ContentWin" style="width:800px;height:400px">
<iframe src="../QzoneEditor/edit.html?I=<% =content.ClientID %>&B=<% =ContentBg.ClientID %>" 
    frameBorder="0" 
    marginHeight="0" 
    marginWidth="0" 
    scrolling="No" 
    style="height:100%;width:100%" id="EditerFrame" name="EditerFrame">
 </iframe>
 </div>
<input type="text" value="" name="ContentBg" id="ContentBg" size=106 class="hid" runat="server"/><br>
<textarea name="content" id="content" style="width:800px;height:100px" class="hid"  runat="server"></textarea>
<script language="javascript" type="text/javascript">
function setContent(){
    var f = window.frames["EditerFrame"];
    var ff = f.document.frames["HtmlEditor"];
    var body = ff.document.getElementsByTagName("BODY")[0];
	document.getElementById("<%=content.ClientID %>").value = body.innerHTML;
}
</script>
<div id="layerbox6" class="layerbox">
    <div class="layer6">
        <div class="layer-c">
            <h3 class="title">
                <a href="javascript:CloseImageDive()"></a>上传身份证扫描件或者照片</h3>
            <div class="l-c">
                <p id="uploadimgErrmsg" class="tips_false" style="display: none">
                    <span></span>
                </p>
                <p class="p1">
                    请上传身份证正面扫描件照片</p>
                <p class="p2">
                    图片应小于120k jpg格式，建议为500x500像素 <a href="javascript:void 0">上传帮助</a></p>
                <p class="p3">
                    <input type="file" id="fileUpload" name="fileUpload" class="input1" /><input type="button"
                        value="上传" onclick="UploadImg()" class="input2" /></p>
            </div>
        </div>
    </div>
</div>
