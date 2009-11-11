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

using System.Collections;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Caching;
using System.Web;
using System.Xml;

using TCG.Utils;
using TCG.Entity;

namespace TCG.Utils
{
    /// <summary>
    /// 站点配置信息服务提供者
    /// </summary>
    public class ConfigService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigService()
        {

        }


        public HttpContext HttpContext
        {
            get
            {
                return this._ihttpcontext;
            }
            set
            {
                this._ihttpcontext = value;
            }
        }
        private HttpContext _ihttpcontext;

        /// <summary>
        /// 管理特殊页面的配置信息
        /// </summary>
        public Dictionary<string, Option> templateTypes
        {
            get
            {
                if (this._templatetypes == null)
                {
                    this.TemplateTypesInit();
                }
                return this._templatetypes;
            }
        }

        /// <summary>
        /// 网站启用皮肤的ID
        /// </summary>
        public string DefaultSkinId
        {
            get
            {
                if (this._defaultskinid == null)
                {
                    this.DefalutSkinIdInit();
                }
                return this._defaultskinid;
            }
        }


        private void DefalutSkinIdInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList skindatabase = this.GetXmlNotListByTagNameAndFilePath(m_skinConfig, "DefaultSkinId");
            if (skindatabase != null)
            {
                foreach (XmlElement element in skindatabase)
                {
                    this._defaultskinid = element.SelectSingleNode("Value").InnerText;
                }
            }
        }

        private string _defaultskinid = null;

        /// <summary>
        /// 皮肤数据库连接字符串
        /// </summary>
        public string SkinDataBaseStr
        {
            get
            {
                if (this._skindatabasestr == null)
                {
                    this.SkinDataBaseInit();
                }
                return this._skindatabasestr;
            }
        }

        /// <summary>
        /// 初始化皮肤数据库连接
        /// </summary>
        private void SkinDataBaseInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList skindatabase = this.GetXmlNotListByTagNameAndFilePath(m_skinConfig, "skinDataBase");
            if (skindatabase != null)
            {
                foreach (XmlElement element in skindatabase)
                {
                    this._skindatabasestr = element.SelectSingleNode("Value").InnerText;
                }
            }
        }

        /// <summary>
        /// 皮肤数据库连接字符串
        /// </summary>
        public string ManageDataBaseStr
        {
            get
            {
                if (this._managedatabasestr == null)
                {
                    this.ManageDataBaseInit();
                }
                return this._managedatabasestr;
            }
        }

        /// <summary>
        /// 初始化皮肤数据库连接
        /// </summary>
        private void ManageDataBaseInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList managedatabase = this.GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "manageDataBase");
            if (managedatabase != null)
            {
                foreach (XmlElement element in managedatabase)
                {
                    this._managedatabasestr = element.SelectSingleNode("Value").InnerText;
                }
            }
        }

        /// <summary>
        /// 设置模版类型
        /// </summary>
        private void TemplateTypesInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList templatetype = this.GetXmlNotListByTagNameAndFilePath(m_skinConfig, "TemplateType");
            if (templatetype != null)
            {
                this._templatetypes = new Dictionary<string,Option>();
                foreach (XmlElement element in templatetype)
                {
                    Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                    this._templatetypes.Add(element.SelectSingleNode("Name").InnerText, option);
                }
            }
        }


        /// <summary>
        /// 管理特殊页面的配置信息
        /// </summary>
        public Dictionary<string, List<Option>> manageOutpages
        {
            get
            {
                if (this._manageoutpages == null)
                {
                    this.ManageOutpagesInit();
                }
                return this._manageoutpages;
            }
        }

        /// <summary>
        /// 文件数据库连接配置
        /// </summary>
        public List<DataBaseConnStr> fileDataBaseConfig
        {
            get
            {
                if (this._filedatabaseconfig == null)
                {
                    this.FileDataBaseConfigInit();
                }
                return this._filedatabaseconfig;
            }
        }

        private void FileDataBaseConfigInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList filedatabaseconfig = this.GetXmlNotListByTagNameAndFilePath(m_FileDataBaseConfigFilePath, "fileDataBase");
            if (filedatabaseconfig != null)
            {
                this._filedatabaseconfig = new List<DataBaseConnStr>();
                foreach (XmlElement element in filedatabaseconfig)
                {
                    DataBaseConnStr filedatabase = new DataBaseConnStr();
                    filedatabase.Text = element.SelectSingleNode("Text").InnerText;
                    filedatabase.Value = element.SelectSingleNode("Value").InnerText;
                    filedatabase.Service = element.SelectSingleNode("Service").InnerText;
                    filedatabase.IsBaseDataBase = objectHandlers.ToBoolen(element.SelectSingleNode("IsBaseDataBase").InnerText, false);
                    this._filedatabaseconfig.Add(filedatabase);
                }
            }
        }

        /// <summary>
        /// 资源数据库连接配置
        /// </summary>
        public Dictionary<string,DataBaseConnStr> ResourceDataBaseConfig
        {
            get
            {
                if (this._resourcedatabaseconfig == null)
                {
                    this.ResourceDataBaseConfigInit();
                }
                return this._resourcedatabaseconfig;
            }
        }

        private void ResourceDataBaseConfigInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList resourcedatabaseconfig = this.GetXmlNotListByTagNameAndFilePath(m_ResourceDataBaseConfigFilePath, "resourceDataBase");
            if (resourcedatabaseconfig != null)
            {
                this._resourcedatabaseconfig = new Dictionary<string,DataBaseConnStr>();
                foreach (XmlElement element in resourcedatabaseconfig)
                {
                    DataBaseConnStr filedatabase = new DataBaseConnStr();
                    filedatabase.Text = element.SelectSingleNode("Text").InnerText;
                    filedatabase.Value = element.SelectSingleNode("Value").InnerText;
                    filedatabase.Service = element.SelectSingleNode("Service").InnerText;
                    filedatabase.IsBaseDataBase = objectHandlers.ToBoolen(element.SelectSingleNode("IsBaseDataBase").InnerText, false);
                    this._resourcedatabaseconfig.Add(filedatabase.Service,filedatabase);
                }
            }
        }

        /// <summary>
        /// 系统基本配置
        /// </summary>
        public Dictionary<string, string> baseConfig
        {
            get
            {
                if (this._baseconfig == null)
                {
                    this.BaseConfigInit();
                }
                return this._baseconfig;
            }
        }


        /// <summary>
        /// 初始化基本配置信息
        /// </summary>
        private void BaseConfigInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList baseconfig = this.GetXmlNotListByTagNameAndFilePath(m_BaseConfigFilePath, "Item");
            if (baseconfig != null)
            {
                this._baseconfig = new Dictionary<string, string>();
                foreach (XmlElement element in baseconfig)
                {
                    this._baseconfig.Add(element.SelectSingleNode("Name").InnerText, element.SelectSingleNode("Value").InnerText);
                }
            }
        }

        /// <summary>
        /// 初始化特殊页面配置属性
        /// </summary>
        private void ManageOutpagesInit()
        {
            //获得所有SpecialPage节点
            XmlNodeList specialPages = this.GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "SpecialPage");
            if (specialPages != null)
            {
                this._manageoutpages = new Dictionary<string, List<Option>>();
                List<Option> specialPageslist = new List<Option>();
                foreach (XmlElement element in specialPages)
                {
                    Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                    specialPageslist.Add(option);
                }
                this._manageoutpages.Add("specialpages", specialPageslist);

                //获得所有需要登陆的特殊页面
                XmlNodeList onlineLoginPages = this.GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "OnlineLoginPage");
                if (onlineLoginPages != null)
                {
                    List<Option> onlineLoginPageslist = new List<Option>();
                    foreach (XmlElement element in onlineLoginPages)
                    {
                        Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                        onlineLoginPageslist.Add(option);
                    }
                    this._manageoutpages.Add("onlineloginpages", onlineLoginPageslist);
                }
            }
        }

        /// <summary>
        /// 获得XML 节点名相同的元素
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="tagname"></param>
        /// <returns></returns>
        private XmlNodeList GetXmlNotListByTagNameAndFilePath(string filepath, string tagname)
        {
            XmlNodeList mylist = null;

            try
            {
                XmlDocument document1 = new XmlDocument();
                if (this.HttpContext != null)
                {
                    document1.Load(this.HttpContext.Server.MapPath(filepath));
                }
                else
                {
                    document1.Load(HttpContext.Current.Server.MapPath(filepath));
                }

                mylist = document1.GetElementsByTagName(tagname);
            }
            catch
            {

            }
            return mylist;
        }

        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void UpdateConfig(string filepath, string element,string nodename,string nodevalue)
        {

            try
            {
                XmlDocument document1 = new XmlDocument();
                string _filepath = string.Empty;
                if (this.HttpContext != null)
                {
                    _filepath = this.HttpContext.Server.MapPath(filepath);
                }
                else
                {
                    _filepath = HttpContext.Current.Server.MapPath(filepath); 
                }
                document1.Load(_filepath);
                document1.GetElementsByTagName(element)[0].SelectSingleNode(nodename).InnerText = nodevalue;
                document1.Save(_filepath);

            }
            catch
            {

            }
        }

        private Dictionary<string, List<Option>> _manageoutpages = null;

        private Dictionary<string, string> _baseconfig = null;
        public string m_BaseConfigFilePath = "~/config/baseConfig.Config";                         //系统基本配置

        private List<DataBaseConnStr> _filedatabaseconfig = null;
        public string m_FileDataBaseConfigFilePath = "~/config/fileDataBase.Config";               //文件数据库配置

        private Dictionary<string, Option> _templatetypes = null;
        public string m_skinConfig = "~/config/skinConfig.Config";                     //皮肤配置
        private string _skindatabasestr = null;                                         //皮肤数据库连接

        public string m_ManageConfigPath = "~/config/manageConfig.Config";                    //管理特殊页面的配置
        private string _managedatabasestr = null;                                 //管理员数据库连接

        private Dictionary<string,DataBaseConnStr> _resourcedatabaseconfig = null;
        public string m_ResourceDataBaseConfigFilePath = "~/config/resourceDataBase.Config";               //资源数据库配置
    }
}