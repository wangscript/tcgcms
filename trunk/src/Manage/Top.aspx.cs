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
using TCG.Manage.Kernel;

public partial class Top : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.adminName.Text = base.admin.adminInfo.vcNickName + "(" + base.admin.adminInfo.vcRoleName + ")";
            this.EmuInit();
        }
    }

    private void EmuInit()
    {
        DataTable dt = base.admin.adminInfo.PopedomUrls;
        if (dt == null) { base.Finish(); return; }
        if (dt.Rows.Count == 0) { base.Finish(); dt.Dispose(); dt.Clear(); return; }
        DataRow[] rows = dt.Select("iParentId=0 AND cValid='Y'");
        if (rows.Length == 0) { base.Finish(); rows = null; dt.Dispose(); dt.Clear(); return; }

        string str = "";
        for (int i = 0; i < rows.Length; i++)
        {
            string text = "top_title1";
            if (i == 0) text = "top_title";

            string text2 = "";
            if (i != (rows.Length - 1))
            {
                text2 = "<a id=\"mm_" + i.ToString() + "\" class=\"top_title_m ttmbg\"></a>\r\n";
            }
            str += "<a id=\"m_" + i.ToString() + "\" href=\"Menu.aspx?ParendId=" + rows[i]["iId"].ToString()
                + "\" target=\"menu\" onclick=\"SelEmu(" + i.ToString() + ")\" class=\""
                + text + " bold\">" + rows[i]["vcPopName"].ToString() + "</a>\r\n" + text2;
        }
        str += "<script type=\"text/javascript\">var Mnum=" + rows.Length.ToString()+";var SelecM=0;</script>";
        this.emu.Text = str;
        base.Finish();
    }
}
