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
    /// <summary>
    /// 后台管理登陆状态的操作方法
    /// </summary>
    public class AdminLoginHandlers : ManageHandlerBase
    {

        public AdminLoginHandlers(Connection conn, ConfigService configservice,HandlerService handlersevice, AdminHandlers adminhandlers)
        {
            base.conn = conn;
            base.configService = configservice;
            base.handlerService = handlersevice;
            this._adminh = adminhandlers;
            this.Initialization();
        }

        private void Initialization()
        {
            if (this._admincookie == null)
            {
                this._admincookie = Cookie.Get(base.configService.baseConfig["AdminCookieName"]);
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
                TempAdmin = SessionState.Get(base.configService.baseConfig["AdminSessionName"]);
                if (TempAdmin != null) SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
                this._admin = null;
                return;
            }
            TempAdmin = SessionState.Get(base.configService.baseConfig["AdminSessionName"]);
            if (TempAdmin == null)
            {
                DataSet ds =  new DataSet();
                int rtn = this._adminh.GetAdminInfoByName(this._name, "01", ref ds);
                if (rtn < 0)
                {
                    SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
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

                SessionState.Set(base.configService.baseConfig["AdminSessionName"], this._admin);
                return;
            }
            this._admin = (Admin)TempAdmin;
            int n = this._admin.PopedomUrls.Rows.Count;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="popnum"></param>
        public void CheckAdminPop()
        {
            this.AdminInit();
            if (!IsSpage(this._currenturl))
            {
                if (this._admin == null)
                {
                    new Terminator().Throw("还未登陆后台，不能访问此页面！", "您还没登陆！", "登陆后台," + base.configService.baseConfig["WebSite"]
                        + base.configService.baseConfig["ManagePath"]
                        + "login.aspx", base.configService.baseConfig["WebSite"] + 
                        base.configService.baseConfig["ManagePath"] + "login.aspx", false);
                    return;
                }
                if (string.IsNullOrEmpty(this._admin.vcAdminName))
                {
                    new Terminator().Throw("还未登陆后台，不能访问此页面！", "您还没登陆！", "登陆后台,"
                        + base.configService.baseConfig["WebSite"] + base.configService.baseConfig["ManagePath"]
                        + "login.aspx", base.configService.baseConfig["WebSite"] + base.configService.baseConfig["ManagePath"] 
                        + "login.aspx", false);
                    return;
                }

                if (IsOnlyLoginPage(this._currenturl)) return;
                if (!HavePower(this._currenturl))
                {
                    new Terminator().Throw("您已经登陆后台，但是不具备该操作的权限！", "您没有该操作权限！",null,null, false);
                    return;
                }
            }
        }

        public bool CheckAdminPop(string page)
        {
            if (!IsSpage(page)) return true;
            if (IsOnlyLoginPage(page)) return true;
            if (HavePower(page)) return true;
            return false;
        }

        private bool IsSpage(string pages)
        {
            List<Option> specialpages = base.configService.manageOutpages["specialpages"];
            bool rtn = false;
            for (int i = 0; i < specialpages.Count; i++)
            {
                string text2 = base.configService.baseConfig["ManagePath"] + specialpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        private bool IsOnlyLoginPage(string pages)
        {
            List<Option> onlineloginpages = base.configService.manageOutpages["onlineloginpages"];
            bool rtn = false;
            for (int i = 0; i < onlineloginpages.Count; i++)
            {
                string text2 = base.configService.baseConfig["ManagePath"] + onlineloginpages[i].Value;
                if (pages.ToLower().IndexOf(text2.ToLower()) > -1) rtn = true;
            }
            return rtn;
        }

        /// <summary>
        /// 根据判断是否有该URL的访问权限
        /// </summary>
        /// <param name="config"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        private bool HavePower(string pages)
        {
            if (this._admin.PopedomUrls.Rows.Count == 0) return false;
            //获得当前URL路径
            string text = this._currenturl.ToLower().Trim();

            ///循环URL权限，包含的返回正确
            for (int i = 0; i < this._admin.PopedomUrls.Rows.Count; i++)
            {
                if (text.IndexOf(this._admin.PopedomUrls.Rows[i]["vcUrl"].ToString()) > -1)
                {
                    return true;
                }
            }
          
            
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
                SessionState.Remove(base.configService.baseConfig["AdminSessionName"]);
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

        
        private HttpCookie _admincookie = null;
        private string _name = string.Empty;
        private Admin _admin = null;
        private AdminHandlers _adminh = null;
        private string _currenturl = string.Empty;
    }
}