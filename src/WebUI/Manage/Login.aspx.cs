using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TCG.Utils;
using TCG.Pages;
using TCG.Manage.Utils;
using TCG.Release;
using TCG.Entity;

using TCG.Handlers;

public partial class aLogin : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.title.Text = base.config["WebTitle"] + " " + Versions.version;
            this.ltitle.Text = base.config["WebTitle"];
            this.month.Text = DateTime.Now.Month.ToString();

            HttpCookie lname = TCG.Utils.Cookie.Get(ManageConst.AdminLoginName);
            if (lname != null)
            {
                this.rUsername.Checked = true;
                this.username.Value = HttpContext.Current.Server.UrlDecode(lname.Values["radminName"].ToString());
            }
        }
        else
        {

            string work = Fetch.Post("Work");

            switch (work)
            {
                case "LOGIN":
                    this.Login();
                    break;
                case "UPDATE" :
                    this.Update();
                    break;
            }
            
            base.Finish();
            return;
        }
    }


    private void Update()
    {
        VersionItem nver = Versions.HigherVersion;
        string vers = Versions.GetVerStr(Versions.HigherVersion.Ver);
        if (nver == null)
        {
            base.AjaxErch("{state:true,message:\"·无法检测到新版本，请跳过...\"}");
            return;
        }
        int sep = objectHandlers.ToInt(Fetch.Post("SqlSep"));
        if (sep > nver.Sqls)
        {
            bool re = false;
            WebClient wc = new WebClient();
            
            string zip = Versions.WebSite + "/update/" + vers + "/" + vers + ".zip";
            string filepath = Server.MapPath("~/" + vers + ".zip");
            try
            {
                Text.SaveFile(filepath, "");
                wc.DownloadFile(zip, filepath);

                string dir = System.IO.Path.GetDirectoryName(filepath);
                string fileName = System.IO.Path.GetFileName(filepath);
                fileZip.UnZipFile(fileName, dir);

                re = true;
            }
            catch
            {
                re = false;
            }
            if (re)
            {
                base.AjaxErch("{state:true,message:\"· " + sep.ToString() + " <span class='red'>"
                   + vers + ".zip</span>文件更新完成！\"}");
            }
            else
            {
                base.AjaxErch("{state:true,message:\"· " + sep.ToString() + "  <span class='red'>"
                    + vers + ".zip</span> 更新失败！\"}");
            }
            return;
        }

        string sqltext = "";
        if (Versions.RunSqlSep(base.conn, sep, ref sqltext))
        {
            base.AjaxErch("{state:false,message:'· " + sep.ToString() + " " + sqltext + " ,执行完成！'}");
        }
        else
        {
            base.AjaxErch("{state:false,message:'· " + sep.ToString() + " " + sqltext + ", 操作失败！'}");
        }
    }

    /// <summary>
    /// 用户登陆
    /// </summary>
    private void Login()
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

        HttpCookie lname = TCG.Utils.Cookie.Get(ManageConst.AdminLoginName);
        if (ranme)
        {
            if (lname == null) lname = TCG.Utils.Cookie.Set(ManageConst.AdminLoginName);
            lname.Values["radminName"] = HttpContext.Current.Server.UrlEncode(adminname);
            lname.Expires = DateTime.Now.AddYears(1);
            TCG.Utils.Cookie.Save(lname);
        }
        else
        {
            if (lname != null) TCG.Utils.Cookie.Remove(lname);
        }
        string password1 = Text.MD5(password);
        string response = "";
        int rtn = base.admin.AdminHandlers.AdminLogin(adminname, password1);

        if (rtn < 0)
        {
            response = "{state:false,message:'" + errHandlers.GetErrTextByErrCode(rtn) + "'}";
        }
        else
        {
            response = "{state:true}";
        }

        base.AjaxErch(response);
    }
}