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
    /// 后台权限项实体
    /// </summary>
    public class Popedom
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int iID { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime dAddtime { get { return this._daddtime; } set { this._daddtime = value; } }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string vcPopName { get { return this._vcpopname; } set { this._vcpopname = value; } }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int iParentId { get { return this._iParentId; } set { this._iParentId = value; } }
        /// <summary>
        /// 是否为后台链接项
        /// </summary>
        public string cValid { get { return this._cValid; } set { this._cValid = value; } }

        private int _iid;
        private DateTime _daddtime = DateTime.Now;
        private string _vcpopname;
        private string _vcurl;
        private int _iParentId;
        private string _cValid;
    }
}