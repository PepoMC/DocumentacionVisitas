using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmEstadisticasVisitantes : BaseForm
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

        private void InitializeComponent()
        {
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.PicLogo = new System.Windows.Forms.PictureBox();
            this.BtnRegresar = new System.Windows.Forms.Button();

            this.GrpBusqueda = new System.Windows.Forms.GroupBox();
            this.TxtBuscar = new System.Windows.Forms.TextBox();
            this.LblBuscar = new System.Windows.Forms.Label();
            this.DgvVisitantes = new System.Windows.Forms.DataGridView();

            this.GrpFiltros = new System.Windows.Forms.GroupBox();
            this.RbHistorico = new System.Windows.Forms.RadioButton();
            this.RbPorDia = new System.Windows.Forms.RadioButton();
            this.RbPorSemana = new System.Windows.Forms.RadioButton();
            this.RbPorMes = new System.Windows.Forms.RadioButton();
            this.RbPorAnio = new System.Windows.Forms.RadioButton();

            this.LblFecha = new System.Windows.Forms.Label();
            this.DtpFecha = new System.Windows.Forms.DateTimePicker();
            this.LblAnio = new System.Windows.Forms.Label();
            this.NumAnio = new System.Windows.Forms.NumericUpDown();
            this.LblMes = new System.Windows.Forms.Label();
            this.CbxMes = new System.Windows.Forms.ComboBox();

            this.BtnConsultar = new System.Windows.Forms.Button();

            this.GrpResultados = new System.Windows.Forms.GroupBox();
            this.LblVisitanteSeleccionado = new System.Windows.Forms.Label();
            this.LblTotalVisitas = new System.Windows.Forms.Label();
            this.LblPeriodoActual = new System.Windows.Forms.Label();
            this.DgvVisitas = new System.Windows.Forms.DataGridView();
            this.BtnGenerarExcel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitantes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitas)).BeginInit();
            this.PanelSuperior.SuspendLayout();
            this.GrpBusqueda.SuspendLayout();
            this.GrpFiltros.SuspendLayout();
            this.GrpResultados.SuspendLayout();
            this.SuspendLayout();

            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Controls.Add(this.BtnRegresar);
            this.PanelSuperior.Controls.Add(this.LblTitulo);
            this.PanelSuperior.Controls.Add(this.PicLogo);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Location = new System.Drawing.Point(0, 0);
            this.PanelSuperior.Name = "PanelSuperior";
            this.PanelSuperior.Size = new System.Drawing.Size(1400, 80);

            this.PicLogo.Image = Properties.Resources.LogoConsejo2;
            this.PicLogo.Location = new System.Drawing.Point(20, 10);
            this.PicLogo.Name = "PicLogo";
            this.PicLogo.Size = new System.Drawing.Size(60, 60);
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.LblTitulo.AutoSize = false;
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(90, 20);
            this.LblTitulo.Name = "LblTitulo";
            this.LblTitulo.Size = new System.Drawing.Size(900, 40);
            this.LblTitulo.Text = "📊 Estadísticas de Visitantes";
            this.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.BtnRegresar.BackColor = System.Drawing.Color.FromArgb(247, 148, 29);
            this.BtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegresar.FlatAppearance.BorderSize = 0;
            this.BtnRegresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRegresar.ForeColor = System.Drawing.Color.White;
            this.BtnRegresar.Location = new System.Drawing.Point(1250, 20);
            this.BtnRegresar.Name = "BtnRegresar";
            this.BtnRegresar.Size = new System.Drawing.Size(130, 40);
            this.BtnRegresar.Text = "← Regresar";
            this.BtnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRegresar.Click += new System.EventHandler(this.BtnRegresar_Click);

            this.GrpBusqueda.BackColor = System.Drawing.Color.White;
            this.GrpBusqueda.Controls.Add(this.DgvVisitantes);
            this.GrpBusqueda.Controls.Add(this.TxtBuscar);
            this.GrpBusqueda.Controls.Add(this.LblBuscar);
            this.GrpBusqueda.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpBusqueda.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpBusqueda.Location = new System.Drawing.Point(20, 95);
            this.GrpBusqueda.Name = "GrpBusqueda";
            this.GrpBusqueda.Size = new System.Drawing.Size(650, 310);
            this.GrpBusqueda.Text = "🔍 1. Buscar y Seleccionar Visitante";

            this.LblBuscar.AutoSize = true;
            this.LblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblBuscar.ForeColor = System.Drawing.Color.Black;
            this.LblBuscar.Location = new System.Drawing.Point(20, 30);
            this.LblBuscar.Name = "LblBuscar";
            this.LblBuscar.Size = new System.Drawing.Size(126, 19);
            this.LblBuscar.Text = "Nombre/Apellido:";

            this.TxtBuscar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtBuscar.Location = new System.Drawing.Point(155, 27);
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.Size = new System.Drawing.Size(475, 27);
            this.TxtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBuscar.TextChanged += new System.EventHandler(this.TxtBuscar_TextChanged);

            this.DgvVisitantes.AllowUserToAddRows = false;
            this.DgvVisitantes.AllowUserToDeleteRows = false;
            this.DgvVisitantes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvVisitantes.BackgroundColor = System.Drawing.Color.White;
            this.DgvVisitantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVisitantes.Location = new System.Drawing.Point(20, 65);
            this.DgvVisitantes.MultiSelect = false;
            this.DgvVisitantes.Name = "DgvVisitantes";
            this.DgvVisitantes.ReadOnly = true;
            this.DgvVisitantes.RowHeadersVisible = false;
            this.DgvVisitantes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVisitantes.Size = new System.Drawing.Size(610, 230);
            this.DgvVisitantes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVisitantes_CellClick);

            this.GrpFiltros.BackColor = System.Drawing.Color.White;
            this.GrpFiltros.Controls.Add(this.BtnConsultar);
            this.GrpFiltros.Controls.Add(this.CbxMes);
            this.GrpFiltros.Controls.Add(this.LblMes);
            this.GrpFiltros.Controls.Add(this.NumAnio);
            this.GrpFiltros.Controls.Add(this.LblAnio);
            this.GrpFiltros.Controls.Add(this.DtpFecha);
            this.GrpFiltros.Controls.Add(this.LblFecha);
            this.GrpFiltros.Controls.Add(this.RbHistorico);
            this.GrpFiltros.Controls.Add(this.RbPorAnio);
            this.GrpFiltros.Controls.Add(this.RbPorMes);
            this.GrpFiltros.Controls.Add(this.RbPorSemana);
            this.GrpFiltros.Controls.Add(this.RbPorDia);

            this.GrpFiltros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpFiltros.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpFiltros.Location = new System.Drawing.Point(690, 95);
            this.GrpFiltros.Name = "GrpFiltros";
            this.GrpFiltros.Size = new System.Drawing.Size(690, 310);
            this.GrpFiltros.Text = "⚙️ 2. Seleccionar Período y Consultar";

            this.RbHistorico.AutoSize = true;
            this.RbHistorico.Checked = true;
            this.RbHistorico.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbHistorico.ForeColor = System.Drawing.Color.Black;
            this.RbHistorico.Location = new System.Drawing.Point(30, 40);
            this.RbHistorico.Name = "RbHistorico";
            this.RbHistorico.Size = new System.Drawing.Size(220, 24);
            this.RbHistorico.TabStop = true;
            this.RbHistorico.Text = "📜 Histórico (Todas las visitas)";
            this.RbHistorico.CheckedChanged += new System.EventHandler(this.RbTipoFiltro_CheckedChanged);

            this.RbPorDia.AutoSize = true;
            this.RbPorDia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbPorDia.ForeColor = System.Drawing.Color.Black;
            this.RbPorDia.Location = new System.Drawing.Point(30, 80);
            this.RbPorDia.Name = "RbPorDia";
            this.RbPorDia.Size = new System.Drawing.Size(100, 24);
            this.RbPorDia.Text = "📅 Por Día";
            this.RbPorDia.CheckedChanged += new System.EventHandler(this.RbTipoFiltro_CheckedChanged);

            this.RbPorSemana.AutoSize = true;
            this.RbPorSemana.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbPorSemana.ForeColor = System.Drawing.Color.Black;
            this.RbPorSemana.Location = new System.Drawing.Point(30, 120);
            this.RbPorSemana.Name = "RbPorSemana";
            this.RbPorSemana.Size = new System.Drawing.Size(135, 24);
            this.RbPorSemana.Text = "📆 Por Semana";
            this.RbPorSemana.CheckedChanged += new System.EventHandler(this.RbTipoFiltro_CheckedChanged);

            this.RbPorMes.AutoSize = true;
            this.RbPorMes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbPorMes.ForeColor = System.Drawing.Color.Black;
            this.RbPorMes.Location = new System.Drawing.Point(30, 160);
            this.RbPorMes.Name = "RbPorMes";
            this.RbPorMes.Size = new System.Drawing.Size(105, 24);
            this.RbPorMes.Text = "📊 Por Mes";
            this.RbPorMes.CheckedChanged += new System.EventHandler(this.RbTipoFiltro_CheckedChanged);

            this.RbPorAnio.AutoSize = true;
            this.RbPorAnio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbPorAnio.ForeColor = System.Drawing.Color.Black;
            this.RbPorAnio.Location = new System.Drawing.Point(30, 200);
            this.RbPorAnio.Name = "RbPorAnio";
            this.RbPorAnio.Size = new System.Drawing.Size(105, 24);
            this.RbPorAnio.Text = "📈 Por Año";
            this.RbPorAnio.CheckedChanged += new System.EventHandler(this.RbTipoFiltro_CheckedChanged);

            this.LblFecha.AutoSize = true;
            this.LblFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblFecha.ForeColor = System.Drawing.Color.Black;
            this.LblFecha.Location = new System.Drawing.Point(250, 82);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(47, 19);
            this.LblFecha.Text = "Fecha:";
            this.LblFecha.Visible = false;

            this.DtpFecha.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.DtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFecha.Location = new System.Drawing.Point(310, 78);
            this.DtpFecha.Name = "DtpFecha";
            this.DtpFecha.Size = new System.Drawing.Size(150, 27);
            this.DtpFecha.Visible = false;

            this.LblAnio.AutoSize = true;
            this.LblAnio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblAnio.ForeColor = System.Drawing.Color.Black;
            this.LblAnio.Location = new System.Drawing.Point(250, 202);
            this.LblAnio.Name = "LblAnio";
            this.LblAnio.Size = new System.Drawing.Size(37, 19);
            this.LblAnio.Text = "Año:";
            this.LblAnio.Visible = false;

            this.NumAnio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.NumAnio.Location = new System.Drawing.Point(310, 198);
            this.NumAnio.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.NumAnio.Minimum = new decimal(new int[] { 2020, 0, 0, 0 });
            this.NumAnio.Name = "NumAnio";
            this.NumAnio.Size = new System.Drawing.Size(150, 27);
            this.NumAnio.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            this.NumAnio.Visible = false;

            this.LblMes.AutoSize = true;
            this.LblMes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblMes.ForeColor = System.Drawing.Color.Black;
            this.LblMes.Location = new System.Drawing.Point(250, 162);
            this.LblMes.Name = "LblMes";
            this.LblMes.Size = new System.Drawing.Size(37, 19);
            this.LblMes.Text = "Mes:";
            this.LblMes.Visible = false;

            this.CbxMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxMes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.CbxMes.FormattingEnabled = true;
            this.CbxMes.Items.AddRange(new object[] {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            });
            this.CbxMes.Location = new System.Drawing.Point(310, 158);
            this.CbxMes.Name = "CbxMes";
            this.CbxMes.Size = new System.Drawing.Size(150, 28);
            this.CbxMes.Visible = false;

            this.BtnConsultar.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConsultar.FlatAppearance.BorderSize = 0;
            this.BtnConsultar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnConsultar.ForeColor = System.Drawing.Color.White;
            this.BtnConsultar.Location = new System.Drawing.Point(480, 40);
            this.BtnConsultar.Name = "BtnConsultar";
            this.BtnConsultar.Size = new System.Drawing.Size(190, 185);
            this.BtnConsultar.Text = "🔍 CONSULTAR\r\nVISITAS";
            this.BtnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnConsultar.Click += new System.EventHandler(this.BtnConsultar_Click);

            this.GrpResultados.BackColor = System.Drawing.Color.White;
            this.GrpResultados.Controls.Add(this.BtnGenerarExcel);
            this.GrpResultados.Controls.Add(this.DgvVisitas);
            this.GrpResultados.Controls.Add(this.LblPeriodoActual);
            this.GrpResultados.Controls.Add(this.LblTotalVisitas);
            this.GrpResultados.Controls.Add(this.LblVisitanteSeleccionado);
            this.GrpResultados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GrpResultados.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpResultados.Location = new System.Drawing.Point(20, 420);
            this.GrpResultados.Name = "GrpResultados";
            this.GrpResultados.Size = new System.Drawing.Size(1360, 330);
            this.GrpResultados.Text = "📊 3. Resultados Detallados";

            this.LblVisitanteSeleccionado.AutoSize = true;
            this.LblVisitanteSeleccionado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.LblVisitanteSeleccionado.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblVisitanteSeleccionado.Location = new System.Drawing.Point(20, 30);
            this.LblVisitanteSeleccionado.Name = "LblVisitanteSeleccionado";
            this.LblVisitanteSeleccionado.Size = new System.Drawing.Size(300, 21);
            this.LblVisitanteSeleccionado.Text = "Selecciona un visitante de la lista arriba";

            this.LblTotalVisitas.AutoSize = true;
            this.LblTotalVisitas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.LblTotalVisitas.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.LblTotalVisitas.Location = new System.Drawing.Point(500, 30);
            this.LblTotalVisitas.Name = "LblTotalVisitas";
            this.LblTotalVisitas.Size = new System.Drawing.Size(0, 21);
            this.LblTotalVisitas.Text = "";

            this.LblPeriodoActual.AutoSize = true;
            this.LblPeriodoActual.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblPeriodoActual.ForeColor = System.Drawing.Color.Gray;
            this.LblPeriodoActual.Location = new System.Drawing.Point(20, 55);
            this.LblPeriodoActual.Name = "LblPeriodoActual";
            this.LblPeriodoActual.Size = new System.Drawing.Size(0, 20);
            this.LblPeriodoActual.Text = "";

            this.BtnGenerarExcel.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.BtnGenerarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerarExcel.FlatAppearance.BorderSize = 0;
            this.BtnGenerarExcel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnGenerarExcel.ForeColor = System.Drawing.Color.White;
            this.BtnGenerarExcel.Location = new System.Drawing.Point(1150, 25);
            this.BtnGenerarExcel.Name = "BtnGenerarExcel";
            this.BtnGenerarExcel.Size = new System.Drawing.Size(180, 45);
            this.BtnGenerarExcel.Text = "📊 Generar Excel";
            this.BtnGenerarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenerarExcel.Click += new System.EventHandler(this.BtnGenerarExcel_Click);

            this.DgvVisitas.AllowUserToAddRows = false;
            this.DgvVisitas.AllowUserToDeleteRows = false;
            this.DgvVisitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvVisitas.BackgroundColor = System.Drawing.Color.White;
            this.DgvVisitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVisitas.Location = new System.Drawing.Point(20, 80);
            this.DgvVisitas.Name = "DgvVisitas";
            this.DgvVisitas.ReadOnly = true;
            this.DgvVisitas.RowHeadersVisible = false;
            this.DgvVisitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVisitas.Size = new System.Drawing.Size(1320, 230);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.ClientSize = new System.Drawing.Size(1400, 770);
            this.Controls.Add(this.GrpResultados);
            this.Controls.Add(this.GrpFiltros);
            this.Controls.Add(this.GrpBusqueda);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.Name = "FrmEstadisticasVisitantes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estadísticas de Visitantes - Consejo Ciudadano";
            this.Load += new System.EventHandler(this.FrmEstadisticasVisitantes_Load);

            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitantes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitas)).EndInit();
            this.PanelSuperior.ResumeLayout(false);
            this.GrpBusqueda.ResumeLayout(false);
            this.GrpBusqueda.PerformLayout();
            this.GrpFiltros.ResumeLayout(false);
            this.GrpFiltros.PerformLayout();
            this.GrpResultados.ResumeLayout(false);
            this.GrpResultados.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Button BtnRegresar;

        private System.Windows.Forms.GroupBox GrpBusqueda;
        private System.Windows.Forms.TextBox TxtBuscar;
        private System.Windows.Forms.Label LblBuscar;
        private System.Windows.Forms.DataGridView DgvVisitantes;

        private System.Windows.Forms.GroupBox GrpFiltros;
        private System.Windows.Forms.RadioButton RbHistorico;
        private System.Windows.Forms.RadioButton RbPorDia;
        private System.Windows.Forms.RadioButton RbPorSemana;
        private System.Windows.Forms.RadioButton RbPorMes;
        private System.Windows.Forms.RadioButton RbPorAnio;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.DateTimePicker DtpFecha;
        private System.Windows.Forms.Label LblAnio;
        private System.Windows.Forms.NumericUpDown NumAnio;
        private System.Windows.Forms.Label LblMes;
        private System.Windows.Forms.ComboBox CbxMes;
        private System.Windows.Forms.Button BtnConsultar;

        private System.Windows.Forms.GroupBox GrpResultados;
        private System.Windows.Forms.Label LblVisitanteSeleccionado;
        private System.Windows.Forms.Label LblTotalVisitas;
        private System.Windows.Forms.Label LblPeriodoActual;
        private System.Windows.Forms.DataGridView DgvVisitas;
        private System.Windows.Forms.Button BtnGenerarExcel;
    }
}