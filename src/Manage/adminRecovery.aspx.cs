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

public partial class adminRecovery : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            string strRolename = string.Empty;
            int admincount = 0;
            int rolecount = 0;


            int rtn = base.admin.AdminHandlers.GetAdminList(-1, ref admincount, ref rolecount, ref strRolename, ref ds);
            if (rtn < 0)
            {
                base.Finish();
                return;
            }

            base.Finish();
            this.sAdmincount.Text = admincount.ToString();
            this.sRolecount.Text = rolecount.ToString();
            this.srolename.Text = strRolename;
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
        }
        else
        {
            string admins = Fetch.Post("admins");
            string action = Fetch.Post("saction");

            if (string.IsNullOrEmpty(admins) || string.IsNullOrEmpty(action))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            int rtn = base.admin.AdminHandlers.DelAdmins(base.admin.adminInfo.vcAdminName, admins, action);
            base.AjaxErch(rtn.ToString());
            base.Finish();
        }
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Anchor loginName = (Anchor)e.Item.FindControl("loginName");
        Span nickname = (Span)e.Item.FindControl("nickname");
        Anchor adminrole = (Anchor)e.Item.FindControl("adminrole");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span CheckID = (Span)e.Item.FindControl("CheckID");

        loginName.Text = Row["vcAdminName"].ToString();
        loginName.Href = "adminmdy.aspx?adminname=" + Fetch.UrlEncode(Row["vcAdminName"].ToString());
        CheckID.Text = Row["vcAdminName"].ToString();
        nickname.Text = Row["vcNickName"].ToString();
        adminrole.Text = Row["vcRoleName"].ToString();
        adminrole.Href = "adminrolemdy.aspx?roleid=" + Fetch.UrlEncode(Row["iID"].ToString());
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy年MM月dd日");
    }
}
