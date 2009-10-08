/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三晕鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

namespace TCG.Data
{
    using TCG.Utils;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Web;

    public class Connection
    {
        private string[] ConnStr = new string[30];   //DB连接串数组
        public Connection()
        {
            ConnStr[0] = "server=127.0.0.1;Database=Manage86865;User Id=sa;Password=woziji112;";
            ConnStr[1] = ConnStr[0];
            ConnStr[2] = ConnStr[0];
            ConnStr[3] = ConnStr[0];
            ConnStr[4] = ConnStr[0];
            ConnStr[5] = ConnStr[0];
            ConnStr[6] = ConnStr[0];

            this.Initialize();
        }

        public virtual void Close()
        {
            if (this._connected)
            {
                this._conn.Close();
                this._conn.Dispose();
                this._connected = false;
                if (this._trace)
                {
                    HttpContext.Current.Response.Write("Close ");
                }
            }
        }

        public string DbDateAdd(string unit, int minuend, string datetimeValue)
        {
            if (this._provider == DbProviderEnum.Access)
            {
                return string.Concat(new object[] { "dateadd('", unit, "', ", minuend, ", ", datetimeValue, ")" });
            }
            return string.Concat(new object[] { "dateadd(", unit, ", ", minuend, ", ", datetimeValue, ")" });
        }

        public string DbDateDiff(string unit, string datetimeValue1, string datetimeValue2)
        {
            if (this._provider == DbProviderEnum.Access)
            {
                return ("datediff('" + unit + "', " + datetimeValue1 + ", " + datetimeValue2 + ")");
            }
            return ("datediff(" + unit + ", " + datetimeValue1 + ", " + datetimeValue2 + ")");
        }

        public string DbDateVar(string datetimeValue)
        {
            if (this._provider == DbProviderEnum.Access)
            {
                return ("#" + datetimeValue + "#");
            }
            return ("'" + datetimeValue + "'");
        }

        public int Execute(string sql)
        {
            if (this._trace)
            {
                HttpContext.Current.Response.Write(sql + "<br />");
            }
            this.Open();
            this._queries = (byte) (this._queries + 1);
            IDbCommand command1 = this.m_factory.CreateCommand(sql, this._conn);
            try
            {
                return Convert.ToInt32(command1.ExecuteNonQuery());
            }
            catch (Exception exception1)
            {
                this.Close();
                if (!exception1.Message.StartsWith("无法从指定的数据表中删除") && !exception1.Message.StartsWith("操作必须使用一个可更新的查询"))
                {
                    throw exception1;
                }
                new Terminator().Throw("没有足够的权限对Access数据库进行操作。<br /><br />请设置论坛中的 files 目录的文件系统权限（文件夹属性中的安全选项卡），添加.NET用户（默认地，Win2K为ASPNET用户，Win2003为IIS_WPG用户组），并且授予完全控制权限。");
                return 0;
            }
        }

        public void Execute(string procName, SqlParameter[] parameters)
        {
            this.Open();
            this._queries = (byte) (this._queries + 1);
            SqlCommand command1 = new SqlCommand(procName, this._conn as SqlConnection);
            command1.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter1 in parameters)
            {
                command1.Parameters.Add(parameter1);
            }
            command1.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行带返回参数的过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="parInt"></param>
        /// <returns></returns>
        public string[] Execute(string procName, SqlParameter[] parameters, int[] parInt)
        {
            this.Execute(procName, parameters);
            if (parInt != null)
            {
                string[] s = new string[parInt.Length];
                for (int i = 0; i < parInt.Length; i++)
                {
                    s[i] = parameters[parInt[i]].Value.ToString();
                }
                return s;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetDataTable(string sql)
        {
            if (this._trace)
            {
                HttpContext.Current.Response.Write(sql + "<br />");
            }
            this.Open();
            this._queries = (byte) (this._queries + 1);
            IDbDataAdapter adapter1 = this.m_factory.CreateDataAdapter(sql, this._conn);
            DataSet set1 = new DataSet();
            adapter1.Fill(set1);
            return set1.Tables[0];
        }

        public DataTable GetDataTable(string procName, SqlParameter[] parameters)
        {
            this.Open();
            this._queries = (byte) (this._queries + 1);
            SqlDataAdapter adapter1 = new SqlDataAdapter(procName, this._conn as SqlConnection);
            adapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter1 in parameters)
            {
                adapter1.SelectCommand.Parameters.Add(parameter1);
            }
            DataSet set1 = new DataSet();
            adapter1.Fill(set1);
            return set1.Tables[0];
        }

        public IDataReader GetReader(string sql)
        {
            if (this._trace)
            {
                HttpContext.Current.Response.Write(sql + "<br />");
            }
            this.Open();
            this._queries = (byte) (this._queries + 1);
            return this.m_factory.CreateCommand(sql, this._conn).ExecuteReader();
        }

        public IDataReader GetReader(string procName, SqlParameter[] parameters)
        {
            this.Open();
            this._queries = (byte) (this._queries + 1);
            SqlCommand command1 = new SqlCommand(procName, this._conn as SqlConnection);
            command1.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter1 in parameters)
            {
                command1.Parameters.Add(parameter1);
            }
            return command1.ExecuteReader();
        }

        public object GetScalar(string sql)
        {
            if (this._trace)
            {
                HttpContext.Current.Response.Write(sql + "<br />");
            }
            this.Open();
            this._queries = (byte) (this._queries + 1);
            return this.m_factory.CreateCommand(sql, this._conn).ExecuteScalar();
        }

