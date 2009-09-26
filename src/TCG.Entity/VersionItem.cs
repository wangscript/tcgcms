using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class VersionItem
    {
        /// <summary>
        /// 更新内容
        /// </summary>
        public string Text { get { return this._text; } set { this._text = value; } }
        /// <summary>
        /// 更新版本号
        /// </summary>
        public int Ver { get { return this._ver; } set { this._ver = value; } }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Date { get { return this._date; } set { this._date = value; } }
        /// <summary>
        /// 更新说明文档
        /// </summary>
        public string LogUrl { get { return this._logurl; } set { this._logurl = value; } }
        /// <summary>
        /// SQL语句文本个数
        /// </summary>
        public int Sqls { get { return this._sqls; } set { this._sqls = value; } }

        private string _text;
        private int _ver;
        private DateTime _date;
        private string _logurl;
        private int _sqls;
    }
}