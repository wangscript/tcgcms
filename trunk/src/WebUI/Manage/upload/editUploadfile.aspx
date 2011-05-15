<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editUploadfile.aspx.cs" Inherits="Manage_upload_editUploadfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
<title></title>
<link href="../css/base.css" rel="stylesheet" type="text/css" />
<link href="../css/adminlist.css" rel="stylesheet" type="text/css" />
<link href="../css/admininfo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.1.3.2.js"></script>
<script type="text/javascript" src="../js/jquery.form.js"></script>

<script type="text/javascript">
    function OnSelFilePath() {
        $("#form1").submit();
    }

    $(document).ready(function() {
        //����ύ����
        var options = {
            beforeSubmit: PostFile,
            dataType: 'json',
            success: uploadimageback
        };
        $("#form1").ajaxForm(options);
    });

    function PostFile() {
        return true;
    }
    var vdata = null;
    function uploadimageback(data) {
        vdata = data;
        if (data.Url) {
            $("#FileView").html("<img src=\"" + data.Url + "\" width=\"100%\" height=\"100%\"/>");
        } else {
            $("#FileView").html("<br/><br/><br/><br/>" + data.Err);
        }
    }
    
</script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <table width='100%' border="0" cellspacing="1" style="background:#9CD2FD" cellpadding="0">
        <tr style='background:#EDF6FD'>
            <td style='height:25px;width:60%;text-align:left;padding-left:5px;font-weight:bold;background:#9CD2FD;color:#ffffff;letter-spacing:2px'>�ϴ��ļ�</td>
		    <td width='40%' align="center" rowspan="3">
		        <div id='FileView' style='width:192px;height:200px;overflow:auto;text-align:center'>
		            <br/><br/><br/><br/>Ԥ �� ��</div>
		    </td>
		</tr>
		<tr style='background:#ffffff'>
		    <td style="height:150px;text-align:left;padding-left:15px;color:#666666" valign="top">
		        <br/>
		        <div id='FileText'>��ѡ����Ҫ�ϴ����ļ�</div>
	            <br /><input type="file" id="FileUrl" name="FileUrl" style="width:268px;" onchange="OnSelFilePath()" />
	            <br/><br/><b>���뷽ʽ</b>:<br/>
	            <input type='radio' onclick="FileAlign=null" name="FA" checked="checked" />�ޡ�
	            <input name='FA' type='radio' onclick="FileAlign='left'" />����롡
	            <input type='radio' name='FA' onclick="FileAlign='center'" />���ж��롡
	            <input name='FA' type='radio' onclick="FileAlign='right'"/>�Ҷ���
		     </td>
		 </tr>
		<tr style='background:#ffffff'>
		    <td style='height:30px'>
		        <input value='ȷ��' onclick="parent.InfoFile(vdata);" type="button" class="btn2 bold" />�� 
		     </td>
		 </tr>
		</table>
    </form>
</body>
</html>