namespace Alpha
{
    partial class PopupWindowForEditCheckpoint
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditCheckpoint));
      this.checkpointOrderInput = new System.Windows.Forms.NumericUpDown();
      this.buttonEdit = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.checkpointDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.checkpointNameInput = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.buttonAddDegreeOfEvidence = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.specialIdInput = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.comboBoxDegreeOfEvidence = new System.Windows.Forms.ComboBox();
      this.timeEstimateInput = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.taskNameInput = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.relativeWeightInput = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.checkpointOrderInput)).BeginInit();
      this.SuspendLayout();
      // 
      // checkpointOrderInput
      // 
      this.checkpointOrderInput.Location = new System.Drawing.Point(18, 464);
      this.checkpointOrderInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.checkpointOrderInput.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.checkpointOrderInput.Name = "checkpointOrderInput";
      this.checkpointOrderInput.Size = new System.Drawing.Size(540, 26);
      this.checkpointOrderInput.TabIndex = 36;
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(18, 564);
      this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(112, 35);
      this.buttonEdit.TabIndex = 35;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(446, 564);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 34;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 440);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 20);
      this.label3.TabIndex = 33;
      this.label3.Text = "Order";
      // 
      // checkpointDescriptionInput
      // 
      this.checkpointDescriptionInput.Location = new System.Drawing.Point(18, 287);
      this.checkpointDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.checkpointDescriptionInput.MaxLength = 255;
      this.checkpointDescriptionInput.Name = "checkpointDescriptionInput";
      this.checkpointDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.checkpointDescriptionInput.Size = new System.Drawing.Size(538, 146);
      this.checkpointDescriptionInput.TabIndex = 32;
      this.checkpointDescriptionInput.Text = "";
      // 
      // checkpointNameInput
      // 
      this.checkpointNameInput.Location = new System.Drawing.Point(18, 165);
      this.checkpointNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.checkpointNameInput.MaxLength = 100;
      this.checkpointNameInput.Name = "checkpointNameInput";
      this.checkpointNameInput.Size = new System.Drawing.Size(258, 26);
      this.checkpointNameInput.TabIndex = 31;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 262);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 20);
      this.label2.TabIndex = 30;
      this.label2.Text = "Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 140);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 29;
      this.label1.Text = "Name";
      // 
      // buttonAddDegreeOfEvidence
      // 
      this.buttonAddDegreeOfEvidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonAddDegreeOfEvidence.Location = new System.Drawing.Point(18, 18);
      this.buttonAddDegreeOfEvidence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonAddDegreeOfEvidence.Name = "buttonAddDegreeOfEvidence";
      this.buttonAddDegreeOfEvidence.Size = new System.Drawing.Size(272, 32);
      this.buttonAddDegreeOfEvidence.TabIndex = 37;
      this.buttonAddDegreeOfEvidence.Text = "Degrees of evidence";
      this.buttonAddDegreeOfEvidence.UseVisualStyleBackColor = true;
      this.buttonAddDegreeOfEvidence.Click += new System.EventHandler(this.buttonAddDegreeOfEvidence_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(14, 500);
      this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(210, 20);
      this.label4.TabIndex = 39;
      this.label4.Text = "Special ID ( can be nullable )";
      // 
      // specialIdInput
      // 
      this.specialIdInput.Location = new System.Drawing.Point(18, 524);
      this.specialIdInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.specialIdInput.MaxLength = 100;
      this.specialIdInput.Name = "specialIdInput";
      this.specialIdInput.Size = new System.Drawing.Size(538, 26);
      this.specialIdInput.TabIndex = 38;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label5.Location = new System.Drawing.Point(14, 78);
      this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(420, 20);
      this.label5.TabIndex = 41;
      this.label5.Text = "The degree of influence of the manager’s opinion";
      // 
      // comboBoxDegreeOfEvidence
      // 
      this.comboBoxDegreeOfEvidence.FormattingEnabled = true;
      this.comboBoxDegreeOfEvidence.Location = new System.Drawing.Point(18, 103);
      this.comboBoxDegreeOfEvidence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.comboBoxDegreeOfEvidence.Name = "comboBoxDegreeOfEvidence";
      this.comboBoxDegreeOfEvidence.Size = new System.Drawing.Size(180, 28);
      this.comboBoxDegreeOfEvidence.TabIndex = 42;
      // 
      // timeEstimateInput
      // 
      this.timeEstimateInput.Location = new System.Drawing.Point(18, 231);
      this.timeEstimateInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.timeEstimateInput.MaxLength = 100;
      this.timeEstimateInput.Name = "timeEstimateInput";
      this.timeEstimateInput.Size = new System.Drawing.Size(258, 26);
      this.timeEstimateInput.TabIndex = 44;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(14, 206);
      this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(133, 20);
      this.label6.TabIndex = 43;
      this.label6.Text = "Time Estimate (h)";
      // 
      // taskNameInput
      // 
      this.taskNameInput.Location = new System.Drawing.Point(284, 165);
      this.taskNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.taskNameInput.MaxLength = 100;
      this.taskNameInput.Name = "taskNameInput";
      this.taskNameInput.Size = new System.Drawing.Size(272, 26);
      this.taskNameInput.TabIndex = 46;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(280, 140);
      this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(89, 20);
      this.label7.TabIndex = 45;
      this.label7.Text = "Task Name";
      // 
      // relativeWeightInput
      // 
      this.relativeWeightInput.Location = new System.Drawing.Point(284, 231);
      this.relativeWeightInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.relativeWeightInput.MaxLength = 100;
      this.relativeWeightInput.Name = "relativeWeightInput";
      this.relativeWeightInput.Size = new System.Drawing.Size(272, 26);
      this.relativeWeightInput.TabIndex = 48;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(280, 206);
      this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(143, 20);
      this.label8.TabIndex = 47;
      this.label8.Text = "Relative Weight (h)";
      // 
      // PopupWindowForEditCheckpoint
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(576, 611);
      this.Controls.Add(this.relativeWeightInput);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.taskNameInput);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.timeEstimateInput);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.comboBoxDegreeOfEvidence);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.specialIdInput);
      this.Controls.Add(this.buttonAddDegreeOfEvidence);
      this.Controls.Add(this.checkpointOrderInput);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.checkpointDescriptionInput);
      this.Controls.Add(this.checkpointNameInput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForEditCheckpoint";
      this.Text = "PopupWindowForEditCheckpoint";
      ((System.ComponentModel.ISupportInitialize)(this.checkpointOrderInput)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown checkpointOrderInput;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox checkpointDescriptionInput;
        private System.Windows.Forms.TextBox checkpointNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddDegreeOfEvidence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox specialIdInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDegreeOfEvidence;
    private System.Windows.Forms.TextBox timeEstimateInput;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox taskNameInput;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox relativeWeightInput;
    private System.Windows.Forms.Label label8;
  }
}