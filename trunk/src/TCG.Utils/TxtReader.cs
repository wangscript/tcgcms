namespace TCG.Utils
{
    using System;
    using System.IO;
    using System.Text;

    public class TxtReader
    {
        public static string Read(string file)
        {
            string text1 = Fetch.MapPath(file);
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

        //得到连接或图片中的文件的网页地址
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
            else
            {
                return page.Substring(0, page.LastIndexOf("/") + 1) + filepath;
            }

            return "";
        }

        public static string GetRequestText(string ls_Url, string charset)
        {
            MSXML2.XMLHTTP lcom_xmlhttp = new MSXML2.XMLHTTP();
            lcom_xmlhttp.open("GET", ls_Url, false, null, null);
            lcom_xmlhttp.send("");

            //MSXML2.XMLDocument lcom_xmldoc = new MSXML2.XMLDocument();
            Byte[] lb_res = (Byte[])lcom_xmlhttp.responseBody;

            return System.Text.Encoding.GetEncoding(charset).GetString(lb_res).Trim();
        }

        public static string GetRequestTextByPost(string ls_Url,string sData, string charset)
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
