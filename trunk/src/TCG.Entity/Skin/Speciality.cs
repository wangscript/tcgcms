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
    /// 资源特性的实体
    /// </summary>
    public class Speciality
    {
        /// <summary>
        /// 特性编号
        /// </summary>
        public int iId { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// 皮肤ID
        /// </summary>
        public int iSiteId { get { return this._isiteid; } set { this._isiteid = value; } }
        /// <summary>
        /// 父亲的编号
        /// </summary>
        public int iParent { get { return this._iparent; } set { this._iparent = value; } }
        /// <summary>
        /// 更新的时间
        /// </summary>
        public DateTime dUpDateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// 特性标题
        /// </summary>
        public string vcTitle { get { return this._vctitle; } set { this._vctitle = value; } }
        /// <summary>
        /// 特性说明
        /// </summary>
        public string vcExplain { get { return this._vcexplain; } set { this._vcexplain = value; } }

        private int _iid;
        private int _isiteid;
        private int _iparent;
        private DateTime _dupdatedate;
        private string _vctitle;
        private string _vcexplain;
    }
}
