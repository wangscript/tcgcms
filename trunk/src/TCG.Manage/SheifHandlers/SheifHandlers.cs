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
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class SheifHandlers : ManageObjectHandlersBase
    {
        public int AddShiefSource(SheifSourceInfo sheifsourceinfo)
        {
            base.SetDataBaseConnection();
            sheifsourceinfo.Id = Guid.NewGuid().ToString();

            SqlParameter sp0 = new SqlParameter("@SourceName", SqlDbType.NVarChar, 500); sp0.Value = sheifsourceinfo.SourceName;
            SqlParameter sp1 = new SqlParameter("@SourceUrl", SqlDbType.NVarChar, 255); sp1.Value = sheifsourceinfo.SourceUrl;
            SqlParameter sp2 = new SqlParameter("@CharSet", SqlDbType.Char, 10); sp2.Value = sheifsourceinfo.CharSet;
            SqlParameter sp3 = new SqlParameter("@ListAreaRole", SqlDbType.NVarChar, 1000); sp3.Value = sheifsourceinfo.ListAreaRole;
            SqlParameter sp4 = new SqlParameter("@TopicListRole", SqlDbType.NVarChar, 1000); sp4.Value = sheifsourceinfo.TopicListRole;
            SqlParameter sp5 = new SqlParameter("@TopicListDataRole", SqlDbType.NVarChar, 1000); sp5.Value = sheifsourceinfo.TopicListDataRole;
            SqlParameter sp6 = new SqlParameter("@TopicRole", SqlDbType.NVarChar, 1000); sp6.Value = sheifsourceinfo.TopicRole;
            SqlParameter sp7 = new SqlParameter("@TopicDataRole", SqlDbType.NVarChar, 1000); sp7.Value = sheifsourceinfo.TopicDataRole;
            SqlParameter sp8 = new SqlParameter("@TopicPagerOld", SqlDbType.NVarChar, 255); sp8.Value = sheifsourceinfo.TopicPagerOld;
            SqlParameter sp9 = new SqlParameter("@TopicPagerTemp", SqlDbType.NVarChar, 255); sp9.Value = sheifsourceinfo.TopicPagerTemp;
            SqlParameter sp10 = new SqlParameter("@Id", SqlDbType.VarChar, 39); sp10.Value = sheifsourceinfo.Id;
            SqlParameter sp11 = new SqlParameter("@reValue", SqlDbType.Int, 4); sp11.Direction = ParameterDirection.Output;
            SqlParameter sp12 = new SqlParameter("@IsRss", SqlDbType.Bit, 2); sp12.Value = sheifsourceinfo.IsRss;

            string[] reValues = conn.Execute("SP_SheifSource_Add", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11,sp12}, new int[] { 11 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        public int GetAllShieSourceInfo(ref Dictionary<string,SheifSourceInfo> sourceinfos)
        {
            base.SetDataBaseConnection();
            DataTable dt = base.conn.GetDataTable("SELECT * FROM [SheifSource] (NOLOCK) ");
            if (dt == null) return -19000000;
            if (dt.Rows.Count == 0) return -19000000;

            sourceinfos = new Dictionary<string,SheifSourceInfo>();

            foreach (DataRow Row in dt.Rows)
            {
                SheifSourceInfo sheifSourceInfo = (SheifSourceInfo)base.GetEntityObjectFromRow(Row, typeof(SheifSourceInfo));
                sourceinfos.Add(sheifSourceInfo.Id,sheifSourceInfo);
            }

            return 1;

        }

        public int CreateSheifCategorieConfig(SheifCategorieConfig sheifcategorieconfig)
        {
            string Sql = "INSERT INTO SheifCategorieConfig (SheifSourceId,LocalCategorieId) VALUES(";
            Sql += "'" + sheifcategorieconfig.SheifSourceId + "','" + sheifcategorieconfig.LocalCategorieId + "')";
            return base.conn.Execute(Sql);
        }

        public int UpdateSheifCategorieConfig(SheifCategorieConfig sheifcategorieconfig)
        {
            string Sql = "UPDATE SheifCategorieConfig SET LocalCategorieId = '"
                + sheifcategorieconfig.LocalCategorieId + "',ResourceCreateDateTime = '" + sheifcategorieconfig.ResourceCreateDateTime.ToString() + "'";
            Sql += " WHERE SheifSourceId='" + sheifcategorieconfig.SheifSourceId + "'";
            return base.conn.Execute(Sql);
        }

        public int GeSheifcategorieconfigs(ref List<SheifCategorieConfig> sheifcategorieconfigs)
        {
            base.SetDataBaseConnection();
            DataTable dt = base.conn.GetDataTable("SELECT * FROM [SheifCategorieConfig] (NOLOCK) ");
            if (dt == null) return -19000000;
            if (dt.Rows.Count == 0) return -19000000;

            sheifcategorieconfigs = new List<SheifCategorieConfig>();

            foreach (DataRow Row in dt.Rows)
            {
                SheifCategorieConfig sheifcategorieconfig = (SheifCategorieConfig)base.GetEntityObjectFromRow(Row, typeof(SheifCategorieConfig));
                sheifcategorieconfigs.Add(sheifcategorieconfig);
            }

            return 1;
        }
    }
}
