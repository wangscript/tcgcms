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
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

using TCG.Entity;
using TCG.Utils;
using TCG.Data;

namespace TCG.Release
{
    public class Versions
    {
        public static int Ver = 68;                         //系统版本号
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
                return "≮三云鬼≯ QQ:644139466,Email:sanyungui@vip.qq.com";
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

        /// <summary>
        /// 下一版本对象
        /// </summary>
        public static VersionItem HigherVersion
        {
            get
            {
                List<VersionItem> vers = WebVersionHistory;
                if (vers == null) return null;
                if (vers.Count == 0) return null;
                int small = -1;
                int tempver = Ver;
                for (int i = 0; i < vers.Count; i++)
                {
                    if (Ver < vers[i].Ver)
                    {
                        if (small == 0)
                        {
                            small = i;
                            tempver = vers[i].Ver;
                        }
                        else
                        {
                            if (vers[i].Ver < tempver)
                            {
                                tempver = vers[i].Ver;
                            }
                        }
                    }
                }
                return small==-1?null:vers[small];
            }
        }

        /// <summary>
        /// 获得所有版本号
        /// </summary>
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
                    version.Sqls = objectHandlers.ToInt(element1.SelectSingleNode("Sqls").InnerText);
                    ver.Add(version);
                }
                return ver;
            }
        }

        public static bool RunSqlSep(Connection conn, int num,ref string SqlText)
        {
            string sqltxtPath = WebSite + "/update/" + GetVerStr(HigherVersion.Ver) + "/sql/" + num.ToString() + ".sql";
            string sql = TxtReader.GetRequestText(sqltxtPath, "gb2312");
            if (string.IsNullOrEmpty(sql)) return false;

            string patten = @"\/\*\*\*(.*?)\$\[(.*?)\]\*\*\*\/";
            Match mt = Regex.Match(sql, patten, RegexOptions.Singleline);
            if (mt.Success)
            {
                SqlText = mt.Result("$1");
            }
            else
            {
                SqlText = "未检测到更新SQL";
                return false;
            }
            mt = null;

            try
            {
                conn.Execute(sql);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
