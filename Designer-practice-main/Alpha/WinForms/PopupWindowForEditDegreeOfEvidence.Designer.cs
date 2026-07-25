namespace Alpha.WinForms
{
    partial class PopupWindowForEditDegreeOfEvidence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditDegreeOfEvidence));
            this.buttonEdit = new System.Windows.Forms.Button();
            this.checkBoxTypeOfEvidence = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDegreeOfEvidence = new System.Windows.Forms.ComboBox();
            this.labelICheckable = new System.Windows.Forms.Label();
            this.listBoxOfICheckable = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(148, 317);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 15;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // checkBoxTypeOfEvidence
            // 
            this.checkBoxTypeOfEvidence.AutoSize = true;
            this.checkBoxTypeOfEvidence.Location = new System.Drawing.Point(176, 264);
            this.checkBoxTypeOfEvidence.Name = "checkBoxTypeOfEvidence";
            this.checkBoxTypeOfEvidence.Size = new System.Drawing.Size(63, 17);
            this.checkBoxTypeOfEvidence.TabIndex = 14;
            this.checkBoxTypeOfEvidence.Text = "Positive";
            this.checkBoxTypeOfEvidence.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(173, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Type of evidence";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Degree of evidence";
            // 
            // comboBoxDegreeOfEvidence
            // 
            this.comboBoxDegreeOfEvidence.FormattingEnabled = true;
            this.comboBoxDegreeOfEvidence.Location = new System.Drawing.Point(12, 260);
            this.comboBoxDegreeOfEvidence.Name = "comboBoxDegreeOfEvidence";
            this.comboBoxDegreeOfEvidence.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDegreeOfEvidence.TabIndex = 11;
            // 
            // labelICheckable
            // 
            this.labelICheckable.AutoSize = true;
            this.labelICheckable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelICheckable.Location = new System.Drawing.Point(12, 12);
            this.labelICheckable.Name = "labelICheckable";
            this.labelICheckable.Size = new System.Drawing.Size(71, 13);
            this.labelICheckable.TabIndex = 10;
            this.labelICheckable.Text = "ICheckable";
            // 
            // listBoxOfICheckable
            // 
            this.listBoxOfICheckable.FormattingEnabled = true;
            this.listBoxOfICheckable.Location = new System.Drawing.Point(12, 28);
            this.listBoxOfICheckable.Name = "listBoxOfICheckable";
            this.listBoxOfICheckable.Size = new System.Drawing.Size(292, 212);
            this.listBoxOfICheckable.TabIndex = 9;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(229, 317);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // PopupWindowForEditDegreeOfEvidence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 358);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.checkBoxTypeOfEvidence);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDegreeOfEvidence);
            this.Controls.Add(this.labelICheckable);
            this.Controls.Add(this.listBoxOfICheckable);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForEditDegreeOfEvidence";
            this.Text = "PopupWindowForEditDegreeOfEvidence";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.CheckBox checkBoxTypeOfEvidence;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDegreeOfEvidence;
        private System.Windows.Forms.Label labelICheckable;
        private System.Windows.Forms.ListBox listBoxOfICheckable;
        private System.Windows.Forms.Button buttonClose;
    }
}