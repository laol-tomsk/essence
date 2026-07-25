namespace Alpha.WinForms
{
    partial class WorkProductsTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkProductsTable));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelWP = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddWP = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanelWP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 393);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanelWP
            // 
            this.tableLayoutPanelWP.AutoScroll = true;
            this.tableLayoutPanelWP.ColumnCount = 3;
            this.tableLayoutPanelWP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelWP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelWP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelWP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelWP.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelWP.Name = "tableLayoutPanelWP";
            this.tableLayoutPanelWP.RowCount = 2;
            this.tableLayoutPanelWP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelWP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelWP.Size = new System.Drawing.Size(800, 393);
            this.tableLayoutPanelWP.TabIndex = 1;
            // 
            // buttonAddWP
            // 
            this.buttonAddWP.Location = new System.Drawing.Point(12, 12);
            this.buttonAddWP.Name = "buttonAddWP";
            this.buttonAddWP.Size = new System.Drawing.Size(138, 39);
            this.buttonAddWP.TabIndex = 1;
            this.buttonAddWP.Text = "Add Work Product";
            this.buttonAddWP.UseVisualStyleBackColor = true;
            this.buttonAddWP.Click += new System.EventHandler(this.buttonAddWorkProduct_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(694, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WorkProductsTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddWP);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WorkProductsTable";
            this.Text = "Work Products Table";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddWP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWP;
    }
}