/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

namespace TCG.Pages
{
    using TCG.Entity;
    using TCG.Handlers;
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

