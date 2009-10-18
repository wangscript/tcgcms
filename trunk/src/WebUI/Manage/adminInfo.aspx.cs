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

using TCG.Entity;

public partial class adminInfo : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int iRoleId = objectHandlers.ToInt(objectHandlers.Get("roleid", CheckGetEnum.Safety));

            if (iRoleId == 0)
            {
                this.iRoleAll.Visible = true;
                this.iRoleOther.Visible = false;
            }
            else
            {
                this.iRoleAll.Visible = false;
                this.iRoleOther.Visible = true;
                this.iRoleOther.InnerHtml = this.iRoleOther.InnerHtml.Replace("$iRoleId$", iRoleId.ToString());
            }
            
            DataSet ds = new DataSet();
            string strRolename = string.Empty;
            int admincount = 0;
            int rolecount = 0;


            int rtn = base.handlerService.adminHandlers.GetAdminList(iRoleId, ref admincount, ref rolecount, ref strRolename, ref ds);
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
    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView Row = (DataRowView)e.Item.DataItem;
        Anchor loginName = (Anchor)e.Item.FindControl("loginName");
        Span nickname = (Span)e.Item.FindControl("nickname");
        Anchor adminrole = (Anchor)e.Item.FindControl("adminrole");
        Span updatedate = (Span)e.Item.FindControl("updatedate");
        Span CheckID = (Span)e.Item.FindControl("CheckID");
        Img iLock = (Img)e.Item.FindControl("iLock");

        loginName.Text =  Row["vcAdminName"].ToString();
        if (Row["cLock"].ToString() == "Y")
        {
            iLock.Src = "images/AdminLock.gif";
        }
        else
        {
            iLock.Src = "images/AdminUnLock.gif";
        }
        loginName.Href = "adminmdy.aspx?adminname=" + objectHandlers.UrlEncode(Row["vcAdminName"].ToString());
        CheckID.Text = Row["vcAdminName"].ToString();
        nickname.Text = Row["vcNickName"].ToString();
        adminrole.Text = Row["vcRoleName"].ToString();
        adminrole.Href = "adminrolemdy.aspx?roleid=" + objectHandlers.UrlEncode(Row["iID"].ToString());
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy年MM月dd日");
    }
}
