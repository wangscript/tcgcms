namespace TCG.Utils
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Web.Caching;
    using System.Web;
    using System.Xml;

    public class Config
    {
        public Config()
        {
            this.m_filename = ConfigurationManager.ConnectionStrings["CustomConfigFile"].ToString();
            this.Initialize();
        }

        public Config(string filename)
        {
            this.m_filename = filename;
            this.Initialize();
        }

        public static Config GetInstant(string filename)
        {
            return new Config("config//" + filename);
        }

        private void Initialize()
        {
            object obj1 = Caching.Get(this.m_configFilePath);
            if (obj1 != null)
            {
                this.m_hashtable = (Hashtable)obj1;
            }
            else if (!File.Exists(this.m_configFilePath))
            {
                new Terminator().Throw("配置文件 " + this.m_configFilePath + " 不存在或访问被拒绝。");
            }
            else
            {
                this.m_hashtable = new Hashtable();
                XmlDocument document1 = new XmlDocument();
                document1.Load(this.m_configFilePath);
                foreach (XmlElement element1 in document1.GetElementsByTagName("Item"))
                {
                    string text1 = element1.SelectSingleNode("Name").InnerText;
                    string text2 = element1.SelectSingleNode("Value").InnerText;
                    this.m_hashtable.Add(text1, text2);
                }
                Caching.Set(this.m_configFilePath, this.m_hashtable, new CacheDependency(this.m_configFilePath));
            }
        }

        public void RemoveSelfCache()
        {
            Caching.Remove(this.m_configFilePath);
        }


        public string this[string key]
        {
            get
            {
                if (this.m_hashtable.Contains(key))
                {
                    return this.m_hashtable[key].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.m_hashtable[key] = value;
            }
        }

        private string m_configFilePath
        {
            get
            {
                return Fetch.MapPath(this.m_filename);
            }
        }

        public static Config Settings
        {
            get
            {
                return new Config();
            }
        }


        private string m_filename;
        private Hashtable m_hashtable;
    }
}