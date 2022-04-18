namespace metube
{
    partial class FrmMetube
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetContent = new System.Windows.Forms.Button();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lsbAudio = new System.Windows.Forms.ListBox();
            this.lsbVideo = new System.Windows.Forms.ListBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.bthPath = new System.Windows.Forms.Button();
            this.folderdialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCleanLog = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetContent
            // 
            this.btnGetContent.Location = new System.Drawing.Point(884, 19);
            this.btnGetContent.Name = "btnGetContent";
            this.btnGetContent.Size = new System.Drawing.Size(112, 23);
            this.btnGetContent.TabIndex = 2;
            this.btnGetContent.Text = "获取视频内容";
            this.btnGetContent.UseVisualStyleBackColor = true;
            this.btnGetContent.Click += new System.EventHandler(this.btnGetContent_Click);
            // 
            // txtVideoPath
            // 
            this.txtVideoPath.Location = new System.Drawing.Point(77, 19);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.Size = new System.Drawing.Size(801, 23);
            this.txtVideoPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "视频地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "音频";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(527, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "视频";
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(3, 19);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(1002, 318);
            this.txtContent.TabIndex = 1;
            // 
            // lsbAudio
            // 
            this.lsbAudio.FormattingEnabled = true;
            this.lsbAudio.ItemHeight = 17;
            this.lsbAudio.Location = new System.Drawing.Point(12, 65);
            this.lsbAudio.Name = "lsbAudio";
            this.lsbAudio.Size = new System.Drawing.Size(469, 259);
            this.lsbAudio.TabIndex = 3;
            // 
            // lsbVideo
            // 
            this.lsbVideo.FormattingEnabled = true;
            this.lsbVideo.ItemHeight = 17;
            this.lsbVideo.Location = new System.Drawing.Point(527, 65);
            this.lsbVideo.Name = "lsbVideo";
            this.lsbVideo.Size = new System.Drawing.Size(469, 259);
            this.lsbVideo.TabIndex = 4;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(884, 347);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(112, 23);
            this.btnDownload.TabIndex = 6;
            this.btnDownload.Text = "下载视频";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtContent);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 389);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1008, 340);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行日志";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtFilePath);
            this.groupBox3.Location = new System.Drawing.Point(12, 328);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 55);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "下载保存路径";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilePath.Location = new System.Drawing.Point(3, 19);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(562, 23);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.Text = "c:\\";
            // 
            // bthPath
            // 
            this.bthPath.Location = new System.Drawing.Point(586, 347);
            this.bthPath.Name = "bthPath";
            this.bthPath.Size = new System.Drawing.Size(112, 23);
            this.bthPath.TabIndex = 9;
            this.bthPath.Text = "保存路径";
            this.bthPath.UseVisualStyleBackColor = true;
            this.bthPath.Click += new System.EventHandler(this.bthPath_Click);
            // 
            // btnCleanLog
            // 
            this.btnCleanLog.Location = new System.Drawing.Point(735, 360);
            this.btnCleanLog.Name = "btnCleanLog";
            this.btnCleanLog.Size = new System.Drawing.Size(112, 23);
            this.btnCleanLog.TabIndex = 9;
            this.btnCleanLog.Text = "清空日志";
            this.btnCleanLog.UseVisualStyleBackColor = true;
            this.btnCleanLog.Click += new System.EventHandler(this.btnCleanLog_Click);
            // 
            // FrmMetube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btnCleanLog);
            this.Controls.Add(this.bthPath);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lsbVideo);
            this.Controls.Add(this.lsbAudio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVideoPath);
            this.Controls.Add(this.btnGetContent);
            this.Name = "FrmMetube";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metube";
            this.Load += new System.EventHandler(this.FrmMetube_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnGetContent;
        private TextBox txtVideoPath;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtContent;
        private ListBox lsbAudio;
        private ListBox lsbVideo;
        private Button btnDownload;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button bthPath;
        private FolderBrowserDialog folderdialog;
        private TextBox txtFilePath;
        private Button btnCleanLog;
    }
}