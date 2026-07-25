namespace Alpha.WinForms
{
    partial class PopupWindowForEditActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditActivity));
            this.buttonEdit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAlphaName = new System.Windows.Forms.Label();
            this.activityDescriptionInput = new System.Windows.Forms.RichTextBox();
            this.activityNameInput = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAddOutput = new System.Windows.Forms.Button();
            this.buttonAddInput = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.tableLayoutPanelOutputCriterions = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelInputCriterions = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(119, 183);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 22;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Activity description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAlphaName
            // 
            this.labelAlphaName.AutoSize = true;
            this.labelAlphaName.Location = new System.Drawing.Point(43, 16);
            this.labelAlphaName.Name = "labelAlphaName";
            this.labelAlphaName.Size = new System.Drawing.Size(70, 13);
            this.labelAlphaName.TabIndex = 20;
            this.labelAlphaName.Text = "Activity name";
            // 
            // activityDescriptionInput
            // 
            this.activityDescriptionInput.Location = new System.Drawing.Point(119, 57);
            this.activityDescriptionInput.MaxLength = 255;
            this.activityDescriptionInput.Name = "activityDescriptionInput";
            this.activityDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.activityDescriptionInput.Size = new System.Drawing.Size(282, 105);
            this.activityDescriptionInput.TabIndex = 19;
            this.activityDescriptionInput.Text = "";
            // 
            // activityNameInput
            // 
            this.activityNameInput.Location = new System.Drawing.Point(119, 13);
            this.activityNameInput.MaxLength = 100;
            this.activityNameInput.Name = "activityNameInput";
            this.activityNameInput.Size = new System.Drawing.Size(282, 20);
            this.activityNameInput.TabIndex = 18;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(200, 183);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 23;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.buttonAddOutput);
            this.panel2.Controls.Add(this.buttonAddInput);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.labelOutput);
            this.panel2.Controls.Add(this.tableLayoutPanelOutputCriterions);
            this.panel2.Controls.Add(this.tableLayoutPanelInputCriterions);
            this.panel2.Location = new System.Drawing.Point(407, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 567);
            this.panel2.TabIndex = 30;
            // 
            // buttonAddOutput
            // 
            this.buttonAddOutput.BackColor = System.Drawing.SystemColors.Info;
            this.buttonAddOutput.Location = new System.Drawing.Point(85, 278);
            this.buttonAddOutput.Name = "buttonAddOutput";
            this.buttonAddOutput.Size = new System.Drawing.Size(48, 23);
            this.buttonAddOutput.TabIndex = 31;
            this.buttonAddOutput.Text = "Add";
            this.buttonAddOutput.UseVisualStyleBackColor = false;
            this.buttonAddOutput.Click += new System.EventHandler(this.buttonAddOutput_Click);
            // 
            // buttonAddInput
            // 
            this.buttonAddInput.BackColor = System.Drawing.SystemColors.Info;
            this.buttonAddInput.Location = new System.Drawing.Point(85, 2);
            this.buttonAddInput.Name = "buttonAddInput";
            this.buttonAddInput.Size = new System.Drawing.Size(48, 23);
            this.buttonAddInput.TabIndex = 30;
            this.buttonAddInput.Text = "Add";
            this.buttonAddInput.UseVisualStyleBackColor = false;
            this.buttonAddInput.Click += new System.EventHandler(this.buttonAddInput_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(32, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Input";
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOutput.Location = new System.Drawing.Point(32, 283);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(45, 13);
            this.labelOutput.TabIndex = 18;
            this.labelOutput.Text = "Output";
            // 
            // tableLayoutPanelOutputCriterions
            // 
            this.tableLayoutPanelOutputCriterions.AutoScroll = true;
            this.tableLayoutPanelOutputCriterions.ColumnCount = 6;
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOutputCriterions.Location = new System.Drawing.Point(32, 302);
            this.tableLayoutPanelOutputCriterions.Name = "tableLayoutPanelOutputCriterions";
            this.tableLayoutPanelOutputCriterions.RowCount = 2;
            this.tableLayoutPanelOutputCriterions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOutputCriterions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOutputCriterions.Size = new System.Drawing.Size(563, 245);
            this.tableLayoutPanelOutputCriterions.TabIndex = 17;
            // 
            // tableLayoutPanelInputCriterions
            // 
            this.tableLayoutPanelInputCriterions.AutoScroll = true;
            this.tableLayoutPanelInputCriterions.ColumnCount = 6;
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputCriterions.Location = new System.Drawing.Point(32, 26);
            this.tableLayoutPanelInputCriterions.Name = "tableLayoutPanelInputCriterions";
            this.tableLayoutPanelInputCriterions.RowCount = 2;
            this.tableLayoutPanelInputCriterions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelInputCriterions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelInputCriterions.Size = new System.Drawing.Size(563, 245);
            this.tableLayoutPanelInputCriterions.TabIndex = 16;
            // 
            // PopupWindowForEditActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 587);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAlphaName);
            this.Controls.Add(this.activityDescriptionInput);
            this.Controls.Add(this.activityNameInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForEditActivity";
            this.Text = "PopupWindowForEditActivity";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAlphaName;
        private System.Windows.Forms.RichTextBox activityDescriptionInput;
        private System.Windows.Forms.TextBox activityNameInput;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonAddOutput;
        private System.Windows.Forms.Button buttonAddInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOutputCriterions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInputCriterions;
    }
}