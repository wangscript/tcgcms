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
using System.Web.Caching;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class NewsFromHandlers
    {
        /// <summary>
        ///添加新的来源 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nfif"></param>
        /// <returns></returns>
        public int AddNewsFromInfo(Connection conn, string adminname, NewsFromInfo nfif)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 150); sp2.Value = nfif.vcTitle;
            SqlParameter sp3 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp3.Value = nfif.vcUrl;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int); sp4.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsFromManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 }, new int[] { 4 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 修改来源信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nfif"></param>
        /// <returns></returns>
        public int UpdateNewsFromInfo(Connection conn, string adminname, NewsFromInfo nfif)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "02";
            SqlParameter sp3 = new SqlParameter("@iID", SqlDbType.Int, 4); sp3.Value = nfif.iId;
            SqlParameter sp4 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 150); sp4.Value = nfif.vcTitle;
            SqlParameter sp5 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp5.Value = nfif.vcUrl;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int); sp6.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsFromManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 }, new int[] { 6 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 删除来源信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteNewsFromInfos(Connection conn, string adminname, string ids)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "03";
            SqlParameter sp3 = new SqlParameter("@ids", SqlDbType.VarChar, 100); sp3.Value = ids;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int); sp4.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_NewsFromManage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 }, new int[] { 4 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 根据来源ID获得来源
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsFromInfo GetNewsFromInfoById(Connection conn, int id)
        {
            conn.Dblink = DBLinkNums.News;
            string SQL = "SELECT iID,vcTitle,vcUrl,dUpdateDate FROM T_News_NewsFromInfo (NOLOCK) WHERE iId=" + id.ToString();
            DataTable dt = conn.GetDataTable(SQL);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    NewsFromInfo item = new NewsFromInfo();
                    item.iId = (int)dt.Rows[0]["iID"];
                    item.vcTitle = (string)dt.Rows[0]["vcTitle"];
                    item.vcUrl = (string)dt.Rows[0]["vcUrl"];
                    item.dUpdateDate = (DateTime)dt.Rows[0]["dUpdateDate"];
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 从数据库得到所有来源
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllNewsFrom(Connection conn)
        {
            conn.Dblink = DBLinkNums.News;
            string SQL = "SELECT iID,vcTitle,vcUrl,dUpdateDate FROM T_News_NewsFromInfo (NOLOCK)";
            return conn.GetDataTable(SQL);
        }

        public DataTable GetAllNewsFromByCach(Connection conn,bool ReadDb)
        {
            if (ReadDb)
            {
                return this.GetAllNewsFrom(conn);
            }
            DataTable newsfrom = (DataTable)CachingService.Get("AllNewsFrom");
            if (newsfrom == null)
            {
                newsfrom  = this.GetAllNewsFrom(conn);
                CachingService.Set("AllNewsFrom", newsfrom, null);
            }
            return newsfrom;
        }
    }
}
