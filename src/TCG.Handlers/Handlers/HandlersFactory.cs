using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Utils;
using TCG.Entity;

namespace TCG.Handlers
{
    /// <summary>
    /// 方法仓库
    /// </summary>
    public class HandlersFactory
    {
        /// <summary>
        /// 提供模版操作方法
        /// </summary>
        public static TemplateHandlers templateHandlers
        {
            get
            {
                if (_templatehandlers == null)
                {
                    _templatehandlers = new TemplateHandlers();
                }
                return _templatehandlers;
            }
        }
        private static TemplateHandlers _templatehandlers = null;

        public static CategoriesHandlers categoriesHandlers
        {
            get
            {
                if (_categorieshandlers == null)
                {
                    _categorieshandlers = new CategoriesHandlers();
                }
                return _categorieshandlers;
            }
        }

        private static CategoriesHandlers _categorieshandlers = null;

        public static AdminHandlers adminHandlers
        {
            get
            {
                if (_adminhandlers == null)
                {
                    _adminhandlers = new AdminHandlers();
                }
                return _adminhandlers;
            }
        }

        private static AdminHandlers _adminhandlers = null;

        public static SkinHandlers skinHandlers
        {
            get
            {
                if (_skinhandlers == null)
                {
                    _skinhandlers = new SkinHandlers();
                }
                return _skinhandlers;
            }
        }

        public static SkinHandlers _skinhandlers = null;

        public static ResourcesHandlers resourcesHandlers
        {
            get
            {
                if (_resourcehandlers == null)
                {
                    _resourcehandlers = new ResourcesHandlers();
                }
                return _resourcehandlers;
            }
        }

        private static ResourcesHandlers _resourcehandlers = null;


        /// <summary>
        /// 从记录集中返回实体
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, EntityBase> GetEntitysObjectFromTable(DataTable dt, Type type)
        {
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            Dictionary<string, EntityBase> list = new Dictionary<string, EntityBase>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow Row = dt.Rows[i];
                EntityBase entity = GetEntityObjectFromRow(Row, type);
                list.Add(entity.Id, entity);
            }
            return list;
        }

