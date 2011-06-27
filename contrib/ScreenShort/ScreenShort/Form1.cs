using System;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Teboscreen;

namespace ScreenShort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.ShowInTaskbar = false; 
            this.Hide();
            timer1.Interval = 1000*30*60;
            timer1.Start();          
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle bounds = Screen.GetBounds(Screen.GetBounds(Point.Empty));
            ScreenShot.CaptureImage(Point.Empty, Point.Empty, bounds, "d:/screenshot/" + DateTime.Now.ToString().Replace(":", "").Replace("-", "") + ".jpg");
        }


    }
}
