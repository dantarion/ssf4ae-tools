using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Runtime;
using System.ComponentModel;
namespace RainbowLib
{
    public struct IndexReference
    {
        public override string ToString()
        {
            return Name;
        }
        public short Value { get; set; }
        public string Name
        {
            get;
            set;
        }
    }

    public static class ResourceManager
    {
        static Dictionary<string, Dictionary<short, IndexReference>> _data = new Dictionary<string, Dictionary<short, IndexReference>>();
        private static Dictionary<short, IndexReference> initDictionary()
        {
            var dict = new Dictionary<short, IndexReference>();
            var none = new IndexReference();
            none.Name = "NONE";
            none.Value = -1;
            dict[-1] = none;

            return dict;
        }
        private static void importIntoDictionary(Dictionary<short, IndexReference> dict, PythonDictionary o)
        {
            foreach (int key in o.Keys)
            {
                var ar = new IndexReference();
                ar.Value = (short)key;
                ar.Name = (string)o[key];
                dict[(short)key] = ar;
            }
        }
        private static void importIntoDictionary(Dictionary<short, IndexReference> dict, List o)
        {
            short key = 0;
            foreach (string item in o)
            {
                var ar = new IndexReference();
                ar.Value = key;
                ar.Name = item;
                dict[key++] = ar;
            }
        }
        public static void LoadCharacterData(string s)
        {
            string baseDir = "../../../python_scripts/out/";
            if (!System.IO.Directory.Exists(baseDir))
                baseDir = Application.StartupPath + "\\data\\";
            if (!System.IO.Directory.Exists(baseDir))
            {
              var result =  MessageBox.Show("Couldn't find data Directory!\nLocated at [ " + baseDir + " ]\n\nBrowse for folder manually?", "Ono! Edit",
                                MessageBoxButtons.YesNo);

              if (result == DialogResult.Yes)
              {
                  var ofd = new FolderBrowserDialog {Description = "Ono! Edit Data Directory"};

                  var res = ofd.ShowDialog();

                  if (res == DialogResult.OK) baseDir = ofd.SelectedPath;
              }
            }
            IronPythonHelper.Reset();
            _data["VFX"] = initDictionary();
            _data["VFX2"] = initDictionary();
            _data["OBJ"] = initDictionary();
            _data["FCE"] = initDictionary();
            _data["CAM"] = initDictionary();
            _data["UC1"] = initDictionary();
            _data["UC2"] = initDictionary();
            if (System.IO.File.Exists(baseDir + "global.py"))
            {
                IronPythonHelper.RunFile(baseDir + "global.py");

                importIntoDictionary(_data["VFX"], IronPythonHelper.Import<PythonDictionary>("VFX"));
                importIntoDictionary(_data["VFX2"], IronPythonHelper.Import<PythonDictionary>("VFX2"));
            }
            if (System.IO.File.Exists(baseDir + "" + s + ".py"))
            {
                IronPythonHelper.RunFile(baseDir + "" + s + ".py");
                importIntoDictionary(_data["VFX"], IronPythonHelper.Import<PythonDictionary>("VFX"));
                importIntoDictionary(_data["VFX2"], IronPythonHelper.Import<PythonDictionary>("VFX2"));
                importIntoDictionary(_data["OBJ"], IronPythonHelper.Import<List>("OBJ"));
                importIntoDictionary(_data["FCE"], IronPythonHelper.Import<List>("FCE"));
                importIntoDictionary(_data["CAM"], IronPythonHelper.Import<List>("CAM"));
                importIntoDictionary(_data["UC1"], IronPythonHelper.Import<List>("UC1"));
                importIntoDictionary(_data["UC2"], IronPythonHelper.Import<List>("UC2"));


            }
        }
        public static Dictionary<short, IndexReference> Load(string type)
        {
            if (_data.ContainsKey(type))
                return _data[type];
            return null;
        }
    }
}
