using System;
using System.Collections.Generic;
using System.Text;

using TCG.IEntity;
using TCG.Attribute;

namespace TCG.Entity
{
    /// <summary>
    /// 系统实体
    /// </summary>
    public class TCGEntity : ITCGEntity
    {
        /// <summary>
        ///  实体 编号
        /// </summary>
        [TCGDataColumn(PrimaryKey=true,Type="VARCHAR",Length=16)]
        public string Id { get { return this._id; } set { this._id = value; } }                 
        /// <summary>
        /// 创建时间
        /// </summary>
        [TCGDataColumn(Type = "DATETIME", Length = 8)]
        public DateTime CreateTime { get { return this._createtime; } set { this._createtime = value; } }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [TCGDataColumn(Type = "DATETIME", Length = 8)]
        public DateTime LastChangeTime { get { return this._lastchangetime; } set { this._lastchangetime = value; } }
        /// <summary>
        /// 用户状态
        /// </summary>
        [TCGDataColumn(Type = "INT", Length = 4)]
        public EntityState State { get { return this._entitystate; } set { this._entitystate = value; } }
        /// <summary>
        /// 创建者IP
        /// </summary>
        [TCGDataColumn(Type = "VARCHAR", Length = 32)]
        public string CreateIp { get { return this._createip; } set { this._createip = value; } }

        private string _id;
        private DateTime _createtime;
        private DateTime _lastchangetime;
        private EntityState _entitystate;
        private string _createip;
        
    }
}