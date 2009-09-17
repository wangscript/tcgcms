using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Manage.Entity;
using TCG.Manage.Handlers;
using TCG.Manage.Kernel;
using TCG.Manage.Utils;

namespace TCG.Manage.AjaxMethod
{
    public class adminMethod
    {
        public adminMethod()
        {
            if (this._conn == null)
            {
                this._conn = new Connection();
            }
            this._conn.Dblink = DBLinkNums.Manage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod]
        public int adminLogin(string adminname,string password,bool ranme)
        {
            if (string.IsNullOrEmpty(adminname) || string.IsNullOrEmpty(password)) return -1;

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
            adminHandlers ahld = new adminHandlers(this._conn);
            int rtn = ahld.AdminLogin(adminname, password1);
            ahld = null;
            return rtn;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        [Ajax.AjaxMethod]
        public void adminlogout()
        {
            AdminLogin adlogin = new AdminLogin(this._conn);
            adlogin.Logout();
        }

        /// <summary>
        /// 验证输入密码跟原始密码是否相等
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public bool CheckPassWord(string pwd)
        {
            object TempAdmin = SessionState.Get(ManageConst.AdminSessionName);
            if (TempAdmin == null) return false;
            pwd = Text.MD5(pwd);
            if (pwd.ToLower() == ((Admin)TempAdmin).vcPassword.ToLower()) return true;
            return false;
        }

        /// <summary>
        /// 更改用户的登陆信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="oldpwd"></param>
        /// <param name="npwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public int ChanageAdminLoginInfo(string adminname, string oldpwd, string npwd, string nickname)
        {
            if (string.IsNullOrEmpty(adminname) || string.IsNullOrEmpty(nickname)) return -1;
            oldpwd = Text.MD5(oldpwd);
            npwd = Text.MD5(npwd);
            adminHandlers ahd = new adminHandlers(this._conn);
            int rtn = ahd.ChanageAdminLoginInfo(adminname, oldpwd, npwd, nickname);
            ahd = null;
            if (rtn == 1)
            {
                SessionState.Remove(ManageConst.AdminSessionName);
            }
            return rtn;
        }

        private Connection _conn; 
    }
}