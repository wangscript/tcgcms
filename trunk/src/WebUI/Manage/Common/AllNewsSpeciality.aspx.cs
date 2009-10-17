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


public partial class Common_AllNewsSpeciality : ScriptsMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Write("var NewsSpeciality = new Array();\r\n");
            NewsSpecialityHandlers cldl = new NewsSpecialityHandlers();
            DataTable dt = cldl.GetAllNewsSpecialityInfoByCache(base.conn,false);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow Row = dt.Rows[i];
                        Response.Write("NewsSpeciality[" + i.ToString() + "]=[" + Row["iID"].ToString() + "," + Row["iSiteId"].ToString() + "," + Row["iParent"].ToString() + ",\"" +
                            Row["vcTitle"].ToString() + "\"];\r\n");
                    }
                }
            }
            base.Finish();

        }
    }
}