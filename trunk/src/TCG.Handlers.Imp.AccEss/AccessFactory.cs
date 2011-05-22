using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCG.DBHelper;
using TCG.Utils;

namespace TCG.Handlers.Imp.AccEss
{
    public class AccessFactory
    {
        public static IConnection conn
        {
            get
            {
                if (_conn == null)
                {
                    _conn = ConnFactory.CreateConn(ConfigServiceEx.baseConfig["dbtype"]);
                    _conn.SetConnStr(HttpContext.Current.Server.MapPath(ConfigServiceEx.ManageDataBaseStr));
                }
                return _conn;
            }
        }

        public static IConnection _conn = null;
    }
}
