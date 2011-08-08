using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AEDataLibrary;

namespace YomiLayer7
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException); 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new YLForm());
            }
            catch (Exception e)
            {
                Houston(e);
            }
        }

        public static void Houston(Exception e)
        {
            AELogger.Log("Exception: " + e.Message);

            AELogger.Log("Exception: " + e.StackTrace);

            if (e.InnerException != null)
            {
                AELogger.Log("InnerException: " + e.InnerException.ToString());
            }
            MessageBox.Show(e.Message, "Exception!", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            AELogger.WriteLog();
            Application.Exit();
        }

        public class ThreadExceptionHandler
        {

            public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            {
                AELogger.Log("Exception: " + e.Exception.Message);

                AELogger.Log("Exception: " + e.Exception.StackTrace);

                if (e.Exception.InnerException != null)
                {
                    AELogger.Log("InnerException: " + e.Exception.InnerException.ToString());
                }
                MessageBox.Show(e.Exception.Message, "Exception!", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                AELogger.WriteLog();
                Application.Exit();
            }

        }
    }
}
