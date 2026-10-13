using System;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Mainform khi mở ứng dụng
            Application.Run(new Mainform());
        }
    }
}