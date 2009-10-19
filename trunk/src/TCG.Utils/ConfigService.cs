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
    public class ConfigService
    {
        public ConfigService()
        {

        }


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
        /// 设置模版类型
        /// </summary>
        private void TemplateTypesInit()
        {
            //获得所有需要登陆的特殊页面
            XmlNodeList templatetype = this.GetXmlNotListByTagNameAndFilePath(m_TemplateTypeFilePath, "TemplateType");
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

        public List<FileDataBase> fileDataBaseConfig
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
                this._filedatabaseconfig = new List<FileDataBase>();
                foreach (XmlElement element in filedatabaseconfig)
                {
                    FileDataBase filedatabase = new FileDataBase();
                    filedatabase.Text = element.SelectSingleNode("Text").InnerText;
                    filedatabase.Value = element.SelectSingleNode("Value").InnerText;
                    filedatabase.Service = element.SelectSingleNode("Service").InnerText;
                    filedatabase.IsBaseDataBase = objectHandlers.ToBoolen(element.SelectSingleNode("IsBaseDataBase").InnerText, false);
                    this._filedatabaseconfig.Add(filedatabase);
                }
            }
        }


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
            XmlNodeList specialPages = this.GetXmlNotListByTagNameAndFilePath(m_ManageOutpagesFilePath, "SpecialPage");
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
                XmlNodeList onlineLoginPages = this.GetXmlNotListByTagNameAndFilePath(m_ManageOutpagesFilePath, "OnlineLoginPage");
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
                document1.Load(HttpContext.Current.Server.MapPath(filepath));
                mylist = document1.GetElementsByTagName(tagname);
            }
            catch
            {

            }
            return mylist;
        }

        private Dictionary<string, List<Option>> _manageoutpages = null;
        private string m_ManageOutpagesFilePath = "~/config/manageOutpages.Config";                 //管理特殊页面的配置

        private Dictionary<string, string> _baseconfig = null;
        private string m_BaseConfigFilePath = "~/config/baseConfig.Config";                         //系统基本配置

        private List<FileDataBase> _filedatabaseconfig = null;
        private string m_FileDataBaseConfigFilePath = "~/config/fileDataBase.Config";               //文件数据库配置

        private Dictionary<string, Option> _templatetypes = null;
        private string m_TemplateTypeFilePath = "~/config/TemplateTypes.Config";                    //管理特殊页面的配置
    }
}