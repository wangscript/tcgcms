using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Pages;
using TCG.Manage.Utils;

public partial class MyAccount : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.iNickName.Value = base.admin.adminInfo.vcNickName;
            this.adminname.Value = base.admin.adminInfo.vcAdminName;
        }
        else
        {
            string adminname = Fetch.Post("adminname");
            string nickname = Fetch.Post("iNickName");
            string oldpwd = Fetch.Post("iOldPWD");  
            string npwd = Fetch.Post("iNewPWD");

            if (string.IsNullOrEmpty(adminname) || string.IsNullOrEmpty(nickname))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            oldpwd = Text.MD5(oldpwd);
            if (!string.IsNullOrEmpty(npwd)) npwd = Text.MD5(npwd);

            int rtn = base.admin.AdminHandlers.ChanageAdminLoginInfo(adminname, oldpwd, npwd, nickname);
            if (rtn == 1)
            {
                SessionState.Remove(ManageConst.AdminSessionName);
            }
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }
}