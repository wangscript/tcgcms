using System;
using System.Data;
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
        public int Register(User user)
        {

            return 1;
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
    }
}