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
            this.chatFileDirectory = new System.Windows.Forms.TextBox();
            this.browseToChooseChatFile = new System.Windows.Forms.Button();
            this.createKChatFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatFileDirectory
            // 
            this.chatFileDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.chatFileDirectory.Location = new System.Drawing.Point(24, 45);
            this.chatFileDirectory.Name = "chatFileDirectory";
            this.chatFileDirectory.ReadOnly = true;
            this.chatFileDirectory.Size = new System.Drawing.Size(331, 25);
            this.chatFileDirectory.TabIndex = 0;
            // 
            // browseToChooseChatFile
            // 
            this.browseToChooseChatFile.Location = new System.Drawing.Point(382, 42);
            this.browseToChooseChatFile.Name = "browseToChooseChatFile";
            this.browseToChooseChatFile.Size = new System.Drawing.Size(75, 30);
            this.browseToChooseChatFile.TabIndex = 1;
            this.browseToChooseChatFile.Text = "Browse";
            this.browseToChooseChatFile.UseVisualStyleBackColor = true;
            this.browseToChooseChatFile.Click += new System.EventHandler(this.browseToChooseChatFile_Click);
            // 
            // createKChatFile
            // 
            this.createKChatFile.Location = new System.Drawing.Point(203, 105);
            this.createKChatFile.Name = "createKChatFile";
            this.createKChatFile.Size = new System.Drawing.Size(75, 30);
            this.createKChatFile.TabIndex = 2;
            this.createKChatFile.Text = "Create";
            this.createKChatFile.UseVisualStyleBackColor = true;
            this.createKChatFile.Click += new System.EventHandler(this.createKChatFile_Click);
            // 
            // OpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 162);
            this.Controls.Add(this.createKChatFile);
            this.Controls.Add(this.browseToChooseChatFile);
            this.Controls.Add(this.chatFileDirectory);
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

        private System.Windows.Forms.TextBox chatFileDirectory;
        private System.Windows.Forms.Button browseToChooseChatFile;
        private System.Windows.Forms.Button createKChatFile;
    }
}