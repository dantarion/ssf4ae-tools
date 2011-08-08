namespace ThatsMindgames
{
    partial class BCMForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CancelListBox = new System.Windows.Forms.ListBox();
            this.InputListBox = new System.Windows.Forms.ListBox();
            this.CurrentListBox = new System.Windows.Forms.ListBox();
            this.add = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.rename = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CancelListBox
            // 
            this.CancelListBox.FormattingEnabled = true;
            this.CancelListBox.ItemHeight = 16;
            this.CancelListBox.Location = new System.Drawing.Point(13, 42);
            this.CancelListBox.Name = "CancelListBox";
            this.CancelListBox.Size = new System.Drawing.Size(219, 356);
            this.CancelListBox.TabIndex = 2;
            this.CancelListBox.SelectedIndexChanged += new System.EventHandler(this.CancelListBox_SelectedIndexChanged);
            // 
            // InputListBox
            // 
            this.InputListBox.FormattingEnabled = true;
            this.InputListBox.ItemHeight = 16;
            this.InputListBox.Location = new System.Drawing.Point(238, 42);
            this.InputListBox.Name = "InputListBox";
            this.InputListBox.Size = new System.Drawing.Size(222, 356);
            this.InputListBox.TabIndex = 3;
            this.InputListBox.SelectedIndexChanged += new System.EventHandler(this.InputListBox_SelectedIndexChanged);
            // 
            // CurrentListBox
            // 
            this.CurrentListBox.FormattingEnabled = true;
            this.CurrentListBox.ItemHeight = 16;
            this.CurrentListBox.Location = new System.Drawing.Point(564, 42);
            this.CurrentListBox.Name = "CurrentListBox";
            this.CurrentListBox.Size = new System.Drawing.Size(165, 356);
            this.CurrentListBox.TabIndex = 4;
            this.CurrentListBox.SelectedIndexChanged += new System.EventHandler(this.CurrentListBox_SelectedIndexChanged);
            // 
            // add
            // 
            this.add.Enabled = false;
            this.add.Location = new System.Drawing.Point(466, 119);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 5;
            this.add.Text = "add->";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // remove
            // 
            this.remove.Enabled = false;
            this.remove.Location = new System.Drawing.Point(466, 192);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(92, 23);
            this.remove.TabIndex = 6;
            this.remove.Text = "<-remove";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(176, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "New Category";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(288, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(189, 22);
            this.textBox1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(591, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(138, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "remove category";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(484, 13);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(101, 23);
            this.rename.TabIndex = 10;
            this.rename.Text = "rename cat";
            this.rename.UseVisualStyleBackColor = true;
            this.rename.Click += new System.EventHandler(this.rename_Click);
            // 
            // BCMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 410);
            this.Controls.Add(this.rename);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.add);
            this.Controls.Add(this.CurrentListBox);
            this.Controls.Add(this.InputListBox);
            this.Controls.Add(this.CancelListBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "BCMForm";
            this.Text = "thats mindgames. by Anotak";
            this.Load += new System.EventHandler(this.ThatsMindgames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox CancelListBox;
        private System.Windows.Forms.ListBox InputListBox;
        private System.Windows.Forms.ListBox CurrentListBox;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button rename;
    }
}

