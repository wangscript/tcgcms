using System;
using System.Collections.Generic;
using System.Text;


namespace TCG.IEntity
{
    /// <summary>
    /// 会员登陆信息
    /// </summary>
    public interface IUser : ITCGEntity
    {
        string PassWord { get; set; }       //密码
        string Name { get; set; }               //名称
    }
}