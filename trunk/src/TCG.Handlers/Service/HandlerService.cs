using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class HandlerService : ObjectHandlersBase
    {
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
                    this._manageservice.handlerService = this;
                }
                return this._manageservice;
            }
        }
        private ManageService _manageservice;

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
                    this._skinservice.handlerService = this;
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
                    this._resourcsservice = new ResourcsService();
                    this._resourcsservice.handlerService = this;
                }
                return this._resourcsservice;
            }
        }
        private ResourcsService _resourcsservice;

        public FileService fileService
        {
            get
            {
                if (this._fileservice == null)
                {
                    this._fileservice = new FileService();
                    this._fileservice.handlerService = this;
                }
                return this._fileservice;
            }
        }
        private FileService _fileservice;

        public SheifService sheifService
        {
            get
            {
                if (this._sheifService == null)
                {
                    this._sheifService = new SheifService();
                    this._sheifService.handlerService = this;
                }
                return this._sheifService;
            }
        }
        private SheifService _sheifService;

        public TagService tagService
        {
            get
            {
                if (this._ragservice == null)
                {
                    this._ragservice = new TagService(this);
                }
                return this._ragservice;
            }
        }
        private TagService _ragservice = null;

        public FeedBackHandlers feedBackHandlers
        {
            get
            {
                if (this._feedbackhandlers == null)
                {
                    this._feedbackhandlers = new FeedBackHandlers();
                }
                return this._feedbackhandlers;
            }
        }
        private FeedBackHandlers _feedbackhandlers = null;
    }
}
