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
    /// 值对 写入数据库字段为{Text:'',Value:''}
    /// </summary>
    public class Option
    {
        public Option(string text, string value)
        {
            this._Text = text;
            this._Value = value;
        }
        /// <summary>
        /// //内容   
        /// </summary>
        public string Text { get { return this._Text; } set { this._Text = value; } }   
        /// <summary>
        /// //值
        /// </summary>
        public string Value { get { return this._Value; } set { this._Value = value; } }      

        private string _Text { get; set; }        //内容     
        private string _Value { get; set; }      //值
    }
}
