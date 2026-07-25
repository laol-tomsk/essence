namespace Alpha.WinForms
{
    partial class PopupWindowForEditLevelOfDetail
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditLevelOfDetail));
      this.levelOfDetailOrderInput = new System.Windows.Forms.NumericUpDown();
      this.buttonEdit = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.levelOfDetailDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.levelOfDetailNameInput = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.specialIdInput = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.levelOfDetailTimeEstimateInput = new System.Windows.Forms.TextBox();
      this.taskNameInput = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.levelOfDetailOrderInput)).BeginInit();
      this.SuspendLayout();
      // 
      // levelOfDetailOrderInput
      // 
      this.levelOfDetailOrderInput.Location = new System.Drawing.Point(18, 374);
      this.levelOfDetailOrderInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailOrderInput.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.levelOfDetailOrderInput.Name = "levelOfDetailOrderInput";
      this.levelOfDetailOrderInput.Size = new System.Drawing.Size(423, 26);
      this.levelOfDetailOrderInput.TabIndex = 28;
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(18, 475);
      this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(112, 35);
      this.buttonEdit.TabIndex = 27;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(140, 475);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 26;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 349);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 20);
      this.label3.TabIndex = 25;
      this.label3.Text = "Order";
      // 
      // levelOfDetailDescriptionInput
      // 
      this.levelOfDetailDescriptionInput.Location = new System.Drawing.Point(18, 197);
      this.levelOfDetailDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailDescriptionInput.MaxLength = 255;
      this.levelOfDetailDescriptionInput.Name = "levelOfDetailDescriptionInput";
      this.levelOfDetailDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.levelOfDetailDescriptionInput.Size = new System.Drawing.Size(421, 146);
      this.levelOfDetailDescriptionInput.TabIndex = 24;
      this.levelOfDetailDescriptionInput.Text = "";
      // 
      // levelOfDetailNameInput
      // 
      this.levelOfDetailNameInput.Location = new System.Drawing.Point(18, 33);
      this.levelOfDetailNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailNameInput.MaxLength = 100;
      this.levelOfDetailNameInput.Name = "levelOfDetailNameInput";
      this.levelOfDetailNameInput.Size = new System.Drawing.Size(421, 26);
      this.levelOfDetailNameInput.TabIndex = 23;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 172);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 20);
      this.label2.TabIndex = 22;
      this.label2.Text = "Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 8);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 21;
      this.label1.Text = "Name";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(16, 409);
      this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(210, 20);
      this.label4.TabIndex = 30;
      this.label4.Text = "Special ID ( can be nullable )";
      // 
      // specialIdInput
      // 
      this.specialIdInput.Location = new System.Drawing.Point(18, 435);
      this.specialIdInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.specialIdInput.MaxLength = 100;
      this.specialIdInput.Name = "specialIdInput";
      this.specialIdInput.Size = new System.Drawing.Size(421, 26);
      this.specialIdInput.TabIndex = 29;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(14, 116);
      this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(133, 20);
      this.label5.TabIndex = 31;
      this.label5.Text = "Time Estimate (h)";
      // 
      // levelOfDetailTimeEstimateInput
      // 
      this.levelOfDetailTimeEstimateInput.Location = new System.Drawing.Point(18, 141);
      this.levelOfDetailTimeEstimateInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailTimeEstimateInput.MaxLength = 100;
      this.levelOfDetailTimeEstimateInput.Name = "levelOfDetailTimeEstimateInput";
      this.levelOfDetailTimeEstimateInput.Size = new System.Drawing.Size(421, 26);
      this.levelOfDetailTimeEstimateInput.TabIndex = 32;
      // 
      // taskNameInput
      // 
      this.taskNameInput.Location = new System.Drawing.Point(19, 88);
      this.taskNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.taskNameInput.MaxLength = 100;
      this.taskNameInput.Name = "taskNameInput";
      this.taskNameInput.Size = new System.Drawing.Size(421, 26);
      this.taskNameInput.TabIndex = 34;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(15, 63);
      this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(89, 20);
      this.label6.TabIndex = 33;
      this.label6.Text = "Task Name";
      // 
      // PopupWindowForEditLevelOfDetail
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(470, 552);
      this.Controls.Add(this.taskNameInput);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.levelOfDetailTimeEstimateInput);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.specialIdInput);
      this.Controls.Add(this.levelOfDetailOrderInput);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.levelOfDetailDescriptionInput);
      this.Controls.Add(this.levelOfDetailNameInput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForEditLevelOfDetail";
      this.Text = "PopupWindowForEditLevelOfDetail";
      ((System.ComponentModel.ISupportInitialize)(this.levelOfDetailOrderInput)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown levelOfDetailOrderInput;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox levelOfDetailDescriptionInput;
        private System.Windows.Forms.TextBox levelOfDetailNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox specialIdInput;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox levelOfDetailTimeEstimateInput;
    private System.Windows.Forms.TextBox taskNameInput;
    private System.Windows.Forms.Label label6;
  }
}