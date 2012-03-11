using System.Diagnostics;
using System.IO;
using System.Text;

namespace ChallengeModeEditor.Clg
{
    public class ClgFile
    {
        public string FileName { get; private set; }

        public string Character { get; private set; }

        public ClgLevel[] Levels { get; private set; }

        public ClgFile()
        {
            this.Levels = new ClgLevel[24];
            for (int i = 0; i < 24; i++)
            {
                this.Levels[i] = new ClgLevel(i + 1);
            }
        }

        public void Write(BinaryWriter writer)
        {
            Debug.Assert(writer.BaseStream.Position == 0);

            // Header bytes 0x00 - 0x30
            writer.Write("#CLG".ToCharArray());
            writer.Write(new byte[] { 0xfe, 0xff, 0x40, 0x00, 0x01, 0x00, 0x00, 0x00 });
            writer.Write(new byte[36]);
            Debug.Assert(writer.BaseStream.Position == 0x30);

            // Header info 0x30 - 0x40
            writer.Write(24);
            writer.Write(0x40);
            writer.Write(new byte[8]); // TODO: unknown data
            
            for (int i = 0; i < 24; i++) { this.Levels[i].WritePart1(writer); }
            for (int i = 0; i < 24; i++) { this.Levels[i].WritePart2(writer); }
            for (int i = 0; i < 24; i++) { this.Levels[i].WritePart3(writer); }
            for (int i = 0; i < 24; i++) { this.Levels[i].WriteCommands(writer); }
            for (int i = 0; i < 24; i++) { this.Levels[i].WriteCriterion(writer); }
        }

        public void Read(BinaryReader reader)
        {
            // Header length 0x30, same bytes for all characters.
            reader.BaseStream.Seek(0x30, SeekOrigin.Begin);

            // Header info 0x30 - 0x40
            reader.ReadConst32(24); // challenge count
            reader.ReadConst32(0x40); // challenge start
            var unkDataCount = reader.ReadInt32(); // RYU has 2, JHA has 1, others have 0.
            var unkDataStart = reader.ReadUInt32();

            // Levels
            for (int i = 0; i < 24; i++) { this.Levels[i].ReadPart1(reader); }
            for (int i = 0; i < 24; i++) { this.Levels[i].ReadPart2(reader); }
            for (int i = 0; i < 24; i++) { this.Levels[i].ReadPart3(reader); }

            for (int i = 0; i < 24; i++)
            {
                Debug.Assert(reader.BaseStream.Position == (this.Levels[i].Part3BaseAddress + this.Levels[i].Part3ScriptOffset));
                this.Levels[i].ReadCommandsAndCriterion(reader);
            }
        }

        public static void Save(ClgFile clg, string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs, Encoding.ASCII))
            {
                clg.Write(writer);
            }
        }

        public static ClgFile Load(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs, Encoding.ASCII))
            {
                var clg = new ClgFile { FileName = filename, Character = filename.Substring(filename.Length - 7, 3) };
                clg.Read(reader);
                return clg;
            }
        }
    }
}
