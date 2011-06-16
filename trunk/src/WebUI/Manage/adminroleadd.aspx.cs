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

using TCG.Handlers;

namespace TCG.CMS.WebUi
{
    public partial class adminroleadd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(10);

            if (!Page.IsPostBack)
            {
                this.DefaultSkinId.Value = ConfigServiceEx.DefaultSkinId;
            }
            else
            {
                string vcRoleName = objectHandlers.Post("vcRoleName");
                string vcContent = objectHandlers.Post("vcContent");
                string popedom = objectHandlers.Post("popedom");
                string classpopedom = objectHandlers.Post("classpopedom");
                if (string.IsNullOrEmpty(vcRoleName))
                {
                    base.AjaxErch("{state:false,message:'权限组名称不能为空！'}");
                    return;
                }
                int rtn;
                try
                {
                    rtn = base.handlerService.manageService.adminHandlers.AddAdminRole(base.adminInfo, vcRoleName, popedom, classpopedom, vcContent);
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
                    base.AjaxErch("{state:true,message:'成功添加新的角色组！'}");
                }
            }
        }
    }
}