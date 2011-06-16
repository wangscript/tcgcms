using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;

using TCG.Utils;
using TCG.Entity;

namespace TCG.CMS.WebUi
{

    public partial class ConfigSetting : BasePage
    {
        private string _file = objectHandlers.MapPath("/config/baseConfig.Config");
        private StringBuilder _sb;

        protected void Page_Load(object sender, EventArgs e)
        {
            //检测管理员登录
            base.handlerService.manageService.adminHandlers.CheckAdminLogin();
            base.handlerService.manageService.adminHandlers.CheckAdminPop(20);

            if (!Page.IsPostBack)
            {
                this.GetData();
            }
            else
            {
                XmlDocument document = new XmlDocument();
                document.Load(this._file);
                foreach (XmlElement element in document.GetElementsByTagName("Item"))
                {
                    string innerText = element.SelectSingleNode("Name").InnerText;
                    string str2 = objectHandlers.Post(innerText);
                    if (element.SelectSingleNode("Mode").InnerText.ToUpper() == "CDATA")
                    {
                        element.SelectSingleNode("Value").InnerXml = "<![CDATA[" + str2 + "]]>";
                    }
                    else
                    {
                        element.SelectSingleNode("Value").InnerText = str2;
                    }
                }
                try
                {
                    document.Save(this._file);
                    ConfigServiceEx.BaseConfigInit();
                    base.AjaxErch("1");
                }
                catch
                {
                    base.AjaxErch("-1000000019");
                }
            }
        }

        private void GetData()
        {
            XmlDocument document = new XmlDocument();
            document.Load(this._file);
            XmlNodeList elementsByTagName = document.GetElementsByTagName("Item");
            int num = 0;
            foreach (XmlElement element in elementsByTagName)
            {
                ConfigItem item = new ConfigItem();
                item.Name = element.SelectSingleNode("Name").InnerText.ToString();
                item.Explain = element.SelectSingleNode("Explain").InnerText.ToString();
                item.Mode = element.SelectSingleNode("Mode").InnerText.ToString();
                item.Value = element.SelectSingleNode("Value").InnerText.ToString();
                XmlNode node = element.SelectSingleNode("Pattern");
                if (node != null)
                {
                    item.Pattern = node.InnerText.ToString();
                }
                node = element.SelectSingleNode("Range");
                if (node != null)
                {
                    item.Range = node.InnerText.ToString();
                }
                node = element.SelectSingleNode("RangeHint");
                if (node != null)
                {
                    item.RangeHint = node.InnerText.ToString();
                }
                node = element.SelectSingleNode("Required");
                if (node != null)
                {
                    item.IsRequired = node.InnerText.ToString().ToLower() == "true";
                }
                this.AddItem(item);
            }
            this.items.Text = this.sb.ToString();
        }

        private void AddItem(ConfigItem item)
        {
            this.sb.Append("<div class='Page_arrb'>");
            this.sb.AppendFormat("\t<span class='p_a_t wd'>{0}：</span>\r\n", item.Name);
            string format = string.Empty;
            string str3 = item.Mode.ToLower();
            if (str3 != null)
            {
                if ((str3 != "text") && (str3 != "numeric"))
                {
                }
                format = "<input id=\"{0}\" name=\"{0}\" type=\"text\" value=\"{1}\" class=\"itxt1 wd2\" onfocus=\"this.className=\'itxt2 wd2'\" onblur=\"this.className='itxt1 wd2'\"/>";
                this.sb.AppendFormat(format, new object[] { item.Name, objectHandlers.TextEncode(item.Value) });
            }
            this.sb.AppendFormat("<span class='info1' id='{0}_msg'>{1}</span>\r\n", item.Name, item.Explain);
            this.sb.Append("</div>\r\n");
        }

        private StringBuilder sb
        {
            get
            {
                if (this._sb == null)
                {
                    this._sb = new StringBuilder();
                }
                return this._sb;
            }
        }
    }
}