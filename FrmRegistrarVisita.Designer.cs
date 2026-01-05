using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmRegistrarVisita : BaseForm
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
            // DECLARACIÓN DE CONTROLES
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.LblTituloPrincipal = new System.Windows.Forms.Label();
            this.PicLogo = new System.Windows.Forms.PictureBox();

            this.PanelContenido = new System.Windows.Forms.Panel();
            this.PanelRegistroEntrada = new System.Windows.Forms.Panel();
            this.PanelSalidaActivas = new System.Windows.Forms.Panel();

            this.GrpVisitante = new System.Windows.Forms.GroupBox();
            this.CbxVisitantesFrecuentes = new System.Windows.Forms.ComboBox();
            this.LblVisitantesFrecuentes = new System.Windows.Forms.Label();
            this.TxtNombreV = new System.Windows.Forms.TextBox();
            this.LblNombreV = new System.Windows.Forms.Label();
            this.TxtAPV = new System.Windows.Forms.TextBox();
            this.LblPrimerApellidoV = new System.Windows.Forms.Label();
            this.TxtAMV = new System.Windows.Forms.TextBox();
            this.LblSegundoApellidoV = new System.Windows.Forms.Label();
            this.CbxCompania = new System.Windows.Forms.ComboBox();
            this.LblCompania = new System.Windows.Forms.Label();
            this.CkbGuardarVisFrec = new System.Windows.Forms.CheckBox();

            this.GrpPersonaVisitada = new System.Windows.Forms.GroupBox();
            this.CbxPersonasVisitadasFrecuentes = new System.Windows.Forms.ComboBox();
            this.LblPersonasFrecuentesE = new System.Windows.Forms.Label();
            this.TxtNombreE = new System.Windows.Forms.TextBox();
            this.LblNombreE = new System.Windows.Forms.Label();
            this.TxtAPE = new System.Windows.Forms.TextBox();
            this.LblPrimerApellidoE = new System.Windows.Forms.Label();
            this.TxtAME = new System.Windows.Forms.TextBox();
            this.LblSegundoApellidoE = new System.Windows.Forms.Label();
            this.CbxDepartamento = new System.Windows.Forms.ComboBox();
            this.LblDepartamento = new System.Windows.Forms.Label();
            this.CkbGuardarPerVisFrec = new System.Windows.Forms.CheckBox();

            this.GrpAcciones = new System.Windows.Forms.GroupBox();
            this.BtnIniciarVisita = new System.Windows.Forms.Button();
            this.BtnGuardarVisita = new System.Windows.Forms.Button();
            this.BtnLimpiar = new System.Windows.Forms.Button();
            this.BtnCorregirDatos = new System.Windows.Forms.Button(); // ← NUEVO
            this.BtnCancelarVisita = new System.Windows.Forms.Button();
            this.LblContadorVisitas = new System.Windows.Forms.Label();
            this.LblTituloContador = new System.Windows.Forms.Label();

            this.PanelEncabezadoSalidas = new System.Windows.Forms.Panel();
            this.LblTituloSalidas = new System.Windows.Forms.Label();
            this.PanelBusqueda = new System.Windows.Forms.Panel();
            this.TxtBuscarVisitante = new System.Windows.Forms.TextBox();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.LblBuscar = new System.Windows.Forms.Label();

            this.DgvVisitas = new System.Windows.Forms.DataGridView();
            this.PanelBotonesInferior = new System.Windows.Forms.Panel();
            this.BtnTerminarVisita = new System.Windows.Forms.Button();
            this.BtnRegresar = new System.Windows.Forms.Button();

            this.PanelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).BeginInit();
            this.PanelContenido.SuspendLayout();
            this.PanelRegistroEntrada.SuspendLayout();
            this.PanelSalidaActivas.SuspendLayout();
            this.GrpVisitante.SuspendLayout();
            this.GrpPersonaVisitada.SuspendLayout();
            this.GrpAcciones.SuspendLayout();
            this.PanelEncabezadoSalidas.SuspendLayout();
            this.PanelBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitas)).BeginInit();
            this.PanelBotonesInferior.SuspendLayout();
            this.SuspendLayout();

            // PANEL SUPERIOR CON LOGO MÁS GRANDE
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.PanelSuperior.Controls.Add(this.PicLogo);
            this.PanelSuperior.Controls.Add(this.LblTituloPrincipal);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Size = new System.Drawing.Size(1200, 80);
            this.PanelSuperior.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);

            this.LblTituloPrincipal.Text = "Control de Visitas";
            this.LblTituloPrincipal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.LblTituloPrincipal.ForeColor = System.Drawing.Color.White;
            this.LblTituloPrincipal.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblTituloPrincipal.Width = 400;
            this.LblTituloPrincipal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // LOGO MÁS GRANDE (120px en lugar de 80px)
            this.PicLogo.Image = Properties.Resources.LogoConsejo2;
            this.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.PicLogo.Width = 120;
            this.PicLogo.BackColor = System.Drawing.Color.Transparent;
            this.PicLogo.Padding = new System.Windows.Forms.Padding(10);

            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(253, 246, 249);
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.PanelContenido.AutoScroll = true;

            this.PanelRegistroEntrada.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelRegistroEntrada.Height = 360;
            this.PanelRegistroEntrada.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);

            // GRUPO VISITANTE
            this.GrpVisitante.Text = "🚶 Datos del Visitante";
            this.GrpVisitante.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.GrpVisitante.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpVisitante.Dock = System.Windows.Forms.DockStyle.Left;
            this.GrpVisitante.Width = 380;
            this.GrpVisitante.Padding = new System.Windows.Forms.Padding(20, 35, 20, 20);

            this.LblVisitantesFrecuentes.Text = "Visitantes frecuentes:";
            this.LblVisitantesFrecuentes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblVisitantesFrecuentes.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblVisitantesFrecuentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblVisitantesFrecuentes.Height = 25;

            this.CbxVisitantesFrecuentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.CbxVisitantesFrecuentes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CbxVisitantesFrecuentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbxVisitantesFrecuentes.Height = 30;
            this.CbxVisitantesFrecuentes.SelectedIndexChanged += new System.EventHandler(this.CbxVisitantesFrecuentes_SelectedIndexChanged);

            this.LblNombreV.Text = "Nombre:";
            this.LblNombreV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblNombreV.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNombreV.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblNombreV.Height = 25;
            this.LblNombreV.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.TxtNombreV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtNombreV.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtNombreV.Height = 30;

            this.LblPrimerApellidoV.Text = "Primer Apellido:";
            this.LblPrimerApellidoV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblPrimerApellidoV.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblPrimerApellidoV.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPrimerApellidoV.Height = 25;
            this.LblPrimerApellidoV.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.TxtAPV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtAPV.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAPV.Height = 30;

            this.LblSegundoApellidoV.Text = "Segundo Apellido:";
            this.LblSegundoApellidoV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblSegundoApellidoV.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblSegundoApellidoV.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSegundoApellidoV.Height = 25;
            this.LblSegundoApellidoV.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.TxtAMV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtAMV.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAMV.Height = 30;

            this.LblCompania.Text = "Compañía:";
            this.LblCompania.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblCompania.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblCompania.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCompania.Height = 25;
            this.LblCompania.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.CbxCompania = new System.Windows.Forms.ComboBox();
            this.CbxCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.CbxCompania.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CbxCompania.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbxCompania.Height = 30;
            this.CbxCompania.Items.Add("N/A");
            this.CbxCompania.SelectedIndex = 0;

            this.CkbGuardarVisFrec.Text = "⭐ Agregar a visitantes frecuentes";
            this.CkbGuardarVisFrec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CkbGuardarVisFrec.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.CkbGuardarVisFrec.Dock = System.Windows.Forms.DockStyle.Top;
            this.CkbGuardarVisFrec.Height = 30;
            this.CkbGuardarVisFrec.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.GrpVisitante.Controls.Add(this.CkbGuardarVisFrec);
            this.GrpVisitante.Controls.Add(this.CbxCompania);
            this.GrpVisitante.Controls.Add(this.LblCompania);
            this.GrpVisitante.Controls.Add(this.TxtAMV);
            this.GrpVisitante.Controls.Add(this.LblSegundoApellidoV);
            this.GrpVisitante.Controls.Add(this.TxtAPV);
            this.GrpVisitante.Controls.Add(this.LblPrimerApellidoV);
            this.GrpVisitante.Controls.Add(this.TxtNombreV);
            this.GrpVisitante.Controls.Add(this.LblNombreV);
            this.GrpVisitante.Controls.Add(this.CbxVisitantesFrecuentes);
            this.GrpVisitante.Controls.Add(this.LblVisitantesFrecuentes);

            // GRUPO PERSONA VISITADA
            this.GrpPersonaVisitada.Text = "👤 Persona Visitada";
            this.GrpPersonaVisitada.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.GrpPersonaVisitada.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpPersonaVisitada.Dock = System.Windows.Forms.DockStyle.Left;
            this.GrpPersonaVisitada.Width = 380;
            this.GrpPersonaVisitada.Padding = new System.Windows.Forms.Padding(20, 35, 20, 20);
            this.GrpPersonaVisitada.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);

            this.LblPersonasFrecuentesE.Text = "Personas visitadas frecuentes:";
            this.LblPersonasFrecuentesE.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblPersonasFrecuentesE.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblPersonasFrecuentesE.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPersonasFrecuentesE.Height = 25;

            this.CbxPersonasVisitadasFrecuentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.CbxPersonasVisitadasFrecuentes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CbxPersonasVisitadasFrecuentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbxPersonasVisitadasFrecuentes.Height = 30;
            this.CbxPersonasVisitadasFrecuentes.SelectedIndexChanged += new System.EventHandler(this.CbxPersonasVisitadasFrecuentes_SelectedIndexChanged);

            this.LblNombreE.Text = "Nombre:";
            this.LblNombreE.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblNombreE.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblNombreE.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblNombreE.Height = 25;
            this.LblNombreE.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.TxtNombreE.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtNombreE.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtNombreE.Height = 30;

            this.LblPrimerApellidoE.Text = "Primer Apellido:";
            this.LblPrimerApellidoE.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblPrimerApellidoE.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblPrimerApellidoE.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPrimerApellidoE.Height = 25;
            this.LblPrimerApellidoE.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.TxtAPE.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtAPE.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAPE.Height = 30;

            this.LblSegundoApellidoE.Text = "Segundo Apellido:";
            this.LblSegundoApellidoE.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblSegundoApellidoE.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblSegundoApellidoE.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSegundoApellidoE.Height = 25;
            this.LblSegundoApellidoE.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.TxtAME.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtAME.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAME.Height = 30;

            this.LblDepartamento.Text = "Departamento:";
            this.LblDepartamento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDepartamento.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblDepartamento.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblDepartamento.Height = 25;
            this.LblDepartamento.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);

            this.CbxDepartamento = new System.Windows.Forms.ComboBox();
            this.CbxDepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.CbxDepartamento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CbxDepartamento.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbxDepartamento.Height = 30;
            this.CbxDepartamento.Items.AddRange(new object[] {
                "Calidad", "Centro de Contacto", "Recursos Humanos", "Recursos Materiales",
                "Psicología", "Jurídico", "Trata de Personas", "Datos", "Comunicación",
                "Tecnología de la Información", "Presidencia", "Comisiones"
            });

            this.CkbGuardarPerVisFrec.Text = "⭐ Agregar a personas visitadas frecuentes";
            this.CkbGuardarPerVisFrec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CkbGuardarPerVisFrec.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.CkbGuardarPerVisFrec.Dock = System.Windows.Forms.DockStyle.Top;
            this.CkbGuardarPerVisFrec.Height = 30;
            this.CkbGuardarPerVisFrec.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.GrpPersonaVisitada.Controls.Add(this.CkbGuardarPerVisFrec);
            this.GrpPersonaVisitada.Controls.Add(this.CbxDepartamento);
            this.GrpPersonaVisitada.Controls.Add(this.LblDepartamento);
            this.GrpPersonaVisitada.Controls.Add(this.TxtAME);
            this.GrpPersonaVisitada.Controls.Add(this.LblSegundoApellidoE);
            this.GrpPersonaVisitada.Controls.Add(this.TxtAPE);
            this.GrpPersonaVisitada.Controls.Add(this.LblPrimerApellidoE);
            this.GrpPersonaVisitada.Controls.Add(this.TxtNombreE);
            this.GrpPersonaVisitada.Controls.Add(this.LblNombreE);
            this.GrpPersonaVisitada.Controls.Add(this.CbxPersonasVisitadasFrecuentes);
            this.GrpPersonaVisitada.Controls.Add(this.LblPersonasFrecuentesE);

            // ═════════════════════════════════════════════════════════
            // GRUPO ACCIONES - CON BOTÓN CORREGIR DATOS
            // ═════════════════════════════════════════════════════════
            this.GrpAcciones.Text = "⚡ Acciones";
            this.GrpAcciones.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.GrpAcciones.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.GrpAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpAcciones.Padding = new System.Windows.Forms.Padding(25, 30, 25, 15);
            this.GrpAcciones.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);

            // CONTADOR DE VISITAS DEL DÍA
            this.LblTituloContador.Text = "📊 Visitas registradas hoy:";
            this.LblTituloContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTituloContador.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblTituloContador.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTituloContador.Height = 25;
            this.LblTituloContador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.LblContadorVisitas.Text = "0";
            this.LblContadorVisitas.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.LblContadorVisitas.ForeColor = System.Drawing.Color.FromArgb(247, 148, 29);
            this.LblContadorVisitas.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblContadorVisitas.Height = 50;
            this.LblContadorVisitas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblContadorVisitas.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.LblContadorVisitas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblContadorVisitas.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);

            // BOTÓN INICIAR VISITA
            this.BtnIniciarVisita.Text = "▶️ INICIAR VISITA";
            this.BtnIniciarVisita.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnIniciarVisita.ForeColor = System.Drawing.Color.White;
            this.BtnIniciarVisita.BackColor = System.Drawing.Color.FromArgb(247, 148, 29);
            this.BtnIniciarVisita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnIniciarVisita.FlatAppearance.BorderSize = 0;
            this.BtnIniciarVisita.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnIniciarVisita.Height = 42;
            this.BtnIniciarVisita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnIniciarVisita.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.BtnIniciarVisita.Click += new System.EventHandler(this.BtnIniciarVisita_Click);

            // BOTÓN GUARDAR VISITA
            this.BtnGuardarVisita.Text = "💾 GUARDAR VISITA";
            this.BtnGuardarVisita.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnGuardarVisita.ForeColor = System.Drawing.Color.White;
            this.BtnGuardarVisita.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.BtnGuardarVisita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardarVisita.FlatAppearance.BorderSize = 0;
            this.BtnGuardarVisita.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnGuardarVisita.Height = 38;
            this.BtnGuardarVisita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGuardarVisita.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);

            // BOTÓN LIMPIAR
            this.BtnLimpiar.Text = "🔄 LIMPIAR CAMPOS";
            this.BtnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnLimpiar.ForeColor = System.Drawing.Color.White;
            this.BtnLimpiar.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.BtnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLimpiar.FlatAppearance.BorderSize = 0;
            this.BtnLimpiar.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnLimpiar.Height = 35;
            this.BtnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLimpiar.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnLimpiar.Click += new System.EventHandler(this.BtnCancelar_Click);

            // BOTÓN CORREGIR DATOS - ¡NUEVO!
            this.BtnCorregirDatos.Text = "✏️ CORREGIR DATOS";
            this.BtnCorregirDatos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnCorregirDatos.ForeColor = System.Drawing.Color.White;
            this.BtnCorregirDatos.BackColor = System.Drawing.Color.FromArgb(23, 162, 184); // Azul cyan
            this.BtnCorregirDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCorregirDatos.FlatAppearance.BorderSize = 0;
            this.BtnCorregirDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCorregirDatos.Height = 35;
            this.BtnCorregirDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCorregirDatos.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnCorregirDatos.Enabled = false; // Deshabilitado inicialmente
            this.BtnCorregirDatos.Click += new System.EventHandler(this.BtnCorregirDatos_Click);

            // BOTÓN CANCELAR VISITA
            this.BtnCancelarVisita.Text = "❌ CANCELAR VISITA";
            this.BtnCancelarVisita.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.BtnCancelarVisita.ForeColor = System.Drawing.Color.White;
            this.BtnCancelarVisita.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.BtnCancelarVisita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelarVisita.FlatAppearance.BorderSize = 0;
            this.BtnCancelarVisita.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCancelarVisita.Height = 35;
            this.BtnCancelarVisita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelarVisita.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnCancelarVisita.Click += new System.EventHandler(this.BtnCancelarVisita_Click);

            // AGREGAR CONTROLES AL GRUPO (orden inverso por Dock.Top)
            this.GrpAcciones.Controls.Add(this.BtnCancelarVisita);
            this.GrpAcciones.Controls.Add(this.BtnCorregirDatos);  // ← NUEVO
            this.GrpAcciones.Controls.Add(this.BtnLimpiar);
            this.GrpAcciones.Controls.Add(this.BtnGuardarVisita);
            this.GrpAcciones.Controls.Add(this.BtnIniciarVisita);
            this.GrpAcciones.Controls.Add(this.LblContadorVisitas);
            this.GrpAcciones.Controls.Add(this.LblTituloContador);

            this.PanelRegistroEntrada.Controls.Add(this.GrpAcciones);
            this.PanelRegistroEntrada.Controls.Add(this.GrpPersonaVisitada);
            this.PanelRegistroEntrada.Controls.Add(this.GrpVisitante);

            // ÁREA INFERIOR
            this.PanelSalidaActivas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSalidaActivas.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);

            this.PanelEncabezadoSalidas.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelEncabezadoSalidas.Height = 50;
            this.PanelEncabezadoSalidas.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);

            this.LblTituloSalidas.Text = "📊 Visitas del Día";
            this.LblTituloSalidas.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblTituloSalidas.ForeColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.LblTituloSalidas.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblTituloSalidas.Width = 500;
            this.LblTituloSalidas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.PanelBusqueda.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelBusqueda.Width = 600;
            this.PanelBusqueda.Padding = new System.Windows.Forms.Padding(0);

            this.LblBuscar.Text = "Buscar visitante:";
            this.LblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblBuscar.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.LblBuscar.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblBuscar.Width = 130;
            this.LblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.TxtBuscarVisitante.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtBuscarVisitante.Dock = System.Windows.Forms.DockStyle.Left;
            this.TxtBuscarVisitante.Width = 350;

            this.BtnBuscar.Text = "🔍 Buscar";
            this.BtnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnBuscar.ForeColor = System.Drawing.Color.White;
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.FlatAppearance.BorderSize = 0;
            this.BtnBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;

            this.PanelBusqueda.Controls.Add(this.BtnBuscar);
            this.PanelBusqueda.Controls.Add(this.TxtBuscarVisitante);
            this.PanelBusqueda.Controls.Add(this.LblBuscar);

            this.PanelEncabezadoSalidas.Controls.Add(this.PanelBusqueda);
            this.PanelEncabezadoSalidas.Controls.Add(this.LblTituloSalidas);

            this.DgvVisitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvVisitas.BackgroundColor = System.Drawing.Color.White;
            this.DgvVisitas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DgvVisitas.AllowUserToAddRows = false;
            this.DgvVisitas.AllowUserToDeleteRows = false;
            this.DgvVisitas.ReadOnly = true;
            this.DgvVisitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVisitas.MultiSelect = false;
            this.DgvVisitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvVisitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DgvVisitas.ColumnHeadersHeight = 45;
            this.DgvVisitas.RowTemplate.Height = 40;
            this.DgvVisitas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.DgvVisitas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.DgvVisitas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.DgvVisitas.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DgvVisitas.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvVisitas.EnableHeadersVisualStyles = false;
            this.DgvVisitas.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Seleccionar);

            this.PanelBotonesInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBotonesInferior.Height = 80;
            this.PanelBotonesInferior.BackColor = System.Drawing.Color.Transparent;
            this.PanelBotonesInferior.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);

            this.BtnTerminarVisita.Text = "✅ MARCAR SALIDA / FINALIZAR VISITA";
            this.BtnTerminarVisita.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnTerminarVisita.ForeColor = System.Drawing.Color.White;
            this.BtnTerminarVisita.BackColor = System.Drawing.Color.FromArgb(111, 18, 64);
            this.BtnTerminarVisita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnTerminarVisita.FlatAppearance.BorderSize = 0;
            this.BtnTerminarVisita.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(140, 25, 80);
            this.BtnTerminarVisita.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnTerminarVisita.Width = 400;
            this.BtnTerminarVisita.Height = 55;
            this.BtnTerminarVisita.Enabled = true;
            this.BtnTerminarVisita.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnTerminarVisita.Click += new System.EventHandler(this.BtnTerminarVisita_Click);

            this.BtnRegresar.Text = "⬅️ Regresar al Menú";
            this.BtnRegresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRegresar.ForeColor = System.Drawing.Color.White;
            this.BtnRegresar.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.BtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegresar.FlatAppearance.BorderSize = 0;
            this.BtnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.BtnRegresar.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnRegresar.Width = 200;
            this.BtnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRegresar.Click += new System.EventHandler(this.BtnRegresar_Click);

            this.PanelBotonesInferior.Controls.Add(this.BtnRegresar);
            this.PanelBotonesInferior.Controls.Add(this.BtnTerminarVisita);

            this.PanelSalidaActivas.Controls.Add(this.DgvVisitas);
            this.PanelSalidaActivas.Controls.Add(this.PanelBotonesInferior);
            this.PanelSalidaActivas.Controls.Add(this.PanelEncabezadoSalidas);

            this.PanelContenido.Controls.Add(this.PanelSalidaActivas);
            this.PanelContenido.Controls.Add(this.PanelRegistroEntrada);

            // CONFIGURACIÓN FORMULARIO
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.PanelContenido);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de Visitas - Consejo Ciudadano";
            this.Load += new System.EventHandler(this.FrmRegistrarVisita_Load);

            this.PanelSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicLogo)).EndInit();
            this.PanelContenido.ResumeLayout(false);
            this.PanelRegistroEntrada.ResumeLayout(false);
            this.PanelSalidaActivas.ResumeLayout(false);
            this.GrpVisitante.ResumeLayout(false);
            this.GrpPersonaVisitada.ResumeLayout(false);
            this.GrpAcciones.ResumeLayout(false);
            this.PanelEncabezadoSalidas.ResumeLayout(false);
            this.PanelBusqueda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVisitas)).EndInit();
            this.PanelBotonesInferior.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.Label LblTituloPrincipal;
        private System.Windows.Forms.PictureBox PicLogo;
        private System.Windows.Forms.Panel PanelContenido;
        private System.Windows.Forms.Panel PanelRegistroEntrada;
        private System.Windows.Forms.Panel PanelSalidaActivas;
        private System.Windows.Forms.GroupBox GrpVisitante;
        private System.Windows.Forms.GroupBox GrpPersonaVisitada;
        private System.Windows.Forms.GroupBox GrpAcciones;
        private System.Windows.Forms.ComboBox CbxVisitantesFrecuentes;
        private System.Windows.Forms.Label LblVisitantesFrecuentes;
        private System.Windows.Forms.TextBox TxtNombreV;
        private System.Windows.Forms.Label LblNombreV;
        private System.Windows.Forms.TextBox TxtAPV;
        private System.Windows.Forms.Label LblPrimerApellidoV;
        private System.Windows.Forms.TextBox TxtAMV;
        private System.Windows.Forms.Label LblSegundoApellidoV;
        private System.Windows.Forms.ComboBox CbxCompania;
        private System.Windows.Forms.Label LblCompania;
        private System.Windows.Forms.CheckBox CkbGuardarVisFrec;
        private System.Windows.Forms.ComboBox CbxPersonasVisitadasFrecuentes;
        private System.Windows.Forms.Label LblPersonasFrecuentesE;
        private System.Windows.Forms.TextBox TxtNombreE;
        private System.Windows.Forms.Label LblNombreE;
        private System.Windows.Forms.TextBox TxtAPE;
        private System.Windows.Forms.Label LblPrimerApellidoE;
        private System.Windows.Forms.TextBox TxtAME;
        private System.Windows.Forms.Label LblSegundoApellidoE;
        private System.Windows.Forms.ComboBox CbxDepartamento;
        private System.Windows.Forms.Label LblDepartamento;
        private System.Windows.Forms.CheckBox CkbGuardarPerVisFrec;
        private System.Windows.Forms.Label LblContadorVisitas;
        private System.Windows.Forms.Label LblTituloContador;
        private System.Windows.Forms.Button BtnIniciarVisita;
        private System.Windows.Forms.Button BtnGuardarVisita;
        private System.Windows.Forms.Button BtnLimpiar;
        private System.Windows.Forms.Button BtnCorregirDatos; // ← NUEVO
        private System.Windows.Forms.Button BtnCancelarVisita;
        private System.Windows.Forms.Panel PanelEncabezadoSalidas;
        private System.Windows.Forms.Panel PanelBusqueda;
        private System.Windows.Forms.TextBox TxtBuscarVisitante;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.Label LblBuscar;
        private System.Windows.Forms.Label LblTituloSalidas;
        private System.Windows.Forms.DataGridView DgvVisitas;
        private System.Windows.Forms.Button BtnGenerarExcel;
        private System.Windows.Forms.Panel PanelBotonesInferior;
        private System.Windows.Forms.Button BtnTerminarVisita;
        private System.Windows.Forms.Button BtnRegresar;
    }
}