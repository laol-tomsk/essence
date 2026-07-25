namespace Alpha
{
    partial class PopupWindowForEditState
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForEditState));
      this.stateDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.stateNameInput = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.buttonEdit = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.stateOrderInput = new System.Windows.Forms.NumericUpDown();
      this.specialIdInput = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.timeEstimateInput = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.taskNameInput = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.stateOrderInput)).BeginInit();
      this.SuspendLayout();
      // 
      // stateDescriptionInput
      // 
      this.stateDescriptionInput.Location = new System.Drawing.Point(12, 207);
      this.stateDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.stateDescriptionInput.MaxLength = 255;
      this.stateDescriptionInput.Name = "stateDescriptionInput";
      this.stateDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.stateDescriptionInput.Size = new System.Drawing.Size(421, 146);
      this.stateDescriptionInput.TabIndex = 15;
      this.stateDescriptionInput.Text = "";
      // 
      // stateNameInput
      // 
      this.stateNameInput.Location = new System.Drawing.Point(12, 34);
      this.stateNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.stateNameInput.MaxLength = 100;
      this.stateNameInput.Name = "stateNameInput";
      this.stateNameInput.Size = new System.Drawing.Size(421, 26);
      this.stateNameInput.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 182);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 20);
      this.label2.TabIndex = 13;
      this.label2.Text = "Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 9);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 12;
      this.label1.Text = "Name";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 359);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 20);
      this.label3.TabIndex = 17;
      this.label3.Text = "Order";
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(12, 485);
      this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(112, 35);
      this.buttonEdit.TabIndex = 19;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(133, 485);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 18;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // stateOrderInput
      // 
      this.stateOrderInput.Location = new System.Drawing.Point(12, 384);
      this.stateOrderInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.stateOrderInput.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.stateOrderInput.Name = "stateOrderInput";
      this.stateOrderInput.Size = new System.Drawing.Size(423, 26);
      this.stateOrderInput.TabIndex = 20;
      // 
      // specialIdInput
      // 
      this.specialIdInput.Location = new System.Drawing.Point(12, 445);
      this.specialIdInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.specialIdInput.MaxLength = 100;
      this.specialIdInput.Name = "specialIdInput";
      this.specialIdInput.Size = new System.Drawing.Size(421, 26);
      this.specialIdInput.TabIndex = 21;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 419);
      this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(210, 20);
      this.label4.TabIndex = 22;
      this.label4.Text = "Special ID ( can be nullable )";
      // 
      // timeEstimateInput
      // 
      this.timeEstimateInput.Location = new System.Drawing.Point(12, 151);
      this.timeEstimateInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.timeEstimateInput.MaxLength = 100;
      this.timeEstimateInput.Name = "timeEstimateInput";
      this.timeEstimateInput.Size = new System.Drawing.Size(421, 26);
      this.timeEstimateInput.TabIndex = 24;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 126);
      this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(133, 20);
      this.label5.TabIndex = 23;
      this.label5.Text = "Time Estimate (h)";
      // 
      // taskNameInput
      // 
      this.taskNameInput.Location = new System.Drawing.Point(12, 93);
      this.taskNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.taskNameInput.MaxLength = 100;
      this.taskNameInput.Name = "taskNameInput";
      this.taskNameInput.Size = new System.Drawing.Size(421, 26);
      this.taskNameInput.TabIndex = 26;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(7, 68);
      this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(89, 20);
      this.label6.TabIndex = 25;
      this.label6.Text = "Task Name";
      // 
      // PopupWindowForEditState
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(447, 538);
      this.Controls.Add(this.taskNameInput);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.timeEstimateInput);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.specialIdInput);
      this.Controls.Add(this.stateOrderInput);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.stateDescriptionInput);
      this.Controls.Add(this.stateNameInput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForEditState";
      this.Text = "PopupWindowForEditState";
      ((System.ComponentModel.ISupportInitialize)(this.stateOrderInput)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox stateDescriptionInput;
        private System.Windows.Forms.TextBox stateNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.NumericUpDown stateOrderInput;
        private System.Windows.Forms.TextBox specialIdInput;
        private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox timeEstimateInput;
    private System.Windows.Forms.TextBox taskNameInput;
    private System.Windows.Forms.Label label6;
  }
}