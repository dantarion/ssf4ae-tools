// anotak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RainbowLib
{
    public class AELogger
    {
        static List<String> Logfile = new List<string>();

        public static bool bLogging = true;
        public static bool bPrintAll = false;

        public static void Log(string message, Boolean time = true, Boolean print = false)
        {
            if (bLogging)
            {
                if (time)
                {
                    Logfile.Add(DateTime.Now + ": " + message);
                    if (print || bPrintAll)
                    {
                        Console.WriteLine(DateTime.Now + ": " + message);
                    }
                }
                else
                {
                    Logfile.Add(message);
                    if (print || bPrintAll)
                    {
                        Console.WriteLine(message);
                    }
                }

            }
        }

        public static void WriteLog()
        {
            if (bLogging)
            {
                StreamWriter SW;
                SW = File.CreateText("Logfile.txt");
                SW.WriteLine("Anotak's logging system v1 adapted for Dantarion's RainbowLib");
                for (int i = 0; i < Logfile.Count; i++)
                {
                    SW.WriteLine(Logfile[i]);
                }
                SW.Close();
            }
        }
    }
}