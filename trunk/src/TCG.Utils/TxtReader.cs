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

namespace TCG.Utils
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 文件的操作方法
    /// </summary>
    public class TxtReader
    {
        /// <summary>
        /// 判断页面是否存在
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string Read(string file)
        {
            string text1 = objectHandlers.MapPath(file);
            if (!File.Exists(text1))
            {
                new Terminator().Throw("\u65e0\u6cd5\u8bfb\u53d6\u5230\u6587\u4ef6 <u>" + text1 + "</u>\uff0c\u53ef\u80fd\u8fd9\u4e2a\u6587\u4ef6\u4e0d\u5b58\u5728\u6216\u65e0\u6cd5\u88ab\u8bbf\u95ee\u3002\uff08\u9700\u7ba1\u7406\u5458\u89e3\u51b3\uff09<br /><br />\u7528\u4e8eCVB\u8bba\u575b\u7684\u63d0\u793a\uff1a<a href='user_abandon.aspx?action=removeall'>\u5220\u9664Cookies\u518d\u91cd\u8bd5</a>", null, null, null, false);
            }
            string text2 = string.Empty;
            using (StreamReader reader1 = new StreamReader(text1, Encoding.Default))
            {
                return reader1.ReadToEnd();
            }
        }

        /// <summary>
        /// 得到连接或图片中的文件的网页地址
        /// </summary>
        /// <param name="page">页面地址</param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileWebPath(string page, string filepath)
        {
            if (filepath.Substring(0, 1) == "/")
            {
                string text1 = page.Replace("http://", "");

                text1 = text1.Substring(0, text1.IndexOf("/"));

                return filepath = "http://" + text1 + filepath;
            }
            else if (filepath.Substring(0, 3) == "../")
            {
                if (page.Substring(page.Length - 1, 1) != "/")
                {
                    page = page.Substring(0, page.LastIndexOf("/") - 1);
                }

                page = page.Substring(0, page.LastIndexOf("/") - 1);
                page = page.Substring(filepath.IndexOf("/"), page.Length);

                if (filepath.Substring(0, 3) == "../")
                {
                    return TxtReader.GetFileWebPath(page, filepath);
                }
                else
                {
                    return page + filepath;
                }
            }
            else if (filepath.Substring(0, 7) == "http://" || filepath.Substring(0, 8) == "https://")
            {
                return filepath;
            }
            else
            {
                return page.Substring(0, page.LastIndexOf("/") + 1) + filepath;
            }

            return "";
        }

        /// <summary>
        /// 获取远程页面的HTML代码！
        /// </summary>
        /// <param name="ls_Url"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetRequestText(string ls_Url, string charset)
        {
            MSXML2.XMLHTTP lcom_xmlhttp = new MSXML2.XMLHTTP();
            lcom_xmlhttp.open("GET", ls_Url, false, null, null);
            lcom_xmlhttp.send("");

            //MSXML2.XMLDocument lcom_xmldoc = new MSXML2.XMLDocument();
            Byte[] lb_res = (Byte[])lcom_xmlhttp.responseBody;

            return System.Text.Encoding.GetEncoding(charset).GetString(lb_res).Trim();
        }

        /// <summary>
        /// 获取远程HTML代码
        /// </summary>
        /// <param name="ls_Url"></param>
        /// <param name="sData"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetRequestTextByPost(string ls_Url, string sData, string charset)
        {
            MSXML2.XMLHTTP lcom_xmlhttp = new MSXML2.XMLHTTP();
            lcom_xmlhttp.open("GET", ls_Url, false, null, null);
            lcom_xmlhttp.send(sData);

            //MSXML2.XMLDocument lcom_xmldoc = new MSXML2.XMLDocument();
            Byte[] lb_res = (Byte[])lcom_xmlhttp.responseBody;

            return System.Text.Encoding.GetEncoding(charset).GetString(lb_res).Trim();
        }

    }
}