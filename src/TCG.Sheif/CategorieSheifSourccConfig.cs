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
    public partial class CategorieSheifSourccConfig : Form
    {
        public CategorieSheifSourccConfig()
        {
            InitializeComponent();
        }

        public string localCategorieId = string.Empty;
        public TCG.SheifService.SheifSourceInfo[] sheifsourceinfos;

        private void CategorieSheifSourccConfig_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn sheifsources = (DataGridViewComboBoxColumn)this.dataGridView1.Columns[1];

            if (sheifsourceinfos != null)
            {
                for (int i = 0; i < sheifsourceinfos.Length; i++)
                {
                    TCG.SheifService.SheifSourceInfo sourceinfo =  sheifsourceinfos[i];
                    sheifsources.Items.Add(sourceinfo.SourceName + "|" + sourceinfo.Id);
                }
            }
        }
    }
}
