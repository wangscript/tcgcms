
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TCG.Data
{
    public class PageSearchItem
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tableName { get { return this._tablename; } set { this._tablename = value; } }
        /// <summary>
        /// 需要返回显示的字段集
        /// </summary>
        public ArrayList arrShowField { get { return this._arrshowfield; } set { this._arrshowfield = value; } }
        /// <summary>
        /// 查询条件字段集
        /// </summary>
        public string strCondition { get { return this._strcondition; } set { this._strcondition = value; } }
        /// <summary>
        /// 排序字段集
        /// </summary>
        public ArrayList arrSortField { get { return this._arrsortfield; } set { this._arrsortfield = value; } }
        /// <summary>
        /// 每页显示的记录个数
        /// </summary>
        public int pageSize { get { return this._pagesize; } set { this._pagesize = value; } }
        /// <summary>
        /// 要显示的页面编号
        /// </summary>
        public int page { get { return this._page; } set { this._page = value; } }

        private string _tablename;
        private ArrayList _arrshowfield;
        private string _strcondition;
        private ArrayList _arrsortfield;
        private int _pagesize;
        private int _page;
    }
}
