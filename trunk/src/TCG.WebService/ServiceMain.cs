using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Handlers;
using TCG.Utils;
using TCG.Data;

namespace TCG.WebService
{
    public class ServiceMain : System.Web.Services.WebService
    {
        public TagService tagService
        {
            get
            {
                if (this._ragservice == null)
                {
                    this._ragservice = new TagService(this.handlerService);
                    this._ragservice.configService = this.configService;
                    this._ragservice.conn = this.conn;
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
                    this._handlerservice.configService = this.configService;
                    this._handlerservice.conn = this.conn;
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

        /// <summary>
        /// 获得配置信息支持
        /// </summary>
        public ConfigService configService
        {
            get
            {
                if (this._configservice == null)
                {
                    this._configservice = new ConfigService();
                }
                return this._configservice;
            }
        }
        private ConfigService _configservice = null;

        /// <summary>
        /// 设置数据库链接
        /// </summary>
        public Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                }
                return this._conn;
            }
        }
        private Connection _conn = null;
    }
}
