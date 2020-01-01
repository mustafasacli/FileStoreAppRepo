using FileStoreApp.Source.Views;
using System;
using System.Windows.Forms;

namespace FileStoreApp
{
    internal static class Program
    {
        public static bool isLogin = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
            if (isLogin)
            {
                Application.Run(new FrmFileList());
            }
        }
    }
}