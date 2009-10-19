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
    public class HandlerService
    {

        public HandlerService(Connection conn, ConfigService configservice)
        {
            this._conn = conn;
            this._configservice = configservice;
        }

        /// <summary>
        /// 提供对管理员操作的方法
        /// </summary>
        public AdminHandlers adminHandlers
        {
            get
            {
                if(this._adminhandlers==null)
                {
                    this._adminhandlers = new AdminHandlers(this._conn);
                    this._adminhandlers.configService = this._configservice;
                }
                return this._adminhandlers;
            }
        }
        private AdminHandlers _adminhandlers;


        /// <summary>
        /// 提供对管理员操作的方法
        /// </summary>
        public AdminLoginHandlers adminLoginHandlers
        {
            get
            {
                if (this._adminloginhandlers == null)
                {
                    this._adminloginhandlers = new AdminLoginHandlers(this._conn,this.adminHandlers,this._configservice);
                }
                return this._adminloginhandlers;
            }
        }
        private AdminLoginHandlers _adminloginhandlers;

        /// <summary>
        /// 提供文件分类操作的方法
        /// </summary>
        public FileService fileService
        {
            get
            {
                if (this._fileservice == null)
                {
                    this._fileservice = new FileService(this._configservice,this._conn);
                }
                return this._fileservice;
            }
        }
        private FileService _fileservice;


        /// <summary>
        /// 提供对资讯分类操作的方法
        /// </summary>
        public NewsClassHandlers newsClassHandlers
        {
            get
            {
                if (this._newsclasshandlers == null)
                {
                    this._newsclasshandlers = new NewsClassHandlers();
                }
                return this._newsclasshandlers;
            }
        }
        private NewsClassHandlers _newsclasshandlers;


        /// <summary>
        /// 提供对文件来源操作的方法
        /// </summary>
        public NewsFromHandlers newsFromHandlers
        {
            get
            {
                if (this._newsfromhandlers == null)
                {
                    this._newsfromhandlers = new NewsFromHandlers();
                }
                return this._newsfromhandlers;
            }
        }
        private NewsFromHandlers _newsfromhandlers;


        /// <summary>
        /// 提供对资讯操作的方法
        /// </summary>
        public NewsInfoHandlers newsInfoHandlers
        {
            get
            {
                if (this._newsinfohandlers == null)
                {
                    this._newsinfohandlers = new NewsInfoHandlers();
                }
                return this._newsinfohandlers;
            }
        }
        private NewsInfoHandlers _newsinfohandlers;

        /// <summary>
        /// 提供对资讯特性操作的方法
        /// </summary>
        public NewsSpecialityHandlers newsSpecialityHandlers
        {
            get
            {
                if (this._newsspecialityhandlers == null)
                {
                    this._newsspecialityhandlers = new NewsSpecialityHandlers();
                }
                return this._newsspecialityhandlers ;
            }
        }
        private NewsSpecialityHandlers _newsspecialityhandlers;



        /// <summary>
        /// 提供对模版操作的方法
        /// </summary>
        public TemplateHandlers templateHandlers
        {
            get
            {
                if (this._templatehandlers == null)
                {
                    this._templatehandlers = new TemplateHandlers();
                }
                return this._templatehandlers;
            }
        }
        private TemplateHandlers _templatehandlers;


        /// <summary>
        /// 提供对模版处理操作的方法
        /// </summary>
        public TCGTagHandlers TCGTagHandlers
        {
            get
            {
                if (this._tcgtaghandlers == null)
                {
                    this._tcgtaghandlers = new TCGTagHandlers();
                    this._tcgtaghandlers.handlerService = this;
                }
                return this._tcgtaghandlers;
            }
        }
        private TCGTagHandlers _tcgtaghandlers;
        

        private Connection _conn;
        private ConfigService _configservice = null;
    }
}