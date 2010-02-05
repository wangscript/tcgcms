﻿

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
    public class SheifHandlers : ObjectHandlersBase
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

            string[] reValues = conn.Execute("SP_SheifSource_Add", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,
                sp7, sp8, sp9 ,sp10,sp11}, new int[] { 11 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        public int GetAllShieSourceInfo(ref List<SheifSourceInfo> sourceinfos)
        {
            base.SetDataBaseConnection();
            DataTable dt = base.conn.GetDataTable("SELECT * FROM [SheifSource] (NOLOCK) ");
            if (dt == null) return -19000000;
            if (dt.Rows.Count == 0) return -19000000;

            sourceinfos = new List<SheifSourceInfo>();

            foreach (DataRow Row in dt.Rows)
            {
                SheifSourceInfo sheifSourceInfo = (SheifSourceInfo)base.GetEntityObjectFromRow(Row, typeof(SheifSourceInfo));
                sourceinfos.Add(sheifSourceInfo);
            }

            return 1;

        }
    }
}
