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

using TCG.Entity;


namespace TCG.CMS.WebUi
{
    public partial class AjaxMethod_Admin_CheckPwd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string pwd = objectHandlers.Get("PWD", CheckGetEnum.Safety);

                if (base.adminInfo == null)
                {
                    base.AjaxErch(-1000000601, "");
                    return;
                }
                pwd = objectHandlers.MD5(pwd);
                if (pwd.ToLower() == base.adminInfo.vcPassword.ToLower())
                {
                    base.AjaxErch(1, "原始密码输入正确！");
                    return;
                }
                base.AjaxErch(-1000000602, "");
            }
        }
    }
}