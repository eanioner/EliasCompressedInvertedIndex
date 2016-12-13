namespace InvertedIndex
{
    partial class fmMainWindow
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
            this.btnCreateInvertedIndex = new System.Windows.Forms.Button();
            this.btnSearchByIndex = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateInvertedIndex
            // 
            this.btnCreateInvertedIndex.Location = new System.Drawing.Point(12, 12);
            this.btnCreateInvertedIndex.Name = "btnCreateInvertedIndex";
            this.btnCreateInvertedIndex.Size = new System.Drawing.Size(224, 102);
            this.btnCreateInvertedIndex.TabIndex = 0;
            this.btnCreateInvertedIndex.Text = "Построить инвертированный индекс";
            this.btnCreateInvertedIndex.UseVisualStyleBackColor = true;
            this.btnCreateInvertedIndex.Click += new System.EventHandler(this.btnCreateInvertedIndex_Click);
            // 
            // btnSearchByIndex
            // 
            this.btnSearchByIndex.Location = new System.Drawing.Point(242, 12);
            this.btnSearchByIndex.Name = "btnSearchByIndex";
            this.btnSearchByIndex.Size = new System.Drawing.Size(224, 102);
            this.btnSearchByIndex.TabIndex = 0;
            this.btnSearchByIndex.Text = "Поиск по индексу";
            this.btnSearchByIndex.UseVisualStyleBackColor = true;
            this.btnSearchByIndex.Click += new System.EventHandler(this.btnSearchByIndex_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(242, 120);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(224, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // fmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 167);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearchByIndex);
            this.Controls.Add(this.btnCreateInvertedIndex);
            this.MaximumSize = new System.Drawing.Size(497, 206);
            this.MinimumSize = new System.Drawing.Size(497, 206);
            this.Name = "fmMainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateInvertedIndex;
        private System.Windows.Forms.Button btnSearchByIndex;
        private System.Windows.Forms.Button btnClose;
    }
}