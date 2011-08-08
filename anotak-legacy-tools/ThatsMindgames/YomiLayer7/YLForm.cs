using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AEDataLibrary;

namespace YomiLayer7
{
    public partial class YLForm : Form
    {
        public BACData currentBAC;

        public YLForm()
        {
            InitializeComponent();

            Text += ", build " + AEDataTools.GetCompileDate();
            Log(Text);
        }

        public void SaferExit()
        {
            AELogger.WriteLog();
            Application.Exit();
        }

        private void Log(string p)
        {
            AELogger.Log(p);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    AELogger.WriteLog();
                    break;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "bac";
            // The Filter property requires a search string after the pipe ( | )
            openFile.Filter = "BAC Files (*.bac)|*.bac|All files (*.*)|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                using (BinaryReader b = new BinaryReader(File.Open(openFile.FileNames[0], FileMode.Open)))
                {
                    currentBAC = new BACData(b);
                    saveButton.Enabled = true;
                    animBox.Enabled = true;
                    animBox.DataSource = currentBAC.GetAnimList();
                    animBox.SelectedIndex = 0;
                }
            }
            else
            {
                Log("nothing selected!");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "BAC files (*.bac)|*.bac|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileNames.Length > 0)
                {
                    currentBAC.WriteOutput(saveFileDialog1.FileNames[0]);
                }
            }
        }

        private void animBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentBAC.GetAnimAt(animBox.SelectedIndex).bEmpty)
            {
                exportButton.Enabled = false;
                importButton.Enabled = true;
                renameButton.Enabled = false;
                deleteButton.Enabled = false;
                extendButton.Enabled = true;
            }
            else
            {
                exportButton.Enabled = true;
                importButton.Enabled = true;
                renameButton.Enabled = true;
                deleteButton.Enabled = true;
                extendButton.Enabled = true;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            BACData.BACAnim outAnim = currentBAC.GetAnimAt(animBox.SelectedIndex);
            if(outAnim.bEmpty)
            {
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "DAT files (*.dat)|*.dat|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.FileName = outAnim.name + ".dat";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileNames.Length > 0)
                {
                    currentBAC.ExportAnimAt(animBox.SelectedIndex, saveFileDialog1.FileNames[0]);
                }
            }
        }

        

        private void importButton_Click(object sender, EventArgs e)
        {            
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "dat";
            // The Filter property requires a search string after the pipe ( | )
            openFile.Filter = "DAT Files (*.dat)|*.dat|All files (*.*)|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                using (BinaryReader b = new BinaryReader(File.Open(openFile.FileNames[0], FileMode.Open)))
                {
                    currentBAC.ImportAnimAt(b, textBox1.Text == "" ? "NO_NAME" : textBox1.Text, animBox.SelectedIndex);
                    RefreshData();
                }
            }
            else
            {
                Log("nothing selected!");
            }
        }

        private void RefreshData()
        {
            int s = animBox.SelectedIndex;
            animBox.DataSource = currentBAC.GetAnimList();
            animBox.SelectedIndex = s;
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            currentBAC.RenameAnimAt(animBox.SelectedIndex, textBox1.Text == "" ? "NO_NAME" : textBox1.Text);
            RefreshData();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            currentBAC.RemoveAnimAt(animBox.SelectedIndex);
            RefreshData();
        }

        private void extendButton_Click(object sender, EventArgs e)
        {
            currentBAC.ExtendAnimList(animBox.SelectedIndex);
            RefreshData();
            animBox.SelectedIndex = animBox.Items.Count-1;
        }
    }
}
