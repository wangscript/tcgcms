using System;
using System.Collections.Generic;
using System.Text;

using TCG.Utils;
using TCG.Entity;
using TCG.Data;

namespace TCG.Handlers
{
    /// <summary>
    /// 文件处理服务
    /// </summary>
    public class FileService
    {

        public FileService(ConfigService configservice, Connection conn)
        {
            this._configservice = configservice;
            this._conn = conn;
        }

        /// <summary>
        /// 提供文件分类操作的方法
        /// </summary>
        public FileCategoriesHandlers fileClassHandlers
        {
            get
            {
                if (this.MianDatabase == null) return null;
                if (this._conn == null) return null;
                if (this._fileclasshandlers == null)
                {
                    this._fileclasshandlers = new FileCategoriesHandlers();
                    this._fileclasshandlers.conn = this._conn;
                    this._fileclasshandlers.ConnStr = this.MianDatabase;
                    this._fileclasshandlers.configService = this._configservice;
                }
                return this._fileclasshandlers;
            }
        }
        private FileCategoriesHandlers _fileclasshandlers;


        /// <summary>
        /// 提供对文件操作的方法
        /// </summary>
        public FileResourcesHandlers fileInfoHandlers
        {
            get
            {
                if (this._fileInfohandlers == null)
                {
                    this._fileInfohandlers = new FileResourcesHandlers();
                    this._fileInfohandlers.fileClassHandlers = this.fileClassHandlers;
                    this._fileInfohandlers.configService = this._configservice;
                    this._fileInfohandlers.conn = this._conn;
                }
                return this._fileInfohandlers;
            }
        }
        private FileResourcesHandlers _fileInfohandlers;


        /// <summary>
        /// 获得主文件库
        /// </summary>
        /// <returns></returns>
        private string GetMainFileDatabaseStr()
        {
            if (this._configservice == null) return null;
            if (this._configservice.fileDataBaseConfig == null) return null;
            if (this._configservice.fileDataBaseConfig.Count == 0) return null;

            foreach (DataBaseConnStr database in this._configservice.fileDataBaseConfig)
            {
                if (database.IsBaseDataBase)
                {
                    return database.Value;

                }
            }
            return null;
        }

        public string MianDatabase
        {
            get
            {
                if (this._maindatabase == null)
                {
                    this._maindatabase = this.GetMainFileDatabaseStr();
                }
                return this._maindatabase;
            }
        }
        private string _maindatabase = null;

        private Connection _conn;
        private ConfigService _configservice = null;
    }
}
