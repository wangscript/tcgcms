using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 用户的等级
    /// </summary>
    public enum UserLevel
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// vip用户
        /// </summary>
        Vip = 1,
        /// <summary>
        /// 高级VIP
        /// </summary>
        SeniorVIP = 2,
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 3,

    }
}