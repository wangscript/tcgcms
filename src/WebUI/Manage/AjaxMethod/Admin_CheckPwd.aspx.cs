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
using TCG.Entity;


public partial class AjaxMethod_Admin_CheckPwd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string pwd = objectHandlers.Get("PWD", CheckGetEnum.Safety);

            if (base.adminInfo == null)
            {
                base.AjaxErch(-1000000601,"");
                base.Finish();
                return;
            }
            pwd = objectHandlers.MD5(pwd);
            if (pwd.ToLower() == base.adminInfo.vcPassword.ToLower())
            {
                base.AjaxErch(1,"true");
                base.Finish();
                return;
            }
            base.AjaxErch(-1000000602,"");
            base.Finish();
        }
    }
}