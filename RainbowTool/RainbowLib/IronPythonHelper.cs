using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
namespace RainbowLib
{
    public class IronPythonHelper
    {
        private static ScriptEngine m_engine = Python.CreateEngine();
        private static ScriptScope m_scope = null;
        public static void Init()
        {
            m_scope = m_engine.CreateScope();
        }
        public static void Reset()
        {
            Init();
        }
        public static void SetOut(System.IO.Stream stream,System.IO.TextWriter tw)
        {
            m_engine.Runtime.IO.SetOutput(stream, tw); ;
        }
        public static void RunFile(string path)
        {
            try
            {
                m_engine.CreateScriptSourceFromFile(path).Execute(m_scope);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        public static void Export(string name, object obj)
        {
            m_scope.SetVariable(name, obj);
        }
        public static T Import<T>(string name)
        {
            return (T)m_scope.GetVariable(name);
        }
    }
}
