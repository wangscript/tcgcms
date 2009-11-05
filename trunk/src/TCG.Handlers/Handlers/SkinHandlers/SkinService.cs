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
    public class SkinService : ObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SkinService(Connection conn, ConfigService configservice)
        {
            base.conn = conn;
            base.configService = configservice;
        }


        public CategoriesHandlers categoriesHandlers
        {
            get
            {
                if (this._categorieshandlers == null)
                {
                    this._categorieshandlers = new CategoriesHandlers();
                    this._categorieshandlers.conn = base.conn;
                    this._categorieshandlers.configService = base.configService;
                }
                return this._categorieshandlers;
            }
        }
        private CategoriesHandlers _categorieshandlers = null;


        public SpecialityHandlers specialityHandlers
        {
            get
            {
                if (this._specialityhandlers == null)
                {
                    this._specialityhandlers = new SpecialityHandlers();
                    this._specialityhandlers.conn = base.conn;
                    this._specialityhandlers.configService = base.configService;
                }
                return this._specialityhandlers;
            }
        }
        private SpecialityHandlers _specialityhandlers = null;


        public TemplateHandlers templateHandlers
        {
            get
            {
                if (this._templateHandlers == null)
                {
                    this._templateHandlers = new TemplateHandlers();
                    this._templateHandlers.conn = base.conn;
                    this._templateHandlers.configService = base.configService;
                }
                return this._templateHandlers;
            }
        }
        private TemplateHandlers _templateHandlers = null;

    }
}
