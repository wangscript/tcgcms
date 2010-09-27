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
        public List<TCG.ResourcesService.Resources> topics = null;
        public string Resourceid = string.Empty;
        public TCG.SheifService.SheifService sheif;

        public SheifSourceDebug()
        {
            InitializeComponent();
        }

        private void SheifSourceDebug_Load(object sender, EventArgs e)
        {
            sheifourceinfo = new TCG.SheifService.SheifSourceInfo();
            if (!string.IsNullOrEmpty(this.Resourceid))
            {
                sheifourceinfo = this.sheif.GetSheifSourceInfoById(this.Resourceid);
                if (sheifourceinfo != null)
                {
                    this.SetSheifSource();
                }
            }
            else
            {
                this.tblisttopicdatarole.Text = "$1,SheifUrl\r\n$2,vcTitle";
                this.tbtopicdatarole.Text = "$1,vcTitle\r\n$2,vcContent";
            }
        }

        private void SetSheifSource()
        {
            this.tbtopicdatarole.Text = this.sheifourceinfo.TopicDataRole;
            this.tblistpage.Text = this.sheifourceinfo.SourceUrl;
            this.tbCharSet.Text = this.sheifourceinfo.CharSet;
            this.tblistrole.Text = this.sheifourceinfo.ListAreaRole;
            this.tblistlinkrole.Text = this.sheifourceinfo.TopicListRole;
            this.tblisttopicdatarole.Text = this.sheifourceinfo.TopicListDataRole;
            this.tbtopicinforole.Text = this.sheifourceinfo.TopicRole;
            this.tbtopicdatarole.Text = this.sheifourceinfo.TopicDataRole;
            this.tbtopicpagerOld.Text = this.sheifourceinfo.TopicPagerOld;
            this.tbtopicpagertemp.Text = this.sheifourceinfo.TopicPagerTemp;
            this.tbshiefsourcname.Text = this.sheifourceinfo.SourceName;
            this.checkBox1.Checked = this.sheifourceinfo.IsRss;
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
            if (rtn < 0)
            {
                MessageBox.Show(rtn.ToString());
            }
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
            int rtn = 0;
            if (string.IsNullOrEmpty(this.Resourceid))
            {
                rtn = sheif.AddSheifSource(this.sheifourceinfo);
            }
            else
            {
                rtn = sheif.UpdateSheifSource(this.sheifourceinfo);
            }

            if (rtn < 0)
            {
                MessageBox.Show("导入失败！" + rtn.ToString());
                return;
            }

            MessageBox.Show("导入成功！");
        }
    }
}
