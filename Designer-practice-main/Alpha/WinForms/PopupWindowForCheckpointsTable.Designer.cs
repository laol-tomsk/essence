namespace Alpha
{
    partial class PopupWindowForCheckpointsTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForCheckpointsTable));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelOfCheckpoints = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddCheckpoint = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanelOfCheckpoints);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 398);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanelOfCheckpoints
            // 
            this.tableLayoutPanelOfCheckpoints.AutoScroll = true;
            this.tableLayoutPanelOfCheckpoints.ColumnCount = 5;
            this.tableLayoutPanelOfCheckpoints.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfCheckpoints.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfCheckpoints.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfCheckpoints.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfCheckpoints.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfCheckpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOfCheckpoints.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelOfCheckpoints.Name = "tableLayoutPanelOfCheckpoints";
            this.tableLayoutPanelOfCheckpoints.RowCount = 2;
            this.tableLayoutPanelOfCheckpoints.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOfCheckpoints.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOfCheckpoints.Size = new System.Drawing.Size(856, 398);
            this.tableLayoutPanelOfCheckpoints.TabIndex = 0;
            // 
            // buttonAddCheckpoint
            // 
            this.buttonAddCheckpoint.Location = new System.Drawing.Point(12, 23);
            this.buttonAddCheckpoint.Name = "buttonAddCheckpoint";
            this.buttonAddCheckpoint.Size = new System.Drawing.Size(75, 23);
            this.buttonAddCheckpoint.TabIndex = 1;
            this.buttonAddCheckpoint.Text = "Add";
            this.buttonAddCheckpoint.UseVisualStyleBackColor = true;
            this.buttonAddCheckpoint.Click += new System.EventHandler(this.buttonAddCheckpoint_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(769, 23);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // PopupWindowForCheckpointsTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 450);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonAddCheckpoint);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForCheckpointsTable";
            this.Text = "PopupWindowForCheckpointsTable";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddCheckpoint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOfCheckpoints;
        private System.Windows.Forms.Button buttonClose;
    }
}