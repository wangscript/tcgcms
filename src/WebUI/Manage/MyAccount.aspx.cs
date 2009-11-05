using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Pages;


public partial class MyAccount : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.iNickName.Value = base.adminInfo.vcNickName;
            this.adminname.Value = base.adminInfo.vcAdminName;
        }
        else
        {
            string adminname = objectHandlers.Post("adminname");
            string nickname = objectHandlers.Post("iNickName");
            string oldpwd = objectHandlers.Post("iOldPWD");  
            string npwd = objectHandlers.Post("iNewPWD");

            if (string.IsNullOrEmpty(adminname) || string.IsNullOrEmpty(nickname))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            oldpwd = objectHandlers.MD5(oldpwd);
            if (!string.IsNullOrEmpty(npwd)) npwd = objectHandlers.MD5(npwd);

            int rtn = base.handlerService.manageService.adminHandlers.ChanageAdminLoginInfo(adminname, oldpwd, npwd, nickname);
            if (rtn == 1)
            {
                SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
            }
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }
}