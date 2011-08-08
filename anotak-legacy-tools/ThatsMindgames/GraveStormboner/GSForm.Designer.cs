namespace GraveStormboner
{
    partial class GSForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.inputListBox = new System.Windows.Forms.ListBox();
            this.buttonsListBox = new System.Windows.Forms.CheckedListBox();
            this.CloseFar_comboBox = new System.Windows.Forms.ComboBox();
            this.SuperMeterReq_control = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuperMeterUsed_control = new System.Windows.Forms.NumericUpDown();
            this.UltraMeterUsed_control = new System.Windows.Forms.NumericUpDown();
            this.UltraMeterReq_control = new System.Windows.Forms.NumericUpDown();
            this.Motion_comboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Anim_control = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.duplicate_button = new System.Windows.Forms.Button();
            this.rename_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.dist_Control = new System.Windows.Forms.NumericUpDown();
            this.float_control1 = new System.Windows.Forms.NumericUpDown();
            this.float_control2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SuperMeterReq_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperMeterUsed_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraMeterUsed_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraMeterReq_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anim_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dist_Control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.float_control1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.float_control2)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.ItemHeight = 16;
            this.inputListBox.Location = new System.Drawing.Point(12, 42);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(157, 292);
            this.inputListBox.TabIndex = 4;
            this.inputListBox.SelectedIndexChanged += new System.EventHandler(this.inputListBox_SelectedIndexChanged);
            // 
            // buttonsListBox
            // 
            this.buttonsListBox.FormattingEnabled = true;
            this.buttonsListBox.Location = new System.Drawing.Point(175, 42);
            this.buttonsListBox.Name = "buttonsListBox";
            this.buttonsListBox.Size = new System.Drawing.Size(164, 293);
            this.buttonsListBox.TabIndex = 6;
            this.buttonsListBox.SelectedIndexChanged += new System.EventHandler(this.buttonsListBox_SelectedIndexChanged);
            // 
            // CloseFar_comboBox
            // 
            this.CloseFar_comboBox.FormattingEnabled = true;
            this.CloseFar_comboBox.Location = new System.Drawing.Point(345, 42);
            this.CloseFar_comboBox.Name = "CloseFar_comboBox";
            this.CloseFar_comboBox.Size = new System.Drawing.Size(115, 24);
            this.CloseFar_comboBox.TabIndex = 7;
            this.CloseFar_comboBox.SelectedIndexChanged += new System.EventHandler(this.CloseFar_comboBox_SelectedIndexChanged);
            // 
            // SuperMeterReq_control
            // 
            this.SuperMeterReq_control.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.SuperMeterReq_control.Location = new System.Drawing.Point(394, 135);
            this.SuperMeterReq_control.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.SuperMeterReq_control.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.SuperMeterReq_control.Name = "SuperMeterReq_control";
            this.SuperMeterReq_control.Size = new System.Drawing.Size(52, 22);
            this.SuperMeterReq_control.TabIndex = 8;
            this.SuperMeterReq_control.ValueChanged += new System.EventHandler(this.SuperMeter_control_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Super";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Ultra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "req / used";
            // 
            // SuperMeterUsed_control
            // 
            this.SuperMeterUsed_control.Location = new System.Drawing.Point(453, 135);
            this.SuperMeterUsed_control.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.SuperMeterUsed_control.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.SuperMeterUsed_control.Name = "SuperMeterUsed_control";
            this.SuperMeterUsed_control.Size = new System.Drawing.Size(56, 22);
            this.SuperMeterUsed_control.TabIndex = 12;
            this.SuperMeterUsed_control.ValueChanged += new System.EventHandler(this.SuperMeterUsed_control_ValueChanged);
            // 
            // UltraMeterUsed_control
            // 
            this.UltraMeterUsed_control.Location = new System.Drawing.Point(453, 163);
            this.UltraMeterUsed_control.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.UltraMeterUsed_control.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.UltraMeterUsed_control.Name = "UltraMeterUsed_control";
            this.UltraMeterUsed_control.Size = new System.Drawing.Size(56, 22);
            this.UltraMeterUsed_control.TabIndex = 14;
            this.UltraMeterUsed_control.ValueChanged += new System.EventHandler(this.UltraMeterUsed_control_ValueChanged);
            // 
            // UltraMeterReq_control
            // 
            this.UltraMeterReq_control.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.UltraMeterReq_control.Location = new System.Drawing.Point(394, 163);
            this.UltraMeterReq_control.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.UltraMeterReq_control.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.UltraMeterReq_control.Name = "UltraMeterReq_control";
            this.UltraMeterReq_control.Size = new System.Drawing.Size(52, 22);
            this.UltraMeterReq_control.TabIndex = 13;
            this.UltraMeterReq_control.ValueChanged += new System.EventHandler(this.UltraMeterReq_control_ValueChanged);
            // 
            // Motion_comboBox
            // 
            this.Motion_comboBox.FormattingEnabled = true;
            this.Motion_comboBox.Location = new System.Drawing.Point(407, 72);
            this.Motion_comboBox.Name = "Motion_comboBox";
            this.Motion_comboBox.Size = new System.Drawing.Size(144, 24);
            this.Motion_comboBox.TabIndex = 15;
            this.Motion_comboBox.SelectedIndexChanged += new System.EventHandler(this.Motion_comboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Motion";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Anim";
            // 
            // Anim_control
            // 
            this.Anim_control.Hexadecimal = true;
            this.Anim_control.Location = new System.Drawing.Point(390, 211);
            this.Anim_control.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.Anim_control.Name = "Anim_control";
            this.Anim_control.Size = new System.Drawing.Size(56, 22);
            this.Anim_control.TabIndex = 18;
            this.Anim_control.ValueChanged += new System.EventHandler(this.Anim_control_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(175, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 22);
            this.textBox1.TabIndex = 19;
            // 
            // duplicate_button
            // 
            this.duplicate_button.Location = new System.Drawing.Point(346, 11);
            this.duplicate_button.Name = "duplicate_button";
            this.duplicate_button.Size = new System.Drawing.Size(101, 23);
            this.duplicate_button.TabIndex = 20;
            this.duplicate_button.Text = "duplicate this";
            this.duplicate_button.UseVisualStyleBackColor = true;
            this.duplicate_button.Click += new System.EventHandler(this.duplicate_button_Click);
            // 
            // rename_button
            // 
            this.rename_button.Location = new System.Drawing.Point(453, 11);
            this.rename_button.Name = "rename_button";
            this.rename_button.Size = new System.Drawing.Size(91, 23);
            this.rename_button.TabIndex = 21;
            this.rename_button.Text = "rename";
            this.rename_button.UseVisualStyleBackColor = true;
            this.rename_button.Click += new System.EventHandler(this.rename_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(551, 13);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(114, 22);
            this.delete_button.TabIndex = 22;
            this.delete_button.Text = "delete";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // dist_Control
            // 
            this.dist_Control.DecimalPlaces = 7;
            this.dist_Control.Location = new System.Drawing.Point(389, 239);
            this.dist_Control.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.dist_Control.Minimum = new decimal(new int[] {
            0,
            1,
            0,
            -2147483648});
            this.dist_Control.Name = "dist_Control";
            this.dist_Control.Size = new System.Drawing.Size(120, 22);
            this.dist_Control.TabIndex = 23;
            this.dist_Control.ValueChanged += new System.EventHandler(this.dist_Control_ValueChanged);
            // 
            // float_control1
            // 
            this.float_control1.DecimalPlaces = 7;
            this.float_control1.Location = new System.Drawing.Point(389, 267);
            this.float_control1.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.float_control1.Minimum = new decimal(new int[] {
            0,
            1,
            0,
            -2147483648});
            this.float_control1.Name = "float_control1";
            this.float_control1.Size = new System.Drawing.Size(120, 22);
            this.float_control1.TabIndex = 24;
            this.float_control1.ValueChanged += new System.EventHandler(this.float_control1_ValueChanged);
            // 
            // float_control2
            // 
            this.float_control2.DecimalPlaces = 7;
            this.float_control2.Location = new System.Drawing.Point(389, 295);
            this.float_control2.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
            this.float_control2.Minimum = new decimal(new int[] {
            0,
            1,
            0,
            -2147483648});
            this.float_control2.Name = "float_control2";
            this.float_control2.Size = new System.Drawing.Size(120, 22);
            this.float_control2.TabIndex = 25;
            this.float_control2.ValueChanged += new System.EventHandler(this.float_control2_ValueChanged);
            // 
            // GSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 380);
            this.Controls.Add(this.float_control2);
            this.Controls.Add(this.float_control1);
            this.Controls.Add(this.dist_Control);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.rename_button);
            this.Controls.Add(this.duplicate_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Anim_control);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Motion_comboBox);
            this.Controls.Add(this.UltraMeterUsed_control);
            this.Controls.Add(this.UltraMeterReq_control);
            this.Controls.Add(this.SuperMeterUsed_control);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SuperMeterReq_control);
            this.Controls.Add(this.CloseFar_comboBox);
            this.Controls.Add(this.buttonsListBox);
            this.Controls.Add(this.inputListBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "GSForm";
            this.Text = "Grave Stormboner by Anotak";
            ((System.ComponentModel.ISupportInitialize)(this.SuperMeterReq_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperMeterUsed_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraMeterUsed_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraMeterReq_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anim_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dist_Control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.float_control1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.float_control2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox inputListBox;
        private System.Windows.Forms.CheckedListBox buttonsListBox;
        private System.Windows.Forms.ComboBox CloseFar_comboBox;
        private System.Windows.Forms.NumericUpDown SuperMeterReq_control;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SuperMeterUsed_control;
        private System.Windows.Forms.NumericUpDown UltraMeterUsed_control;
        private System.Windows.Forms.NumericUpDown UltraMeterReq_control;
        private System.Windows.Forms.ComboBox Motion_comboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Anim_control;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button duplicate_button;
        private System.Windows.Forms.Button rename_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.NumericUpDown dist_Control;
        private System.Windows.Forms.NumericUpDown float_control1;
        private System.Windows.Forms.NumericUpDown float_control2;
    }
}

