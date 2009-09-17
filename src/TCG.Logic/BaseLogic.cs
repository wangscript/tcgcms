/*
 * Copyright (C) 2009-2009 Q2Mx <http://cms.q4mx.com/>
 * 
 *    本代码以公共的方式开发下载，任何个人和组织可以下载，
 * 修改，进行第二次开发使用，但请保留作者版权信息。
 * 
 *    任何个人或组织在使用本软件过程中造成的直接或间接损失，
 * 需要自行承担后果与本软件开发者(勤奋猪)无关。
 * 
 *    本软件解决中小型商家产品网络化销售方案。
 *    
 *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com
 */

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using TCG.Data;

namespace TCG.Logic
{
    /// <summary>
    /// 数据处理逻辑层基类
    /// </summary>
    public class BaseLogic : IDisposable
    {
        /// <summary>
        /// 数据访问方法，页面从该处调用
        /// </summary>
        protected Connection conn
        {
            get
            {
                if (this._conn == null)
                {
                    this._conn = new Connection();
                    this._conn.Dblink = DBLinkNums.Manage;
                }
                return this._conn;
            }
        }

        /// <summary>
        /// 清理数据处理内存
        /// </summary>
        protected int Clear()
        {
            if (this._cacheDs != null)
            {
                this._cacheDs.Dispose();
                this._cacheDs = null;
            }
            this._revalue = 0;
            this._errtext = null;
            return 1;
        }

        /// <summary>
        /// IDisposable的实现
        /// 调用虚拟的Dispose方法。禁止Finalization（终结操作）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// 虚函数，处理资源回收 
        /// </summary>
        /// <param name="isDisposing">是否释放资源</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this._cacheDs != null)
                {
                    this._cacheDs.Dispose();
                    this._cacheDs = null;
                }
                if (this._conn != null)
                {
                    this.conn.Close();
                }
                this._conn = null;
                this._revalue = 0;
                this._errtext = null;
            }
        }

        /// <summary>
        /// 获得命名空间
        /// </summary>
        /// <param name="servicetype"></param>
        /// <returns></returns>
        protected string GetServiceNameByType(Type servicetype)
        {
            string text = servicetype.Namespace;
            string[] text1 = text.Split('.');
            return text1[text1.Length - 1];
        }

        /// <summary>
        /// 数据访问实体
        /// </summary>
        private Connection _conn;

        /// <summary>
        /// 返回参数
        /// </summary>
        protected int _revalue = 0;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string _errtext = "";
        /// <summary>
        /// 暂存数据集
        /// </summary>
        protected DataSet _cacheDs = null;
    }
}
