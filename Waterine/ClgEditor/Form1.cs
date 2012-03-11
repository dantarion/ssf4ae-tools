using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ChallengeModeEditor.Clg;
using ClgEditor;

namespace ChallengeModeEditor
{
    public partial class Form1 : Form
    {
        private ClgFile ActiveFile { get; set; }

        private ComboBox[] cbMatrix;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ActiveFile = Clg.ClgFile.Load(this.openFileDialog1.FileName);

                var commands = from p in CommandPart.KnownParts
                               where string.Equals(p.Character, "CMN", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(p.Character, this.ActiveFile.Character)
                               select p;
                foreach (var cb in this.cbMatrix)
                {
                    // !!!Intentionally re-evaluate the query to get a new list for each combobox!!!
                    cb.DataSource = commands.ToArray();
                    cb.DisplayMember = "Description";
                    cb.ValueMember = "Id";
                }

                this.bsLevelSelect.DataSource = this.ActiveFile.Levels;
                this.cmdSave.Enabled = true;
                this.toolStripScripts.Enabled = true;
                this.lblStatus.Text = this.ActiveFile.FileName;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ClgFile.Save(this.ActiveFile, this.saveFileDialog1.FileName);
            }
        }

        private void cmdNewScript_Click(object sender, EventArgs e)
        {
            var bs = this.lbScripts.DataSource as BindingSource;
            if (bs == null) return;

            bs.AddNew();
        }

        private void cmdDeleteScript_Click(object sender, EventArgs e)
        {
            var bs = this.lbScripts.DataSource as BindingSource;
            if (bs == null) return;

            bs.RemoveCurrent();
        }

        private void cmdMoveUpScript_Click(object sender, EventArgs e)
        {
            var bs = this.lbScripts.DataSource as BindingSource;
            if (bs == null) return;

            int position = bs.Position;
            if (position == 0) return;  // already at top

            bs.RaiseListChangedEvents = false;

            var current = bs.Current;
            bs.Remove(current);
            position--;
            bs.Insert(position, current);
            bs.Position = position;

            bs.RaiseListChangedEvents = true;
            bs.ResetBindings(false);
        }

        private void cmdMoveDownScript_Click(object sender, EventArgs e)
        {
            var bs = this.lbScripts.DataSource as BindingSource;
            if (bs == null) return;

            int position = bs.Position;
            if (position == bs.Count - 1) return;  // already at bottom

            bs.RaiseListChangedEvents = false;
            var current = bs.Current;
            bs.Remove(current);
            position++;
            bs.Insert(position, current);
            bs.Position = position;

            bs.RaiseListChangedEvents = true;
            bs.ResetBindings(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cbUltraSelection.DataSource = Enum.GetValues(typeof(ClgLevel.UltraOption));
            this.cbTargetState.DataSource = Enum.GetValues(typeof(ClgLevel.TargetStateOption));
            this.cbCriteriaType.DataSource = Enum.GetValues(typeof(ClgCommand.CriteriaTypeOption));
            this.cbMatrix = new[]
            {
                this.comboBox1, this.comboBox2, this.comboBox3, this.comboBox4, 
                this.comboBox5, this.comboBox6, this.comboBox7, this.comboBox8
            };

            if (File.Exists("CommandIds.txt"))
            {
                CommandPart.Load(@"CommandIds.txt");
            }
            else
            {
                throw new FileNotFoundException("CommandIds.txt not found.");
            }

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime compileDate =
                new DateTime(Math.Max(version.Build - 1, 0) * TimeSpan.TicksPerDay + version.Revision * TimeSpan.TicksPerSecond * 2).AddYears(1999);
            this.lblAbout.Text = string.Format(
                "Last updated: {0} Author: Waterine",
                compileDate.ToShortDateString());
        }
    }
}
