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

namespace TCG.Files.Entity
{
    public class FileInfos
    {
        public int iClassId { get { return this._iclassid; } set { this._iclassid = value; } }
        public int iSize { get { return this._isize; } set { this._isize = value; } }
        public int iDowns { get { return this._idowns; } set { this._idowns = value; } }
        public int iRequest { get { return this._irequest; } set { this._irequest = value; } }
        public DateTime dCreateDate { get { return this._dcreatedate; } set { this._dcreatedate = value; } }
        public long iID { get { return this._iid; } set { this._iid = value; } }
        public string vcType { get { return this._vctype; } set { this._vctype = value; } }
        public string vcIP { get { return this._vcip; } set { this._vcip = value; } }
        public string vcFileName { get { return this._vcfilename; } set { this._vcfilename = value; } }


        private int _iclassid;
        private int _isize;
        private int _idowns;
        private int _irequest;
        private DateTime _dcreatedate;
        private long _iid;
        private string _vctype;
        private string _vcip;
        private string _vcfilename;
    }

}
