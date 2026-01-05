using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmAdministracion : BaseForm
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
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.PicLogo = new System.Windows.Forms.PictureBox();
            this.BtnRegresar = new System.Windows.Forms.Button();

            this.PanelContenido = new System.Windows.Forms.Panel();
            this.LblUsuarioActual = new System.Windows.Forms.Label();
            this.TabControlPrincipal = new System.Windows.Forms.TabControl();

            this.TabUsuarios = new System.Windows.Forms.TabPage();
            this.DgvUsuarios = new System.Windows.Forms.DataGridView();
            this.PanelBotonesUsuarios = new System.Windows.Forms.Panel();
            this.LblTotalUsuarios = new System.Windows.Forms.Label();
            this.BtnRefrescarUsuarios = new System.Windows.Forms.Button();
            this.BtnActivarUsuario = new System.Windows.Forms.Button();
            this.BtnDesactivarUsuario = new System.Windows.Forms.Button();

            this.TabCodigos = new System.Windows.Forms.TabPage();
            this.GroupGenerarCodigo = new System.Windows.Forms.GroupBox();
            this.LblDiasValidez = new System.Windows.Forms.Label();
            this.NumDiasValidez = new System.Windows.Forms.NumericUpDown();
            this.BtnGenerarCodigo = new System.Windows.Forms.Button();
            this.TxtCodigoGenerado = new System.Windows.Forms.TextBox();
            this.BtnCopiarCodigo = new System.Windows.Forms.Button();
            this.DgvCodigos = new System.Windows.Forms.DataGridView();
            this.PanelBotonesCodigos = new System.Windows.Forms.Panel();
            this.LblTotalCodigos = new System.Windows.Forms.Label();
            this.BtnRefrescarCodigos = new System.Windows.Forms.Button();

            this.TabEstadisticas = new System.Windows.Forms.TabPage();
            this.PanelEstadisticas = new System.Windows.Forms.Panel();
            this.LblEstTotalUsuarios = new System.Windows.Forms.Label();
            this.LblEstUsuariosActivos = new System.Windows.Forms.Label();
            this.LblEstAdministradores = new System.Windows.Forms.Label();
            this.LblEstCodigosDisponibles = new System.Windows.Forms.Label();
            this.LblEstUsuariosHoy = new System.Windows.Forms.Label();
            this.LblEstAdministradoresHoy = new System.Windows.Forms.Label();
            this.BtnRefrescarEstadisticas = new System.Windows.Forms.Button();

            this.PanelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            this.PanelContenido.SuspendLayout();
            this.TabControlPrincipal.SuspendLayout();
            this.TabUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsuarios)).BeginInit();
            this.PanelBotonesUsuarios.SuspendLayout();
            this.TabCodigos.SuspendLayout();
            this.GroupGenerarCodigo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumDiasValidez)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCodigos)).BeginInit();
            this.PanelBotonesCodigos.SuspendLayout();
            this.TabEstadisticas.SuspendLayout();
            this.PanelEstadisticas.SuspendLayout();
            this.SuspendLayout();

            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Controls.Add(this.BtnRegresar);
            this.PanelSuperior.Controls.Add(this.LblTitulo);
            this.PanelSuperior.Controls.Add(this.PicLogo);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(1200, 80);

            try { this.PicLogo.Image = Properties.Resources.LogoConsejo2; } catch { }
            this.PicLogo.Location = new System.Drawing.Point(20, 10);
            this.PicLogo.Name = "PicLogo";
            this.PicLogo.Size = new System.Drawing.Size(60, 60);
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.LblTitulo.Text = "⚙️ Panel de Administración";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(90, 20);
            this.LblTitulo.Size = new System.Drawing.Size(800, 40);
            this.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.BtnRegresar.BackColor = System.Drawing.Color.FromArgb(247, 148, 29);
            this.BtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegresar.FlatAppearance.BorderSize = 0;
            this.BtnRegresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRegresar.ForeColor = System.Drawing.Color.White;
            this.BtnRegresar.Location = new System.Drawing.Point(1050, 20);
            this.BtnRegresar.Name = "BtnRegresar";
            this.BtnRegresar.Size = new System.Drawing.Size(130, 40);
            this.BtnRegresar.Text = "← Regresar";
            this.BtnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRegresar.Click += new System.EventHandler(this.BtnCerrar_Click);

            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(20);

            this.LblUsuarioActual.Text = "Administrador: Cargando...";
            this.LblUsuarioActual.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblUsuarioActual.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblUsuarioActual.Location = new System.Drawing.Point(20, 10);
            this.LblUsuarioActual.Size = new System.Drawing.Size(800, 25);

            this.TabControlPrincipal.Location = new System.Drawing.Point(20, 45);
            this.TabControlPrincipal.Size = new System.Drawing.Size(1140, 620);
            this.TabControlPrincipal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.TabControlPrincipal.Controls.Add(this.TabUsuarios);
            this.TabControlPrincipal.Controls.Add(this.TabCodigos);
            this.TabControlPrincipal.Controls.Add(this.TabEstadisticas);

            this.PanelContenido.Controls.Add(this.TabControlPrincipal);
            this.PanelContenido.Controls.Add(this.LblUsuarioActual);

            this.TabUsuarios.Text = "👥 Gestión de Usuarios";
            this.TabUsuarios.BackColor = System.Drawing.Color.White;
            this.TabUsuarios.Padding = new System.Windows.Forms.Padding(15);

            this.DgvUsuarios.Location = new System.Drawing.Point(15, 15);
            this.DgvUsuarios.Size = new System.Drawing.Size(1095, 480);
            this.DgvUsuarios.AllowUserToAddRows = false;
            this.DgvUsuarios.AllowUserToDeleteRows = false;
            this.DgvUsuarios.ReadOnly = true;
            this.DgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvUsuarios.MultiSelect = false;
            this.DgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.DgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DgvUsuarios.ColumnHeadersHeight = 40;
            this.DgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.DgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.DgvUsuarios.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvUsuarios.EnableHeadersVisualStyles = false;
            this.DgvUsuarios.RowTemplate.Height = 35;

            this.PanelBotonesUsuarios.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBotonesUsuarios.Height = 70;
            this.PanelBotonesUsuarios.BackColor = System.Drawing.Color.White;
            this.PanelBotonesUsuarios.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);

            this.LblTotalUsuarios.Text = "Total de usuarios: 0";
            this.LblTotalUsuarios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTotalUsuarios.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblTotalUsuarios.Location = new System.Drawing.Point(15, 20);
            this.LblTotalUsuarios.Size = new System.Drawing.Size(300, 30);
            this.LblTotalUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.BtnRefrescarUsuarios.Text = "🔄 Refrescar";
            this.BtnRefrescarUsuarios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefrescarUsuarios.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnRefrescarUsuarios.ForeColor = System.Drawing.Color.White;
            this.BtnRefrescarUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefrescarUsuarios.FlatAppearance.BorderSize = 0;
            this.BtnRefrescarUsuarios.Location = new System.Drawing.Point(650, 15);
            this.BtnRefrescarUsuarios.Size = new System.Drawing.Size(130, 40);
            this.BtnRefrescarUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefrescarUsuarios.Click += new System.EventHandler(this.BtnRefrescarUsuarios_Click);

            this.BtnActivarUsuario.Text = "✅ Activar";
            this.BtnActivarUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnActivarUsuario.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnActivarUsuario.ForeColor = System.Drawing.Color.White;
            this.BtnActivarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnActivarUsuario.FlatAppearance.BorderSize = 0;
            this.BtnActivarUsuario.Location = new System.Drawing.Point(790, 15);
            this.BtnActivarUsuario.Size = new System.Drawing.Size(150, 40);
            this.BtnActivarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnActivarUsuario.Click += new System.EventHandler(this.BtnActivarUsuario_Click);

            this.BtnDesactivarUsuario.Text = "❌ Desactivar";
            this.BtnDesactivarUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnDesactivarUsuario.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.BtnDesactivarUsuario.ForeColor = System.Drawing.Color.White;
            this.BtnDesactivarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDesactivarUsuario.FlatAppearance.BorderSize = 0;
            this.BtnDesactivarUsuario.Location = new System.Drawing.Point(950, 15);
            this.BtnDesactivarUsuario.Size = new System.Drawing.Size(150, 40);
            this.BtnDesactivarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDesactivarUsuario.Click += new System.EventHandler(this.BtnDesactivarUsuario_Click);

            this.PanelBotonesUsuarios.Controls.Add(this.BtnDesactivarUsuario);
            this.PanelBotonesUsuarios.Controls.Add(this.BtnActivarUsuario);
            this.PanelBotonesUsuarios.Controls.Add(this.BtnRefrescarUsuarios);
            this.PanelBotonesUsuarios.Controls.Add(this.LblTotalUsuarios);

            this.TabUsuarios.Controls.Add(this.PanelBotonesUsuarios);
            this.TabUsuarios.Controls.Add(this.DgvUsuarios);

            this.TabCodigos.Text = "🎫 Códigos de Activación";
            this.TabCodigos.BackColor = System.Drawing.Color.White;
            this.TabCodigos.Padding = new System.Windows.Forms.Padding(15);

            this.GroupGenerarCodigo.Text = " Generar Nuevo Código ";
            this.GroupGenerarCodigo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.GroupGenerarCodigo.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GroupGenerarCodigo.Location = new System.Drawing.Point(15, 15);
            this.GroupGenerarCodigo.Size = new System.Drawing.Size(1095, 130);
            this.GroupGenerarCodigo.Padding = new System.Windows.Forms.Padding(20);

            this.LblDiasValidez.Text = "Días de validez:";
            this.LblDiasValidez.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblDiasValidez.Location = new System.Drawing.Point(20, 35);
            this.LblDiasValidez.Size = new System.Drawing.Size(120, 25);

            this.NumDiasValidez.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.NumDiasValidez.Location = new System.Drawing.Point(140, 35);
            this.NumDiasValidez.Size = new System.Drawing.Size(100, 27);
            this.NumDiasValidez.Minimum = 1;
            this.NumDiasValidez.Maximum = 365;
            this.NumDiasValidez.Value = 30;

            this.BtnGenerarCodigo.Text = "🎫 Generar Código";
            this.BtnGenerarCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnGenerarCodigo.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnGenerarCodigo.ForeColor = System.Drawing.Color.White;
            this.BtnGenerarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerarCodigo.FlatAppearance.BorderSize = 0;
            this.BtnGenerarCodigo.Location = new System.Drawing.Point(260, 30);
            this.BtnGenerarCodigo.Size = new System.Drawing.Size(180, 35);
            this.BtnGenerarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenerarCodigo.Click += new System.EventHandler(this.BtnGenerarCodigo_Click);

            this.TxtCodigoGenerado.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.TxtCodigoGenerado.Location = new System.Drawing.Point(20, 80);
            this.TxtCodigoGenerado.Size = new System.Drawing.Size(420, 30);
            this.TxtCodigoGenerado.ReadOnly = true;
            this.TxtCodigoGenerado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtCodigoGenerado.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
            this.TxtCodigoGenerado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.BtnCopiarCodigo.Text = "📋 Copiar";
            this.BtnCopiarCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCopiarCodigo.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnCopiarCodigo.ForeColor = System.Drawing.Color.White;
            this.BtnCopiarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopiarCodigo.FlatAppearance.BorderSize = 0;
            this.BtnCopiarCodigo.Location = new System.Drawing.Point(450, 78);
            this.BtnCopiarCodigo.Size = new System.Drawing.Size(100, 34);
            this.BtnCopiarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopiarCodigo.Click += new System.EventHandler(this.BtnCopiarCodigo_Click);

            this.GroupGenerarCodigo.Controls.Add(this.BtnCopiarCodigo);
            this.GroupGenerarCodigo.Controls.Add(this.TxtCodigoGenerado);
            this.GroupGenerarCodigo.Controls.Add(this.BtnGenerarCodigo);
            this.GroupGenerarCodigo.Controls.Add(this.NumDiasValidez);
            this.GroupGenerarCodigo.Controls.Add(this.LblDiasValidez);

            this.DgvCodigos.Location = new System.Drawing.Point(15, 160);
            this.DgvCodigos.Size = new System.Drawing.Size(1095, 335);
            this.DgvCodigos.AllowUserToAddRows = false;
            this.DgvCodigos.AllowUserToDeleteRows = false;
            this.DgvCodigos.ReadOnly = true;
            this.DgvCodigos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCodigos.MultiSelect = false;
            this.DgvCodigos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvCodigos.BackgroundColor = System.Drawing.Color.White;
            this.DgvCodigos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DgvCodigos.ColumnHeadersHeight = 40;
            this.DgvCodigos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.DgvCodigos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DgvCodigos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.DgvCodigos.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvCodigos.EnableHeadersVisualStyles = false;
            this.DgvCodigos.RowTemplate.Height = 35;

            this.PanelBotonesCodigos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBotonesCodigos.Height = 70;
            this.PanelBotonesCodigos.BackColor = System.Drawing.Color.White;
            this.PanelBotonesCodigos.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);

            this.LblTotalCodigos.Text = "Total códigos: 0";
            this.LblTotalCodigos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTotalCodigos.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblTotalCodigos.Location = new System.Drawing.Point(15, 20);
            this.LblTotalCodigos.Size = new System.Drawing.Size(500, 30);
            this.LblTotalCodigos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.BtnRefrescarCodigos.Text = "🔄 Refrescar Lista";
            this.BtnRefrescarCodigos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefrescarCodigos.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnRefrescarCodigos.ForeColor = System.Drawing.Color.White;
            this.BtnRefrescarCodigos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefrescarCodigos.FlatAppearance.BorderSize = 0;
            this.BtnRefrescarCodigos.Location = new System.Drawing.Point(930, 15);
            this.BtnRefrescarCodigos.Size = new System.Drawing.Size(170, 40);
            this.BtnRefrescarCodigos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefrescarCodigos.Click += new System.EventHandler(this.BtnRefrescarCodigos_Click);

            this.PanelBotonesCodigos.Controls.Add(this.BtnRefrescarCodigos);
            this.PanelBotonesCodigos.Controls.Add(this.LblTotalCodigos);

            this.TabCodigos.Controls.Add(this.PanelBotonesCodigos);
            this.TabCodigos.Controls.Add(this.DgvCodigos);
            this.TabCodigos.Controls.Add(this.GroupGenerarCodigo);

            this.TabEstadisticas.Text = "📊 Estadísticas";
            this.TabEstadisticas.BackColor = System.Drawing.Color.White;
            this.TabEstadisticas.Padding = new System.Windows.Forms.Padding(15);

            this.PanelEstadisticas.Location = new System.Drawing.Point(50, 50);
            this.PanelEstadisticas.Size = new System.Drawing.Size(1000, 500);
            this.PanelEstadisticas.BackColor = System.Drawing.Color.White;

            this.LblEstTotalUsuarios.Text = "📊 Total de Cuentas Registradas: Cargando...";
            this.LblEstTotalUsuarios.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstTotalUsuarios.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblEstTotalUsuarios.Location = new System.Drawing.Point(20, 20);
            this.LblEstTotalUsuarios.Size = new System.Drawing.Size(950, 40);
            this.LblEstTotalUsuarios.BackColor = System.Drawing.Color.FromArgb(240, 240, 250);
            this.LblEstTotalUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstTotalUsuarios.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.LblEstUsuariosActivos.Text = "✅ Cuentas Habilitadas: Cargando...";
            this.LblEstUsuariosActivos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstUsuariosActivos.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.LblEstUsuariosActivos.Location = new System.Drawing.Point(20, 75);
            this.LblEstUsuariosActivos.Size = new System.Drawing.Size(950, 40);
            this.LblEstUsuariosActivos.BackColor = System.Drawing.Color.FromArgb(240, 250, 240);
            this.LblEstUsuariosActivos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstUsuariosActivos.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.LblEstAdministradores.Text = "👑 Administradores: Cargando...";
            this.LblEstAdministradores.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstAdministradores.ForeColor = System.Drawing.Color.FromArgb(247, 148, 29);
            this.LblEstAdministradores.Location = new System.Drawing.Point(20, 130);
            this.LblEstAdministradores.Size = new System.Drawing.Size(950, 40);
            this.LblEstAdministradores.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
            this.LblEstAdministradores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstAdministradores.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.LblEstCodigosDisponibles.Text = "🎫 Códigos Válidos Disponibles: Cargando...";
            this.LblEstCodigosDisponibles.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstCodigosDisponibles.ForeColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.LblEstCodigosDisponibles.Location = new System.Drawing.Point(20, 185);
            this.LblEstCodigosDisponibles.Size = new System.Drawing.Size(950, 40);
            this.LblEstCodigosDisponibles.BackColor = System.Drawing.Color.FromArgb(240, 245, 255);
            this.LblEstCodigosDisponibles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstCodigosDisponibles.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.LblEstUsuariosHoy.Text = "👤 Usuarios que entraron hoy: Cargando...";
            this.LblEstUsuariosHoy.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstUsuariosHoy.ForeColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.LblEstUsuariosHoy.Location = new System.Drawing.Point(20, 240);
            this.LblEstUsuariosHoy.Size = new System.Drawing.Size(950, 40);
            this.LblEstUsuariosHoy.BackColor = System.Drawing.Color.FromArgb(240, 250, 255);
            this.LblEstUsuariosHoy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstUsuariosHoy.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.LblEstAdministradoresHoy.Text = "👑 Administradores que entraron hoy: Cargando...";
            this.LblEstAdministradoresHoy.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblEstAdministradoresHoy.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.LblEstAdministradoresHoy.Location = new System.Drawing.Point(20, 295);
            this.LblEstAdministradoresHoy.Size = new System.Drawing.Size(950, 40);
            this.LblEstAdministradoresHoy.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.LblEstAdministradoresHoy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblEstAdministradoresHoy.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);

            this.BtnRefrescarEstadisticas.Text = "🔄 Actualizar Estadísticas";
            this.BtnRefrescarEstadisticas.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.BtnRefrescarEstadisticas.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnRefrescarEstadisticas.ForeColor = System.Drawing.Color.White;
            this.BtnRefrescarEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefrescarEstadisticas.FlatAppearance.BorderSize = 0;
            this.BtnRefrescarEstadisticas.Location = new System.Drawing.Point(20, 370);
            this.BtnRefrescarEstadisticas.Size = new System.Drawing.Size(300, 60);
            this.BtnRefrescarEstadisticas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefrescarEstadisticas.Click += new System.EventHandler(this.BtnRefrescarEstadisticas_Click);

            this.PanelEstadisticas.Controls.Add(this.BtnRefrescarEstadisticas);
            this.PanelEstadisticas.Controls.Add(this.LblEstAdministradoresHoy);
            this.PanelEstadisticas.Controls.Add(this.LblEstUsuariosHoy);
            this.PanelEstadisticas.Controls.Add(this.LblEstCodigosDisponibles);
            this.PanelEstadisticas.Controls.Add(this.LblEstAdministradores);
            this.PanelEstadisticas.Controls.Add(this.LblEstUsuariosActivos);
            this.PanelEstadisticas.Controls.Add(this.LblEstTotalUsuarios);

            this.TabEstadisticas.Controls.Add(this.PanelEstadisticas);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 750);

            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ControlBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Administración - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmAdministracion_Load);

            this.PanelSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            this.PanelContenido.ResumeLayout(false);
            this.TabControlPrincipal.ResumeLayout(false);
            this.TabUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsuarios)).EndInit();
            this.PanelBotonesUsuarios.ResumeLayout(false);
            this.TabCodigos.ResumeLayout(false);
            this.GroupGenerarCodigo.ResumeLayout(false);
            this.GroupGenerarCodigo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumDiasValidez)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCodigos)).EndInit();
            this.PanelBotonesCodigos.ResumeLayout(false);
            this.TabEstadisticas.ResumeLayout(false);
            this.PanelEstadisticas.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Button BtnRegresar;

        private System.Windows.Forms.Panel PanelContenido;
        private System.Windows.Forms.Label LblUsuarioActual;
        private System.Windows.Forms.TabControl TabControlPrincipal;

        private System.Windows.Forms.TabPage TabUsuarios;
        private System.Windows.Forms.DataGridView DgvUsuarios;
        private System.Windows.Forms.Panel PanelBotonesUsuarios;
        private System.Windows.Forms.Label LblTotalUsuarios;
        private System.Windows.Forms.Button BtnRefrescarUsuarios;
        private System.Windows.Forms.Button BtnActivarUsuario;
        private System.Windows.Forms.Button BtnDesactivarUsuario;

        private System.Windows.Forms.TabPage TabCodigos;
        private System.Windows.Forms.GroupBox GroupGenerarCodigo;
        private System.Windows.Forms.Label LblDiasValidez;
        private System.Windows.Forms.NumericUpDown NumDiasValidez;
        private System.Windows.Forms.Button BtnGenerarCodigo;
        private System.Windows.Forms.TextBox TxtCodigoGenerado;
        private System.Windows.Forms.Button BtnCopiarCodigo;
        private System.Windows.Forms.DataGridView DgvCodigos;
        private System.Windows.Forms.Panel PanelBotonesCodigos;
        private System.Windows.Forms.Label LblTotalCodigos;
        private System.Windows.Forms.Button BtnRefrescarCodigos;

        private System.Windows.Forms.TabPage TabEstadisticas;
        private System.Windows.Forms.Panel PanelEstadisticas;
        private System.Windows.Forms.Label LblEstTotalUsuarios;
        private System.Windows.Forms.Label LblEstUsuariosActivos;
        private System.Windows.Forms.Label LblEstAdministradores;
        private System.Windows.Forms.Label LblEstCodigosDisponibles;
        private System.Windows.Forms.Label LblEstUsuariosHoy;
        private System.Windows.Forms.Label LblEstAdministradoresHoy;
        private System.Windows.Forms.Button BtnRefrescarEstadisticas;
    }
}