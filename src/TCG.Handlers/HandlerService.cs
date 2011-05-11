using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class HandlerServiceEx : ObjectHandlersBase
    {
        /// <summary>
        /// 后台框架操作方法
        /// </summary>
        public ManageServiceEx manageService
        {
            get
            {
                if (this._manageservice == null)
                {
                    this._manageservice = new ManageServiceEx();
                    this._manageservice.handlerService = this;
                }
                return this._manageservice;
            }
        }
        private ManageServiceEx _manageservice;
    }
}
