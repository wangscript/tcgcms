using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Attribute
{
    /// <summary>
    /// 数据表单元属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TCGDataTableAttribute : System.Attribute
    {
        /// <summary>
        /// 是否是数据库中的表
        /// </summary>
        public bool IsDataTable
        {
            get
            {
                return this._isdatatable;
            }
            set
            {
                this._isdatatable = value;
            }
        }
        private bool _isdatatable = false;
    }
}