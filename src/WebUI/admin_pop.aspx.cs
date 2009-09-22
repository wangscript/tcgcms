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
using TCG.Pages;
using TCG.Manage.Utils;

public partial class admin_pop : adminMain
{
    private int inum = 1;
    private string astr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            int admincount = 0;
            int delcount = 0;
            int rtn = base.admin.AdminHandlers.GetAdminRoleInfo(ref admincount,ref delcount, ref ds);
            if (rtn < 0)
            {
                base.Finish();
                return;
            }
            base.Finish();
            this.admincount.Text = admincount.ToString();
            this.deladmincount.Text = delcount.ToString();
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
            this.emunum.Text = ds.Tables[0].Rows.Count.ToString();
            this.alist.Text = astr;
        }
        else
        {
            string admins = Fetch.Post("admins");
            int iRole = Bases.ToInt(Fetch.Post("iRole"));

            if (string.IsNullOrEmpty(admins) || iRole == 0)
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.admin.AdminHandlers.AdminChangeGroup(base.admin.adminInfo.vcAdminName, admins, iRole);
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Span rolename = (Span)e.Item.FindControl("rolename");
        Span count = (Span)e.Item.FindControl("count");
        Span num = (Span)e.Item.FindControl("num");
        Span roleid = (Span)e.Item.FindControl("roleid");

        if (!string.IsNullOrEmpty(astr))astr += ",";
        astr += "{href:\"javascript:GoTo();\",onclick:\"AddGroup(" + Row["iID"].ToString() +");\",Text:\""
            + Row["vcRoleName"].ToString() + "(" + Row["num"].ToString() + ")\"}";
        rolename.Text = Row["vcRoleName"].ToString();
        count.Text = Row["num"].ToString();
        num.Text = (inum + 2).ToString();
        inum = inum + 1;
        roleid.Text = Row["iID"].ToString();
    }
}