using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Xml;

namespace TCG.Sheif
{
    public class SheifHandlers
    {
        public static int SheifTopicList(ref string errText, ref List<TCG.ResourcesService.Resources> topics, TCG.SheifService.SheifSourceInfo sheifourceinfo,
            int startpage, int endpage)
        {
            topics = new List<TCG.ResourcesService.Resources>();
            if (string.IsNullOrEmpty(sheifourceinfo.CharSet))
            {
                sheifourceinfo.CharSet = "gb2312";
            }

            if (startpage > endpage)
            {
                return -1;
            }

            for (int i = startpage; i < endpage + 1; i++)
            {
                string ListPageHtml = HttpWebHandlers.GetHtml(string.Format(sheifourceinfo.SourceUrl, i.ToString()), Encoding.GetEncoding(sheifourceinfo.CharSet)).Replace("\r\n","").Replace("\n","").Replace("\t","");

                if (sheifourceinfo.IsRss)
                {
                    XmlDocument document = null;
                    try
                    {
                        document = new XmlDocument();
                        document.LoadXml(ListPageHtml);
                    }
                    catch { }

                    if (document != null)
                    {
                        XmlNodeList mylist = document.GetElementsByTagName("item");
                        if (mylist != null)
                        {
                            foreach (XmlElement node in mylist)
                            {
                                TCG.ResourcesService.Resources res = new TCG.ResourcesService.Resources();
                                res.vcTitle = node.SelectSingleNode("title").InnerText;
                                res.SheifUrl = node.SelectSingleNode("link").InnerText;
                                res.dAddDate = objectHandlers.ToTime(node.SelectSingleNode("pubDate").InnerText);
                                topics.Add(res);
                               
                            }
                        }
                    }
                }
                else
                {
                    Match item = Regex.Match(ListPageHtml, sheifourceinfo.ListAreaRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    if (item.Success)
                    {
                        MatchCollection matchs = Regex.Matches(item.Value, sheifourceinfo.TopicListRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        if (matchs.Count > 0)
                        {
                            foreach (Match m in matchs)
                            {
                                TCG.ResourcesService.Resources res1 = new TCG.ResourcesService.Resources();

                                int rtn = SheifTopicDataInt(ref errText, ref res1, m, sheifourceinfo.TopicListDataRole);
                                if (!string.IsNullOrEmpty(res1.SheifUrl))
                                {
                                    res1.SheifUrl = UrlCheck(res1.SheifUrl, sheifourceinfo);
                                    topics.Add(res1);
                                }
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            return 1;
        }

        public static string UrlCheck(string url, TCG.SheifService.SheifSourceInfo sheifourceinfo)
        {
            if (url.Substring(0, 7).ToLower() == "http://") return url;
            if (url.Substring(0, 1).ToLower() == "/")
            {
                string text = sheifourceinfo.SourceUrl.Substring(7, sheifourceinfo.SourceUrl.Length - 7);
                string www = text.Substring(0, text.IndexOf("/"));
                return "http://" + www + url;
            }
            else
            {
                return sheifourceinfo.SourceUrl.Substring(0, sheifourceinfo.SourceUrl.LastIndexOf("/") + 1) + url;
            }
            return url;
        }

        public static int SheifTopicDataInt(ref string errText, ref TCG.ResourcesService.Resources topic, Match match, string datarole)
        {
            if (string.IsNullOrEmpty(datarole))
            {
                return -1;
            }

            string splitstr = datarole.IndexOf("\r\n")>-1 ? "\r\n" : "\n";
            string[] datas = datarole.Split(new string[] { splitstr }, StringSplitOptions.None);
            if (datas.Length == 0)
            {
                return -2;
            }

            PropertyInfo[] tproperty = topic.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < datas.Length; i++)
            {
                if (!string.IsNullOrEmpty(datas[i]))
                {
                    string[] keyvalue = datas[i].Split(',');
                    int rtn = SheifTopicSheValue(ref errText, ref topic, tproperty, match.Result(keyvalue[0]), keyvalue[1]);
                }
            }
            return 1;
        }

        public static int SheifTopicSheValue(ref string errText, ref TCG.ResourcesService.Resources topic, PropertyInfo[] tproperty, string value, string key)
        {
            foreach (PropertyInfo piTemp in tproperty)
            {
                Type tTemp = piTemp.PropertyType;
                if (piTemp.Name.ToLower() == key.ToLower())
                {
                    piTemp.SetValue(topic, value, null);
                }
            }

            return 1;
        }

        public static int SheifTopic(ref string errText, ref TCG.ResourcesService.Resources topicinfo, TCG.SheifService.SheifSourceInfo sourceinfo, string topicurl)
        {
            string pagehtml = HttpWebHandlers.GetHtml(topicurl, Encoding.GetEncoding(sourceinfo.CharSet)).Replace("\r\n", "").Replace("\n", "").Replace("\t", "");
            Match match = Regex.Match(pagehtml, sourceinfo.TopicRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success)
            {
                topicinfo.SheifUrl = topicurl;
                int rtn = SheifTopicDataInt(ref errText, ref topicinfo, match, sourceinfo.TopicDataRole);
            }
            return 1;
        }


        public static int SheifTopic(ref string errText, ref List<TCG.ResourcesService.Resources> topics, TCG.SheifService.SheifSourceInfo sourceinfo, string topicurl)
        {
            topics = new List<TCG.ResourcesService.Resources>();

            TCG.ResourcesService.Resources topicinfo = new TCG.ResourcesService.Resources();
            int rtn = SheifHandlers.SheifTopic(ref errText, ref topicinfo, sourceinfo, topicurl);
            if (rtn < 0)
            {
                return -1;
            }

            topics.Add(topicinfo);

            //分页文章
            //string tempurl = Regex.Replace(topicurl, sourceinfo.TopicPagerOld, sourceinfo.TopicPagerTemp, RegexOptions.Singleline);

            //for (int i = 0; i < 20; i++)
            //{
            //    string tempurlex = string.Format(tempurl, i);

            //    TCG.ResourcesService.Resources topicinfo1 = new TCG.ResourcesService.Resources();

            //    int rtn1 = SheifHandlers.SheifTopic(ref errText, ref topicinfo1, sourceinfo, tempurlex);

            //    if (topicinfo.vcContent != topicinfo1.vcContent && !string.IsNullOrEmpty(topicinfo1.vcContent))
            //    {
            //        topics.Add(topicinfo1);
            //    }
            //}

            return 1;
        }
    }
}
