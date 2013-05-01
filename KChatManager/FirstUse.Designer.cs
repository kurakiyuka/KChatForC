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
            this.kChatFileFolderDirectory = new System.Windows.Forms.TextBox();
            this.browseKChatFileFolder = new System.Windows.Forms.Button();
            this.saveKChatFileFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kChatFileFolderDirectory
            // 
            this.kChatFileFolderDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.kChatFileFolderDirectory.Location = new System.Drawing.Point(24, 45);
            this.kChatFileFolderDirectory.Name = "kChatFileFolderDirectory";
            this.kChatFileFolderDirectory.ReadOnly = true;
            this.kChatFileFolderDirectory.Size = new System.Drawing.Size(331, 25);
            this.kChatFileFolderDirectory.TabIndex = 0;
            // 
            // browseKChatFileFolder
            // 
            this.browseKChatFileFolder.Location = new System.Drawing.Point(382, 42);
            this.browseKChatFileFolder.Name = "browseKChatFileFolder";
            this.browseKChatFileFolder.Size = new System.Drawing.Size(75, 30);
            this.browseKChatFileFolder.TabIndex = 1;
            this.browseKChatFileFolder.Text = "Browse";
            this.browseKChatFileFolder.UseVisualStyleBackColor = true;
            this.browseKChatFileFolder.Click += new System.EventHandler(this.browseKChatFileFolder_Click);
            // 
            // saveKChatFileFolder
            // 
            this.saveKChatFileFolder.Location = new System.Drawing.Point(203, 105);
            this.saveKChatFileFolder.Name = "saveKChatFileFolder";
            this.saveKChatFileFolder.Size = new System.Drawing.Size(75, 30);
            this.saveKChatFileFolder.TabIndex = 2;
            this.saveKChatFileFolder.Text = "Save";
            this.saveKChatFileFolder.UseVisualStyleBackColor = true;
            this.saveKChatFileFolder.Click += new System.EventHandler(this.saveKChatFileFolder_Click);
            // 
            // FirstUse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(494, 172);
            this.Controls.Add(this.saveKChatFileFolder);
            this.Controls.Add(this.browseKChatFileFolder);
            this.Controls.Add(this.kChatFileFolderDirectory);
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

        private System.Windows.Forms.TextBox kChatFileFolderDirectory;
        private System.Windows.Forms.Button browseKChatFileFolder;
        private System.Windows.Forms.Button saveKChatFileFolder;
    }
}