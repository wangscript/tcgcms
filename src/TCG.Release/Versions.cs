using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using TCG.Entity;
using TCG.Utils;

namespace TCG.Release
{
    public class Versions
    {
        public static int Ver = 14;                         //系统版本号
        private static string SystemName = "TCG CMS System"; //系统名称

        /// <summary>
        /// 获得版本好的文字内容
        /// </summary>
        public static string version
        {
            get
            {
                return SystemName + " Version " + GetVerStr(Ver);
            }
        }
        
        /// <summary>
        /// 根据系统版本号，得到版本号
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        public static string GetVerStr(int ver)
        {

            double v1, v2, v3, v4;

            v1 = ver / 1000;
            v2 = ver / 100;
            v3 = ver / 10;
            v4 = ver / 1;


            return Math.Floor(v1).ToString() + "." + Math.Floor(v2).ToString() + "."
                + Math.Floor(v3).ToString() + "." + Math.Floor(v4).ToString();
        }

        /// <summary>
        /// 程序作者
        /// </summary>
        public static string Author
        {
            get
            {
                return "≮三云鬼≯";
            }
        }

        /// <summary>
        /// 程序官方网站
        /// </summary>
        public static string WebSite
        {
            get
            {
                return "http://www.tcgcms.cn";
            }
        }


        public static List<VersionItem> WebVersionHistory
        {
            get
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(WebSite + @"/update/Version.xml");
                XmlNodeList nodes = document1.GetElementsByTagName("Item");
                if (nodes.Count == 0) return null;
                List<VersionItem> ver = new List<VersionItem>();
                foreach (XmlElement element1 in nodes)
                {
                    VersionItem version = new VersionItem();
                    version.Text = element1.SelectSingleNode("Text").InnerText;
                    version.Ver = objectHandlers.ToInt(element1.SelectSingleNode("Ver").InnerText);
                    version.Date = objectHandlers.ToTime(element1.SelectSingleNode("Date").InnerText);
                    version.LogUrl = element1.SelectSingleNode("LogUrl").InnerText;
                    ver.Add(version);
                }
                return ver;
            }
        }
    }
}
