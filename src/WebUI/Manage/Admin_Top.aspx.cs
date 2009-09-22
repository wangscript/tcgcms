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
using TCG.Manage.Utils;

public partial class Admin_Top : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            string admins = Fetch.Post("admins");
            
            if (string.IsNullOrEmpty(admins))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.admin.AdminHandlers.DelAdmins(base.admin.adminInfo.vcAdminName, admins, "01");
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }
}