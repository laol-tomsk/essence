namespace Alpha.WinForms
{
    partial class PopupWindowForCheckpointDegreeOfEvidences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForCheckpointDegreeOfEvidences));
            this.buttonClose = new System.Windows.Forms.Button();
            this.baseObjectDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.baseObjectNameTextBox = new System.Windows.Forms.RichTextBox();
            this.checkpointDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.checkpointNameTextBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanelDegreeOfEvidence = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddDegreeOfEvidence = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(616, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // baseObjectDescriptionTextBox
            // 
            this.baseObjectDescriptionTextBox.Location = new System.Drawing.Point(12, 74);
            this.baseObjectDescriptionTextBox.Name = "baseObjectDescriptionTextBox";
            this.baseObjectDescriptionTextBox.ReadOnly = true;
            this.baseObjectDescriptionTextBox.Size = new System.Drawing.Size(679, 68);
            this.baseObjectDescriptionTextBox.TabIndex = 3;
            this.baseObjectDescriptionTextBox.Text = "";
            // 
            // baseObjectNameTextBox
            // 
            this.baseObjectNameTextBox.Location = new System.Drawing.Point(12, 42);
            this.baseObjectNameTextBox.Name = "baseObjectNameTextBox";
            this.baseObjectNameTextBox.ReadOnly = true;
            this.baseObjectNameTextBox.Size = new System.Drawing.Size(679, 26);
            this.baseObjectNameTextBox.TabIndex = 4;
            this.baseObjectNameTextBox.Text = "";
            // 
            // checkpointDescriptionTextBox
            // 
            this.checkpointDescriptionTextBox.Location = new System.Drawing.Point(12, 180);
            this.checkpointDescriptionTextBox.Name = "checkpointDescriptionTextBox";
            this.checkpointDescriptionTextBox.ReadOnly = true;
            this.checkpointDescriptionTextBox.Size = new System.Drawing.Size(679, 68);
            this.checkpointDescriptionTextBox.TabIndex = 5;
            this.checkpointDescriptionTextBox.Text = "";
            // 
            // checkpointNameTextBox
            // 
            this.checkpointNameTextBox.Location = new System.Drawing.Point(12, 148);
            this.checkpointNameTextBox.Name = "checkpointNameTextBox";
            this.checkpointNameTextBox.ReadOnly = true;
            this.checkpointNameTextBox.Size = new System.Drawing.Size(679, 26);
            this.checkpointNameTextBox.TabIndex = 6;
            this.checkpointNameTextBox.Text = "";
            // 
            // tableLayoutPanelDegreeOfEvidence
            // 
            this.tableLayoutPanelDegreeOfEvidence.AutoScroll = true;
            this.tableLayoutPanelDegreeOfEvidence.ColumnCount = 6;
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDegreeOfEvidence.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelDegreeOfEvidence.Location = new System.Drawing.Point(0, 254);
            this.tableLayoutPanelDegreeOfEvidence.Name = "tableLayoutPanelDegreeOfEvidence";
            this.tableLayoutPanelDegreeOfEvidence.RowCount = 2;
            this.tableLayoutPanelDegreeOfEvidence.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelDegreeOfEvidence.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelDegreeOfEvidence.Size = new System.Drawing.Size(703, 308);
            this.tableLayoutPanelDegreeOfEvidence.TabIndex = 7;
            // 
            // buttonAddDegreeOfEvidence
            // 
            this.buttonAddDegreeOfEvidence.Location = new System.Drawing.Point(12, 12);
            this.buttonAddDegreeOfEvidence.Name = "buttonAddDegreeOfEvidence";
            this.buttonAddDegreeOfEvidence.Size = new System.Drawing.Size(228, 23);
            this.buttonAddDegreeOfEvidence.TabIndex = 8;
            this.buttonAddDegreeOfEvidence.Text = "Add degree of evidence";
            this.buttonAddDegreeOfEvidence.UseVisualStyleBackColor = true;
            this.buttonAddDegreeOfEvidence.Click += new System.EventHandler(this.buttonAddDegreeOfEvidence_Click);
            // 
            // PopupWindowForCheckpointDegreeOfEvidences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(703, 562);
            this.Controls.Add(this.buttonAddDegreeOfEvidence);
            this.Controls.Add(this.tableLayoutPanelDegreeOfEvidence);
            this.Controls.Add(this.checkpointNameTextBox);
            this.Controls.Add(this.checkpointDescriptionTextBox);
            this.Controls.Add(this.baseObjectNameTextBox);
            this.Controls.Add(this.baseObjectDescriptionTextBox);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForCheckpointDegreeOfEvidences";
            this.Text = "Degrees of evidence";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.RichTextBox baseObjectDescriptionTextBox;
        private System.Windows.Forms.RichTextBox baseObjectNameTextBox;
        private System.Windows.Forms.RichTextBox checkpointDescriptionTextBox;
        private System.Windows.Forms.RichTextBox checkpointNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDegreeOfEvidence;
        private System.Windows.Forms.Button buttonAddDegreeOfEvidence;
    }
}