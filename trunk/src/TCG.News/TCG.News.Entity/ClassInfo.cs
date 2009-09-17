using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace TCG.News.Entity
{
    public class ClassInfo
    {
        public int iId { get { return this._iid; } set { this._iid = value; } }
        public int iParent { get { return this._iparent; } set { this._iparent = value; } }
        public int iTemplate { get { return this._itemplate; } set { this._itemplate = value; } }
        public int iListTemplate { get { return this._ilisttemplate; } set { this._ilisttemplate = value; } }
        public int iOrder { get { return this._iorder; } set { this._iorder = value; } }
        public DateTime dUpdateDate { get { return this._dupdatedate; } set { this._dupdatedate = value; } }
        public string vcClassName { get { return this._vcclassname; } set { this._vcclassname = value; } }
        public string vcName { get { return this._vcname; } set { this._vcname = value; } }
        public string vcDirectory { get { return this._vcdirectory; } set { this._vcdirectory = value; } }
        public string vcUrl { get { return this._vcurl; } set { this._vcurl = value; } }

        private int _iid;
        private int _iparent;
        private int _itemplate;
        private int _ilisttemplate;
        private int _iorder;
        private DateTime _dupdatedate;
        private string _vcclassname;
        private string _vcname;
        private string _vcdirectory;
        private string _vcurl;
    }
}