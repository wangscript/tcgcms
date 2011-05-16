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
using TCG.Entity;

public partial class adminrolemdy : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
            this.Init();
        }
        else
        {
            string vcRoleName = objectHandlers.Post("vcRoleName");
            string vcContent = objectHandlers.Post("vcContent");
            string popedom = objectHandlers.Post("popedom");
            string classpopedom = objectHandlers.Post("classpopedom");
            int iRoleId = objectHandlers.ToInt(objectHandlers.Get("roleid", CheckGetEnum.Int));
            if (string.IsNullOrEmpty(vcRoleName))
            {
                base.AjaxErch("{state:false,message:'权限组名称不能为空！'}");
                
                return;
            }
            int rtn;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.MdyAdminRole(base.adminInfo.vcAdminName, vcRoleName, popedom, classpopedom, vcContent, iRoleId);
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
                base.AjaxErch("{state:true,message:'您已经成功完成对角色组的操作！'}");
            }
            return;
        }
    }

    private void Init()
    {
        int iRoleId = objectHandlers.ToInt(objectHandlers.Get("roleid", CheckGetEnum.Int));
        if (iRoleId == 0) return;
        AdminRole role = base.handlerService.manageService.adminHandlers.GetAdminRoleInfoByRoleId(iRoleId);
        if (role != null)
        {
            this.vcRoleName.Value = role.vcRoleName;
            this.vcContent.Value = role.vcContent;
            this.popedom.Value = role.PopedomStr;
            this.classpopedom.Value = role.vcClassPopedom;
        }
        this.DefaultSkinId.Value = ConfigServiceEx.DefaultSkinId;
    }
}
