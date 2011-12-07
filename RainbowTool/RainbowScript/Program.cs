using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RainbowLib;
namespace RainbowScript
{
    class Program
    {
        static void Main(string[] args)
        {
            //Check for config.py and create dirs if they don't exist
            if (!File.Exists("config.py"))
            {
                Console.Write("error! config.py doesn't exist!\nRainbowScript doesn't know where to find bcm/bac files!\nExiting!");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("loading up config.py!");
            IronPythonHelper.Init();
            IronPythonHelper.RunFile("config.py");
            if (!Directory.Exists("scripts"))
                Directory.CreateDirectory("scripts");
            if (!Directory.Exists("output"))
                Directory.CreateDirectory("output");

            var dir = IronPythonHelper.Import<string>("IN_DIR");
            if (args.Count() == 0)
            {
                var scripts = Directory.EnumerateFiles("scripts/", "*.py").ToList();
                for (int i = 0; i < scripts.Count; i++)
                {
                    Console.WriteLine("{0,3}: {1}", i + 1, scripts[i]);
                }
                int scriptIndex = -1;
                while (scriptIndex < 1 || scriptIndex > scripts.Count)
                {
                    Console.WriteLine("Enter a number to select a script!");
                    int.TryParse(Console.ReadLine(), out scriptIndex);
                }
                var filename = scripts[scriptIndex - 1];
                Console.WriteLine("Enter a 3 letter character code like RYU");
                var charname = Console.ReadLine();
                if (charname == "ALL")
                    RunScriptAll(dir, filename);
                else
                    RunScript(dir, filename, charname);
            }
            else if (args.Count() == 2)
            {
                if (args[1] == "ALL")
                    RunScriptAll(dir, "scripts/" + args[0]);
                else
                    RunScript(dir, "scripts/" + args[0], args[1]);
            }
            Console.ReadLine();
        }
        private static void RunScriptAll(string dir, string script)
        {
            var charnames = Directory.EnumerateDirectories(dir).ToList();
            File.Delete("output/" + Path.GetFileNameWithoutExtension(script) + ".html");
            foreach (string charname in charnames)
                RunScript(dir, script, Path.GetFileName(charname));

        }
        private static void RunScript(string dir, string script,string charname)
        {
            var str = Path.GetFileNameWithoutExtension(script);
            var ms = new MemoryStream();
            var sw = new StringWriter();
            sw.WriteLine("//RainbowScript output for " + str + " on " + charname);
            Console.WriteLine("running " + str + " on " + charname);
            //var oldout = Console.Out;
            IronPythonHelper.SetOut(ms, sw);
            var bcm = BCMFile.FromFilename(dir + "/" + charname + "/" + charname + ".bcm");
            var bac = BACFile.FromFilename(dir + "/" + charname + "/" + charname + ".bac", bcm);
            IronPythonHelper.Export("bcm", bcm);
            IronPythonHelper.Export("bac", bac);
            IronPythonHelper.Export("charName", charname);
            IronPythonHelper.RunFile(script);
            Directory.CreateDirectory("output/" + str + "/");

            File.WriteAllText("output/" + str + "/" + charname + ".html", sw.ToString());
            File.AppendAllText("output/" + str + ".html",sw.ToString());
            //Console.SetOut(oldout);
            //Console.ReadLine();
        }
    }
}
