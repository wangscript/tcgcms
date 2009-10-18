using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.IEntity
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum EntityState : int
    {
        /// <summary>
        /// 正常
        /// </summary>
        Default = 1,
        /// <summary>
        /// 未激活
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// 审核未通过
        /// </summary>
        Auditdidnotpass,
    }
}