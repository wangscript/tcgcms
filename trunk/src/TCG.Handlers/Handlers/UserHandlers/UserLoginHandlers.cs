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
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UserLogin(ref User user)
        {
            DataSet ds = new DataSet();
            SqlParameter sp0 = new SqlParameter("@UserName", SqlDbType.NVarChar, 50); sp0.Value = user.Name;
            SqlParameter sp1 = new SqlParameter("@UserPassWord", SqlDbType.VarChar, 32); sp1.Value = user.PassWord;
            SqlParameter sp2 = new SqlParameter("@reValue", SqlDbType.Int); sp2.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.GetDataSet("SP_UserLogin", new SqlParameter[] { sp0, sp1, sp2}, new int[] { 2 },ref ds);
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = ds.Tables[0].Rows[0];
                    user.Name = Row["Name"].ToString();
                    user.CreateTime = (DateTime)Row["CreateTime"];
                    user.Id = Row["Id"].ToString();
                    user.UserClubLevel = objectHandlers.GetUserClubLevel(objectHandlers.ToInt(Row["UserClubLevel"].ToString()));
                }
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public void UserLogout()
        {
            HttpCookie cookie = Cookie.Get(Cookie.USER_COOKIE);
            if (cookie != null) Cookie.Remove(Cookie.USER_COOKIE);
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

            cookie.Values["createtime"] = user.CreateTime.ToString("G");
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
