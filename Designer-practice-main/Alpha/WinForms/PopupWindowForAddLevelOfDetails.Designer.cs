namespace Alpha.WinForms
{
    partial class PopupWindowForAddLevelOfDetails
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForAddLevelOfDetails));
      this.levelOfDetailDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.levelOfDetailNameInput = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.buttonClose = new System.Windows.Forms.Button();
      this.buttonAdd = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.specialIdInput = new System.Windows.Forms.TextBox();
      this.levelOfDetailTimeEstimateInput = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.taskNameInput = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // levelOfDetailDescriptionInput
      // 
      this.levelOfDetailDescriptionInput.Location = new System.Drawing.Point(20, 202);
      this.levelOfDetailDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailDescriptionInput.MaxLength = 255;
      this.levelOfDetailDescriptionInput.Name = "levelOfDetailDescriptionInput";
      this.levelOfDetailDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.levelOfDetailDescriptionInput.Size = new System.Drawing.Size(421, 146);
      this.levelOfDetailDescriptionInput.TabIndex = 17;
      this.levelOfDetailDescriptionInput.Text = "";
      // 
      // levelOfDetailNameInput
      // 
      this.levelOfDetailNameInput.Location = new System.Drawing.Point(20, 31);
      this.levelOfDetailNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailNameInput.MaxLength = 100;
      this.levelOfDetailNameInput.Name = "levelOfDetailNameInput";
      this.levelOfDetailNameInput.Size = new System.Drawing.Size(421, 26);
      this.levelOfDetailNameInput.TabIndex = 16;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 177);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 20);
      this.label2.TabIndex = 15;
      this.label2.Text = "Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 6);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 20);
      this.label1.TabIndex = 14;
      this.label1.Text = "Name";
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(230, 429);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 13;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // buttonAdd
      // 
      this.buttonAdd.Location = new System.Drawing.Point(108, 429);
      this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new System.Drawing.Size(112, 35);
      this.buttonAdd.TabIndex = 12;
      this.buttonAdd.Text = "Add";
      this.buttonAdd.UseVisualStyleBackColor = true;
      this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 363);
      this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(210, 20);
      this.label4.TabIndex = 28;
      this.label4.Text = "Special ID ( can be nullable )";
      // 
      // specialIdInput
      // 
      this.specialIdInput.Location = new System.Drawing.Point(20, 389);
      this.specialIdInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.specialIdInput.MaxLength = 100;
      this.specialIdInput.Name = "specialIdInput";
      this.specialIdInput.Size = new System.Drawing.Size(421, 26);
      this.specialIdInput.TabIndex = 27;
      // 
      // levelOfDetailTimeEstimateInput
      // 
      this.levelOfDetailTimeEstimateInput.Location = new System.Drawing.Point(20, 146);
      this.levelOfDetailTimeEstimateInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.levelOfDetailTimeEstimateInput.MaxLength = 100;
      this.levelOfDetailTimeEstimateInput.Name = "levelOfDetailTimeEstimateInput";
      this.levelOfDetailTimeEstimateInput.Size = new System.Drawing.Size(421, 26);
      this.levelOfDetailTimeEstimateInput.TabIndex = 30;
      this.levelOfDetailTimeEstimateInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 121);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 20);
      this.label3.TabIndex = 29;
      this.label3.Text = "Time Estimate (h)";
      // 
      // taskNameInput
      // 
      this.taskNameInput.Location = new System.Drawing.Point(21, 88);
      this.taskNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.taskNameInput.MaxLength = 100;
      this.taskNameInput.Name = "taskNameInput";
      this.taskNameInput.Size = new System.Drawing.Size(421, 26);
      this.taskNameInput.TabIndex = 32;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(16, 63);
      this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 20);
      this.label5.TabIndex = 31;
      this.label5.Text = "Task Name";
      // 
      // PopupWindowForAddLevelOfDetails
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 477);
      this.Controls.Add(this.taskNameInput);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.levelOfDetailTimeEstimateInput);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.specialIdInput);
      this.Controls.Add(this.levelOfDetailDescriptionInput);
      this.Controls.Add(this.levelOfDetailNameInput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.buttonAdd);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForAddLevelOfDetails";
      this.Text = "Add Level Of Detail";
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox levelOfDetailDescriptionInput;
        private System.Windows.Forms.TextBox levelOfDetailNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox specialIdInput;
    private System.Windows.Forms.TextBox levelOfDetailTimeEstimateInput;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox taskNameInput;
    private System.Windows.Forms.Label label5;
  }
}