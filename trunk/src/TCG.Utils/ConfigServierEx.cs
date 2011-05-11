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
    public class ConfigServiceEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigServiceEx()
        {

        }


        public static HttpContext HttpContext
        {
            get
            {
                return _ihttpcontext;
            }
            set
            {
                _ihttpcontext = value;
            }
        }
        private static HttpContext _ihttpcontext;

        /// <summary>
        /// 管理特殊页面的配置信息
        /// </summary>
        public static Dictionary<string, Option> templateTypes
        {
            get
            {
                if (_templatetypes == null)
                {
                    TemplateTypesInit();
                }
                return _templatetypes;
            }
        }

        /// <summary>
        /// 网站启用皮肤的ID
        /// </summary>
        public static string DefaultSkinId
        {
            get
            {
                if (_defaultskinid == null)
                {
                    DefalutSkinIdInit();
                }
                return _defaultskinid;
            }
        }


        private static void DefalutSkinIdInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList skindatabase = GetXmlNotListByTagNameAndFilePath(m_skinConfig, "DefaultSkinId");
            if (skindatabase != null)
            {
                foreach (XmlElement element in skindatabase)
                {
                    _defaultskinid = element.SelectSingleNode("Value").InnerText;
                }
            }
        }

        private static string _defaultskinid = null;


        /// <summary>
        /// 皮肤数据库连接字符串
        /// </summary>
        public static string ManageDataBaseStr
        {
            get
            {
                if (_managedatabasestr == null)
                {
                    ManageDataBaseInit();
                }
                return  _managedatabasestr;
            }
        }

        /// <summary>
        /// 初始化皮肤数据库连接
        /// </summary>
        private static void ManageDataBaseInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList managedatabase = GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "manageDataBase");
            if (managedatabase != null)
            {
                foreach (XmlElement element in managedatabase)
                {
                    _managedatabasestr = element.SelectSingleNode("Value").InnerText;
                }
            }
        }

        /// <summary>
        /// 设置模版类型
        /// </summary>
        private static void TemplateTypesInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList templatetype = GetXmlNotListByTagNameAndFilePath(m_skinConfig, "TemplateType");
            if (templatetype != null)
            {
                _templatetypes = new Dictionary<string, Option>();
                foreach (XmlElement element in templatetype)
                {
                    Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                    _templatetypes.Add(element.SelectSingleNode("Name").InnerText, option);
                }
            }
        }


        /// <summary>
        /// 管理特殊页面的配置信息
        /// </summary>
        public static Dictionary<string, List<Option>> manageOutpages
        {
            get
            {
                if (_manageoutpages == null)
                {
                    ManageOutpagesInit();
                }
                return _manageoutpages;
            }
        }

        /// <summary>
        /// 文件数据库连接配置
        /// </summary>
        public static List<DataBaseConnStr> fileDataBaseConfig
        {
            get
            {
                if (_filedatabaseconfig == null)
                {
                    FileDataBaseConfigInit();
                }
                return _filedatabaseconfig;
            }
        }

        private static void FileDataBaseConfigInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList filedatabaseconfig = GetXmlNotListByTagNameAndFilePath(m_FileDataBaseConfigFilePath, "fileDataBase");
            if (filedatabaseconfig != null)
            {
                _filedatabaseconfig = new List<DataBaseConnStr>();
                foreach (XmlElement element in filedatabaseconfig)
                {
                    DataBaseConnStr filedatabase = new DataBaseConnStr();
                    filedatabase.Text = element.SelectSingleNode("Text").InnerText;
                    filedatabase.Value = element.SelectSingleNode("Value").InnerText;
                    filedatabase.Service = element.SelectSingleNode("Service").InnerText;
                    filedatabase.IsBaseDataBase = objectHandlers.ToBoolen(element.SelectSingleNode("IsBaseDataBase").InnerText, false);
                    _filedatabaseconfig.Add(filedatabase);
                }
            }
        }


        /// <summary>
        /// 系统基本配置
        /// </summary>
        public static Dictionary<string, string> baseConfig
        {
            get
            {
                if (_baseconfig == null)
                {
                    BaseConfigInit();
                }
                return _baseconfig;
            }
        }


        /// <summary>
        /// 初始化基本配置信息
        /// </summary>
        private static void BaseConfigInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList baseconfig = GetXmlNotListByTagNameAndFilePath(m_BaseConfigFilePath, "Item");
            if (baseconfig != null)
            {
                _baseconfig = new Dictionary<string, string>();
                foreach (XmlElement element in baseconfig)
                {
                    _baseconfig.Add(element.SelectSingleNode("Name").InnerText, element.SelectSingleNode("Value").InnerText);
                }
            }
        }

        /// <summary>
        /// 初始化特殊页面配置属性
        /// </summary>
        private static void ManageOutpagesInit()
        {
            //获得所有SpecialPage节点
            XmlNodeList specialPages = GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "SpecialPage");
            if (specialPages != null)
            {
                _manageoutpages = new Dictionary<string, List<Option>>();
                List<Option> specialPageslist = new List<Option>();
                foreach (XmlElement element in specialPages)
                {
                    Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                    specialPageslist.Add(option);
                }
                _manageoutpages.Add("specialpages", specialPageslist);

                //获得所有需要登陆的特殊页面
                XmlNodeList onlineLoginPages = GetXmlNotListByTagNameAndFilePath(m_ManageConfigPath, "OnlineLoginPage");
                if (onlineLoginPages != null)
                {
                    List<Option> onlineLoginPageslist = new List<Option>();
                    foreach (XmlElement element in onlineLoginPages)
                    {
                        Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                        onlineLoginPageslist.Add(option);
                    }
                    _manageoutpages.Add("onlineloginpages", onlineLoginPageslist);
                }
            }
        }

        /// <summary>
        /// 获得XML 节点名相同的元素
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="tagname"></param>
        /// <returns></returns>
        private static XmlNodeList GetXmlNotListByTagNameAndFilePath(string filepath, string tagname)
        {
            XmlNodeList mylist = null;

            try
            {
                XmlDocument document1 = new XmlDocument();
                if (HttpContext != null)
                {
                    document1.Load(HttpContext.Server.MapPath(filepath));
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
        public static void UpdateConfig(string filepath, string element, string nodename, string nodevalue)
        {

            try
            {
                XmlDocument document1 = new XmlDocument();
                string _filepath = string.Empty;
                if (HttpContext != null)
                {
                    _filepath = HttpContext.Server.MapPath(filepath);
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

        private static Dictionary<string, List<Option>> _manageoutpages = null;

        private static Dictionary<string, string> _baseconfig = null;
        public static string m_BaseConfigFilePath = "~/config/baseConfig.Config";                         //系统基本配置

        private static List<DataBaseConnStr> _filedatabaseconfig = null;
        public static string m_FileDataBaseConfigFilePath = "~/config/fileDataBase.Config";               //文件数据库配置

        private static Dictionary<string, Option> _templatetypes = null;
        public static string m_skinConfig = "~/config/skinConfig.Config";                     //皮肤配置
        private static string _skindatabasestr = null;                                         //皮肤数据库连接

        public static string m_ManageConfigPath = "~/config/manageConfig.Config";                    //管理特殊页面的配置
        private static string _managedatabasestr = null;                                 //管理员数据库连接

        private static Dictionary<string, DataBaseConnStr> _resourcedatabaseconfig = null;
        public static string m_ResourceDataBaseConfigFilePath = "~/config/resourceDataBase.Config";               //资源数据库配置
    }
}
