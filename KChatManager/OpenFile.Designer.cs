namespace KChatManager
{
    partial class OpenFile
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
            this.chatFileDirectory_txt = new System.Windows.Forms.TextBox();
            this.browseToChooseChatFile_btn = new System.Windows.Forms.Button();
            this.createKChatFile_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatFileDirectory_txt
            // 
            this.chatFileDirectory_txt.BackColor = System.Drawing.SystemColors.Window;
            this.chatFileDirectory_txt.Location = new System.Drawing.Point(24, 45);
            this.chatFileDirectory_txt.Name = "chatFileDirectory_txt";
            this.chatFileDirectory_txt.ReadOnly = true;
            this.chatFileDirectory_txt.Size = new System.Drawing.Size(331, 25);
            this.chatFileDirectory_txt.TabIndex = 0;
            // 
            // browseToChooseChatFile_btn
            // 
            this.browseToChooseChatFile_btn.Location = new System.Drawing.Point(382, 42);
            this.browseToChooseChatFile_btn.Name = "browseToChooseChatFile_btn";
            this.browseToChooseChatFile_btn.Size = new System.Drawing.Size(75, 30);
            this.browseToChooseChatFile_btn.TabIndex = 1;
            this.browseToChooseChatFile_btn.Text = "Browse";
            this.browseToChooseChatFile_btn.UseVisualStyleBackColor = true;
            this.browseToChooseChatFile_btn.Click += new System.EventHandler(this.browseToChooseChatFile_Click);
            // 
            // createKChatFile_btn
            // 
            this.createKChatFile_btn.Location = new System.Drawing.Point(203, 105);
            this.createKChatFile_btn.Name = "createKChatFile_btn";
            this.createKChatFile_btn.Size = new System.Drawing.Size(75, 30);
            this.createKChatFile_btn.TabIndex = 2;
            this.createKChatFile_btn.Text = "Create";
            this.createKChatFile_btn.UseVisualStyleBackColor = true;
            this.createKChatFile_btn.Click += new System.EventHandler(this.createKChatFile_Click);
            // 
            // OpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 162);
            this.Controls.Add(this.createKChatFile_btn);
            this.Controls.Add(this.browseToChooseChatFile_btn);
            this.Controls.Add(this.chatFileDirectory_txt);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatFileDirectory_txt;
        private System.Windows.Forms.Button browseToChooseChatFile_btn;
        private System.Windows.Forms.Button createKChatFile_btn;
    }
}