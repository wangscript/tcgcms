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
    /// 标签的页面暂存内容，存储在标签检测中的零时变量
    /// </summary>
    public class TCGTagPagerInfo
    {
        /// <summary>
        /// 文章是否需要分页
        /// </summary>
        public bool NeedPager { get { return this._needpager; } set { this._needpager = value; } }
        /// <summary>
        /// 分页步骤
        /// </summary>
        public int PageSep { get { return this._pagesep; } set { this._pagesep = value; } }
        /// <summary>
        /// 是否有检测到标签
        /// </summary>
        public bool Read { get { return this._read; } set { this._read = value; } }
        /// <summary>
        /// 分页时，当前页
        /// </summary>
        public int Page { get { return this._page; } set { this._page = value; } }
        /// <summary>
        /// 分页时，总页数
        /// </summary>
        public int PageCount { get { return this._pgercount; } set { this._pgercount = value; } }
        /// <summary>
        /// 分页时，当前页 修正
        /// </summary>
        public int curPage { get { return this._curpage; } set { this._curpage = value; } }
        /// <summary>
        /// 分页时，总记录数
        /// </summary>
        public int TopicCount { get { return this._topiccount; } set { this._topiccount = value; } }
        /// <summary>
        /// 页面的标题
        /// </summary>
        public string PageTitle { get { return this._pagetitle; } set { this._pagetitle = value; } }
        /// <summary>
        /// 页面的脚本和样式 
        /// </summary>
        public string ScriptCss { get { return this._scriptcss; } set { this._scriptcss = value; } }
        /// <summary>
        /// 是否声称所有页面
        /// </summary>
        public bool DoAllPage { get { return this._doallpage; } set { this._doallpage = value; } }
        /// <summary>
        /// 生成文件列表
        /// </summary>
        public string CreatePagesNotic { get { return this._createpagesnotic; } set { this._createpagesnotic = value; } }

        private int _curpage = 1;
        private int _topiccount = 0;
        private int _page = 1;
        private int _pgercount = 0;
        private int _pagesep = 0;
        private bool _needpager = false;
        private bool _read = true;
        private string _pagetitle = string.Empty;
        private string _scriptcss = string.Empty;
        private bool _doallpage = true; // 是否生成所有页面
        private string _createpagesnotic = string.Empty;
    }
}
