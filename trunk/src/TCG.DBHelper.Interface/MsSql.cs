using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TCG.DBHelper
{
    public class MsSql : IConnection
    {

        private string is_ConnectionString;
        private bool ib_ConnInd = false; //应用程序是否控制连接（默认为“否”，系统自动生成连接 ）


        private bool ib_TranInd = true; //应用程序是否控制事务（默认为“是”，系统不自动生成事务 但应用程序加事务必须控制连接）


        private bool ib_TranBegin = false; //判断事务是否已开始


        private int ii_CommandTimeOut = 60; //数据库连接超时时间 执行超时 (单位：秒)
        private string ls_ConnectionString;


        private SqlConnection iconn_SQL;
        private SqlTransaction itran_SQL;
        private ArrayList iar_Param = new ArrayList();

        public struct StruParam
        {
            //保存存储过程参数结构
            public string Name;
            public SqlDbType DataType;
            public int Length;
            public ParameterDirection Direction;
            public string Value;
        }

        /// <summary>
        /// 设置链接字符串
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public void SetConnStr(string conn)
        {
            this.ls_ConnectionString = conn;
        }


        /// <summary>
        /// 连接串属性

        /// </summary>
        public string p_ConnectionString
        {
            get { return is_ConnectionString; }
            set { is_ConnectionString = value; }
        }

        /// <summary>
        /// 应用程序是否控制连接（默认为“否”，系统自动生成连接 ）


        /// </summary>
        public bool p_ConnInd
        {
            get { return ib_ConnInd; }
            set { ib_ConnInd = value; }
        }

        /// <summary>
        /// 应用程序是否控制事务（默认为“是”，系统不自动生成事务）
        /// </summary>
        public bool p_TranInd
        {
            get { return ib_TranInd; }
            set { ib_TranInd = value; }
        }

        /// <summary>
        /// 打开数据库连接

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ConnOpen(ref string as_ErrText)
        {
            if (ls_ConnectionString == "")
            {
                as_ErrText = "SQL连接串为空";
                return -190010001; //SQL连接串为空

            }
            try
            {
                iconn_SQL = new SqlConnection(ls_ConnectionString);
                iconn_SQL.Open();
            }
            catch (Exception ex)
            {
                as_ErrText = "数据库操作错误@@@@m_ConnOpen：" + ls_ConnectionString + " ERROR:" + ex.Message;
                return -190010002;
            }
            return 1;
        }

        /// <summary>
        /// 关闭数据库连接

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ConnClose(ref string as_ErrText)
        {
            if (iconn_SQL == null)
            {
                as_ErrText = "数据库连接未初始化";
                return -190010003; //数据库连接未初始化		
            }
            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }
            try
            {
                iconn_SQL.Close();
            }
            catch (Exception ex)
            {
                as_ErrText = "数据库操作错误@@@@m_ConnClose：" + ex.Message;
                return -190010002;
            }
            return 1;
        }

        /// <summary>
        /// 开始事务

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_TranBegin(ref string as_ErrText)
        {
            if (iconn_SQL == null)
            {
                as_ErrText = "数据库连接未初始化";
                return -190010003; //数据库连接未初始化		
            }
            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if (ib_TranBegin == true)
            {
                as_ErrText = "数据库已开始事务";
                return -190010009;
            }
            try
            {
                itran_SQL = iconn_SQL.BeginTransaction();
            }
            catch (Exception ex)
            {
                as_ErrText = "数据库操作错误@@@@m_TranBegin：" + ex.Message;
                return -190010002;
            }
            ib_TranBegin = true;
            return 1;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_TranRollback(ref string as_ErrText)
        {
            if (iconn_SQL == null)
            {
                as_ErrText = "数据库连接未初始化";
                return -190010003; //数据库连接未初始化		
            }
            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if (ib_TranBegin == false)
            {
                as_ErrText = "数据库未开始事务";
                return -190010010;
            }
            try
            {
                itran_SQL.Rollback();
            }
            catch (Exception ex)
            {
                as_ErrText = "数据库操作错误@@@@m_TranRollback：" + ex.Message;
                return -190010002;
            }
            ib_TranBegin = false;
            return 1;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="as_ErrText"></param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_TranCommit(ref string as_ErrText)
        {
            if (iconn_SQL == null)
            {
                as_ErrText = "数据库连接未初始化";
                return -190010003; //数据库连接未初始化		
            }
            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if (ib_TranBegin == false)
            {
                as_ErrText = "数据库未开始事务";
                return -190010010;
            }

            try
            {
                itran_SQL.Commit();
            }
            catch (Exception ex)
            {
                as_ErrText = "数据库操作错误@@@@m_TranRollback：" + ex.Message;
                return -190010002;
            }
            ib_TranBegin = false;
            return 1;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <param name="as_SQL">SQL语句</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_RunSQL(ref string as_ErrText, string as_SQL)
        {
            SqlCommand lcmd_SQL = new SqlCommand();
            int li_Return;

            if (as_SQL == "")
            {
                as_ErrText = "SQL语句为空";
                return -190010005; //SQL语句为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }

            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandText = as_SQL;
                lcmd_SQL.CommandType = CommandType.Text;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                lcmd_SQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回


                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSQL：" + ex.Message + "@@@@" + as_SQL;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            return 1;
        }

        /// <summary>
        /// 执行SQL语句返回结果集

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <param name="as_SQL">SQL语句</param>
        /// <param name="ads_out_Data">返回结果集</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_RunSQLData(ref string as_ErrText, string as_SQL, ref DataSet ads_out_Data)
        {
            DataSet lds_Data = new DataSet();
            SqlCommand lcmd_SQL = new SqlCommand();
            int li_Return;

            if (as_SQL == "")
            {
                as_ErrText = "SQL语句为空";
                return -190010005; //SQL语句为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }

            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandText = as_SQL;
                lcmd_SQL.CommandType = CommandType.Text;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                SqlDataAdapter ladpt_Operation = new SqlDataAdapter(lcmd_SQL);
                ladpt_Operation.Fill(lds_Data);
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回

                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSQLData：" + ex.Message + "@@@@" + as_SQL;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            ads_out_Data = lds_Data;
            return 1;
        }


        /// <summary>
        /// 执行SQL语句返回结果集的第一个字段

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <param name="as_SQL">SQL语句</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ExecuteScalar(ref string as_ErrText, string as_SQL, ref string as_out_Column)
        {
            SqlCommand lcmd_SQL = new SqlCommand();
            int li_Return;

            if (as_SQL == "")
            {
                as_ErrText = "SQL语句为空";
                return -190010005; //SQL语句为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }

            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandText = as_SQL;
                lcmd_SQL.CommandType = CommandType.Text;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                as_out_Column = lcmd_SQL.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回

                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSQLData：" + ex.Message + "@@@@" + as_SQL;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            return 1;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <param name="as_SPName">存储过程名称</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_RunSP(ref string as_ErrText, string as_SPName)
        {
            int li_Return, li_ParamCount, li_ParamNum;
            SqlCommand lcmd_SQL = new SqlCommand();
            SqlParameter lparam_SQL;
            StruParam lstru_Param;

            if (as_SPName == "")
            {
                as_ErrText = "存储过程名称为空";
                return -190010006; //存储过程名称为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004;     //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }

            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandType = CommandType.StoredProcedure;
                lcmd_SQL.CommandText = as_SPName;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = 0; li_ParamNum < li_ParamCount; li_ParamNum++)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        lparam_SQL = new SqlParameter();
                        lparam_SQL = lcmd_SQL.Parameters.Add(lstru_Param.Name, lstru_Param.DataType, lstru_Param.Length);
                        lparam_SQL.Direction = lstru_Param.Direction;
                        lparam_SQL.Value = lstru_Param.Value;
                        /*
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lparam_SQL.Direction = lstru_Param.Direction;
                            lparam_SQL.Value = lstru_Param.Value;
                        }
                        else
                        {
                            lparam_SQL.Value = lstru_Param.Value;
                        }
                        */
                    }
                }

                lcmd_SQL.ExecuteNonQuery();

                //取返回参数


                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = li_ParamCount - 1; li_ParamNum >= 0; li_ParamNum--)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lstru_Param.Value = lcmd_SQL.Parameters[lstru_Param.Name].Value.ToString();
                            iar_Param.RemoveAt(li_ParamNum);
                            iar_Param.Add(lstru_Param);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回


                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSP：" + ex.Message + "@@@@" + as_SPName;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            return 1;
        }

        /// <summary>
        /// 执行存储过程返回结果集

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="ai_ConnectID">数据库编号</param>
        /// <param name="as_SPName">存储过程名称</param>
        /// <param name="ads_out_Data">返回结果集</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_RunSPData(ref string as_ErrText, string as_SPName, ref DataSet ads_out_Data)
        {
            int li_Return, li_ParamCount, li_ParamNum;
            SqlCommand lcmd_SQL = new SqlCommand();
            DataSet lds_Data = new DataSet();
            SqlParameter lparam_SQL;
            StruParam lstru_Param;

            if (as_SPName == "")
            {
                as_ErrText = "存储过程名称为空";
                return -190010006; //存储过程名称为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }

            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandType = CommandType.StoredProcedure;
                lcmd_SQL.CommandText = as_SPName;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = 0; li_ParamNum < li_ParamCount; li_ParamNum++)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        lparam_SQL = new SqlParameter();
                        lparam_SQL = lcmd_SQL.Parameters.Add(lstru_Param.Name, lstru_Param.DataType, lstru_Param.Length);
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lparam_SQL.Direction = lstru_Param.Direction;
                        }
                        else
                        {
                            lparam_SQL.Value = lstru_Param.Value;
                        }
                    }
                }

                SqlDataAdapter ladapt_Operation = new SqlDataAdapter(lcmd_SQL);
                ladapt_Operation.Fill(lds_Data);

                //取返回参数


                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = li_ParamCount - 1; li_ParamNum >= 0; li_ParamNum--)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lstru_Param.Value = lcmd_SQL.Parameters[lstru_Param.Name].Value.ToString();
                            iar_Param.RemoveAt(li_ParamNum);
                            iar_Param.Add(lstru_Param);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回


                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSPData：" + ex.Message + "@@@@" + as_SPName;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            ads_out_Data = lds_Data;
            return 1;
        }

        /// <summary>
        /// 添加存储过程参数
        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="as_ParamName">参数名</param>
        /// <param name="adt_DataType">数据类型</param>
        /// <param name="apd_Direction">输入输出类型</param>
        /// <param name="ai_DataLength">参数长度</param>
        /// <param name="as_ParamValue">参数值</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ParamAdd(ref string as_ErrText, string as_ParamName, SqlDbType adt_DataType, ParameterDirection apd_Direction, int ai_DataLength, string as_ParamValue)
        {
            if (as_ParamName == "")
            {
                as_ErrText = "存储过程参数名称为空";
                return -190010007; //存储过程参数名称为空		
            }
            StruParam lstru_Param;
            lstru_Param.Name = as_ParamName;
            lstru_Param.DataType = adt_DataType;
            lstru_Param.Direction = apd_Direction;
            lstru_Param.Length = ai_DataLength;
            lstru_Param.Value = as_ParamValue;
            iar_Param.Add(lstru_Param);
            as_ErrText = "";
            return 1;
        }

        /// <summary>
        /// 清空原有的存储过程参数定义

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int ClearSqlParams(ref string as_ErrText)
        {
            int li_ParamCount;
            li_ParamCount = iar_Param.Count;
            if (li_ParamCount > 0)
            {
                iar_Param.RemoveRange(0, li_ParamCount);
            }

            as_ErrText = "";
            return 1;
        }

        /// <summary>
        /// 得到存储过程参数值

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="as_ParamName">参数名</param>
        /// <param name="as_ParamValue">参数值</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ParamGet(ref string as_ErrText, string as_ParamName, ref string as_ParamValue)
        {
            int li_ParamCount, li_ParamNum;
            StruParam lstru_Param;

            li_ParamCount = iar_Param.Count;
            if (li_ParamCount < 1)
            {
                as_ErrText = "存储过程参数名称为空";
                return -190010007; //存储过程参数名称为空
            }
            for (li_ParamNum = 0; li_ParamNum < li_ParamCount; li_ParamNum++)
            {
                lstru_Param = (StruParam)iar_Param[li_ParamNum];
                if (lstru_Param.Name == as_ParamName)
                {
                    as_ParamValue = lstru_Param.Value;
                    as_ErrText = "";
                    return 1;
                }
            }
            as_ErrText = "存储过程没有指定参数";
            return -190010008; //存储过程没有指定参数
        }

        /// <summary>
        /// 得到所有存储过程参数值（暂时）

        /// </summary>
        /// <param name="as_ErrText">错误信息</param>
        /// <param name="as_Value">参数值（以@@@@分隔参数，以&&&&分隔参数名和参数值）</param>
        /// <returns>成功返回1 失败小于0</returns>
        public int m_ParamAllGet(ref string as_ErrText, ref string as_Value)
        {
            int li_ParamCount, li_ParamNum;
            StruParam lstru_Param;
            as_Value = "";

            li_ParamCount = iar_Param.Count;
            if (li_ParamCount < 1)
            {
                as_ErrText = "存储过程参数名称为空";
                return -190010007; //存储过程参数名称为空
            }
            for (li_ParamNum = 0; li_ParamNum < li_ParamCount; li_ParamNum++)
            {
                lstru_Param = (StruParam)iar_Param[li_ParamNum];
                as_Value = as_Value + lstru_Param.Name + "&&&&" + lstru_Param.Value + "@@@@";
            }
            as_ErrText = "";
            return 1;
        }


        public int m_RunSQLDataWithPara(ref string as_ErrText, string as_SQL, ref DataSet ads_out_Data)
        {
            int li_Return, li_ParamCount, li_ParamNum;
            SqlCommand lcmd_SQL = new SqlCommand();
            DataSet lds_Data = new DataSet();
            SqlParameter lparam_SQL;
            StruParam lstru_Param;

            if (as_SQL == "")
            {
                as_ErrText = "SQL语句为空";
                return -190010005; //SQL语句为空
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnOpen(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (iconn_SQL.State != ConnectionState.Open)
            {
                as_ErrText = "数据库连接未打开";
                return -190010004; //数据库连接未打开
            }

            if ((ib_TranInd == false) && (ib_TranBegin == false)) //不控制事务
            {
                li_Return = m_TranBegin(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }
            if ((ib_TranBegin == true) && (itran_SQL == null))
            {
                as_ErrText = "数据库事务未开始";
                return -190010011;
            }


            try
            {
                lcmd_SQL.Connection = iconn_SQL;
                lcmd_SQL.CommandText = as_SQL;
                lcmd_SQL.CommandType = CommandType.Text;
                lcmd_SQL.CommandTimeout = ii_CommandTimeOut;
                if (itran_SQL != null) lcmd_SQL.Transaction = itran_SQL;

                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = 0; li_ParamNum < li_ParamCount; li_ParamNum++)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        lparam_SQL = new SqlParameter();
                        lparam_SQL = lcmd_SQL.Parameters.Add(lstru_Param.Name, lstru_Param.DataType, lstru_Param.Length);
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lparam_SQL.Direction = lstru_Param.Direction;
                        }
                        else
                        {
                            lparam_SQL.Value = lstru_Param.Value;
                        }
                    }
                }

                SqlDataAdapter ladapt_Operation = new SqlDataAdapter(lcmd_SQL);
                ladapt_Operation.Fill(lds_Data);

                //取返回参数


                if (iar_Param.Count > 0)
                {
                    li_ParamCount = iar_Param.Count;
                    for (li_ParamNum = li_ParamCount - 1; li_ParamNum >= 0; li_ParamNum--)
                    {
                        lstru_Param = (StruParam)iar_Param[li_ParamNum];
                        if (lstru_Param.Direction == ParameterDirection.Output)
                        {
                            lstru_Param.Value = lcmd_SQL.Parameters[lstru_Param.Name].Value.ToString();
                            iar_Param.RemoveAt(li_ParamNum);
                            iar_Param.Add(lstru_Param);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ib_TranInd == false) //不控制事务
                {
                    li_Return = m_TranRollback(ref as_ErrText); //对于系统自动回滚，不影响正常的返回，所以不处理其返回

                }
                if (ib_ConnInd == false) //不控制连接
                {
                    li_Return = m_ConnClose(ref as_ErrText);
                }
                as_ErrText = "数据库操作错误@@@@m_RunSPData：" + ex.Message + "@@@@" + as_SQL;
                return -190010002;
            }
            lcmd_SQL.Dispose();

            if (ib_TranInd == false) //不控制事务
            {
                li_Return = m_TranCommit(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            if (ib_ConnInd == false) //不控制连接
            {
                li_Return = m_ConnClose(ref as_ErrText);
                if (li_Return < 0)
                {
                    return li_Return;
                }
            }

            ads_out_Data = lds_Data;
            return 1;
        }

        public DataTable ExecutePager(int pageIndex, int pageSize, string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            string errText = string.Empty;

            this.ClearSqlParams(ref errText);
            int rtn = this.m_ParamAdd(ref errText, "@tblName", SqlDbType.NVarChar, ParameterDirection.Input, 500, queryString);
            rtn = this.m_ParamAdd(ref errText, "@fldName", SqlDbType.NVarChar, ParameterDirection.Input, 500, showString);
            rtn = this.m_ParamAdd(ref errText, "@fldSort", SqlDbType.NVarChar, ParameterDirection.Input, 200, orderString);
            rtn = this.m_ParamAdd(ref errText, "@strCondition", SqlDbType.NVarChar, ParameterDirection.Input, 4000, whereString);
            rtn = this.m_ParamAdd(ref errText, "@pageSize", SqlDbType.Int, ParameterDirection.Input, 4, pageSize.ToString());
            rtn = this.m_ParamAdd(ref errText, "@page", SqlDbType.Int, ParameterDirection.Input, 4, pageIndex.ToString());

            rtn = this.m_ParamAdd(ref errText, "@curpage", SqlDbType.Int, ParameterDirection.Output, 4, "");
            rtn = this.m_ParamAdd(ref errText, "@pageCount", SqlDbType.Int, ParameterDirection.Output, 4, "");
            rtn = this.m_ParamAdd(ref errText, "@counts", SqlDbType.Int, ParameterDirection.Output, 4, "");
            rtn = this.m_ParamAdd(ref errText, "@retval", SqlDbType.Int, ParameterDirection.Output, 4, "");

            string _val = string.Empty, _curpage = string.Empty, _pageCount = string.Empty, _counts = string.Empty;
            DataSet ds = null;
            rtn = this.m_RunSPData(ref errText, "SP_TCG_GetPage",ref ds);
            if (rtn == 1)
            {
                rtn = this.m_ParamGet(ref errText, "@retval", ref _val);
                if (rtn < 0)
                {
                    pageCount = 0;
                    recordCount = 0;
                    return null;
                }
                rtn = this.m_ParamGet(ref errText, "@curpage", ref _curpage);
                if (rtn < 0)
                {
                    pageCount = 0;
                    recordCount = 0;
                    return null;
                }
                rtn = this.m_ParamGet(ref errText, "@pageCount", ref _pageCount);
                if (rtn < 0)
                {
                    pageCount = 0;
                    recordCount = 0;
                    return null;
                }
                rtn = this.m_ParamGet(ref errText, "@counts", ref _counts);
                if (rtn < 0)
                {
                    pageCount = 0;
                    recordCount = 0;
                    return null;
                }

                pageCount = int.Parse(_pageCount);
                recordCount = int.Parse(_counts);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }

            pageCount = 0;
            recordCount = 0;
            return null;

        }

    }
}
