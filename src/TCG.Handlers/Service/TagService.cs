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
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using TCG.Data;

using TCG.Entity;
using TCG.Utils;
namespace TCG.Handlers
{
    public class TagService : TCGTagBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TagService(HandlerService handlerservice)
        {
            base.handlerService = handlerservice;
        }

        /// <summary>
        /// 提供对模版处理操作的方法
        /// </summary>
        public TCGTagHandlers tcgTagHandlers
        {
            get
            {
                if (this._tcgtaghandlers == null)
                {
                    this._tcgtaghandlers = new TCGTagHandlers(base.handlerService);
                }
                return this._tcgtaghandlers;
            }
        }

        public int CreateResourcHtmlById(ref string errText, int id)
        {
            if (id <= 0) return -1000000308;

            Resources item = tcgTagHandlers.handlerService.resourcsService.resourcesHandlers.GetResourcesById(id);
            if (item == null) return -1000000309;

            if (item.vcUrl.IndexOf(".") > -1) return -1000000310;

            int rtn = 0;
            tcgTagHandlers.Template = item.Categorie.ResourceTemplate.Content.Replace("_$Id$_", id.ToString());
            tcgTagHandlers.FilePath = HttpContext.Current.Server.MapPath("~" + item.vcFilePath);
            tcgTagHandlers.WebPath = item.vcFilePath;


            string text1 = string.Empty;

            if (tcgTagHandlers.Replace())
            {
                item.cCreated = "Y";
                item.cChecked = "Y";
                rtn = tcgTagHandlers.handlerService.resourcsService.resourcesHandlers.UpdateResources(item);
            }
            else
            {
                rtn = -1000000311;
            }

            return 1;
        }

        private TCGTagHandlers _tcgtaghandlers;
        private HandlerService _handlerservice;
    }
}
