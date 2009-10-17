
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

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