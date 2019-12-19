using System;
using System.Linq;
using System.Windows.Forms;
using Fintrak.Presentation.Desktop.UI;

namespace Fintrak.Presentation.Desktop
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
            Application.Run(new MainForm());
        }
    }
}