﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Entity
{
    public class EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get { return this._iid; } set { this._iid = value; } }
        private string _iid;
    }
}