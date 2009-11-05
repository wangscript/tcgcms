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
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using TCG.Data;

using TCG.Entity;
using TCG.Utils;
namespace TCG.Handlers
{
    public class TagService : TCGTagBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TagService(Connection conn, ConfigService configservice, HandlerService handlerservice)
        {
            base.conn = conn;
            base.configService = configservice;
            base.handlerService = handlerservice;
        }

        /// <summary>
        /// 提供对模版处理操作的方法
        /// </summary>
        public TCGTagHandlers TCGTagHandlers
        {
            get
            {
                if (this._tcgtaghandlers == null)
                {
                    this._tcgtaghandlers = new TCGTagHandlers(base.conn,base.configService,base.handlerService);
                }
                return this._tcgtaghandlers;
            }
        }
        private TCGTagHandlers _tcgtaghandlers;
        private HandlerService _handlerservice;
    }
}
