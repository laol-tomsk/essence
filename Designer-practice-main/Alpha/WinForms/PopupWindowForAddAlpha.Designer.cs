namespace Alpha
{
    partial class PopupWindowForAddAlpha
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForAddAlpha));
      this.buttonClose = new System.Windows.Forms.Button();
      this.buttonAdd = new System.Windows.Forms.Button();
      this.alphaNameInput = new System.Windows.Forms.TextBox();
      this.alphaDescriptionInput = new System.Windows.Forms.RichTextBox();
      this.labelAlphaName = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.listBoxAlphas = new System.Windows.Forms.ListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.checkBoxChildAlpha = new System.Windows.Forms.CheckBox();
      this.checkBoxKeyAlpha = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(290, 462);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(112, 35);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // buttonAdd
      // 
      this.buttonAdd.Location = new System.Drawing.Point(168, 462);
      this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new System.Drawing.Size(112, 35);
      this.buttonAdd.TabIndex = 1;
      this.buttonAdd.Text = "Add";
      this.buttonAdd.UseVisualStyleBackColor = true;
      this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
      // 
      // alphaNameInput
      // 
      this.alphaNameInput.Location = new System.Drawing.Point(159, 60);
      this.alphaNameInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.alphaNameInput.MaxLength = 100;
      this.alphaNameInput.Name = "alphaNameInput";
      this.alphaNameInput.Size = new System.Drawing.Size(421, 26);
      this.alphaNameInput.TabIndex = 2;
      // 
      // alphaDescriptionInput
      // 
      this.alphaDescriptionInput.Location = new System.Drawing.Point(159, 100);
      this.alphaDescriptionInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.alphaDescriptionInput.MaxLength = 255;
      this.alphaDescriptionInput.Name = "alphaDescriptionInput";
      this.alphaDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
      this.alphaDescriptionInput.Size = new System.Drawing.Size(421, 146);
      this.alphaDescriptionInput.TabIndex = 3;
      this.alphaDescriptionInput.Text = "";
      // 
      // labelAlphaName
      // 
      this.labelAlphaName.AutoSize = true;
      this.labelAlphaName.Location = new System.Drawing.Point(18, 60);
      this.labelAlphaName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.labelAlphaName.Name = "labelAlphaName";
      this.labelAlphaName.Size = new System.Drawing.Size(94, 20);
      this.labelAlphaName.TabIndex = 4;
      this.labelAlphaName.Text = "Alpha name";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 152);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(131, 20);
      this.label1.TabIndex = 5;
      this.label1.Text = "Alpha description";
      // 
      // listBoxAlphas
      // 
      this.listBoxAlphas.Enabled = false;
      this.listBoxAlphas.FormattingEnabled = true;
      this.listBoxAlphas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.listBoxAlphas.ItemHeight = 20;
      this.listBoxAlphas.Location = new System.Drawing.Point(159, 306);
      this.listBoxAlphas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.listBoxAlphas.Name = "listBoxAlphas";
      this.listBoxAlphas.Size = new System.Drawing.Size(421, 144);
      this.listBoxAlphas.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(18, 365);
      this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 20);
      this.label2.TabIndex = 7;
      this.label2.Text = "Alpha parent";
      // 
      // checkBoxChildAlpha
      // 
      this.checkBoxChildAlpha.AutoSize = true;
      this.checkBoxChildAlpha.Location = new System.Drawing.Point(159, 258);
      this.checkBoxChildAlpha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.checkBoxChildAlpha.Name = "checkBoxChildAlpha";
      this.checkBoxChildAlpha.Size = new System.Drawing.Size(176, 24);
      this.checkBoxChildAlpha.TabIndex = 8;
      this.checkBoxChildAlpha.Text = "Is the alpha a child?";
      this.checkBoxChildAlpha.UseVisualStyleBackColor = true;
      this.checkBoxChildAlpha.CheckedChanged += new System.EventHandler(this.checkBoxChildAlpha_CheckedChanged);
      // 
      // checkBoxKeyAlpha
      // 
      this.checkBoxKeyAlpha.AutoSize = true;
      this.checkBoxKeyAlpha.Location = new System.Drawing.Point(343, 256);
      this.checkBoxKeyAlpha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.checkBoxKeyAlpha.Name = "checkBoxKeyAlpha";
      this.checkBoxKeyAlpha.Size = new System.Drawing.Size(155, 24);
      this.checkBoxKeyAlpha.TabIndex = 9;
      this.checkBoxKeyAlpha.Text = "Is it a Key alpha?";
      this.checkBoxKeyAlpha.UseVisualStyleBackColor = true;
      this.checkBoxKeyAlpha.Visible = checkBoxChildAlpha.Checked;
      // 
      // PopupWindowForAddAlpha
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(606, 515);
      this.Controls.Add(this.checkBoxKeyAlpha);
      this.Controls.Add(this.checkBoxChildAlpha);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.listBoxAlphas);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.labelAlphaName);
      this.Controls.Add(this.alphaDescriptionInput);
      this.Controls.Add(this.alphaNameInput);
      this.Controls.Add(this.buttonAdd);
      this.Controls.Add(this.buttonClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "PopupWindowForAddAlpha";
      this.Text = "Add a new alpha";
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox alphaNameInput;
        private System.Windows.Forms.RichTextBox alphaDescriptionInput;
        private System.Windows.Forms.Label labelAlphaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxAlphas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxChildAlpha;
    private System.Windows.Forms.CheckBox checkBoxKeyAlpha;
  }
}