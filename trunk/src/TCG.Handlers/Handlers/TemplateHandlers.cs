using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Entity;
using TCG.Utils;
using System.Xml;

namespace TCG.Handlers
{
    public class TemplateHandlers 
    {
        public DataTable GetAllTemplates()
        {
            DataTable dt = (DataTable)CachingService.Get(CachingService.CACHING_All_TEMPLATES);
            if (dt == null)
            {
                dt = GetAllTemplatesWithOutCaching();
                CachingService.Set(CachingService.CACHING_All_TEMPLATES, dt, null);
            }
            return dt;
        }

        public Dictionary<string, EntityBase> GetAllTemplatesEntity()
        {
            Dictionary<string, EntityBase> templates = (Dictionary<string, EntityBase>)CachingService.Get(CachingService.CACHING_All_TEMPLATES_ENTITY);
            if (templates == null)
            {
                templates = HandlersFactory.GetEntitysObjectFromTable(this.GetAllTemplates(), typeof(Template));
                CachingService.Set(CachingService.CACHING_All_TEMPLATES_ENTITY, templates, null);
            }
            return templates;
        }

        public Template GetTemplateByID(string templateid)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (!templates.ContainsKey(templateid)) return null;
            return (Template)templates[templateid];
        }

        public DataTable GetAllTemplatesWithOutCaching()
        {
            return DataBaseFactory.TemplateHandlers.GetAllTemplatesWithOutCaching();
            
        }

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetTemplatesByTemplateType(TemplateType templatetype, string skinid)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (templates.Count == 0) return null;
            Dictionary<string, EntityBase> childtemplates = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (temp.TemplateType == templatetype&&temp.SkinInfo.Id == skinid)
                {
                    childtemplates.Add(temp.Id, (EntityBase)temp);
                }
            }
            return childtemplates;
        }

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetTemplates(string skinid, string parentid, int[] templatetypes)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (templates.Count == 0) return null;
            Dictionary<string, EntityBase> childtemplates = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (templatetypes == null)
                {
                    if (skinid == temp.SkinInfo.Id && parentid == temp.iParentId)
                    {
                        childtemplates.Add(temp.Id, (EntityBase)temp);
                    }
                }
                else
                {
                    bool temptypein = false;

                    for (int i = 0; i < templatetypes.Length; i++)
                    {
                        if (templatetypes[i] == (int)temp.TemplateType) temptypein = true;
                    }
                    if (temptypein)
                    {
                        if (skinid == temp.SkinInfo.Id && parentid == temp.iParentId)
                        {
                            childtemplates.Add(temp.Id, (EntityBase)temp);
                        }
                    }
                }
            }
            return childtemplates;
        }

        public string GetTemplatePagePatch(string tid)
        {
            Template template = this.GetTemplateByID(tid);
            string str = string.Empty;
            if (template != null)
            {
                return this.GetTemplatePagePatch(template.iParentId) + "/" + template.vcTempName;
            }
            return str;
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddTemplate(Template item, Admin admin)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            item.Id = Guid.NewGuid().ToString();

            return DataBaseFactory.TemplateHandlers.AddTemplate(item);
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddTemplateForXml(Template item, Admin admin)
        {

            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            //单页时地址不能为空 
            if (((int)item.TemplateType) == -1 && string.IsNullOrEmpty(item.vcUrl))
            {
                return -1000000024;
            }

            //模板内容不能为空
            if (string.IsNullOrEmpty(item.Content))
            {
                return -1000000027;
            }
            return DataBaseFactory.TemplateHandlers.AddTemplate(item);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int DelTemplate(string temps, Admin admin)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            //尚未选择需要删除的资讯模版
            if (string.IsNullOrEmpty(temps))
            {
                return -1000000028;
            }

            return DataBaseFactory.TemplateHandlers.DelTemplate(temps);
        }

        /// <summary>
        /// 修改模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int MdyTemplate(Template item, Admin admin)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            //单页时地址不能为空 
            if (((int)item.TemplateType) == -1 && string.IsNullOrEmpty(item.vcUrl))
            {
                return -1000000024;
            }

            //模板内容不能为空
            if (string.IsNullOrEmpty(item.Content))
            {
                return -1000000027;
            }

            return DataBaseFactory.TemplateHandlers.MdyTemplate(item);
        }

        /// <summary>
        /// 从XML中更新模板
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int UpdateTemplateFromXML(string skinid, Admin admin)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;

            Skin skininfo = HandlersFactory.skinHandlers.GetSkinEntityBySkinId(skinid);

            if (skininfo == null)
            {
                return -1000000701;
            }


            XmlDocument document = new XmlDocument();
            document.Load(HttpContext.Current.Server.MapPath("~/skin/" + skininfo.Filename + "/template.config"));
            XmlNodeList nodelist = document.GetElementsByTagName("Template");
            if (nodelist != null && nodelist.Count > 0)
            {
                foreach (XmlElement element in nodelist)
                {
                    Template template = new Template();
                    template.Id = element.SelectSingleNode("Id").InnerText.ToString();
                    template.Content = element.SelectSingleNode("Content").InnerText.ToString();
                    template.Content = Regex.Replace(template.Content, "<![CDATA[(.+?)]]>", "$1", RegexOptions.Multiline);

                    template.SkinInfo = HandlersFactory.skinHandlers.GetSkinEntityBySkinId(element.SelectSingleNode("SkinId").InnerText.ToString());
                    template.TemplateType = this.GetTemplateType(objectHandlers.ToInt(element.SelectSingleNode("TemplateType").InnerText.ToString()));
                    template.iParentId = element.SelectSingleNode("iParentId").InnerText.ToString();
                    template.iSystemType = objectHandlers.ToInt(element.SelectSingleNode("iSystemType").InnerText.ToString());
                    template.dUpdateDate = objectHandlers.ToTime(element.SelectSingleNode("dUpdateDate").InnerText.ToString());
                    template.dAddDate = objectHandlers.ToTime(element.SelectSingleNode("dAddDate").InnerText.ToString());
                    template.vcTempName = element.SelectSingleNode("vcTempName").InnerText.ToString();
                    template.vcUrl = element.SelectSingleNode("vcUrl").InnerText.ToString();

                    Template t_template = this.GetTemplateByID(template.Id);
                    if (t_template == null)
                    {
                        this.AddTemplateForXml(template, admin);
                    }
                    else
                    {
                        this.MdyTemplate(template, admin);
                    }
                }

                CachingService.Remove(CachingService.CACHING_All_TEMPLATES);
                CachingService.Remove(CachingService.CACHING_All_TEMPLATES_ENTITY);
            }
            return 1;
        }

        /// <summary>
        /// 创建皮肤模板文件 
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int CreateTemplateToXML( Admin admin,string skinid)
        {
            int rtn = HandlersFactory.adminHandlers.CheckAdminPower(admin);
            if (rtn < 0) return rtn;


            Skin skininfo = HandlersFactory.skinHandlers.GetSkinEntityBySkinId(skinid);

            if (skininfo == null)
            {
                return -1000000701;
            }

            //得到所有模板
            StringBuilder sbtemplate = new StringBuilder();
            sbtemplate.Append("<?xml version=\"1.0\"?>\r\n");
            sbtemplate.Append("<Templates>\r\n");
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates != null && templates.Count > 0)
            {
                foreach (KeyValuePair<string, EntityBase> entity in templates)
                {
                    Template temp = (Template)entity.Value;
                    if (temp.SkinInfo.Id == skinid)
                    {
                        sbtemplate.Append("<Template>\r\n");
                        sbtemplate.Append("\t<Id>" + temp.Id + "</Id>\r\n");
                        sbtemplate.Append("\t<Content><![CDATA[" + temp.Content + "]]></Content>\r\n");
                        sbtemplate.Append("\t<SkinId>" + temp.SkinInfo.Id + "</SkinId>\r\n");
                        sbtemplate.Append("\t<TemplateType>" + ((int)temp.TemplateType).ToString() + "</TemplateType>\r\n");
                        sbtemplate.Append("\t<iParentId>" + temp.iParentId + "</iParentId>\r\n");
                        sbtemplate.Append("\t<iSystemType>" + temp.iSystemType + "</iSystemType>\r\n");
                        sbtemplate.Append("\t<dUpdateDate>" + temp.dUpdateDate + "</dUpdateDate>\r\n");
                        sbtemplate.Append("\t<dAddDate>" + temp.dAddDate + "</dAddDate>\r\n");
                        sbtemplate.Append("\t<vcTempName>" + temp.vcTempName + "</vcTempName>\r\n");
                        sbtemplate.Append("\t<vcUrl>" + temp.vcUrl + "</vcUrl>\r\n");
                        sbtemplate.Append("</Template>\r\n");
                    }
                }
            }
            sbtemplate.Append("</Templates>");

            objectHandlers.SaveFile(HttpContext.Current.Server.MapPath("~/skin/" + skininfo.Filename + "/template.config"), sbtemplate.ToString());

            return 1;
        }

        /// <summary>
        /// 得到模板类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TemplateType GetTemplateType(int type)
        {
            switch (type)
            {
                case 0:
                    return TemplateType.SinglePageType;
                case 1:
                    return TemplateType.InfoType;
                case 2:
                    return TemplateType.ListType;
                case 3:
                    return TemplateType.OriginalType;
                case 4:
                    return TemplateType.SystemFolider;
                case 5:
                    return TemplateType.Folider;
                case 6:
                    return TemplateType.SystemFile;
            }
            return TemplateType.SinglePageType;
        }
    }
}
