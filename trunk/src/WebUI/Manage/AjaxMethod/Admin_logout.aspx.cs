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


public partial class AjaxMethod_Admin_logout : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                base.handlerService.manageService.adminHandlers.Logout();
                CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
            }
            catch(Exception ex)
            {
                base.AjaxErch("{state:false}");
                return;
            }

            base.AjaxErch("{state:true}");
        }
    }
}
