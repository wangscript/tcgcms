using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Manage.Entity;
using TCG.Manage.Utils;
using TCG.Manage.Handlers;

namespace TCG.Pages
{
    public class AdminLogin
    {

        public AdminLogin(Connection conn)
        {
            this._adminh = new adminHandlers(conn);
            this.Initialization();
        }

        private void Initialization()
        {
            if (this._admincookie == null)
            {
                this._admincookie = Cookie.Get(ManageConst.AdminCookieName);
                if (this._admincookie != null)
                {
                    if (this._admincookie.Values.Count != 1) return;
                    this._name = Fetch.UrlDecode(this._admincookie.Values["AdminName"].ToString());
                }
            }
            this._currenturl = this.RemoveA(Fetch.CurrentUrl);
        }

        private void AdminInit()
        {
            if (this._admin != null) return;
            object TempAdmin = null;
            if (string.IsNullOrEmpty(this._name))
            {
                TempAdmin = SessionState.Get(ManageConst.AdminSessionName);
                if (TempAdmin != null) SessionState.Remove(ManageConst.AdminSessionName);
                this._admin = null;
                return;
            }
            TempAdmin =  SessionState.Get(ManageConst.AdminSessionName);
            if (TempAdmin == null)
            {
                DataSet ds =  new DataSet();
                int rtn = this._adminh.GetAdminInfoByName(this._name, "01", ref ds);
                if (rtn < 0)
                {
                    SessionState.Remove(ManageConst.AdminSessionName);
                    this._admin = null;
                    return;
                }
                this._admin = new Admin();
                if (ds != null)
                {
                    
                    this._admin.PopedomUrls = ds.Tables[0];
                    this._admin.vcAdminName = ds.Tables[1].Rows[0]["vcAdminName"].ToString();
                    this._admin.vcNickName = ds.Tables[1].Rows[0]["vcNickName"].ToString();
                    this._admin.vcPassword = ds.Tables[1].Rows[0]["vcPassword"].ToString();
                    this._admin.vcRoleName = ds.Tables[1].Rows[0]["vcRoleName"].ToString();
                }

                SessionState.Set(ManageConst.AdminSessionName, this._admin);
                return;
            }
            this._admin = (Admin)TempAdmin;
            int n = this._admin.PopedomUrls.Rows.Count;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="popnum"></param>
        public void CheckAdminPop(Config config)
        {
            this.AdminInit();
            if (!IsSpage(config,this._currenturl))
            {
                if (this._admin == null)
                {
                    new Terminator().Throw("还未登陆后台，不能访问此页面！", "您还没登陆！", "登陆后台," + config["WebSite"] + config["ManagePath"]
                        + "login.aspx", config["WebSite"] + config["ManagePath"] + "login.aspx", false);
                    return;
                }
                if (string.IsNullOrEmpty(this._admin.vcAdminName))
                {
                    new Terminator().Throw("还未登陆后台，不能访问此页面！", "您还没登陆！", "登陆后台," + config["WebSite"] + config["ManagePath"]
                        + "login.aspx", config["WebSite"] + config["ManagePath"] + "login.aspx", false);
                    return;
                }

                if (IsOnlyLoginPage(config, this._currenturl)) return;
                if (!HavePower(config, this._currenturl))
                {
                    new Terminator().Throw("您已经登陆后台，但是不具备该操作的权限！", "您没有该操作权限！",null,null, false);
                    return;
                }
            }
        }

        public bool CheckAdminPop(Config config, string page)
        {
            if (!IsSpage(config, page)) return true;
            if (IsOnlyLoginPage(config, page)) return true;
            if (HavePower(config, page)) return true;
            return false;
        }

        private bool IsSpage(Config config,string pages)
        {
            bool rtn = false;
            for (int i = 0; i < ManageConst.SpecialPages.Length; i++)
            {
                string text2 = config["WebSite"] + config["ManagePath"] + ManageConst.SpecialPages[i];
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool IsOnlyLoginPage(Config config,string pages)
        {
            bool rtn = false;
            for (int i = 0; i < ManageConst.OnlineLoginPages.Length; i++)
            {
                string text2 = config["WebSite"] + config["ManagePath"] + ManageConst.OnlineLoginPages[i];
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool HavePower(Config config,string pages)
        {
            if (this._admin.PopedomUrls.Rows.Count == 0) return false;
            string text = this._currenturl.ToLower().Trim();

            text = text.ToLower().Replace(config["WebSite"].ToLower() + config["ManagePath"].ToLower(), "").Trim();
          
            DataRow[] rows = this._admin.PopedomUrls.Select("vcUrl='" + text + "'");
            if (rows.Length > 0) return true;
            rows = this._admin.PopedomUrls.Select("vcUrl='" + "$filesite$" + text + "'");
            if (rows.Length > 0) return true;
            rows = this._admin.PopedomUrls.Select("vcUrl='" + "$bbsSite$" + text + "'");
            if (rows.Length > 0) return true; 
            return false;
        }

        private string RemoveA(string str)
        {
            if (str.IndexOf("?") > 0)
            {
                str = str.Substring(0, str.IndexOf("?"));
            }
            return str;
        }

        public void Logout()
        {
            this.AdminInit();
            this._adminh.AdminLoginOut(this._name);
            if (this._admincookie != null)
            {
                Cookie.Remove(this._admincookie);
            }
            if (this._admin != null)
            {
                SessionState.Remove(ManageConst.AdminSessionName);
            }
        }

       
        public adminHandlers AdminHandlers { get { return this._adminh; } }
        public Admin adminInfo 
        {
            get 
            {
                this.AdminInit();
                if (this._admin == null) return new Admin();
                return this._admin; 
            }
        }

        
        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private adminHandlers _adminh = null;
        private Admin _admin = null;
        private string _currenturl = string.Empty;
    }
}