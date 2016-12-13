namespace InvertedIndex
{
    partial class fmSearchEngine
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
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.lbConnectionString = new System.Windows.Forms.Label();
            this.lbQuery = new System.Windows.Forms.Label();
            this.pnConnectionString = new System.Windows.Forms.Panel();
            this.pnQuery = new System.Windows.Forms.Panel();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.lbFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblQueryForIIView = new System.Windows.Forms.Label();
            this.lblQueryForII = new System.Windows.Forms.Label();
            this.pnResponses = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnConnectionString.SuspendLayout();
            this.pnQuery.SuspendLayout();
            this.pnResponses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectionString.Location = new System.Drawing.Point(15, 30);
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Size = new System.Drawing.Size(571, 20);
            this.tbConnectionString.TabIndex = 3;
            this.tbConnectionString.Text = "Data Source=EANI;Initial Catalog=CrawlerDB;Integrated Security=True";
            // 
            // lbConnectionString
            // 
            this.lbConnectionString.AutoSize = true;
            this.lbConnectionString.Location = new System.Drawing.Point(12, 14);
            this.lbConnectionString.Name = "lbConnectionString";
            this.lbConnectionString.Size = new System.Drawing.Size(144, 13);
            this.lbConnectionString.TabIndex = 2;
            this.lbConnectionString.Text = "Строка подключения к БД:";
            // 
            // lbQuery
            // 
            this.lbQuery.AutoSize = true;
            this.lbQuery.Location = new System.Drawing.Point(10, 56);
            this.lbQuery.Name = "lbQuery";
            this.lbQuery.Size = new System.Drawing.Size(50, 13);
            this.lbQuery.TabIndex = 4;
            this.lbQuery.Text = "Запрос: ";
            // 
            // pnConnectionString
            // 
            this.pnConnectionString.Controls.Add(this.lbConnectionString);
            this.pnConnectionString.Controls.Add(this.tbConnectionString);
            this.pnConnectionString.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnConnectionString.Location = new System.Drawing.Point(0, 271);
            this.pnConnectionString.Name = "pnConnectionString";
            this.pnConnectionString.Size = new System.Drawing.Size(803, 66);
            this.pnConnectionString.TabIndex = 5;
            // 
            // pnQuery
            // 
            this.pnQuery.Controls.Add(this.btnChooseFile);
            this.pnQuery.Controls.Add(this.btnSearch);
            this.pnQuery.Controls.Add(this.tbQuery);
            this.pnQuery.Controls.Add(this.lbFileName);
            this.pnQuery.Controls.Add(this.label1);
            this.pnQuery.Controls.Add(this.lblQueryForIIView);
            this.pnQuery.Controls.Add(this.lblQueryForII);
            this.pnQuery.Controls.Add(this.lbQuery);
            this.pnQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnQuery.Location = new System.Drawing.Point(0, 0);
            this.pnQuery.Name = "pnQuery";
            this.pnQuery.Size = new System.Drawing.Size(803, 99);
            this.pnQuery.TabIndex = 6;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(266, 17);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(134, 23);
            this.btnChooseFile.TabIndex = 7;
            this.btnChooseFile.Text = "Выбрать...";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(649, 44);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(122, 36);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Поиск...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbQuery
            // 
            this.tbQuery.Enabled = false;
            this.tbQuery.Location = new System.Drawing.Point(66, 53);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(577, 20);
            this.tbQuery.TabIndex = 5;
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Location = new System.Drawing.Point(406, 22);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(10, 13);
            this.lbFileName.TabIndex = 4;
            this.lbFileName.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите файл с инвертированным индексом: ";
            // 
            // lblQueryForIIView
            // 
            this.lblQueryForIIView.AutoSize = true;
            this.lblQueryForIIView.Location = new System.Drawing.Point(107, 78);
            this.lblQueryForIIView.Name = "lblQueryForIIView";
            this.lblQueryForIIView.Size = new System.Drawing.Size(14, 13);
            this.lblQueryForIIView.TabIndex = 4;
            this.lblQueryForIIView.Text = "#";
            // 
            // lblQueryForII
            // 
            this.lblQueryForII.AutoSize = true;
            this.lblQueryForII.Location = new System.Drawing.Point(10, 78);
            this.lblQueryForII.Name = "lblQueryForII";
            this.lblQueryForII.Size = new System.Drawing.Size(87, 13);
            this.lblQueryForII.TabIndex = 4;
            this.lblQueryForII.Text = "Запрос для ИИ:";
            // 
            // pnResponses
            // 
            this.pnResponses.AutoScroll = true;
            this.pnResponses.Controls.Add(this.dgv);
            this.pnResponses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnResponses.Location = new System.Drawing.Point(42, 99);
            this.pnResponses.Name = "pnResponses";
            this.pnResponses.Size = new System.Drawing.Size(761, 172);
            this.pnResponses.TabIndex = 7;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(706, 172);
            this.dgv.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Inverted Index|*.ii|Compressed Inverted Index|*.cii";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(42, 172);
            this.panel1.TabIndex = 0;
            // 
            // fmSearchEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 337);
            this.Controls.Add(this.pnResponses);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnQuery);
            this.Controls.Add(this.pnConnectionString);
            this.MaximumSize = new System.Drawing.Size(819, 376);
            this.MinimumSize = new System.Drawing.Size(819, 376);
            this.Name = "fmSearchEngine";
            this.Text = "SearchEngine";
            this.pnConnectionString.ResumeLayout(false);
            this.pnConnectionString.PerformLayout();
            this.pnQuery.ResumeLayout(false);
            this.pnQuery.PerformLayout();
            this.pnResponses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.Label lbConnectionString;
        private System.Windows.Forms.Label lbQuery;
        private System.Windows.Forms.Panel pnConnectionString;
        private System.Windows.Forms.Panel pnQuery;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnResponses;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.Label lblQueryForIIView;
        private System.Windows.Forms.Label lblQueryForII;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
    }
}