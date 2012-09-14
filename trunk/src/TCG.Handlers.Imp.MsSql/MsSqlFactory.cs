using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using TCG.DBHelper;
using TCG.Utils;

namespace TCG.Handlers.Imp.MsSql
{
    public class MsSqlFactory
    {
        public static IConnection conn
        {
            get
            {
                if (_conn == null)
                {
                    _conn = ConnFactory.CreateConn(ConfigServiceEx.baseConfig["dbtype"]);
                    _conn.SetConnStr(ConfigServiceEx.ManageDataBaseStr);
                }
                return _conn;
            }
        }

        public static IConnection _conn = null;
    }
}
