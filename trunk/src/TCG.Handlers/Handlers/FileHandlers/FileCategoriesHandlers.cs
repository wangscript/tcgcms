/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾���������(���ƹ�)�޹ء� 
  *  
  *    ����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
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
        /// �����ݿ��ȡ����������Ϣ
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllFilesClassFromDb()
        {
            base.SetMainFileDataBase();
            return base.conn.GetDataTable("SELECT * FROM filecategories (NOLOCK)");
        }

        /// <summary>
        /// ����������·���ʵ��
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
        /// ����ID������·�����Ϣ
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
        /// ��ȡ���з�����Ϣ
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
        /// ���ݸ��׻���ļ���
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
        /// ����µķ���
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