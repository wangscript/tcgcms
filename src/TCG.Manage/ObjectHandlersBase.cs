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

using TCG.Data;
using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法基类
    /// </summary>
    public class ManageObjectHandlersBase : ObjectHandlersBase
    {
        /// <summary>
        /// 设置数据库链接
        /// </summary>
        public Connection conn
        {
            set
            {
                this._conn = value;
            }
            get
            {
                return this._conn;
            }
        }

        private Connection _conn = null;
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
            if (entity == null) return null;
            if(type==null)return null;
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
                    sb.Append("SkinId:\"" + categories.SkinId + "\",");
                    sb.Append("ClassName:\"" + categories.vcClassName + "\"");
                    break;
                case "TCG.Entity.Template":
                    Template template = (Template)entity;
                    sb.Append("Id:\"" + template.Id + "\",");
                    sb.Append("SkinId:\"" + template.SkinId + "\",");
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
                    sb.Append("Info:\"" + skin.Info + "\"");
                    break;
                case "TCG.Entity.FileCategories":
                    FileCategories filecategories = (FileCategories)entity;
                    sb.Append("Id:" + filecategories.Id + ",");
                    sb.Append("iParentId:" + filecategories.iParentId.ToString() + ",");
                    sb.Append("vcFileName:\"" + filecategories.vcFileName + "\"");
                    break;
            }
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 设置资讯数据库连接
        /// </summary>
        public void SetDataBaseConnection()
        {
            if (this.configService == null) return;
            if (this.configService.ManageDataBaseStr == null) return;
            this.conn.SetConnStr = this.configService.ManageDataBaseStr;

        }

        /// <summary>
        /// 从记录行中得到实体
        /// </summary>
        /// <param name="?"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntityBase GetEntityObjectFromRow(DataRow row, Type type)
        {
            if (row == null) return null;
            switch (type.ToString())
            {
                case "TCG.Entity.Categories":
                    Categories categories = new Categories();
                    categories.Id = row["Id"].ToString();
                    categories.ResourceListTemplate = this.handlerService.skinService.templateHandlers.GetTemplateByID(row["iListTemplate"].ToString());
                    categories.iOrder = (int)row["iOrder"];
                    categories.Parent = row["Parent"].ToString();
                    categories.ResourceTemplate = this.handlerService.skinService.templateHandlers.GetTemplateByID(row["iTemplate"].ToString());
                    categories.vcClassName = row["vcClassName"].ToString();
                    categories.vcDirectory = row["vcDirectory"].ToString();
                    categories.vcName = row["vcName"].ToString();
                    categories.vcUrl = row["vcUrl"].ToString();
                    categories.dUpdateDate = (DateTime)row["dUpdateDate"];
                    categories.cVisible = row["Visible"].ToString();
                    categories.DataBaseService = row["DataBaseService"].ToString();
                    categories.SkinId = row["SkinId"].ToString();
                    return (EntityBase)categories;
                case "TCG.Entity.Resources":
                    Resources resources = new Resources();
                    resources.Id = row["iId"].ToString();
                    resources.vcTitle = row["vcTitle"].ToString();
                    resources.Categorie = this.handlerService.skinService.categoriesHandlers.GetCategoriesById(row["iClassID"].ToString());
                    resources.vcUrl = (string)row["vcUrl"].ToString();
                    resources.vcContent = (string)row["vcContent"].ToString();
                    resources.vcAuthor = (string)row["vcAuthor"].ToString();
                    resources.iCount = (int)row["iCount"];
                    resources.vcKeyWord = (string)row["vcKeyWord"].ToString();
                    resources.vcEditor = (string)row["vcEditor"].ToString();
                    resources.cCreated = (string)row["cCreated"].ToString();
                    resources.vcSmallImg = (string)row["vcSmallImg"].ToString();
                    resources.vcBigImg = (string)row["vcBigImg"].ToString();
                    resources.vcShortContent = (string)row["vcShortContent"].ToString();
                    resources.vcSpeciality = (string)row["vcSpeciality"].ToString();
                    resources.cChecked = (string)row["cChecked"].ToString();
                    resources.cDel = (string)row["cDel"].ToString();
                    resources.cPostByUser = (string)row["cPostByUser"].ToString();
                    resources.vcFilePath = (string)row["vcFilePath"].ToString();
                    resources.dAddDate = (DateTime)row["dAddDate"];
                    resources.dUpDateDate = (DateTime)row["dUpDateDate"];
                    resources.vcTitleColor = (string)row["vcTitleColor"].ToString();
                    resources.cStrong = (string)row["cStrong"].ToString();
                    CachingService.Set(resources.Id, resources, null);
                    return (EntityBase)resources;
                case "TCG.Entity.Template":
                    Template template = new Template();
                    template.Id = row["Id"].ToString();
                    template.SkinId = row["SkinId"].ToString();
                    template.TemplateType = this.handlerService.skinService.templateHandlers.GetTemplateType((int)row["TemplateType"]);
                    template.iParentId = row["iParentId"].ToString();
                    template.iSystemType = (int)row["iSystemType"];
                    template.vcTempName = (string)row["vcTempName"];
                    template.Content = (string)row["vcContent"];
                    template.vcUrl = (string)row["vcUrl"];
                    template.dAddDate = (DateTime)row["dAddDate"];
                    template.dUpdateDate = (DateTime)row["dUpdateDate"];
                    return (EntityBase)template;
                case "TCG.Entity.Skin":
                    Skin skin = new Skin();
                    skin.Id = row["Id"].ToString();
                    skin.Name = row["Name"].ToString();
                    skin.Pic = row["Pic"].ToString();
                    skin.Info = row["info"].ToString();
                    skin.Filename = row["Filename"].ToString();
                    return (EntityBase)skin;
                case "TCG.Entity.SheifSourceInfo":
                    SheifSourceInfo sourceinfo = new SheifSourceInfo();
                    sourceinfo.Id = row["ID"].ToString();
                    sourceinfo.SourceName = row["SourceName"].ToString();
                    sourceinfo.SourceUrl = row["SourceUrl"].ToString();
                    sourceinfo.CharSet = row["CharSet"].ToString();
                    sourceinfo.ListAreaRole = row["ListAreaRole"].ToString();
                    sourceinfo.TopicListRole = row["TopicListRole"].ToString();
                    sourceinfo.TopicListDataRole = row["TopicListDataRole"].ToString();
                    sourceinfo.TopicRole = row["TopicRole"].ToString();
                    sourceinfo.TopicDataRole = row["TopicDataRole"].ToString();
                    sourceinfo.TopicPagerOld = row["TopicPagerOld"].ToString();
                    sourceinfo.TopicPagerTemp = row["TopicPagerTemp"].ToString();
                    return (EntityBase)sourceinfo;
                case "TCG.Entity.FileCategories":
                    FileCategories filecagegories = new FileCategories();
                    filecagegories.Id = row["iId"].ToString();
                    filecagegories.iParentId = objectHandlers.ToInt(row["iParentId"]);
                    filecagegories.dCreateDate = objectHandlers.ToTime(row["dCreateDate"].ToString());
                    filecagegories.vcFileName = row["vcFileName"].ToString();
                    filecagegories.vcMeno = row["vcMeno"].ToString();
                    filecagegories.vcKey = row["vcKey"].ToString();
                    filecagegories.MaxSpace = objectHandlers.ToLong(row["MaxSpace"]);
                    filecagegories.Space = objectHandlers.ToLong(row["Space"]);
                    return (EntityBase)filecagegories;
                case "TCG.Entity.FileResources":
                    FileResources fileresource = new FileResources();
                    fileresource.Id = row["iID"].ToString();
                    fileresource.iClassId = (int)row["iClassId"];
                    fileresource.iSize = (int)row["iSize"];
                    fileresource.vcFileName = row["vcFileName"].ToString();
                    fileresource.vcIP = row["vcIP"].ToString();
                    fileresource.vcType = row["vcType"].ToString();
                    fileresource.iRequest = (int)row["iRequest"];
                    fileresource.iDowns = (int)row["iDowns"];
                    fileresource.dCreateDate = (DateTime)row["dCreateDate"];
                    return (EntityBase)fileresource;

            }
            return null;
        }
    }
}
