/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    �������Թ����ķ�ʽ�������أ��κθ��˺���֯�������أ� 
  * �޸ģ����еڶ��ο���ʹ�ã����뱣�����߰�Ȩ��Ϣ�� 
  *  
  *    �κθ��˻���֯��ʹ�ñ�������������ɵ�ֱ�ӻ�����ʧ�� 
  * ��Ҫ���ге�����뱾����������(���ƹ�)�޹ء� 
  *  
  *    �����������С���̼Ҳ�Ʒ���绯���۷����� 
  *     
  *    ʹ���е����⣬��ѯ����QQ���� sanyungui@vip.qq.com 
  */

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;


namespace TCG.Handlers
{
    public class CategoriesHandlers : SkinHandlerBase
    {

        /// <summary>
        /// ���ݸ���õ���Դ���
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public DataTable GetCategoriesByParentId(int parentid)
        {
            if (!objectHandlers.ToBoolen(base.configService.baseConfig["IsUsedCaching"],true))
            {
                base.SetSkinDataBaseConnection();
                string Sql = "SELECT Id,vcClassName,vcName,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM Categories WITH (NOLOCK)"
                    + " WHERE iParent = " + parentid.ToString();
                return conn.GetDataTable(Sql);
            }
            else
            {
                DataTable dt = GetAllCategories();
                if (dt == null) return null;
                DataRow[] rows = dt.Select("iParent=" + parentid.ToString());
                DataSet dts = new DataSet();
                dts.Merge(rows);
                if (dts.Tables.Count == 0) return null;
                return dts.Tables[0];
            }
        }

        public List<Categories> GetAllCategoriesEntity()
        {
            DataTable dt = GetAllCategories();
            if (dt == null) return null;


        }

        /// <summary>
        /// ������з�����Ϣ
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllCategories()
        {
            if (!objectHandlers.ToBoolen(base.configService.baseConfig["IsUsedCaching"], true))
            {
                return GetAllCategoriesWithOutCaching();
            }
            else
            {
                DataTable allcategories = (DataTable)CachingService.Get(CachingService.CACHING_ALL_CATEGORIES);
                if (allcategories == null)
                {
                    return GetAllCategoriesWithOutCaching();
                }
                return allcategories;
            }
        }

