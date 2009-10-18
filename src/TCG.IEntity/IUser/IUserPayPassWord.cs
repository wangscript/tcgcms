using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.IEntity
{
    /// <summary>
    /// 用户支付密码
    /// </summary>
    public interface IUserPayPassWord
    {
        string Issue { get; set; }        //问题      
        string Answers { get; set; }      //答案
    }
}
