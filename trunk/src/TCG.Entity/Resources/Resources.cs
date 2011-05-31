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
    /// 资源信息
    /// </summary>
    public class Resources : EntityBase
    {
        /// <summary>
        /// 资源分类信息
        /// </summary>
        public Categories Categorie { get { return this._iClassID; } set { this._iClassID = value; } }
        /// <summary>
        /// 资源标题
        /// </summary>
        public string vcTitle { get { return this._vcTitle; } set { this._vcTitle = value; } }
        /// <summary>
        /// 资源跳转地址
        /// </summary>
        public string vcUrl { get { return this._vcUrl; } set { this._vcUrl = value; } }
        /// <summary>
        /// 资源内容
        /// </summary>
        public string vcContent { get { return this._vcContent; } set { this._vcContent = value; } }
        /// <summary>
        /// 资源作者
        /// </summary>
        public string vcAuthor { get { return this._vcAuthor; } set { this._vcAuthor = value; } }
        /// <summary>
        /// 资源访问量
        /// </summary>
        public int iCount { get { return this._iCount; } set { this._iCount = value; } }
        /// <summary>
        /// 资源关键字
        /// </summary>
        public string vcKeyWord { get { return this._vcKeyWord; } set { this._vcKeyWord = value; } }
        /// <summary>
        /// 资源编辑者
        /// </summary>
        public string vcEditor { get { return this._vcEditor; } set { this._vcEditor = value; } }
        /// <summary>
        /// 资源是否生成
        /// </summary>
        public string cCreated { get { return this._cCreated; } set { this._cCreated = value; } }
        /// <summary>
        /// 资源发布者
        /// </summary>
        public string cPostByUser { get { return this._cPostByUser; } set { this._cPostByUser = value; } }
        /// <summary>
        /// 资源小图，用户列表
        /// </summary>
        public string vcSmallImg { get { return this._vcSmallImg; } set { this._vcSmallImg = value; } }
        /// <summary>
        /// 资源大图，用于图片展示
        /// </summary>
        public string vcBigImg { get { return this._vcBigImg; } set { this._vcBigImg = value; } }
        /// <summary>
        /// 资源简介
        /// </summary>
        public string vcShortContent { get { return this._vcShortContent; } set { this._vcShortContent = value; } }
        /// <summary>
        /// 资源特性
        /// </summary>
        public string vcSpeciality { get { return this._vcSpeciality; } set { this._vcSpeciality = value; } }
        /// <summary>
        /// 资源审核状态
        /// </summary>
        public string cChecked { get { return this._cChecked; } set { this._cChecked = value; } }
        /// <summary>
        /// 资源是否删除
        /// </summary>
        public string cDel { get { return this._cDel; } set { this._cDel = value; } }
        /// <summary>
        /// 资源路径
        /// </summary>
        public string vcFilePath { get { return this._vcFilePath; } set { this._vcFilePath = value; } }
        /// <summary>
        /// 资源添加时间
        /// </summary>
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        /// <summary>
        /// 资源修改时间
        /// </summary>
        public DateTime dUpDateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// 资源标题颜色
        /// </summary>
        public string vcTitleColor { get { return this._vctitlecolor; } set { this._vctitlecolor = value; } }
        /// <summary>
        /// 资源标题是否加粗
        /// </summary>
        public string cStrong { get { return this._cstrong; } set { this._cstrong = value; } }
        /// <summary>
        /// 抓取URL
        /// </summary>
        public string SheifUrl { get { return this._sheifurl; } set { this._sheifurl = value; } }

        /// <summary>
        /// 属性分类ID
        /// </summary>
        public int PropertiesCategorieId { get; set; }

        public string GetUrl()
        {
            return string.IsNullOrEmpty(this.vcUrl) ? this.vcFilePath : this.vcUrl;
        }
         

        private Categories _iClassID = new Categories();
        private string _vcTitle = string.Empty;
        private string _vcUrl = string.Empty;
        private string _vcContent = string.Empty;
        private string _vcAuthor = string.Empty;
        private int _iCount = 0;
        private string _vcKeyWord = string.Empty;
        private string _vcEditor = string.Empty;
        private string _cCreated = string.Empty;
        private string _cPostByUser = string.Empty;
        private string _vcSmallImg = string.Empty;
        private string _vcBigImg = string.Empty;
        private string _vcShortContent = string.Empty;
        private string _vcSpeciality = string.Empty;
        private string _cChecked = string.Empty;
        private string _cDel = "N";
        private string _vcFilePath = string.Empty;
        private DateTime _dadddate = DateTime.Now;
        private DateTime _dupdatedate;
        private string _vctitlecolor;
        private string _cstrong;
        private string _sheifurl;
    }
}
