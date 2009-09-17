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
using TCG.Manage.Kernel;
using TCG.Manage.Utils;

public partial class adminadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.iRoleInit();
        }
        else
        {
            string vcAdminName = Fetch.Post("vcAdminName");
            string vcNickname = Fetch.Post("iNickName");
            int iRole = Bases.ToInt(Fetch.Post("sRole"));
            string cLock = Fetch.Post("iLock");
            string popedom = Fetch.Post("popedom");
            string classpopedom = Fetch.Post("classpopedom");
            string pwd = Fetch.Post("iNewPWD");

            if (string.IsNullOrEmpty(vcAdminName) || string.IsNullOrEmpty(vcNickname))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }
            pwd = Text.MD5(pwd);
            int rtn = base.admin.AdminHandlers.AddAdmin(base.admin.adminInfo.vcAdminName, vcAdminName, vcNickname, pwd, iRole, cLock,
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
        int rtn = base.admin.AdminHandlers.GetAdminRoleInfo(ref t,ref p, ref ds);
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
