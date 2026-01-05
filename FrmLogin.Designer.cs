using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmLogin : BaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // CONTROLES
            this.PanelIzquierdo = new System.Windows.Forms.Panel();
            this.PicLogo = new System.Windows.Forms.PictureBox();
            this.LblBienvenida = new System.Windows.Forms.Label();
            this.LblSistema = new System.Windows.Forms.Label();

            this.PanelDerecho = new System.Windows.Forms.Panel();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.LblSubtitulo = new System.Windows.Forms.Label();

            this.LblUsuario = new System.Windows.Forms.Label();
            this.TxtUsuario = new System.Windows.Forms.TextBox();

            this.LblContrasena = new System.Windows.Forms.Label();
            this.TxtContrasena = new System.Windows.Forms.TextBox();
            this.ChkMostrarContrasena = new System.Windows.Forms.CheckBox();
            this.LnkOlvidasteContrasena = new System.Windows.Forms.LinkLabel();

            this.BtnIniciarSesion = new System.Windows.Forms.Button();
            this.LblRegistro = new System.Windows.Forms.Label();
            this.LnkRegistrarse = new System.Windows.Forms.LinkLabel();

            this.LblVersion = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            this.PanelIzquierdo.SuspendLayout();
            this.PanelDerecho.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // PANEL IZQUIERDO (LOGO Y BIENVENIDA)
            // =============================================
            this.PanelIzquierdo.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelIzquierdo.Size = new System.Drawing.Size(350, 500);
            this.PanelIzquierdo.Padding = new System.Windows.Forms.Padding(30);

            // ═════════════════════════════════════════════════════════
            // LOGO - ACTUALIZADO CON TU IMAGEN ConsejoLogo3
            // ═════════════════════════════════════════════════════════
            try
            {
                this.PicLogo.Image = Properties.Resources.ConsejoLogo3;
            }
            catch
            {
                // Si no se encuentra la imagen, continuará sin ella
            }
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicLogo.Location = new System.Drawing.Point(50, 50);
            this.PicLogo.Size = new System.Drawing.Size(250, 200);
            this.PicLogo.BackColor = System.Drawing.Color.Transparent;

            // Bienvenida
            this.LblBienvenida.Text = "Bienvenido al";
            this.LblBienvenida.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.LblBienvenida.ForeColor = System.Drawing.Color.White;
            this.LblBienvenida.Location = new System.Drawing.Point(50, 270);
            this.LblBienvenida.Size = new System.Drawing.Size(250, 30);
            this.LblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Sistema
            this.LblSistema.Text = "Control de Visitas";
            this.LblSistema.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblSistema.ForeColor = System.Drawing.Color.White;
            this.LblSistema.Location = new System.Drawing.Point(50, 300);
            this.LblSistema.Size = new System.Drawing.Size(250, 35);
            this.LblSistema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.PanelIzquierdo.Controls.Add(this.LblSistema);
            this.PanelIzquierdo.Controls.Add(this.LblBienvenida);
            this.PanelIzquierdo.Controls.Add(this.PicLogo);

            // =============================================
            // PANEL DERECHO (FORMULARIO)
            // =============================================
            this.PanelDerecho.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.PanelDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDerecho.Padding = new System.Windows.Forms.Padding(50, 80, 50, 50);

            // Título
            this.LblTitulo.Text = "Iniciar Sesión";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblTitulo.Location = new System.Drawing.Point(50, 80);
            this.LblTitulo.Size = new System.Drawing.Size(350, 40);

            // Subtítulo
            this.LblSubtitulo.Text = "Ingresa tus credenciales para continuar";
            this.LblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.LblSubtitulo.Location = new System.Drawing.Point(50, 125);
            this.LblSubtitulo.Size = new System.Drawing.Size(350, 20);

            // Label Usuario
            this.LblUsuario.Text = "Usuario";
            this.LblUsuario.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblUsuario.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblUsuario.Location = new System.Drawing.Point(50, 170);
            this.LblUsuario.Size = new System.Drawing.Size(350, 25);

            // TextBox Usuario
            this.TxtUsuario.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtUsuario.Location = new System.Drawing.Point(50, 200);
            this.TxtUsuario.Size = new System.Drawing.Size(350, 29);
            this.TxtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUsuario.MaxLength = 50;

            // Label Contraseña
            this.LblContrasena.Text = "Contraseña";
            this.LblContrasena.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblContrasena.Location = new System.Drawing.Point(50, 245);
            this.LblContrasena.Size = new System.Drawing.Size(350, 25);

            // TextBox Contraseña
            this.TxtContrasena.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtContrasena.Location = new System.Drawing.Point(50, 275);
            this.TxtContrasena.Size = new System.Drawing.Size(350, 29);
            this.TxtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContrasena.UseSystemPasswordChar = true;
            this.TxtContrasena.MaxLength = 100;
            this.TxtContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtContrasena_KeyPress);

            // CheckBox Mostrar Contraseña
            this.ChkMostrarContrasena.Text = "Mostrar contraseña";
            this.ChkMostrarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ChkMostrarContrasena.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.ChkMostrarContrasena.Location = new System.Drawing.Point(50, 315);
            this.ChkMostrarContrasena.Size = new System.Drawing.Size(150, 20);
            this.ChkMostrarContrasena.CheckedChanged += new System.EventHandler(this.ChkMostrarContrasena_CheckedChanged);

            // LinkLabel Olvidaste Contraseña
            this.LnkOlvidasteContrasena.Text = "¿Olvidaste tu contraseña?";
            this.LnkOlvidasteContrasena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LnkOlvidasteContrasena.LinkColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LnkOlvidasteContrasena.Location = new System.Drawing.Point(230, 315);
            this.LnkOlvidasteContrasena.Size = new System.Drawing.Size(170, 20);
            this.LnkOlvidasteContrasena.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LnkOlvidasteContrasena.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkOlvidasteContrasena_LinkClicked);

            // Botón Iniciar Sesión
            this.BtnIniciarSesion.Text = "🔐 INICIAR SESIÓN";
            this.BtnIniciarSesion.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.BtnIniciarSesion.ForeColor = System.Drawing.Color.White;
            this.BtnIniciarSesion.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnIniciarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnIniciarSesion.FlatAppearance.BorderSize = 0;
            this.BtnIniciarSesion.Location = new System.Drawing.Point(50, 360);
            this.BtnIniciarSesion.Size = new System.Drawing.Size(350, 50);
            this.BtnIniciarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnIniciarSesion.Click += new System.EventHandler(this.BtnIniciarSesion_Click);

            // Label ¿No tienes cuenta?
            this.LblRegistro.Text = "¿No tienes cuenta?";
            this.LblRegistro.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblRegistro.ForeColor = System.Drawing.Color.Gray;
            this.LblRegistro.Location = new System.Drawing.Point(115, 425);
            this.LblRegistro.Size = new System.Drawing.Size(120, 20);

            // LinkLabel Regístrate
            this.LnkRegistrarse.Text = "Regístrate aquí";
            this.LnkRegistrarse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LnkRegistrarse.LinkColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LnkRegistrarse.Location = new System.Drawing.Point(235, 425);
            this.LnkRegistrarse.Size = new System.Drawing.Size(100, 20);
            this.LnkRegistrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkRegistrarse_LinkClicked);

            // Label Versión
            this.LblVersion.Text = "Versión 1.0.0 - Consejo Ciudadano © 2025";
            this.LblVersion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.LblVersion.ForeColor = System.Drawing.Color.LightGray;
            this.LblVersion.Location = new System.Drawing.Point(50, 470);
            this.LblVersion.Size = new System.Drawing.Size(350, 20);
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.PanelDerecho.Controls.Add(this.LblVersion);
            this.PanelDerecho.Controls.Add(this.LnkRegistrarse);
            this.PanelDerecho.Controls.Add(this.LblRegistro);
            this.PanelDerecho.Controls.Add(this.BtnIniciarSesion);
            this.PanelDerecho.Controls.Add(this.LnkOlvidasteContrasena);
            this.PanelDerecho.Controls.Add(this.ChkMostrarContrasena);
            this.PanelDerecho.Controls.Add(this.TxtContrasena);
            this.PanelDerecho.Controls.Add(this.LblContrasena);
            this.PanelDerecho.Controls.Add(this.TxtUsuario);
            this.PanelDerecho.Controls.Add(this.LblUsuario);
            this.PanelDerecho.Controls.Add(this.LblSubtitulo);
            this.PanelDerecho.Controls.Add(this.LblTitulo);

            // =============================================
            // CONFIGURACIÓN DEL FORMULARIO
            // =============================================
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.PanelDerecho);
            this.Controls.Add(this.PanelIzquierdo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmLogin_Load);

            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            this.PanelIzquierdo.ResumeLayout(false);
            this.PanelDerecho.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelIzquierdo;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Label LblBienvenida;
        private System.Windows.Forms.Label LblSistema;

        private System.Windows.Forms.Panel PanelDerecho;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.Label LblSubtitulo;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label LblContrasena;
        private System.Windows.Forms.TextBox TxtContrasena;
        private System.Windows.Forms.CheckBox ChkMostrarContrasena;
        private System.Windows.Forms.LinkLabel LnkOlvidasteContrasena;
        private System.Windows.Forms.Button BtnIniciarSesion;
        private System.Windows.Forms.Label LblRegistro;
        private System.Windows.Forms.LinkLabel LnkRegistrarse;
        private System.Windows.Forms.Label LblVersion;
    }
}