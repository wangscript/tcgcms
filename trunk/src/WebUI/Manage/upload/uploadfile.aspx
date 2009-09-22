<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadfile.aspx.cs" Inherits="upload_uploadfile" %>
<%@ Register tagPrefix="TCG" namespace="TCG.Controls.HtmlControls" assembly="TCG.Controls"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>文件上传</title>
	<link href="../css/filesinfo.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../js/common.js"></script>
	<script type="text/javascript" src="../js/AJAXRequest.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="uploadDiv_w">
		<div class="uploadDiv_Title">
			<a href ="javascript:GoTo();" class="startupbtn" onclick="StartUpload();"></a>
			<a href ="javascript:GoTo();" id="addfilebtn" class="addfilebtn" ></a>
			<a href ="javascript:GoTo();" class="alldelbtn" onclick="DelAllUpload();"></a>
			<a href ="javascript:GoTo();" class="cbtn" onclick="ClearUpdateFile();"></a>
		</div>
		<div class="uploaddiv_info" id="uploaddiv_info">
			<div class="uploaddiv_info_title" id="uploaddiv_info_title">请添加要上传的照片，点击开始上传</div>
			<div class="uploaddiv_info_title_emu emubg">
				<span class="emu_tittle">名称</span>
				<span class="emu_state">状态</span>
				<span class="emu_manage">操作</span>
			</div>
		</div>
	</div>
	<input type="hidden" id="filenum" name="filenum"  value="0"/>
	<input type="hidden" id="Cfilenum" name="Cfilenum"  value="0"/>
	<input type="hidden" id="iClassId" name="iClassId"  value="0" runat="server"/>
	<input type="file" id="tFiles" style="display:none;" onchange="SeBack()"/>
    </form>
	<script language="javascript" type="text/javascript">
		var FileCount=<TCG:Span id='sMaxNum' runat='server' />;
		var FileType=[<TCG:Span id='stype' runat='server' />];
		var CallBack="<TCG:Span id='sCallBack' runat='server' />";
	</script> 
	<script type="text/javascript" src="../js/uploadfile.js"></script>
	<div class="uploadfileFinsh hid" id="uploadfileFinsh">文件上传完成...</div>
</body>
</html>