        /// <summary>
        /// 从记录行中得到实体
        /// </summary>
        /// <param name="?"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityBase GetEntityObjectFromRow(DataRow row, Type type)
        {
            if (row == null) return null;
            switch (type.ToString())
            {
                case "TCG.Entity.Categories":
                    Categories categories = new Categories();
                    categories.Id = row["Id"].ToString().Trim();
                    categories.ResourceListTemplate = templateHandlers.GetTemplateByID(row["iListTemplate"].ToString());
                    categories.iOrder = (int)row["iOrder"];
                    categories.Parent = row["Parent"].ToString().Trim();
                    categories.ResourceTemplate = templateHandlers.GetTemplateByID(row["iTemplate"].ToString());
                    categories.vcClassName = row["vcClassName"].ToString().Trim();
                    categories.vcDirectory = row["vcDirectory"].ToString().Trim();
                    categories.vcName = row["vcName"].ToString().Trim();
                    categories.vcUrl = row["vcUrl"].ToString().Trim();
                    categories.dUpdateDate = (DateTime)row["dUpdateDate"];
                    categories.cVisible = row["Visible"].ToString().Trim();
                    categories.DataBaseService = row["DataBaseService"].ToString().Trim();
                    categories.SkinInfo = skinHandlers.GetSkinEntityBySkinId(row["SkinId"].ToString().Trim());
                    categories.IsSinglePage = row["IsSinglePage"].ToString().Trim();
                    categories.vcPic = row["vcPic"].ToString().Trim();
                    return (EntityBase)categories;
                case "TCG.Entity.Resources":
                    Resources resources = new Resources();
                    resources.Id = row["Id"].ToString();
                    resources.vcTitle = row["vcTitle"].ToString();
                    resources.Categorie = categoriesHandlers.GetCategoriesById(row["iClassID"].ToString());
                    resources.vcUrl = (string)row["vcUrl"].ToString();
                    resources.vcContent = (string)row["vcContent"].ToString().Trim();
                    resources.vcAuthor = (string)row["vcAuthor"].ToString().Trim();
                    resources.iCount = (int)row["iCount"];
                    resources.vcKeyWord = (string)row["vcKeyWord"].ToString().Trim();
                    resources.vcEditor = (string)row["vcEditor"].ToString().Trim();
                    resources.cCreated = (string)row["cCreated"].ToString().Trim();
                    resources.vcSmallImg = (string)row["vcSmallImg"].ToString().Trim();
                    resources.vcBigImg = (string)row["vcBigImg"].ToString().Trim();
                    resources.vcShortContent = (string)row["vcShortContent"].ToString().Trim();
                    resources.vcSpeciality = (string)row["vcSpeciality"].ToString().Trim();
                    resources.cChecked = (string)row["cChecked"].ToString().Trim();
                    resources.cDel = (string)row["cDel"].ToString().Trim();
                    resources.cPostByUser = (string)row["cPostByUser"].ToString().Trim();
                    resources.vcFilePath = (string)row["vcFilePath"].ToString().Trim();
                    resources.dAddDate = (DateTime)row["dAddDate"];
                    resources.dUpDateDate = (DateTime)row["dUpDateDate"];
                    resources.vcTitleColor = (string)row["vcTitleColor"].ToString().Trim();
                    resources.cStrong = (string)row["cStrong"].ToString().Trim();
                    resources.SheifUrl = (string)row["SheifUrl"].ToString().Trim();
                    resources.PropertiesCategorieId = objectHandlers.ToInt(row["PropertiesCategorieId"]);
                    CachingService.Set(resources.Id, resources, null);
                    return (EntityBase)resources;
                case "TCG.Entity.Template":
                    Template template = new Template();
                    template.Id = row["Id"].ToString();
                    template.SkinInfo = skinHandlers.GetSkinEntityBySkinId(row["SkinId"].ToString());
                    template.TemplateType = templateHandlers.GetTemplateType((int)row["TemplateType"]);
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
                    skin.Id = row["Id"].ToString().Trim();
                    skin.Name = row["Name"].ToString().Trim();
                    skin.Pic = row["Pic"].ToString().Trim();
                    skin.WebDescription = row["WebDescription"].ToString().Trim();
                    skin.Filename = row["Filename"].ToString().Trim();
                    skin.WebKeyWords = row["WebKeyWords"].ToString().Trim();
                    skin.IndexPage = row["IndexPage"].ToString().Trim();
                    return (EntityBase)skin;
                case "TCG.Entity.SheifSourceInfo":
                    SheifSourceInfo sourceinfo = new SheifSourceInfo();
                    sourceinfo.Id = row["ID"].ToString().Trim();
                    sourceinfo.SourceName = row["SourceName"].ToString().Trim();
                    sourceinfo.SourceUrl = row["SourceUrl"].ToString().Trim();
                    sourceinfo.CharSet = row["CharSet"].ToString().Trim();
                    sourceinfo.ListAreaRole = row["ListAreaRole"].ToString().Trim();
                    sourceinfo.TopicListRole = row["TopicListRole"].ToString().Trim();
                    sourceinfo.TopicListDataRole = row["TopicListDataRole"].ToString().Trim();
                    sourceinfo.TopicRole = row["TopicRole"].ToString().Trim();
                    sourceinfo.TopicDataRole = row["TopicDataRole"].ToString().Trim();
                    sourceinfo.TopicPagerOld = row["TopicPagerOld"].ToString().Trim();
                    sourceinfo.TopicPagerTemp = row["TopicPagerTemp"].ToString().Trim();
                    sourceinfo.IsRss = (bool)row["IsRss"];
                    return (EntityBase)sourceinfo;
                case "TCG.Entity.FileCategories":
                //FileCategories filecagegories = new FileCategories();
                //filecagegories.Id = row["iId"].ToString();
                //filecagegories.iParentId = objectHandlers.ToInt(row["iParentId"]);
                //filecagegories.dCreateDate = objectHandlers.ToTime(row["dCreateDate"].ToString());
                //filecagegories.vcFileName = row["vcFileName"].ToString().Trim();
                //filecagegories.vcMeno = row["vcMeno"].ToString().Trim();
                //filecagegories.vcKey = row["vcKey"].ToString().Trim();
                //filecagegories.MaxSpace = objectHandlers.ToLong(row["MaxSpace"]);
                //filecagegories.Space = objectHandlers.ToLong(row["Space"]);
                //return (EntityBase)filecagegories;
                case "TCG.Entity.FileResources":
                //FileResources fileresource = new FileResources();
                //fileresource.Id = row["iID"].ToString().Trim();
                //fileresource.iClassId = (int)row["iClassId"];
                //fileresource.iSize = (int)row["iSize"];
                //fileresource.vcFileName = row["vcFileName"].ToString().Trim();
                //fileresource.vcIP = row["vcIP"].ToString().Trim();
                //fileresource.vcType = row["vcType"].ToString().Trim();
                //fileresource.iRequest = (int)row["iRequest"];
                //fileresource.iDowns = (int)row["iDowns"];
                //fileresource.dCreateDate = (DateTime)row["dCreateDate"];
                //return (EntityBase)fileresource;
                case "TCG.Entity.SheifCategorieConfig":
                    SheifCategorieConfig sheifcategorieconfig = new SheifCategorieConfig();
                    sheifcategorieconfig.Id = row["Id"].ToString();
                    sheifcategorieconfig.SheifSourceId = row["SheifSourceId"].ToString().Trim();
                    sheifcategorieconfig.LocalCategorieId = row["LocalCategorieId"].ToString().Trim();
                    sheifcategorieconfig.ResourceCreateDateTime = objectHandlers.ToTime(row["ResourceCreateDateTime"].ToString().Trim());
                    return (EntityBase)sheifcategorieconfig;
                case "TCG.Entity.Properties":
                    Properties categorieProperties = new Properties();
                    categorieProperties.Id = row["Id"].ToString();
                    categorieProperties.ProertieName = row["ProertieName"].ToString();
                    categorieProperties.PropertiesCategorieId = row["PropertiesCategorieId"].ToString();
                    categorieProperties.Type = row["Type"].ToString();
                    categorieProperties.Values = row["Values"].ToString();
                    categorieProperties.width = objectHandlers.ToInt(row["width"].ToString());
                    categorieProperties.height = objectHandlers.ToInt(row["height"].ToString());
                    categorieProperties.iOrder = objectHandlers.ToInt(row["iOrder"].ToString());
                    return (EntityBase)categorieProperties;
                case "TCG.Entity.ResourceProperties":
                    ResourceProperties rategorieProperties = new ResourceProperties();
                    rategorieProperties.Id = row["Id"].ToString();
                    rategorieProperties.ResourceId = row["ResourceId"].ToString();
                    rategorieProperties.PropertieName = row["PropertieName"].ToString();
                    rategorieProperties.PropertieValue = row["PropertieValue"].ToString();
                    rategorieProperties.PropertieId = objectHandlers.ToInt(row["PropertieId"].ToString());
                    rategorieProperties.iOrder = objectHandlers.ToInt(row["iOrder"].ToString());
                    return (EntityBase)rategorieProperties;
                case "TCG.Entity.PropertiesCategorie":
                    PropertiesCategorie pc = new PropertiesCategorie();
                    pc.CategoriePropertiesName = row["CategoriePropertiesName"].ToString();
                    pc.Id = row["Id"].ToString();
                    pc.Visible = row["Visible"].ToString();
                    pc.Visible = row["Visible"].ToString();
                    return (EntityBase)pc;
                case "TCG.Entity.Speciality":
                    Speciality speciality = new Speciality();
                    speciality.Id = row["Id"].ToString();
                    speciality.SkinId = (string)row["SkinId"];
                    speciality.iParent = (int)row["iParent"];
                    speciality.vcTitle = (string)row["vcTitle"];
                    speciality.vcExplain = (string)row["vcExplain"];
                    speciality.dUpDateDate = (DateTime)row["dUpDateDate"];
                    return (EntityBase)speciality;
                case "TCG.Entity.FeedBack":
                    FeedBack feedBack = new FeedBack();
                    feedBack.Id = row["Id"].ToString();
                    feedBack.UserName = row["UserName"].ToString();
                    feedBack.Tel = row["Tel"].ToString();
                    feedBack.QQ = row["QQ"].ToString();
                    feedBack.Content = row["Content"].ToString();
                    feedBack.AddDate = objectHandlers.ToTime(row["AddDate"].ToString());
                    feedBack.Ip = row["Ip"].ToString();
                    feedBack.SkinId = row["SkinId"].ToString();
                    feedBack.Title = row["Title"].ToString();
                    feedBack.Email = row["Email"].ToString();
                    return (EntityBase)feedBack;
            }
            return null;
        }
    }
}
