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
using TCG.News.Handlers;
using TCG.Pages;


public partial class Common_AllNewsFrom : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var NewsFromItems = new Array();\r\n");
            newsFromHandlers cldl = new newsFromHandlers();
            DataTable dt = cldl.GetAllNewsFromByCach(base.conn,false);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow Row = dt.Rows[i];
                        Response.Write("NewsFromItems[" + i.ToString() + "]=[" + Row["iID"].ToString() + ",\"" + Row["vcTitle"].ToString() + "\",\"" +
                            Row["vcUrl"].ToString() + "\"];\r\n");
                    }
                }
            }
            base.Finish();

        }
    }
}