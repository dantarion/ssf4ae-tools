using System.Collections.Generic;
using System.IO;
using ClgEditor;

namespace ChallengeModeEditor.Clg
{
    public class ClgCommand
    {
        public ClgCommand()
        {
            //this.ResStrings = new string[8];
            this.CriteriaIds = new List<int>();
            this.OnScreenPart1 = string.Empty;
            this.OnScreenPart2 = string.Empty;
            this.OnScreenPart3 = string.Empty;
            this.OnScreenPart4 = string.Empty;
            this.HelpMenuPart1 = string.Empty;
            this.HelpMenuPart2 = string.Empty;
            this.HelpMenuPart3 = string.Empty;
            this.HelpMenuPart4 = string.Empty;
        }

        public enum CriteriaTypeOption : short
        {
            Script = 0,
            Attack = 1,
        }

        public string DisplayName 
        { 
            get
            {
                return string.Format(
                    "{0}:{1}{2}{3}{4}",
                    this.CriteriaType.ToString(),
                    CommandPart.Find(this.OnScreenPart1).Acronym,
                    CommandPart.Find(this.OnScreenPart2).Acronym,
                    CommandPart.Find(this.OnScreenPart3).Acronym,
                    CommandPart.Find(this.OnScreenPart4).Acronym);
            }
        }

        //private string[] ResStrings { get; set; }
        
        public CriteriaTypeOption CriteriaType { get; set; }

        public List<int> CriteriaIds { get; set; }

        public string OnScreenPart1 { get; set; }
        public string OnScreenPart2 { get; set; }
        public string OnScreenPart3 { get; set; }
        public string OnScreenPart4 { get; set; }
        public string HelpMenuPart1 { get; set; }
        public string HelpMenuPart2 { get; set; }
        public string HelpMenuPart3 { get; set; }
        public string HelpMenuPart4 { get; set; }

        public string CriteriaString
        {
            get { return string.Join(", ", this.CriteriaIds); }
            set
            {
                this.CriteriaIds.Clear();
                var str = value ?? string.Empty;
                var parts = str.Split(',');
                foreach (var part in parts)
                {
                    int result;
                    if (int.TryParse(part.Trim(), out result))
                    {
                        this.CriteriaIds.Add(result);
                    }
                }
            }
        }

        internal uint BaseAddress { get; set; }

        public static ClgCommand ReadScriptAndCriteria(BinaryReader reader)
        {
            ClgCommand s= new ClgCommand();
            s.BaseAddress = (uint)reader.BaseStream.Position;

            s.OnScreenPart1 = reader.ReadCharArrayString(32);
            s.OnScreenPart2 = reader.ReadCharArrayString(32);
            s.OnScreenPart3 = reader.ReadCharArrayString(32);
            s.OnScreenPart4 = reader.ReadCharArrayString(32);
            s.HelpMenuPart1 = reader.ReadCharArrayString(32);
            s.HelpMenuPart2 = reader.ReadCharArrayString(32);
            s.HelpMenuPart3 = reader.ReadCharArrayString(32);
            s.HelpMenuPart4 = reader.ReadCharArrayString(32);
            
            s.CriteriaType = (CriteriaTypeOption)reader.ReadInt16();
            var criteriaCount = reader.ReadUInt16();
            var criteriaStart = s.BaseAddress + reader.ReadUInt32();

            var lastAddr = reader.BaseStream.Position;

            reader.BaseStream.Seek(criteriaStart, SeekOrigin.Begin);
            for (int i = 0; i < criteriaCount; i++)
            {
                s.CriteriaIds.Add(reader.ReadInt32());
            }

            reader.BaseStream.Seek(lastAddr, SeekOrigin.Begin);
            return s;
        }

        public void WriteScript(BinaryWriter writer)
        {
            this.BaseAddress = (uint)writer.BaseStream.Position;

            writer.WriteCharArrayString(this.OnScreenPart1, 32);
            writer.WriteCharArrayString(this.OnScreenPart2, 32);
            writer.WriteCharArrayString(this.OnScreenPart3, 32);
            writer.WriteCharArrayString(this.OnScreenPart4, 32);
            writer.WriteCharArrayString(this.HelpMenuPart1, 32);
            writer.WriteCharArrayString(this.HelpMenuPart2, 32);
            writer.WriteCharArrayString(this.HelpMenuPart3, 32);
            writer.WriteCharArrayString(this.HelpMenuPart4, 32);

            writer.Write((short)this.CriteriaType);
            writer.Write((ushort)this.CriteriaIds.Count);

            // Criteria offset placeholder
            writer.WriteInt32(0);
        }

        public void WriteCriteria(BinaryWriter writer)
        {
            var criteriaStart = writer.BaseStream.Position;
            var criteriaOffset = criteriaStart - this.BaseAddress;
            writer.Seek((int)(this.BaseAddress + 0x104), SeekOrigin.Begin);
            writer.Write((uint)criteriaOffset);
            writer.Seek((int)criteriaStart, SeekOrigin.Begin);

            for (int i = 0; i < this.CriteriaIds.Count; i++)
            {
                writer.WriteInt32(this.CriteriaIds[i]);
            }
        }
    }
}
