namespace PlatformUpdater
{
    partial class frmUpdate
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ffdsafasdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ffdsafsdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pbUpdate = new System.Windows.Forms.ProgressBar();
            this.lblState = new System.Windows.Forms.Label();
            this.lvUpdateList = new ListView();
            this.lvFileName = new System.Windows.Forms.ColumnHeader();
            this.lvVersion = new System.Windows.Forms.ColumnHeader();
            this.lvInfo = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.fadsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 48);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.aToolStripMenuItem.Text = "a";
            // 
            // fadsToolStripMenuItem
            // 
            this.fadsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faToolStripMenuItem,
            this.ffdsafasdToolStripMenuItem,
            this.ffdsafsdToolStripMenuItem});
            this.fadsToolStripMenuItem.Name = "fadsToolStripMenuItem";
            this.fadsToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.fadsToolStripMenuItem.Text = "fads";
            // 
            // faToolStripMenuItem
            // 
            this.faToolStripMenuItem.Checked = true;
            this.faToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.faToolStripMenuItem.Name = "faToolStripMenuItem";
            this.faToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.faToolStripMenuItem.Text = "fa";
            // 
            // ffdsafasdToolStripMenuItem
            // 
            this.ffdsafasdToolStripMenuItem.Name = "ffdsafasdToolStripMenuItem";
            this.ffdsafasdToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ffdsafasdToolStripMenuItem.Text = "ffdsafasd";
            // 
            // ffdsafsdToolStripMenuItem
            // 
            this.ffdsafsdToolStripMenuItem.Name = "ffdsafsdToolStripMenuItem";
            this.ffdsafsdToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ffdsafsdToolStripMenuItem.Text = "ffdsafsd";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.pbUpdate);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.lvUpdateList);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(4, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 230);
            this.panel1.TabIndex = 7;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(4, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(495, 14);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "更新文件列表";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbUpdate
            // 
            this.pbUpdate.Location = new System.Drawing.Point(4, 185);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(499, 17);
            this.pbUpdate.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(8, 205);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(495, 20);
            this.lblState.TabIndex = 4;
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvFileName,
            this.lvVersion,
            this.lvInfo});
            this.lvUpdateList.DoubleBuffered = false;
            this.lvUpdateList.Location = new System.Drawing.Point(3, 21);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(500, 157);
            this.lvUpdateList.TabIndex = 6;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // lvFileName
            // 
            this.lvFileName.Text = "组件名";
            this.lvFileName.Width = 372;
            // 
            // lvVersion
            // 
            this.lvVersion.Text = "版本号";
            this.lvVersion.Width = 79;
            // 
            // lvInfo
            // 
            this.lvInfo.Text = "提示";
            this.lvInfo.Width = 47;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 10);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 237);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmUpdate";
            this.Text = "PMSAutoUpate";
            this.Load += new System.EventHandler(this.frmUpdate_Load);
            this.Shown += new System.EventHandler(this.frmUpdate_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ffdsafasdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ffdsafsdToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInfo;
        private ListView lvUpdateList;
        private System.Windows.Forms.ColumnHeader lvFileName;
        private System.Windows.Forms.ColumnHeader lvVersion;
        private System.Windows.Forms.ColumnHeader lvInfo;
        private System.Windows.Forms.ProgressBar pbUpdate;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

