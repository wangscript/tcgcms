using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class ResourcsServiceEx : ObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResourcsServiceEx()
        {
        }

        /// <summary>
        /// 提供对资讯操作的方法
        /// </summary>
        public IResourceHandlers resourcesHandlers
        {
            get
            {
                if (this._resourceshandlers == null)
                {
                    this._resourceshandlers = HandlerFactory.CreateResourceHandlers();
                }
                return this._resourceshandlers;
            }
        }
        private IResourceHandlers _resourceshandlers;
    }
}
