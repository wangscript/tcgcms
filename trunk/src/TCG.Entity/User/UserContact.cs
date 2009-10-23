using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 用户联系方式
    /// </summary>
    public class UserContact
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserRealName { get { return this._UserRealName; } set { this._UserRealName = value; } }
        /// <summary>
        /// 省
        /// </summary>
        public int Province { get { return this._Province; } set { this._Province = value; } }
        /// <summary>
        /// 市
        /// </summary>
        public int City { get { return this._City; } set { this._City = value; } }
        /// <summary>
        /// 区
        /// </summary>
        public int District { get { return this._District; } set { this._District = value; } }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Fulladdress { get { return this._Fulladdress; } set { this._Fulladdress = value; } }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Postcode { get { return this._Postcode; } set { this._Postcode = value; } }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get { return this._Telephone; } set { this._Telephone = value; } }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get { return this._Mobile; } set { this._Mobile = value; } }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get { return this._email; } set { this._email = value; } }

        
        private string _UserRealName;
        private int _Province;
        private int _City;
        private int _District;
        private string _Fulladdress;
        private string _Postcode;
        private string _Telephone;
        private string _Mobile;
        private string _email;

    }
}