        public object GetScalar(string procName, SqlParameter[] parameters)
        {
            this.Open();
            this._queries = (byte) (this._queries + 1);
            SqlCommand command1 = new SqlCommand(procName, this._conn as SqlConnection);
            command1.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter1 in parameters)
            {
                command1.Parameters.Add(parameter1);
            }
            return command1.ExecuteScalar();
        }

        public DataSet GetDataSet(string sql)
        {
            return this.GetDataSet(sql, "");
        }

        public DataSet GetDataSet(string sql, string name)
        {
            this.Open();
            this._queries++;
            SqlDataAdapter da = new SqlDataAdapter(sql, this._conn as SqlConnection);
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(name))
                da.Fill(ds);
            else
                da.Fill(ds, name);
            return ds;
        }

        public DataSet GetDataSet(string procName, SqlParameter[] parameters)
        {//执行过程
            this.Open();
            this._queries++;
            SqlDataAdapter da = new SqlDataAdapter(procName, this._conn as SqlConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter1 in parameters)
                {
                    da.SelectCommand.Parameters.Add(parameter1);
                }
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables.Count <= 0)
            {
                da.Dispose();
                ds.Dispose();
                ds.Clear();
                return null;
            }
            return ds;
        }

        public DataSet GetDataSet(string procName, SqlParameter[] parameters, string name)
        {//执行过程
            this.Open();
            this._queries++;
            SqlDataAdapter da = new SqlDataAdapter(procName, this._conn as SqlConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter1 in parameters)
                {
                    da.SelectCommand.Parameters.Add(parameter1);
                }
            }
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(name))
                da.Fill(ds);
            else
                da.Fill(ds, name);
            if (ds.Tables.Count <= 0)
            {
                da.Dispose();
                ds.Dispose();
                ds.Clear();
                return null;
            }
            return ds;
        }

        public string[] GetDataSet(string procName, SqlParameter[] parameters, int[] parInt,ref DataSet ds)
        {
            this.Open();
            this._queries++;
            SqlDataAdapter da = new SqlDataAdapter(procName, this._conn as SqlConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter1 in parameters)
                {
                    da.SelectCommand.Parameters.Add(parameter1);
                }
            }
            
            da.Fill(ds);

            if (ds.Tables.Count <= 0)
            {
                da.Dispose();
                ds.Dispose();
                ds.Clear();
            }

            if (parInt != null)
            {
                string[] s = new string[parInt.Length];
                for (int i = 0; i < parInt.Length; i++)
                {
                    s[i] = parameters[parInt[i]].Value.ToString();
                }
                return s;
            }

            return null;
        }

        public void Open()
        {
            if (!this._connected)
            {
                this._conn = this.m_factory.CreateConnection(this.m_dbLink);
                try
                {
                    this._conn.Open();
                    this._connected = true;
                }
                catch (Exception exception1)
                {
                    new Terminator().Throw("数据库连接失败: " + exception1.Message);
                }
                if (this._trace)
                {
                    HttpContext.Current.Response.Write("Open ");
                }
            }
        }

        private void Initialize()
        { 
            this._provider = DbProviderEnum.SqlServer;
            this.m_factory = new SqlDbFactory();
            
            this._queries = 0;
            this._connected = false;
        }

        public int Dblink
        {
            set
            {
                if (this.m_dbLink != ConnStr[value])
                {
                    //this.Close();
                    this.m_dbLink = ConnStr[value];
                }
            }
        }

        public bool Connected
        {
            get
            {
                return this._connected;
            }
        }

        public string DbDateFunc
        {
            get
            {
                return "getdate()";
            }
        }

        public IDbConnection IDbConnectionInstance
        {
            get
            {
                return this._conn;
            }
        }

        public DbProviderEnum Provider
        {
            get
            {
                return this._provider;
            }
        }

        public byte Queries
        {
            get
            {
                return this._queries;
            }
        }

        public bool Trace
        {
            get
            {
                return this._trace;
            }
            set
            {
                this._trace = value;
            }
        }

        private IDbConnection _conn;
        private bool _connected;
        private DbProviderEnum _provider;
        private byte _queries;
        private bool _trace;
        private string m_dbLink;
        private DbFactory m_factory;


        internal abstract class DbFactory
        {
            protected DbFactory()
            {
            }

            public abstract IDbCommand CreateCommand(string sql, IDbConnection conn);
            public abstract IDbConnection CreateConnection(string connstr);
            public abstract IDbDataAdapter CreateDataAdapter(string sql, IDbConnection conn);
        }

        internal class OleDbFactory : Connection.DbFactory
        {
            public override IDbCommand CreateCommand(string sql, IDbConnection conn)
            {
                return new OleDbCommand(sql, (OleDbConnection) conn);
            }

            public override IDbConnection CreateConnection(string connstr)
            {
                return new OleDbConnection(connstr);
            }

            public override IDbDataAdapter CreateDataAdapter(string sql, IDbConnection conn)
            {
                return new OleDbDataAdapter(sql, (OleDbConnection) conn);
            }

        }

        internal class SqlDbFactory : Connection.DbFactory
        {
            public override IDbCommand CreateCommand(string sql, IDbConnection conn)
            {
                return new SqlCommand(sql, (SqlConnection) conn);
            }

            public override IDbConnection CreateConnection(string connstr)
            {
                return new SqlConnection(connstr);
            }

            public override IDbDataAdapter CreateDataAdapter(string sql, IDbConnection conn)
            {
                return new SqlDataAdapter(sql, (SqlConnection) conn);
            }

        }
    }
}

