using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TamizajeApp
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
            Application.Run(new Login());
            //Application.Run(new Main());
            //Application.Run(new ValidarResultados());
            //Application.Run(new Principal(usuarioIniciado));
        }
    }
}
