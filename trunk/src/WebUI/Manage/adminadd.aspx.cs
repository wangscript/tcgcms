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


public partial class adminadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.iRoleInit();
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
            pwd = objectHandlers.MD5(pwd);
            int rtn = base.handlerService.manageService.adminHandlers.AddAdmin(base.adminInfo.vcAdminName, vcAdminName, vcNickname, pwd, iRole, cLock,
                popedom, classpopedom);
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }

    private void iRoleInit()
    {   
        DataSet ds = new DataSet();
        int t = 0;
        int p = 0;
        int rtn = base.handlerService.manageService.adminHandlers.GetAdminRoleInfo(ref t,ref p, ref ds);
        if (rtn < 0)
        {
            base.Finish();
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
            this.sRole.Items.Add(new ListItem(Row["vcRoleName"].ToString(),Row["iID"].ToString()));
        }

        ds.Dispose();
        ds.Clear();
        base.Finish();
    }
}
