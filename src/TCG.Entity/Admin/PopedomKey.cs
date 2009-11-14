using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    /// <summary>
    /// 后台管理权限项操作
    /// </summary>
    public enum PopedomKey
    {
        /// <summary>
        /// 系统管理 总操作项
        /// </summary>
        SystemManage = 1,
        /// <summary>
        /// 系统管理 帐号管理
        /// </summary>
        SystemManageAccManage = 2,
        /// <summary>
        /// 系统管理 帐号管理 个人帐号维护
        /// </summary>
        SystemManageAccManageMyAcc = 3,
    }
}
