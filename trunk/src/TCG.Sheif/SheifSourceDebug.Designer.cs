namespace TCG.Sheif
{
    partial class SheifSourceDebug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label12 = new System.Windows.Forms.Label();
            this.btDebugTopicInfo = new System.Windows.Forms.Button();
            this.tbtopicdatarole = new System.Windows.Forms.TextBox();
            this.gbresview = new System.Windows.Forms.GroupBox();
            this.thdrTestRes = new System.Windows.Forms.DataGridView();
            this.gblisttest = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tblisttopicdatarole = new System.Windows.Forms.TextBox();
            this.tbshiefsourcname = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbtopicpagertemp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbtopicpagerOld = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbtopicinforole = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tblistlinkrole = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCharSet = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbListPageEnd = new System.Windows.Forms.TextBox();
            this.tbListPageStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tblistrole = new System.Windows.Forms.TextBox();
            this.tblistpage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btPostRole = new System.Windows.Forms.Button();
            this.btlistsheif = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sr_content = new System.Windows.Forms.WebBrowser();
            this.sr_title = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gbresview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thdrTestRes)).BeginInit();
            this.gblisttest.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 408);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "文章项匹配规则：";
            // 
            // btDebugTopicInfo
            // 
            this.btDebugTopicInfo.Location = new System.Drawing.Point(154, 517);
            this.btDebugTopicInfo.Name = "btDebugTopicInfo";
            this.btDebugTopicInfo.Size = new System.Drawing.Size(64, 23);
            this.btDebugTopicInfo.TabIndex = 19;
            this.btDebugTopicInfo.Text = "调试详细页";
            this.btDebugTopicInfo.UseVisualStyleBackColor = true;
            this.btDebugTopicInfo.Click += new System.EventHandler(this.btDebugTopicInfo_Click);
            // 
            // tbtopicdatarole
            // 
            this.tbtopicdatarole.Location = new System.Drawing.Point(111, 402);
            this.tbtopicdatarole.Multiline = true;
            this.tbtopicdatarole.Name = "tbtopicdatarole";
            this.tbtopicdatarole.Size = new System.Drawing.Size(175, 56);
            this.tbtopicdatarole.TabIndex = 24;
            // 
            // gbresview
            // 
            this.gbresview.Controls.Add(this.thdrTestRes);
            this.gbresview.Location = new System.Drawing.Point(325, 5);
            this.gbresview.Name = "gbresview";
            this.gbresview.Size = new System.Drawing.Size(337, 535);
            this.gbresview.TabIndex = 18;
            this.gbresview.TabStop = false;
            this.gbresview.Text = "调试结果";
            // 
            // thdrTestRes
            // 
            this.thdrTestRes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.thdrTestRes.Location = new System.Drawing.Point(6, 16);
            this.thdrTestRes.Name = "thdrTestRes";
            this.thdrTestRes.RowTemplate.Height = 23;
            this.thdrTestRes.Size = new System.Drawing.Size(325, 513);
            this.thdrTestRes.TabIndex = 0;
            // 
            // gblisttest
            // 
            this.gblisttest.Controls.Add(this.checkBox1);
            this.gblisttest.Controls.Add(this.label13);
            this.gblisttest.Controls.Add(this.label12);
            this.gblisttest.Controls.Add(this.tbtopicdatarole);
            this.gblisttest.Controls.Add(this.label11);
            this.gblisttest.Controls.Add(this.tblisttopicdatarole);
            this.gblisttest.Controls.Add(this.tbshiefsourcname);
            this.gblisttest.Controls.Add(this.label10);
            this.gblisttest.Controls.Add(this.tbtopicpagertemp);
            this.gblisttest.Controls.Add(this.label9);
            this.gblisttest.Controls.Add(this.tbtopicpagerOld);
            this.gblisttest.Controls.Add(this.label8);
            this.gblisttest.Controls.Add(this.tbtopicinforole);
            this.gblisttest.Controls.Add(this.label7);
            this.gblisttest.Controls.Add(this.tblistlinkrole);
            this.gblisttest.Controls.Add(this.label6);
            this.gblisttest.Controls.Add(this.tbCharSet);
            this.gblisttest.Controls.Add(this.label5);
            this.gblisttest.Controls.Add(this.label4);
            this.gblisttest.Controls.Add(this.label2);
            this.gblisttest.Controls.Add(this.tbListPageEnd);
            this.gblisttest.Controls.Add(this.tbListPageStart);
            this.gblisttest.Controls.Add(this.label3);
            this.gblisttest.Controls.Add(this.tblistrole);
            this.gblisttest.Controls.Add(this.tblistpage);
            this.gblisttest.Controls.Add(this.label1);
            this.gblisttest.Location = new System.Drawing.Point(10, 5);
            this.gblisttest.Name = "gblisttest";
            this.gblisttest.Size = new System.Drawing.Size(307, 501);
            this.gblisttest.TabIndex = 17;
            this.gblisttest.TabStop = false;
            this.gblisttest.Text = "列表调试";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(76, 75);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(36, 16);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "是";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "是否为RSS：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 208);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "列表文章项匹配规则：";
            // 
            // tblisttopicdatarole
            // 
            this.tblisttopicdatarole.Location = new System.Drawing.Point(133, 202);
            this.tblisttopicdatarole.Multiline = true;
            this.tblisttopicdatarole.Name = "tblisttopicdatarole";
            this.tblisttopicdatarole.Size = new System.Drawing.Size(155, 56);
            this.tblisttopicdatarole.TabIndex = 22;
            // 
            // tbshiefsourcname
            // 
            this.tbshiefsourcname.Location = new System.Drawing.Point(68, 16);
            this.tbshiefsourcname.Name = "tbshiefsourcname";
            this.tbshiefsourcname.Size = new System.Drawing.Size(221, 21);
            this.tbshiefsourcname.TabIndex = 21;
            this.tbshiefsourcname.Text = "绿色软件 → 编程开发 -> .Net 专栏";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "源名称：";
            // 
            // tbtopicpagertemp
            // 
            this.tbtopicpagertemp.Location = new System.Drawing.Point(185, 465);
            this.tbtopicpagertemp.Name = "tbtopicpagertemp";
            this.tbtopicpagertemp.Size = new System.Drawing.Size(100, 21);
            this.tbtopicpagertemp.TabIndex = 19;
            this.tbtopicpagertemp.Text = "_{0}.html";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(162, 471);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "-";
            // 
            // tbtopicpagerOld
            // 
            this.tbtopicpagerOld.Location = new System.Drawing.Point(68, 466);
            this.tbtopicpagerOld.Name = "tbtopicpagerOld";
            this.tbtopicpagerOld.Size = new System.Drawing.Size(83, 21);
            this.tbtopicpagerOld.TabIndex = 17;
            this.tbtopicpagerOld.Text = "Article_242_2.html";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 468);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "文章分页：";
            // 
            // tbtopicinforole
            // 
            this.tbtopicinforole.Location = new System.Drawing.Point(68, 334);
            this.tbtopicinforole.Multiline = true;
            this.tbtopicinforole.Name = "tbtopicinforole";
            this.tbtopicinforole.Size = new System.Drawing.Size(220, 61);
            this.tbtopicinforole.TabIndex = 15;
            this.tbtopicinforole.Text = "<div class=\"title\">(.*?)</div>[\\s\\S]*<span id=\"adr\"><script type=\"text/javascript" +
                "\" language=\"javascript\" src=\"/orsoon/wzygg2.js\"></script></span>([\\s\\S]*)<div cl" +
                "ass=\"copy\">";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "文章规则：";
            // 
            // tblistlinkrole
            // 
            this.tblistlinkrole.Location = new System.Drawing.Point(68, 264);
            this.tblistlinkrole.Multiline = true;
            this.tblistlinkrole.Name = "tblistlinkrole";
            this.tblistlinkrole.Size = new System.Drawing.Size(220, 61);
            this.tblistlinkrole.TabIndex = 13;
            this.tblistlinkrole.Text = "<div class=\"wztitle\"><a href=\'([^\']+)\' title=\'[^\']+\' class=\"showtopic\">([^<]+)</a" +
                "></div>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "链接规则：";
            // 
            // tbCharSet
            // 
            this.tbCharSet.Location = new System.Drawing.Point(201, 105);
            this.tbCharSet.Name = "tbCharSet";
            this.tbCharSet.Size = new System.Drawing.Size(88, 21);
            this.tbCharSet.TabIndex = 11;
            this.tbCharSet.Text = "gb2312";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "编码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "列区规则：";
            // 
            // tbListPageEnd
            // 
            this.tbListPageEnd.Location = new System.Drawing.Point(115, 105);
            this.tbListPageEnd.Name = "tbListPageEnd";
            this.tbListPageEnd.Size = new System.Drawing.Size(26, 21);
            this.tbListPageEnd.TabIndex = 8;
            this.tbListPageEnd.Text = "2";
            // 
            // tbListPageStart
            // 
            this.tbListPageStart.Location = new System.Drawing.Point(70, 105);
            this.tbListPageStart.Name = "tbListPageStart";
            this.tbListPageStart.Size = new System.Drawing.Size(28, 21);
            this.tbListPageStart.TabIndex = 7;
            this.tbListPageStart.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "起 始 页：";
            // 
            // tblistrole
            // 
            this.tblistrole.Location = new System.Drawing.Point(69, 135);
            this.tblistrole.Multiline = true;
            this.tblistrole.Name = "tblistrole";
            this.tblistrole.Size = new System.Drawing.Size(220, 61);
            this.tblistrole.TabIndex = 3;
            this.tblistrole.Text = "<div class=\"wzbox\">([\\s\\S]*)<div class=\"next\">";
            // 
            // tblistpage
            // 
            this.tblistpage.Location = new System.Drawing.Point(69, 42);
            this.tblistpage.Name = "tblistpage";
            this.tblistpage.Size = new System.Drawing.Size(220, 21);
            this.tblistpage.TabIndex = 1;
            this.tblistpage.Text = "http://www.orsoon.com/Article/211/242/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地   址：";
            // 
            // btPostRole
            // 
            this.btPostRole.Location = new System.Drawing.Point(224, 517);
            this.btPostRole.Name = "btPostRole";
            this.btPostRole.Size = new System.Drawing.Size(38, 23);
            this.btPostRole.TabIndex = 20;
            this.btPostRole.Text = "提交";
            this.btPostRole.UseVisualStyleBackColor = true;
            this.btPostRole.Click += new System.EventHandler(this.btPostRole_Click);
            // 
            // btlistsheif
            // 
            this.btlistsheif.Location = new System.Drawing.Point(81, 517);
            this.btlistsheif.Name = "btlistsheif";
            this.btlistsheif.Size = new System.Drawing.Size(69, 23);
            this.btlistsheif.TabIndex = 16;
            this.btlistsheif.Text = "测试列表";
            this.btlistsheif.UseVisualStyleBackColor = true;
            this.btlistsheif.Click += new System.EventHandler(this.btlistsheif_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sr_content);
            this.groupBox1.Controls.Add(this.sr_title);
            this.groupBox1.Location = new System.Drawing.Point(670, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 535);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分析文章结果";
            // 
            // sr_content
            // 
            this.sr_content.Location = new System.Drawing.Point(16, 59);
            this.sr_content.MinimumSize = new System.Drawing.Size(20, 20);
            this.sr_content.Name = "sr_content";
            this.sr_content.Size = new System.Drawing.Size(404, 461);
            this.sr_content.TabIndex = 1;
            // 
            // sr_title
            // 
            this.sr_title.AutoSize = true;
            this.sr_title.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sr_title.Location = new System.Drawing.Point(62, 25);
            this.sr_title.Name = "sr_title";
            this.sr_title.Size = new System.Drawing.Size(63, 14);
            this.sr_title.TabIndex = 0;
            this.sr_title.Text = "label14";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 517);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "字符转换";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(269, 517);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "新增";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SheifSourceDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 560);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btDebugTopicInfo);
            this.Controls.Add(this.gbresview);
            this.Controls.Add(this.gblisttest);
            this.Controls.Add(this.btPostRole);
            this.Controls.Add(this.btlistsheif);
            this.Name = "SheifSourceDebug";
            this.Text = "SheifSourceDebug";
            this.Load += new System.EventHandler(this.SheifSourceDebug_Load);
            this.gbresview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thdrTestRes)).EndInit();
            this.gblisttest.ResumeLayout(false);
            this.gblisttest.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btDebugTopicInfo;
        private System.Windows.Forms.TextBox tbtopicdatarole;
        private System.Windows.Forms.GroupBox gbresview;
        private System.Windows.Forms.GroupBox gblisttest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tblisttopicdatarole;
        private System.Windows.Forms.TextBox tbshiefsourcname;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbtopicpagertemp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbtopicpagerOld;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbtopicinforole;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tblistlinkrole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCharSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbListPageEnd;
        private System.Windows.Forms.TextBox tbListPageStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tblistrole;
        private System.Windows.Forms.TextBox tblistpage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btPostRole;
        private System.Windows.Forms.Button btlistsheif;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView thdrTestRes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label sr_title;
        private System.Windows.Forms.WebBrowser sr_content;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}