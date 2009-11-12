using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;

using TCG.Entity;

public partial class adminmdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.AdminInfoInit();
            base.Finish();
        }
        else
        {
            string vcAdminName = objectHandlers.Post("vcAdminName");
            string vcNickname = objectHandlers.Post("iNickName");
            int iRole = objectHandlers.ToInt(objectHandlers.Post("sRole"));
            string cLock = objectHandlers.Post("iLock");
            string popedom = objectHandlers.Post("popedom");
            string classpopedom = objectHandlers.Post("classpopedom");
            string pwd = objectHandlers.Post("iNewPWD");

            if (string.IsNullOrEmpty(vcAdminName) || string.IsNullOrEmpty(vcNickname))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }
            if (!string.IsNullOrEmpty(pwd)) pwd = objectHandlers.MD5(pwd);
            int rtn = base.handlerService.manageService.adminHandlers.UpdateAdminInfo(base.adminInfo.vcAdminName, vcAdminName, vcNickname, pwd, iRole, cLock,
                popedom, classpopedom);
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }

    private void AdminInfoInit()
    {
        string adminname = objectHandlers.Get("adminname", CheckGetEnum.Safety);
        this.vcAdminName.Value = adminname;
        Admin admin = base.handlerService.manageService.adminHandlers.GetAdminEntityByAdminName(adminname);
        if (admin == null) { Response.End(); return; }

        this.popedom.Value = admin.vcPopedom.ToString();
        this.classpopedom.Value = admin.vcClassPopedom.ToString();
        this.iNickName.Value = admin.vcNickName.ToString();
        this.iRoleInit(admin.iRole.ToString());
        if (admin.cLock.ToString() == "Y") { this.iLockY.Checked = true; } else { this.iLockN.Checked = true; }

    }

    private void iRoleInit(string s)
    {
        DataSet ds = new DataSet();
        int t = 0;
        int p = 0;
        int rtn = base.handlerService.manageService.adminHandlers.GetAdminRoleInfo(ref t,ref p, ref ds);
        if (rtn < 0)
        {
            ds.Dispose();
            ds.Clear();
            return;
        }

        if (ds.Tables.Count == 0)
        {
            ds.Dispose();
            ds.Clear();
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow Row = ds.Tables[0].Rows[i];
            this.sRole.Items.Add(new ListItem(Row["vcRoleName"].ToString(), Row["iID"].ToString()));
            if (s == Row["iID"].ToString()) this.sRole.SelectedIndex = i+1;
        }

        ds.Dispose();
        ds.Clear();
    }
}
