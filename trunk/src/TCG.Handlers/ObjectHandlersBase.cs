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
using TCG.Utils;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法基类
    /// </summary>
    public class ObjectHandlersBase
    {
        /// <summary>
        /// 设置数据库链接
        /// </summary>
        public Connection conn
        {
            set
            {
                this._conn = value;
            }
            get
            {
                return this._conn;
            }
        }

        private Connection _conn;
        /// <summary>
        /// 获得配置信息支持
        /// </summary>
        public ConfigService configService
        {
            set
            {
                this._configservice = value;
            }
            get
            {
                return this._configservice;
            }
        }
        private ConfigService _configservice;
    }
}
