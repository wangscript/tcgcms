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

namespace TCG.Files.Entity
{
    public class FileClassInfo
    {
        public int iId { get { return this._iid; } set { this._iid = value; } }
        public int iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        public string vcFileName { get { return this._vcfilename; } set { this._vcfilename = value; } }
        public string vcMeno { get { return this._vcmeno; } set { this._vcmeno = value; } }
      
        private int _iid;
        private int _iparentid;
        private DateTime _dcreatedate;
        private string _vcfilename;
        private string _vcmeno;
    }
}
