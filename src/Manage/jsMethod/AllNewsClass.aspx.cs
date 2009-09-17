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
using TCG.News.Handlers;


public partial class jsMethod_AllNewsClass : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var NewsLis = new Array();\r\n");
            classHandlers cldl = new classHandlers();
            DataTable dt = cldl.GetClassInfos(base.conn);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow Row = dt.Rows[i];
                        Response.Write("NewsLis[" + i.ToString() + "]=[" + Row["iID"].ToString() + "," + Row["iParent"].ToString() + ",\"" + Row["vcClassName"].ToString() + "\",\""+
                            Row["vcName"].ToString() + "\"];\r\n");
                    }
                }
            }
            dt.Dispose();
            dt.Clear();
            base.Finish();

        }
    }
}
