using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmRecuperarContrasena : BaseForm
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

            // PASO 1: INGRESAR USUARIO
            this.GrpPaso1 = new System.Windows.Forms.GroupBox();
            this.LblInstruccion1 = new System.Windows.Forms.Label();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.BtnBuscarUsuario = new System.Windows.Forms.Button();

            // PASO 2: PREGUNTA DE SEGURIDAD
            this.GrpPaso2 = new System.Windows.Forms.GroupBox();
            this.LblInstruccion2 = new System.Windows.Forms.Label();
            this.LblPreguntaSeguridad = new System.Windows.Forms.Label();
            this.TxtPreguntaMostrada = new System.Windows.Forms.TextBox();
            this.LblRespuesta = new System.Windows.Forms.Label();
            this.TxtRespuesta = new System.Windows.Forms.TextBox();
            this.BtnValidarRespuesta = new System.Windows.Forms.Button();

            // PASO 3: NUEVA CONTRASEÑA
            this.GrpPaso3 = new System.Windows.Forms.GroupBox();
            this.LblInstruccion3 = new System.Windows.Forms.Label();
            this.LblNuevaContrasena = new System.Windows.Forms.Label();
            this.TxtNuevaContrasena = new System.Windows.Forms.TextBox();
            this.LblConfirmarContrasena = new System.Windows.Forms.Label();
            this.TxtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.ChkMostrarContrasenas = new System.Windows.Forms.CheckBox();
            this.ProgressFortaleza = new System.Windows.Forms.ProgressBar();
            this.LblFortaleza = new System.Windows.Forms.Label();
            this.LblRequisitos = new System.Windows.Forms.Label();
            this.BtnCambiarContrasena = new System.Windows.Forms.Button();

            // BOTONES INFERIORES
            this.BtnCancelar = new System.Windows.Forms.Button();


            this.PanelSuperior.SuspendLayout();
            this.PanelContenido.SuspendLayout();
            this.GrpPaso1.SuspendLayout();
            this.GrpPaso2.SuspendLayout();
            this.GrpPaso3.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // PANEL SUPERIOR
            // =============================================
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(700, 70);
            this.PanelSuperior.Padding = new System.Windows.Forms.Padding(20);

            // Título
            this.LblTitulo.Text = "🔐 Recuperar Contraseña";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(20, 12);
            this.LblTitulo.Size = new System.Drawing.Size(660, 32);

            // Subtítulo
            this.LblSubtitulo.Text = "Sigue los pasos para restablecer tu contraseña";
            this.LblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.LblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.LblSubtitulo.Location = new System.Drawing.Point(20, 44);
            this.LblSubtitulo.Size = new System.Drawing.Size(660, 20);

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
            // GRUPO PASO 1: INGRESAR USUARIO
            // =============================================
            this.GrpPaso1.Text = "Paso 1: Identificar Usuario";
            this.GrpPaso1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpPaso1.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpPaso1.Location = new System.Drawing.Point(30, 10);
            this.GrpPaso1.Size = new System.Drawing.Size(640, 110);
            this.GrpPaso1.Padding = new System.Windows.Forms.Padding(12);

            // Instrucción 1
            this.LblInstruccion1.Text = "Ingresa tu nombre de usuario para comenzar";
            this.LblInstruccion1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.LblInstruccion1.ForeColor = System.Drawing.Color.Gray;
            this.LblInstruccion1.Location = new System.Drawing.Point(12, 23);
            this.LblInstruccion1.Size = new System.Drawing.Size(615, 16);

            // Label Usuario
            this.LblUsuario.Text = "Nombre de Usuario:";
            this.LblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblUsuario.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblUsuario.Location = new System.Drawing.Point(12, 44);
            this.LblUsuario.Size = new System.Drawing.Size(615, 16);

            // TextBox Usuario
            this.TxtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtUsuario.Location = new System.Drawing.Point(12, 64);
            this.TxtUsuario.Size = new System.Drawing.Size(450, 25);
            this.TxtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUsuario.MaxLength = 50;

            // Botón Buscar Usuario
            this.BtnBuscarUsuario.Text = "🔍 Buscar";
            this.BtnBuscarUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnBuscarUsuario.ForeColor = System.Drawing.Color.White;
            this.BtnBuscarUsuario.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnBuscarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarUsuario.FlatAppearance.BorderSize = 0;
            this.BtnBuscarUsuario.Location = new System.Drawing.Point(472, 63);
            this.BtnBuscarUsuario.Size = new System.Drawing.Size(155, 28);
            this.BtnBuscarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBuscarUsuario.Click += new System.EventHandler(this.BtnBuscarUsuario_Click);

            this.GrpPaso1.Controls.Add(this.BtnBuscarUsuario);
            this.GrpPaso1.Controls.Add(this.TxtUsuario);
            this.GrpPaso1.Controls.Add(this.LblUsuario);
            this.GrpPaso1.Controls.Add(this.LblInstruccion1);

            // =============================================
            // GRUPO PASO 2: PREGUNTA DE SEGURIDAD
            // =============================================
            this.GrpPaso2.Text = "Paso 2: Verificar Identidad";
            this.GrpPaso2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpPaso2.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpPaso2.Location = new System.Drawing.Point(30, 130);
            this.GrpPaso2.Size = new System.Drawing.Size(640, 160);
            this.GrpPaso2.Padding = new System.Windows.Forms.Padding(12);
            this.GrpPaso2.Enabled = false;

            // Instrucción 2
            this.LblInstruccion2.Text = "Responde correctamente tu pregunta de seguridad";
            this.LblInstruccion2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.LblInstruccion2.ForeColor = System.Drawing.Color.Gray;
            this.LblInstruccion2.Location = new System.Drawing.Point(12, 23);
            this.LblInstruccion2.Size = new System.Drawing.Size(615, 16);

            // Label Pregunta
            this.LblPreguntaSeguridad.Text = "Tu pregunta de seguridad:";
            this.LblPreguntaSeguridad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblPreguntaSeguridad.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblPreguntaSeguridad.Location = new System.Drawing.Point(12, 44);
            this.LblPreguntaSeguridad.Size = new System.Drawing.Size(615, 16);

            // TextBox Pregunta (ReadOnly)
            this.TxtPreguntaMostrada.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtPreguntaMostrada.Location = new System.Drawing.Point(12, 64);
            this.TxtPreguntaMostrada.Size = new System.Drawing.Size(615, 23);
            this.TxtPreguntaMostrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPreguntaMostrada.ReadOnly = true;
            this.TxtPreguntaMostrada.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Label Respuesta
            this.LblRespuesta.Text = "Tu respuesta:";
            this.LblRespuesta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblRespuesta.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblRespuesta.Location = new System.Drawing.Point(12, 95);
            this.LblRespuesta.Size = new System.Drawing.Size(615, 16);

            // TextBox Respuesta
            this.TxtRespuesta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtRespuesta.Location = new System.Drawing.Point(12, 115);
            this.TxtRespuesta.Size = new System.Drawing.Size(450, 25);
            this.TxtRespuesta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRespuesta.MaxLength = 200;

            // Botón Validar Respuesta
            this.BtnValidarRespuesta.Text = "✅ Validar";
            this.BtnValidarRespuesta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnValidarRespuesta.ForeColor = System.Drawing.Color.White;
            this.BtnValidarRespuesta.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnValidarRespuesta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnValidarRespuesta.FlatAppearance.BorderSize = 0;
            this.BtnValidarRespuesta.Location = new System.Drawing.Point(472, 114);
            this.BtnValidarRespuesta.Size = new System.Drawing.Size(155, 28);
            this.BtnValidarRespuesta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnValidarRespuesta.Click += new System.EventHandler(this.BtnValidarRespuesta_Click);

            this.GrpPaso2.Controls.Add(this.BtnValidarRespuesta);
            this.GrpPaso2.Controls.Add(this.TxtRespuesta);
            this.GrpPaso2.Controls.Add(this.LblRespuesta);
            this.GrpPaso2.Controls.Add(this.TxtPreguntaMostrada);
            this.GrpPaso2.Controls.Add(this.LblPreguntaSeguridad);
            this.GrpPaso2.Controls.Add(this.LblInstruccion2);

            // =============================================
            // GRUPO PASO 3: NUEVA CONTRASEÑA
            // =============================================
            this.GrpPaso3.Text = "Paso 3: Nueva Contraseña";
            this.GrpPaso3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpPaso3.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpPaso3.Location = new System.Drawing.Point(30, 300);
            this.GrpPaso3.Size = new System.Drawing.Size(640, 250);
            this.GrpPaso3.Padding = new System.Windows.Forms.Padding(12);
            this.GrpPaso3.Enabled = false;

            // Instrucción 3
            this.LblInstruccion3.Text = "Ingresa tu nueva contraseña (debe cumplir requisitos de seguridad)";
            this.LblInstruccion3.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.LblInstruccion3.ForeColor = System.Drawing.Color.Gray;
            this.LblInstruccion3.Location = new System.Drawing.Point(12, 23);
            this.LblInstruccion3.Size = new System.Drawing.Size(615, 16);

            // Label Nueva Contraseña
            this.LblNuevaContrasena.Text = "Nueva Contraseña:";
            this.LblNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNuevaContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNuevaContrasena.Location = new System.Drawing.Point(12, 44);
            this.LblNuevaContrasena.Size = new System.Drawing.Size(615, 16);

            // TextBox Nueva Contraseña
            this.TxtNuevaContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtNuevaContrasena.Location = new System.Drawing.Point(12, 64);
            this.TxtNuevaContrasena.Size = new System.Drawing.Size(615, 25);
            this.TxtNuevaContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNuevaContrasena.UseSystemPasswordChar = true;
            this.TxtNuevaContrasena.MaxLength = 100;
            this.TxtNuevaContrasena.TextChanged += new System.EventHandler(this.TxtNuevaContrasena_TextChanged);

            // Label Confirmar
            this.LblConfirmarContrasena.Text = "Confirmar Contraseña:";
            this.LblConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblConfirmarContrasena.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblConfirmarContrasena.Location = new System.Drawing.Point(12, 96);
            this.LblConfirmarContrasena.Size = new System.Drawing.Size(615, 16);

            // TextBox Confirmar
            this.TxtConfirmarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtConfirmarContrasena.Location = new System.Drawing.Point(12, 116);
            this.TxtConfirmarContrasena.Size = new System.Drawing.Size(615, 25);
            this.TxtConfirmarContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConfirmarContrasena.UseSystemPasswordChar = true;
            this.TxtConfirmarContrasena.MaxLength = 100;

            // Checkbox Mostrar
            this.ChkMostrarContrasenas.Text = "Mostrar contraseñas";
            this.ChkMostrarContrasenas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.ChkMostrarContrasenas.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.ChkMostrarContrasenas.Location = new System.Drawing.Point(12, 148);
            this.ChkMostrarContrasenas.Size = new System.Drawing.Size(180, 16);
            this.ChkMostrarContrasenas.CheckedChanged += new System.EventHandler(this.ChkMostrarContrasenas_CheckedChanged);

            // ProgressBar Fortaleza
            this.ProgressFortaleza.Location = new System.Drawing.Point(12, 170);
            this.ProgressFortaleza.Size = new System.Drawing.Size(450, 16);
            this.ProgressFortaleza.Maximum = 100;

            // Label Fortaleza
            this.LblFortaleza.Text = "Sin contraseña";
            this.LblFortaleza.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.LblFortaleza.ForeColor = System.Drawing.Color.Gray;
            this.LblFortaleza.Location = new System.Drawing.Point(472, 170);
            this.LblFortaleza.Size = new System.Drawing.Size(155, 16);

            // Label Requisitos - CORREGIDO
            this.LblRequisitos.Text = "Requisitos: 8+ caracteres, mayúscula, minúscula, número y carácter especial";
            this.LblRequisitos.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.LblRequisitos.ForeColor = System.Drawing.Color.Gray;
            this.LblRequisitos.Location = new System.Drawing.Point(12, 192);
            this.LblRequisitos.Size = new System.Drawing.Size(615, 16);

            // Botón Cambiar Contraseña
            this.BtnCambiarContrasena.Text = "🔒 Cambiar Contraseña";
            this.BtnCambiarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCambiarContrasena.ForeColor = System.Drawing.Color.White;
            this.BtnCambiarContrasena.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnCambiarContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCambiarContrasena.FlatAppearance.BorderSize = 0;
            this.BtnCambiarContrasena.Location = new System.Drawing.Point(200, 210);
            this.BtnCambiarContrasena.Size = new System.Drawing.Size(427, 35);
            this.BtnCambiarContrasena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCambiarContrasena.Click += new System.EventHandler(this.BtnCambiarContrasena_Click);

            this.GrpPaso3.Controls.Add(this.BtnCambiarContrasena);
            this.GrpPaso3.Controls.Add(this.LblRequisitos);
            this.GrpPaso3.Controls.Add(this.LblFortaleza);
            this.GrpPaso3.Controls.Add(this.ProgressFortaleza);
            this.GrpPaso3.Controls.Add(this.ChkMostrarContrasenas);
            this.GrpPaso3.Controls.Add(this.TxtConfirmarContrasena);
            this.GrpPaso3.Controls.Add(this.LblConfirmarContrasena);
            this.GrpPaso3.Controls.Add(this.TxtNuevaContrasena);
            this.GrpPaso3.Controls.Add(this.LblNuevaContrasena);
            this.GrpPaso3.Controls.Add(this.LblInstruccion3);

            // Agregar grupos al panel
            this.PanelContenido.Controls.Add(this.GrpPaso3);
            this.PanelContenido.Controls.Add(this.GrpPaso2);
            this.PanelContenido.Controls.Add(this.GrpPaso1);

            // =============================================
            // BOTONES INFERIORES
            // =============================================


            // Botón Cancelar
            this.BtnCancelar.Text = "✖ Cancelar";
            this.BtnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancelar.ForeColor = System.Drawing.Color.White;
            this.BtnCancelar.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.Location = new System.Drawing.Point(520, 565);
            this.BtnCancelar.Size = new System.Drawing.Size(150, 35);
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            this.PanelContenido.Controls.Add(this.BtnCancelar);


            // =============================================
            // CONFIGURACIÓN DEL FORMULARIO
            // =============================================
            this.ClientSize = new System.Drawing.Size(700, 680);
            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperar Contraseña - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmRecuperarContrasena_Load);

            this.PanelSuperior.ResumeLayout(false);
            this.PanelContenido.ResumeLayout(false);
            this.GrpPaso1.ResumeLayout(false);
            this.GrpPaso1.PerformLayout();
            this.GrpPaso2.ResumeLayout(false);
            this.GrpPaso2.PerformLayout();
            this.GrpPaso3.ResumeLayout(false);
            this.GrpPaso3.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.Label LblSubtitulo;

        private System.Windows.Forms.Panel PanelContenido;

        private System.Windows.Forms.GroupBox GrpPaso1;
        private System.Windows.Forms.Label LblInstruccion1;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Button BtnBuscarUsuario;

        private System.Windows.Forms.GroupBox GrpPaso2;
        private System.Windows.Forms.Label LblInstruccion2;
        private System.Windows.Forms.Label LblPreguntaSeguridad;
        private System.Windows.Forms.TextBox TxtPreguntaMostrada;
        private System.Windows.Forms.Label LblRespuesta;
        private System.Windows.Forms.TextBox TxtRespuesta;
        private System.Windows.Forms.Button BtnValidarRespuesta;

        private System.Windows.Forms.GroupBox GrpPaso3;
        private System.Windows.Forms.Label LblInstruccion3;
        private System.Windows.Forms.Label LblNuevaContrasena;
        private System.Windows.Forms.TextBox TxtNuevaContrasena;
        private System.Windows.Forms.Label LblConfirmarContrasena;
        private System.Windows.Forms.TextBox TxtConfirmarContrasena;
        private System.Windows.Forms.CheckBox ChkMostrarContrasenas;
        private System.Windows.Forms.ProgressBar ProgressFortaleza;
        private System.Windows.Forms.Label LblFortaleza;
        private System.Windows.Forms.Label LblRequisitos;
        private System.Windows.Forms.Button BtnCambiarContrasena;

        private System.Windows.Forms.Button BtnCancelar;
       
    }
}