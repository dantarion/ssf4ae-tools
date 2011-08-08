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

namespace GraveStormboner
{
    public partial class GSForm : Form
    {
        public BCMData currentBCM;
        public Boolean reading;

        public GSForm()
        {
            InitializeComponent();
            buttonsListBox.Items.Add("MP"); //0
            buttonsListBox.Items.Add("LP"); //1
            buttonsListBox.Items.Add("bit 2???"); //2
            buttonsListBox.Items.Add("FWD"); //3
            buttonsListBox.Items.Add("BACK"); //4
            buttonsListBox.Items.Add("DOWN"); //5
            buttonsListBox.Items.Add("UP"); //6
            buttonsListBox.Items.Add("NEUTRAL"); //7
            buttonsListBox.Items.Add("bit 8???"); //8

            buttonsListBox.Items.Add("bit 9???"); //9
            buttonsListBox.Items.Add("bit 10???"); //10
            buttonsListBox.Items.Add("bit 11???"); //11
            buttonsListBox.Items.Add("HK"); //12
            buttonsListBox.Items.Add("MK"); //13
            buttonsListBox.Items.Add("LK"); //14
            buttonsListBox.Items.Add("HP"); //15

            buttonsListBox.Items.Add("any button");
            buttonsListBox.Items.Add("any 2 set (requires any set)");
            buttonsListBox.Items.Add("all set buttons");
            buttonsListBox.Items.Add("any set button");
            buttonsListBox.Items.Add("bit 20???");
            buttonsListBox.Items.Add("bit 21???");
            buttonsListBox.Items.Add("need direction (strict)");
            buttonsListBox.Items.Add("need direction (nonstrict)");

            buttonsListBox.Items.Add("bit 24???");
            buttonsListBox.Items.Add("bit 25???");
            buttonsListBox.Items.Add("release button");
            buttonsListBox.Items.Add("press button");
            buttonsListBox.Items.Add("bit 28???");
            buttonsListBox.Items.Add("hold direction?");
            buttonsListBox.Items.Add("bit 30???");
            buttonsListBox.Items.Add("bit 31???");

            CloseFar_comboBox.Items.Add("Neither");
            CloseFar_comboBox.Items.Add("Far");
            CloseFar_comboBox.Items.Add("Close");

            this.buttonsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.buttonsListBox_ItemCheck);
            reading = false;

            Text += ", build " + AEDataTools.GetCompileDate();
            Log(Text);
        }

        // logs
        public void SaferExit()
        {
            AELogger.WriteLog();
            Application.Exit();
        }

