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
using System.Xml;
using TCG.Data;
using System.Text.RegularExpressions;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Handlers
{
    public class TemplateHandlers : ManageObjectHandlersBase
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
                templates = base.GetEntitysObjectFromTable(this.GetAllTemplates(), typeof(Template));
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
            
            string Sql = "SELECT * FROM Template (NOLOCK)";
            return conn.GetDataTable(Sql);
        }

        /// <summary>
        /// 根据模板类型获取模板
        /// </summary>
        /// <param name="templatetype"></param>
        /// <returns></returns>
        public Dictionary<string, EntityBase> GetTemplatesByTemplateType(TemplateType templatetype)
        {
            Dictionary<string, EntityBase> templates = this.GetAllTemplatesEntity();
            if (templates == null) return null;
            if (templates.Count == 0) return null;
            Dictionary<string, EntityBase> childtemplates = new Dictionary<string, EntityBase>();
            foreach (KeyValuePair<string, EntityBase> entity in templates)
            {
                Template temp = (Template)entity.Value;
                if (temp.TemplateType == templatetype)
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
        public Dictionary<string, EntityBase> GetTemplates(string skinid, string parentid, int [] templatetypes)
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
                    if (skinid == temp.SkinId && parentid == temp.iParentId)
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
                        if (skinid == temp.SkinId && parentid == temp.iParentId)
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
            if(template!=null)
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
        public int AddTemplate(Template item,Admin admin)
        {
            
            item.Id = Guid.NewGuid().ToString();

            return this.AddTemplateForXml(item, admin);
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddTemplateForXml(Template item, Admin admin)
        {
            

            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@SkinId", SqlDbType.VarChar, 36); sp2.Value = item.SkinId;
            SqlParameter sp3 = new SqlParameter("@TemplateType", SqlDbType.Int, 4); sp3.Value = (int)item.TemplateType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.Content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@iParentId", SqlDbType.VarChar, 36); sp7.Value = item.iParentId;
            SqlParameter sp8 = new SqlParameter("@iSystemType", SqlDbType.Int, 4); sp8.Value = item.iSystemType;
            SqlParameter sp9 = new SqlParameter("@Id", SqlDbType.VarChar, 36); sp9.Value = item.Id;
            SqlParameter sp10 = new SqlParameter("@reValue", SqlDbType.Int); sp10.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_ManageTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3, 
                sp4, sp5, sp6, sp7 , sp8 , sp9,sp10}, new int[] { 10 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int DelTemplate(string temps,Admin admin)
        {
            
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@vctemps", SqlDbType.VarChar, 1000); sp2.Value = temps;
            SqlParameter sp3 = new SqlParameter("@reValue", SqlDbType.Int); sp3.Direction = ParameterDirection.Output;
            string[] reValues = conn.Execute("SP_Template_DelTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3 }, new int[] { 3 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 修改模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int MdyTemplate(Template item,Admin admin)
        {
            
            SqlParameter sp0 = new SqlParameter("@vcAdminName", SqlDbType.VarChar, 50); sp0.Value = admin.vcAdminName;
            SqlParameter sp1 = new SqlParameter("@vcip", SqlDbType.VarChar, 15); sp1.Value = objectHandlers.UserIp;
            SqlParameter sp2 = new SqlParameter("@SkinId", SqlDbType.VarChar, 36); sp2.Value = item.SkinId;
            SqlParameter sp3 = new SqlParameter("@TemplateType", SqlDbType.Int, 4); sp3.Value = (int)item.TemplateType;
            SqlParameter sp4 = new SqlParameter("@vcTempName", SqlDbType.VarChar, 50); sp4.Value = item.vcTempName;
            SqlParameter sp5 = new SqlParameter("@vcContent", SqlDbType.Text, 0); sp5.Value = item.Content;
            SqlParameter sp6 = new SqlParameter("@vcUrl", SqlDbType.VarChar, 255); sp6.Value = item.vcUrl;
            SqlParameter sp7 = new SqlParameter("@action", SqlDbType.Char, 2); sp7.Value = "02";
            SqlParameter sp8 = new SqlParameter("@Id", SqlDbType.VarChar, 36); sp8.Value = item.Id;
            SqlParameter sp9 = new SqlParameter("@reValue", SqlDbType.Int); sp9.Direction = ParameterDirection.Output;
            SqlParameter sp10 = new SqlParameter("@iParentId", SqlDbType.VarChar, 36); sp10.Value = item.iParentId;
            string[] reValues = conn.Execute("SP_Template_ManageTemplate", new SqlParameter[] { sp0, sp1, sp2, sp3,
                sp4, sp5, sp6, sp7, sp8, sp9 ,sp10}, new int[] { 9 });
            if (reValues != null)
            {
                int rtn = (int)Convert.ChangeType(reValues[0], typeof(int));
                return rtn;
            }
            return -19000000;
        }

        /// <summary>
        /// 从XML中更新模板
        /// </summary>
        /// <param name="skinid"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int UpdateTemplateFromXML(string skinid, Admin admin)
        {
            Skin skininfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(skinid);

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

                    template.SkinId = element.SelectSingleNode("SkinId").InnerText.ToString();
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
        public int CreateTemplateToXML(string skinid)
        {
            Skin skininfo = base.handlerService.skinService.skinHandlers.GetSkinEntityBySkinId(skinid);

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
                    if (temp.SkinId == skinid)
                    {
                        sbtemplate.Append("<Template>\r\n");
                        sbtemplate.Append("\t<Id>" + temp.Id + "</Id>\r\n");
                        sbtemplate.Append("\t<Content><![CDATA[" + temp.Content + "]]></Content>\r\n");
                        sbtemplate.Append("\t<SkinId>" + temp.SkinId + "</SkinId>\r\n");
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