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

public partial class adminrolemdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Init();
        }
        else
        {
            string vcRoleName = Fetch.Post("vcRoleName");
            string vcContent = Fetch.Post("vcContent");
            string popedom = Fetch.Post("popedom");
            string classpopedom = Fetch.Post("classpopedom");
            int iRoleId = Bases.ToInt(Fetch.Get("roleid", CheckGetEnum.Int));
            if (string.IsNullOrEmpty(vcRoleName))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.admin.AdminHandlers.MdyAdminRole(base.admin.adminInfo.vcAdminName, vcRoleName, popedom, classpopedom, vcContent,iRoleId);
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }

    private void Init()
    {
        int iRoleId = Bases.ToInt(Fetch.Get("roleid", CheckGetEnum.Int));
        if (iRoleId == 0) return;
        DataTable dt = base.admin.AdminHandlers.GetAdminRoleInfoByRoleId(iRoleId);
        if (dt == null) { dt.Dispose(); dt.Clear(); }
        if (dt.Rows.Count == 1)
        {
            this.vcRoleName.Value = dt.Rows[0]["vcRoleName"].ToString();
            this.vcContent.Value = dt.Rows[0]["vcContent"].ToString();
            this.popedom.Value = dt.Rows[0]["vcPopedom"].ToString();
            this.classpopedom.Value = dt.Rows[0]["vcClassPopedom"].ToString();
        }
    }
}
