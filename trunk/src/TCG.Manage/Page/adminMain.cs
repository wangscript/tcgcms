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

        public adminMain()
        {
           
        }

        public TagService tagService
        {
            get
            {
                if (this._ragservice == null)
                {
                    this._ragservice = new TagService(this.handlerService);
                    this._ragservice.configService = base.configService;
                    this._ragservice.conn = base.conn;
                }
                return this._ragservice;
            }
        }
        private TagService _ragservice = null;


        /// <summary>
        /// 提供系统操作方法的服务
        /// </summary>
        protected HandlerService handlerService
        {
            get
            {
                if (this._handlerservice == null)
                {
                    this._handlerservice = new HandlerService();
                    this._handlerservice.configService = base.configService;
                    this._handlerservice.conn = base.conn;
                }
                return this._handlerservice;
            }
        }
        private HandlerService _handlerservice = null;

        protected Admin adminInfo
        {
            get
            {
                if (this._admininfo == null)
                {
                    this._admininfo = this.handlerService.manageService.adminLoginHandlers.adminInfo;
                }
                return this._admininfo;
            }
        }
        private Admin _admininfo = null;
    }
}

