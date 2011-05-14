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

using TCG.Entity;


public partial class AjaxMethod_Admin_DelAdminRole : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int iRole = objectHandlers.ToInt(objectHandlers.Post("iRole"));
            if (iRole == 0)
            {
                base.AjaxErch(-1,"");
                return;
            }
            int rtn = base.handlerService.manageService.adminHandlers.DelAdminRole(base.adminInfo.vcAdminName, iRole);
            base.AjaxErch(rtn.ToString());
        }
    }
}
