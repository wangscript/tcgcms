using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TCG.Pages;
using TCG.Utils;

public partial class interface_userget : Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string action = objectHandlers.Get("action");
            switch (action)
            {
                case "CHECK_USER_NAME":
                    this.CheckUserName();
                    break;
                case "CHECK_USER_VALIDATECODE":
                    this.CheckUserValidateCode();
                    break;
            }
        }
    }

    /// <summary>
    /// 检测验证码是否正确
    /// </summary>
    private void CheckUserValidateCode()
    {
        string code = objectHandlers.Get("code");
        if (objectHandlers.CheckValiCode(code))
        {
            base.ajaxdata = "{state:true,message:''}";
        }
        else
        {
            base.ajaxdata = "{state:false,message:''}";
        }
        base.AjaxErch(ajaxdata);
    }

    /// <summary>
    /// 检测用户名是否存在
    /// </summary>
    public void CheckUserName()
    {
        string name = objectHandlers.Get("name");
        if (string.IsNullOrEmpty(name))
        {
            base.ajaxdata = "{state:false,message:'没有收到您发送的用户名,请联系系统管理员！'}";
            base.AjaxErch(ajaxdata);
            return;
        }

        try
        {
            if (base.handlerService.userService.userHandlers.CheckUserName(name))
            {
                base.ajaxdata = "{state:true,message:''}";
            }
            else
            {
                base.ajaxdata = "{state:false,message:''}";
            }
        }
        catch(Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + ex.Message.ToString() + "\"}";
            base.AjaxErch(ajaxdata);
            return;
        }

        base.AjaxErch(ajaxdata);
    }
}
