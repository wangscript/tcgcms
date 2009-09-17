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
                return "�Բ��𣬱���Ʒ���ֹ���Ŀǰֻ����ҵ���û����š�<br /><br />������ <a href='http://www.xzdsw.com/' title='���޶�����' target='_blank'><u>��ϵ����</u></a> ����ò�Ʒ��ע���룬Ȼ�󽫻�õ�ע����������������е� SerialNumber �";
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