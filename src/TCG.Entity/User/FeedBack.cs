using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCG.Entity
{
    public class FeedBack : EntityBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        string Tel { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        string QQ { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        DateTime AddDate { get; set; }
        /// <summary>
        /// 添加人IP
        /// </summary>
        string Ip { get; set; }
    }
}
