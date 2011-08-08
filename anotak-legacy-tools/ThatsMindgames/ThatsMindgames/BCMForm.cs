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

namespace ThatsMindgames
{
    public partial class BCMForm : Form
    {
        public BCMData currentBCM;

        public BCMForm()
        {
            try {
                InitializeComponent();

                Text += ", build " + AEDataTools.GetCompileDate();
                Log(Text);
            }
            catch (Exception e)
            {
                Houston(e);
            }
        }

        private void ThatsMindgames_Load(object sender, EventArgs e)
        {

        }

        // open
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.DefaultExt = "bcm";
                // The Filter property requires a search string after the pipe ( | )
                openFile.Filter = "BCM Files (*.bcm)|*.bcm|All files (*.*)|*.*";
                openFile.ShowDialog();
                if (openFile.FileNames.Length > 0)
                {
                    using (BinaryReader b = new BinaryReader(File.Open(openFile.FileNames[0], FileMode.Open)))
                    {
                        currentBCM = new BCMData(b);

                        RefreshListBoxes();
                        CancelListBox.SelectedIndex = 0;
                        CurrentListBox.SelectedIndex = 0;
                        InputListBox.SelectedIndex = 0;
                        button3.Enabled = true;
                        add.Enabled = true;
                        remove.Enabled = true;
                        textBox1.Enabled = true;
                        button4.Enabled = true;
                    }
                }
                else
                {
                    Log("nothing selected!");
                }
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void RefreshListBoxes()
        {
            try {
                int si = CancelListBox.SelectedIndex;
                CancelListBox.DataSource = currentBCM.GetCancelList();
                CancelListBox.SelectedIndex = si;
                RefreshCurrentAndInput();
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void RefreshCurrentAndInput()
        {
            try {
                int si = InputListBox.SelectedIndex;
                InputListBox.DataSource = currentBCM.GetInputList();
                InputListBox.SelectedIndex = si;
                CurrentListBox.DataSource = currentBCM.GetCurrentList(CancelListBox.SelectedIndex);
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
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

        

        // logs
        public void SaferExit()
        {
            AELogger.WriteLog();
            Application.Exit();
        }

        // when we have a problem then
        public void Houston(Exception e)
        {
            Log("Exception: " + e.Message);

            Log("Exception: " + e.StackTrace);

            if (e.InnerException != null)
            {
                Log("InnerException: " + e.InnerException.ToString());
            }
            MessageBox.Show(e.Message, "Exception!", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            SaferExit();
        }

        private void Log(string p)
        {
            AELogger.Log(p);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "BCM files (*.bcm)|*.bcm|All files (*.*)|*.*";
                //saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileNames.Length > 0)
                    {
                        currentBCM.WriteOutput(saveFileDialog1.FileNames[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void InputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CancelListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCurrentAndInput();
        }

        private void CurrentListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                currentBCM.AddCancel(CancelListBox.SelectedIndex, InputListBox.SelectedIndex);
                RefreshListBoxes();
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            try {
                currentBCM.RemoveCancel(CancelListBox.SelectedIndex, CurrentListBox.SelectedIndex);
                RefreshListBoxes();
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                currentBCM.AddCancelGroup(textBox1.Text == "" ? "NO_NAME" : textBox1.Text);
                RefreshListBoxes();
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                currentBCM.RemoveCancelGroup(CancelListBox.SelectedIndex);
                RefreshListBoxes();
            }
            catch (Exception ex)
            {
                Houston(ex);
            }
        }

        private void rename_Click(object sender, EventArgs e)
        {
            currentBCM.RenameCancel(CancelListBox.SelectedIndex, textBox1.Text == "" ? "NO_NAME" : textBox1.Text);
            RefreshListBoxes();
        }
    }
}
