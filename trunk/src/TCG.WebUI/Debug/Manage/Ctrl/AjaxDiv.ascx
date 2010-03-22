<%@ control language="C#" autoeventwireup="true" inherits="Ctrl_AjaxDiv, TCG.WebUI" %>
<div class="ajaxdiv hid" id="ajaxdiv">
		<img src="../images/ajax-loader1.gif" id="ajaximg" /><span id="ajaxText"> 正在发送请求...</span><a href="javascript:GoTo();" class="ajaxclose" onclick="ajaxClose();" title="关闭"></a>
</div>