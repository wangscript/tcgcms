using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TCG.Sheif
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            SheifSourceDebug sheifsourcedebug = new SheifSourceDebug();
            DialogResult debugresult = sheifsourcedebug.ShowDialog(this);

            if (DialogResult.OK == debugresult)
            {

            }

            return;

            TCG.SheifService.SheifService sheif = new TCG.SheifService.SheifService();
            TCG.SheifConfig.SheifConfig sheifconfig = new TCG.SheifConfig.SheifConfig();
            TCG.ResourcesService.ResourcesService ResourcesService = new TCG.ResourcesService.ResourcesService();

            TCG.SheifService.TCGSoapHeader myHeader = new TCG.SheifService.TCGSoapHeader();
            myHeader.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            sheif.TCGSoapHeaderValue = myHeader;


            TCG.SheifConfig.TCGSoapHeader myHeader1 = new TCG.SheifConfig.TCGSoapHeader();
            myHeader1.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            sheifconfig.TCGSoapHeaderValue = myHeader1;

            TCG.ResourcesService.TCGSoapHeader myHeader2 = new TCG.ResourcesService.TCGSoapHeader();
            myHeader2.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            ResourcesService.TCGSoapHeaderValue = myHeader2;

            TCG.SheifConfig.SheifCategorieConfig[] sheifconfigs = sheifconfig.GetSheifCategorieConfigs();

            for (int i = 0; i < sheifconfigs.Length; i++)
            {
                TCG.SheifConfig.SheifCategorieConfig scc = sheifconfigs[i];
                TCG.SheifService.SheifSourceInfo sourc = sheif.GetSheifSourceInfoById(scc.SheifSourceId);
                if (sourc != null)
                {
                    //获得列表页面HTML
                    string html = HttpWebHandlers.GetHtml(string.Format(sourc.SourceUrl, 1), Encoding.GetEncoding(sourc.CharSet));

                    if (sourc.IsRss)
                    {
                        XmlDocument document = null;
                        try
                        {
                            document = new XmlDocument();
                            document.LoadXml(html);
                        }
                        catch { }

                        if (document != null)
                        {
                            XmlNodeList mylist = document.GetElementsByTagName("item");
                            if (mylist != null)
                            {
                                foreach (XmlElement node in mylist)
                                {
                                    TCG.ResourcesService.Resources res1 = new TCG.ResourcesService.Resources();
                                    res1.vcTitle = node.SelectSingleNode("title").InnerText;
                                    res1.SheifUrl = node.SelectSingleNode("link").InnerText;
                                    res1.dAddDate = objectHandlers.ToTime(node.SelectSingleNode("pubDate").InnerText);

                                    string reshtml = HttpWebHandlers.GetHtml(res1.SheifUrl, Encoding.GetEncoding(sourc.CharSet));
                                    Match m = Regex.Match(html, sourc.TopicRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                                    if (m.Success)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    else
                    {


                        //获得列表区域
                        Match m = Regex.Match(html, sourc.ListAreaRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        if (m.Success)
                        {
                            string listcontenthtml = m.Value;

                            //获得文章页面URL

                            MatchCollection matchs = Regex.Matches(listcontenthtml, sourc.TopicListRole, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                            if (matchs.Count > 0)
                            {
                                foreach (Match mt in matchs)
                                {
                                    TCG.ResourcesService.Resources res = new TCG.ResourcesService.Resources();

                                    int rtn = ResourcesService.CreateResources(res);
                                }
                            }
                        }
                    }
                }
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SheifSourceDebug sheifsourcedebug = new SheifSourceDebug();
            DialogResult debugresult =  sheifsourcedebug.ShowDialog(this);

            if (DialogResult.OK == debugresult)
            {

            }
        }
    }
}
