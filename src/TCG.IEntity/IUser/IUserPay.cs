using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.IEntity
{
    /// <summary>
    /// 用户资金
    /// </summary>
    public interface IUserPay :IUser
    {
        decimal FreeMoney { get; set; }                 //可用金额
        decimal FrezzMoney { get; set; }                //冻结金额
        decimal SumMoney { get; set; }                  //总金额
        decimal Points { get; set; }                    //用户积分
        IUserPayPassWord PayPassWord { get; set; }      //用户支付密码
    }
}