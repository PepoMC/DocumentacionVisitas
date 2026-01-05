namespace Visitas
{
    partial class FrmMenu : BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picEntrada;

        // Botones Originales
        private System.Windows.Forms.Button btnControlVisitas;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnEstadisticas;

        // NUEVOS BOTONES
        private System.Windows.Forms.Button btnAdministracion;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnCambiarContrasena;
        private System.Windows.Forms.Button btnCerrarSesion;

        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuarioSesion; // Para mostrar quién está conectado

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Colores del tema
            System.Drawing.Color colorFondoMenu = System.Drawing.Color.FromArgb(111, 18, 64);
            System.Drawing.Color colorHover = System.Drawing.Color.FromArgb(247, 148, 29);
            System.Drawing.Color colorFondoPanel = System.Drawing.Color.FromArgb(253, 246, 249);

            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();

            // Inicializar Botones
            this.btnControlVisitas = CrearBotonMenu("📋 Control de Visitas", colorHover);
            this.btnReportes = CrearBotonMenu("📊 Reportes", colorHover);
            this.btnEstadisticas = CrearBotonMenu("📈 Estadísticas", colorHover);
            this.btnAdministracion = CrearBotonMenu("⚙️ Administración", colorHover);
            this.btnHistorial = CrearBotonMenu("🕒 Historial Cambios", colorHover);
            this.btnCambiarContrasena = CrearBotonMenu("🔑 Cambiar Contraseña", colorHover);
            this.btnCerrarSesion = CrearBotonMenu("🚪 Cerrar Sesión", System.Drawing.Color.Red); // Rojo para salir

            this.panelDerecho = new System.Windows.Forms.Panel();
            System.Windows.Forms.Panel panelCentro = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.picEntrada = new System.Windows.Forms.PictureBox();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblUsuarioSesion = new System.Windows.Forms.Label();

            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = colorFondoMenu;
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Padding = new System.Windows.Forms.Padding(10);
            this.panelIzquierdo.Width = 260;

            // 
            // picLogo
            // 
            // NOTA: Si te da error aquí, asegúrate de tener la imagen en Resources o comenta esta línea
            try { this.picLogo.Image = Properties.Resources.LogoConsejo; } catch { }
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.Height = 140;
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.Padding = new System.Windows.Forms.Padding(10);

            // lblUsuarioSesion (Texto debajo del logo)
            this.lblUsuarioSesion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuarioSesion.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioSesion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUsuarioSesion.Height = 40;
            this.lblUsuarioSesion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblUsuarioSesion.Text = "Usuario: Cargando...";

            // Agregar controles al panel izquierdo (ORDEN INVERSO porque es Dock.Top)
            // Primero los de abajo, al final los de arriba
            this.panelIzquierdo.Controls.Add(this.btnCerrarSesion);
            this.panelIzquierdo.Controls.Add(this.btnCambiarContrasena);
            // Espaciador flexible o panel vacío si quisieras separar grupos
            this.panelIzquierdo.Controls.Add(this.btnHistorial);
            this.panelIzquierdo.Controls.Add(this.btnAdministracion);
            this.panelIzquierdo.Controls.Add(this.btnEstadisticas);
            this.panelIzquierdo.Controls.Add(this.btnReportes);
            this.panelIzquierdo.Controls.Add(this.btnControlVisitas);
            this.panelIzquierdo.Controls.Add(this.lblUsuarioSesion);
            this.panelIzquierdo.Controls.Add(this.picLogo);

            // 
            // panelDerecho
            // 
            this.panelDerecho.BackColor = colorFondoPanel;
            this.panelDerecho.Controls.Add(panelCentro);
            this.panelDerecho.Controls.Add(this.lblFooter);
            this.panelDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDerecho.Padding = new System.Windows.Forms.Padding(30);

            // 
            // panelCentro
            // 
            panelCentro.BackColor = System.Drawing.Color.Transparent;
            panelCentro.Controls.Add(this.picEntrada);
            panelCentro.Controls.Add(this.lblSubtitulo);
            panelCentro.Controls.Add(this.lblTitulo);
            panelCentro.Dock = System.Windows.Forms.DockStyle.Fill;

            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = colorFondoMenu;
            this.lblTitulo.Height = 80;
            this.lblTitulo.Text = "Sistema de Control de Visitantes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Height = 30;
            this.lblSubtitulo.Text = "Selecciona una opción del menú lateral";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            // 
            // picEntrada
            // 
            this.picEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            // NOTA: Si te da error aquí, asegúrate de tener la imagen en Resources o comenta esta línea
            try { this.picEntrada.Image = Properties.Resources.Entrada; } catch { }
            this.picEntrada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEntrada.Padding = new System.Windows.Forms.Padding(50);

            // 
            // lblFooter
            // 
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Height = 30;
            this.lblFooter.Text = "Consejo Ciudadano © 2025 | Sistema de Control de Accesos";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // FrmMenu
            // 
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.panelDerecho);
            this.Controls.Add(this.panelIzquierdo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Principal - Consejo Ciudadano";
        }

        // Método auxiliar para crear botones idénticos y ahorrar código
        private System.Windows.Forms.Button CrearBotonMenu(string texto, System.Drawing.Color colorHover)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            btn.Text = "  " + texto; // Espacio para el icono
            btn.Dock = System.Windows.Forms.DockStyle.Top;
            btn.Height = 55;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = colorHover;
            btn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            return btn;
        }
    }
}






