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


namespace TCG.CMS.WebUi
{
    public partial class MyAccount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(3);

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
                    return;
                }

                oldpwd = objectHandlers.MD5(oldpwd);
                if (!string.IsNullOrEmpty(npwd)) npwd = objectHandlers.MD5(npwd);

                int rtn = 0;

                try
                {
                    rtn = base.handlerService.manageService.adminHandlers.ChanageAdminLoginInfo(adminname, oldpwd, npwd, nickname);
                }
                catch (Exception ex)
                {
                    base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                    base.AjaxErch(base.ajaxdata);
                    return;
                }

                if (rtn == 1)
                {
                    SessionState.Remove(ConfigServiceEx.baseConfig["AdminSessionName"]);
                    CachingService.Remove(CachingService.CACHING_ALL_ADMIN_ENTITY);
                }

                base.AjaxErch(rtn, "密码修改成功！");
            }
        }
    }
}