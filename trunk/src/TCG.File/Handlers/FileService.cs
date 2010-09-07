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
    public class FileService : FileObjectHandlersBase
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
                    this._fileclasshandlers.configService = base.configService;
                    this._fileclasshandlers.conn = base.conn;
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
                    this._fileInfohandlers.configService = base.configService;
                    this._fileInfohandlers.conn = base.conn;
                }
                return this._fileInfohandlers;
            }
        }
        private FileResourcesHandlers _fileInfohandlers;

    }
}
