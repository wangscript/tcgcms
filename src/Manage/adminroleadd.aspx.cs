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
using TCG.Manage.Kernel;
using TCG.News.Handlers;

public partial class adminroleadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        else
        {
            string vcRoleName = Fetch.Post("vcRoleName");
            string vcContent = Fetch.Post("vcContent");
            string popedom = Fetch.Post("popedom");
            string classpopedom = Fetch.Post("classpopedom");
            if (string.IsNullOrEmpty(vcRoleName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.admin.AdminHandlers.AddAdminRole(base.admin.adminInfo.vcAdminName, vcRoleName, popedom, classpopedom, vcContent);
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }
}
