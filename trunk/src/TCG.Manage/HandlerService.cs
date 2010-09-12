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

using System;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    /// <summary>
    /// 逻辑层方法提供
    /// </summary>
    public class HandlerService : ManageObjectHandlersBase
    {

        public HandlerService()
        {
        }

        /// <summary>
        /// 后台框架操作方法
        /// </summary>
        public SkinService skinService
        {
            get
            {
                if (this._skinservice == null)
                {
                    this._skinservice = new SkinService();
                    this._skinservice.configService = base.configService;
                    this._skinservice.conn = base.conn;
                    this._skinservice.handlerService = this; 
                }
                return this._skinservice;
            }
        }
        private SkinService _skinservice;

        /// <summary>
        /// 后台框架操作方法
        /// </summary>
        public ManageService manageService
        {
            get
            {
                if (this._manageservice == null)
                {
                    this._manageservice = new ManageService();
                    this._manageservice.configService = base.configService;
                    this._manageservice.conn = base.conn;
                    this._manageservice.handlerService = this;
                }
                return this._manageservice;
            }
        }
        private ManageService _manageservice;

        /// <summary>
        /// 提供文件分类操作的方法
        /// </summary>
        public FileService fileService
        {
            get
            {
                if (this._fileservice == null)
                {
                    this._fileservice = new FileService();
                    this._fileservice.configService = base.configService;
                    this._fileservice.conn = base.conn;
                }
                return this._fileservice;
            }
        }
        private FileService _fileservice;


        public ResourcsService resourcsService
        {
            get
            {
                if (this._resourcsservice == null)
                {
                    this._resourcsservice = new ResourcsService();
                    this._resourcsservice.configService = base.configService;
                    this._resourcsservice.conn = base.conn;
                    this._resourcsservice.handlerService = this;
                }
                return this._resourcsservice;
            }
        }
        private ResourcsService _resourcsservice;


        public SheifService sheifService
        {
            get
            {
                if (this._sheifservice == null)
                {
                    this._sheifservice = new SheifService();
                    this._sheifservice.configService = base.configService;
                    this._sheifservice.conn = base.conn;
                    this._sheifservice.handlerService = this;
                }
                return this._sheifservice;
            }
        }
        private SheifService _sheifservice;

   
    }
}