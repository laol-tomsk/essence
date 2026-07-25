namespace Alpha
{
    partial class PopupWindowForAddState
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForAddState));
      this.buttonAdd = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.stateDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.stateNameInput = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.specialIdInput = new System.Windows.Forms.TextBox();
      this.timeEstimateInput = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.taskNameInput = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // buttonAdd
      // 
      this.buttonAdd.Location = new System.Drawing.Point(104, 430);
      this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new System.Drawing.Size(112, 35);
      this.buttonAdd.TabIndex = 0;
      this.buttonAdd.Text = "Add";
      this.buttonAdd.UseVisualStyleBackColor = true;
      this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(225, 430);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 10);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "Name";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 180);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 20);
      this.label2.TabIndex = 3;
      this.label2.Text = "Description";
      // 
      // stateDescriptionInput
      // 
      this.stateDescriptionInput.Location = new System.Drawing.Point(18, 204);
      this.stateDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.stateDescriptionInput.MaxLength = 255;
      this.stateDescriptionInput.Name = "stateDescriptionInput";
      this.stateDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.stateDescriptionInput.Size = new System.Drawing.Size(421, 146);
      this.stateDescriptionInput.TabIndex = 11;
      this.stateDescriptionInput.Text = "";
      // 
      // stateNameInput
      // 
      this.stateNameInput.Location = new System.Drawing.Point(18, 34);
      this.stateNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.stateNameInput.MaxLength = 100;
      this.stateNameInput.Name = "stateNameInput";
      this.stateNameInput.Size = new System.Drawing.Size(421, 26);
      this.stateNameInput.TabIndex = 10;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(16, 364);
      this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(210, 20);
      this.label4.TabIndex = 24;
      this.label4.Text = "Special ID ( can be nullable )";
      // 
      // specialIdInput
      // 
      this.specialIdInput.Location = new System.Drawing.Point(18, 390);
      this.specialIdInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.specialIdInput.MaxLength = 100;
      this.specialIdInput.Name = "specialIdInput";
      this.specialIdInput.Size = new System.Drawing.Size(421, 26);
      this.specialIdInput.TabIndex = 23;
      // 
      // timeEstimateInput
      // 
      this.timeEstimateInput.Location = new System.Drawing.Point(20, 148);
      this.timeEstimateInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.timeEstimateInput.MaxLength = 100;
      this.timeEstimateInput.Name = "timeEstimateInput";
      this.timeEstimateInput.Size = new System.Drawing.Size(421, 26);
      this.timeEstimateInput.TabIndex = 26;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(17, 124);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 20);
      this.label3.TabIndex = 25;
      this.label3.Text = "Time Estimate (h)";
      // 
      // taskNameInput
      // 
      this.taskNameInput.Location = new System.Drawing.Point(21, 92);
      this.taskNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.taskNameInput.MaxLength = 100;
      this.taskNameInput.Name = "taskNameInput";
      this.taskNameInput.Size = new System.Drawing.Size(421, 26);
      this.taskNameInput.TabIndex = 28;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(17, 68);
      this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 20);
      this.label5.TabIndex = 27;
      this.label5.Text = "Task Name";
      // 
      // PopupWindowForAddState
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(459, 484);
      this.Controls.Add(this.taskNameInput);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.timeEstimateInput);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.specialIdInput);
      this.Controls.Add(this.stateDescriptionInput);
      this.Controls.Add(this.stateNameInput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.buttonAdd);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForAddState";
      this.Text = "Create State";
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox stateDescriptionInput;
        private System.Windows.Forms.TextBox stateNameInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox specialIdInput;
    private System.Windows.Forms.TextBox timeEstimateInput;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox taskNameInput;
    private System.Windows.Forms.Label label5;
  }
}