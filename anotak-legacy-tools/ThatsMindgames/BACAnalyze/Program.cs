using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AEDataLibrary;
using System.IO;

namespace BACAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //AELogger.bPrintAll = true;
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
                        BACData bac = new BACData(b);
                        AnimAnalyze(bac);
                    }
                }
                else
                {
                    AELogger.Log(args[0] + " is target");
                    using (BinaryReader b = new BinaryReader(File.Open(args[0], FileMode.Open)))
                    {
                        BACData bac = new BACData(b);
                        if (args.Length == 2)
                        {
                            AnimAnalyzeRange(bac, int.Parse(args[1]), int.Parse(args[1]));
                        }
                        else
                        {
                            if (args[1].ToLower() == "i")
                            {
                                AnimAnalyzeIndividual(bac, int.Parse(args[2]));
                            }
                            else
                            {
                                AnimAnalyzeRange(bac, int.Parse(args[1]), int.Parse(args[2]));
                            }
                        }
                    }
                }
                AELogger.WriteLog();
            }
            catch (Exception e)
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

        private static void AnimAnalyzeIndividual(BACData bac, int p)
        {
            AnimAnalyzeIndividual(bac.GetAnimAt(p));
        }

        private static void AnimAnalyzeIndividual(BACData.BACAnim anim)
        {
            AELogger.bPrintAll = true;
            if (anim.bEmpty)
            {
                AELogger.Log("anim empty", false);
                return;
            }
            AELogger.Log("anim: " + anim.name, false);

            int ptr_chunk_list = (int)AEDataTools.UInt32FromData(anim.data.data, 20);
            // header size is usually 24 but we just gotta be safe

            String headerstring = "";
            for (int i = 0; i < ptr_chunk_list; i++)
            {
                headerstring += anim.data.data[i].ToString("X2");
            }
            AELogger.Log("header:", false);
            AELogger.Log(headerstring, false);

            int size_chunk_list = AEDataTools.UInt16FromData(anim.data.data, 18);
            AELogger.Log("ptr_chunk_list: " + ptr_chunk_list.ToString("X2"), false);
            AELogger.Log("size_chunk_list: " + size_chunk_list.ToString("X2"), false);

            int ptr_current = ptr_chunk_list;

            for (int i = 0; i < size_chunk_list; i++)
            {
                int cur_chunk = AEDataTools.UInt16FromData(anim.data.data, ptr_chunk_list + (i * 12));
                int cur_chunk_size = AEDataTools.UInt16FromData(anim.data.data, ptr_chunk_list + (i * 12) + 2);
                int cur_chunk_start = (int)AEDataTools.UInt32FromData(anim.data.data, ptr_chunk_list + (i * 12) + 4) + ptr_chunk_list + (i * 12);
                int cur_chunk_end = (int)AEDataTools.UInt32FromData(anim.data.data, ptr_chunk_list + (i * 12) + 8) + ptr_chunk_list + (i * 12);

                AELogger.Log((BACData.BACChunkType)cur_chunk + " " +
                    cur_chunk_size + " " + cur_chunk_start + " " + cur_chunk_end, false);
                
                for (int j = 0; j < cur_chunk_size; j++)
                {
                    AELogger.Log("frames: " + AEDataTools.UInt16FromData(anim.data.data, cur_chunk_start + j * 4).ToString() +
                        "-" + AEDataTools.UInt16FromData(anim.data.data, cur_chunk_start + j * 4 + 2).ToString(), false);
                }
            }

            headerstring = "";
            for (int i = 0; i < anim.data.data.Count; i++)
            {
                headerstring += anim.data.data[i];
            }
            AELogger.Log(headerstring, false);
            //AELogger.Log("num subanims: " + anim.data.data[ptrHeader + 2].ToString("X2"), false);
        }

        private static void AnimAnalyze(BACData bac)
        {
            List<List<Byte>> byteList = new List<List<Byte>>();
            List<List<String>> stringList = new List<List<String>>();
            Console.WriteLine("shortest and longest " + bac.ShortestAnim + " and " + bac.LongestAnim);
            AELogger.Log("shortest and longest " + bac.ShortestAnim + " and " + bac.LongestAnim, false);
            for (int i = 0; i < bac.LongestAnim; i++)
            {
                byteList.Add(new List<Byte>());
                stringList.Add(new List<String>());
            }
            for (int inputIndex = 0; inputIndex < bac.AnimList.Count; inputIndex++)
            {
                if (!bac.AnimList[inputIndex].bEmpty)
                {
                    for (int byteIndex = 0; byteIndex < bac.AnimList[inputIndex].data.Count; byteIndex++)
                    {
                        int outByteIndex = byteList[byteIndex].IndexOf(bac.AnimList[inputIndex].data.data[byteIndex]);
                        //byteList[byteIndex].Contains(bac.AnimList[inputIndex].data[byteIndex])
                        if (outByteIndex == -1)
                        {
                            byteList[byteIndex].Add(bac.AnimList[inputIndex].data.data[byteIndex]);
                            stringList[byteIndex].Add(bac.AnimList[inputIndex].name);
                        }
                        else
                        {
                            stringList[byteIndex][outByteIndex] = stringList[byteIndex][outByteIndex] + ", " + bac.AnimList[inputIndex].name;
                        }
                    }
                }
            }

            for (int i = 0; i < bac.LongestAnim; i++)
            {
                String output2 = "Byte " + i;
                Console.WriteLine(output2);
                AELogger.Log(output2, false);
                for (int j = 0; j < byteList[i].Count; j++)
                {
                    String output = "----" + byteList[i][j].ToString("X2") + " occurs in " + stringList[i][j];
                    Console.WriteLine(output);
                    AELogger.Log(output, false);
                }
            }
        }

        private static void AnimAnalyzeRange(BACData bac, int start, int end)
        {
            List<String> valueList = new List<String>();
            List<String> nameList = new List<String>();
            Console.WriteLine("shortest and longest " + bac.ShortestAnim + " and " + bac.LongestAnim);
            AELogger.Log("shortest and longest " + bac.ShortestAnim + " and " + bac.LongestAnim, false);
            end++;
            if (end > bac.LongestAnim)
            {
                end = bac.LongestAnim;
            }
            if (start > end)
            {
                start = end - 1;
            }
            for (int inputIndex = 0; inputIndex < bac.AnimList.Count; inputIndex++)
            {
                String current = "";
                if(!bac.AnimList[inputIndex].bEmpty)
                {
                    for (int byteIndex = start; byteIndex < end; byteIndex++)
                    {
                        // = byteList[byteIndex].IndexOf(bac.AnimList[inputIndex].data[byteIndex]);
                        current += bac.AnimList[inputIndex].data.data[byteIndex].ToString("X2");
                    }
                    int outByteIndex = valueList.IndexOf(current);
                    if (outByteIndex == -1)
                    {
                        valueList.Add(current);
                        nameList.Add(bac.AnimList[inputIndex].name);
                    }
                    else
                    {
                        nameList[outByteIndex] = nameList[outByteIndex] + ", " + bac.AnimList[inputIndex].name;
                    }
                }
            }


            String output2 = "Bytes " + start + " to " + (end - 1);
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
