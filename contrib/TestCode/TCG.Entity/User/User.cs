using System;
using System.Collections.Generic;
using System.Text;

using TCG.IEntity;
using TCG.Attribute;

namespace TCG.Entity
{
    /// <summary>
    /// 用户登陆实体
    /// </summary>
    [TCGDataTableAttribute(IsDataTable=true)]
    public class User : TCGEntity, IUser
    {
        /// <summary>
        /// 名称
        /// </summary>
        [TCGDataColumn(Type = "NVARCHAR", Length = 100)]
        public string Name { get { return this._name; } set { this._name = value; } } 
        /// <summary>
        /// 用户登陆名
        /// </summary>
        [TCGDataColumn(Type = "VARCHAR", Length = 32)]
        public string PassWord { get { return this._password; } set { this._password = value; } }

        private string _password;
        private string _name;
    }
}