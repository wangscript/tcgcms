using System;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Utils;

namespace TCG.Handlers
{
    public class ManageService : ManageObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManageService()
        {
        }

        /// <summary>
        /// 管理员的操作方法
        /// </summary>
        public AdminHandlers adminHandlers
        {
            get
            {
                if (this._adminhandlers == null)
                {
                    this._adminhandlers = new AdminHandlers();
                    this._adminhandlers.configService = base.configService;
                    this._adminhandlers.conn = base.conn;
                    this._adminhandlers.handlerService = base.handlerService;
                }
                return this._adminhandlers;
            }
        }
        private AdminHandlers _adminhandlers = null;


        public AdminLoginHandlers adminLoginHandlers
        {
            get
            {
                if (this._adminloginhandlers == null)
                {
                    this._adminloginhandlers = new AdminLoginHandlers(base.conn,base.configService,base.handlerService, this.adminHandlers);       
                }
                return this._adminloginhandlers;
            }
        }
        private AdminLoginHandlers _adminloginhandlers = null;
    }
}
