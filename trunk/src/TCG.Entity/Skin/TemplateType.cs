using System;
using System.Collections.Generic;
using System.Text;


namespace TCG.Entity
{
    /// <summary>
    /// 模版类型
    /// </summary>
    public enum TemplateType : int
    {
        /// <summary>
        /// //单页模版
        /// </summary>
        SinglePageType = 0, 
        /// <summary>
        /// //详细页模版
        /// </summary>
        InfoType = 1,       
        /// <summary>
        /// //列表模版
        /// </summary>
        ListType = 2,  
        /// <summary>
        /// //原件模版
        /// </summary>
        OriginalType = 3,   
        /// <summary>
        /// //系统必须的页面
        /// </summary>
        SystemPage = 4,     
    }
}