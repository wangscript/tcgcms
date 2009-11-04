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
using TCG.Handlers;
using TCG.Pages;

public partial class Common_AllNewsClass : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var NewsLis = new Array();\r\n");
            CategoriesHandlers cldl = new CategoriesHandlers();
            DataTable dt = cldl.GetCategoriesByCach(base.conn,false);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow Row = dt.Rows[i];
                        Response.Write("NewsLis[" + i.ToString() + "]=[\"" + Row["ID"].ToString() 
                            + "\",\"" + Row["Parent"].ToString() + "\",\"" + Row["vcClassName"].ToString() + "\",\""+
                            Row["vcName"].ToString() + "\",\"" + Row["iTemplate"].ToString() + "\"];\r\n");
                    }
                }
            }
            cldl = null;
            base.Finish();

        }
    }
}
