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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;



namespace TCG.Data
{
    public class DBHandlers
    {

        /// <summary>
        /// �����趨�������Ҳ�ѯ���������ҳ
        /// </summary>
        /// <param name="errText">������Ϣ</param>
        /// <param name="tableName">����</param>
        /// <param name="arrShowField">��Ҫ������ʾ���ֶμ�</param>
        /// <param name="strConditionField">��ѯ�����ֶμ�(Ϊ����in�Ĳ�ѯ����hashtable�ĳ�string)</param>
        /// <param name="arrSortField">�����ֶμ�</param>
        /// <param name="pageSize">ÿҳ��ʾ�ļ�¼����</param>
        /// <param name="page">Ҫ��ʾ��ҳ����</param>
        /// <param name="ads_out_Data">���صĽ�����ݼ�</param>
        /// <returns>�ɹ�����1 ʧ��С��0</returns>

        public static int GetPage(PageSearchItem sItem,Connection conn, ref int curPage, ref int pageCount, ref int counts, ref DataSet ads_out_Data)
        {
            //������Ҫ��ʾ���ֶμ���SQL��
            StringBuilder strShowField = new StringBuilder();
            for (int i = 0; i < sItem.arrShowField.Count; i++)
            {
                if (i == 0)
                {
                    strShowField.Append(sItem.arrShowField[i]);
                }
                else
                {
                    strShowField.Append(", " + sItem.arrShowField[i]);
                }
            }


            //��������SQL��
            StringBuilder strSortField = new StringBuilder();
            for (int i = 0; i < sItem.arrSortField.Count; i++)
            {
                if (i == 0)
                {
                    strSortField.Append(sItem.arrSortField[i]);
                }
                else
                {
                    strSortField.Append(", " + sItem.arrSortField[i]);
                }
            }

            SqlParameter sp0 = new SqlParameter("@tblName", SqlDbType.NVarChar, 50); sp0.Value = sItem.tableName;
            SqlParameter sp1 = new SqlParameter("@fldName", SqlDbType.NVarChar, 500); sp1.Value = strShowField.ToString();
            SqlParameter sp2 = new SqlParameter("@fldSort", SqlDbType.NVarChar, 200); sp2.Value = strSortField.ToString();
            SqlParameter sp3 = new SqlParameter("@strCondition", SqlDbType.NVarChar, 4000); sp3.Value = sItem.strCondition;
            SqlParameter sp4 = new SqlParameter("@pageSize", SqlDbType.Int, 4); sp4.Value = sItem.pageSize.ToString();
            SqlParameter sp5 = new SqlParameter("@page", SqlDbType.Int, 4); sp5.Value = sItem.page.ToString();

            SqlParameter sp6 = new SqlParameter("@curpage", SqlDbType.Int, 4); sp6.Direction = ParameterDirection.Output;
            SqlParameter sp7 = new SqlParameter("@pageCount", SqlDbType.Int, 4); sp7.Direction = ParameterDirection.Output;
            SqlParameter sp8 = new SqlParameter("@counts", SqlDbType.Int, 4); sp8.Direction = ParameterDirection.Output;
            SqlParameter sp9 = new SqlParameter("@retval", SqlDbType.Int, 4); sp9.Direction = ParameterDirection.Output;

            string[] reValues = conn.GetDataSet("SP_TCG_GetPage", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 }, new int[] { 6, 7, 8, 9 }, ref ads_out_Data);
            if(reValues!=null)
            {
                //�ܼ�¼��
                counts = (int)Convert.ChangeType(reValues[2], typeof(int));
                //��ҳ��
                pageCount = (int)Convert.ChangeType(reValues[1], typeof(int));
                //��ǰҳ��
                curPage = (int)Convert.ChangeType(reValues[0], typeof(int));

                return (int)Convert.ChangeType(reValues[3], typeof(int));
            }

            return -19000000;
        }
    }
}
