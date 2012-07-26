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
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers.Imp.MsSql
{
    public class SpecialityHandlers : ISpecialityHandlers
    {
        /// <summary>
        /// 添加新的资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityAdd(Speciality nif)
        {

            string sql = "INSERT INTO Speciality(SkinId,iParent,dUpDateDate,vcTitle,vcExplain) VALUES("
            + "'" + nif.SkinId + "'," + nif.iParent.ToString() + ",getdate(),'" + nif.vcTitle + "','" + nif.vcExplain + "')";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }

        /// <summary>
        /// 修改资讯类型
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityMdy(Speciality nif)
        {
            string sql = "UPDATE Speciality SET SkinId='" + nif.SkinId + "',iParent=" + nif.iParent + ",dUpDateDate=getdate ()"
                + ",vcTitle='" + nif.vcTitle + "',vcExplain='" + nif.vcExplain + "' WHERE id=" + nif.Id.ToString();
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }


        /// <summary>
        /// 删除特性
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int NewSpecialityDel(string ids)
        {
            string sql = "DELETE FROM Speciality WHERE id in (" + ids + ")";
            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }


        /// <summary>
        /// 获得所有资源特性
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllNewsSpecialityInfo(string skinid)
        {
            string Sql = "SELECT * FROM Speciality WHERE SkinId='" + skinid + "'";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, Sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }
    }
}
