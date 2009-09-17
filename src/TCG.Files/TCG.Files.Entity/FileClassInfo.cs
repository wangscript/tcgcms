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
