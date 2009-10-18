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
using TCG.Controls.HtmlControls;
using TCG.Pages;
using TCG.Manage.Utils;

public partial class aMenu : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        DataTable dt = base.adminInfo.PopedomUrls;
        if (dt == null) { this.temp1.Text = ""; base.Finish(); return; }
        if (dt.Rows.Count == 0) { this.temp1.Text = ""; base.Finish(); dt.Dispose(); dt.Clear(); return; }
        DataRow[] Rows = dt.Select("iParentId=" + ParentId.ToString() + " AND cValid='Y'");
        if (Rows.Length == 0) { this.temp1.Text = ""; return; }
        string str = "";
        string script = "<script type=\"text/javascript\">\r\n";
        script += "var btNum =" + Rows.Length.ToString() + ";\r\n";
        for (int i = 0; i < Rows.Length; i++)
        {
            string Url = Rows[i]["vcUrl"].ToString().Replace("$filesite$", base.configService.baseConfig["FileSite"] + base.configService.baseConfig["ManagePath"]);
            
            if (0 == i) script += "window.parent.main.location.href='" + Url + "';\r\n";
            str += string.Format(tempClass, i, Url, Rows[i]["vcPopName"].ToString());
            DataRow[] sRows = dt.Select("iParentId=" + Rows[i]["iId"].ToString() + " AND cValid='Y'");
            if (sRows.Length == 0) continue;
            script += "stNums[" + i.ToString() + "]=" + sRows.Length.ToString() + ";\r\n";
            for (int n = 0; n < sRows.Length; n++)
            {
                string Url1 = sRows[n]["vcUrl"].ToString().Replace("$filesite$", base.configService.baseConfig["FileSite"] + base.configService.baseConfig["ManagePath"]);

                str += string.Format(tempSClass, i, n, Url1, sRows[n]["vcPopName"].ToString(), sRows[n]["iId"].ToString());
            }
        }
        script += "</script>\r\n";
        this.temp1.Text = script;
        this.emu.Text = str;
    }
}