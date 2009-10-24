using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using TCG.Pages;
using TCG.Utils;

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
        }

    }

    /// <summary>
    /// 用户注册
    /// </summary>
    private void UserRegister()
    {

    }
}