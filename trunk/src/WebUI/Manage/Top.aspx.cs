using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;

using TCG.Utils;

using TCG.Entity;


namespace TCG.CMS.WebUi
{

    public partial class Top : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(15);

            if (!Page.IsPostBack)
            {
                this.adminName.Text = base.adminInfo.vcNickName + "(" + base.adminInfo.iRole.vcRoleName + ")";
                this.EmuInit();
            }
        }

        private void EmuInit()
        {
            Dictionary<int, Popedom> managepop = base.handlerService.manageService.adminHandlers.GetChildManagePopedomEntity(0);

            StringBuilder sb = new StringBuilder();
            if (managepop != null)
            {
                int i = 0;
                foreach (KeyValuePair<int, Popedom> keyvalue in managepop)
                {

                    if (base.handlerService.manageService.adminHandlers.CheckAdminPopEx(keyvalue.Value.iID))
                    {
                        string text = "top_title1";
                        if (i == 0) text = "top_title";
                        sb.Append("<a id=\"m_" + i.ToString() + "\" href=\"Menu.aspx?ParendId=" + keyvalue.Value.iID.ToString()
                                + "\" target=\"menu\" onclick=\"SelEmu(" + i.ToString() + ")\" class=\""
                                + text + " bold\">" + keyvalue.Value.vcPopName + "</a>\r\n");
                        i++;
                        if (i != managepop.Count) sb.Append("<a id=\"mm_" + i.ToString() + "\" class=\"top_title_m ttmbg\"></a>\r\n");
                    }
                }
            }
            sb.Append("<script type=\"text/javascript\">var Mnum=" + managepop.Count.ToString() + ";var SelecM=0;</script>");

            this.emu.Text = sb.ToString();

        }
    }
}