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
    /// <summary>
     /// 用户登陆实体
    /// </summary>
    public class User 
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return this._name; } set { this._name = value; } } 
        /// <summary>
        /// 用户登陆名
        /// </summary>
        public string PassWord { get { return this._password; } set { this._password = value; } }
        /// <summary>
        ///  实体 编号
        /// </summary>
        public string Id { get { return this._id; } set { this._id = value; } }                 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get { return this._createtime; } set { this._createtime = value; } }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastLoginTime { get { return this._lastlogintime; } set { this._lastlogintime = value; } }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserState State { get { return this._entitystate; } set { this._entitystate = value; } }
        /// <summary>
        /// 用户资金
        /// </summary>
        public UserPay UserPay { get { return this._userpay; } set { this._userpay = value; } }
        /// <summary>
        /// 最后一次登陆的IP
        /// </summary>
        public string LastLoginIp { get { return this._lastloginip; } set { this._lastloginip = value; } }
        /// <summary>
        /// 用户联系方式
        /// </summary>
        public UserContact UserContact { get { return this._usercontact; } set { this._usercontact = value; } }
        /// <summary>
        /// 用户的等级
        /// </summary>
        public UserLevel UserLevel { get { return this._userlevel; } set { this._userlevel = value; } }
        /// <summary>
        /// 用户的俱乐部等级
        /// </summary>
        public UserClubLevel UserClubLevel { get { return this._userclublevel; } set { this._userclublevel = value; } }


        private string _id;
        private DateTime _createtime = DateTime.Now;
        private DateTime _lastlogintime = DateTime.Now;
        private UserState _entitystate = UserState.Inactive; 
        private string _lastloginip;
        private string _password;
        private string _name;
        private UserPay _userpay;
        private UserContact _usercontact;
        private UserLevel _userlevel = UserLevel.Ordinary;
        private UserClubLevel _userclublevel = UserClubLevel.Ordinary;

    }
}