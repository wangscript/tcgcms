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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TCG.DBHelper
{
    public interface IConnection
    {
        /// <summary>
        /// 执行SQL语句，返回结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        void Execute(string sql);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        void Execute(string procName, SqlParameter[] parameters);

        /// <summary>
        /// 执行存储过程返回输出参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="parInt"></param>
        /// <returns></returns>
        string[] Execute(string procName, SqlParameter[] parameters, int[] parInt);

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet DataSet(string sql);

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable DataTable(string sql);

        /// <summary>
        /// 执行存储过程返回记录集
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet DataSet(string procName, SqlParameter[] parameters);

        /// <summary>
        ///  执行存储过程返回记录集 和输出参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="parInt"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        string[] DataSet(string procName, SqlParameter[] parameters, int[] parInt, ref DataSet ds);

        /// <summary>
        /// 设置链接字符串
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        void SetConnStr(string conn);
    }
}