using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace Willprint_Reservation_System
{
    internal static class Program
    {
        private static string filePath = "login.txt";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string fileContent = File.ReadAllText(filePath);
            if (!fileContent.Contains("true"))
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new main());
            }
        }
    }
}
