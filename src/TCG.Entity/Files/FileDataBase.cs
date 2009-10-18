using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 文件数据库配置
    /// </summary>
    public class FileDataBase
    {
        /// <summary>
        /// 说明
        /// </summary>
        public string Text { get { return this._text; } set { this._text = value; } }
        /// <summary>
        /// 链接串
        /// </summary>
        public string Value { get { return this._value; } set { this._value = value; } }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string Service { get { return this._service; } set { this._service = value; } }
        /// <summary>
        /// 是否为主数据库
        /// </summary>
        public bool IsBaseDataBase { get { return this._isbasedatabase; } set { this._isbasedatabase = value; } }
        private string _text;
        private string _value;
        private string _service;
        private bool _isbasedatabase = false;
    }
}