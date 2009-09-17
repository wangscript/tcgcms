using System;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Files.Utils;

namespace TCG.Files.Kernel
{
    public class FilesMain : Origin
    {
        private Connection _conn;

        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = FilesConst.FilesDbLinks[0];
                }
                return this._conn;
            }
        }

        protected void AjaxErch(string str)
        {
            Response.Write(str);
            Response.End();
        }

        protected void Finish()
        {
            if ((this._conn != null) && this._conn.Connected) { this._conn.Close(); }    
        }
    }
}
