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
using TCG.Pages;
using TCG.News.Handlers;


public partial class jsMethod_AllPop : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var PopLis = new Array();\r\n");
            DataSet ds = base.admin.AdminHandlers.GetAllPopedom();
            if (ds.Tables.Count == 0)
            {
                base.Finish();
                ds = null;
                return;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow Row = ds.Tables[0].Rows[i];
                    Response.Write("PopLis[" + i.ToString() + "]=[" + Row["iID"].ToString() + "," + Row["iParentId"].ToString() + ",\"" + Row["vcPopName"].ToString() + "\"];\r\n");
                }
            }
            ds.Dispose();
            ds.Clear();
            base.Finish();
        }
    }
}
