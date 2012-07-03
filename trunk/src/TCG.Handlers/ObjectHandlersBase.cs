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
using System.Collections.Generic;
using System.Collections;
using System.Text;

using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法基类
    /// </summary>
    public class ObjectHandlersBase
    {
        /// <summary>
        /// 提供系统操作方法的服务
        /// </summary>
        public HandlerService handlerService
        {
            set
            {
                this._handlerservice = value;
            }
            get
            {
                return this._handlerservice;
            }
        }
        private HandlerService _handlerservice = null;


        /// <summary>
        /// 获得配置信息支持
        /// </summary>
        public ConfigService configService
        {
            set
            {
                this._configservice = value;
            }
            get
            {
                return this._configservice;
            }
        }
        private ConfigService _configservice = null;

        /// <summary>
        /// 从记录集中返回实体
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string,EntityBase> GetEntitysObjectFromTable(DataTable dt, Type type)
        {
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            Dictionary<string ,EntityBase> list = new Dictionary<string,EntityBase>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow Row = dt.Rows[i];
                EntityBase entity = this.GetEntityObjectFromRow(Row,type);
                list.Add(entity.Id,entity);
            }
            return list;
        }

        public string GetJsEntitys(Dictionary<string, EntityBase> entitys, Type type)
        {

            if (type == null) return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("var _" + type.ToString().Split('.')[2] + "=[");

            if (entitys != null)
            {
                int i = 0;
                foreach (KeyValuePair<string, EntityBase> keyvalue in entitys)
                {
                    sb.Append(this.GetJsEntity(keyvalue.Value, type));
                    i++;
                    if (i != entitys.Count) sb.Append(",");
                }
            }
            sb.Append("];");
            return sb.ToString();
        }

        /// <summary>
        /// 得到实体的JSON对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetJsEntity(EntityBase entity, Type type)
        {
            if (entity == null) return "{}";
            if (type == null) return "{}";
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            switch (type.ToString())
            {
                case "TCG.Entity.Categories":
                    Categories categories = (Categories)entity;
                    sb.Append("Id:\"" + categories.Id + "\",");
                    sb.Append("ResourceListTemplate:" + this.GetJsEntity((EntityBase)categories.ResourceListTemplate, typeof(Template)) + ",");
                    sb.Append("iOrder:" + categories.iOrder.ToString() + ",");
                    sb.Append("ParentId:\"" + categories.Parent + "\",");
                    sb.Append("ResourceTemplate:" + this.GetJsEntity((EntityBase)categories.ResourceTemplate, typeof(Template)) + ",");
                    sb.Append("Directory:\"" + categories.vcDirectory + "\",");
                    sb.Append("Url:\"" + categories.vcUrl + "\",");
                    sb.Append("UpdateDate:\"" + categories.dUpdateDate.ToString() + "\",");
                    sb.Append("Visible:\"" + categories.cVisible + "\",");
                    sb.Append("DataBaseService:\"" + categories.DataBaseService + "\",");
                    sb.Append("Name:\"" + categories.vcName + "\",");
                    sb.Append("Skin:" + this.GetJsEntity((EntityBase)categories.SkinInfo, typeof(Skin)) + ",");
                    sb.Append("ClassName:\"" + categories.vcClassName + "\"");
                    break;
                case "TCG.Entity.Template":
                    Template template = (Template)entity;
                    sb.Append("Id:\"" + template.Id + "\",");
                    sb.Append("SkinId:\"" + template.SkinInfo + "\",");
                    sb.Append("TemplateType:" + ((int)template.TemplateType).ToString() + ",");
                    sb.Append("ParentId:\"" + template.iParentId + "\",");
                    sb.Append("SystemType:" + template.iSystemType.ToString() + ",");
                    sb.Append("TempName:\"" + template.vcTempName + "\",");
                    sb.Append("Url:\"" + template.vcUrl + "\"");
                    break;
                case "TCG.Entity.Skin":
                    Skin skin = (Skin)entity;
                    sb.Append("Id:\"" + skin.Id + "\",");
                    sb.Append("Name:\"" + skin.Name + "\",");
                    sb.Append("Pic:\"" + skin.Pic + "\",");
                    sb.Append("WebDescription:\"" + skin.WebDescription + "\",");
                    sb.Append("IndexPage:\"" + skin.IndexPage + "\",");
                    sb.Append("WebKeyWords:\"" + skin.WebKeyWords + "\"");
                    break;
                case "TCG.Entity.FileCategories":
                    FileCategories filecategories = (FileCategories)entity;
                    sb.Append("Id:" + filecategories.Id + ",");
                    sb.Append("iParentId:" + filecategories.iParentId.ToString() + ",");
                    sb.Append("vcFileName:\"" + filecategories.vcFileName + "\"");
                    break;
                case "TCG.Entity.Properties":
                    Properties categorieProperties = (Properties)entity;
                    sb.Append("Id:" + categorieProperties.Id + ",");
                    sb.Append("PropertiesCategorieId:" + categorieProperties.PropertiesCategorieId + ",");
                    sb.Append("ProertieName:\"" + categorieProperties.ProertieName + "\",");
                    sb.Append("Type:\"" + categorieProperties.Type + "\",");
                    sb.Append("Values:\"" + categorieProperties.Values + "\",");
                    sb.Append("width:" + categorieProperties.width + ",");
                    sb.Append("height:" + categorieProperties.height + ",");
                    sb.Append("iOrder:" + categorieProperties.iOrder + "");
                    break;

                case "TCG.Entity.ResourceProperties":
                    ResourceProperties rategorieProperties = (ResourceProperties)entity;
                    sb.Append("Id:" + rategorieProperties.Id + ",");
                    sb.Append("ResourceId:\"" + rategorieProperties.ResourceId + "\",");
                    sb.Append("PropertieName:\"" + rategorieProperties.PropertieName + "\",");
                    sb.Append("PropertieValue:\"" + objectHandlers.JSEncode( rategorieProperties.PropertieValue) + "\",");
                    sb.Append("PropertieId:" + rategorieProperties.PropertieId + ",");
                    sb.Append("iOrder:" + rategorieProperties.iOrder + "");
                    break;

                case "TCG.Entity.Speciality":
                    Speciality speciality = (Speciality)entity;
                    sb.Append("Id:" + speciality.Id + ",");
                    sb.Append("iParent:" + speciality.iParent + ",");
                    sb.Append("SkinId:'" + speciality.SkinId + "',");
                    sb.Append("vcTitle:'" + speciality.vcTitle + "'");
                    break;

                case "TCG.Entity.Resources":
                    Resources resources = (Resources)entity;
                    sb.Append("Id:" + resources.Id + ",");
                    sb.Append("vcTitle:'" + resources.vcTitle + "',");
                    sb.Append("vcSmallImg:'" + resources.vcSmallImg + "',");
                    sb.Append("vcFilePath:'" + resources.vcFilePath + "',");
                    sb.Append("dAddDate:'" + resources.dAddDate + "',");
                    sb.Append("vcBigImg:'" + resources.vcBigImg + "',");
                    sb.Append("vcShortContent:'" + resources.vcShortContent + "',");
                    sb.Append("vcContent:'" + objectHandlers.JSEncode( objectHandlers.GetTextWithoutHtml( resources.vcContent)) + "'");
                    break;

            }
            sb.Append("}");
            return sb.ToString();
        }

        

        /// <summary>
        /// 从记录行中得到实体
        /// </summary>
        /// <param name="?"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntityBase GetEntityObjectFromRow(DataRow row, Type type)
        {
            return null;
        }
    }
}
