using System;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;

namespace TCG.Pages
{
    public class ScriptsMain : Origin
    {
        private Connection _conn;

        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = DBLinkNums.News;
                }
                return this._conn;
            }
        }

        protected void Finish()
        {
            if ((this._conn != null) && this._conn.Connected) { this._conn.Close(); }    
        }
    }
}
