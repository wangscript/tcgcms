using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class SheifCategorieConfig
    {
        private string _sheifsourceid;
        private string _localcategorieid;
        private DateTime _resourcecreatedatetime;

        public string SheifSourceId { set { _sheifsourceid = value; } get { return _sheifsourceid; } }
        public string LocalCategorieId { set { _localcategorieid = value; } get { return _localcategorieid; } }
        public DateTime ResourceCreateDateTime { set { _resourcecreatedatetime = value; } get { return _resourcecreatedatetime; } }
    }
}
