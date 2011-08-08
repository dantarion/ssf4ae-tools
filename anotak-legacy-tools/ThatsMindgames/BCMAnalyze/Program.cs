using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AEDataLibrary;
using System.IO;

namespace BCMAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AELogger.Log("BCM Analyze, build " + AEDataTools.GetCompileDate(),false,true);
                if (args.Length <= 0)
                {
                    AELogger.Log("No file specified.");
                    Console.WriteLine("No file specified.");
                }
                else if (args.Length == 1)
                {
                    AELogger.Log(args[0] + " is target");
                    using (BinaryReader b = new BinaryReader(File.Open(args[0], FileMode.Open)))
                    {
                        BCMData bcm = new BCMData(b);

                        BCMAnalyze(bcm);
                    }
                }
                else
                {
                    AELogger.Log(args[0] + " is target");
                    using (BinaryReader b = new BinaryReader(File.Open(args[0], FileMode.Open)))
                    {
                        BCMData bcm = new BCMData(b);
                        if (args.Length == 2)
                        {
                            BCMAnalyzeRange(bcm, int.Parse(args[1]), int.Parse(args[1]));
                        }
                        else
                        {
                            BCMAnalyzeRange(bcm, int.Parse(args[1]), int.Parse(args[2]));
                        }
                    }
                }
                AELogger.WriteLog();
            } catch(Exception e)
            {
                AELogger.Log("Exception: " + e.Message);

                AELogger.Log("Exception: " + e.StackTrace);

                if (e.InnerException != null)
                {
                    AELogger.Log("InnerException: " + e.InnerException.ToString());
                }
                Console.WriteLine(e.Message);
                AELogger.WriteLog();
            }
        }

        private static void BCMAnalyze(BCMData bcm)
        {
            List<List<Byte>> byteList = new List<List<Byte>>();
            List<List<String>> stringList = new List<List<String>>();
            for (int i = 0; i < 84; i++)
            {
                byteList.Add(new List<Byte>());
                stringList.Add(new List<String>());
            }
            for (int inputIndex = 0; inputIndex < bcm.InputList.Count; inputIndex++)
            {
                for (int byteIndex = 0; byteIndex < bcm.InputList[inputIndex].data.Count; byteIndex++)
                {
                    int outByteIndex = byteList[byteIndex].IndexOf(bcm.InputList[inputIndex].data[byteIndex]);
                    //byteList[byteIndex].Contains(bcm.InputList[inputIndex].data[byteIndex])
                    if (outByteIndex == -1)
                    {
                        byteList[byteIndex].Add(bcm.InputList[inputIndex].data[byteIndex]);
                        stringList[byteIndex].Add(bcm.InputList[inputIndex].name);
                    }
                    else
                    {
                        stringList[byteIndex][outByteIndex] = stringList[byteIndex][outByteIndex] + ", " + bcm.InputList[inputIndex].name;
                    }
                }
            }

            for (int i = 0; i < 84; i++)
            {
                String output2 = "Byte " + i;
                Console.WriteLine(output2);
                AELogger.Log(output2, false);
                for(int j = 0; j < byteList[i].Count; j++)
                {
                    String output = "----" + byteList[i][j].ToString("X2") + " occurs in " + stringList[i][j];
                    Console.WriteLine(output);
                    AELogger.Log(output, false);
                }
            }
        }

        private static void BCMAnalyzeRange(BCMData bcm, int start, int end)
        {
            List<String> valueList = new List<String>();
            List<String> nameList = new List<String>();
            end++;
            if (end > 84)
            {
                end = 84;
            }
            if (start > end)
            {
                start = end - 1;
            }
            for (int inputIndex = 0; inputIndex < bcm.InputList.Count; inputIndex++)
            {
                String current = "";
                for (int byteIndex = start; byteIndex < end; byteIndex++)
                {
                    // = byteList[byteIndex].IndexOf(bcm.InputList[inputIndex].data[byteIndex]);
                    current += bcm.InputList[inputIndex].data[byteIndex].ToString("X2");
                }
                int outByteIndex = valueList.IndexOf(current);
                if (outByteIndex == -1)
                {
                    valueList.Add(current);
                    nameList.Add(bcm.InputList[inputIndex].name);
                }
                else
                {
                    nameList[outByteIndex] = nameList[outByteIndex] + ", " + bcm.InputList[inputIndex].name;
                }
            }

            
            String output2 = "Bytes " + start + " to " + (end-1);
            Console.WriteLine(output2);
            AELogger.Log(output2, false);
            for (int j = 0; j < valueList.Count; j++)
            {
                String output = "----" + valueList[j] + " occurs in " + nameList[j];
                Console.WriteLine(output);
                AELogger.Log(output, false);
            }
        }
    }
}
