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
using System.Data;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 后台管理员实体
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string vcAdminName { set { this._vcadminname = value; } get { return this._vcadminname; } }
        /// <summary>
        /// 管理员昵称
        /// </summary>
        public string vcNickName { set { this._vcnickname = value; } get { return this._vcnickname; } }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string vcPassword { set { this._vcpassword = value; } get { return this._vcpassword; } }
        /// <summary>
        /// 管理员分组
        /// </summary>
        public AdminRole iRole { set { this._irole = value; } get { return this._irole; } }
        /// <summary>
        /// 管理员特定权限
        /// </summary>
        public Dictionary<int, Popedom> vcPopedom { set { this._vcpopedom = value; } get { return this._vcpopedom; } }
        /// <summary>
        /// 管理员分类权限
        /// </summary>
        public string vcClassPopedom { set { this._vcclasspopedom = value; } get { return this._vcclasspopedom; } }
        /// <summary>
        /// 是否锁定 ，为Y的时候表示锁定
        /// </summary>
        public string cLock { set { this._clock = value; } get { return this._clock; } }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime dAddDate { set { this._dadddate = value; } get { return this._dadddate; } }
        /// <summary>
        /// 最后一次更新的时间
        /// </summary>
        public DateTime dUpdateDate { set { this._dupdatedate = value; } get { return this._dupdatedate; } }
        /// <summary>
        /// 最后一次登陆的时间
        /// </summary>
        public DateTime dLoginDate { set { this._dlogindate = value; } get { return this._dlogindate; } }
        /// <summary>
        /// 最后一次登陆的时间
        /// </summary>
        public DateTime dLastLoginDate { set { this._dlastlogindate = value; } get { return this._dlastlogindate; } }
        /// <summary>
        /// 登陆次数
        /// </summary>
        public int iLoginCount { set { this._ilogincount = value; } get { return this._ilogincount; } }
        /// <summary>
        /// 最后一次登陆的IP
        /// </summary>
        public string vcLastLoginIp { set { this._vclastloginip = value; } get { return this._vclastloginip; } }
        /// <summary>
        /// 是否在线
        /// </summary>
        public string cIsOnline { set { this._cisonline = value; } get { return this._cisonline; } }
        
        /// <summary>
        /// 是否被删除
        /// </summary>
        public string cIsDel { set { this._isdel = value; } get { return this._isdel; } }
        /// <summary>
        /// 权限ID字符串
        /// </summary>
        public string PopedomStr { get { return this._popedomstr; } set { this._popedomstr = value; } }

        private string _cisonline = "";
        private string _vclastloginip = "";
        private int _ilogincount = 0;
        private DateTime _dlastlogindate;
        private DateTime _dlogindate;
        private DateTime _dupdatedate;
        private DateTime _dadddate;
        private string _clock = "Y";
        private string _vcclasspopedom = "";
        private Dictionary<int, Popedom> _vcpopedom = null;

        private AdminRole _irole = null;
        private string _vcpassword = "";
        private string _vcnickname = "";
        private string _vcadminname = "";
        private string _isdel = "N";
        private string _popedomstr;
    }
}
