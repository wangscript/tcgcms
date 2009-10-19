/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;

using TCG.Handlers;

namespace TCG.Handlers
{
    public class AdminLoginHandlers
    {

        public AdminLoginHandlers(Connection conn, AdminHandlers adminhandlers, ConfigService configservice)
        {
            this._adminh = adminhandlers;
            this._configservice = configservice;
            this.Initialization();
        }

        private void Initialization()
        {
            if (this._admincookie == null)
            {
                this._admincookie = Cookie.Get(this._configservice.baseConfig["AdminCookieName"]);
                if (this._admincookie != null)
                {
                    if (this._admincookie.Values.Count != 1) return;
                    this._name = objectHandlers.UrlDecode(this._admincookie.Values["AdminName"].ToString());
                }
            }
            this._currenturl = this.RemoveA(objectHandlers.CurrentUrl);
        }

        private void AdminInit()
        {
            if (this._admin != null) return;
            object TempAdmin = null;
            if (string.IsNullOrEmpty(this._name))
            {
                TempAdmin = SessionState.Get(this._configservice.baseConfig["AdminSessionName"]);
                if (TempAdmin != null) SessionState.Remove(this._configservice.baseConfig["AdminSessionName"]);
                this._admin = null;
                return;
            }
            TempAdmin = SessionState.Get(this._configservice.baseConfig["AdminSessionName"]);
            if (TempAdmin == null)
            {
                DataSet ds =  new DataSet();
                int rtn = this._adminh.GetAdminInfoByName(this._name, "01", ref ds);
                if (rtn < 0)
                {
                    SessionState.Remove(this._configservice.baseConfig["AdminSessionName"]);
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

                SessionState.Set(this._configservice.baseConfig["AdminSessionName"], this._admin);
                return;
            }
            this._admin = (Admin)TempAdmin;
            int n = this._admin.PopedomUrls.Rows.Count;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="popnum"></param>
        public void CheckAdminPop(Dictionary<string, string> config)
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

        public bool CheckAdminPop(Dictionary<string, string> config, string page)
        {
            if (!IsSpage(config, page)) return true;
            if (IsOnlyLoginPage(config, page)) return true;
            if (HavePower(config, page)) return true;
            return false;
        }

        private bool IsSpage(Dictionary<string, string> config, string pages)
        {
            List<Option> specialpages = this._configservice.manageOutpages["specialpages"];
            bool rtn = false;
            for (int i = 0; i < specialpages.Count; i++)
            {
                string text2 = config["WebSite"] + config["ManagePath"] + specialpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool IsOnlyLoginPage(Dictionary<string, string> config, string pages)
        {
            List<Option> onlineloginpages = this._configservice.manageOutpages["onlineloginpages"];
            bool rtn = false;
            for (int i = 0; i < onlineloginpages.Count; i++)
            {
                string text2 = config["WebSite"] + config["ManagePath"] + onlineloginpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool HavePower(Dictionary<string, string> config, string pages)
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
                SessionState.Remove(this._configservice.baseConfig["AdminSessionName"]);
            }
        }

       
        public Admin adminInfo 
        {
            get 
            {
                this.AdminInit();
                if (this._admin == null) return new Admin();
                return this._admin; 
            }
        }

        private Connection _conn;
        /// <summary>
        /// 获得配置信息支持
        /// </summary>
        public ConfigService configService
        {
            set
            {
                this._configservice = value;
            }
        }
        private ConfigService _configservice;

        
        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private Admin _admin = null;
        private AdminHandlers _adminh = null;
        private string _currenturl = string.Empty;
    }
}