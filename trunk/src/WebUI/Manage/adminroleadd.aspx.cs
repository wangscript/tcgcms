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
using TCG.Handlers;

public partial class adminroleadd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

            this.DefaultSkinId.Value = base.configService.DefaultSkinId;
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
                rtn = base.handlerService.manageService.adminHandlers.AddAdminRole(base.adminInfo.vcAdminName, vcRoleName, popedom, classpopedom, vcContent);
            }
            catch (Exception ex)
            {
                base.AjaxErch("{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}");
                return;
            }

            if (rtn < 0)
            {
                base.AjaxErch("{state:false,message:'" + errHandlers.GetErrTextByErrCode(rtn, base.configService.baseConfig["ManagePath"]) + "'}");
            }
            else
            {
                base.AjaxErch("{state:true,message:'成功添加新的角色组！'}");
            }
        }
    }
}
