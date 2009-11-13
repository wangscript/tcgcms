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
            int rtn = 0;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.CheckAdminNameForReg(adminname);
            }
            catch (Exception ex)
            {
                base.AjaxErch("{state:false,message:'" + objectHandlers.JSEncode(ex.Message.ToString()) + "'}");
                return;
            }

            if (rtn == 0)
            {
                base.AjaxErch("{state:true,message:'该用户名可以使用!'}");
            }
            else
            {
                base.AjaxErch("{state:false,message:'该用户名已经被其他人使用!'}");
            }
        }
    }
}
