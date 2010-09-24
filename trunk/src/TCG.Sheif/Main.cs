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

        private TreeNode selectNode;

        private void Main_Load(object sender, EventArgs e)
        {
            this.ServiceInit();

            this.CategoriesInit();

            TCG.SheifConfig.SheifCategorieConfig[] sheifconfigs = sheifConfig.GetSheifCategorieConfigs();

            for (int i = 0; i < sheifconfigs.Length; i++)
            {
                TCG.SheifConfig.SheifCategorieConfig scc = sheifconfigs[i];
                TCG.SheifService.SheifSourceInfo sourc = sheifService.GetSheifSourceInfoById(scc.SheifSourceId);
                if (sourc != null)
                {
                    
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
           
        }

    }
}
