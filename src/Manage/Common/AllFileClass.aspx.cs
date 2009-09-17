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
using TCG.Files.Handlers;
using TCG.Scripts.Kernel;

public partial class Common_AllFileClass : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var AllFileClass = new Array();\r\n");
            fileclasshandlers clhd = new fileclasshandlers();
            DataTable dt = clhd.GetAllFileClass(base.conn, true);
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
            clhd = null;
            base.Finish();
        }
    }
}
