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

namespace TCG.Release
{
    using TCG.Utils;
    using System;
    using System.Net;
    using System.Text;

    public sealed class Authorization
    {
        public Authorization(string product)
        {
            this.m_product = product;
        }

        private string GenerateSerialNumber(string domain)
        {
            string text1 = Text.MD5("!%sa^te" + domain + "2l*~l#$i)^t?e-" + this.m_product);
            return (this.m_product + "-" + text1.Substring(5, 5) + "-" + text1.Substring(14, 5) + "-" + text1.Substring(0x16, 5)).ToUpper();
        }


        public static string Bulletin
        {
            get
            {
                return "对不起，本产品部分功能目前只向商业版用户开放。<br /><br />您可以 <a href='http://www.xzdsw.com/' title='新洲都市网' target='_blank'><u>联系我们</u></a> 购买该产品的注册码，然后将获得的注册码填入程序设置中的 SerialNumber 项。";
            }
        }

        private bool IsFoundRegisterLog
        {
            get
            {
                string text1 = "http://www.xzdsw.com/business/buyer_validate.aspx?domain=" + Fetch.ServerDomain + "&product=" + this.m_product;
                try
                {
                    byte[] buffer1 = new WebClient().DownloadData(text1);
                    string text2 = Encoding.Default.GetString(buffer1).Trim();
                    return (text2 != "0");
                }
                catch
                {
                    return true;
                }
            }
        }

        private bool IsMatch
        {
            get
            {
                string text1 = Config.Settings["SerialNumber"].Trim().ToUpper();
                if (text1.Split(new char[] { '-' }).Length != 4)
                {
                    return false;
                }
                return (this.GenerateSerialNumber(Fetch.ServerDomain) == text1);
            }
        }

        public bool IsRegistered
        {
            get
            {
                if (!this.IsMatch)
                {
                    return false;
                }
                return true;
            }
        }


        private string m_product;
    }
}