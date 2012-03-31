using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class ResourceProperties : EntityBase
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertieName { get; set; }
        /// <summary>
        /// 属性内容
        /// </summary>
        public string PropertieValue { get; set; }
        /// <summary>
        /// 属性编号
        /// </summary>
        public int PropertieId { get; set; }
        /// <summary>
        /// 排序规则
        /// </summary>
        public int iOrder { get; set; }

    }
}
