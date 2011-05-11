using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.DBHelper;
using TCG.Utils;

namespace TCG.Handlers.Imp
{
    public class DBHelper
    {
        public static IConnection conn
        {
            get
            {
                if (_conn == null)
                {
                    _conn = ConnFactory.CreateConn(ConfigServiceEx.baseConfig["dbtype"]);
                }
                return _conn;
            }
        }

        public static IConnection _conn = null;
    }
}
