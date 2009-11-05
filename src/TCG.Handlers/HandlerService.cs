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

        public HandlerService(Connection conn, ConfigService configservice)
        {
            base.conn = conn;
            base.configService = configservice;
        }

        /// <summary>
        /// 后台框架操作方法
        /// </summary>
        public ManageService manageService
        {
            get
            {
                if (this._manageservice == null)
                {
                    this._manageservice = new ManageService(base.conn,base.configService);
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
                    this._fileservice = new FileService(base.configService, base.conn);
                }
                return this._fileservice;
            }
        }
        private FileService _fileservice;


        public SkinService skinService
        {
            get
            {
                if (this._skinservice == null)
                {
                    this._skinservice = new SkinService(base.conn,base.configService);
                }
                return this._skinservice;
            }
        }
        private SkinService _skinservice;


        public ResourcsService resourcsService
        {
            get
            {
                if (this._resourcsservice == null)
                {
                    this._resourcsservice = new ResourcsService(base.conn, base.configService);
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
                    this._userservice = new UserService(base.conn,base.configService);

                }
                return this._userservice;
            }
        }
        private UserService _userservice;
        
    }
}