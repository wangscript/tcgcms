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
    public partial class SheifSource : Form
    {
        public SheifSource()
        {
            InitializeComponent();
        }

        public TCG.SheifService.SheifService sheifService;
        public TCG.SheifService.SheifSourceInfo[] sourceinfos;

        private void SheifSource_Load(object sender, EventArgs e)
        {
            sourceinfos = sheifService.GetAllSheifSourceInfos();

            this.dataGridView1.DataSource = sourceinfos;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string sourceid = (dataGridView1[dataGridView1.Columns["Id"].Index, e.RowIndex].Value + "").ToString();

            if (!string.IsNullOrEmpty(sourceid))
            {
                SheifSourceDebug sheifsourcedebug = new SheifSourceDebug();
                sheifsourcedebug.Resourceid = sourceid;
                sheifsourcedebug.sheif = this.sheifService;
                DialogResult debugresult = sheifsourcedebug.ShowDialog(this);

                if (DialogResult.OK == debugresult)
                {

                }
            }
        }
    }
}
