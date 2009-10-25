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
        SinglePageType = 0, //单页模版
        InfoType = 1,       //详细页模版
        ListType = 2,       //列表模版
        OriginalType = 3,   //原件模版
        SystemPage = 4,
    }
}