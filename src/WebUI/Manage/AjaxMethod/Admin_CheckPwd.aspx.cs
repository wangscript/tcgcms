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
using TCG.Manage.Utils;

public partial class AjaxMethod_Admin_CheckPwd : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string pwd = Fetch.Get("PWD", CheckGetEnum.Safety);
            object TempAdmin = SessionState.Get(ManageConst.AdminSessionName);
            if (TempAdmin == null)
            {
                base.AjaxErch("false");
                base.Finish();
                return;
            }
            pwd = Text.MD5(pwd);
            if (pwd.ToLower() == ((Admin)TempAdmin).vcPassword.ToLower())
            {
                base.AjaxErch("true");
                base.Finish();
                return;
            }
            base.AjaxErch("false");
            base.Finish();
        }
    }
}
