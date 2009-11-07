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
    public class HandlerService : ObjectHandlersBase
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
                    this._fileservice.handlerService = this;
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


        /// <summary>
        /// 用户的操作的服务
        /// </summary>
        public UserService userService
        {
            get
            {
                if (this._userservice == null)
                {
                    this._userservice = new UserService();
                    this._userservice.configService = base.configService;
                    this._userservice.conn = base.conn;
                    this._userservice.handlerService = this;
                }
                return this._userservice;
            }
        }
        private UserService _userservice;
        
    }
}