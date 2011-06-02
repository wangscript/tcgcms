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
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;

namespace TCG.DBHelper
{
    public class AccEss : IConnection
    {
        /// <summary>
        /// 设置链接字符串
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public void SetConnStr(string conn)
        {
            this.ConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + conn;
        }


        /// <summary>
        /// 执行SQL语句，返回结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public void Execute(string sqlstr)
        {
            try
            {
                this.connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connClose();
            }
        }

        /// <summary>
        /// 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>object 返回值 </returns>
        public object ExecuteScalar(string sqlstr)
        {
            object obj = new object();
            try
            {
                this.connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                obj = comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connClose();
            }
            return obj;
        }

        /// <summary>
        /// 执行Sql查询语句,同时进行事务处理
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        public void ExecuteSqlWithTransaction(string sqlstr)
        {
            connClose();
            connOpen();

            OleDbTransaction trans;
            trans = conn.BeginTransaction();
            comm.Transaction = trans;
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                comm.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                connClose();
            }
        }

        /// <summary>
        /// 返回指定Sql语句的OleDbDataReader，请注意，在使用后请关闭本对象，同时将自动调用connClose()来关闭数据库连接
        /// 方法关闭数据库连接
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>OleDbDataReader对象</returns>
        public OleDbDataReader DataReader(string sqlstr)
        {
            OleDbDataReader dr = null;
            try
            {
                connOpen();
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    connClose();
                }
                catch
                {
                }
            }
            return dr;
        }

        /// <summary>
        /// 返回指定Sql语句的OleDbDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接
        /// 方法关闭数据库连接
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="dr">传入的ref DataReader 对象</param>
        public void DataReader(string sqlstr, ref OleDbDataReader dr)
        {
            try
            {
                connOpen();
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    if (dr != null && !dr.IsClosed)
                        dr.Close();
                }
                catch
                {
                }
                finally
                {
                    connClose();
                }
            }
        }

        /// <summary>
        /// 返回指定Sql语句的DataSet
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>DataSet</returns>
        public DataSet DataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
            return ds;
        }

        /// <summary>
        /// 返回指定Sql语句的DataSet
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="ds">传入的引用DataSet对象</param>
        public void DataSet(string sqlstr, ref DataSet ds)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
        }
        /// <summary>
        /// 返回指定Sql语句的DataTable
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>DataTable</returns>
        public DataTable DataTable(string sqlstr)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable Datatable = new DataTable();
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(Datatable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
            return Datatable;
        }

        /// <summary>
        /// 执行指定Sql语句,同时给传入DataTable进行赋值
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <param name="dt">ref DataTable dt </param>
        public void DataTable(string sqlstr, ref DataTable dt)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
        }

        /// <summary>
        /// 返回指定Sql语句的DataView
        /// </summary>
        /// <param name="sqlstr">传入的Sql语句</param>
        /// <returns>DataView</returns>
        public DataView DataView(string sqlstr)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            try
            {
                connOpen();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
            return dv;
        }

        /// <summary> 
        /// 关闭数据库 
        /// </summary> 
        public void connClose()
        {
            if (conn.State == ConnectionState.Open)
                try
                {
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("数据库关闭错误!" + ex.Message);
                }
        }

        private void connOpen()
        {
            if (conn.State == ConnectionState.Closed)
                try
                {
                    conn.ConnectionString = this.ConnStr;
                    comm.Connection = conn;
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("数据库初始化错误!!!" + ex.Message);
                }
        }

        private OleDbConnection conn = new OleDbConnection();
        private OleDbCommand comm = new OleDbCommand();
        private string ConnStr = string.Empty;


        /// <summary>
        /// 执行存储过程返回输出参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="parInt"></param>
        /// <returns></returns>
        public string[] Execute(string procName, SqlParameter[] parameters, int[] parInt)
        {
            throw new Exception("ACCESS数据库不支持该方法! string[] Execute(string procName, SqlParameter[] parameters, int[] parInt)");
        }

        /// <summary>
        /// 执行存储过程返回记录集
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet DataSet(string procName, SqlParameter[] parameters)
        {
            throw new Exception("ACCESS数据库不支持该方法! DataSet DataSet(string procName, SqlParameter[] parameters)");
        }

        /// <summary>
        ///  执行存储过程返回记录集 和输出参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="parInt"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string[] DataSet(string procName, SqlParameter[] parameters, int[] parInt, ref DataSet ds)
        {
            throw new Exception("ACCESS数据库不支持该方法! string[] DataSet(string procName, SqlParameter[] parameters, int[] parInt, ref DataSet ds");
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        public void Execute(string procName, SqlParameter[] parameters)
        {
            throw new Exception("ACCESS数据库不支持该方法!   Execute(string procName, SqlParameter[] parameters)");
        }

        /**/
        /// <summary>
        /// 获取当前页应该显示的记录，注意：查询中必须包含名为ID的自动编号列，若不符合你的要求，就修改一下源码吧 :)
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        public DataTable ExecutePager(int pageIndex, int pageSize, string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";
            string myVw = string.Format(" {0} tempVw ", queryString);

            string text1 = string.Format(" select count(0) as recordCount from {0} WHERE {1}", myVw, whereString);

            recordCount = Convert.ToInt32(this.ExecuteScalar(text1));

            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            

            string sql111 =string.Empty;
            if (pageIndex == 1)//第一页
            {
                sql111 = string.Format("select top {0} {1} from {2} WHERE {3} order by {4} ", pageSize, showString, myVw, whereString, orderString);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                sql111 = string.Format("select top {0} {1} from {2} WHERE {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString);
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                string recordIDs = recordID(string.Format("select top {0} {1} from {2} WHERE {3} order by {4} ", pageLowerBound, "ID", myVw, whereString, orderString), pageUpperBound);
                sql111 = string.Format("select {0} from {1} where id in ({2}) order by {3} ", showString, myVw, recordIDs, orderString);  

            }

            return this.DataTable(sql111);
        }

        private string recordID(string query, int passCount)
        {
            connOpen();
            string result = string.Empty;
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;
            using (IDataReader dr = comm.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        result += "," + dr.GetInt32(0);
                    }
                    passCount--;
                }
            }
            connClose();
            return result.Substring(1);
        }

    }
}