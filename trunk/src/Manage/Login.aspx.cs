using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Pages;
using TCG.Manage.Utils;

public partial class aLogin : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            this.title.Text = base.config["SysTitle"];
            this.ltitle.Text = base.config["SysTitle"];
            this.month.Text = DateTime.Now.Month.ToString();

            HttpCookie lname = Cookie.Get(ManageConst.AdminLoginName);
            if (lname != null)
            {
                this.rUsername.Checked = true;
                this.username.Value = HttpContext.Current.Server.UrlDecode(lname.Values["radminName"].ToString());
            }
        }
        else
        {
            
            string adminname = Fetch.Post("username");
            string password = Fetch.Post("password");
            string anme = Fetch.Post("rUsername");

            bool ranme = anme == "checkbox" ? true : false;

            if (string.IsNullOrEmpty(adminname) || string.IsNullOrEmpty(password))
            {
                base.AjaxErch("-1");
                base.Finish();
                return;
            }

            HttpCookie lname = Cookie.Get(ManageConst.AdminLoginName);
            if (ranme)
            {
                if (lname == null) lname = Cookie.Set(ManageConst.AdminLoginName);
                lname.Values["radminName"] = HttpContext.Current.Server.UrlEncode(adminname);
                lname.Expires = DateTime.Now.AddYears(1);
                Cookie.Save(lname);
            }
            else
            {
                if (lname != null) Cookie.Remove(lname);
            }
            string password1 = Text.MD5(password);
             int rtn = base.admin.AdminHandlers.AdminLogin(adminname, password1);
            base.AjaxErch(rtn.ToString());
            base.Finish();
            return;
        }
    }
}