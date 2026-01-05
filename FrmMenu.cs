using System;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmMenu : BaseForm
    {
        public FrmMenu()
        {
            InitializeComponent();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            // Asignar eventos Click
            this.Load += FrmMenu_Load;
            btnControlVisitas.Click += (s, e) => AbrirFormulario(new FrmRegistrarVisita());
            btnReportes.Click += (s, e) => AbrirFormulario(new FrmReportes());
            btnEstadisticas.Click += (s, e) => AbrirFormulario(new FrmEstadisticasVisitantes());

            // Nuevos formularios - SOLO CAMBIADO FrmAdministracion
            btnAdministracion.Click += (s, e) => AbrirFormulario(new FrmAdministracion(SesionActual.NombreUsuario, SesionActual.IdUsuario));
            btnHistorial.Click += (s, e) => AbrirFormulario(new FrmHistorialCambios());
            btnCambiarContrasena.Click += (s, e) => AbrirFormulario(new FrmCambiarContrasena());

            btnCerrarSesion.Click += BtnCerrarSesion_Click;
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            // Mostrar info del usuario en el menú lateral
            if (SesionActual.SesionActiva)
            {
                lblUsuarioSesion.Text = $"Hola, {SesionActual.NombreUsuario}\n({SesionActual.Rol})";

                // SEGURIDAD: Ocultar botones si NO es Administrador
                if (!SesionActual.EsAdministrador())
                {
                    btnAdministracion.Visible = false;
                    btnHistorial.Visible = false;
                    // btnReportes.Visible = false; // Descomenta si los reportes son solo para admin
                }
            }
        }

        /// <summary>
        /// Método genérico para abrir cualquier formulario de manera segura
        /// </summary>
        private void AbrirFormulario(Form formulario)
        {
            try
            {
                this.Hide();
                formulario.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario:\n\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Asegurar que el menú vuelva a aparecer al cerrar el otro form
                this.Show();

                // Si cambiaron la contraseña y cerraron sesión desde el otro form, cerramos este menú
                if (!SesionActual.SesionActiva)
                {
                    this.Close();
                }
            }
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Deseas cerrar tu sesión actual?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                SesionActual.CerrarSesion();
                this.Close(); // Esto cerrará el menú y debería volver al Login si el Login fue quien abrió el menú

                // NOTA: Si la aplicación se cierra completa en lugar de ir al login,
                // necesitarás instanciar FrmLogin aquí:
                // Application.Restart(); 
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Si el usuario cierra con la X, confirmar salida completa
            if (e.CloseReason == CloseReason.UserClosing && SesionActual.SesionActiva)
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Estás seguro de que deseas salir del sistema?",
                    "Confirmar Salida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
            base.OnFormClosing(e);
        }
    }
}