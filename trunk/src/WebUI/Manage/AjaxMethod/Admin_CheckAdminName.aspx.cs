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
using TCG.Pages;
using TCG.Entity;



public partial class AjaxMethod_Admin_CheckAdminName : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string adminname = objectHandlers.Get("admin", CheckGetEnum.Safety);
            int rtn = base.handlerService.adminHandlers.CheckAdminNameForReg(adminname);
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }
}
