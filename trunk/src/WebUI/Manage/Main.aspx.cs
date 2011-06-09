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

using TCG.Release;


public partial class Main : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();
        base.handlerService.manageService.adminHandlers.CheckAdminPop(13);

        if (!Page.IsPostBack)
        {
            this.title.Text = ConfigServiceEx.baseConfig["WebTitle"] + " - " + Versions.version;
        }
    }
}
