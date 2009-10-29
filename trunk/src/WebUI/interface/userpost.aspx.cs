using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Pages;
using TCG.Utils;
using TCG.Entity;

public partial class interface_userpost : Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = objectHandlers.Get("action");
        string name = objectHandlers.Post("email");

        switch (action)
        {
            case "USER_REGISTER":
                this.UserRegister();
                break;
            case "USER_LOGIN":
                this.UserLogin();
                break;
        }

    }


    private void UserLogin()
    {
        User user = new User();
        user.Name = objectHandlers.Post("UserName");
        user.PassWord = objectHandlers.MD5(objectHandlers.Post("UserPassWord"));

        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.PassWord))
        {
            base.ajaxdata = "{state:false,message:'用户名或密码为空！'}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        int rtn = 0;
        try
        {
            rtn = base.handlerService.userService.userLoginHandlers.UserLogin(ref user);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (rtn < 0)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(errHandlers.GetErrTextByErrCode(rtn, base.configService.baseConfig["ManagePath"])) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        //保存登陆信息
        try
        {
            base.handlerService.userService.userLoginHandlers.SetUserLoginCookie(user);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.ajaxdata = "{state:true,message:''}";
        base.AjaxErch(base.ajaxdata);

    }


    /// <summary>
    /// 用户注册
    /// </summary>
    private void UserRegister()
    {
        string email = objectHandlers.Post("Email");                //用户名
        string PassWord = objectHandlers.Post("PassWord");          //密码
        string RePassWord = objectHandlers.Post("RePassWord");      //重复密码
        string Validate_Code = objectHandlers.Post("Validate_Code"); //验证码

        if (string.IsNullOrEmpty(email))
        {
            base.ajaxdata = "{state:false,message:'用户名为空！'}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (!objectHandlers.IsMatch(email, @"([a-zA-Z0-9\@\.]{2,50})|[\u4e00-\u9fffa-zA-Z0-9\@\.]{2,25}"))
        {
            base.ajaxdata = "{state:false,message:'会员名只能由3到50个英文和数字或2到25个中文组成，不能含空格或特殊符号！！'}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (string.IsNullOrEmpty(PassWord))
        {
            base.ajaxdata = "{state:false,message:'密码为空！'}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (!objectHandlers.CheckValiCode(Validate_Code))
        {
            base.ajaxdata = "{state:false,message:'验证码不正确！'}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        User user = new User();
        user.Name = email;
        user.Id = base.handlerService.userService.userHandlers.CreateUserId();
        user.PassWord = objectHandlers.MD5(PassWord);
        user.LastLoginIp = objectHandlers.UserIp;

        if (objectHandlers.IsEmail(user.Name))
        {
            user.UserContact.Email = user.Name;
        }

        int rtn = 0;
        try
        {
            rtn = base.handlerService.userService.userHandlers.UserManage(user, "01");
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        if (rtn < 0)
        {
            base.ajaxdata = "{state:false,message:\""
                + objectHandlers.JSEncode(errHandlers.GetErrTextByErrCode(rtn, base.configService.baseConfig["ManagePath"])) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        //保存登陆信息
        try
        {
            base.handlerService.userService.userLoginHandlers.SetUserLoginCookie(user);
        }
        catch (Exception ex)
        {
            base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
            base.AjaxErch(base.ajaxdata);
            return;
        }

        base.ajaxdata = "{state:true,message:''}";
        base.AjaxErch(base.ajaxdata);
    }
}