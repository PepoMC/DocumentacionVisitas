using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmHistorialCambios : BaseForm
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
            // CONTROLES PRINCIPALES
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.PicLogo = new System.Windows.Forms.PictureBox();
            this.BtnRegresar = new System.Windows.Forms.Button();

            this.PanelContenido = new System.Windows.Forms.Panel();

            // Panel de Filtros
            this.GroupFiltros = new System.Windows.Forms.GroupBox();
            this.LblFechaInicio = new System.Windows.Forms.Label();
            this.DtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.LblFechaFin = new System.Windows.Forms.Label();
            this.DtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.CmbUsuario = new System.Windows.Forms.ComboBox();
            this.LblTipoAccion = new System.Windows.Forms.Label();
            this.CmbTipoAccion = new System.Windows.Forms.ComboBox();
            this.LblBuscar = new System.Windows.Forms.Label();
            this.TxtBuscar = new System.Windows.Forms.TextBox();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.BtnLimpiarFiltros = new System.Windows.Forms.Button();

            // DataGridView
            this.DgvHistorial = new System.Windows.Forms.DataGridView();
            this.LblTotalRegistros = new System.Windows.Forms.Label();

            // Botones
            this.BtnRefrescar = new System.Windows.Forms.Button();
            this.BtnVerDetalles = new System.Windows.Forms.Button();
            this.BtnExportar = new System.Windows.Forms.Button();

            this.PanelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            this.PanelContenido.SuspendLayout();
            this.GroupFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistorial)).BeginInit();
            this.SuspendLayout();

            // =============================================
            // PANEL SUPERIOR CON LOGO
            // =============================================
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Controls.Add(this.BtnRegresar);
            this.PanelSuperior.Controls.Add(this.LblTitulo);
            this.PanelSuperior.Controls.Add(this.PicLogo);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(1200, 80);

            // Logo
            try { this.PicLogo.Image = Properties.Resources.LogoConsejo2; } catch { }
            this.PicLogo.Location = new System.Drawing.Point(20, 10);
            this.PicLogo.Name = "PicLogo";
            this.PicLogo.Size = new System.Drawing.Size(60, 60);
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // Título
            this.LblTitulo.Text = "📋 Historial de Cambios";
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(90, 20);
            this.LblTitulo.Size = new System.Drawing.Size(800, 40);
            this.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Botón Regresar
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

            // =============================================
            // PANEL CONTENIDO
            // =============================================
            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(20);

            // =============================================
            // GRUPO DE FILTROS
            // =============================================
            this.GroupFiltros.Text = " Filtros de Búsqueda ";
            this.GroupFiltros.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.GroupFiltros.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GroupFiltros.Location = new System.Drawing.Point(20, 10);
            this.GroupFiltros.Size = new System.Drawing.Size(1140, 130);

            // Fecha Inicio
            this.LblFechaInicio.Text = "Fecha Inicio:";
            this.LblFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblFechaInicio.Location = new System.Drawing.Point(20, 30);
            this.LblFechaInicio.Size = new System.Drawing.Size(100, 20);

            this.DtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFechaInicio.Location = new System.Drawing.Point(20, 55);
            this.DtpFechaInicio.Size = new System.Drawing.Size(120, 23);

            // Fecha Fin
            this.LblFechaFin.Text = "Fecha Fin:";
            this.LblFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblFechaFin.Location = new System.Drawing.Point(160, 30);
            this.LblFechaFin.Size = new System.Drawing.Size(100, 20);

            this.DtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFechaFin.Location = new System.Drawing.Point(160, 55);
            this.DtpFechaFin.Size = new System.Drawing.Size(120, 23);

            // Usuario
            this.LblUsuario.Text = "Usuario:";
            this.LblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblUsuario.Location = new System.Drawing.Point(300, 30);
            this.LblUsuario.Size = new System.Drawing.Size(100, 20);

            this.CmbUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUsuario.Location = new System.Drawing.Point(300, 55);
            this.CmbUsuario.Size = new System.Drawing.Size(150, 23);

            // Tipo de Acción
            this.LblTipoAccion.Text = "Tipo de Acción:";
            this.LblTipoAccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblTipoAccion.Location = new System.Drawing.Point(470, 30);
            this.LblTipoAccion.Size = new System.Drawing.Size(100, 20);

            this.CmbTipoAccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CmbTipoAccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTipoAccion.Location = new System.Drawing.Point(470, 55);
            this.CmbTipoAccion.Size = new System.Drawing.Size(200, 23);

            // Buscar
            this.LblBuscar.Text = "Buscar:";
            this.LblBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblBuscar.Location = new System.Drawing.Point(690, 30);
            this.LblBuscar.Size = new System.Drawing.Size(150, 20);

            this.TxtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtBuscar.Location = new System.Drawing.Point(690, 55);
            this.TxtBuscar.Size = new System.Drawing.Size(250, 23);
            this.TxtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBuscar_KeyPress);

            // Botón Buscar
            this.BtnBuscar.Text = "🔍 Buscar";
            this.BtnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnBuscar.ForeColor = System.Drawing.Color.White;
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.FlatAppearance.BorderSize = 0;
            this.BtnBuscar.Location = new System.Drawing.Point(960, 50);
            this.BtnBuscar.Size = new System.Drawing.Size(90, 30);
            this.BtnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);

            // Botón Limpiar
            this.BtnLimpiarFiltros.Text = "🗑️ Limpiar";
            this.BtnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.BtnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.BtnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            this.BtnLimpiarFiltros.Location = new System.Drawing.Point(1060, 50);
            this.BtnLimpiarFiltros.Size = new System.Drawing.Size(70, 30);
            this.BtnLimpiarFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLimpiarFiltros.Click += new System.EventHandler(this.BtnLimpiarFiltros_Click);

            // Label Total Registros
            this.LblTotalRegistros.Text = "Total de registros: 0";
            this.LblTotalRegistros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTotalRegistros.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblTotalRegistros.Location = new System.Drawing.Point(20, 95);
            this.LblTotalRegistros.Size = new System.Drawing.Size(500, 20);

            this.GroupFiltros.Controls.Add(this.LblTotalRegistros);
            this.GroupFiltros.Controls.Add(this.BtnLimpiarFiltros);
            this.GroupFiltros.Controls.Add(this.BtnBuscar);
            this.GroupFiltros.Controls.Add(this.TxtBuscar);
            this.GroupFiltros.Controls.Add(this.LblBuscar);
            this.GroupFiltros.Controls.Add(this.CmbTipoAccion);
            this.GroupFiltros.Controls.Add(this.LblTipoAccion);
            this.GroupFiltros.Controls.Add(this.CmbUsuario);
            this.GroupFiltros.Controls.Add(this.LblUsuario);
            this.GroupFiltros.Controls.Add(this.DtpFechaFin);
            this.GroupFiltros.Controls.Add(this.LblFechaFin);
            this.GroupFiltros.Controls.Add(this.DtpFechaInicio);
            this.GroupFiltros.Controls.Add(this.LblFechaInicio);

            // =============================================
            // DATAGRIDVIEW HISTORIAL
            // =============================================
            this.DgvHistorial.Location = new System.Drawing.Point(20, 160);
            this.DgvHistorial.Size = new System.Drawing.Size(1140, 430);
            this.DgvHistorial.AllowUserToAddRows = false;
            this.DgvHistorial.AllowUserToDeleteRows = false;
            this.DgvHistorial.ReadOnly = true;
            this.DgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvHistorial.MultiSelect = false;
            this.DgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.DgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.DgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DgvHistorial.ColumnHeadersHeight = 40;
            this.DgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.DgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DgvHistorial.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.DgvHistorial.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvHistorial.EnableHeadersVisualStyles = false;
            this.DgvHistorial.RowHeadersWidth = 25;
            this.DgvHistorial.RowTemplate.Height = 35;
            this.DgvHistorial.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvHistorial_CellDoubleClick);

            // =============================================
            // BOTONES INFERIORES
            // =============================================
            this.BtnRefrescar.Text = "🔄 Refrescar";
            this.BtnRefrescar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefrescar.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnRefrescar.ForeColor = System.Drawing.Color.White;
            this.BtnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefrescar.FlatAppearance.BorderSize = 0;
            this.BtnRefrescar.Location = new System.Drawing.Point(730, 610);
            this.BtnRefrescar.Size = new System.Drawing.Size(130, 40);
            this.BtnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefrescar.Click += new System.EventHandler(this.BtnRefrescar_Click);

            this.BtnVerDetalles.Text = "🔍 Ver Detalles";
            this.BtnVerDetalles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnVerDetalles.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnVerDetalles.ForeColor = System.Drawing.Color.White;
            this.BtnVerDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVerDetalles.FlatAppearance.BorderSize = 0;
            this.BtnVerDetalles.Location = new System.Drawing.Point(870, 610);
            this.BtnVerDetalles.Size = new System.Drawing.Size(140, 40);
            this.BtnVerDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnVerDetalles.Click += new System.EventHandler(this.BtnVerDetalles_Click);

            this.BtnExportar.Text = "📥 Reporte";
            this.BtnExportar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnExportar.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.BtnExportar.ForeColor = System.Drawing.Color.Black;
            this.BtnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExportar.FlatAppearance.BorderSize = 0;
            this.BtnExportar.Location = new System.Drawing.Point(1020, 610);
            this.BtnExportar.Size = new System.Drawing.Size(140, 40);
            this.BtnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExportar.Click += new System.EventHandler(this.BtnExportar_Click);

            this.PanelContenido.Controls.Add(this.BtnExportar);
            this.PanelContenido.Controls.Add(this.BtnVerDetalles);
            this.PanelContenido.Controls.Add(this.BtnRefrescar);
            this.PanelContenido.Controls.Add(this.DgvHistorial);
            this.PanelContenido.Controls.Add(this.GroupFiltros);

            // =============================================
            // CONFIGURACIÓN DEL FORMULARIO
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 750);

            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ControlBox = false;  // ❌ SIN botones en la barra
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de Cambios - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmHistorialCambios_Load);

            this.PanelSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            this.PanelContenido.ResumeLayout(false);
            this.GroupFiltros.ResumeLayout(false);
            this.GroupFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistorial)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Button BtnRegresar;

        private System.Windows.Forms.Panel PanelContenido;
        private System.Windows.Forms.GroupBox GroupFiltros;
        private System.Windows.Forms.Label LblFechaInicio;
        private System.Windows.Forms.DateTimePicker DtpFechaInicio;
        private System.Windows.Forms.Label LblFechaFin;
        private System.Windows.Forms.DateTimePicker DtpFechaFin;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.ComboBox CmbUsuario;
        private System.Windows.Forms.Label LblTipoAccion;
        private System.Windows.Forms.ComboBox CmbTipoAccion;
        private System.Windows.Forms.Label LblBuscar;
        private System.Windows.Forms.TextBox TxtBuscar;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.Button BtnLimpiarFiltros;
        private System.Windows.Forms.Label LblTotalRegistros;
        private System.Windows.Forms.DataGridView DgvHistorial;
        private System.Windows.Forms.Button BtnRefrescar;
        private System.Windows.Forms.Button BtnVerDetalles;
        private System.Windows.Forms.Button BtnExportar;

          
    }
}