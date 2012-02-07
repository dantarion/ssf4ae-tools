// anotak
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace RainbowLib
{
    public class AELogger
    {
        public static AELogger Logger = new AELogger();
        public const String O_SEPARATOR = "-------------------------";
        public ObservableCollection<LogString> _Logfile;
        public ObservableCollection<LogString> Logfile
        {
            get { return _Logfile; }
        }

        public static bool bLogging = true;
        public static bool bPrintAll = false;

        public AELogger()
        {
            _Logfile = new ObservableCollection<LogString>();
        }

        public static void Log(string message, Boolean time = true, Boolean print = false)
        {
            Logger._Log(message, time, print);
        }

        public void _Log(string message, Boolean time = true, Boolean print = false)
        {
            if (bLogging)
            {
                if (time)
                {
                    Logfile.Add(new LogString(DateTime.Now + ": " + message));
                    if (print || bPrintAll)
                    {
                        Console.WriteLine(DateTime.Now + ": " + message);
                    }
                }
                else
                {
                    Logfile.Add(new LogString(message));
                    if (print || bPrintAll)
                    {
                        Console.WriteLine(message);
                    }
                }
                
            }
        }

        public static void WriteLog()
        {
            Logger._WriteLog();
        }

        public void _WriteLog()
        {
            if (bLogging)
            {
                StreamWriter SW;
                SW = File.CreateText("Logfile.txt");
                SW.WriteLine("Anotak's logging system v1 adapted for Dantarion's RainbowLib");
                for (int i = 0; i < _Logfile.Count; i++)
                {
                    SW.WriteLine(_Logfile[i]);
                }
                SW.Close();
            }
        }
    }


    public class LogString : INotifyPropertyChanged
    {
        public LogString()
            : this("NEW")
        {
        }

        public LogString(string input = null)
        {
            Value = input;
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
                AELogger.Log(Name);
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}