using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ClgEditor
{
    public class CommandPart
    {
        public string Character { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public string FullText { get; set; }

        public string Acronym { get; set; }

        public static List<CommandPart> KnownParts { get; set; }

        public static CommandPart Find(string id)
        {
            return KnownParts.FirstOrDefault(p => p.Id == id) ?? KnownParts[0];
        }

        public static void Load(string filename)
        {
            KnownParts = new List<CommandPart>();
            KnownParts.Add(new CommandPart { Id = string.Empty, Character = "CMN", Description = "None" });
            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var data = line.Split(',');
                Debug.Assert(data.Length == 3 || data.Length == 5);
                
                var part = new CommandPart
                {
                    Id = data[0].Trim(),
                    Character = string.IsNullOrWhiteSpace(data[1]) ? data[0].Substring(7, 3) : data[1].Trim(),
                    Description = data[2].Trim(),
                };

                if (data.Length == 5)
                {
                    part.FullText = data[3].Trim();
                    part.Acronym = data[4].Trim();
                }

                if (string.IsNullOrWhiteSpace(part.FullText))
                {
                    part.FullText = part.Description;
                }

                if (string.IsNullOrWhiteSpace(part.Acronym))
                {
                    part.Acronym = part.Description;
                }

                KnownParts.Add(part);
            }
        }
    }
}
