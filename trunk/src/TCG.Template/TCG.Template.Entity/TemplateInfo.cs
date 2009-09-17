using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Template.Entity
{
    public class TemplateInfo
    {
        public string vcContent { get { return this._vccontent; } set { this._vccontent = value; } }
        public int iId { get { return this._iid; } set { this._iid = value; } }
        public int iSiteId { get { return this._isiteid; } set { this._isiteid = value; } }
        public int iType { get { return this._itype; } set { this._itype = value; } }
        public int iParentId { get { return this._iparentid; } set { this._iparentid = value; } }
        public int iSystemType { get { return this._isystemtype; } set { this._isystemtype = value; } }
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        public DateTime dAddDate { get { return this._dadddate; } set { this._dadddate = value; } }
        public string vcTempName { get { return this._vctempname; } set { this._vctempname = value; } }
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }

        private string _vccontent;
        private int _iid;
        private int _isiteid;
        private int _itype;
        private int _iparentid;
        private int _isystemtype;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _vctempname;
        private string _vcurl;
    }
}
