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
using System.Linq;
using System.Text;
using TCG.Entity;

namespace TCG.Handlers
{
    public interface ITemplateHandlers
    {


        Dictionary<string, EntityBase> GetAllTemplatesEntity();

        Template GetTemplateByID(string templateid);


        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetTemplatesByTemplateType(TemplateType templatetype,string skinid);

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        Dictionary<string, EntityBase> GetTemplates(string skinid, string parentid, int[] templatetypes);

        string GetTemplatePagePatch(string tid);

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int AddTemplate(Template item, Admin admin);

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int AddTemplateForXml(Template item, Admin admin);

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int DelTemplate(string temps, Admin admin);

        /// <summary>
        /// 修改模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int MdyTemplate(Template item, Admin admin);

        /// <summary>
        /// 从XML中更新模板
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int UpdateTemplateFromXML(string skinid, Admin admin);

        /// <summary>
        /// 创建皮肤模板文件 
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        int CreateTemplateToXML( Admin admin,string skinid);


        /// <summary>
        /// 得到模板类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        TemplateType GetTemplateType(int type);
    }
}