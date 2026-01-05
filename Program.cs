using System;
using System.Windows.Forms;

namespace Visitas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // =============================================
            // INICIAR CON EL FORMULARIO DE LOGIN
            // =============================================
            // Antes era: Application.Run(new FrmMenu());
            // Ahora iniciamos con el login:
            Application.Run(new FrmLogin());
        }
    }
}

