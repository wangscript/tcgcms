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



public partial class Admin_Top : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();
        base.handlerService.manageService.adminHandlers.CheckAdminPop(14);

        if (Page.IsPostBack)
        {
            string admins = objectHandlers.Post("admins");
            
            if (string.IsNullOrEmpty(admins))
            {
                base.AjaxErch(-1,"");
                return;
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.DelAdmins(base.adminInfo.vcAdminName, admins, "01");
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }
            base.AjaxErch(rtn, "删除管理员[" + admins + "]成功", "refash()");
        }
    }
}