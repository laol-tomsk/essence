namespace Alpha.WinForms
{
    partial class ActivitiesTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivitiesTable));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonAddActivity = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelActivities = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(694, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(94, 39);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonAddActivity
            // 
            this.buttonAddActivity.Location = new System.Drawing.Point(12, 12);
            this.buttonAddActivity.Name = "buttonAddActivity";
            this.buttonAddActivity.Size = new System.Drawing.Size(138, 39);
            this.buttonAddActivity.TabIndex = 4;
            this.buttonAddActivity.Text = "Add Activity";
            this.buttonAddActivity.UseVisualStyleBackColor = true;
            this.buttonAddActivity.Click += new System.EventHandler(this.buttonAddActivity_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanelActivities);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 393);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanelActivities
            // 
            this.tableLayoutPanelActivities.AutoScroll = true;
            this.tableLayoutPanelActivities.ColumnCount = 3;
            this.tableLayoutPanelActivities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelActivities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelActivities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActivities.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelActivities.Name = "tableLayoutPanelActivities";
            this.tableLayoutPanelActivities.RowCount = 2;
            this.tableLayoutPanelActivities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelActivities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelActivities.Size = new System.Drawing.Size(800, 393);
            this.tableLayoutPanelActivities.TabIndex = 1;
            // 
            // ActivitiesTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonAddActivity);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivitiesTable";
            this.Text = "ActivitiesTable";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAddActivity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActivities;
    }
}