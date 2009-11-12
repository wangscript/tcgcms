using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class AdminRole
    {
        /// <summary>
        /// 权限组编号
        /// </summary>
        public int iID { get { return this._iid; } set { this._iid = value; } }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string vcRoleName { get { return this._vcrolename; } set { this._vcrolename = value; } }
        /// <summary>
        /// 权限组说明
        /// </summary>
        public string vcContent { get { return this._vccontent; } set { this._vccontent = value; } }
        /// <summary>
        /// 功能项权限 
        /// </summary>
        public Dictionary<int, Popedom> vcPopedom { get { return this._vcpopedom; } set { this._vcpopedom = value; } }
        /// <summary>
        /// 分类管理权限
        /// </summary>
        public string vcClassPopedom { get { return this._vcclasspopedom; } set { this._vcclasspopedom = value; } }
        /// <summary>
        /// 权限ID字符串
        /// </summary>
        public string PopedomStr { get { return this._popedomstr; } set { this._popedomstr = value; } }

        private int _iid;
        private DateTime _dupdatedate;
        private string _vcrolename;
        private string _vccontent;
        private Dictionary<int, Popedom> _vcpopedom;
        private string _popedomstr;
        private string _vcclasspopedom;
    }

}
