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
        public string UserName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 添加人IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 皮肤ID
        /// </summary>
        public string SkinId { get; set; }
        /// <summary>
        /// 留言主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 客户邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
