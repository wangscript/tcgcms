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
    public class Skin : EntityBase
    {
        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string Name { get { return this._name; } set { this._name = value; } }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Pic { get { return this._pic; } set { this._pic = value; } }
        /// <summary>
        /// 皮肤说明
        /// </summary>
        public string WebDescription { get { return this._info; } set { this._info = value; } }
        /// <summary>
        /// 皮肤文件夹
        /// </summary>
        public string Filename { get { return this._filename; } set { this._filename = value; } }
        /// <summary>
        /// 首页地址
        /// </summary>
        public string IndexPage { get { return this._indexpage; } set { this._indexpage = value; } }
        /// <summary>
        /// 关键字
        /// </summary>
        public string WebKeyWords { get { return this._webkeywords; } set { this._webkeywords = value; } }

        private string _name = string.Empty;
        private string _pic = string.Empty;
        private string _info = string.Empty;
        private string _filename = string.Empty;
        private string _indexpage = string.Empty;
        private string _webkeywords = string.Empty;
    }
}