using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChallengeModeEditor.Clg;

namespace ChallengeModeEditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
//            var files = Directory.GetFiles(@"E:\SSFIV\resource\battle\chara", "*.clg", SearchOption.AllDirectories);

//            var lines = new List<string>();
//            foreach (var f in files)
//            {
//                var clg = ClgFile.Load(f);

//                foreach (var e in clg.Levels)
//                {
//                    //string line = string.Join(
//                    //    ",",
//                    //    clg.Character,
//                    //    e.Index,
//                    //    //c.ResUnk1,
//                    //    e.ResUnk1_1,
//                    //    e.ResUnk1_2,
//                    //    e.ResUnk2 == int.MaxValue ? "MAX" : e.ResUnk2.ToString(),
//                    //    e.Unk_Confusing,
//                    //    e.TargetState,
//                    //    e.UnkFlag);
//                    //lines.Add(line);

//                    for (int i = 0; i < e.Commands.Count; i++)
//                    {
//                        var s = e.Commands[i];
//                        string line =
//                            string.Join(",", clg.Character, e.Index, i, 
//                            s.OnScreenPart1,
//                            s.OnScreenPart2,
//                            s.OnScreenPart3,
//                            s.OnScreenPart4,
//                            s.HelpMenuPart1,
//s.HelpMenuPart2,
//s.HelpMenuPart3,
//s.HelpMenuPart4,
//                            s.CriteriaType,
//                            string.Join("/", s.CriteriaIds));
//                        lines.Add(line);
//                        //foreach (var str in e.Commands[i].OnScreenStrings)
//                        //{
//                        //    if (!resIds.Contains(str)) { resIds.Add(str); }
//                        //}

//                        //foreach (var str in e.Commands[i].HelpStrings)
//                        //{
//                        //    if (!resIds.Contains(str)) { resIds.Add(str); }
//                        //}
//                    }
//                }
//            }

//            //File.WriteAllLines(@"E:\SSFIV\DIY\CommandIds.txt", resIds.Select(r => r + "," + r));

//            //File.WriteAllLines(@"E:\SSFIV\DIY\ExportData\clg.csv", lines);
//            File.WriteAllLines(@"E:\SSFIV\DIY\ExportData\clg_scripts.csv", lines);



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
