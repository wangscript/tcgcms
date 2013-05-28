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
    public partial class TexExChange : Form
    {
        public TexExChange()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = string.Format(@"{0}", this.textBox1.Text.Replace("\r\n","\\r\\n"));
        }
    }
}
