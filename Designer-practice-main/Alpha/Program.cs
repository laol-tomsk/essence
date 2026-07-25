using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.WinForms;
using Alpha.Services;

namespace Alpha
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// TODO: Сделать шаблон для popupWindow
        /// TODO: singleton
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataStorageService.GetInstance();
            Application.Run(new MainForm());
        }
    }
}
