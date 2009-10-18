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
        public Dictionary<string, List<Option>> ManageOutpages
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
                if (specialPages != null)
                {
                    List<Option> onlineLoginPageslist = new List<Option>();
                    foreach (XmlElement element in specialPages)
                    {
                        Option option = new Option(element.SelectSingleNode("Text").InnerText, element.SelectSingleNode("Value").InnerText);
                        onlineLoginPageslist.Add(option);
                    }
                    this._manageoutpages.Add("onlineloginpages", specialPageslist);
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
        private string m_ManageOutpagesFilePath = "~/config/manageOutpages.xml";               //管理特殊页面的配置
    }
}