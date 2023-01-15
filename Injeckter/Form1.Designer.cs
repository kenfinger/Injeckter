namespace Injeckter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxProcess = new System.Windows.Forms.TextBox();
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.checkBoxCustomProcess = new System.Windows.Forms.CheckBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.checkBoxShutdown = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "URL:";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(66, 12);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(279, 20);
            this.textBoxURL.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Process:";
            // 
            // textBoxProcess
            // 
            this.textBoxProcess.Location = new System.Drawing.Point(66, 43);
            this.textBoxProcess.Name = "textBoxProcess";
            this.textBoxProcess.Size = new System.Drawing.Size(212, 20);
            this.textBoxProcess.TabIndex = 13;
            this.textBoxProcess.Visible = false;
            this.textBoxProcess.Enter += new System.EventHandler(this.textBoxProcess_Enter);
            this.textBoxProcess.Leave += new System.EventHandler(this.textBoxProcess_Leave);
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Items.AddRange(new object[] {
            "notepad.exe",
            "calc.exe",
            "mspaint.exe"});
            this.comboBoxProcess.Location = new System.Drawing.Point(66, 43);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(212, 21);
            this.comboBoxProcess.TabIndex = 14;
            // 
            // checkBoxCustomProcess
            // 
            this.checkBoxCustomProcess.AutoSize = true;
            this.checkBoxCustomProcess.Location = new System.Drawing.Point(291, 45);
            this.checkBoxCustomProcess.Name = "checkBoxCustomProcess";
            this.checkBoxCustomProcess.Size = new System.Drawing.Size(61, 17);
            this.checkBoxCustomProcess.TabIndex = 15;
            this.checkBoxCustomProcess.Text = "Custom";
            this.checkBoxCustomProcess.UseVisualStyleBackColor = true;
            this.checkBoxCustomProcess.CheckedChanged += new System.EventHandler(this.checkBoxCustomProcess_CheckedChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(66, 92);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 16;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(270, 92);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 17;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // checkBoxShutdown
            // 
            this.checkBoxShutdown.AutoSize = true;
            this.checkBoxShutdown.Location = new System.Drawing.Point(66, 70);
            this.checkBoxShutdown.Name = "checkBoxShutdown";
            this.checkBoxShutdown.Size = new System.Drawing.Size(109, 17);
            this.checkBoxShutdown.TabIndex = 18;
            this.checkBoxShutdown.Text = "Exit After Payload";
            this.checkBoxShutdown.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 125);
            this.Controls.Add(this.checkBoxShutdown);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.checkBoxCustomProcess);
            this.Controls.Add(this.comboBoxProcess);
            this.Controls.Add(this.textBoxProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Injeckter v0.13";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxProcess;
        private System.Windows.Forms.ComboBox comboBoxProcess;
        private System.Windows.Forms.CheckBox checkBoxCustomProcess;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.CheckBox checkBoxShutdown;
    }
}

