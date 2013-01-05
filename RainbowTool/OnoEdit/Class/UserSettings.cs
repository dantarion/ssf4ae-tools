using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;

namespace OnoEdit
{

    #region Typedef

    [Serializable]
    internal class TypeSettings
    {

        public Point ThisLocation { get; set; }

        public Size ThisSize { get; set; }

        public bool IsMaximized { get; set; }

        public TypeSettings()
        {
            ThisLocation = new Point(0, 0);
            ThisSize = new Size(0, 0);
            IsMaximized = false;
        }

    }

    #endregion

    #region Base

    [Serializable]
    internal class Settings
    {
        public Dictionary<String, TypeSettings> WindowCollection { get; set; }

        public bool LearnLinkEnabled { get; set; }
        public bool RememberLastFile { get; set; }
        public bool CheckForUpdates  { get; set; }
        public bool ShowFriendlyNames { get; set; }
        public bool UseAeroScheme { get; set; }

        public String LastOpenedFile { get; set; }

        public Settings()
        {

            //Initial setting when no file saved

            WindowCollection = new Dictionary<string, TypeSettings>();
            LearnLinkEnabled = true;
            RememberLastFile = false;
            CheckForUpdates = false;
            LastOpenedFile = String.Empty;
            ShowFriendlyNames = true;
            UseAeroScheme = true;
        }
    }

    #endregion

    class UserSettings
    {
        #region Delegates
        public delegate void SettingsChanged(object sender, object oVar, Type pType); // __void(void &sender, const char[] *oVar, TypeDef &pType)
        #endregion

        #region IO

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

                var stream = File.Open(fileName, FileMode.Create);
                var bformat = new BinaryFormatter();

                bformat.Serialize(stream, serializableObject);
                stream.Close();

        }

        /// <summary>
        /// Desterilizes an binary file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            var objectOut = default(T);

                var stream = File.Open(fileName, FileMode.Open);
                var bformat = new BinaryFormatter();

                objectOut = (T)bformat.Deserialize(stream);
                stream.Close();

            return objectOut;
        }

        #endregion

        #region Const

        private const String OpsLocations = "{0}\\{1}\\UserSettings.bin";

        #endregion

        #region Declarations //SP?

        private static bool Hasloaded { get; set; }

        public static Settings CurrentSettings { get; set; }

        #endregion

        #region Events

        public static event SettingsChanged OnSettingsChanged;
        public static event EventHandler OnSettingsSaved;


        public static void RaiseEvent(Object sender, Object oVar, Type pType)
        {
            if (OnSettingsChanged != null)
                OnSettingsChanged(sender, oVar, pType);
        }

        #endregion

        #region Save

        public static void Save()
        {           
            try
            {
                var path = String.Format(OpsLocations, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetExecutingAssembly().GetName().Name);

                SerializeObject(CurrentSettings, path);

                if (OnSettingsSaved != null)
                    OnSettingsSaved(null, new EventArgs());

            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }

        }

        #endregion

        #region Load

        public static void Load()
        {
            if (Hasloaded) return; /*throw new InvalidOperationException("Settings File Already Loaded");*/ //Not sure if crash is the best way to go here

            try
            {
                var path = String.Format(OpsLocations, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetExecutingAssembly().GetName().Name);

                var opDirectory = Path.GetDirectoryName(path);

                Debug.Assert(opDirectory != null, "opDirectory != null"); //really? I doubt it ever will be, but meh
                if(!Directory.Exists(opDirectory))
                {
                    Directory.CreateDirectory(opDirectory); //I really am starting to enjoy auto-gc
                }

                CurrentSettings = DeSerializeObject<Settings>(path);
                Hasloaded = true;
            }
            catch(Exception er)
            {
                Console.WriteLine(er);
                CurrentSettings = new Settings();
            }

        }

        #endregion

    }
}
