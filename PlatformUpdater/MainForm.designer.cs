namespace PlatformUpdater
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnurl = new System.Windows.Forms.Button();
            this.txturl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnsvr = new System.Windows.Forms.Button();
            this.btnlocal = new System.Windows.Forms.Button();
            this.txtsvr = new System.Windows.Forms.TextBox();
            this.txtlocal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(936, 317);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.WordWrap = false;
            this.rtbInfo.TextChanged += new System.EventHandler(this.rtbInfo_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnurl);
            this.panel1.Controls.Add(this.txturl);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnsvr);
            this.panel1.Controls.Add(this.btnlocal);
            this.panel1.Controls.Add(this.txtsvr);
            this.panel1.Controls.Add(this.txtlocal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 68);
            this.panel1.TabIndex = 1;
            // 
            // btnurl
            // 
            this.btnurl.Location = new System.Drawing.Point(304, 38);
            this.btnurl.Name = "btnurl";
            this.btnurl.Size = new System.Drawing.Size(54, 23);
            this.btnurl.TabIndex = 9;
            this.btnurl.Text = "浏览";
            this.btnurl.UseVisualStyleBackColor = true;
            this.btnurl.Click += new System.EventHandler(this.btnurl_Click);
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(83, 40);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(203, 21);
            this.txturl.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "UpdateUrl:";
            // 
            // btnsvr
            // 
            this.btnsvr.Location = new System.Drawing.Point(718, 5);
            this.btnsvr.Name = "btnsvr";
            this.btnsvr.Size = new System.Drawing.Size(51, 23);
            this.btnsvr.TabIndex = 6;
            this.btnsvr.Text = "浏览";
            this.btnsvr.UseVisualStyleBackColor = true;
            this.btnsvr.Click += new System.EventHandler(this.btnsvr_Click);
            // 
            // btnlocal
            // 
            this.btnlocal.Location = new System.Drawing.Point(304, 5);
            this.btnlocal.Name = "btnlocal";
            this.btnlocal.Size = new System.Drawing.Size(54, 23);
            this.btnlocal.TabIndex = 5;
            this.btnlocal.Text = "浏览";
            this.btnlocal.UseVisualStyleBackColor = true;
            this.btnlocal.Click += new System.EventHandler(this.btnlocal_Click);
            // 
            // txtsvr
            // 
            this.txtsvr.Location = new System.Drawing.Point(490, 3);
            this.txtsvr.Name = "txtsvr";
            this.txtsvr.Size = new System.Drawing.Size(222, 21);
            this.txtsvr.TabIndex = 4;
            // 
            // txtlocal
            // 
            this.txtlocal.Location = new System.Drawing.Point(87, 6);
            this.txtlocal.Name = "txtlocal";
            this.txtlocal.Size = new System.Drawing.Size(199, 21);
            this.txtlocal.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "更新配置文件：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "原配置文件：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(936, 317);
            this.panel2.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 385);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnsvr;
        private System.Windows.Forms.Button btnlocal;
        private System.Windows.Forms.TextBox txtsvr;
        private System.Windows.Forms.TextBox txtlocal;
        private System.Windows.Forms.Button btnurl;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Label label3;
    }
}