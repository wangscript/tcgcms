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
    public class NewsInfo
    {
        public int iId { get { return this._iId; } set { this._iId = value; } }
        public ClassInfo ClassInfo { get { return this._iClassID; } set { this._iClassID = value; } }
        public string vcTitle { get { return this._vcTitle; } set { this._vcTitle = value; } }
        public string vcUrl { get { return this._vcUrl; } set { this._vcUrl = value; } }
        public string vcContent { get { return this._vcContent; } set { this._vcContent = value; } }
        public string vcAuthor { get { return this._vcAuthor; } set { this._vcAuthor = value; } }
        public NewsFromInfo FromInfo { get { return this._NewsFromInfo; } set { this._NewsFromInfo = value; } }
        public int iCount { get { return this._iCount; } set { this._iCount = value; } }
        public string vcKeyWord { get { return this._vcKeyWord; } set { this._vcKeyWord = value; } }
        public string vcEditor { get { return this._vcEditor; } set { this._vcEditor = value; } }
        public string cCreated { get { return this._cCreated; } set { this._cCreated = value; } }
        public string cPostByUser { get { return this._cPostByUser; } set { this._cPostByUser = value; } }
        public string vcSmallImg { get { return this._vcSmallImg; } set { this._vcSmallImg = value; } }
        public string vcBigImg { get { return this._vcBigImg; } set { this._vcBigImg = value; } }
        public string vcShortContent { get { return this._vcShortContent; } set { this._vcShortContent = value; } }
        public string vcSpeciality { get { return this._vcSpeciality; } set { this._vcSpeciality = value; } }
        public string cChecked { get { return this._cChecked; } set { this._cChecked = value; } }
        public string cDel { get { return this._cDel; } set { this._cDel = value; } }
        public string vcFilePath { get { return this._vcFilePath; } set { this._vcFilePath = value; } }
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        public DateTime dUpDateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        public string vcTitleColor { get { return this._vctitlecolor; } set { this._vctitlecolor = value; } }
        public string cStrong { get { return this._cstrong; } set { this._cstrong = value; } }
          

        private int _iId = 0;
        private ClassInfo _iClassID = new ClassInfo();
        private string _vcTitle = string.Empty;
        private string _vcUrl = string.Empty;
        private string _vcContent = string.Empty;
        private string _vcAuthor = string.Empty;
        private NewsFromInfo _NewsFromInfo = new NewsFromInfo();
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
        private string _cDel = string.Empty;
        private string _vcFilePath = string.Empty;
        private DateTime _dadddate;
        private DateTime _dupdatedate;
        private string _vctitlecolor;
        private string _cstrong;
    }
}
