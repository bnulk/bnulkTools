namespace bnulkTools
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.guassian_1_ReadOuts = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gaussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readOutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConvertData = new System.Windows.Forms.Button();
            this.TDDFT_Steep = new System.Windows.Forms.Button();
            this.TDA_Steep = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guassian_1_ReadOuts
            // 
            this.guassian_1_ReadOuts.Location = new System.Drawing.Point(231, 80);
            this.guassian_1_ReadOuts.Name = "guassian_1_ReadOuts";
            this.guassian_1_ReadOuts.Size = new System.Drawing.Size(119, 52);
            this.guassian_1_ReadOuts.TabIndex = 0;
            this.guassian_1_ReadOuts.Text = "按文件夹读能量和自由能";
            this.guassian_1_ReadOuts.UseVisualStyleBackColor = true;
            this.guassian_1_ReadOuts.Click += new System.EventHandler(this.guassian_1_ReadOuts_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1401, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gaussianToolStripMenuItem
            // 
            this.gaussianToolStripMenuItem.Name = "gaussianToolStripMenuItem";
            this.gaussianToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.gaussianToolStripMenuItem.Text = "Gaussian";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // gaussianToolStripMenuItem1
            // 
            this.gaussianToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readOutsToolStripMenuItem});
            this.gaussianToolStripMenuItem1.Name = "gaussianToolStripMenuItem1";
            this.gaussianToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.gaussianToolStripMenuItem1.Text = "Gaussian";
            this.gaussianToolStripMenuItem1.Click += new System.EventHandler(this.gaussianToolStripMenuItem1_Click);
            // 
            // readOutsToolStripMenuItem
            // 
            this.readOutsToolStripMenuItem.Name = "readOutsToolStripMenuItem";
            this.readOutsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.readOutsToolStripMenuItem.Text = "ReadOuts";
            this.readOutsToolStripMenuItem.Click += new System.EventHandler(this.readOutsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ConvertData
            // 
            this.ConvertData.Location = new System.Drawing.Point(64, 80);
            this.ConvertData.Name = "ConvertData";
            this.ConvertData.Size = new System.Drawing.Size(119, 52);
            this.ConvertData.TabIndex = 0;
            this.ConvertData.Text = "单位转换";
            this.ConvertData.UseVisualStyleBackColor = true;
            this.ConvertData.Click += new System.EventHandler(this.ConvertData_Click);
            // 
            // TDDFT_Steep
            // 
            this.TDDFT_Steep.Location = new System.Drawing.Point(414, 80);
            this.TDDFT_Steep.Name = "TDDFT_Steep";
            this.TDDFT_Steep.Size = new System.Drawing.Size(119, 52);
            this.TDDFT_Steep.TabIndex = 2;
            this.TDDFT_Steep.Text = "TDDFT优化过程电子态能量变化";
            this.TDDFT_Steep.UseVisualStyleBackColor = true;
            // 
            // TDA_Steep
            // 
            this.TDA_Steep.Location = new System.Drawing.Point(587, 80);
            this.TDA_Steep.Name = "TDA_Steep";
            this.TDA_Steep.Size = new System.Drawing.Size(119, 52);
            this.TDA_Steep.TabIndex = 2;
            this.TDA_Steep.Text = "TDA优化过程电子态能量变化";
            this.TDA_Steep.UseVisualStyleBackColor = true;
            this.TDA_Steep.Click += new System.EventHandler(this.TDA_Steep_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 702);
            this.Controls.Add(this.TDA_Steep);
            this.Controls.Add(this.TDDFT_Steep);
            this.Controls.Add(this.ConvertData);
            this.Controls.Add(this.guassian_1_ReadOuts);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button guassian_1_ReadOuts;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gaussianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readOutsToolStripMenuItem;
        private System.Windows.Forms.Button ConvertData;
        private System.Windows.Forms.Button TDDFT_Steep;
        private System.Windows.Forms.Button TDA_Steep;
    }
}

