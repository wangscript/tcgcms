﻿/* 
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
using TCG.Utils;

namespace TCG.Handlers
{
    /// <summary>
    /// 用户操作服务
    /// </summary>
    public class UserService :ObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserService(Connection conn, ConfigService configservice)
        {
            base.conn = conn;
            base.configService = configservice;
        }

        /// <summary>
        /// 提供用户操作的方法
        /// </summary>
        public UserHandlers userHandlers
        {
            get
            {
                if (this._userhandlers == null)
                {
                    this._userhandlers = new UserHandlers();
                    this._userhandlers.conn = base.conn;
                    this._userhandlers.configService = base.configService;
                }
                return this._userhandlers;
            }
        }

        private UserHandlers _userhandlers = null;


        /// <summary>
        /// 前台用户登录的操作类
        /// </summary>
        public UserLoginHandlers userLoginHandlers
        {
            get
            {
                if (this._userloginhandlers == null)
                {
                    this._userloginhandlers = new UserLoginHandlers();
                    this._userloginhandlers.conn = base.conn;
                    this._userloginhandlers.configService = base.configService;
                }
                return this._userloginhandlers;
            }
        }

        private UserLoginHandlers _userloginhandlers = null;
    }
}