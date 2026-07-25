namespace Alpha.WinForms
{
    partial class PopupWindowForEditCriterion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditCriterion));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMinimal = new System.Windows.Forms.NumericUpDown();
            this.checkBoxPartial = new System.Windows.Forms.CheckBox();
            this.labelDetail = new System.Windows.Forms.Label();
            this.listBoxDetails = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimal)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(93, 198);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 21;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(12, 198);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 20;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Minimal";
            // 
            // numericUpDownMinimal
            // 
            this.numericUpDownMinimal.Location = new System.Drawing.Point(12, 48);
            this.numericUpDownMinimal.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMinimal.Name = "numericUpDownMinimal";
            this.numericUpDownMinimal.Size = new System.Drawing.Size(282, 20);
            this.numericUpDownMinimal.TabIndex = 18;
            // 
            // checkBoxPartial
            // 
            this.checkBoxPartial.AutoSize = true;
            this.checkBoxPartial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxPartial.Location = new System.Drawing.Point(12, 12);
            this.checkBoxPartial.Name = "checkBoxPartial";
            this.checkBoxPartial.Size = new System.Drawing.Size(55, 17);
            this.checkBoxPartial.TabIndex = 17;
            this.checkBoxPartial.Text = "Partial";
            this.checkBoxPartial.UseVisualStyleBackColor = true;
            // 
            // labelDetail
            // 
            this.labelDetail.AutoSize = true;
            this.labelDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDetail.Location = new System.Drawing.Point(9, 71);
            this.labelDetail.Name = "labelDetail";
            this.labelDetail.Size = new System.Drawing.Size(139, 13);
            this.labelDetail.TabIndex = 16;
            this.labelDetail.Text = "States and Levels of Details";
            // 
            // listBoxDetails
            // 
            this.listBoxDetails.FormattingEnabled = true;
            this.listBoxDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listBoxDetails.Location = new System.Drawing.Point(12, 87);
            this.listBoxDetails.Name = "listBoxDetails";
            this.listBoxDetails.Size = new System.Drawing.Size(282, 95);
            this.listBoxDetails.TabIndex = 15;
            // 
            // PopupWindowForEditCriterion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 239);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMinimal);
            this.Controls.Add(this.checkBoxPartial);
            this.Controls.Add(this.labelDetail);
            this.Controls.Add(this.listBoxDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForEditCriterion";
            this.Text = "PopupWindowForEditCriterion";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimal;
        private System.Windows.Forms.CheckBox checkBoxPartial;
        private System.Windows.Forms.Label labelDetail;
        private System.Windows.Forms.ListBox listBoxDetails;
    }
}