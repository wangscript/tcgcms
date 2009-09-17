using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TCG.Manage.Entity
{
    public class Admin
    {
        public string vcAdminName { set { this._vcadminname = value; } get { return this._vcadminname; } }
        public string vcNickName { set { this._vcnickname = value; } get { return this._vcnickname; } }
        public string vcPassword { set { this._vcpassword = value; } get { return this._vcpassword; } }
        public int iRole { set { this._irole = value; } get { return this._irole; } }
        public string vcPopedom { set { this._vcpopedom = value; } get { return this._vcpopedom; } }
        public string vcClassPopedom { set { this._vcclasspopedom = value; } get { return this._vcclasspopedom; } }
        public string cLock { set { this._clock = value; } get { return this._clock; } }
        public DateTime dAddDate { set { this._dadddate = value; } get { return this._dadddate; } }
        public DateTime dUpdateDate { set { this._dupdatedate = value; } get { return this._dupdatedate; } }
        public DateTime dLoginDate { set { this._dlogindate = value; } get { return this._dlogindate; } }
        public DateTime dLastLoginDate { set { this._dlastlogindate = value; } get { return this._dlastlogindate; } }
        public int iLoginCount { set { this._ilogincount = value; } get { return this._ilogincount; } }
        public string vcLastLoginIp { set { this._vclastloginip = value; } get { return this._vclastloginip; } }
        public string cIsOnline { set { this._cisonline = value; } get { return this._cisonline; } }
        public DataTable PopedomUrls { set { this._popedomurls = value; } get { return this._popedomurls; } }
        public string vcRoleName { set { this._rolename = value; } get { return this._rolename; } }

        private string _cisonline = "";
        private string _vclastloginip = "";
        private int _ilogincount = 0;
        private DateTime _dlastlogindate;
        private DateTime _dlogindate;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _clock = "Y";
        private string _vcclasspopedom = "";
        private string _vcpopedom = "";
        private int _irole = 0;
        private string _vcpassword = "";
        private string _vcnickname = "";
        private string _vcadminname = "";
        private DataTable _popedomurls = null;
        private string _rolename = "";
    }
}
