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
    /// 文件分类实体 
    /// <log>by :sanyungui@yahoo.com.cn date: 2009-10-23 info:</log>
    /// </summary>
    public class FileCategories : EntityBase
    {
        /// <summary>
        /// 文件分类父类
        /// </summary>
        public int iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string vcFileName { get { return this._vcfilename; } set { this._vcfilename = value; } }
        /// <summary>
        /// 说明
        /// </summary>
        public string vcMeno { get { return this._vcmeno; } set { this._vcmeno = value; } }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get { return this._key; } set { this._key = value; } }
        /// <summary>
        /// 允许使用的最大空间
        /// </summary>
        public long MaxSpace { get { return this._maxspace; } set { this._maxspace = value; } }
        /// <summary>
        /// 剩余空间
        /// </summary>
        public long Space { get { return this._space; } set { this._space = value; } }
        
      
        private int _iid;
        private int _iparentid;
        private DateTime _dcreatedate;
        private string _vcfilename;
        private string _vcmeno;
        private string _key;
        private long _maxspace;
        private long _space;
    }
}
