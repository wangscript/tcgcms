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
        public TCG.CategorieService.CategorieService categorieservice;
        public TCG.SheifConfig.SheifConfig sheifConfig;
        public TCG.SheifService.SheifSourceInfo[] sheifsourceinfos;
        public TCG.CategorieService.Categories categories = null;
        public TCG.SheifConfig.SheifCategorieConfig sheifcategorieconfig = null; 

        private void CategorieSheifSourccConfig_Load(object sender, EventArgs e)
        {
            categories = categorieservice.GetCategorieById(this.localCategorieId);
            if (categories == null || sheifConfig == null)
            {
                return;
            }

            this.Text = categories.vcClassName + "源网站配置";

            DataGridViewComboBoxColumn sheifsources = (DataGridViewComboBoxColumn)this.dataGridView1.Columns[0];

            if (sheifsourceinfos != null)
            {
                for (int i = 0; i < sheifsourceinfos.Length; i++)
                {
                    TCG.SheifService.SheifSourceInfo sourceinfo = sheifsourceinfos[i];
                    sheifsources.Items.Add(sourceinfo.SourceName + "|" + sourceinfo.Id);
                }
            }


            sheifcategorieconfig = sheifConfig.GetSheifCategorieConfigById(this.localCategorieId);
            if (sheifcategorieconfig == null)
            {
                sheifcategorieconfig = new TCG.SheifConfig.SheifCategorieConfig();
                sheifcategorieconfig.LocalCategorieId = this.localCategorieId;
            }
            else
            {
                if (sheifcategorieconfig.SheifSourceId.IndexOf(",") > -1)
                {
                    string[] sccs = sheifcategorieconfig.SheifSourceId.Split(',');
                    for (int i = 0; i < sccs.Length; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewComboBoxCell cel = new DataGridViewComboBoxCell(); 
                        if (sheifsourceinfos != null)
                        {
                            for (int n = 0; n < sheifsourceinfos.Length; n++)
                            {
                                TCG.SheifService.SheifSourceInfo sourceinfo = sheifsourceinfos[n];
                                cel.Items.Add(sourceinfo.SourceName + "|" + sourceinfo.Id);
                                if (sourceinfo.Id == sccs[i])
                                {
                                    cel.Value = sourceinfo.SourceName + "|" + sourceinfo.Id;
                                }
                            }
                        }
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[i].Cells[0] = cel; 

                        
                    }
                }
                else
                {

                    DataGridViewComboBoxCell cel = new DataGridViewComboBoxCell();

                    if (sheifsourceinfos != null)
                    {
                        for (int i = 0; i < sheifsourceinfos.Length; i++)
                        {
                            TCG.SheifService.SheifSourceInfo sourceinfo = sheifsourceinfos[i];
                            cel.Items.Add(sourceinfo.SourceName + "|" + sourceinfo.Id);
                            if (sourceinfo.Id == sheifcategorieconfig.SheifSourceId)
                            {
                                cel.Value = sourceinfo.SourceName + "|" + sourceinfo.Id;
                            }
                        }
                    }
                    this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[0].Cells[0] = cel; 
                }
            }

            this.Closed += new EventHandler(CategorieSheifSourccConfig_Closed);

        }

        public void CategorieSheifSourccConfig_Closed(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                string sources = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {
                        string[] arrs = dataGridView1.Rows[i].Cells[0].Value.ToString().Split('|');
                        if (sources.IndexOf(arrs[1]) == -1)
                        {
                            string text = string.IsNullOrEmpty(sources) ? "" : ",";
                            sources += text + arrs[1];
                        }
                    }
                }

                this.sheifcategorieconfig.SheifSourceId = sources;
                if (string.IsNullOrEmpty(this.sheifcategorieconfig.Id))
                {
                    this.sheifConfig.CreateSheifCategorieConfig(this.sheifcategorieconfig);
                }
                else
                {
                    this.sheifConfig.UpdateSheifCategorieConfig(this.sheifcategorieconfig);
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string value = (dataGridView1[e.ColumnIndex, e.RowIndex].Value + "").ToString();
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("分类不能为空");
                    //dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    return;
                }

                if (IsInList(value, e.RowIndex))
                {
                    MessageBox.Show("已经设置了该分类");
                    //dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    return;
                }
            }
        }

        private bool IsInList(string value,int rowindex)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i != rowindex&&(dataGridView1.Rows[i].Cells[0].Value + "").ToString() == value) return true;
            }
            return false;
        }

    }
}
