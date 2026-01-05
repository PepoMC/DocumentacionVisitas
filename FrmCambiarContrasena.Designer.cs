using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmCambiarContrasena : BaseForm
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
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.LblSubtitulo = new System.Windows.Forms.Label();

            this.PanelContenido = new System.Windows.Forms.Panel();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.LblAdvertencia = new System.Windows.Forms.Label();

            this.LblContrasenaActual = new System.Windows.Forms.Label();
            this.TxtContrasenaActual = new System.Windows.Forms.TextBox();

            this.LblNuevaContrasena = new System.Windows.Forms.Label();
            this.TxtNuevaContrasena = new System.Windows.Forms.TextBox();

            this.LblFortaleza = new System.Windows.Forms.Label();
            this.ProgressFortaleza = new System.Windows.Forms.ProgressBar();

            this.LblConfirmarContrasena = new System.Windows.Forms.Label();
            this.TxtConfirmarContrasena = new System.Windows.Forms.TextBox();

            this.ChkMostrarContrasenas = new System.Windows.Forms.CheckBox();

            this.LblRequisitos = new System.Windows.Forms.Label();

            this.BtnCambiar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();

            this.PanelSuperior.SuspendLayout();
            this.PanelContenido.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // PANEL SUPERIOR (NUEVO)
            // =============================================
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(500, 90);
            this.PanelSuperior.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);

            // Título
            this.LblTitulo.Text = "Cambiar Contraseña";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(30, 20);
            this.LblTitulo.Size = new System.Drawing.Size(440, 35);

            // Subtítulo
            this.LblSubtitulo.Text = "Actualiza tu contraseña de acceso";
            this.LblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.LblSubtitulo.Location = new System.Drawing.Point(30, 55);
            this.LblSubtitulo.Size = new System.Drawing.Size(440, 20);

            this.PanelSuperior.Controls.Add(this.LblSubtitulo);
            this.PanelSuperior.Controls.Add(this.LblTitulo);

            // =============================================
            // PANEL CONTENIDO
            // =============================================
            this.PanelContenido.BackColor = System.Drawing.Color.White;
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(30);

            // Usuario
            this.LblUsuario.Text = "Usuario: Cargando...";
            this.LblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblUsuario.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblUsuario.Location = new System.Drawing.Point(30, 15);
            this.LblUsuario.Size = new System.Drawing.Size(440, 20);

            // Advertencia (solo visible si es obligatorio)
            this.LblAdvertencia.Text = "⚠️ CAMBIO OBLIGATORIO";
            this.LblAdvertencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblAdvertencia.ForeColor = System.Drawing.Color.Red;
            this.LblAdvertencia.BackColor = System.Drawing.Color.FromArgb(255, 235, 235);
            this.LblAdvertencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblAdvertencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblAdvertencia.Location = new System.Drawing.Point(30, 45);
            this.LblAdvertencia.Size = new System.Drawing.Size(440, 40);
            this.LblAdvertencia.Visible = false;

            // =============================================
            // CONTRASEÑA ACTUAL
            // =============================================
            this.LblContrasenaActual.Text = "Contraseña Actual:";
            this.LblContrasenaActual.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblContrasenaActual.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblContrasenaActual.Location = new System.Drawing.Point(30, 100);
            this.LblContrasenaActual.Size = new System.Drawing.Size(440, 25);

            this.TxtContrasenaActual.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtContrasenaActual.Location = new System.Drawing.Point(30, 130);
            this.TxtContrasenaActual.Size = new System.Drawing.Size(350, 27);
            this.TxtContrasenaActual.UseSystemPasswordChar = true;
            this.TxtContrasenaActual.MaxLength = 100;
            this.TxtContrasenaActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContrasenaActual.BackColor = System.Drawing.Color.White;

            // =============================================
            // NUEVA CONTRASEÑA
            // =============================================
            this.LblNuevaContrasena.Text = "Nueva Contraseña:";
            this.LblNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblNuevaContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNuevaContrasena.Location = new System.Drawing.Point(30, 180);
            this.LblNuevaContrasena.Size = new System.Drawing.Size(440, 25);

            this.TxtNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtNuevaContrasena.Location = new System.Drawing.Point(30, 210);
            this.TxtNuevaContrasena.Size = new System.Drawing.Size(350, 27);
            this.TxtNuevaContrasena.UseSystemPasswordChar = true;
            this.TxtNuevaContrasena.MaxLength = 100;
            this.TxtNuevaContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNuevaContrasena.BackColor = System.Drawing.Color.White;
            this.TxtNuevaContrasena.TextChanged += new System.EventHandler(this.TxtNuevaContrasena_TextChanged);

            // Indicador de Fortaleza
            this.LblFortaleza.Text = "Sin contraseña";
            this.LblFortaleza.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblFortaleza.ForeColor = System.Drawing.Color.Gray;
            this.LblFortaleza.Location = new System.Drawing.Point(30, 245);
            this.LblFortaleza.Size = new System.Drawing.Size(200, 20);

            this.ProgressFortaleza.Location = new System.Drawing.Point(30, 270);
            this.ProgressFortaleza.Size = new System.Drawing.Size(350, 12);
            this.ProgressFortaleza.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            // =============================================
            // CONFIRMAR CONTRASEÑA
            // =============================================
            this.LblConfirmarContrasena.Text = "Confirmar Nueva Contraseña:";
            this.LblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblConfirmarContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblConfirmarContrasena.Location = new System.Drawing.Point(30, 300);
            this.LblConfirmarContrasena.Size = new System.Drawing.Size(440, 25);

            this.TxtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtConfirmarContrasena.Location = new System.Drawing.Point(30, 330);
            this.TxtConfirmarContrasena.Size = new System.Drawing.Size(350, 27);
            this.TxtConfirmarContrasena.UseSystemPasswordChar = true;
            this.TxtConfirmarContrasena.MaxLength = 100;
            this.TxtConfirmarContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConfirmarContrasena.BackColor = System.Drawing.Color.White;

            // =============================================
            // CHECKBOX MOSTRAR
            // =============================================
            this.ChkMostrarContrasenas.Text = "Mostrar contraseñas";
            this.ChkMostrarContrasenas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ChkMostrarContrasenas.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.ChkMostrarContrasenas.Location = new System.Drawing.Point(30, 370);
            this.ChkMostrarContrasenas.Size = new System.Drawing.Size(200, 20);
            this.ChkMostrarContrasenas.CheckedChanged += new System.EventHandler(this.ChkMostrarContrasenas_CheckedChanged);

            // =============================================
            // REQUISITOS (FORMATO LISTA - MANTENER)
            // =============================================
            this.LblRequisitos.Text =
                "📋 Requisitos de la nueva contraseña:\n" +
                "   • Mínimo 8 caracteres\n" +
                "   • Al menos una letra MAYÚSCULA\n" +
                "   • Al menos una letra minúscula\n" +
                "   • Al menos un número (0-9)\n" +
                "   • Al menos un carácter especial (!@#$%^&*...)";
            this.LblRequisitos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblRequisitos.ForeColor = System.Drawing.Color.Gray;
            this.LblRequisitos.Location = new System.Drawing.Point(30, 405);
            this.LblRequisitos.Size = new System.Drawing.Size(440, 100);

            // =============================================
            // BOTONES
            // =============================================
            this.BtnCambiar.Text = "✅ Cambiar Contraseña";
            this.BtnCambiar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnCambiar.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnCambiar.ForeColor = System.Drawing.Color.White;
            this.BtnCambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCambiar.FlatAppearance.BorderSize = 0;
            this.BtnCambiar.Location = new System.Drawing.Point(30, 520);
            this.BtnCambiar.Size = new System.Drawing.Size(210, 50);
            this.BtnCambiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCambiar.Click += new System.EventHandler(this.BtnCambiar_Click);

            this.BtnCancelar.Text = "❌ Cancelar";
            this.BtnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnCancelar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.BtnCancelar.ForeColor = System.Drawing.Color.White;
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.Location = new System.Drawing.Point(260, 520);
            this.BtnCancelar.Size = new System.Drawing.Size(210, 50);
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            this.PanelContenido.Controls.Add(this.BtnCancelar);
            this.PanelContenido.Controls.Add(this.BtnCambiar);
            this.PanelContenido.Controls.Add(this.LblRequisitos);
            this.PanelContenido.Controls.Add(this.ChkMostrarContrasenas);
            this.PanelContenido.Controls.Add(this.TxtConfirmarContrasena);
            this.PanelContenido.Controls.Add(this.LblConfirmarContrasena);
            this.PanelContenido.Controls.Add(this.ProgressFortaleza);
            this.PanelContenido.Controls.Add(this.LblFortaleza);
            this.PanelContenido.Controls.Add(this.TxtNuevaContrasena);
            this.PanelContenido.Controls.Add(this.LblNuevaContrasena);
            this.PanelContenido.Controls.Add(this.TxtContrasenaActual);
            this.PanelContenido.Controls.Add(this.LblContrasenaActual);
            this.PanelContenido.Controls.Add(this.LblAdvertencia);
            this.PanelContenido.Controls.Add(this.LblUsuario);

            // =============================================
            // CONFIGURACIÓN DEL FORMULARIO
            // =============================================
            this.ClientSize = new System.Drawing.Size(500, 670);
            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Contraseña - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmCambiarContrasena_Load);

            this.PanelSuperior.ResumeLayout(false);
            this.PanelContenido.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.Label LblSubtitulo;

        private System.Windows.Forms.Panel PanelContenido;

        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.Label LblAdvertencia;
        private System.Windows.Forms.Label LblContrasenaActual;
        private System.Windows.Forms.TextBox TxtContrasenaActual;
        private System.Windows.Forms.Label LblNuevaContrasena;
        private System.Windows.Forms.TextBox TxtNuevaContrasena;
        private System.Windows.Forms.Label LblFortaleza;
        private System.Windows.Forms.ProgressBar ProgressFortaleza;
        private System.Windows.Forms.Label LblConfirmarContrasena;
        private System.Windows.Forms.TextBox TxtConfirmarContrasena;
        private System.Windows.Forms.CheckBox ChkMostrarContrasenas;
        private System.Windows.Forms.Label LblRequisitos;
        private System.Windows.Forms.Button BtnCambiar;
        private System.Windows.Forms.Button BtnCancelar;
    }
}