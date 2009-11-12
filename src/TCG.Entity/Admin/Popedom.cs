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