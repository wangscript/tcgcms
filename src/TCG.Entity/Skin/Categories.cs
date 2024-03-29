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


namespace TCG.Entity
{
    /// <summary>
    /// 资源分类
    /// </summary>
    public class Categories : EntityBase
    {
        /// <summary>
        /// 父类ID
        /// </summary>
        public string Parent { get { return this._iparent; } set { this._iparent = value; } }
        /// <summary>
        /// 文章模版
        /// </summary>
        public Template ResourceTemplate { get { return this._itemplate; } set { this._itemplate = value; } }
        /// <summary>
        /// 列表模版
        /// </summary>
        public Template ResourceListTemplate { get { return this._ilisttemplate; } set { this._ilisttemplate = value; } }
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
        /// 分类下详细页生成地址
        /// </summary>
        public string vcDirectory { get { return this._vcdirectory; } set { this._vcdirectory = value; } }
        /// <summary>
        /// 分类列表地址
        /// </summary>
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }
        /// <summary>
        /// 是否显示
        /// </summary>
        public string cVisible { get { return this._cvisible; } set { this._cvisible = value; } }
        /// <summary>
        /// 指定数据库连接
        /// </summary>
        public string DataBaseService { get { return this._dbService; } set { this._dbService = value; } }
        /// <summary>
        ///  所属皮肤的ID
        /// </summary>
        public Skin SkinInfo { get { return this._skin; } set { this._skin = value; } }
        /// <summary>
        /// 是否为单页
        /// </summary>
        public string IsSinglePage { get { return this._sssinglepage; } set { this._sssinglepage = value; } }
        /// <summary>
        /// 分类展示图片
        /// </summary>
        public string vcPic { get { return this._pic; } set { this._pic = value; } }
        /// <summary>
        /// 资源特性
        /// </summary>
        public string vcSpeciality { get { return this._vcSpeciality; } set { this._vcSpeciality = value; } }

        private string _iparent = string.Empty;
        private Template _itemplate;
        private Template _ilisttemplate;
        private int _iorder;
        private DateTime _dupdatedate;
        private string _vcclassname = string.Empty;
        private string _vcname = string.Empty;
        private string _vcdirectory = string.Empty;
        private string _vcurl = string.Empty;
        private string _cvisible = string.Empty;
        private string _dbService = null;
        private string _skinid = string.Empty;
        private string _sssinglepage = string.Empty;
        private Skin _skin;
        private string _pic = string.Empty;
        private string _vcSpeciality = string.Empty;
    }
}