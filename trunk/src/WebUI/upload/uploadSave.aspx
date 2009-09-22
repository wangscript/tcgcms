<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadSave.aspx.cs" Inherits="upload_uploadSave" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>文件上传</title>
	<link href="../css/filesinfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" language="javascript">
		function $(element) { 
			var elements = new Array(); 
			for (var i = 0;i<arguments.length;i++){ 
				var element = arguments[i]; 
				if (typeof element == 'string') 
					element = document.getElementById(element); 
				if (arguments.length == 1) 
					return element; 
				elements.push(element); 
			} 
			return elements; 
		}
		
		function OnSelFilePath(obj){
			window.parent.SeBack(obj.value);
		}
		
		function Save(){
			$("form1").submit();
		}
	</script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
		<input type="file" id="ifile" name="ifile" onchange="OnSelFilePath(this)" />
		<input type="hidden" id="filenum" name="filenum" />
		<input type="hidden" id="iId" name="iId"  value="0" runat="server"/>
		<input type="hidden" id="iClassId" name="iClassId"  value="0" runat="server"/>
 	</form>
</body>
</html>