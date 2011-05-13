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


public partial class adminRecovery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();

            DataSet ds = new DataSet();
            string strRolename = string.Empty;
            int admincount = 0;
            int rolecount = 0;


            int rtn = base.handlerService.manageService.adminHandlers.GetAdminList(-1, ref admincount, ref rolecount, ref strRolename, ref ds);
            if (rtn < 0)
            {
                return;
            }

            this.sAdmincount.Text = admincount.ToString();
            this.sRolecount.Text = rolecount.ToString();
            this.srolename.Text = strRolename;
            this.ItemRepeater.DataSource = ds;
            this.ItemRepeater.DataBind();
        }
        else
        {
            string admins = objectHandlers.Post("admins");
            string action = objectHandlers.Post("saction");

            if (string.IsNullOrEmpty(admins) || string.IsNullOrEmpty(action))
            {
                base.AjaxErch("-1");
                return;
            }

            int rtn = 0;
            try
            {
                rtn = base.handlerService.manageService.adminHandlers.DelAdmins(base.adminInfo.vcAdminName, admins, action);
            }
            catch (Exception ex)
            {
                base.ajaxdata = "{state:false,message:\"" + objectHandlers.JSEncode(ex.Message.ToString()) + "\"}";
                base.AjaxErch(base.ajaxdata);
                return;
            }
            base.AjaxErch(rtn, "管理员[" + admins + "]删除成功！");
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
        loginName.Href = "adminmdy.aspx?adminname=" + objectHandlers.UrlEncode(Row["vcAdminName"].ToString());
        CheckID.Text = Row["vcAdminName"].ToString();
        nickname.Text = Row["vcNickName"].ToString();
        adminrole.Text = Row["vcRoleName"].ToString();
        adminrole.Href = "adminrolemdy.aspx?roleid=" + objectHandlers.UrlEncode(Row["iID"].ToString());
        updatedate.Text = ((DateTime)Row["dUpdateDate"]).ToString("yyyy年MM月dd日");
    }
}
