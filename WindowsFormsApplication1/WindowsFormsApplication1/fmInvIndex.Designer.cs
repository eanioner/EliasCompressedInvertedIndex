namespace InvertedIndex
{
    partial class fmInvIndex
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
            this.lbConnectionString = new System.Windows.Forms.Label();
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.btnBuildIndex = new System.Windows.Forms.Button();
            this.btnSaveInvertedIndexToFile = new System.Windows.Forms.Button();
            this.pnBuildAndSaveIndex = new System.Windows.Forms.Panel();
            this.btnSaveInvertedIndexToFileUsingCompression = new System.Windows.Forms.Button();
            this.tbProgressDisplay = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnBuildAndSaveIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbConnectionString
            // 
            this.lbConnectionString.AutoSize = true;
            this.lbConnectionString.Location = new System.Drawing.Point(12, 9);
            this.lbConnectionString.Name = "lbConnectionString";
            this.lbConnectionString.Size = new System.Drawing.Size(144, 13);
            this.lbConnectionString.TabIndex = 0;
            this.lbConnectionString.Text = "Строка подключения к БД:";
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectionString.Location = new System.Drawing.Point(15, 25);
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Size = new System.Drawing.Size(553, 20);
            this.tbConnectionString.TabIndex = 1;
            this.tbConnectionString.Text = "Data Source=EANI;Initial Catalog=CrawlerDB;Integrated Security=True";
            // 
            // btnBuildIndex
            // 
            this.btnBuildIndex.Location = new System.Drawing.Point(3, 3);
            this.btnBuildIndex.Name = "btnBuildIndex";
            this.btnBuildIndex.Size = new System.Drawing.Size(234, 23);
            this.btnBuildIndex.TabIndex = 3;
            this.btnBuildIndex.Text = "Построить индекс";
            this.btnBuildIndex.UseVisualStyleBackColor = true;
            this.btnBuildIndex.Click += new System.EventHandler(this.btnBuildIndex_Click);
            // 
            // btnSaveInvertedIndexToFile
            // 
            this.btnSaveInvertedIndexToFile.Enabled = false;
            this.btnSaveInvertedIndexToFile.Location = new System.Drawing.Point(3, 28);
            this.btnSaveInvertedIndexToFile.Name = "btnSaveInvertedIndexToFile";
            this.btnSaveInvertedIndexToFile.Size = new System.Drawing.Size(234, 23);
            this.btnSaveInvertedIndexToFile.TabIndex = 4;
            this.btnSaveInvertedIndexToFile.Text = "Сохранить индекс в файл";
            this.btnSaveInvertedIndexToFile.UseVisualStyleBackColor = true;
            this.btnSaveInvertedIndexToFile.Click += new System.EventHandler(this.btnSaveInvertedIndexToFile_Click);
            // 
            // pnBuildAndSaveIndex
            // 
            this.pnBuildAndSaveIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBuildAndSaveIndex.Controls.Add(this.btnBuildIndex);
            this.pnBuildAndSaveIndex.Controls.Add(this.btnSaveInvertedIndexToFileUsingCompression);
            this.pnBuildAndSaveIndex.Controls.Add(this.btnSaveInvertedIndexToFile);
            this.pnBuildAndSaveIndex.Location = new System.Drawing.Point(15, 51);
            this.pnBuildAndSaveIndex.Name = "pnBuildAndSaveIndex";
            this.pnBuildAndSaveIndex.Size = new System.Drawing.Size(243, 128);
            this.pnBuildAndSaveIndex.TabIndex = 5;
            // 
            // btnSaveInvertedIndexToFileUsingCompression
            // 
            this.btnSaveInvertedIndexToFileUsingCompression.Enabled = false;
            this.btnSaveInvertedIndexToFileUsingCompression.Location = new System.Drawing.Point(3, 55);
            this.btnSaveInvertedIndexToFileUsingCompression.Name = "btnSaveInvertedIndexToFileUsingCompression";
            this.btnSaveInvertedIndexToFileUsingCompression.Size = new System.Drawing.Size(234, 23);
            this.btnSaveInvertedIndexToFileUsingCompression.TabIndex = 4;
            this.btnSaveInvertedIndexToFileUsingCompression.Text = "Сохранить индекс в файл (сжатие)";
            this.btnSaveInvertedIndexToFileUsingCompression.UseVisualStyleBackColor = true;
            this.btnSaveInvertedIndexToFileUsingCompression.Click += new System.EventHandler(this.btnSaveInvertedIndexToFileUsingCompression_Click);
            // 
            // tbProgressDisplay
            // 
            this.tbProgressDisplay.Location = new System.Drawing.Point(264, 51);
            this.tbProgressDisplay.Multiline = true;
            this.tbProgressDisplay.Name = "tbProgressDisplay";
            this.tbProgressDisplay.ReadOnly = true;
            this.tbProgressDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProgressDisplay.Size = new System.Drawing.Size(304, 128);
            this.tbProgressDisplay.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(421, 185);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(147, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // fmInvIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 211);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbProgressDisplay);
            this.Controls.Add(this.pnBuildAndSaveIndex);
            this.Controls.Add(this.tbConnectionString);
            this.Controls.Add(this.lbConnectionString);
            this.MaximumSize = new System.Drawing.Size(596, 250);
            this.MinimumSize = new System.Drawing.Size(596, 250);
            this.Name = "fmInvIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Инвертированный индекс";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmInvIndex_FormClosing);
            this.Load += new System.EventHandler(this.fmInvIndex_Load);
            this.pnBuildAndSaveIndex.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbConnectionString;
        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.Button btnBuildIndex;
        private System.Windows.Forms.Button btnSaveInvertedIndexToFile;
        private System.Windows.Forms.Panel pnBuildAndSaveIndex;
        private System.Windows.Forms.TextBox tbProgressDisplay;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnSaveInvertedIndexToFileUsingCompression;
        private System.Windows.Forms.Button btnClose;
    }
}

