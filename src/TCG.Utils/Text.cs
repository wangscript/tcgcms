
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
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Security;

    public class Text
    {
        public static string AddZero(int i)
        {
            return (((i > 9) ? string.Empty : "0") + i);
        }

        public static string GetTextWithoutHtml(string str)
        {
            string strpatrn = "<.*?>";
            str = Regex.Replace(str, strpatrn, "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace("\t", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("\"", "");
            str = str.Replace("'", "");
            str = str.Replace("&amp;", "");
            str = str.Replace("&lt;", "");
            str = str.Replace("&gt;", "");
            str = str.Replace("&quot;", "");
            str = str.Replace("&#39;", "");
            str = str.Replace("&nbsp;", "");
            str = str.Replace("　", "");
            str = str.Replace(" ", "");
            return str;
        }

        /// <summary>
        /// 去掉标点符号！
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceBD(string str)
        {
            str = ToDBC(str).Replace("?", "");
            str = ToDBC(str).Replace(",", "");
            str = ToDBC(str).Replace(".", "");
            str = ToDBC(str).Replace(" ", "");
            str = ToDBC(str).Replace("。", "");
            str = ToDBC(str).Replace("`", "");
            str = ToDBC(str).Replace("@", "");
            str = ToDBC(str).Replace("#", "");
            str = ToDBC(str).Replace("$", "");
            str = ToDBC(str).Replace("%", "");
            str = ToDBC(str).Replace("^", "");
            str = ToDBC(str).Replace("&", "");
            str = ToDBC(str).Replace("*", "");
            str = ToDBC(str).Replace("(", "");
            str = ToDBC(str).Replace(")", "");
            str = ToDBC(str).Replace("[", "");
            str = ToDBC(str).Replace("]", "");
            str = ToDBC(str).Replace("{", "");
            str = ToDBC(str).Replace("}", "");
            str = ToDBC(str).Replace("'", "");
            str = ToDBC(str).Replace("\"", "");
            return str;
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>        
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static bool ComparePassword(string pwd1, string pwd2)
        {
            string text1 = pwd1.ToLower();
            string text2 = pwd2.ToLower();
            if ((text1.Length == text2.Length) && (text1.Length != 0))
            {
                return (text1 == text2);
            }
            if ((text1.Length == 0x20) && (text2.Length == 0x10))
            {
                return (text1.Substring(8, 0x10) == text2);
            }
            if ((text1.Length == 0x10) && (text2.Length == 0x20))
            {
                return (text1 == text2.Substring(8, 0x10));
            }
            return false;
        }

        public static string EmailEncode(string s)
        {
            string text1 = Text.TextEncode(s).Replace("@", "&#64;").Replace(".", "&#46;");
            return ("<a href='mailto:" + text1 + "'>" + text1 + "</a>");
        }

        public static string GenerateToken(string s)
        {
            return Text.MD5(s + ConfigurationManager.AppSettings["CookieToken"].ToString());
        }

        public static string HtmlDecode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("&amp;", "&");
            builder1.Replace("&lt;", "<");
            builder1.Replace("&gt;", ">");
            builder1.Replace("&quot;", "\"");
            builder1.Replace("&#39;", "'");
            builder1.Replace("&nbsp; &nbsp; ", "\t");
            builder1.Replace("<p></p>", "\r\n\r\n");
            builder1.Replace("<br />", "\r\n");
            return builder1.ToString();
        }

        public static string HtmlEncode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            builder1.Replace("\t", "&nbsp; &nbsp; ");
            builder1.Replace("\r", "");
            builder1.Replace("\n\n", "<p></p>");
            builder1.Replace("\n", "<br />");
            return Text.ShitEncode(builder1.ToString());
        }

        public static string Left(string s, int need, bool encode)
        {
            char ch1;
            if ((s == null) || (s == ""))
            {
                return string.Empty;
            }
            int num1 = s.Length;
            if (num1 < (need / 2))
            {
                return (encode ? Text.TextEncode(s) : s);
            }
            int num4 = 0;
            int num2 = 0;
            while (num2 < num1)
            {
                ch1 = s[num2];
                num4 += RegExp.IsUnicode(ch1.ToString()) ? 2 : 1;
                if (num4 >= need)
                {
                    break;
                }
                num2++;
            }
            string text1 = s.Substring(0, num2);
            if (num1 > num2)
            {
                int num3 = 0;
                while (num3 < 5)
                {
                    ch1 = s[num2 - num3];
                    num4 -= RegExp.IsUnicode(ch1.ToString()) ? 2 : 1;
                    if (num4 <= need)
                    {
                        break;
                    }
                    num3++;
                }
                text1 = s.Substring(0, num2 - num3);
            }
            return (encode ? Text.TextEncode(text1) : text1);
        }

        public static int Len(string s)
        {
            return Encoding.GetEncoding(0x3a8).GetByteCount(s);
        }

        public static string MD5(string s)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
        }

        public static string ShitEncode(string s)
        {
            string text1 = ConfigurationManager.ConnectionStrings["BadWords"].ToString();
            switch (text1)
            {
                case null:
                case "":
                    text1 = "\u5988\u7684|\u4f60\u5988|\u4ed6\u5988|\u5988b|\u5988\u6bd4|fuck|shit|\u6211\u65e5|\u6cd5\u8f6e|\u6211\u64cd";
                    break;

                default:
                    text1 = Regex.Replace(text1, @"\|{2,}", "|");
                    text1 = Regex.Replace(text1, @"(^\|)|(\|$)", string.Empty);
                    break;
            }
            return Regex.Replace(s, text1, "**", RegexOptions.IgnoreCase);
        }

        public static string GetTextFromHtml(string s)
        {
            if (s == null || s == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(s, "<.*?>", "");
            }
        }

        public static string JSEncode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("\"", "\\\"");
            return Text.ShitEncode(builder1.ToString());
        }

        public static string SqlEncode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("'", "''");
            builder1.Replace("\t", "&nbsp; &nbsp; ");
            builder1.Replace("\r", "");
            builder1.Replace("\n\n", "<p></p>");
            builder1.Replace("\n", "<br />");
            return Text.ShitEncode(builder1.ToString());
        }

        public static string TextEncode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            return Text.ShitEncode(builder1.ToString());
        }

        public static string RandomStr(int n)
        {
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < n; i++)
            {
                string text1 = arrChar[rnd.Next(0, arrChar.Length)].ToString();
                if (rnd.Next(2) == 1)
                {
                    text1 = text1.ToUpper();
                }
                else
                {
                    text1 = text1.ToLower();
                }

                num.Append(text1);

            }
            return num.ToString();
        }

        public static bool SaveFile(string filepath, string text)
        {
            string dir = filepath.Substring(0, filepath.LastIndexOf("\\"));
            try
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                using (System.IO.FileStream fs = System.IO.File.Create(filepath))
                {
                    byte[] info = System.Text.Encoding.GetEncoding("utf-8").GetBytes(text);   //防止乱码
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得文件夹的大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long getDirectorySize(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            long sumSize = 0;
            foreach (FileSystemInfo fsInfo in dirInfo.GetFileSystemInfos())
            {
                if (fsInfo.Attributes.ToString().ToLower() == "directory")
                {
                    sumSize += getDirectorySize(fsInfo.FullName);
                }
                else
                {
                    FileInfo fiInfo = new FileInfo(fsInfo.FullName);
                    sumSize += fiInfo.Length;
                }
            }
            return sumSize;
        }
    }
}

