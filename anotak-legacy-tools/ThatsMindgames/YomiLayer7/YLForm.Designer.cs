namespace YomiLayer7
{
    partial class YLForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.animBox = new System.Windows.Forms.ListBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.extendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(94, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 4;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // animBox
            // 
            this.animBox.FormattingEnabled = true;
            this.animBox.ItemHeight = 16;
            this.animBox.Location = new System.Drawing.Point(12, 42);
            this.animBox.Name = "animBox";
            this.animBox.Size = new System.Drawing.Size(205, 308);
            this.animBox.TabIndex = 6;
            this.animBox.SelectedIndexChanged += new System.EventHandler(this.animBox_SelectedIndexChanged);
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(224, 42);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(128, 23);
            this.exportButton.TabIndex = 7;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(224, 71);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(128, 23);
            this.importButton.TabIndex = 8;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(358, 99);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(128, 22);
            this.textBox1.TabIndex = 9;
            // 
            // renameButton
            // 
            this.renameButton.Enabled = false;
            this.renameButton.Location = new System.Drawing.Point(224, 99);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(128, 23);
            this.renameButton.TabIndex = 10;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(224, 157);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(128, 23);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // extendButton
            // 
            this.extendButton.Enabled = false;
            this.extendButton.Location = new System.Drawing.Point(224, 128);
            this.extendButton.Name = "extendButton";
            this.extendButton.Size = new System.Drawing.Size(128, 23);
            this.extendButton.TabIndex = 12;
            this.extendButton.Text = "Extend";
            this.extendButton.UseVisualStyleBackColor = true;
            this.extendButton.Click += new System.EventHandler(this.extendButton_Click);
            // 
            // YLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 360);
            this.Controls.Add(this.extendButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.animBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.openButton);
            this.Name = "YLForm";
            this.Text = "YomiLayer7 by Anotak";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.ListBox animBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button extendButton;
    }
}

