
/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

namespace TCG.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Web.UI;

    public class CodeImage : Page
    {
        protected CodeImage()
        {
            this._width = 95;
            this._height = 18;
            this._fontSize = 12;
            this._fontFamily = "Arial Black";
            this._bold = false;
            this._italic = false ;
            this._impurity = 4;
        }

        private void FillImageProperties()
        {
            string text1 = string.Empty;
            text1 = objectHandlers.Get("width");
            if (objectHandlers.IsNumeric(text1))
            {
                this._width = int.Parse(text1);
            }
            text1 = objectHandlers.Get("height");
            if (objectHandlers.IsNumeric(text1))
            {
                this._height = int.Parse(text1);
            }
            text1 = objectHandlers.Get("fontsize");
            if (objectHandlers.IsNumeric(text1))
            {
                this._fontSize = int.Parse(text1);
            }
            text1 = objectHandlers.Get("fontfamily");
            if (text1.Length > 0)
            {
                this._fontFamily = text1;
            }
            text1 = objectHandlers.Get("bold").ToLower();
            if (text1.Length > 0)
            {
                this._bold = text1 == "true";
            }
            text1 = objectHandlers.Get("italic").ToLower();
            if (text1.Length > 0)
            {
                this._italic = text1 == "true";
            }
            text1 = objectHandlers.Get("impurity");
            if (objectHandlers.IsNumeric(text1))
            {
                this._impurity = int.Parse(text1);
            }
        }

        private FontStyle GetFontStyle()
        {
            if (this._bold && this._italic)
            {
                return (FontStyle.Italic | FontStyle.Bold);
            }
            if (this._bold)
            {
                return FontStyle.Bold;
            }
            if (this._italic)
            {
                return FontStyle.Italic;
            }
            return FontStyle.Regular;
        }

        private Bitmap GetneralCodeImage()
        {
            Bitmap bitmap1 = new Bitmap(this._width, this._height);
            Graphics graphics1 = Graphics.FromImage(bitmap1);
            graphics1.Clear(Color.Silver);
            Pen pen1 = new Pen(Color.Black, 1f);
            pen1.DashStyle = DashStyle.DashDotDot;
            Point[] pointArray1 = this.GetRandomPointGroup();
            for (int num1 = 0; num1 < pointArray1.Length; num1 += 2)
            {
                graphics1.DrawLine(pen1, pointArray1[num1], pointArray1[num1 + 1]);
            }
            graphics1.DrawRectangle(pen1, 0, 0, (this._width-1), (this._height-1));
            string text1 = objectHandlers.RandomStr(4);
            SessionState.Set("verification", text1.ToLower());
            string text2 = "";
            foreach(char i in text1)
            {
                text2 += " " + i;
            }
            graphics1.DrawString(text2, new Font(this._fontFamily, (float) this._fontSize, this.GetFontStyle()), 
                new SolidBrush(Color.DarkSlateGray), (float) new Random().Next(0,17), (float) new Random().Next(-3,0));
            graphics1.Dispose();
            return bitmap1;
        }

        private Point[] GetRandomPointGroup()
        {
            Point[] pointArray1 = new Point[this._impurity * 2];
            for (int num1 = 0; num1 < pointArray1.Length; num1++)
            {
                int num2 = new Random().Next(0x989680, 0x5f5e0ff);
                int num3 = new Random((num2 / (num1 + 2)) * (num1 + 1)).Next(0, this._width);
                int num4 = new Random((num2 / (num3 + 2)) * (num3 + 1)).Next(0, this._height);
                pointArray1[num1] = new Point(num3, num4);
            }
            return pointArray1;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.FillImageProperties();
            base.Response.ContentType = "image/gif";
            this.GetneralCodeImage().Save(base.Response.OutputStream, ImageFormat.Gif);
        }


        private bool _bold;
        private string _fontFamily;
        private int _fontSize;
        private int _height;
        private int _impurity;
        private bool _italic;
        private int _width;
    }
}

