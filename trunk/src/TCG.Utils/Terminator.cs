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
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Configuration;

    public class Terminator
    {
        public virtual void Alert(string s)
        {
            this.Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');history.back();</script>");
            this.End();
        }

        public virtual void Alert(string s, string backurl)
        {
            this.Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');location.href='" + backurl + "';</script>");
            this.End();
        }

        private void Echo(string s)
        {
            HttpContext.Current.Response.Write(s);
        }

        private void End()
        {
            HttpContext.Current.Response.End();
        }

        public virtual void Throw(string message)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");
            this.Throw(message, null, null, null, true);
        }

        public virtual void Throw(int errvalue , string autojump, bool showbak)
        {
            string ErrText = TxtReader.Read(ConfigurationManager.ConnectionStrings["ErrTxtPath"].ToString());
            if (!string.IsNullOrEmpty(ErrText))
            {
                string patten = errvalue.ToString() + @"@@@([A-Z_a-z]+)@@@([^-\r\n]+)";
                Match mt = Regex.Match(ErrText, patten, RegexOptions.Singleline);
                if (mt.Success)
                {
                    ErrText = mt.Result("$2");
                }
                else
                {
                    ErrText = errvalue.ToString();
                }
                mt = null;
            }
            else
            {
                ErrText = errvalue.ToString();
            }
            this.Throw(ErrText, "系统错误", null, autojump, showbak);
        }

        public virtual void Throw(string message, string title, string links, string autojump, bool showback)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");
            StringBuilder builder1 = new StringBuilder(this.template);
            builder1.Replace("{$Message}", message);
            builder1.Replace("{$Title}", ((title == null) || (title == "")) ? "\u7cfb\u7edf\u63d0\u793a" : title);
            if ((links != null) && (links != ""))
            {
                string[] textArray1 = links.Split(new char[] { '|' });
                for (int num1 = 0; num1 < textArray1.Length; num1++)
                {
                    string[] textArray2 = textArray1[num1].Split(new char[] { ',' });
                    if (textArray2.Length > 1)
                    {
                        if (textArray2[1].Trim() == "RefHref")
                        {
                            textArray2[1] = objectHandlers.Referrer;
                        }
                        if ((textArray2[1] != string.Empty) && (textArray2[1] != null))
                        {
                            string text1 = "<li><a href='" + textArray2[1] + "'";
                            if (textArray2.Length == 3)
                            {
                                text1 = text1 + " target='" + textArray2[2].Trim() + "'";
                            }
                            if (textArray2[0].Trim() == "RefText")
                            {
                                textArray2[0] = objectHandlers.TextEncode(objectHandlers.Referrer);
                            }
                            builder1.Replace("{$Links}", (text1 + ">" + textArray2[0].Trim() + "</a></li>\r\n\t\t\t\t") + "{$Links}");
                        }
                    }
                }
            }
            if ((autojump != null) && (autojump != string.Empty))
            {
                builder1.Replace("{$AutoJump}", "<meta http-equiv='refresh' content='3; url=" + ((autojump == "back") ? "javascript:history.back()" : autojump) + "' />");
            }
            else
            {
                builder1.Replace("{$AutoJump}", "<!-- no jump -->");
            }
            if (showback)
            {
                builder1.Replace("{$Links}", "<li><a href='javascript:history.back()'>\u8fd4\u56de\u4e0a\u4e00\u9875</a></li>");
            }
            else
            {
                builder1.Replace("{$Links}", "<!-- no back -->");
            }
            this.Echo(builder1.ToString());
            this.End();
        }


        public virtual string template
        {
            get
            {
                return ("<html xmlns:v>\r\n<head>\r\n<title>{$Title}</title>\r\n<meta http-equiv='Content-Type' content='text/html; charset=" + Encoding.Default.BodyName + "' />\r\n<meta name='description' content='.NET\u7c7b\u5e93 TCG.Web.dll \u9875\u9762\u4e2d\u6b62\u7a0b\u5e8f' />\r\n<meta name='copyright' content='http://www.TCG.net/' />\r\n<meta name='generator' content='editplus2.11' />\r\n<meta name='usefor' content='application termination' />\r\n{$AutoJump}\r\n<style rel='stylesheet'>\r\nv\\:*\t{\r\n\tbehavior:url(#default#vml);\r\n}\r\nbody, div, span, li, td, a {\r\n\tcolor: #222222;\r\n\tfont-size: 12px !important;\r\n\tfont-size: 11px;\r\n\tfont-family: tahoma, arial, 'courier new', verdana, sans-serif;\r\n\tline-height: 19px;\r\n}\r\na {\r\n\tcolor: #2c78c5;\r\n\ttext-decoration: none;\r\n}\r\na:hover {\r\n\tcolor: red;\r\n\ttext-decoration: none;\r\n}\r\n</style>\r\n</head>\r\n<body style='text-align:center;margin:90px 20px 50px 20px'>\r\n<?xml:namespace prefix='v' />\r\n<div style='margin:auto; width:450px; text-align:center'>\r\n\t<v:roundrect style='text-align:left; display:table; margin:auto; padding:15px; width:450px; height:210px; overflow:hidden; position:relative;' arcsize='3200f' coordsize='21600,21600' fillcolor='#fdfdfd' strokecolor='#e6e6e6' strokeweight='1px'>\r\n\t\t<table width='100%' cellpadding='0' cellspacing='0' border='0' style='padding-bottom:6px; border-bottom:1px #cccccc solid'>\r\n\t\t\t<tr>\r\n\t\t\t\t<td><b>{$Title}</b></td>\r\n\t\t\t\t<td align='right' style='color:#f8f8f8'>--- TCG terminator</td>\r\n\t\t\t</tr>\r\n\t\t</table>\r\n\t\t<table width='100%' cellpadding='0' cellspacing='0' border='0' style='word-break:break-all; overflow:hidden'>\r\n\t\t\t<tr>\r\n\t\t\t\t<td width='80' valign='top' style='padding-top:13px'><span style='font-size:16px; zoom:4; color:#aaaaaa'><font face='webdings'>i</font></span></td>\r\n\t\t\t\t<td valign='top' style='padding-top:17px'>\r\n\t\t\t\t\t<p style='margin-bottom:22px'>{$Message}</p>\r\n\t\t\t\t\t{$Links}\r\n\t\t\t\t</td>\r\n\t\t\t</tr>\r\n\t\t</table>\r\n\t</v:roundrect>\r\n</div>\r\n</body>\r\n</html>");
            }
        }

    }
}

