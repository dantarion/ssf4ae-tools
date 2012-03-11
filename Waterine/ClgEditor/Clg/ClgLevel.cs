using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ChallengeModeEditor.Clg
{
    public class ClgLevel
    {
        public enum TargetStateOption : int
        {
            Standing = 0,
            Crouching = 1,
            Jumping = 2,
            StandingAttacking = 3,
            CrouchingAttacking = 4,
        }

        public enum UltraOption : int
        {
            UC1 = 0,
            UC2 = 1
        }

        public int Index { get; private set; }

        public string Name { get; set; }

        // Some unknown resource ID, part1
        public short ResUnk1_1 { get; set; }

        // Some unknown resource ID, part2
        public short ResUnk1_2 { get; set; }

        // Guess resource related. 
        // DDL = 1, other "new challenger" = 0, "vanilla character" = Int32.Max
        public int ResUnk2 { get; set; }

        // A confusing unknown value.
        // 0 for BLK,BLR,BSN,CNL,DDL,DSM,GUL,HKN,HDN,KEN,RYU,SGT,VEG,ZGF all levels and JRI only lv24.
        // 1 for other character all levels and JRI lv1-lv23.
        public int Unk_Confusing { get; set; }

        // Unknown flag.
        // 1 for HWK lv9, JRI lv4, VEG lv18
        // 0 for all other characters/levels
        public int UnkFlag { get; set; }

        // SGT (all levels) = 5, all others = 0.
        public uint Unk_SGT5 { get; set; }

        // BLR (all levels) = 1, all others = 0
        public int Unk_BLR1 { get; set; }
        
        public TargetStateOption TargetState { get; set; }
        public UltraOption UltraSelection { get; set; }

        internal uint Part3BaseAddress { get; set; }
        internal uint Part3ScriptOffset { get; set; }

        public List<ClgCommand> Commands { get; set; }

        public ClgLevel(int index)
        {
            this.Index = index;
            this.Commands = new List<ClgCommand>();
        }

        public void ReadCommandsAndCriterion(BinaryReader reader)
        {
            for (int i = 0; i < this.Commands.Count; i++) { this.Commands[i] = ClgCommand.ReadScriptAndCriteria(reader); }
        }

        public void WriteCommands(BinaryWriter writer)
        {
            // write-back offset
            var scriptStart = writer.BaseStream.Position;
            this.Part3ScriptOffset = (uint)(scriptStart - this.Part3BaseAddress);
            writer.Seek((int)(this.Part3BaseAddress + 12), SeekOrigin.Begin);
            writer.Write(this.Part3ScriptOffset);
            writer.Seek((int)scriptStart, SeekOrigin.Begin);

            for (int i = 0; i < this.Commands.Count; i++) { this.Commands[i].WriteScript(writer); }
        }

        public void WriteCriterion(BinaryWriter writer)
        {
            for (int i = 0; i < this.Commands.Count; i++) { this.Commands[i].WriteCriteria(writer); }
        }

        public void ReadPart3(BinaryReader reader)
        {
            this.Part3BaseAddress = (uint)reader.BaseStream.Position;
            Debug.Assert(this.Part3BaseAddress == 0xf30 + this.Index * 0x10);

            this.TargetState = (TargetStateOption)reader.ReadInt32();
            this.UnkFlag = reader.ReadInt32();
            uint count= reader.ReadUInt32();
            for (int i = 0; i < count; i++)
            {
                this.Commands.Add(null);
            }
            this.Part3ScriptOffset = reader.ReadUInt32();
        }

        public void WritePart3(BinaryWriter writer)
        {
            this.Part3BaseAddress = (uint)writer.BaseStream.Position;
            Debug.Assert(this.Part3BaseAddress == 0xf30 + this.Index * 0x10);

            writer.WriteInt32((int)this.TargetState);
            writer.WriteInt32(this.UnkFlag);
            writer.WriteInt32(this.Commands.Count);
            writer.WriteInt32(0); // script address placeholder
        }

        public void ReadPart2(BinaryReader reader)
        {
            // 0x00 - 0x10
            reader.ReadZeroBytes(4);
            this.Unk_BLR1 = reader.ReadInt32();
            this.ResUnk2 = reader.ReadInt32();
            reader.ReadConst32(-1);

            // 0x10 - 0x20
            reader.ReadConst32(0x1a);
            reader.ReadConst32(1);
            this.Unk_Confusing = reader.ReadInt32();
            reader.ReadZeroBytes(4);

            // 0x20 - 0x30
            reader.ReadZeroBytes(16);
        }

        public void WritePart2(BinaryWriter writer)
        {
            writer.WriteZeroBytes(4);
            writer.WriteInt32(this.Unk_BLR1);
            writer.WriteInt32(this.ResUnk2);
            writer.WriteInt32(-1);

            writer.WriteInt32(0x1a);
            writer.WriteInt32(1);
            writer.WriteInt32(this.Unk_Confusing);
            writer.WriteZeroBytes(4);

            writer.WriteZeroBytes(16);
        }

        public void ReadPart1(BinaryReader reader)
        {
            // 0x00 - 0x30
            this.Name = reader.ReadCharArrayString(8);
            reader.ReadZeroBytes(36);
            this.ResUnk1_1 = reader.ReadInt16();
            this.ResUnk1_2 = reader.ReadInt16();

            // 0x30 - 0x40
            reader.ReadConst32(-1);
            reader.ReadZeroBytes(12);

            // 0x40 - 0x50
            this.Unk_SGT5 = reader.ReadUInt32();
            reader.ReadZeroBytes(12);

            // 0x50 - 0x60
            reader.ReadConst32(-2);
            this.UltraSelection = (UltraOption)reader.ReadInt32();
            reader.ReadConst32(1);
            reader.ReadConst32(0x0ac0 - this.Index * 0x40);

            // 0x60 - 0x70
            reader.ReadZeroBytes(8);
            reader.ReadConst32(1);
            reader.ReadConst32(0x0f60 - this.Index * 0x60);
        }

        public void WritePart1(BinaryWriter writer)
        {
            writer.WriteCharArrayString(this.Name, 8);
            writer.WriteZeroBytes(36);
            writer.Write(this.ResUnk1_1);
            writer.Write(this.ResUnk1_2);

            writer.WriteInt32(-1);
            writer.WriteZeroBytes(12);

            writer.Write(this.Unk_SGT5);
            writer.WriteZeroBytes(12);

            writer.WriteInt32(-2);
            writer.WriteInt32((int)this.UltraSelection);
            writer.WriteInt32(1);
            writer.WriteInt32(0x0ac0 - this.Index * 0x40);

            writer.WriteZeroBytes(8);
            writer.WriteInt32(1);
            writer.WriteInt32(0x0f60 - this.Index * 0x60);
        }
    }
}
