namespace ChallengeModeEditor
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
            this.components = new System.ComponentModel.Container();
            this.toolStripMainMenu = new System.Windows.Forms.ToolStrip();
            this.cmdOpen = new System.Windows.Forms.ToolStripButton();
            this.cmdSave = new System.Windows.Forms.ToolStripButton();
            this.lblAbout = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLevelSelect = new System.Windows.Forms.ListBox();
            this.bsLevelSelect = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbScripts = new System.Windows.Forms.ListBox();
            this.bsScripts = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripScripts = new System.Windows.Forms.ToolStrip();
            this.cmdNewScript = new System.Windows.Forms.ToolStripButton();
            this.cmdDeleteScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdMoveUpScript = new System.Windows.Forms.ToolStripButton();
            this.cmdMoveDownScript = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCriteriaType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTargetState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbUltraSelection = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevelSelect)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsScripts)).BeginInit();
            this.toolStripScripts.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMainMenu
            // 
            this.toolStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOpen,
            this.cmdSave,
            this.lblAbout});
            this.toolStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMainMenu.Name = "toolStripMainMenu";
            this.toolStripMainMenu.Size = new System.Drawing.Size(888, 27);
            this.toolStripMainMenu.TabIndex = 0;
            this.toolStripMainMenu.Text = "toolStrip1";
            // 
            // cmdOpen
            // 
            this.cmdOpen.Image = global::ClgEditor.Properties.Resources.openHS;
            this.cmdOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(74, 24);
            this.cmdOpen.Text = "Open...";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Image = global::ClgEditor.Properties.Resources.saveHS;
            this.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(69, 24);
            this.cmdSave.Text = "Save...";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // lblAbout
            // 
            this.lblAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(216, 24);
            this.lblAbout.Text = "Build Date: ?? Author: Waterine";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(888, 433);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLevelSelect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 433);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Levels";
            // 
            // lbLevelSelect
            // 
            this.lbLevelSelect.DataSource = this.bsLevelSelect;
            this.lbLevelSelect.DisplayMember = "Name";
            this.lbLevelSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLevelSelect.FormattingEnabled = true;
            this.lbLevelSelect.ItemHeight = 16;
            this.lbLevelSelect.Location = new System.Drawing.Point(3, 18);
            this.lbLevelSelect.Name = "lbLevelSelect";
            this.lbLevelSelect.Size = new System.Drawing.Size(102, 412);
            this.lbLevelSelect.TabIndex = 0;
            // 
            // bsLevelSelect
            // 
            this.bsLevelSelect.DataSource = typeof(ChallengeModeEditor.Clg.ClgLevel);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 375);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commands";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 18);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbScripts);
            this.splitContainer2.Panel1.Controls.Add(this.toolStripScripts);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.cbCriteriaType);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox5);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox6);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox7);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox8);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox4);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox3);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer2.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(770, 354);
            this.splitContainer2.SplitterDistance = 135;
            this.splitContainer2.TabIndex = 1;
            // 
            // lbScripts
            // 
            this.lbScripts.DataSource = this.bsScripts;
            this.lbScripts.DisplayMember = "DisplayName";
            this.lbScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScripts.FormattingEnabled = true;
            this.lbScripts.ItemHeight = 16;
            this.lbScripts.Location = new System.Drawing.Point(0, 25);
            this.lbScripts.Name = "lbScripts";
            this.lbScripts.Size = new System.Drawing.Size(135, 329);
            this.lbScripts.TabIndex = 0;
            // 
            // bsScripts
            // 
            this.bsScripts.DataMember = "Commands";
            this.bsScripts.DataSource = this.bsLevelSelect;
            // 
            // toolStripScripts
            // 
            this.toolStripScripts.Enabled = false;
            this.toolStripScripts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNewScript,
            this.cmdDeleteScript,
            this.toolStripSeparator1,
            this.cmdMoveUpScript,
            this.cmdMoveDownScript});
            this.toolStripScripts.Location = new System.Drawing.Point(0, 0);
            this.toolStripScripts.Name = "toolStripScripts";
            this.toolStripScripts.Size = new System.Drawing.Size(135, 25);
            this.toolStripScripts.TabIndex = 1;
            // 
            // cmdNewScript
            // 
            this.cmdNewScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdNewScript.Image = global::ClgEditor.Properties.Resources.Annotation_New;
            this.cmdNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNewScript.Name = "cmdNewScript";
            this.cmdNewScript.Size = new System.Drawing.Size(23, 22);
            this.cmdNewScript.Text = "toolStripButton1";
            this.cmdNewScript.Click += new System.EventHandler(this.cmdNewScript_Click);
            // 
            // cmdDeleteScript
            // 
            this.cmdDeleteScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeleteScript.Image = global::ClgEditor.Properties.Resources.DeleteHS;
            this.cmdDeleteScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeleteScript.Name = "cmdDeleteScript";
            this.cmdDeleteScript.Size = new System.Drawing.Size(23, 22);
            this.cmdDeleteScript.Text = "toolStripButton2";
            this.cmdDeleteScript.Click += new System.EventHandler(this.cmdDeleteScript_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdMoveUpScript
            // 
            this.cmdMoveUpScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMoveUpScript.Image = global::ClgEditor.Properties.Resources.BuilderDialog_moveup;
            this.cmdMoveUpScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdMoveUpScript.Name = "cmdMoveUpScript";
            this.cmdMoveUpScript.Size = new System.Drawing.Size(23, 22);
            this.cmdMoveUpScript.Text = "toolStripButton3";
            this.cmdMoveUpScript.Click += new System.EventHandler(this.cmdMoveUpScript_Click);
            // 
            // cmdMoveDownScript
            // 
            this.cmdMoveDownScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMoveDownScript.Image = global::ClgEditor.Properties.Resources.BuilderDialog_movedown;
            this.cmdMoveDownScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdMoveDownScript.Name = "cmdMoveDownScript";
            this.cmdMoveDownScript.Size = new System.Drawing.Size(23, 22);
            this.cmdMoveDownScript.Text = "toolStripButton4";
            this.cmdMoveDownScript.Click += new System.EventHandler(this.cmdMoveDownScript_Click);
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScripts, "CriteriaString", true));
            this.textBox1.Location = new System.Drawing.Point(10, 229);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(610, 22);
            this.textBox1.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(308, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Criteria indices (use comma to separate values)";
            // 
            // cbCriteriaType
            // 
            this.cbCriteriaType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsScripts, "CriteriaType", true));
            this.cbCriteriaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriteriaType.FormattingEnabled = true;
            this.cbCriteriaType.Location = new System.Drawing.Point(10, 161);
            this.cbCriteriaType.Name = "cbCriteriaType";
            this.cbCriteriaType.Size = new System.Drawing.Size(134, 24);
            this.cbCriteriaType.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Criteria type";
            // 
            // comboBox5
            // 
            this.comboBox5.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "HelpMenuPart2", true));
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(164, 95);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(148, 24);
            this.comboBox5.TabIndex = 4;
            // 
            // comboBox6
            // 
            this.comboBox6.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "HelpMenuPart4", true));
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(472, 95);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(148, 24);
            this.comboBox6.TabIndex = 5;
            // 
            // comboBox7
            // 
            this.comboBox7.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "HelpMenuPart3", true));
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(318, 95);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(148, 24);
            this.comboBox7.TabIndex = 6;
            // 
            // comboBox8
            // 
            this.comboBox8.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "HelpMenuPart1", true));
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(10, 95);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(148, 24);
            this.comboBox8.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Help menu command text";
            // 
            // comboBox4
            // 
            this.comboBox4.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "OnScreenPart2", true));
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(164, 31);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(148, 24);
            this.comboBox4.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "OnScreenPart4", true));
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(472, 31);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(148, 24);
            this.comboBox3.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "OnScreenPart3", true));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(318, 31);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(148, 24);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScripts, "OnScreenPart1", true));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "On-screen command text";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbTargetState);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbUltraSelection);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 58);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target State";
            // 
            // cbTargetState
            // 
            this.cbTargetState.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsLevelSelect, "TargetState", true));
            this.cbTargetState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetState.FormattingEnabled = true;
            this.cbTargetState.Location = new System.Drawing.Point(369, 26);
            this.cbTargetState.Name = "cbTargetState";
            this.cbTargetState.Size = new System.Drawing.Size(134, 24);
            this.cbTargetState.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ultra Selection";
            // 
            // cbUltraSelection
            // 
            this.cbUltraSelection.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsLevelSelect, "UltraSelection", true));
            this.cbUltraSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUltraSelection.FormattingEnabled = true;
            this.cbUltraSelection.Location = new System.Drawing.Point(128, 26);
            this.cbUltraSelection.Name = "cbUltraSelection";
            this.cbUltraSelection.Size = new System.Drawing.Size(130, 24);
            this.cbUltraSelection.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Clg Files|*.clg|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "clg";
            this.saveFileDialog1.Filter = "Clg Files|*.clg";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(888, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 20);
            this.lblStatus.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripMainMenu);
            this.Name = "Form1";
            this.Text = "SSF4 Challenge Mode Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStripMainMenu.ResumeLayout(false);
            this.toolStripMainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsLevelSelect)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsScripts)).EndInit();
            this.toolStripScripts.ResumeLayout(false);
            this.toolStripScripts.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMainMenu;
        private System.Windows.Forms.ToolStripButton cmdOpen;
        private System.Windows.Forms.ToolStripButton cmdSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbLevelSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUltraSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTargetState;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lbScripts;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.BindingSource bsLevelSelect;
        private System.Windows.Forms.BindingSource bsScripts;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCriteriaType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripLabel lblAbout;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStrip toolStripScripts;
        private System.Windows.Forms.ToolStripButton cmdNewScript;
        private System.Windows.Forms.ToolStripButton cmdDeleteScript;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdMoveUpScript;
        private System.Windows.Forms.ToolStripButton cmdMoveDownScript;
    }
}

