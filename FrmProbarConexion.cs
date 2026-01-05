using System;
using System.Windows.Forms;

namespace Visitas
{
    /// <summary>
    /// Formulario simple para probar la conexión a la base de datos
    /// </summary>
    public partial class FrmProbarConexion : Form
    {
        public FrmProbarConexion()
        {
            InitializeComponent();
        }

        private void FrmProbarConexion_Load(object sender, EventArgs e)
        {
            // Probar conexión automáticamente al cargar
            ProbarConexion();
        }

        private void BtnProbar_Click(object sender, EventArgs e)
        {
            ProbarConexion();
        }

        private void ProbarConexion()
        {
            if (ConexionDB.ProbarConexion())
            {
                MessageBox.Show(
                    "✅ CONEXIÓN EXITOSA\n\n" +
                    "La conexión a la base de datos 'ControlVisitas' funciona correctamente.",
                    "Conexión Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "❌ ERROR DE CONEXIÓN\n\n" +
                    "No se pudo conectar a la base de datos.\n\n" +
                    "Verifica:\n" +
                    "1. SQL Server está ejecutándose\n" +
                    "2. El nombre del servidor es correcto\n" +
                    "3. La base de datos 'ControlVisitas' existe",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}