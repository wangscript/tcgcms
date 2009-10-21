using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 用户俱乐部等级
    /// </summary>
    public enum UserClubLevel
    {
        /// <summary>
        /// 被限制用户
        /// </summary>
        Limit = 0,
        /// <summary>
        /// 普通用户
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 3,
    }
}