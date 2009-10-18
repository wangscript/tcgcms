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
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    public class NewsSpecialityHandlers
    {
        /// <summary>
        /// ����µ���Ѷ����
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityAdd(Connection conn, string adminname, NewsSpecialityInfo nif)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = nif.iSiteId;
            SqlParameter sp3 = new SqlParameter("@iParent", SqlDbType.Int, 4); sp3.Value = nif.iParent;
            SqlParameter sp4 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 50); sp4.Value = nif.vcTitle;
            SqlParameter sp5 = new SqlParameter("@vcExplain", SqlDbType.VarChar, 50); sp5.Value = nif.vcExplain;
            SqlParameter sp6 = new SqlParameter("@reValue", SqlDbType.Int); sp6.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_SpecialityAdmin", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6 }, new int[] { 6 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// �޸���Ѷ����
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="nif"></param>
        /// <returns></returns>
        public int NewsSpecialityMdy(Connection conn, string adminname, NewsSpecialityInfo nif)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@iSiteId", SqlDbType.Int, 4); sp2.Value = nif.iSiteId;
            SqlParameter sp3 = new SqlParameter("@iParent", SqlDbType.Int, 4); sp3.Value = nif.iParent;
            SqlParameter sp4 = new SqlParameter("@vcTitle", SqlDbType.VarChar, 50); sp4.Value = nif.vcTitle;
            SqlParameter sp5 = new SqlParameter("@vcExplain", SqlDbType.VarChar, 50); sp5.Value = nif.vcExplain;
            SqlParameter sp6 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp6.Value = "02";
            SqlParameter sp7 = new SqlParameter("@iId", SqlDbType.Int, 4); sp7.Value = nif.iId;
            SqlParameter sp8 = new SqlParameter("@reValue", SqlDbType.Int); sp8.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_SpecialityAdmin", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6,sp7,sp8 }, new int[] { 8 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }


        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="adminname"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int NewSpecialityDel(Connection conn, string adminname, string ids)
        {
            conn.Dblink = DBLinkNums.News;
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = adminname;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = Fetch.UserIp;
            SqlParameter sp2 = new SqlParameter("@cAction", SqlDbType.Char, 2); sp2.Value = "03";
            SqlParameter sp3 = new SqlParameter("@IDs", SqlDbType.VarChar, 200); sp3.Value = ids;
            SqlParameter sp4 = new SqlParameter("@reValue", SqlDbType.Int); sp4.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_News_SpecialityAdmin", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 }, new int[] { 4 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// ����ID�����Դ����
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsSpecialityInfo GetNewsSpecialityInfoById(Connection conn, int id)
        {
            conn.Dblink = DBLinkNums.News;
            string SQL = "SELECT iId,iSiteId,iParent,vcTitle,vcExplain,dUpDateDate FROM T_News_Speciality (NOLOCK) WHERE iId=" + id.ToString();
            DataTable dt = conn.GetDataTable(SQL);
            if (dt == null) return null;
            if (dt.Rows.Count != 1) return null;
            NewsSpecialityInfo item = new NewsSpecialityInfo();
            DataRow Row = dt.Rows[0];
            item.iId = (int)Row["iId"];
            item.iSiteId = (int)Row["iSiteId"];
            item.iParent = (int)Row["iParent"];
            item.vcTitle = (string)Row["vcTitle"];
            item.vcExplain = (string)Row["vcExplain"];
            item.dUpDateDate = (DateTime)Row["dUpDateDate"];
            dt.Clear();
            dt.Dispose();
            dt = null;
            Row.Delete();
            return item;
        }

        /// <summary>
        /// ���������Դ����
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public DataTable GetAllNewsSpecialityInfo(Connection conn)
        {
            conn.Dblink = DBLinkNums.News;
            string SQL = "SELECT iId,iSiteId,iParent,vcTitle,vcExplain,dUpDateDate FROM T_News_Speciality (NOLOCK)";
            return conn.GetDataTable(SQL);
        }

        public DataTable GetAllNewsSpecialityInfoByCache(Connection conn, bool readdb)
        {
            if (readdb)
            {
                return this.GetAllNewsSpecialityInfo(conn);
            }
            else
            {
                DataTable dt = (DataTable)CachingService.Get("AllNewsSpeciality");
                if (dt == null)
                {
                    dt = this.GetAllNewsSpecialityInfo(conn);
                    CachingService.Set("AllNewsSpeciality", dt, null);
                }
                return dt;
            }
        }
    }
}