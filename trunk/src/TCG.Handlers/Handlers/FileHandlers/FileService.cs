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
    public class FileService : ObjectHandlersBase
    {

        public FileService()
        {
        }

        /// <summary>
        /// 提供文件分类操作的方法
        /// </summary>
        public FileCategoriesHandlers fileClassHandlers
        {
            get
            {
                if (this._fileclasshandlers == null)
                {
                    this._fileclasshandlers = new FileCategoriesHandlers();
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
                }
                return this._fileInfohandlers;
            }
        }
        private FileResourcesHandlers _fileInfohandlers;

    }
}
