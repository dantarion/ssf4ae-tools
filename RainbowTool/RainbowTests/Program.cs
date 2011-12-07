using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RainbowLib;
using RainbowLib.BAC;
using RainbowLib.BCM;
namespace RainbowTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //rainbow();
            unitTests();
            Console.ReadLine();
            //
        }
        private static void rainbow()
        {
            foreach (string bcmfilename in Directory.EnumerateFiles(@"C:\Users\Eric\Desktop\BLECE\latest\", "*.bcm", SearchOption.AllDirectories))
            {
                //Console.WriteLine(bcmfilename);
                string charname = bcmfilename.Substring(bcmfilename.Length - 7, 3);
                Console.WriteLine(charname);
                var bcm = BCMFile.FromFilename(bcmfilename);
                var bac = BACFile.FromFilename(bcmfilename.Replace(".bcm", ".bac"), bcm);
                string targetbcm = @"C:\Program Files (x86)\Capcom\Super Street Fighter IV\dlc\03_character_free\battle\regulation\latest\" +
                    Path.GetFileNameWithoutExtension(bcmfilename) + "\\" + Path.GetFileNameWithoutExtension(bcmfilename) + ".bcm";
                string targetbac = targetbcm.Replace(".bcm", ".bac");
                foreach (Charge charge in bcm.Charges)
                    charge.Frames = 2;
                foreach (Script script in bac.Scripts)
                {
                    if (script.Name == "NONE")
                        continue;
                    foreach (HitboxCommand hitbox in script.CommandLists[(int)CommandListType.HITBOX])
                    {
                        //hitbox.Juggle = 1;
                        hitbox.JugglePotential = 20;
                        if (hitbox.Type == HitboxCommand.HitboxType.GRAB)
                            hitbox.Type = HitboxCommand.HitboxType.NORMAL;
                        if (hitbox.HitLevel == HitboxCommand.HitLevelType.AIR_ONLY)
                            hitbox.HitLevel = HitboxCommand.HitLevelType.MID;
                    }
                    foreach (SpeedCommand speed in script.CommandLists[(int)CommandListType.SPEED])
                    {
                        speed.Multiplier = (float)(speed.Multiplier * 1.7);
                    }
                    foreach (CancelCommand cancel in script.CommandLists[(int)CommandListType.CANCELS])
                    {
                        if(cancel.Type == CancelCommand.CancelType.ON_HIT)
                            cancel.Type = CancelCommand.CancelType.START;
                    }
                }
                if(!Directory.Exists(Path.GetDirectoryName(targetbcm)))
                    Directory.CreateDirectory(Path.GetDirectoryName(targetbcm));

                BCMFile.ToFilename(targetbcm,bcm);
                BACFile.ToFilename(targetbac, bac, bcm);
            }
        }
        private static void unitTests()
        {
            foreach (string bcmfilename in Directory.EnumerateFiles(@"C:\Users\Eric\Desktop\BLECE\latest\", "*.bcm", SearchOption.AllDirectories))
            {
                //Console.WriteLine(bcmfilename);
                string charname = bcmfilename.Substring(bcmfilename.Length - 7, 3);
                Console.Write(charname + "-BCM-");
                var bcm = BCMFile.FromFilename(bcmfilename);
                var bac = BACFile.FromFilename(bcmfilename.Replace(".bcm", ".bac"), bcm);
                BCMFile.ToFilename("tmp." + charname + ".bcm",bcm);
                BACFile.ToFilename("tmp." + charname + ".bac", bac, bcm);
                byte[] original = File.ReadAllBytes(bcmfilename);
                byte[] output = File.ReadAllBytes("tmp." + charname + ".bcm");
                if (original.Length != output.Length)
                {
                    Console.WriteLine("--------------------------REBUILD FAIL:These arent even the same size?");
                    Console.WriteLine("original: {0} rebuild: {1} diff: {2}", original.Length, output.Length, original.Length - output.Length);
                    continue;
                }
                //Console.WriteLine("Good, files are the same size");
                bool good = true;
                for (int i = 0; i < original.Length; i++)
                {
                    if (original[i] != output[i])
                    {
                        Console.WriteLine("-----------------------REBUILD FAIL:Difference at {0:X}", i);
                        good = false;
                        break;
                    }
                }
                if (good == true)
                    Console.WriteLine("----This files ok!");
                Console.Write(charname + "-BAC");
                original = File.ReadAllBytes(bcmfilename.Replace(".bcm", ".bac"));
                output = File.ReadAllBytes("tmp." + charname + ".bac");
                if (original.Length != output.Length)
                {
                    Console.WriteLine("--------------------------REBUILD FAIL:These arent even the same size?");
                    continue;
                }
                //Console.WriteLine("Good, files are the same size");
                good = true;
                for (int i = 0; i < original.Length; i++)
                {
                    if (original[i] != output[i])
                    {
                        Console.WriteLine("-----------------------REBUILD FAIL:Difference at {0:X}", i);
                        good = false;
                        break;
                    }
                }
                if (good == true)
                    Console.WriteLine("----This files ok!");
            }
        }
    }
}
