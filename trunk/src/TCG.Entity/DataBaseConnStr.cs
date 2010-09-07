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
    /// <summary>
    /// 文件数据库配置
    /// </summary>
    public class DataBaseConnStr
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