namespace TCG.Pages
{
    using TCG.Manage.Entity;
    using TCG.Manage.Handlers;
    using TCG.Utils;
    using TCG.Data;
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;

    public class adminMain : Origin
    {
        private AdminLogin _admin;
        private Connection _conn;


        public adminMain()
        {
           
        }

        protected override void OnInit(EventArgs e)
        {
            base.Load += new EventHandler(this.CheckPop);
            base.OnInit(e);
        }

        private void CheckPop(object sender, EventArgs e)
        {
            this.admin.CheckAdminPop(base.config);
        }

        protected AdminLogin admin
        {
            get
             {
                if (this._admin == null)
                {
                    this._admin = new AdminLogin(this.conn);
                }
                return this._admin;
            }
        }

        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = DBLinkNums.Manage;
                }
                return this._conn;
            }
        }

        protected void AjaxErch(string str)
        {
            Response.Write(str);
            this.Finish();
            Response.End();
        }

        protected void Finish()
        {
            if ((this._conn != null) && this._conn.Connected){this._conn.Close();}
            if (this._admin != null) this._admin = null;
        }
    }
}

