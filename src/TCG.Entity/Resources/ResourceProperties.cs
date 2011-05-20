using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 属性对应的分类属性编号
        /// </summary>
        public int CategoriePropertieId { get; set; }

    }
}
