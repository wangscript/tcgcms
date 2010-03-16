
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
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCG.Data;
using TCG.Utils;
using TCG.Release;
using TCG.Handlers;

using TCG.Entity;

namespace TCG.Pages
{
    public class Origin : Page
    {
        public Origin()
        {
            
        }

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

        protected void AjaxErch(string str)
        {
            Response.Write(str);
            Response.End();
        }

        /// <summary>
        /// 判断数据库执行参数
        /// </summary>
        /// <param name="rtn"></param>
        /// <param name="okmessage"></param>
        protected void AjaxErch(int rtn,string okmessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{state:");
            if (rtn < 0)
            {
                sb.Append("false,message:\"" + errHandlers.GetErrTextByErrCode(rtn, this.configService.baseConfig["ManagePath"]) + "\"");
            }
            else
            {
                sb.Append("true,message:'" + okmessage + "'");
            }
            sb.Append("}");
            Response.ContentType = "application/x-javascript";
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void AjaxErch(int rtn, string okmessage, string callback)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{state:");
            if (rtn < 0)
            {
                sb.Append("false,message:\"" + errHandlers.GetErrTextByErrCode(rtn, this.configService.baseConfig["ManagePath"]) + "\"");
            }
            else
            {
                sb.Append("true,message:\"" + okmessage + "\",callback:\"" + callback + "\"");
            }
            sb.Append("}");
            Response.Write(sb.ToString());
            Response.End();
        }

        /// <summary>
        /// AJAX输出字符串
        /// </summary>
        public string ajaxdata
        {
            get
            {
                return this._ajaxdata;
            }
            set
            {
                this._ajaxdata = value;
            }
        }

        public User User
        {
            get
            {
                if (this._user == null)
                {
                    this._user = this.handlerService.userService.userLoginHandlers.User;
                }
                return this._user;
            }
        }

        private User _user = null;

        private string _ajaxdata = string.Empty;
        private HandlerService _handlerservice = null;
    }
}
