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

        private int _iid;
        private DateTime _daddtime = DateTime.Now;
        private string _vcpopname;
    }
}
