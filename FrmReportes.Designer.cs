using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmReportes : BaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.BtnRegresar = new System.Windows.Forms.Button();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.PanelContenido = new System.Windows.Forms.Panel();
            this.GrpAcciones = new System.Windows.Forms.GroupBox();
            this.BtnGenerarExcel = new System.Windows.Forms.Button();
            this.LblInfo = new System.Windows.Forms.Label();
            this.GrpParametros = new System.Windows.Forms.GroupBox();
            this.CbxMes = new System.Windows.Forms.ComboBox();
            this.LblMes = new System.Windows.Forms.Label();
            this.NumAnio = new System.Windows.Forms.NumericUpDown();
            this.LblAnio = new System.Windows.Forms.Label();
            this.DtpFecha = new System.Windows.Forms.DateTimePicker();
            this.LblFecha = new System.Windows.Forms.Label();
            this.GrpTipoReporte = new System.Windows.Forms.GroupBox();
            this.RbAnio = new System.Windows.Forms.RadioButton();
            this.RbMes = new System.Windows.Forms.RadioButton();
            this.RbSemana = new System.Windows.Forms.RadioButton();
            this.RbDia = new System.Windows.Forms.RadioButton();
            this.PanelSuperior.SuspendLayout();
            this.PanelContenido.SuspendLayout();
            this.GrpAcciones.SuspendLayout();
            this.GrpParametros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumAnio)).BeginInit();
            this.GrpTipoReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelSuperior
            // 
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.PanelSuperior.Controls.Add(this.BtnRegresar);
            this.PanelSuperior.Controls.Add(this.LblTitulo);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Location = new System.Drawing.Point(0, 0);
            this.PanelSuperior.Name = "PanelSuperior";
            this.PanelSuperior.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.PanelSuperior.Size = new System.Drawing.Size(800, 80);
            this.PanelSuperior.TabIndex = 1;
            // 
            // BtnRegresar
            // 
            this.BtnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(148)))), ((int)(((byte)(29)))));
            this.BtnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRegresar.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnRegresar.FlatAppearance.BorderSize = 0;
            this.BtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRegresar.ForeColor = System.Drawing.Color.White;
            this.BtnRegresar.Location = new System.Drawing.Point(620, 10);
            this.BtnRegresar.Name = "BtnRegresar";
            this.BtnRegresar.Size = new System.Drawing.Size(150, 60);
            this.BtnRegresar.TabIndex = 0;
            this.BtnRegresar.Text = "⬅️ Regresar";
            this.BtnRegresar.UseVisualStyleBackColor = false;
            this.BtnRegresar.Click += new System.EventHandler(this.BtnRegresar_Click);
            // 
            // LblTitulo
            // 
            this.LblTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.White;
            this.LblTitulo.Location = new System.Drawing.Point(30, 10);
            this.LblTitulo.Name = "LblTitulo";
            this.LblTitulo.Size = new System.Drawing.Size(500, 60);
            this.LblTitulo.TabIndex = 1;
            this.LblTitulo.Text = "📊 Generador de Reportes";
            this.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelContenido
            // 
            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.PanelContenido.Controls.Add(this.GrpAcciones);
            this.PanelContenido.Controls.Add(this.GrpParametros);
            this.PanelContenido.Controls.Add(this.GrpTipoReporte);
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Location = new System.Drawing.Point(0, 80);
            this.PanelContenido.Name = "PanelContenido";
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(40);
            this.PanelContenido.Size = new System.Drawing.Size(800, 470);
            this.PanelContenido.TabIndex = 0;
            this.PanelContenido.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContenido_Paint);
            // 
            // GrpAcciones
            // 
            this.GrpAcciones.Controls.Add(this.BtnGenerarExcel);
            this.GrpAcciones.Controls.Add(this.LblInfo);
            this.GrpAcciones.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.GrpAcciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.GrpAcciones.Location = new System.Drawing.Point(50, 290);
            this.GrpAcciones.Name = "GrpAcciones";
            this.GrpAcciones.Padding = new System.Windows.Forms.Padding(20);
            this.GrpAcciones.Size = new System.Drawing.Size(680, 180);
            this.GrpAcciones.TabIndex = 0;
            this.GrpAcciones.TabStop = false;
            this.GrpAcciones.Text = "⚡ Acciones";
            // 
            // BtnGenerarExcel
            // 
            this.BtnGenerarExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnGenerarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenerarExcel.FlatAppearance.BorderSize = 0;
            this.BtnGenerarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerarExcel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnGenerarExcel.ForeColor = System.Drawing.Color.White;
            this.BtnGenerarExcel.Location = new System.Drawing.Point(100, 100);
            this.BtnGenerarExcel.Name = "BtnGenerarExcel";
            this.BtnGenerarExcel.Size = new System.Drawing.Size(480, 60);
            this.BtnGenerarExcel.TabIndex = 0;
            this.BtnGenerarExcel.Text = "📥 GENERAR REPORTE EXCEL";
            this.BtnGenerarExcel.UseVisualStyleBackColor = false;
            this.BtnGenerarExcel.Click += new System.EventHandler(this.BtnGenerarExcel_Click);
            // 
            // LblInfo
            // 
            this.LblInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.LblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.LblInfo.Location = new System.Drawing.Point(30, 35);
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(620, 50);
            this.LblInfo.TabIndex = 1;
            this.LblInfo.Text = "ℹ️ Selecciona el tipo de reporte y los parámetros, luego presiona el botón para g" +
    "enerar el archivo Excel.";
            // 
            // GrpParametros
            // 
            this.GrpParametros.Controls.Add(this.CbxMes);
            this.GrpParametros.Controls.Add(this.LblMes);
            this.GrpParametros.Controls.Add(this.NumAnio);
            this.GrpParametros.Controls.Add(this.LblAnio);
            this.GrpParametros.Controls.Add(this.DtpFecha);
            this.GrpParametros.Controls.Add(this.LblFecha);
            this.GrpParametros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.GrpParametros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.GrpParametros.Location = new System.Drawing.Point(380, 20);
            this.GrpParametros.Name = "GrpParametros";
            this.GrpParametros.Padding = new System.Windows.Forms.Padding(20);
            this.GrpParametros.Size = new System.Drawing.Size(350, 250);
            this.GrpParametros.TabIndex = 1;
            this.GrpParametros.TabStop = false;
            this.GrpParametros.Text = "⚙️ Parámetros";
            // 
            // CbxMes
            // 
            this.CbxMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxMes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.CbxMes.Items.AddRange(new object[] {
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.CbxMes.Location = new System.Drawing.Point(30, 150);
            this.CbxMes.Name = "CbxMes";
            this.CbxMes.Size = new System.Drawing.Size(290, 28);
            this.CbxMes.TabIndex = 0;
            this.CbxMes.Visible = false;
            // 
            // LblMes
            // 
            this.LblMes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblMes.Location = new System.Drawing.Point(30, 120);
            this.LblMes.Name = "LblMes";
            this.LblMes.Size = new System.Drawing.Size(290, 25);
            this.LblMes.TabIndex = 1;
            this.LblMes.Text = "Mes:";
            this.LblMes.Visible = false;
            // 
            // NumAnio
            // 
            this.NumAnio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.NumAnio.Location = new System.Drawing.Point(30, 150);
            this.NumAnio.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.NumAnio.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.NumAnio.Name = "NumAnio";
            this.NumAnio.Size = new System.Drawing.Size(290, 27);
            this.NumAnio.TabIndex = 2;
            this.NumAnio.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.NumAnio.Visible = false;
            // 
            // LblAnio
            // 
            this.LblAnio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblAnio.Location = new System.Drawing.Point(30, 120);
            this.LblAnio.Name = "LblAnio";
            this.LblAnio.Size = new System.Drawing.Size(290, 25);
            this.LblAnio.TabIndex = 3;
            this.LblAnio.Text = "Año:";
            this.LblAnio.Visible = false;
            // 
            // DtpFecha
            // 
            this.DtpFecha.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.DtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFecha.Location = new System.Drawing.Point(30, 70);
            this.DtpFecha.Name = "DtpFecha";
            this.DtpFecha.Size = new System.Drawing.Size(290, 27);
            this.DtpFecha.TabIndex = 4;
            // 
            // LblFecha
            // 
            this.LblFecha.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblFecha.Location = new System.Drawing.Point(30, 40);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(290, 25);
            this.LblFecha.TabIndex = 5;
            this.LblFecha.Text = "Selecciona la fecha:";
            // 
            // GrpTipoReporte
            // 
            this.GrpTipoReporte.Controls.Add(this.RbAnio);
            this.GrpTipoReporte.Controls.Add(this.RbMes);
            this.GrpTipoReporte.Controls.Add(this.RbSemana);
            this.GrpTipoReporte.Controls.Add(this.RbDia);
            this.GrpTipoReporte.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.GrpTipoReporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.GrpTipoReporte.Location = new System.Drawing.Point(50, 20);
            this.GrpTipoReporte.Name = "GrpTipoReporte";
            this.GrpTipoReporte.Padding = new System.Windows.Forms.Padding(20);
            this.GrpTipoReporte.Size = new System.Drawing.Size(300, 250);
            this.GrpTipoReporte.TabIndex = 2;
            this.GrpTipoReporte.TabStop = false;
            this.GrpTipoReporte.Text = "🔍 Tipo de Reporte";
            // 
            // RbAnio
            // 
            this.RbAnio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbAnio.Location = new System.Drawing.Point(30, 190);
            this.RbAnio.Name = "RbAnio";
            this.RbAnio.Size = new System.Drawing.Size(250, 40);
            this.RbAnio.TabIndex = 0;
            this.RbAnio.Text = "📈 Reporte por Año";
            this.RbAnio.CheckedChanged += new System.EventHandler(this.RbTipoReporte_CheckedChanged);
            // 
            // RbMes
            // 
            this.RbMes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbMes.Location = new System.Drawing.Point(30, 140);
            this.RbMes.Name = "RbMes";
            this.RbMes.Size = new System.Drawing.Size(250, 40);
            this.RbMes.TabIndex = 1;
            this.RbMes.Text = "📊 Reporte por Mes";
            this.RbMes.CheckedChanged += new System.EventHandler(this.RbTipoReporte_CheckedChanged);
            // 
            // RbSemana
            // 
            this.RbSemana.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbSemana.Location = new System.Drawing.Point(30, 90);
            this.RbSemana.Name = "RbSemana";
            this.RbSemana.Size = new System.Drawing.Size(250, 40);
            this.RbSemana.TabIndex = 2;
            this.RbSemana.Text = "📆 Reporte por Semana";
            this.RbSemana.CheckedChanged += new System.EventHandler(this.RbTipoReporte_CheckedChanged);
            // 
            // RbDia
            // 
            this.RbDia.Checked = true;
            this.RbDia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.RbDia.Location = new System.Drawing.Point(30, 40);
            this.RbDia.Name = "RbDia";
            this.RbDia.Size = new System.Drawing.Size(250, 40);
            this.RbDia.TabIndex = 3;
            this.RbDia.TabStop = true;
            this.RbDia.Text = "📅 Reporte por Día";
            this.RbDia.CheckedChanged += new System.EventHandler(this.RbTipoReporte_CheckedChanged);
            // 
            // FrmReportes
            // 
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.ControlBox = false;
            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generador de Reportes - Consejo Ciudadano";
            this.PanelSuperior.ResumeLayout(false);
            this.PanelContenido.ResumeLayout(false);
            this.GrpAcciones.ResumeLayout(false);
            this.GrpParametros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumAnio)).EndInit();
            this.GrpTipoReporte.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.Button BtnRegresar;
        private System.Windows.Forms.Panel PanelContenido;
        private System.Windows.Forms.GroupBox GrpTipoReporte;
        private System.Windows.Forms.RadioButton RbDia;
        private System.Windows.Forms.RadioButton RbSemana;
        private System.Windows.Forms.RadioButton RbMes;
        private System.Windows.Forms.RadioButton RbAnio;
        private System.Windows.Forms.GroupBox GrpParametros;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.DateTimePicker DtpFecha;
        private System.Windows.Forms.Label LblAnio;
        private System.Windows.Forms.NumericUpDown NumAnio;
        private System.Windows.Forms.Label LblMes;
        private System.Windows.Forms.ComboBox CbxMes;
        private System.Windows.Forms.GroupBox GrpAcciones;
        private System.Windows.Forms.Button BtnGenerarExcel;
        private System.Windows.Forms.Label LblInfo;
    }
}




































