namespace RandomPlayDance_Generator_3
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelExcelPath = new System.Windows.Forms.Label();
            this.buttonImportExcel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.intervalChoice3 = new System.Windows.Forms.RadioButton();
            this.intervalChoice2 = new System.Windows.Forms.RadioButton();
            this.intervalChoice1 = new System.Windows.Forms.RadioButton();
            this.labelIntervalFile = new System.Windows.Forms.Label();
            this.buttonImportAudio = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.orderChoice2 = new System.Windows.Forms.RadioButton();
            this.orderChoice1 = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonClearCache = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textLogBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelExcelPath);
            this.groupBox1.Controls.Add(this.buttonImportExcel);
            this.groupBox1.Location = new System.Drawing.Point(742, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "歌单文件";
            // 
            // labelExcelPath
            // 
            this.labelExcelPath.AutoSize = true;
            this.labelExcelPath.Location = new System.Drawing.Point(14, 76);
            this.labelExcelPath.Name = "labelExcelPath";
            this.labelExcelPath.Size = new System.Drawing.Size(377, 18);
            this.labelExcelPath.TabIndex = 1;
            this.labelExcelPath.Text = "未导入歌单，默认使用程序目录下的歌单.xlsx";
            this.labelExcelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonImportExcel
            // 
            this.buttonImportExcel.Location = new System.Drawing.Point(16, 27);
            this.buttonImportExcel.Name = "buttonImportExcel";
            this.buttonImportExcel.Size = new System.Drawing.Size(128, 38);
            this.buttonImportExcel.TabIndex = 0;
            this.buttonImportExcel.Text = "导入歌单";
            this.buttonImportExcel.UseVisualStyleBackColor = true;
            this.buttonImportExcel.Click += new System.EventHandler(this.buttonImportExcel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.intervalChoice3);
            this.groupBox2.Controls.Add(this.intervalChoice2);
            this.groupBox2.Controls.Add(this.intervalChoice1);
            this.groupBox2.Controls.Add(this.labelIntervalFile);
            this.groupBox2.Controls.Add(this.buttonImportAudio);
            this.groupBox2.Location = new System.Drawing.Point(742, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "音频过渡";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(124, 28);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(62, 28);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // intervalChoice3
            // 
            this.intervalChoice3.AutoSize = true;
            this.intervalChoice3.Location = new System.Drawing.Point(16, 104);
            this.intervalChoice3.Name = "intervalChoice3";
            this.intervalChoice3.Size = new System.Drawing.Size(87, 22);
            this.intervalChoice3.TabIndex = 5;
            this.intervalChoice3.Text = "自定义";
            this.intervalChoice3.UseVisualStyleBackColor = true;
            this.intervalChoice3.CheckedChanged += new System.EventHandler(this.intervalChoice3_CheckedChanged);
            // 
            // intervalChoice2
            // 
            this.intervalChoice2.AutoSize = true;
            this.intervalChoice2.Checked = true;
            this.intervalChoice2.Location = new System.Drawing.Point(16, 68);
            this.intervalChoice2.Name = "intervalChoice2";
            this.intervalChoice2.Size = new System.Drawing.Size(114, 22);
            this.intervalChoice2.TabIndex = 4;
            this.intervalChoice2.TabStop = true;
            this.intervalChoice2.Text = "5秒倒计时";
            this.intervalChoice2.UseVisualStyleBackColor = true;
            this.intervalChoice2.CheckedChanged += new System.EventHandler(this.intervalChoice2_CheckedChanged);
            // 
            // intervalChoice1
            // 
            this.intervalChoice1.AutoSize = true;
            this.intervalChoice1.Location = new System.Drawing.Point(16, 32);
            this.intervalChoice1.Name = "intervalChoice1";
            this.intervalChoice1.Size = new System.Drawing.Size(204, 22);
            this.intervalChoice1.TabIndex = 3;
            this.intervalChoice1.Text = "空白间隔         秒";
            this.intervalChoice1.UseVisualStyleBackColor = true;
            this.intervalChoice1.CheckedChanged += new System.EventHandler(this.intervalChoice1_CheckedChanged);
            // 
            // labelIntervalFile
            // 
            this.labelIntervalFile.AutoSize = true;
            this.labelIntervalFile.Location = new System.Drawing.Point(244, 108);
            this.labelIntervalFile.Name = "labelIntervalFile";
            this.labelIntervalFile.Size = new System.Drawing.Size(98, 18);
            this.labelIntervalFile.TabIndex = 1;
            this.labelIntervalFile.Text = "未选择音频";
            // 
            // buttonImportAudio
            // 
            this.buttonImportAudio.Location = new System.Drawing.Point(110, 98);
            this.buttonImportAudio.Name = "buttonImportAudio";
            this.buttonImportAudio.Size = new System.Drawing.Size(128, 38);
            this.buttonImportAudio.TabIndex = 0;
            this.buttonImportAudio.Text = "导入音频";
            this.buttonImportAudio.UseVisualStyleBackColor = true;
            this.buttonImportAudio.Click += new System.EventHandler(this.buttonImportAudio_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.orderChoice2);
            this.groupBox3.Controls.Add(this.orderChoice1);
            this.groupBox3.Location = new System.Drawing.Point(742, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 106);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "歌曲顺序";
            // 
            // orderChoice2
            // 
            this.orderChoice2.AutoSize = true;
            this.orderChoice2.Location = new System.Drawing.Point(16, 68);
            this.orderChoice2.Name = "orderChoice2";
            this.orderChoice2.Size = new System.Drawing.Size(105, 22);
            this.orderChoice2.TabIndex = 4;
            this.orderChoice2.Text = "随机打乱";
            this.orderChoice2.UseVisualStyleBackColor = true;
            this.orderChoice2.CheckedChanged += new System.EventHandler(this.orderChoice2_CheckedChanged);
            // 
            // orderChoice1
            // 
            this.orderChoice1.AutoSize = true;
            this.orderChoice1.Checked = true;
            this.orderChoice1.Location = new System.Drawing.Point(16, 32);
            this.orderChoice1.Name = "orderChoice1";
            this.orderChoice1.Size = new System.Drawing.Size(105, 22);
            this.orderChoice1.TabIndex = 3;
            this.orderChoice1.TabStop = true;
            this.orderChoice1.Text = "顺序播放";
            this.orderChoice1.UseVisualStyleBackColor = true;
            this.orderChoice1.CheckedChanged += new System.EventHandler(this.orderChoice1_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(742, 561);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(396, 38);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "生成随舞音频";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonClearCache);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(742, 434);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(396, 100);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "暂存管理";
            // 
            // buttonClearCache
            // 
            this.buttonClearCache.Location = new System.Drawing.Point(262, 16);
            this.buttonClearCache.Name = "buttonClearCache";
            this.buttonClearCache.Size = new System.Drawing.Size(128, 38);
            this.buttonClearCache.TabIndex = 6;
            this.buttonClearCache.Text = "清空暂存";
            this.buttonClearCache.UseVisualStyleBackColor = true;
            this.buttonClearCache.Click += new System.EventHandler(this.buttonClearCache_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "如遇歌曲缺失，可尝试清空暂存，重新生成";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "暂存的歌曲不会重复下载";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 562);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(717, 33);
            this.progressBar.TabIndex = 6;
            // 
            // textLogBox
            // 
            this.textLogBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textLogBox.DetectUrls = false;
            this.textLogBox.ForeColor = System.Drawing.Color.White;
            this.textLogBox.Location = new System.Drawing.Point(12, 12);
            this.textLogBox.Name = "textLogBox";
            this.textLogBox.ReadOnly = true;
            this.textLogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.textLogBox.Size = new System.Drawing.Size(716, 522);
            this.textLogBox.TabIndex = 2;
            this.textLogBox.Text = "欢迎使用随舞音频生成器\n本程序使用Excel格式的歌单文件（包含歌名，BV号，剪辑时间）生成随舞音频\n为避免可能的文件冲突，使用时建议先保存并关闭歌单文件\n注意：" +
    "当前版本仅支持使用B站视频，暂不支持其他平台来源\n-------------------------------------------------------" +
    "---------------------\n";
            this.textLogBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textLogBox_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCopy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 34);
            // 
            // ToolStripMenuItemCopy
            // 
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            this.ToolStripMenuItemCopy.Size = new System.Drawing.Size(116, 30);
            this.ToolStripMenuItemCopy.Text = "复制";
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1146, 606);
            this.Controls.Add(this.textLogBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "随舞音频生成器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelExcelPath;
        private System.Windows.Forms.Button buttonImportExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton intervalChoice2;
        private System.Windows.Forms.RadioButton intervalChoice1;
        private System.Windows.Forms.Label labelIntervalFile;
        private System.Windows.Forms.Button buttonImportAudio;
        private System.Windows.Forms.RadioButton intervalChoice3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton orderChoice2;
        private System.Windows.Forms.RadioButton orderChoice1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonClearCache;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RichTextBox textLogBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
    }
}

