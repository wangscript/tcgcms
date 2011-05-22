﻿using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Entity;
using TCG.Utils;
using System.Xml;
using TCG.Handlers;

namespace TCG.Handlers.Imp.AccEss
{
    public class TemplateHandlers : ITemplateHandlers
    {

        public DataTable GetAllTemplatesWithOutCaching()
        {

            string Sql = "SELECT * FROM Template";
            return AccessFactory.conn.DataTable(Sql);
        }


        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddTemplate(Template item)
        {

            //单页时地址不能为空 
            if (((int)item.TemplateType) == -1 && string.IsNullOrEmpty(item.vcUrl))
            {
                return -1000000024;
            }

            //模板内容不能为空
            if (string.IsNullOrEmpty(item.Content))
            {
                return -1000000027;
            }

            AccessFactory.conn.Execute("INSERT INTO Template (Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl,dUpdateDate,dAddDate)"
                + "VALUES('" + item.Id + "','" + item.SkinInfo.Id + "','" + (int)item.TemplateType + "','" + item.iParentId + "','" + item.iSystemType + "','"
                + item.vcTempName + "','" + item.Content.Replace("'", "''") + "','" + item.vcUrl + "',now(),now())");

            return 1;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int DelTemplate(string temps)
        {
            //尚未选择需要删除的资讯模版
            if (string.IsNullOrEmpty(temps))
            {
                return -1000000028;
            }

            AccessFactory.conn.Execute("DELETE FROM Template WHERE Id IN (" + temps + ")");
            return 1;
        }

        /// <summary>
        /// 修改模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int MdyTemplate(Template item)
        {
           
            //单页时地址不能为空 
            if (((int)item.TemplateType) == -1 && string.IsNullOrEmpty(item.vcUrl))
            {
                return -1000000024;
            }

            //模板内容不能为空
            if (string.IsNullOrEmpty(item.Content))
            {
                return -1000000027;
            }
            string sql = "UPDATE Template SET vcTempName='" + item.vcTempName + "',vcContent='" + item.Content.Replace("'", "''") + "',vcUrl='" + item.vcUrl
                                    + "',dUpdateDate=now(),iParentId='" + item.iParentId + "' WHERE Id = '" + item.Id + "'";

            AccessFactory.conn.Execute(sql);

            return 1;
        }

    }
}
