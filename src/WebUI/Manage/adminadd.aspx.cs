using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Entity;

public partial class adminadd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();

            this.DefaultSkinId.Value = ConfigServiceEx.DefaultSkinId;

            this.iRoleInit();
        }
        else
        {
            string vcAdminName = objectHandlers.Post("vcAdminName");
            string vcNickname = objectHandlers.Post("iNickName");
            int iRole = objectHandlers.ToInt(objectHandlers.Post("sRole"));
            string cLock = objectHandlers.Post("iLock");
            string popedom = objectHandlers.Post("popedom");
            string classpopedom = objectHandlers.Post("classpopedom");
            string pwd = objectHandlers.Post("iNewPWD");

            if (string.IsNullOrEmpty(vcAdminName) || string.IsNullOrEmpty(vcNickname))
            {
                base.AjaxErch("{state:false,message:'用户名和昵称不能为空!'}");
                return;
            }
            pwd = objectHandlers.MD5(pwd);
            int rtn = 0;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.AddAdmin(base.adminInfo.vcAdminName, vcAdminName, vcNickname, pwd, iRole, cLock,
                     popedom, classpopedom);
            }
            catch (Exception ex)
            {
                base.AjaxErch("{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}");
                return;
            }

            try
            {
                CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
            }
            catch (Exception ex)
            {
            }

            base.AjaxErch(rtn,"成功添加新的管理员!");
        }
    }

    private void iRoleInit()
    {
        Dictionary<int, AdminRole> allrole = base.handlerService.manageService.adminHandlers.GetAllAdminRoleEntity();

        foreach (KeyValuePair<int, AdminRole> keyvalue in allrole)
        {
            this.sRole.Items.Add(new ListItem(keyvalue.Value.vcRoleName, keyvalue.Value.iID.ToString()));
        }
       
    }
}
