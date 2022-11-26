using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace NetworkOnlineMonitor
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            StaticTools.AllowOnlyOneInstance();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new XXXForm1());  //For testing/debugging fragments
            Application.Run(new FormMain());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(null, e.ExceptionObject?.ToString()??"Unknown", "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
