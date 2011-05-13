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

        /// <summary>
        /// 后台框架操作方法
        /// </summary>
        public SkinServiceEx skinService
        {
            get
            {
                if (this._skinservice == null)
                {
                    this._skinservice = new SkinServiceEx();
                    this._skinservice.handlerService = this;
                }
                return this._skinservice;
            }
        }
        private SkinServiceEx _skinservice;
    }
}
