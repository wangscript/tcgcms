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
using TCG.Controls.HtmlControls;
using TCG.Manage.Kernel;
using TCG.Manage.Utils;
using TCG.News.Handlers;

public partial class Template_NewsInfo : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            classHandlers chdl = new classHandlers();
            DataTable dt = chdl.GetClassInfosByParentId(0, base.conn,false);
            if (dt != null)
            {
                this.ItemRepeater.DataSource = dt;
                this.ItemRepeater.DataBind();

                dt.Clear();
                dt.Dispose();
            }
           
            chdl = null;
            base.Finish();
        }
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Anchor sitename = (Anchor)e.Item.FindControl("sitename");
        Span info = (Span)e.Item.FindControl("info");

        info.Text = Row["vcName"].ToString();
        sitename.Text = Row["vcClassName"].ToString();
        sitename.Href = "newlist.aspx?iSiteId=" + Row["iId"].ToString();
    }
}

