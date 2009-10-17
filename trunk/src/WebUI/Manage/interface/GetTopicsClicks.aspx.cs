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
using TCG.Data;
using TCG.Template.Handlers;
using TCG.Pages;

using TCG.Entity;
using TCG.News.Handlers;

public partial class Manage_interface_GetTopicsClicks : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string topics = Fetch.Get("topics");
            if (!string.IsNullOrEmpty(topics))
            {
                string SQL = "SELECT * FROM T_News_NewsInfo (NOLOCK) WHERE iId In (" + topics + ")";
                DataTable dt = base.conn.GetDataTable(SQL);
                if (dt == null) return;

                string text = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string txx = i == dt.Rows.Count - 1 ? "" : ",";
                    text += "{Id:\"" + dt.Rows[i]["iId"].ToString() + "\",Click:\"" + dt.Rows[i]["iCount"].ToString() + "\"}" + txx;
                }
                Response.Write(text);
                base.conn.Close();
            }
        }
    }
}
