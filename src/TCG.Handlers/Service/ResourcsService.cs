using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Handlers
{
    public class ResourcsService : ObjectHandlersBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResourcsService()
        {
        }

        /// <summary>
        /// 提供对资讯操作的方法
        /// </summary>
        public ResourcesHandlers resourcesHandlers
        {
            get
            {
                if (this._resourceshandlers == null)
                {
                    this._resourceshandlers = new ResourcesHandlers();
                }
                return this._resourceshandlers;
            }
        }
        private ResourcesHandlers _resourceshandlers;
    }
}
