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
    /// <summary>
    /// 提供皮肤操作的一系列方法，模板操作，分类操作，特性操作，皮肤分类操作等
    /// </summary>
    public class SkinService : ManageObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SkinService()
        {

        }

        /// <summary>
        /// 提供系统的分类操作
        /// </summary>
        public CategoriesHandlers categoriesHandlers
        {
            get
            {
                if (this._categorieshandlers == null)
                {
                    this._categorieshandlers = new CategoriesHandlers();
                    this._categorieshandlers.configService = base.configService;
                    this._categorieshandlers.conn = base.conn;
                    this._categorieshandlers.handlerService = base.handlerService;
                }
                return this._categorieshandlers;
            }
        }
        private CategoriesHandlers _categorieshandlers = null;

        /// <summary>
        /// 提供系统资源特性的操作
        /// </summary>
        public SpecialityHandlers specialityHandlers
        {
            get
            {
                if (this._specialityhandlers == null)
                {
                    this._specialityhandlers = new SpecialityHandlers();
                    this._specialityhandlers.configService = base.configService;
                    this._specialityhandlers.conn = base.conn;
                    this._specialityhandlers.handlerService = base.handlerService;
                }
                return this._specialityhandlers;
            }
        }
        private SpecialityHandlers _specialityhandlers = null;

        /// <summary>
        /// 提供皮肤的操作
        /// </summary>
        public TemplateHandlers templateHandlers
        {
            get
            {
                if (this._templateHandlers == null)
                {
                    this._templateHandlers = new TemplateHandlers();
                    this._templateHandlers.configService = base.configService;
                    this._templateHandlers.conn = base.conn;
                    this._templateHandlers.handlerService = base.handlerService;
                }
                return this._templateHandlers;
            }
        }
        private TemplateHandlers _templateHandlers = null;

        /// <summary>
        /// 提供皮肤类别的操作
        /// </summary>
        public SkinHandlers skinHandlers
        {
            get
            {
                if (this._skinhandlers == null)
                {
                    this._skinhandlers = new SkinHandlers();
                    this._skinhandlers.configService = base.configService;
                    this._skinhandlers.conn = base.conn;
                    this._skinhandlers.handlerService = base.handlerService;
                }
                return this._skinhandlers;
            }
        }
        private SkinHandlers _skinhandlers = null;


    }
}
