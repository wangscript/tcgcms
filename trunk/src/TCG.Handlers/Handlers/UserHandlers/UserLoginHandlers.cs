using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


using TCG.Data;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class UserLoginHandlers : ObjectHandlersBase
    {
       
        public UserLoginHandlers()
        {
            this.Initialize();
        }


        private void Initialize()
        {
            HttpCookie cookie = Cookie.Get(Cookie.USER_COOKIE);
            if (cookie == null)
            {

                this.GuestInit();
            }
            else
            {
                try
                {
                    this.User.Name = HttpContext.Current.Server.UrlDecode(cookie.Values["name"]);
                    this.User.PassWord = cookie.Values["token2"];
                    this._token = cookie.Values["token1"];
                    this.User.CreateTime = DateTime.Parse(cookie.Values["createtime"]);
                    this.User.Id = cookie.Values["ID"];
                    this.User.UserClubLevel = objectHandlers.GetUserClubLevel(objectHandlers.ToInt(cookie.Values["level"],-1));
                }
                catch
                {
                    Cookie.Remove(cookie);
                    this.GuestInit();
                }
            }
        }

        /// <summary>
        /// 保存登陆信息
        /// </summary>
        /// <param name="user"></param>
        public void SetUserLoginCookie(User user)
        {
            HttpCookie cookie = Cookie.Get(Cookie.USER_COOKIE);
            if (cookie == null)
            {
                cookie = Cookie.Set(Cookie.USER_COOKIE);
            }

            cookie.Values["ID"] = HttpContext.Current.Server.UrlEncode(user.Id);
            cookie.Values["name"] = HttpContext.Current.Server.UrlEncode(user.Name);
            cookie.Values["token2"] = HttpContext.Current.Server.UrlEncode(user.PassWord);
            cookie.Values["token1"] = HttpContext.Current.Server.UrlEncode(objectHandlers.GenerateToken(
                string.Concat(new object[] { user.Name, user.PassWord, user.CreateTime.ToString("yyyy-MM-dd H:mm:ss") })));

            cookie.Values["createtime"] = HttpContext.Current.Server.UrlEncode(user.CreateTime.ToString());
            cookie.Values["level"] = HttpContext.Current.Server.UrlEncode(((int)user.UserClubLevel).ToString());
            Cookie.Save(cookie);
        }

        /// <summary>
        /// 初始化游客信息
        /// </summary>
        private void GuestInit()
        {
            this.User.UserClubLevel = UserClubLevel.Guest;
        }


        public bool VerifyUserStatus()
        {
            if (this.User.UserClubLevel != UserClubLevel.Guest &&
                (objectHandlers.GenerateToken(string.Concat(new object[] { this.User.Name, this.User.PassWord, this.User.CreateTime.ToString("yyyy-MM-dd H:mm:ss") })) != this._token))
            {
                Cookie.Remove(Cookie.USER_COOKIE);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 用户实体
        /// </summary>
        public User User
        {
            get
            {
                if (this._user == null)
                {
                    this._user = new User();
                }
                return this._user;
            }
        }

        private User _user = null;
        private string _token = string.Empty;
    }
}
