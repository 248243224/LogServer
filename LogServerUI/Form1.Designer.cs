namespace LogServerUI
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
            this.Start_button = new System.Windows.Forms.Button();
            this.Stop_button = new System.Windows.Forms.Button();
            this.IPAddress_textBox = new System.Windows.Forms.TextBox();
            this.Port_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Status_label = new System.Windows.Forms.Label();
            this.USB_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Start_button
            // 
            this.Start_button.Location = new System.Drawing.Point(204, 93);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(75, 23);
            this.Start_button.TabIndex = 0;
            this.Start_button.Text = "Start";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // Stop_button
            // 
            this.Stop_button.Enabled = false;
            this.Stop_button.Location = new System.Drawing.Point(204, 122);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(75, 23);
            this.Stop_button.TabIndex = 1;
            this.Stop_button.Text = "Stop";
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // IPAddress_textBox
            // 
            this.IPAddress_textBox.Location = new System.Drawing.Point(74, 19);
            this.IPAddress_textBox.Name = "IPAddress_textBox";
            this.IPAddress_textBox.Size = new System.Drawing.Size(97, 20);
            this.IPAddress_textBox.TabIndex = 2;
            this.IPAddress_textBox.Text = "10.229.19.16";
            // 
            // Port_textBox
            // 
            this.Port_textBox.Location = new System.Drawing.Point(74, 45);
            this.Port_textBox.Name = "Port_textBox";
            this.Port_textBox.Size = new System.Drawing.Size(97, 20);
            this.Port_textBox.TabIndex = 3;
            this.Port_textBox.Text = "15556";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status:";
            // 
            // Status_label
            // 
            this.Status_label.AutoSize = true;
            this.Status_label.ForeColor = System.Drawing.Color.Red;
            this.Status_label.Location = new System.Drawing.Point(71, 127);
            this.Status_label.Name = "Status_label";
            this.Status_label.Size = new System.Drawing.Size(29, 13);
            this.Status_label.TabIndex = 7;
            this.Status_label.Text = "Stop";
            // 
            // USB_checkBox
            // 
            this.USB_checkBox.AutoSize = true;
            this.USB_checkBox.Location = new System.Drawing.Point(191, 21);
            this.USB_checkBox.Name = "USB_checkBox";
            this.USB_checkBox.Size = new System.Drawing.Size(88, 17);
            this.USB_checkBox.TabIndex = 8;
            this.USB_checkBox.Text = "USB Support";
            this.USB_checkBox.UseVisualStyleBackColor = true;
            this.USB_checkBox.CheckedChanged += new System.EventHandler(this.USB_checkBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 166);
            this.Controls.Add(this.USB_checkBox);
            this.Controls.Add(this.Status_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Port_textBox);
            this.Controls.Add(this.IPAddress_textBox);
            this.Controls.Add(this.Stop_button);
            this.Controls.Add(this.Start_button);
            this.Name = "Form1";
            this.Text = "LogServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_button;
        private System.Windows.Forms.Button Stop_button;
        private System.Windows.Forms.TextBox IPAddress_textBox;
        private System.Windows.Forms.TextBox Port_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Status_label;
        private System.Windows.Forms.CheckBox USB_checkBox;
    }
}

