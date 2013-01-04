using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnoEdit.Class
{
   public class CharNames
   {

       private static readonly Dictionary<String, String> Namesinternal = new Dictionary<String, String>();

       public static void LoadDictionary()
       {
           Namesinternal.Add("ADN", @"Adon");
           Namesinternal.Add("AGL", @"C. Viper");
           Namesinternal.Add("BLK", @"Blanka");
           Namesinternal.Add("BLR", @"Vega (Claw)");
           Namesinternal.Add("BOS", @"Seth");
           Namesinternal.Add("BSN", @"Balrog (Boxer)");
           Namesinternal.Add("CDY", @"Cody");
           Namesinternal.Add("CHB", @"Rufus");
           Namesinternal.Add("CMY", @"Cammy");
           Namesinternal.Add("CNL", @"Chun-Li");
           Namesinternal.Add("DAN", @"Dan");
           Namesinternal.Add("DDL", @"Dudley");
           Namesinternal.Add("DJY", @"Dee Jay");
           Namesinternal.Add("DSM", @"Dhalsim");
           Namesinternal.Add("FLN", @"Fei Long");
           Namesinternal.Add("GKN", @"Gouken");
           Namesinternal.Add("GKI", @"Akuma (Gouki)");
           Namesinternal.Add("GKX", @"Oni");
           Namesinternal.Add("GUL", @"Guile");
           Namesinternal.Add("GEN", @"Gen");
           Namesinternal.Add("GUY", @"Guy");
           Namesinternal.Add("HKN", @"Hakan");
           Namesinternal.Add("HND", @"E. Honda");
           Namesinternal.Add("HWK", @"T. Hawk");
           Namesinternal.Add("IBK", @"Ibuki");
           Namesinternal.Add("JHA", @"Abel");
           Namesinternal.Add("JRI", @"Juri");
           Namesinternal.Add("KEN", @"Ken");
           Namesinternal.Add("MKT", @"Makoto");
           Namesinternal.Add("RIC", @"El Fuerte");
           Namesinternal.Add("ROS", @"Rose");
           Namesinternal.Add("RYU", @"Ryu");
           Namesinternal.Add("RYX", @"Evil Ryu");
           Namesinternal.Add("SGT", @"Sagat");
           Namesinternal.Add("SKR", @"Sakura");
           Namesinternal.Add("VEG", @"M. Bison (Dictator)");
           Namesinternal.Add("YAN", @"Yang");
           Namesinternal.Add("YUN", @"Yun");
           Namesinternal.Add("ZGF", @"Zangief");
       }

       public static String GetName(String subname)
       {
           return !Namesinternal.ContainsKey(subname) ? subname : Namesinternal[subname];
       }
   }
}
