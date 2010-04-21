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
    public class Template : EntityBase
    {
        /// <summary>
        /// 膜板块内容
        /// </summary>
        public string Content { get { return this._vccontent; } set { this._vccontent = value; } }
        
        /// <summary>
        /// 皮肤ID
        /// </summary>
        public string SkinId { get { return this._isiteid; } set { this._isiteid = value; } }
        /// <summary>
        /// 模板类型
        /// </summary>
        public TemplateType TemplateType { get { return this._itype; } set { this._itype = value; } }
        /// <summary>
        /// 模板父亲ID
        /// </summary>
        public string iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        public int iSystemType { get { return this._isystemtype; } set { this._isystemtype = value; } }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string vcTempName { get { return this._vctempname; } set { this._vctempname = value; } }
        /// <summary>
        /// 单页URL地址
        /// </summary>
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }

        private string _vccontent;
        private string _iid;
        private string _isiteid;
        private TemplateType _itype;
        private string _iparentid;
        private int _isystemtype;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _vctempname;
        private string _vcurl;
    }
}
