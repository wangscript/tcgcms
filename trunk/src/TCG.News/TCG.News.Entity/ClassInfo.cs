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
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace TCG.News.Entity
{
    public class ClassInfo
    {
        public int iId { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// 父类ID
        /// </summary>
        public int iParent { get { return this._iparent; } set { this._iparent = value; } }
        /// <summary>
        /// 文章模版
        /// </summary>
        public int iTemplate { get { return this._itemplate; } set { this._itemplate = value; } }
        /// <summary>
        /// 列表模版
        /// </summary>
        public int iListTemplate { get { return this._ilisttemplate; } set { this._ilisttemplate = value; } }
        /// <summary>
        /// 排序
        /// </summary>
        public int iOrder { get { return this._iorder; } set { this._iorder = value; } }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string vcClassName { get { return this._vcclassname; } set { this._vcclassname = value; } }
        /// <summary>
        /// 别名
        /// </summary>
        public string vcName { get { return this._vcname; } set { this._vcname = value; } }
        /// <summary>
        /// 磁盘路径
        /// </summary>
        public string vcDirectory { get { return this._vcdirectory; } set { this._vcdirectory = value; } }
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }
        /// <summary>
        /// 是否显示
        /// </summary>
        public string cVisible { get { return this._cvisible; } set { this._cvisible = value; } }

        private int _iid;
        private int _iparent;
        private int _itemplate;
        private int _ilisttemplate;
        private int _iorder;
        private DateTime _dupdatedate;
        private string _vcclassname;
        private string _vcname;
        private string _vcdirectory;
        private string _vcurl;
        private string _cvisible;
    }
}