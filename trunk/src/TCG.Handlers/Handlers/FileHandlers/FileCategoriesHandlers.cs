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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;

namespace TCG.Handlers
{
    public class FileCategoriesHandlers : FileResourcesHandlerBase
    {

        public FileCategoriesHandlers()
        {
        }

        /// <summary>
        /// 从数据库获取所遇分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllFilesClassFromDb()
        {
            base.SetMainFileDataBase();
            return base.conn.GetDataTable("SELECT * FROM filecategories (NOLOCK)");
        }

        /// <summary>
        /// 获得所有文章分类实体
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetAllFileCategoriesEntity()
        {
            Dictionary<string, EntityBase> allfilecategories = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_ALL_FILECATEGORIES_ENTITY);
            if (allfilecategories == null)
            {
                DataTable dt = GetAllFilesClassFromDb();
                if (dt == null) return null;
                allfilecategories = base.GetEntitysObjectFromTable(dt, typeof(FileCategories));
                CachingService.Set(CachingService.CACHING_ALL_FILECATEGORIES_ENTITY, allfilecategories, null);
            }
            return allfilecategories;
        }

        /// <summary>
        /// 根据ID获得文章分类信息
        /// </summary>
        /// <param name="filecateorieid"></param>
        /// <returns></returns>
        public FileCategories GetFileCategories(string filecateorieid)
        {
            Dictionary<string, EntityBase> allfilecategories = this.GetAllFileCategoriesEntity();
            if (allfilecategories == null) return null;
            return allfilecategories.ContainsKey(filecateorieid) ? (FileCategories)allfilecategories[filecateorieid] : null;
        }

        /// <summary>
        /// 获取所有分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="readdb"></param>
        /// <returns></returns>
        public DataTable GetAllFileClass(bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                dt = this.GetAllFilesClassFromDb();
            }
            else
            {
                dt = (DataTable)CachingService.Get(CachingService.CACHING_ALL_FILECLASS);
                if (dt == null)
                {
                    dt = this.GetAllFilesClassFromDb();
                    CachingService.Set(CachingService.CACHING_ALL_FILECLASS, dt, null);
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
        public DataTable GetFilesClassInfosByParendId(int prarentid)
        {
            DataTable dt = this.GetAllFileClass(false);
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

        public string GetFilesPathByClassId(int classid)
        {
            string text1 = this.GetFilesPathByClassIdW(classid);
            return base.configService.baseConfig["filePatch"] + text1 + @"/";
        }

        private string GetFilesPathByClassIdW(int classid)
        {
            DataTable dt = this.GetAllFileClass(false);
            if (dt != null)
            {
                DataRow[] Rows = dt.Select("iId =" + classid.ToString());
                if (Rows.Length == 1)
                {
                    string text1 = this.GetFilesPathByClassIdW(objectHandlers.ToInt(Rows[0]["iParentId"]));
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
        public int AddFileClass( string adminname, FileCategories fcif)
        {
            base.SetMainFileDataBase();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcFileName", SqlDbType.NVarChar, 100); sp2.Value = fcif.vcFileName;
            SqlParameter sp3 = new SqlParameter("@iParentId", SqlDbType.Int, 4); sp3.Value = fcif.iParentId;
            SqlParameter sp4 = new SqlParameter("@vcMeno", SqlDbType.NVarChar, 100); sp4.Value = fcif.vcMeno;
            SqlParameter sp5 = new SqlParameter("@reValue", SqlDbType.Int); sp5.Direction = ParameterDirection.Output;
            string[] reValues = base.conn.Execute("SP_Files_FilesClassManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5 }, new int[] { 5 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        
    }
}