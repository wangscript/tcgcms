//--------------


function CheckForm() {
    var pwd = $("#iNewPWD");
    var cpwd = $("#iCPWD");
    var o = $("#iLockY");
    var p = $("#iLockN");
    var lockmsg = $("#lockmsg");
    if (p.attr("checked") == false && o.attr("checked") == false) {
        lockmsg[0].className = "info_err";
        lockmsg.text("��δ�趨����Ա��״̬");
    } else {
        lockmsg[0].className = "info_ok";
        lockmsg.text("�ɹ����ù���Ա״̬!");
    }

    var nnmsg = $("#nnmsg");
    var iNickName = $("#iNickName");
    if (iNickName.val() == "") {
        nnmsg[0].className = "info_err";
        nnmsg.text("�ǳƲ���Ϊ��!");
    }

    if (!(CheckAdminName() && CheckRole() && CheckCPWD(cpwd))) return false;
    return true;
}


$(document).ready(function () {
    //����ύ����
    var options = {
        beforeSubmit: CheckForm,
        dataType: 'json',
        success: AddadminPostBack
    };
    $("#form1").ajaxForm(options);

});


function AddadminPostBack(data) {
    if (data.state) {
        window.parent.adminpop.refinsh();
        SetAjaxDivRoot("ok", false, data.message);
    } else {
        SetAjaxDivRoot("err", false, data.message);
    }
}

function CheckAdminName() {
    var adminname = $("#vcAdminName");
    if (!adminname.attr("disabled")) {
        adminname[0].className = "itxt1";
        var adminmsg = $("#adminmsg");
        if (adminname.length == 0) return;
        if (adminname.val() == "" || (adminname.val().length < 3 || adminname.val() > 18)) {
            adminmsg[0].className = "info_err";
            adminmsg.text("��½������Ϊ�ջ򳤶Ȳ���3-18���ַ�֮��!");
            adminmsg.focus();
            return false;
        }

        $.ajax({
            type: "GET", url: "AjaxMethod/Admin_CheckAdminName.aspx?temp=" + new Date().toString(), data: "admin=" + adminname.val(),
            errror: function () { },
            success: function (data) {

                if (data.state) {
                    SetAjaxDivRoot("ok", false, data.message);
                } else {
                    SetAjaxDivRoot("err", false, data.message);
                }
            },
            dataType: "json"
        });
    }
    return true;
}


function CheckRole() {
    var sRole = $("#sRole");
    var rolemsg = $("#rolemsg");
    if (sRole.val() == "0") {
        rolemsg[0].className = "info_err";
        rolemsg.text("��δ������Ա�����ɫ!");
        return false;
    }
    rolemsg[0].className = "info_ok";
    rolemsg.text("����Ա��ɫ����ɹ�!");
    return true;
}