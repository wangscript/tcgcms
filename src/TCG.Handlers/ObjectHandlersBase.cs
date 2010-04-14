/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法基类
    /// </summary>
    public class ObjectHandlersBase
    {
        /// <summary>
        /// 设置数据库链接
        /// </summary>
        public Connection conn
        {
            set
            {
                this._conn = value;
            }
            get
            {
                return this._conn;
            }
        }

        private Connection _conn = null;
        /// <summary>
        /// 获得配置信息支持
        /// </summary>
        public ConfigService configService
        {
            set
            {
                this._configservice = value;
            }
            get
            {
                return this._configservice;
            }
        }
        private ConfigService _configservice = null;

        /// <summary>
        /// 提供系统操作方法的服务
        /// </summary>
        public HandlerService handlerService
        {
            set
            {
                this._handlerservice = value;
            }
            get
            {
                return this._handlerservice;
            }
        }
        private HandlerService _handlerservice = null;

        /// <summary>
        /// 从记录集中返回实体
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string,EntityBase> GetEntitysObjectFromTable(DataTable dt, Type type)
        {
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            Dictionary<string ,EntityBase> list = new Dictionary<string,EntityBase>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow Row = dt.Rows[i];
                EntityBase entity = this.GetEntityObjectFromRow(Row,type);
                list.Add(entity.Id,entity);
            }
            return list;
        }

        public string GetJsEntitys(Dictionary<string, EntityBase> entitys, Type type)
        {

            if (type == null) return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("var _" + type.ToString().Split('.')[2] + "=[");

            if (entitys != null)
            {
                int i = 0;
                foreach (KeyValuePair<string, EntityBase> keyvalue in entitys)
                {
                    sb.Append(this.GetJsEntity(keyvalue.Value, type));
                    i++;
                    if (i != entitys.Count) sb.Append(",");
                }
            }
            sb.Append("];");
            return sb.ToString();
        }

        /// <summary>
        /// 得到实体的JSON对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual string GetJsEntity(EntityBase entity, Type type)
        {
            return string.Empty;
        }

        

        /// <summary>
        /// 从记录行中得到实体
        /// </summary>
        /// <param name="?"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual EntityBase GetEntityObjectFromRow(DataRow row, Type type)
        {
            return null;
        }
    }
}
