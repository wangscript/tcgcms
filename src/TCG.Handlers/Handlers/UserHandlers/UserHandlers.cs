using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{

    /// <summary>
    /// 用户登陆相关操作类
    /// </summary>
    public class UserHandlers : ObjectHandlersBase
    {
        /// <summary>
        /// 用修改，增加操作
        /// </summary>
        /// <param name="user"></param>
        /// <param name="usercontact"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int UserManage(User user,string action)
        {
            SqlParameter sp0 = new SqlParameter("@State", SqlDbType.Int, 4); sp0.Value = (int)user.State;
            SqlParameter sp1 = new SqlParameter("@UserLevel", SqlDbType.Int, 4); sp1.Value = (int)user.UserLevel;
            SqlParameter sp2 = new SqlParameter("@UserClubLevel", SqlDbType.Int,  4); sp2.Value = (int)user.UserClubLevel;
            SqlParameter sp3 = new SqlParameter("@LastLoginTime", SqlDbType.DateTime, 8); sp3.Value = user.LastLoginTime;
            SqlParameter sp4 = new SqlParameter("@Id", SqlDbType.VarChar, 36); sp4.Value = user.Id;
            SqlParameter sp5 = new SqlParameter("@PassWord", SqlDbType.VarChar,32); sp5.Value = user.PassWord;
            SqlParameter sp6 = new SqlParameter("@LastLoginIp", SqlDbType.VarChar, 32); sp6.Value = user.LastLoginIp;
            SqlParameter sp7 = new SqlParameter("@Name", SqlDbType.NVarChar, 50); sp7.Value = user.Name;
            SqlParameter sp8 = new SqlParameter("@Email", SqlDbType.NVarChar, 50); sp8.Value = user.UserContact.Email;
            SqlParameter sp9 = new SqlParameter("@action", SqlDbType.Char, 2); sp9.Value = action;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_UserManage", new SqlParameter[] { sp0, sp1, sp2, sp3 ,sp4,sp5,sp6
            ,sp7,sp8,sp9,sp10}, new int[] { 10 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool CheckUserName(string UserName)
        {
            string SQL = "SELECT * FROM [User] WHERE [name] = '" + objectHandlers.SqlEndcode(UserName) + "'";
            DataTable dt = base.conn.GetDataTable(SQL);
            if (dt.Rows.Count == 0) return true;
            return false;
        }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        /// <returns></returns>
        public string CreateUserId()
        {
            return Guid.NewGuid().ToString().Replace("-","") + "-001";
        }
    }
}