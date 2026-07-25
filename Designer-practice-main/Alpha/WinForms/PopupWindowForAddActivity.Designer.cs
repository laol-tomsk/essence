namespace Alpha.WinForms
{
    partial class PopupWindowForAddActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupWindowForAddActivity));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAlphaName = new System.Windows.Forms.Label();
            this.activityDescriptionInput = new System.Windows.Forms.RichTextBox();
            this.activityNameInput = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelInputCriterions = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelOutputCriterions = new System.Windows.Forms.TableLayoutPanel();
            this.labelOutput = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddInput = new System.Windows.Forms.Button();
            this.buttonAddOutput = new System.Windows.Forms.Button();
            this.labelLogger = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(116, 164);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 17;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(197, 164);
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
            this.label1.Location = new System.Drawing.Point(15, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Activity description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAlphaName
            // 
            this.labelAlphaName.AutoSize = true;
            this.labelAlphaName.Location = new System.Drawing.Point(40, 24);
            this.labelAlphaName.Name = "labelAlphaName";
            this.labelAlphaName.Size = new System.Drawing.Size(70, 13);
            this.labelAlphaName.TabIndex = 14;
            this.labelAlphaName.Text = "Activity name";
            // 
            // activityDescriptionInput
            // 
            this.activityDescriptionInput.Location = new System.Drawing.Point(116, 47);
            this.activityDescriptionInput.MaxLength = 255;
            this.activityDescriptionInput.Name = "activityDescriptionInput";
            this.activityDescriptionInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.activityDescriptionInput.Size = new System.Drawing.Size(282, 96);
            this.activityDescriptionInput.TabIndex = 13;
            this.activityDescriptionInput.Text = "";
            // 
            // activityNameInput
            // 
            this.activityNameInput.Location = new System.Drawing.Point(116, 21);
            this.activityNameInput.MaxLength = 100;
            this.activityNameInput.Name = "activityNameInput";
            this.activityNameInput.Size = new System.Drawing.Size(282, 20);
            this.activityNameInput.TabIndex = 12;
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
            this.panel2.Location = new System.Drawing.Point(404, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 567);
            this.panel2.TabIndex = 29;
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
            // labelLogger
            // 
            this.labelLogger.AutoSize = true;
            this.labelLogger.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogger.Location = new System.Drawing.Point(112, 232);
            this.labelLogger.Name = "labelLogger";
            this.labelLogger.Size = new System.Drawing.Size(110, 24);
            this.labelLogger.TabIndex = 30;
            this.labelLogger.Text = "labelLogger";
            // 
            // PopupWindowForAddActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 615);
            this.Controls.Add(this.labelLogger);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAlphaName);
            this.Controls.Add(this.activityDescriptionInput);
            this.Controls.Add(this.activityNameInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopupWindowForAddActivity";
            this.Text = "PopupWindowForAddActivity";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAlphaName;
        private System.Windows.Forms.RichTextBox activityDescriptionInput;
        private System.Windows.Forms.TextBox activityNameInput;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOutputCriterions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInputCriterions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddOutput;
        private System.Windows.Forms.Button buttonAddInput;
        private System.Windows.Forms.Label labelLogger;
    }
}