namespace Alpha.WinForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonOpenAlphasTable = new System.Windows.Forms.Button();
            this.buttonOpenWPTable = new System.Windows.Forms.Button();
            this.buttonOpenActivitiesTable = new System.Windows.Forms.Button();
            this.Import = new System.Windows.Forms.Button();
            this.export = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenAlphasTable
            // 
            this.buttonOpenAlphasTable.Location = new System.Drawing.Point(12, 19);
            this.buttonOpenAlphasTable.Name = "buttonOpenAlphasTable";
            this.buttonOpenAlphasTable.Size = new System.Drawing.Size(146, 44);
            this.buttonOpenAlphasTable.TabIndex = 0;
            this.buttonOpenAlphasTable.Text = "Open Alphas Table";
            this.buttonOpenAlphasTable.UseVisualStyleBackColor = true;
            this.buttonOpenAlphasTable.Click += new System.EventHandler(this.buttonOpenAlphasTable_Click);
            // 
            // buttonOpenWPTable
            // 
            this.buttonOpenWPTable.Location = new System.Drawing.Point(12, 69);
            this.buttonOpenWPTable.Name = "buttonOpenWPTable";
            this.buttonOpenWPTable.Size = new System.Drawing.Size(146, 44);
            this.buttonOpenWPTable.TabIndex = 2;
            this.buttonOpenWPTable.Text = "Open Work Products Table";
            this.buttonOpenWPTable.UseVisualStyleBackColor = true;
            this.buttonOpenWPTable.Click += new System.EventHandler(this.buttonOpenWPTable_Click);
            // 
            // buttonOpenActivitiesTable
            // 
            this.buttonOpenActivitiesTable.Location = new System.Drawing.Point(12, 119);
            this.buttonOpenActivitiesTable.Name = "buttonOpenActivitiesTable";
            this.buttonOpenActivitiesTable.Size = new System.Drawing.Size(146, 44);
            this.buttonOpenActivitiesTable.TabIndex = 3;
            this.buttonOpenActivitiesTable.Text = "Open Activities Table";
            this.buttonOpenActivitiesTable.UseVisualStyleBackColor = true;
            this.buttonOpenActivitiesTable.Click += new System.EventHandler(this.buttonOpenActivitiesTable_Click);
            // 
            // Import
            // 
            this.Import.Location = new System.Drawing.Point(186, 69);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(56, 44);
            this.Import.TabIndex = 4;
            this.Import.Text = "Import";
            this.Import.UseVisualStyleBackColor = true;
            this.Import.Click += new System.EventHandler(this.import_Click);
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(186, 119);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(56, 44);
            this.export.TabIndex = 5;
            this.export.Text = "export";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 181);
            this.Controls.Add(this.export);
            this.Controls.Add(this.Import);
            this.Controls.Add(this.buttonOpenActivitiesTable);
            this.Controls.Add(this.buttonOpenWPTable);
            this.Controls.Add(this.buttonOpenAlphasTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Main menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenAlphasTable;
        private System.Windows.Forms.Button buttonOpenWPTable;
        private System.Windows.Forms.Button buttonOpenActivitiesTable;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.Button export;
    }
}