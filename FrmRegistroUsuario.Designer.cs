using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmRegistroUsuario : BaseForm
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

            // Código de Activación
            this.GroupCodigo = new System.Windows.Forms.GroupBox();
            this.LblCodigoActivacion = new System.Windows.Forms.Label();
            this.TxtCodigoActivacion = new System.Windows.Forms.TextBox();
            this.BtnValidarCodigo = new System.Windows.Forms.Button();
            this.LblInfoCodigo = new System.Windows.Forms.Label();

            // Panel de Datos del Usuario
            this.PanelDatosUsuario = new System.Windows.Forms.Panel();

            this.LblNombreUsuario = new System.Windows.Forms.Label();
            this.TxtNombreUsuario = new System.Windows.Forms.TextBox();

            this.LblNombreCompleto = new System.Windows.Forms.Label();
            this.TxtNombreCompleto = new System.Windows.Forms.TextBox();

            this.LblContrasena = new System.Windows.Forms.Label();
            this.TxtContrasena = new System.Windows.Forms.TextBox();

            this.LblFortaleza = new System.Windows.Forms.Label();
            this.ProgressFortaleza = new System.Windows.Forms.ProgressBar();

            this.LblConfirmarContrasena = new System.Windows.Forms.Label();
            this.TxtConfirmarContrasena = new System.Windows.Forms.TextBox();

            this.ChkMostrarContrasenas = new System.Windows.Forms.CheckBox();

            this.LblPreguntaSeguridad = new System.Windows.Forms.Label();
            this.TxtPreguntaSeguridad = new System.Windows.Forms.TextBox();

            this.LblRespuestaSeguridad = new System.Windows.Forms.Label();
            this.TxtRespuestaSeguridad = new System.Windows.Forms.TextBox();

            this.LblRequisitos = new System.Windows.Forms.Label();

            this.BtnRegistrar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();

            this.PanelSuperior.SuspendLayout();
            this.PanelContenido.SuspendLayout();
            this.GroupCodigo.SuspendLayout();
            this.PanelDatosUsuario.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // PANEL SUPERIOR
            // =============================================
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(640, 70);
            this.PanelSuperior.Padding = new System.Windows.Forms.Padding(20);

            // Título
            this.LblTitulo.Text = "Registro de Nuevo Usuario";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(20, 12);
            this.LblTitulo.Size = new System.Drawing.Size(600, 32);

            // Subtítulo
            this.LblSubtitulo.Text = "Completa los siguientes datos para crear tu cuenta";
            this.LblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.LblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.LblSubtitulo.Location = new System.Drawing.Point(20, 44);
            this.LblSubtitulo.Size = new System.Drawing.Size(600, 20);

            this.PanelSuperior.Controls.Add(this.LblSubtitulo);
            this.PanelSuperior.Controls.Add(this.LblTitulo);

            // =============================================
            // PANEL CONTENIDO
            // =============================================
            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.PanelContenido.AutoScroll = true;

            // =============================================
            // GRUPO: CÓDIGO DE ACTIVACIÓN
            // =============================================
            this.GroupCodigo.Text = "Paso 1: Código de Activación";
            this.GroupCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GroupCodigo.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GroupCodigo.Location = new System.Drawing.Point(30, 10);
            this.GroupCodigo.Size = new System.Drawing.Size(580, 90);
            this.GroupCodigo.Padding = new System.Windows.Forms.Padding(12);

            this.LblCodigoActivacion.Text = "Ingresa tu código de activación:";
            this.LblCodigoActivacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblCodigoActivacion.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblCodigoActivacion.Location = new System.Drawing.Point(12, 25);
            this.LblCodigoActivacion.Size = new System.Drawing.Size(555, 18);

            this.TxtCodigoActivacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtCodigoActivacion.Location = new System.Drawing.Point(12, 47);
            this.TxtCodigoActivacion.Size = new System.Drawing.Size(555, 25);
            this.TxtCodigoActivacion.MaxLength = 50;
            this.TxtCodigoActivacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCodigoActivacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.BtnValidarCodigo.Text = "✓ Validar";
            this.BtnValidarCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnValidarCodigo.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnValidarCodigo.ForeColor = System.Drawing.Color.White;
            this.BtnValidarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnValidarCodigo.FlatAppearance.BorderSize = 0;
            this.BtnValidarCodigo.Location = new System.Drawing.Point(330, 55);
            this.BtnValidarCodigo.Size = new System.Drawing.Size(100, 27);
            this.BtnValidarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnValidarCodigo.Visible = false;
            this.BtnValidarCodigo.Click += new System.EventHandler(this.BtnValidarCodigo_Click);

            this.LblInfoCodigo.Text = "ℹ️ Solicita un código al administrador del sistema";
            this.LblInfoCodigo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.LblInfoCodigo.ForeColor = System.Drawing.Color.Gray;
            this.LblInfoCodigo.Location = new System.Drawing.Point(20, 90);
            this.LblInfoCodigo.Size = new System.Drawing.Size(500, 20);
            this.LblInfoCodigo.Visible = false;

            this.GroupCodigo.Controls.Add(this.LblInfoCodigo);
            this.GroupCodigo.Controls.Add(this.BtnValidarCodigo);
            this.GroupCodigo.Controls.Add(this.TxtCodigoActivacion);
            this.GroupCodigo.Controls.Add(this.LblCodigoActivacion);

            // =============================================
            // PANEL: DATOS DEL USUARIO
            // =============================================
            this.PanelDatosUsuario.Location = new System.Drawing.Point(30, 110);
            this.PanelDatosUsuario.Size = new System.Drawing.Size(580, 440);
            this.PanelDatosUsuario.Enabled = true;
            this.PanelDatosUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDatosUsuario.BackColor = System.Drawing.Color.White;
            this.PanelDatosUsuario.Padding = new System.Windows.Forms.Padding(12);

            // Nombre de Usuario
            this.LblNombreUsuario.Text = "Nombre de Usuario:";
            this.LblNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNombreUsuario.Location = new System.Drawing.Point(12, 12);
            this.LblNombreUsuario.Size = new System.Drawing.Size(550, 18);

            this.TxtNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtNombreUsuario.Location = new System.Drawing.Point(12, 34);
            this.TxtNombreUsuario.Size = new System.Drawing.Size(550, 25);
            this.TxtNombreUsuario.MaxLength = 50;
            this.TxtNombreUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Nombre Completo
            this.LblNombreCompleto.Text = "Nombre Completo:";
            this.LblNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNombreCompleto.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNombreCompleto.Location = new System.Drawing.Point(12, 68);
            this.LblNombreCompleto.Size = new System.Drawing.Size(550, 18);

            this.TxtNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtNombreCompleto.Location = new System.Drawing.Point(12, 90);
            this.TxtNombreCompleto.Size = new System.Drawing.Size(550, 25);
            this.TxtNombreCompleto.MaxLength = 100;
            this.TxtNombreCompleto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Contraseña
            this.LblContrasena.Text = "Contraseña:";
            this.LblContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblContrasena.Location = new System.Drawing.Point(12, 124);
            this.LblContrasena.Size = new System.Drawing.Size(550, 18);

            this.TxtContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtContrasena.Location = new System.Drawing.Point(12, 146);
            this.TxtContrasena.Size = new System.Drawing.Size(550, 25);
            this.TxtContrasena.UseSystemPasswordChar = true;
            this.TxtContrasena.MaxLength = 100;
            this.TxtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContrasena.TextChanged += new System.EventHandler(this.TxtContrasena_TextChanged);

            // ProgressBar + Label Fortaleza
            this.ProgressFortaleza.Location = new System.Drawing.Point(12, 176);
            this.ProgressFortaleza.Size = new System.Drawing.Size(400, 16);
            this.ProgressFortaleza.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            this.LblFortaleza.Text = "Sin contraseña";
            this.LblFortaleza.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.LblFortaleza.ForeColor = System.Drawing.Color.Gray;
            this.LblFortaleza.Location = new System.Drawing.Point(420, 176);
            this.LblFortaleza.Size = new System.Drawing.Size(142, 16);

            // Confirmar Contraseña
            this.LblConfirmarContrasena.Text = "Confirmar Contraseña:";
            this.LblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblConfirmarContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblConfirmarContrasena.Location = new System.Drawing.Point(12, 200);
            this.LblConfirmarContrasena.Size = new System.Drawing.Size(550, 18);

            this.TxtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtConfirmarContrasena.Location = new System.Drawing.Point(12, 222);
            this.TxtConfirmarContrasena.Size = new System.Drawing.Size(550, 25);
            this.TxtConfirmarContrasena.UseSystemPasswordChar = true;
            this.TxtConfirmarContrasena.MaxLength = 100;
            this.TxtConfirmarContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Checkbox Mostrar Contraseñas
            this.ChkMostrarContrasenas.Text = "Mostrar contraseñas";
            this.ChkMostrarContrasenas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.ChkMostrarContrasenas.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.ChkMostrarContrasenas.Location = new System.Drawing.Point(12, 254);
            this.ChkMostrarContrasenas.Size = new System.Drawing.Size(180, 18);
            this.ChkMostrarContrasenas.CheckedChanged += new System.EventHandler(this.ChkMostrarContrasenas_CheckedChanged);

            // Pregunta de Seguridad
            this.LblPreguntaSeguridad.Text = "Pregunta de Seguridad (Ej: ¿Nombre de tu primera mascota?):";
            this.LblPreguntaSeguridad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblPreguntaSeguridad.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblPreguntaSeguridad.Location = new System.Drawing.Point(12, 280);
            this.LblPreguntaSeguridad.Size = new System.Drawing.Size(550, 18);

            this.TxtPreguntaSeguridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtPreguntaSeguridad.Location = new System.Drawing.Point(12, 302);
            this.TxtPreguntaSeguridad.Size = new System.Drawing.Size(550, 25);
            this.TxtPreguntaSeguridad.MaxLength = 200;
            this.TxtPreguntaSeguridad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Respuesta de Seguridad
            this.LblRespuestaSeguridad.Text = "Respuesta:";
            this.LblRespuestaSeguridad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblRespuestaSeguridad.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblRespuestaSeguridad.Location = new System.Drawing.Point(12, 336);
            this.LblRespuestaSeguridad.Size = new System.Drawing.Size(550, 18);

            this.TxtRespuestaSeguridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtRespuestaSeguridad.Location = new System.Drawing.Point(12, 358);
            this.TxtRespuestaSeguridad.Size = new System.Drawing.Size(550, 25);
            this.TxtRespuestaSeguridad.MaxLength = 200;
            this.TxtRespuestaSeguridad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Requisitos de Contraseña
            this.LblRequisitos.Text = "Requisitos: 8+ caracteres, mayúscula, minúscula, número y carácter especial";
            this.LblRequisitos.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.LblRequisitos.ForeColor = System.Drawing.Color.Gray;
            this.LblRequisitos.Location = new System.Drawing.Point(12, 392);
            this.LblRequisitos.Size = new System.Drawing.Size(550, 16);

            // Agregar controles al panel
            this.PanelDatosUsuario.Controls.Add(this.LblRequisitos);
            this.PanelDatosUsuario.Controls.Add(this.TxtRespuestaSeguridad);
            this.PanelDatosUsuario.Controls.Add(this.LblRespuestaSeguridad);
            this.PanelDatosUsuario.Controls.Add(this.TxtPreguntaSeguridad);
            this.PanelDatosUsuario.Controls.Add(this.LblPreguntaSeguridad);
            this.PanelDatosUsuario.Controls.Add(this.ChkMostrarContrasenas);
            this.PanelDatosUsuario.Controls.Add(this.TxtConfirmarContrasena);
            this.PanelDatosUsuario.Controls.Add(this.LblConfirmarContrasena);
            this.PanelDatosUsuario.Controls.Add(this.LblFortaleza);
            this.PanelDatosUsuario.Controls.Add(this.ProgressFortaleza);
            this.PanelDatosUsuario.Controls.Add(this.TxtContrasena);
            this.PanelDatosUsuario.Controls.Add(this.LblContrasena);
            this.PanelDatosUsuario.Controls.Add(this.TxtNombreCompleto);
            this.PanelDatosUsuario.Controls.Add(this.LblNombreCompleto);
            this.PanelDatosUsuario.Controls.Add(this.TxtNombreUsuario);
            this.PanelDatosUsuario.Controls.Add(this.LblNombreUsuario);

            this.PanelContenido.Controls.Add(this.PanelDatosUsuario);
            this.PanelContenido.Controls.Add(this.GroupCodigo);

            // =============================================
            // BOTONES
            // =============================================
            this.BtnRegistrar.Text = "✅ Registrar";
            this.BtnRegistrar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRegistrar.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnRegistrar.ForeColor = System.Drawing.Color.White;
            this.BtnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegistrar.FlatAppearance.BorderSize = 0;
            this.BtnRegistrar.Location = new System.Drawing.Point(155, 565);
            this.BtnRegistrar.Size = new System.Drawing.Size(220, 40);
            this.BtnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);

            this.BtnCancelar.Text = "✖ Cancelar";
            this.BtnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCancelar.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.BtnCancelar.ForeColor = System.Drawing.Color.White;
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.Location = new System.Drawing.Point(390, 565);
            this.BtnCancelar.Size = new System.Drawing.Size(220, 40);
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            this.PanelContenido.Controls.Add(this.BtnCancelar);
            this.PanelContenido.Controls.Add(this.BtnRegistrar);

            // =============================================
            // CONFIGURACIÓN DEL FORMULARIO
            // =============================================
            this.ClientSize = new System.Drawing.Size(640, 680);
            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Usuario - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmRegistroUsuario_Load);

            this.PanelSuperior.ResumeLayout(false);
            this.PanelContenido.ResumeLayout(false);
            this.GroupCodigo.ResumeLayout(false);
            this.GroupCodigo.PerformLayout();
            this.PanelDatosUsuario.ResumeLayout(false);
            this.PanelDatosUsuario.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.Label LblSubtitulo;

        private System.Windows.Forms.Panel PanelContenido;

        private System.Windows.Forms.GroupBox GroupCodigo;
        private System.Windows.Forms.Label LblCodigoActivacion;
        private System.Windows.Forms.TextBox TxtCodigoActivacion;
        private System.Windows.Forms.Button BtnValidarCodigo;
        private System.Windows.Forms.Label LblInfoCodigo;

        private System.Windows.Forms.Panel PanelDatosUsuario;
        private System.Windows.Forms.Label LblNombreUsuario;
        private System.Windows.Forms.TextBox TxtNombreUsuario;
        private System.Windows.Forms.Label LblNombreCompleto;
        private System.Windows.Forms.TextBox TxtNombreCompleto;
        private System.Windows.Forms.Label LblContrasena;
        private System.Windows.Forms.TextBox TxtContrasena;
        private System.Windows.Forms.Label LblFortaleza;
        private System.Windows.Forms.ProgressBar ProgressFortaleza;
        private System.Windows.Forms.Label LblConfirmarContrasena;
        private System.Windows.Forms.TextBox TxtConfirmarContrasena;
        private System.Windows.Forms.CheckBox ChkMostrarContrasenas;
        private System.Windows.Forms.Label LblPreguntaSeguridad;
        private System.Windows.Forms.TextBox TxtPreguntaSeguridad;
        private System.Windows.Forms.Label LblRespuestaSeguridad;
        private System.Windows.Forms.TextBox TxtRespuestaSeguridad;
        private System.Windows.Forms.Label LblRequisitos;

        private System.Windows.Forms.Button BtnRegistrar;
        private System.Windows.Forms.Button BtnCancelar;
    }
}