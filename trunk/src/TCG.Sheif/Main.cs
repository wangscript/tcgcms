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

        public TCG.SheifConfig.SheifConfig sheifConfig;
        public TCG.SheifService.SheifService sheifService;
        public TCG.ResourcesService.ResourcesService resourcesService;
        public TCG.CategorieService.CategorieService categorieService;
        public TCG.CategorieService.Categories[] categories;
        public TCG.SheifService.SheifSourceInfo[] sheifsourceinfos;

        private TreeNode selectNode;
         

        private void Main_Load(object sender, EventArgs e)
        {
            this.ServiceInit();

            this.CategoriesInit();

            StartSheif();
        }

        private void StartSheif()
        {
            TCG.SheifConfig.SheifCategorieConfig[] sheifconfigs = sheifConfig.GetSheifCategorieConfigs();

            if (sheifconfigs != null)
            {
                for (int i = 0; i < sheifconfigs.Length; i++)
                {
                    TCG.SheifConfig.SheifCategorieConfig scc = sheifconfigs[i];
                    if (scc.SheifSourceId.IndexOf(",") == -1)
                    {
                        TCG.SheifService.SheifSourceInfo sourc = sheifService.GetSheifSourceInfoById(scc.SheifSourceId);
                        if (sourc != null)
                        {
                            this.SheifRes(sourc, scc);
                        }
                    }
                    else
                    {
                        string[] sourceids = scc.SheifSourceId.Split(',');
                        for (int n = 0; n < sourceids.Length; i++)
                        {
                            TCG.SheifService.SheifSourceInfo sourc1 = sheifService.GetSheifSourceInfoById(sourceids[n]);
                            if (sourc1 != null)
                            {
                                this.SheifRes(sourc1, scc);
                            }
                        }
                    }
                }
            }
        }

        private void SheifRes(TCG.SheifService.SheifSourceInfo sourc, TCG.SheifConfig.SheifCategorieConfig scc)
        {
            string errText = string.Empty;
            List<TCG.ResourcesService.Resources> res = new List<TCG.ResourcesService.Resources>();
            int rtn = SheifHandlers.SheifTopicList(ref errText, ref res, sourc, 1, 1);
            if (res != null && res.Count > 0)
            {
                for (int i = 0; i < res.Count; i++)
                {
                    TCG.ResourcesService.Resources resinfo =new TCG.ResourcesService.Resources();
                    rtn = SheifHandlers.SheifTopic(ref errText, ref resinfo, sourc, res[i].SheifUrl);

                    this.dataGridView1.Rows.Add(resinfo.vcTitle,resinfo.SheifUrl,resinfo.dAddDate.ToString());

                }
            }
        }

        private void CategoriesInit()
        {
            categories = categorieService.GetAllCategorieEntity();
            this.trCategories.Nodes.Add("0", "所有分类");
            this.InitTreeViewNodes(this.trCategories.Nodes[0], "0");
        }

        private void InitTreeViewNodes(TreeNode node, string parentid)
        {
            for (int i = 0; i < this.categories.Length;i++)
            {
                TCG.CategorieService.Categories tempcategories = this.categories[i];
                if (tempcategories.Parent == parentid)
                {
                    TreeNode tnode = new TreeNode();
                    tnode.Name = tempcategories.Id;
                    tnode.Text = tempcategories.vcClassName;
                    node.Nodes.Add(tnode);
                    this.InitTreeViewNodes(tnode, tempcategories.Id);
                }
            }
        }

        private void ServiceInit()
        {
            sheifService = new TCG.SheifService.SheifService();
            sheifConfig = new TCG.SheifConfig.SheifConfig();
            resourcesService = new TCG.ResourcesService.ResourcesService();
            categorieService = new TCG.CategorieService.CategorieService();

            TCG.SheifService.TCGSoapHeader myHeader = new TCG.SheifService.TCGSoapHeader();
            myHeader.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            sheifService.TCGSoapHeaderValue = myHeader;


            TCG.SheifConfig.TCGSoapHeader myHeader1 = new TCG.SheifConfig.TCGSoapHeader();
            myHeader1.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            sheifConfig.TCGSoapHeaderValue = myHeader1;

            TCG.ResourcesService.TCGSoapHeader myHeader2 = new TCG.ResourcesService.TCGSoapHeader();
            myHeader2.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            resourcesService.TCGSoapHeaderValue = myHeader2;

            TCG.CategorieService.TCGSoapHeader myHeader3 = new TCG.CategorieService.TCGSoapHeader();
            myHeader3.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            categorieService.TCGSoapHeaderValue = myHeader3;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SheifSourceDebug sheifsourcedebug = new SheifSourceDebug();
            DialogResult debugresult =  sheifsourcedebug.ShowDialog(this);

            if (DialogResult.OK == debugresult)
            {

            }
        }

        private void trCategories_MouseClick(object sender, MouseEventArgs e)
        {
            trCategories.ContextMenuStrip = null;
            selectNode = trCategories.GetNodeAt(e.X, e.Y);

            if (e.Button == MouseButtons.Right)
            {

                ContextMenuStrip con = new ContextMenuStrip();

                con.Items.Add("配置爬虫", null, new EventHandler(trCategories_SheifCategorieConfig));

                trCategories.ContextMenuStrip = con;
                con.Show(trCategories, new Point(e.X + 10, e.Y));
                trCategories.ContextMenuStrip = null;
            }
        }

        private void trCategories_SheifCategorieConfig(object sender, EventArgs e)
        {
            string selectid = this.selectNode.Name;
            if (selectid == "0") return;

            sheifsourceinfos = this.sheifService.GetAllSheifSourceInfos();

            CategorieSheifSourccConfig categorieSheifSourccConfig = new CategorieSheifSourccConfig();
            categorieSheifSourccConfig.localCategorieId = selectid;
            categorieSheifSourccConfig.sheifsourceinfos = sheifsourceinfos;
            categorieSheifSourccConfig.categorieservice = this.categorieService;
            categorieSheifSourccConfig.sheifConfig = this.sheifConfig;

            DialogResult configresult = categorieSheifSourccConfig.ShowDialog(this);

            if (DialogResult.OK == configresult)
            {

            }
           
        }

    }
}
