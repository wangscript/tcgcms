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
using System.Collections.Generic;
using System.Text;

namespace TCG.News.Entity
{
    public class NewsFromInfo
    {
        public int iId { get { return this._iId; } set { this._iId = value; } }
        public DateTime dUpdateDate { get { return this._dUpdateDate; } set { this._dUpdateDate = value; } }
        public string vcTitle { get { return this._vcTitle; } set { this._vcTitle = value; } }
        public string vcUrl { get { return this._vcUrl; } set { this._vcUrl = value; } }

        private int _iId = 0;
        private DateTime _dUpdateDate;
        private string _vcTitle = string.Empty;
        private string _vcUrl = string.Empty;
    }
}