        private void Log(string p)
        {
            AELogger.Log(p);
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    ReadDataImportant();
                    
                }
            }
            else
            {
                Log("nothing selected!");
            }
        }

        private void ReadDataImportant()
        {
            Motion_comboBox.Items.Clear();
            Motion_comboBox.Items.Add("255 - no motion");
            for (int i = 0; i < currentBCM.MotionList.Count; i++)
            {
                Motion_comboBox.Items.Add(i + " - " + currentBCM.MotionList[i].name);
            }

            inputListBox.DataSource = currentBCM.GetInputList();
            inputListBox.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
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

        private void inputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Log(inputListBox.SelectedIndex.ToString());
            ReadDataAgain();
        }

        private void ReadDataAgain()
        {
            reading = true;
            List<Boolean> a = currentBCM.InputList[inputListBox.SelectedIndex].Flags;
            for (int i = 0; i < a.Count; i++)
            {
                buttonsListBox.SetItemChecked(i, a[i]);
            }
            Log(currentBCM.InputList[inputListBox.SelectedIndex].CloseFar.ToString());
            switch (currentBCM.InputList[inputListBox.SelectedIndex].CloseFar)
            {
                case 1:
                    CloseFar_comboBox.SelectedIndex = 1;
                    break;
                case 2:
                    CloseFar_comboBox.SelectedIndex = 2;
                    break;
                default:
                    CloseFar_comboBox.SelectedIndex = 0;
                    break;
            }

            //numericUpDown1.Value = currentBCM.InputList[inputListBox.SelectedIndex].data[0];

            SuperMeterReq_control.Value = currentBCM.InputList[inputListBox.SelectedIndex].SuperMeterRequired;
            SuperMeterUsed_control.Value = currentBCM.InputList[inputListBox.SelectedIndex].SuperMeterUsed;
            UltraMeterReq_control.Value = currentBCM.InputList[inputListBox.SelectedIndex].UltraMeterRequired;
            UltraMeterUsed_control.Value = currentBCM.InputList[inputListBox.SelectedIndex].UltraMeterUsed;
            dist_Control.Value = (decimal)currentBCM.InputList[inputListBox.SelectedIndex].RestrictionDistance;
            float_control1.Value = (decimal)currentBCM.InputList[inputListBox.SelectedIndex].UnknownFloat44;
            float_control2.Value = (decimal)currentBCM.InputList[inputListBox.SelectedIndex].UnknownFloat48;
            Anim_control.Value = currentBCM.InputList[inputListBox.SelectedIndex].Animation;

            Motion_comboBox.SelectedIndex = currentBCM.InputList[inputListBox.SelectedIndex].Motion == 255 ? 0 : currentBCM.InputList[inputListBox.SelectedIndex].Motion + 1;

            reading = false;
        }

        private void buttonsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonsListBox_ItemCheck(object sender, EventArgs e)
        {
            if (reading)
                return;
            //UInt16 output = 0;
            List<Boolean> flags = new List<bool>();
            
            ItemCheckEventArgs a = (ItemCheckEventArgs)e;
            Log(a.Index.ToString());

            for (int i = 0; i < buttonsListBox.Items.Count; i++)
            {
                flags.Add(false);
            }
            for (int i = 0; i < buttonsListBox.CheckedIndices.Count; i++)
            {
                flags[buttonsListBox.CheckedIndices[i]] = true;
                // 1 (15) + 2 (14) + 4 (13) + 1024 (6)  + 16384 (1) + 32768 (0)
                //output += (UInt16)AEDataTools.IntPow(2, 15 - (uint)buttonsListBox.CheckedIndices[i]);
            }

            if (a != null)
            {
                if (a.NewValue == CheckState.Checked)
                {
                    flags[a.Index] = true;
                }
                else if (a.NewValue == CheckState.Unchecked)
                {
                    flags[a.Index] = false;
                }
            }

            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.Flags = flags;
            //input.ButtonsRequiredUInt16 = output;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void CloseFar_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentBCM.InputList[inputListBox.SelectedIndex].data[4] = (byte)CloseFar_comboBox.SelectedIndex;
        }

        private void SuperMeter_control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.SuperMeterRequired = (short)SuperMeterReq_control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void SuperMeterUsed_control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.SuperMeterUsed = (short)SuperMeterUsed_control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void UltraMeterReq_control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.UltraMeterRequired = (short)UltraMeterReq_control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void UltraMeterUsed_control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.UltraMeterUsed = (short)UltraMeterUsed_control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void Motion_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.Motion = (byte)((Motion_comboBox.SelectedIndex == 0) ? 255 : Motion_comboBox.SelectedIndex - 1);
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void Anim_control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.Animation = (UInt32)Anim_control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void duplicate_button_Click(object sender, EventArgs e)
        {
            currentBCM.DuplicateInput(inputListBox.SelectedIndex, textBox1.Text == "" ? "NO_NAME" : textBox1.Text);
            ReadDataImportant();
        }

        private void rename_button_Click(object sender, EventArgs e)
        {
            currentBCM.RenameInput(inputListBox.SelectedIndex, textBox1.Text == "" ? "NO_NAME" : textBox1.Text);
            ReadDataImportant();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            currentBCM.RemoveInput(inputListBox.SelectedIndex);
            ReadDataImportant();
        }

        private void dist_Control_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.RestrictionDistance = (float)dist_Control.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void float_control1_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.UnknownFloat44 = (float)float_control1.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }

        private void float_control2_ValueChanged(object sender, EventArgs e)
        {
            if (reading)
                return;
            BCMData.BCMInput input = currentBCM.InputList[inputListBox.SelectedIndex];
            input.UnknownFloat48 = (float)float_control1.Value;
            currentBCM.InputList[inputListBox.SelectedIndex] = input;
        }
    }
}
