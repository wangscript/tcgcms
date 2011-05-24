using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class SkinService : ObjectHandlersBase
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
                }
                return this._categorieshandlers;
            }
        }
        private CategoriesHandlers _categorieshandlers = null;


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
                }
                return this._skinhandlers;
            }
        }
        private SkinHandlers _skinhandlers = null;


        /// <summary>
        /// 属性操作类
        /// </summary>
        public PropertiesHandlers propertiesHandlers
        {
            get
            {
                if (this._propertieshandlers == null)
                {
                    this._propertieshandlers = new PropertiesHandlers();
                }
                return this._propertieshandlers;
            }
        }
        private PropertiesHandlers _propertieshandlers = null;

        /// <summary>
        /// 特性操作类
        /// </summary>
        public SpecialityHandlers specialityHandlers
        {
            get
            {
                if (this._specialityhandlers == null)
                {
                    this._specialityhandlers = new SpecialityHandlers();
                }
                return this._specialityhandlers;
            }
        }
        private SpecialityHandlers _specialityhandlers = null;
    }
}
