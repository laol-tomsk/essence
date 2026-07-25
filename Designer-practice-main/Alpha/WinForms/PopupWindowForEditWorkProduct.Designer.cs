namespace Alpha.WinForms
{
    partial class PopupWindowForEditWorkProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditWorkProduct));
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAlphaName = new System.Windows.Forms.Label();
            this.workProductDescriptionInput = new System.Windows.Forms.RichTextBox();
            this.workProductNameInput = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelOfLevelOfDetails = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddlevelOfDetail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(128, 185);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 17;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(976, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 26);
            this.label1.TabIndex = 15;
            this.label1.Text = "Work Product \r\ndescription";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAlphaName
            // 
            this.labelAlphaName.AutoSize = true;
            this.labelAlphaName.Location = new System.Drawing.Point(20, 41);
            this.labelAlphaName.Name = "labelAlphaName";
            this.labelAlphaName.Size = new System.Drawing.Size(102, 13);
            this.labelAlphaName.TabIndex = 14;
            this.labelAlphaName.Text = "Work Product name";
            // 
            // workProductDescriptionInput
            // 
            this.workProductDescriptionInput.Location = new System.Drawing.Point(128, 83);
            this.workProductDescriptionInput.MaxLength = 255;
            this.workProductDescriptionInput.Name = "workProductDescriptionInput";
            this.workProductDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.workProductDescriptionInput.Size = new System.Drawing.Size(282, 96);
            this.workProductDescriptionInput.TabIndex = 13;
            this.workProductDescriptionInput.Text = "";
            // 
            // workProductNameInput
            // 
            this.workProductNameInput.Location = new System.Drawing.Point(128, 41);
            this.workProductNameInput.MaxLength = 100;
            this.workProductNameInput.Name = "workProductNameInput";
            this.workProductNameInput.Size = new System.Drawing.Size(282, 20);
            this.workProductNameInput.TabIndex = 12;
            // 
            // tableLayoutPanelOfLevelOfDetails
            // 
            this.tableLayoutPanelOfLevelOfDetails.AutoScroll = true;
            this.tableLayoutPanelOfLevelOfDetails.ColumnCount = 6;
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOfLevelOfDetails.Location = new System.Drawing.Point(439, 41);
            this.tableLayoutPanelOfLevelOfDetails.Name = "tableLayoutPanelOfLevelOfDetails";
            this.tableLayoutPanelOfLevelOfDetails.RowCount = 2;
            this.tableLayoutPanelOfLevelOfDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOfLevelOfDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelOfLevelOfDetails.Size = new System.Drawing.Size(612, 266);
            this.tableLayoutPanelOfLevelOfDetails.TabIndex = 18;
            // 
            // buttonAddlevelOfDetail
            // 
            this.buttonAddlevelOfDetail.Location = new System.Drawing.Point(439, 8);
            this.buttonAddlevelOfDetail.Name = "buttonAddlevelOfDetail";
            this.buttonAddlevelOfDetail.Size = new System.Drawing.Size(115, 23);
            this.buttonAddlevelOfDetail.TabIndex = 19;
            this.buttonAddlevelOfDetail.Text = "Add Level Of Detail";
            this.buttonAddlevelOfDetail.UseVisualStyleBackColor = true;
            this.buttonAddlevelOfDetail.Click += new System.EventHandler(this.buttonAddlevelOfDetail_Click);
            // 
            // PopupWindowForEditWorkProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 348);
            this.Controls.Add(this.buttonAddlevelOfDetail);
            this.Controls.Add(this.tableLayoutPanelOfLevelOfDetails);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAlphaName);
            this.Controls.Add(this.workProductDescriptionInput);
            this.Controls.Add(this.workProductNameInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForEditWorkProduct";
            this.Text = "Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAlphaName;
        private System.Windows.Forms.RichTextBox workProductDescriptionInput;
        private System.Windows.Forms.TextBox workProductNameInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOfLevelOfDetails;
        private System.Windows.Forms.Button buttonAddlevelOfDetail;
    }
}