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
using System.Web;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using TCG.Utils;
using TCG.Entity;
using System.Net;

namespace TCG.Handlers.Imp.MsSql
{
    public class FileHandlers : IFileHandlers 
    {
        /// <summary>
        /// 从数据库获取所遇分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllFilesClassFromDb()
        {
            string sql = "SELECT * FROM filecategories";
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, sql, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileClass(FileCategories fcif)
        {

            //文件名不能为空
            if (string.IsNullOrEmpty(fcif.vcFileName))
            {
                return -1000000057;
            }

            //简单说明不能为空
            if (string.IsNullOrEmpty(fcif.vcMeno))
            {
               return -1000000058;
            }

            string sql = "INSERT INTO filecategories (vcFileName,iParentId,vcMeno) VALUES('" + fcif.vcFileName + "','" + fcif.iParentId + "','" + fcif.vcMeno + "')";

            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }



        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileInfoByAdmin(FileResources fif)
        {
            //文件所在的文件夹不能为空
            if (fif.iClassId == 0)
            {
                return -1000000060;
            }

            //文件名称不能为空
            if (string.IsNullOrEmpty(fif.vcFileName))
            {
                return -1000000061;
            }

            //文件类型不能为空
            if (string.IsNullOrEmpty(fif.vcType))
            {
                return -1000000062;
            }

            //文件类型不能为空
            if (fif.iSize==0)
            {
                return -1000000063;
            }

            string sql= "INSERT INTO fileresources (iID,iClassId,vcFileName,iSize,vcType,dCreateDate,vcIP)"
                                        + "VALUES('" + fif.Id + "'," + fif.iClassId + ",'" + fif.vcFileName + "'," + fif.iSize + ",'"
                                        + fif.vcType + "',getdate(),'" + fif.vcIP + "')";

            string errText = string.Empty;
            return MsSqlFactory.conn.m_RunSQL(ref errText, sql);
        }


        /// <summary>
        ///根据ID获得文件信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetFileInfosById(string id)
        {
            string SQL = "SELECT iID,iClassId,vcFileName,iSize,vcType,iDowns,iRequest,vcIP,dCreateDate FROM fileresources WHERE iId=" + id.ToString();
            string errText = string.Empty;
            DataSet ds = null;
            int rtn = MsSqlFactory.conn.m_RunSQLData(ref errText, SQL, ref ds);
            if (rtn < 0) return null;
            if (ds == null || ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

    }
}