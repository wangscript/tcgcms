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
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Entity;


public partial class aMenu : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检测管理员登录
        base.handlerService.manageService.adminHandlers.CheckAdminLogin();

        if (!Page.IsPostBack)
        {
            this.meunInit();
            this.temp2.Text = "";
        }
    }

    private void meunInit()
    {
        string tempClass = this.temp1.Text;
        string tempSClass = this.temp2.Text;

        int ParentId = objectHandlers.ToInt(objectHandlers.Get("ParendId"));
        if (ParentId == 0) ParentId = 1;

        Dictionary<int, Popedom> cpop = base.handlerService.manageService.adminHandlers.GetChildManagePopedomEntity(ParentId);

        StringBuilder sb = new StringBuilder();
        StringBuilder script = new StringBuilder();

        script.Append("<script type=\"text/javascript\">\r\n");
        script.Append("var btNum =" + cpop.Count.ToString() + ";\r\n");

        int i = 0;
        foreach (KeyValuePair<int, Popedom> keyvalue in cpop)
        {
            string Url = keyvalue.Value.vcUrl;

            if (0 == i) script.Append("window.parent.main.location.href='" + Url + "';\r\n");
            sb.Append( string.Format(tempClass, i, Url, keyvalue.Value.vcPopName));

            Dictionary<int, Popedom> tpop = base.handlerService.manageService.adminHandlers.GetChildManagePopedomEntity(keyvalue.Value.iID);
            if (tpop == null) continue;

            script.Append("stNums[" + i.ToString() + "]=" + tpop.Count.ToString() + ";\r\n");

            int n = 0;
            foreach (KeyValuePair<int, Popedom> keyvalue1 in tpop)
            {
                string Url1 = keyvalue1.Value.vcUrl;
                sb.Append(string.Format(tempSClass, i, n, Url1, keyvalue1.Value.vcPopName, keyvalue1.Value.iID.ToString()));
                n++;
            }

            i++;
        }


        script.Append("</script>\r\n");
        this.temp1.Text = script.ToString();
        this.emu.Text = sb.ToString();
    }
}