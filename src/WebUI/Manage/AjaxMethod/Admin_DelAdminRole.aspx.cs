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

namespace TCG.CMS.WebUi
{

    public partial class AjaxMethod_Admin_DelAdminRole : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!base.handlerService.manageService.adminHandlers.CheckAdminPopEx(7))
                {
                    base.AjaxErch(-2, "");
                    return;
                }

                int iRole = objectHandlers.ToInt(objectHandlers.Get("iRole"));
                if (iRole == 0)
                {
                    base.AjaxErch(-1, "");
                    return;
                }
                int rtn = 0;

                try
                {
                    rtn = base.handlerService.manageService.adminHandlers.DelAdminRole(base.adminInfo, iRole);
                }
                catch (Exception ex)
                {
                    base.AjaxErch("{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}");
                    return;
                }

                if (rtn < 0)
                {
                    base.AjaxErch("{state:false,message:'" + errHandlers.GetErrTextByErrCode(rtn, ConfigServiceEx.baseConfig["ManagePath"]) + "'}");
                }
                else
                {
                    if (rtn == 1)
                    {
                        CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
                    }
                    base.AjaxErch(rtn, "删除角色组成功", "refash()");
                }



            }
        }
    }
}
