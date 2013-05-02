namespace KChatManager
{
    partial class FirstUse
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
            this.kChatFileFolder_txt = new System.Windows.Forms.TextBox();
            this.browseKChatFileFolder_btn = new System.Windows.Forms.Button();
            this.saveKChatFileFolder_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kChatFileFolder_txt
            // 
            this.kChatFileFolder_txt.BackColor = System.Drawing.SystemColors.Window;
            this.kChatFileFolder_txt.Location = new System.Drawing.Point(24, 45);
            this.kChatFileFolder_txt.Name = "kChatFileFolder_txt";
            this.kChatFileFolder_txt.ReadOnly = true;
            this.kChatFileFolder_txt.Size = new System.Drawing.Size(331, 25);
            this.kChatFileFolder_txt.TabIndex = 0;
            // 
            // browseKChatFileFolder_btn
            // 
            this.browseKChatFileFolder_btn.Location = new System.Drawing.Point(382, 42);
            this.browseKChatFileFolder_btn.Name = "browseKChatFileFolder_btn";
            this.browseKChatFileFolder_btn.Size = new System.Drawing.Size(75, 30);
            this.browseKChatFileFolder_btn.TabIndex = 1;
            this.browseKChatFileFolder_btn.Text = "Browse";
            this.browseKChatFileFolder_btn.UseVisualStyleBackColor = true;
            this.browseKChatFileFolder_btn.Click += new System.EventHandler(this.browseKChatFileFolder_Click);
            // 
            // saveKChatFileFolder_btn
            // 
            this.saveKChatFileFolder_btn.Location = new System.Drawing.Point(203, 105);
            this.saveKChatFileFolder_btn.Name = "saveKChatFileFolder_btn";
            this.saveKChatFileFolder_btn.Size = new System.Drawing.Size(75, 30);
            this.saveKChatFileFolder_btn.TabIndex = 2;
            this.saveKChatFileFolder_btn.Text = "Save";
            this.saveKChatFileFolder_btn.UseVisualStyleBackColor = true;
            this.saveKChatFileFolder_btn.Click += new System.EventHandler(this.saveKChatFileFolder_Click);
            // 
            // FirstUse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(494, 172);
            this.Controls.Add(this.saveKChatFileFolder_btn);
            this.Controls.Add(this.browseKChatFileFolder_btn);
            this.Controls.Add(this.kChatFileFolder_txt);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirstUse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set KChat File Folder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox kChatFileFolder_txt;
        private System.Windows.Forms.Button browseKChatFileFolder_btn;
        private System.Windows.Forms.Button saveKChatFileFolder_btn;
    }
}