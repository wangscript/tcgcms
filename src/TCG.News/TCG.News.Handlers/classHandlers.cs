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
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Data;
using TCG.Utils;
using TCG.News.Entity;


namespace TCG.News.Handlers
{
    public class classHandlers
    {
        /// <summary>
        /// 根据父类得到资源类别
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public DataTable GetClassInfosByParentId(int parentid,Connection conn,bool readdb)
        {
            if (readdb)
            {
                conn.Dblink = DBLinkNums.News;
                string Sql = "SELECT iID,vcClassName,vcName,iParent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM T_News_ClassInfo WITH (NOLOCK)"
                    + " WHERE iParent = " + parentid.ToString();
                return conn.GetDataTable(Sql);
            }
            else
            {
                DataTable dt = GetClassInfoByCach(conn, false);
                if (dt == null) return null;
                DataRow[] rows = dt.Select("iParent=" + parentid.ToString());
                DataSet dts = new DataSet();
                dts.Merge(rows);
                if (dts.Tables.Count == 0) return null;
                return dts.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获得所有分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetClassInfos(Connection conn)
        {
            conn.Dblink = DBLinkNums.News;
            string Sql = "SELECT iID,vcClassName,vcName,iParent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM T_News_ClassInfo WITH (NOLOCK)";
            return conn.GetDataTable(Sql);
        }

        public DataTable GetClassInfoByCach(Connection conn, bool readdb)
        {
            if (readdb)
            {
                return this.GetClassInfos(conn);
            }
            else
            {
                DataTable classlist = (DataTable)Caching.Get("AllNewsClass");
                if (classlist == null)
                {
                    classlist = this.GetClassInfos(conn);
                    Caching.Set("AllNewsClass", classlist, null);
                }
                return classlist;
            }
        }

        public string GetAllChildClassIdByClassId(Connection conn, int id, bool readdb)
        {
            //if (id == 0) return "";
            DataTable allClass = this.GetClassInfoByCach(conn, readdb);
            if (allClass == null) return "";
            DataRow[] rows = allClass.Select("iParent = " + id.ToString());
            string str = id.ToString();
            for (int i = 0; i < rows.Length; i++)
            {
                
                string t = GetAllChildClassIdByClassId(conn, (int)rows[i]["iId"], readdb);
                if (!string.IsNullOrEmpty(t)) str += "," + t;
            }
            return str;
        }

        /// <summary>
        /// 获得文章的导航！~
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="config"></param>
        /// <param name="classid"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public string GetTopicClassTitleList(Connection conn,Config config, int classid,string sh)
        {
            if (classid == 0) return "";
            DataTable allClass = this.GetClassInfoByCach(conn, false);
            if (allClass == null) return "";
            DataRow[] rows = allClass.Select("iID = " + classid.ToString());
            string str = "";
            if (rows.Length==1)
            {

                string url = (rows[0]["vcUrl"].ToString().IndexOf(".") > -1) ? rows[0]["vcUrl"].ToString() : rows[0]["vcUrl"].ToString() + config["FileExtension"];
                str = "<a href=\"" + url + "\" target=\"_blank\">" + rows[0]["vcName"].ToString() + "</a>";
                string t = GetTopicClassTitleList(conn, config, (int)rows[0]["iParent"], sh);
                if (!string.IsNullOrEmpty(t)) str = t + sh + str;
            }
            return str;
        }

        /// <summary>
        /// 根据ID获得分类信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        public ClassInfo GetClassInfoById(Connection conn, int iClassID, bool readdb)
        {
            DataTable dt = null;
            if (readdb)
            {
                conn.Dblink = DBLinkNums.News;
                string Sql = "SELECT iID,vcClassName,vcName,iParent,dUpdateDate,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible FROM T_News_ClassInfo WITH (NOLOCK)"
                    + " WHERE iID =" + iClassID.ToString();
                dt = conn.GetDataTable(Sql);
            }
            else
            {
                DataTable allclass = this.GetClassInfoByCach(conn, false);
                if (allclass != null)
                {
                    DataRow[] rows = allclass.Select("iID =" + iClassID.ToString());
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
                ClassInfo cif = new ClassInfo();
                DataRow Row = dt.Rows[0];
                cif.iId = (int)Row["iId"];
                cif.iListTemplate = (int)Row["iListTemplate"];
                cif.iOrder = (int)Row["iOrder"];
                cif.iParent = (int)Row["iParent"];
                cif.iTemplate = (int)Row["iTemplate"];
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
        /// 添加新闻分类
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
        public int AddNewsClass(Connection conn, ClassInfo cif, string adminname)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp2.Value = cif.vcClassName;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = cif.vcName;
            SqlParameter sp4 = new SqlParameter("@iParent", SqlDbType.Int, 4); sp4.Value = cif.iParent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.Int, 4); sp5.Value = cif.iTemplate;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.Int, 4); sp6.Value = cif.iListTemplate;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = cif.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = cif.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = cif.iOrder;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            SqlParameter sp11 = new SqlParameter("@cVisible", SqlDbType.Char, 1); sp11.Value = cif.cVisible;
            string[] reValues = conn.Execute("SP_News_ClassInfoManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11}, new int[] { 10 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }


        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="classinf"></param>
        /// <returns></returns>
        public int EditNewsClass(Connection conn, string adminname, ClassInfo classinf)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcClassName", SqlDbType.VarChar, 200); sp2.Value = classinf.vcClassName;
            SqlParameter sp3 = new SqlParameter("@vcName", SqlDbType.VarChar, 50); sp3.Value = classinf.vcName;
            SqlParameter sp4 = new SqlParameter("@iParent", SqlDbType.Int, 4); sp4.Value = classinf.iParent;
            SqlParameter sp5 = new SqlParameter("@iTemplate", SqlDbType.Int, 4); sp5.Value = classinf.iTemplate;
            SqlParameter sp6 = new SqlParameter("@iListTemplate", SqlDbType.Int, 4); sp6.Value = classinf.iListTemplate;
            SqlParameter sp7 = new SqlParameter("@vcDirectory", SqlDbType.VarChar, 200); sp7.Value = classinf.vcDirectory;
            SqlParameter sp8 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp8.Value = classinf.vcUrl;
            SqlParameter sp9 = new SqlParameter("@iOrder", SqlDbType.Int, 4); sp9.Value = classinf.iOrder;
            SqlParameter sp10 = new SqlParameter("@action", SqlDbType.Char, 2); sp10.Value = "02";
            SqlParameter sp11 = new SqlParameter("@iClassId", SqlDbType.Int, 4); sp11.Value = classinf.iId;
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
        /// 删除资讯分类
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="classid"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int DelNewsClassByClassId(Connection conn, int classid, string adminname)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
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
