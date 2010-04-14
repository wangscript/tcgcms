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

public partial class Common_AllFileClass : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var AllFileClass = new Array();\r\n");

            DataTable dt = base.handlerService.fileService.fileClassHandlers.GetAllFileClass(true);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow Row = dt.Rows[i];
                        Response.Write("AllFileClass[" + i.ToString() + "]=[" + Row["iId"].ToString() + "," + Row["iParentId"].ToString() + ",\"" +
                            Row["vcFileName"].ToString() + "\",\"" + Row["vcMeno"].ToString() + "\"];\r\n");
                    }
                }
            }
            base.Finish();
        }
    }
}
