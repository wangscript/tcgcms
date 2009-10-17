using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Attribute
{
    /// <summary>
    /// 数据行，字段属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class TCGDataColumnAttribute : System.Attribute
    {
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        private int _length = 50;

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        private string _type = "NVARCHAR";

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool PrimaryKey
        {
            get
            {
                return this._primarykey;
            }
            set
            {
                this._primarykey = value;
            }
        }
        private bool _primarykey = false;

        /// <summary>
        /// 是否为字符串
        /// </summary>
        public bool String
        {
            get
            {
                return this._string;
            }
            set
            {
                this._string = value;
            }
        }
        private bool _string = true;

    }
}
