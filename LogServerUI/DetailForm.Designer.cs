namespace LogServerUI
{
    partial class DetailForm
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
            this.Log_GridView = new System.Windows.Forms.DataGridView();
            this.Message_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Log_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Log_GridView
            // 
            this.Log_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Log_GridView.Location = new System.Drawing.Point(12, 12);
            this.Log_GridView.Name = "Log_GridView";
            this.Log_GridView.ReadOnly = true;
            this.Log_GridView.RowHeadersVisible = false;
            this.Log_GridView.Size = new System.Drawing.Size(400, 194);
            this.Log_GridView.TabIndex = 0;
            this.Log_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Log_GridView_CellClick);
            this.Log_GridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Log_GridView_RowPrePaint);
            // 
            // Message_textBox
            // 
            this.Message_textBox.Location = new System.Drawing.Point(12, 238);
            this.Message_textBox.Multiline = true;
            this.Message_textBox.Name = "Message_textBox";
            this.Message_textBox.Size = new System.Drawing.Size(400, 150);
            this.Message_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Log Message:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 400);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Message_textBox);
            this.Controls.Add(this.Log_GridView);
            this.Name = "DetailForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DetailForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Log_GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Log_GridView;
        private System.Windows.Forms.TextBox Message_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}