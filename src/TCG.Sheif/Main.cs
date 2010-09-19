using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            //TCG.SheifService.SheifService sheif = new TCG.SheifService.SheifService();
            //TCG.SheifService.TCGSoapHeader myHeader = new TCG.SheifService.TCGSoapHeader();
            //myHeader.PassWord = "593fd7e4-1c00-4ade-bab0-426342b9e0d9";
            //sheif.TCGSoapHeaderValue = myHeader;

            //TCG.SheifService.SheifSourceInfo [] sourcs =  sheif.GetAllSheifSourceInfos();

        }
    }
}
