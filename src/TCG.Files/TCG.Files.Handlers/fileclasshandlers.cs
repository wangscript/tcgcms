/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Files.Utils;
using TCG.Files.Entity;

namespace TCG.Files.Handlers
{
    public class fileclasshandlers
    {

        /// <summary>
        /// 从数据库获取所遇分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllFilesClassFromDb(Connection conn)
        {
            conn.Dblink = FilesConst.FilesDbLinks[0];
            return conn.GetDataTable("SELECT iId,vcFileName,iParentId,vcMeno,dCreateDate FROM T_Files_Class (NOLOCK)");
        }

        /// <summary>
        /// 获取所有分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="readdb"></param>
        /// <returns></returns>
        public DataTable GetAllFileClass(Connection conn, bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                dt = this.GetAllFilesClassFromDb(conn);
            }
            else
            {
                dt = (DataTable)Caching.Get(FilesConst.CACHING_ALL_FILECLASS);
                if (dt == null)
                {
                    dt = this.GetAllFilesClassFromDb(conn);
                    Caching.Set(FilesConst.CACHING_ALL_FILECLASS, dt, null);
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据父亲获得文件夹
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="prarentid"></param>
        /// <returns></returns>
        public DataTable GetFilesClassInfosByParendId(Connection conn, int prarentid)
        {
            DataTable dt = this.GetAllFileClass(conn, false);
            if (dt != null)
            {
                DataRow[] rows = dt.Select("iParentId=" + prarentid.ToString());
                if (rows.Length != 0)
                {
                    DataSet ds = new DataSet();
                    ds.Merge(rows);
                    return ds.Tables[0];
                }
                return null;
            }
            return dt;
        }

        public string GetFilesPathByClassId(Connection conn, int classid)
        {
            string text1 = this.GetFilesPathByClassIdW(conn, classid);
            return FilesConst.FilePath + text1 + @"/";
        }

        private string GetFilesPathByClassIdW(Connection conn, int classid)
        {
            DataTable dt = this.GetAllFileClass(conn, false);
            if (dt != null)
            {
                DataRow[] Rows = dt.Select("iId =" + classid.ToString());
                if (Rows.Length == 1)
                {
                    string text1 = this.GetFilesPathByClassIdW(conn, Bases.ToInt(Rows[0]["iParentId"]));
                    string text2 = (text1 == "") ? "" : "/";
                    return text1 + text2 + Rows[0]["vcFileName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 添加新的分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="fcif"></param>
        /// <returns></returns>
        public int AddFileClass(Connection conn, string adminname, FileClassInfo fcif)
        {
            conn.Dblink = FilesConst.FilesDbLinks[0];
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcFileName", SqlDbType.NVarChar, 100); sp2.Value = fcif.vcFileName;
            SqlParameter sp3 = new SqlParameter("@iParentId", SqlDbType.Int, 4); sp3.Value = fcif.iParentId;
            SqlParameter sp4 = new SqlParameter("@vcMeno", SqlDbType.NVarChar, 100); sp4.Value = fcif.vcMeno;
            SqlParameter sp5 = new SqlParameter("@reValue", SqlDbType.Int); sp5.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Files_FilesClassManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5}, new int[] { 5 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }
    }
}