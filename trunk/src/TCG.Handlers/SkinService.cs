using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class SkinServiceEx : ObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SkinServiceEx()
        {

        }

        /// <summary>
        /// 提供系统的分类操作
        /// </summary>
        public ICategoriesHandlers categoriesHandlers
        {
            get
            {
                if (this._categorieshandlers == null)
                {
                    this._categorieshandlers = HandlerFactory.CreateCategoriesHandlers();
                }
                return this._categorieshandlers;
            }
        }
        private ICategoriesHandlers _categorieshandlers = null;


        /// <summary>
        /// 提供皮肤的操作
        /// </summary>
        public ITemplateHandlers templateHandlers
        {
            get
            {
                if (this._templateHandlers == null)
                {
                    this._templateHandlers = HandlerFactory.CreateTemplateHandlers();
                }
                return this._templateHandlers;
            }
        }
        private ITemplateHandlers _templateHandlers = null;

        /// <summary>
        /// 提供皮肤类别的操作
        /// </summary>
        public ISkinHandlers skinHandlers
        {
            get
            {
                if (this._skinhandlers == null)
                {
                    this._skinhandlers = HandlerFactory.CreateSkinHandlers();
                }
                return this._skinhandlers;
            }
        }
        private ISkinHandlers _skinhandlers = null;
    }
}
