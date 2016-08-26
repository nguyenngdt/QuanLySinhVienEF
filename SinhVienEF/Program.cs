using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SinhVienEF
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
            //Application.Run(new SinhVienQL());

            if (new Form1().ShowDialog() == DialogResult.OK)
            {
                Application.Run(new SinhVienMain());
            }
            
        }
    }
}
