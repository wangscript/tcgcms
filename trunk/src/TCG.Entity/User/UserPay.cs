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
    /// 用户资金表
    /// </summary>
    public class UserPay
    {
        /// <summary>
        /// //可用金额
        /// </summary>
        public decimal FreeMoney { get { return this._FreeMoney; } set { this._FreeMoney = value; } }                 
        /// <summary>
        ///  //冻结金额
        /// </summary>
        public decimal FrezzMoney { get { return this._FrezzMoney; } set { this._FrezzMoney = value; } }               
        /// <summary>
        ///  //总金额
        /// </summary>
        public decimal SumMoney { get { return this._SumMoney; } set { this._SumMoney = value; } }                 
        /// <summary>
        ///  //用户积分
        /// </summary>
        public decimal Points { get { return this._Points; } set { this._Points = value; } }                   
        /// <summary>
        /// //用户支付密码
        /// </summary>
        public Option PayPassWord { get { return this._PayPassWord; } set { this._PayPassWord = value; } }

        private decimal _FreeMoney = 0;                     //可用金额
        private decimal _FrezzMoney = 0;                    //冻结金额
        private decimal _SumMoney = 0;                      //总金额
        private decimal _Points = 0;                        //用户积分
        private Option _PayPassWord = null;        //用户支付密码
    }
}
