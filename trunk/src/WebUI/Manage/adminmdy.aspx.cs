using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Controls.HtmlControls;


using TCG.Entity;

public partial class adminmdy : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();
        base.handlerService.manageService.adminHandlers.CheckAdminPop(12);

        if (!Page.IsPostBack)
        {
            this.DefaultSkinId.Value = ConfigServiceEx.DefaultSkinId;

            this.AdminInfoInit();
        }
        else
        {
            string vcAdminName = objectHandlers.Post("adminname");
            string vcNickname = objectHandlers.Post("iNickName");
            int iRole = objectHandlers.ToInt(objectHandlers.Post("sRole"));
            string cLock = objectHandlers.Post("iLock");
            string popedom = objectHandlers.Post("popedom");
            string classpopedom = objectHandlers.Post("classpopedom");
            string pwd = objectHandlers.Post("iNewPWD");

            if (string.IsNullOrEmpty(vcAdminName) || string.IsNullOrEmpty(vcNickname))
            {
                base.AjaxErch("{state:false,message:'用户名和昵称不能为空!'}");
                return;
            }
            if (!string.IsNullOrEmpty(pwd)) pwd = objectHandlers.MD5(pwd);
            int rtn = 0;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.UpdateAdminInfo(base.adminInfo.vcAdminName, vcAdminName, vcNickname, pwd, iRole, cLock,
                    popedom, classpopedom);

            }
            catch (Exception ex)
            {
                base.AjaxErch("{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}");
                return;
            }

            base.AjaxErch(rtn,"管理员信息修改成功!");
        }
    }

    private void AdminInfoInit()
    {
        string adminname = objectHandlers.Get("adminname", CheckGetEnum.Safety);
        this.vcAdminName.Value = adminname;
        this.adminname.Value = adminname;
        Admin admin = base.handlerService.manageService.adminHandlers.GetAdminEntityByAdminName(adminname);
        if (admin == null) return;

        this.popedom.Value = admin.PopedomStr;
        this.classpopedom.Value = admin.vcClassPopedom.ToString();
        this.iNickName.Value = admin.vcNickName.ToString();
        this.iRoleInit(admin.iRole.iID.ToString());
        if (admin.cLock.ToString() == "Y") { this.iLockY.Checked = true; } else { this.iLockN.Checked = true; }

    }

    private void iRoleInit(string s)
    {

        Dictionary<int, AdminRole> allrole = base.handlerService.manageService.adminHandlers.GetAllAdminRoleEntity();

        foreach (KeyValuePair<int, AdminRole> keyvalue in allrole)
        {
            this.sRole.Items.Add(new ListItem(keyvalue.Value.vcRoleName, keyvalue.Value.iID.ToString()));
        }
        this.sRole.Value = s;
    }
}
