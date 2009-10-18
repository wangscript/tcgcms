using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.IEntity
{
    /// <summary>
    /// 系统实体基本接口
    /// </summary>
    public interface ITCGEntity
    {
        string Id { get; set; }                 // 实体 编号
        DateTime CreateTime { get; set; }       // 创建时间
        DateTime LastChangeTime { get; set; }   //最后一次修改时间
        EntityState State { get; set; }         //用户状态
        string CreateIp { get; set; }           //创建者IP
    }
}