        public DataTable GetAllCategoriesWithOutCaching()
        {
            base.SetSkinDataBaseConnection();
            string Sql = "SELECT Id,vcClassName,vcName,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM Categories WITH (NOLOCK)";
            return conn.GetDataTable(Sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <param name="readdb"></param>
        /// <returns></returns>
        public string GetAllChildCategoriesIdByCategoriesId(string id, bool readdb)
        {
            //if (id == 0) return "";
            DataTable allClass = this.GetAllCategories(readdb);
            if (allClass == null) return "";
            DataRow[] rows = allClass.Select("[Parent] = '" + id + "' ");
            string str = "'" + id.ToString() + "'";
            for (int i = 0; i < rows.Length; i++)
            {

                string t = GetAllChildCategoriesIdByCategoriesId(rows[i]["Id"].ToString(), readdb);
                if (!string.IsNullOrEmpty(t)) str += "," + t;
            }
            return str;
        }

        /// <summary>
        /// ������µĵ�����~
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="classid"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public string GetResourcesCategoriesIndex(string classid, string sh)
        {
            if (string.IsNullOrEmpty(classid)) return "";
            DataTable allClass = this.GetCategoriesByCach(false);
            if (allClass == null) return "";
            DataRow[] rows = allClass.Select("Id = '" + classid.ToString() + "'");
            string str = "";
            if (rows.Length==1)
            {

                string url = (rows[0]["vcUrl"].ToString().IndexOf(".") > -1) ? rows[0]["vcUrl"].ToString() : rows[0]["vcUrl"].ToString() + base.configService.baseConfig["FileExtension"];
                str = "<a href=\"" + url + "\" target=\"_blank\">" + rows[0]["vcName"].ToString() + "</a>";
                string t = GetResourcesCategoriesIndex(rows[0]["Parent"].ToString(), sh);
                if (!string.IsNullOrEmpty(t)) str = t + sh + str;
            }
            return str;
        }

        /// <summary>
        /// ����ID��÷�����Ϣ
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        public Categories GetCategoriesById(string iClassID, bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                base.SetSkinDataBaseConnection();
                string Sql = "SELECT Id,vcClassName,vcName,Parent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM Categories WITH (NOLOCK)"
                    + " WHERE ID = '" + iClassID.ToString() + "'";
                dt = base.conn.GetDataTable(Sql);
            }
            else
            {
                DataTable allclass = this.GetCategoriesByCach(false);
                if (allclass != null)
                {
                    DataRow[] rows = allclass.Select("Id = '" + iClassID.ToString() + "'");
                    if (rows.Length == 1)
                    {
                        DataSet ds = new DataSet();
                        ds.Merge(rows);
                        dt = ds.Tables[0];
                    }
                }
            }
            if (dt != null)
            {
                Categories cif = new Categories();
                DataRow Row = dt.Rows[0];
                cif.Id = Row["Id"].ToString();
                cif.iListTemplate = Row["iListTemplate"].ToString();
                cif.iOrder = (int)Row["iOrder"];
                cif.Parent = Row["Parent"].ToString();
                cif.iTemplate = Row["iTemplate"].ToString();
                cif.vcClassName = (string)Row["vcClassName"];
                cif.vcDirectory = (string)Row["vcDirectory"];
                cif.vcName = (string)Row["vcName"];
                cif.vcUrl = (string)Row["vcUrl"];
                cif.dUpdateDate = (DateTime)Row["dUpdateDate"];
                cif.cVisible = (string)Row["Visible"];
                return cif;
            }
            return null;
        }

        /// <summary>
        /// �������ŷ���
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classname"></param>
        /// <param name="lname"></param>
        /// <param name="parentid"></param>
        /// <param name="templateid"></param>
        /// <param name="ltemplateid"></param>
        /// <param name="dir"></param>
        /// <param name="url"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int CreateCategories(Connection conn, Categories cif, string adminname)
        {
            base.SetSkinDataBaseConnection();
            cif.Id = Guid.NewGuid().ToString();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp2.Value = cif.vcClassName;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = cif.vcName;
            SqlParameter sp4 = new SqlParameter("@Parent", SqlDbType.VarChar, 36); sp4.Value = cif.Parent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.VarChar, 36); sp5.Value = cif.iTemplate;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.VarChar, 36); sp6.Value = cif.iListTemplate;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = cif.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = cif.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = cif.iOrder;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            SqlParameter sp11 = new SqlParameter("@cVisible", SqlDbType.Char, 1); sp11.Value = cif.cVisible;
            SqlParameter sp12 = new SqlParameter("@iClassId", SqlDbType.VarChar, 36); sp12.Value = cif.Id;
            string[] reValues = conn.Execute("SP_News_ClassInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11,sp12}, new int[] { 10 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }


        /// <summary>
        /// �޸ķ���
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        public int UpdateCategories(Connection conn, string adminname, Categories classinf)
        {
            base.SetSkinDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp2.Value = classinf.vcClassName;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = classinf.vcName;
            SqlParameter sp4 = new SqlParameter("@Parent", SqlDbType.VarChar, 36); sp4.Value = classinf.Parent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.VarChar, 36); sp5.Value = classinf.iTemplate;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.VarChar, 36); sp6.Value = classinf.iListTemplate;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = classinf.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = classinf.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = classinf.iOrder;
            SqlParameter sp10 = new SqlParameter("@action", SqlDbType.Char, 2); sp10.Value = "02";
            SqlParameter sp11 = new SqlParameter("@iClassId", SqlDbType.VarChar, 36); sp11.Value = classinf.Id;
            SqlParameter sp12 = new SqlParameter("@reValue", SqlDbType.Int); sp12.Direction = ParameterDirection.Output;
            SqlParameter sp13 = new SqlParameter("@cVisible", SqlDbType.Char, 1); sp13.Value = classinf.cVisible;
            string[] reValues = conn.Execute("SP_News_ClassInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11,sp12,sp13}, new int[] { 12 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// ɾ����Ѷ����
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int DelCategories(Connection conn, int classid, string adminname)
        {
            base.SetSkinDataBaseConnection();
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@iClassId", SqlDbType.Int, 4); sp2.Value = classid;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_DelNewsClassById", new SqlParameter[] { sp0, sp1, sp2, sp3}, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }
    }
}