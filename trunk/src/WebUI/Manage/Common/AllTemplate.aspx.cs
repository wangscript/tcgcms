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

public partial class Common_AllTemplate : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var AllTemplates = new Array();\r\n");
            TemplateHandlers cldl = new TemplateHandlers();
            DataSet ds = cldl.GetAllTemplates(base.conn,false);
            if (ds != null)
            {
                if (ds.Tables.Count == 0) { base.Finish(); return; }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow Row = ds.Tables[0].Rows[i];
                        Response.Write("AllTemplates[" + i.ToString() + "]=[" + Row["iID"].ToString() + "," + Row["iSiteId"].ToString()
                            + "," + Row["iParentId"].ToString() + "," + Row["iSystemType"].ToString() + ",\"" +
                            Row["vcTempName"].ToString() + "\"," + Row["iType"].ToString() + "];\r\n");
                    }
                }
            }
            cldl = null;
            base.Finish();
        }
    }
}
