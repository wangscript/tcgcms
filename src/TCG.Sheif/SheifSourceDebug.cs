using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCG.Sheif
{
    public partial class SheifSourceDebug : Form
    {
        private TCG.SheifService.SheifSourceInfo sheifourceinfo;
        List<TCG.ResourcesService.Resources> topics = null;

        public SheifSourceDebug()
        {
            InitializeComponent();
        }

        private void SheifSourceDebug_Load(object sender, EventArgs e)
        {
            sheifourceinfo = new TCG.SheifService.SheifSourceInfo();
            this.tblisttopicdatarole.Text = "$1,SheifUrl\r\n$2,vcTitle";
            this.tbtopicdatarole.Text = "$1,vcTitle\r\n$2,vcContent";
        }

        private void SheifSourceInfoInit()
        {
            this.sheifourceinfo.TopicDataRole = this.tbtopicdatarole.Text;
            this.sheifourceinfo.SourceUrl = this.tblistpage.Text;
            this.sheifourceinfo.CharSet = this.tbCharSet.Text;
            this.sheifourceinfo.ListAreaRole = this.tblistrole.Text;
            this.sheifourceinfo.TopicListRole = this.tblistlinkrole.Text;
            this.sheifourceinfo.TopicListDataRole = this.tblisttopicdatarole.Text;
            this.sheifourceinfo.TopicRole = this.tbtopicinforole.Text;
            this.sheifourceinfo.TopicDataRole = this.tbtopicdatarole.Text;
            this.sheifourceinfo.TopicPagerOld = this.tbtopicpagerOld.Text;
            this.sheifourceinfo.TopicPagerTemp = this.tbtopicpagertemp.Text;
            this.sheifourceinfo.SourceName = this.tbshiefsourcname.Text;
            this.sheifourceinfo.IsRss = this.checkBox1.Checked;
        }

        private void btlistsheif_Click(object sender, EventArgs e)
        {
            this.SheifSourceInfoInit();
            int spage = objectHandlers.StrToInt(this.tbListPageStart.Text);
            int epage = objectHandlers.StrToInt(this.tbListPageEnd.Text);

            string errText = string.Empty;
            int rtn = SheifHandlers.SheifTopicList(ref errText, ref topics, this.sheifourceinfo, spage, epage);

            this.thdrTestRes.DataSource = topics;
        }

        private void btDebugTopicInfo_Click(object sender, EventArgs e)
        {
            this.SheifSourceInfoInit();
            if (this.thdrTestRes.Rows.Count == 0) return;

            DataGridViewRow row = this.thdrTestRes.Rows[0];
            TCG.ResourcesService.Resources topic = new TCG.ResourcesService.Resources();

            string errText = string.Empty;
            string Url = topics[0].SheifUrl;

            int rtn = SheifHandlers.SheifTopic(ref errText, ref topic, this.sheifourceinfo, Url);
            if (rtn < 0)
            {
                return;
            }

            this.sr_title.Text = topic.vcTitle;
            this.sr_content.DocumentText = topic.vcContent;
        }

        private void btPostRole_Click(object sender, EventArgs e)
        {
            this.SheifSourceInfoInit();

            TCG.SheifService.SheifService sheif = new TCG.SheifService.SheifService();

            TCG.SheifService.TCGSoapHeader myHeader = new TCG.SheifService.TCGSoapHeader();
            myHeader.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            sheif.TCGSoapHeaderValue = myHeader;

            int rtn = sheif.AddSheifSource(this.sheifourceinfo);

            if (rtn < 0)
            {
                MessageBox.Show("导入失败！" + rtn.ToString());
                return;
            }

            MessageBox.Show("导入成功！");
        }
    }
}
