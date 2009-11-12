﻿using System;
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
using TCG.Entity;

public partial class adminrolemdy : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminLoginHandlers.CheckAdminLogin();

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
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.handlerService.manageService.adminHandlers.MdyAdminRole(base.adminInfo.vcAdminName, vcRoleName, popedom, classpopedom, vcContent,iRoleId);
            base.AjaxErch(rtn.ToString());
            base.Finish();
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
        this.DefaultSkinId.Value = base.configService.DefaultSkinId;
    }
}
