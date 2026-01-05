using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

#pragma warning disable CS0618

namespace Visitas
{
    public partial class FrmEstadisticasVisitantes : BaseForm
    {
        private string nombreSeleccionado = "";
        private string primerApellidoSeleccionado = "";
        private string segundoApellidoSeleccionado = "";

        public FrmEstadisticasVisitantes()
        {
            InitializeComponent();
        }

        private void FrmEstadisticasVisitantes_Load(object sender, EventArgs e)
        {
            try
            {
                CargarListaVisitantes("");
                DtpFecha.Value = DateTime.Now;
                NumAnio.Value = DateTime.Now.Year;
                CbxMes.SelectedIndex = DateTime.Now.Month - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el formulario:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarListaVisitantes(string filtro)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@TextoBusqueda", string.IsNullOrWhiteSpace(filtro) ? "" : filtro.Trim())
                };

                DataTable dt = ConexionDB.EjecutarConsulta("sp_BuscarVisitantesConTotales", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DgvVisitantes.DataSource = dt;

                    DgvVisitantes.Columns["NombreVisitante"].Visible = false;
                    DgvVisitantes.Columns["PrimerApellidoVisitante"].Visible = false;
                    DgvVisitantes.Columns["SegundoApellidoVisitante"].Visible = false;

                    DgvVisitantes.Columns["NombreCompleto"].HeaderText = "Visitante";
                    DgvVisitantes.Columns["NombreCompleto"].Width = 250;

                    DgvVisitantes.Columns["TotalVisitas"].HeaderText = "Total";
                    DgvVisitantes.Columns["TotalVisitas"].Width = 70;

                    DgvVisitantes.Columns["PrimeraVisita"].HeaderText = "1ª Visita";
                    DgvVisitantes.Columns["PrimeraVisita"].Width = 100;

                    DgvVisitantes.Columns["UltimaVisita"].HeaderText = "Últ. Visita";
                    DgvVisitantes.Columns["UltimaVisita"].Width = 100;

                    DgvVisitantes.Columns["PrimeraVisita"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    DgvVisitantes.Columns["UltimaVisita"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    DgvVisitantes.Columns["TotalVisitas"].DefaultCellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                    DgvVisitantes.Columns["TotalVisitas"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    DgvVisitantes.Columns["TotalVisitas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    DgvVisitantes.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar visitantes:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarListaVisitantes(TxtBuscar.Text);
        }

        private void DgvVisitantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = DgvVisitantes.Rows[e.RowIndex];

                    nombreSeleccionado = row.Cells["NombreVisitante"].Value.ToString();
                    primerApellidoSeleccionado = row.Cells["PrimerApellidoVisitante"].Value.ToString();
                    segundoApellidoSeleccionado = row.Cells["SegundoApellidoVisitante"].Value != DBNull.Value
                        ? row.Cells["SegundoApellidoVisitante"].Value.ToString()
                        : "";

                    string nombreCompleto = row.Cells["NombreCompleto"].Value.ToString();
                    LblVisitanteSeleccionado.Text = nombreCompleto;
                    LblTotalVisitas.Text = string.Format("Total histórico: {0} visitas",
                        row.Cells["TotalVisitas"].Value.ToString());
                    LblPeriodoActual.Text = "Seleccione un período y haga clic en Consultar";

                    DgvVisitas.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar visitante:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void RbTipoFiltro_CheckedChanged(object sender, EventArgs e)
        {
            LblFecha.Visible = false;
            DtpFecha.Visible = false;
            LblAnio.Visible = false;
            NumAnio.Visible = false;
            LblMes.Visible = false;
            CbxMes.Visible = false;

            if (RbPorDia.Checked || RbPorSemana.Checked)
            {
                LblFecha.Visible = true;
                DtpFecha.Visible = true;
            }
            else if (RbPorMes.Checked)
            {
                LblAnio.Visible = true;
                NumAnio.Visible = true;
                LblMes.Visible = true;
                CbxMes.Visible = true;
            }
            else if (RbPorAnio.Checked)
            {
                LblAnio.Visible = true;
                NumAnio.Visible = true;
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreSeleccionado))
                {
                    MessageBox.Show(
                        "⚠️ Por favor, selecciona primero un visitante de la lista izquierda.",
                        "Visitante No Seleccionado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DataTable dt = null;
                string periodo = "";

                if (RbHistorico.Checked)
                {
                    dt = ConsultarHistorico();
                    periodo = "Período: Histórico completo (todas las visitas)";
                }
                else if (RbPorDia.Checked)
                {
                    dt = ConsultarPorDia();
                    periodo = string.Format("Período: Día {0:dd/MM/yyyy}", DtpFecha.Value);
                }
                else if (RbPorSemana.Checked)
                {
                    dt = ConsultarPorSemana();
                    DateTime inicioSemana = ObtenerInicioSemana(DtpFecha.Value);
                    DateTime finSemana = inicioSemana.AddDays(6);
                    periodo = string.Format("Período: Semana del {0:dd/MM/yyyy} al {1:dd/MM/yyyy}",
                        inicioSemana, finSemana);
                }
                else if (RbPorMes.Checked)
                {
                    dt = ConsultarPorMes();
                    periodo = string.Format("Período: {0} de {1}",
                        CbxMes.SelectedItem.ToString(), NumAnio.Value);
                }
                else if (RbPorAnio.Checked)
                {
                    dt = ConsultarPorAnio();
                    periodo = string.Format("Período: Año completo {0}", NumAnio.Value);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    DgvVisitas.DataSource = dt;
                    LblPeriodoActual.Text = periodo;
                    LblTotalVisitas.Text = string.Format("Se encontraron {0} visitas en este período", dt.Rows.Count);

                    ConfigurarColumnasResultados();
                }
                else
                {
                    DgvVisitas.DataSource = null;
                    LblPeriodoActual.Text = periodo;
                    LblTotalVisitas.Text = "0 visitas encontradas";

                    MessageBox.Show(
                        "ℹ️ No se encontraron registros de visitas para los criterios seleccionados.",
                        "Sin Resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar la base de datos:\n\n" + ex.Message,
                    "Error Crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ConfigurarColumnasResultados()
        {
            if (DgvVisitas.Columns.Count > 0)
            {
                DgvVisitas.Columns["ID"].Width = 60;

                DgvVisitas.Columns["Hora Entrada"].Width = 150;
                DgvVisitas.Columns["Hora Entrada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DgvVisitas.Columns["Hora Salida"].Width = 150;
                DgvVisitas.Columns["Hora Salida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DgvVisitas.Columns["Compañía"].Width = 250;
                DgvVisitas.Columns["Persona Visitada"].Width = 300;
                DgvVisitas.Columns["Departamento"].Width = 200;

                DgvVisitas.Columns["Estado"].Width = 150;
                DgvVisitas.Columns["Estado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                foreach (DataGridViewRow row in DgvVisitas.Rows)
                {
                    string estado = row.Cells["Estado"].Value?.ToString();

                    if (estado == "En curso")
                    {
                        row.Cells["Estado"].Style.ForeColor = Color.FromArgb(247, 148, 29);
                        row.Cells["Estado"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                        row.Cells["Hora Salida"].Style.ForeColor = Color.FromArgb(247, 148, 29);
                        row.Cells["Hora Salida"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    }
                    else if (estado == "Finalizada")
                    {
                        row.Cells["Estado"].Style.ForeColor = Color.FromArgb(40, 167, 69);
                        row.Cells["Estado"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    }
                }
            }
        }

        private DataTable ConsultarHistorico()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", nombreSeleccionado),
                new SqlParameter("@PrimerApellido", primerApellidoSeleccionado),
                new SqlParameter("@SegundoApellido",
                    string.IsNullOrWhiteSpace(segundoApellidoSeleccionado) ? (object)DBNull.Value : segundoApellidoSeleccionado)
            };
            return ConexionDB.EjecutarConsulta("sp_ObtenerVisitasHistoricas_Visitante", parametros);
        }

        private DataTable ConsultarPorDia()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", nombreSeleccionado),
                new SqlParameter("@PrimerApellido", primerApellidoSeleccionado),
                new SqlParameter("@SegundoApellido",
                    string.IsNullOrWhiteSpace(segundoApellidoSeleccionado) ? (object)DBNull.Value : segundoApellidoSeleccionado),
                new SqlParameter("@Fecha", DtpFecha.Value.Date)
            };
            return ConexionDB.EjecutarConsulta("sp_ObtenerVisitasPorDia_Visitante", parametros);
        }

        private DataTable ConsultarPorSemana()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", nombreSeleccionado),
                new SqlParameter("@PrimerApellido", primerApellidoSeleccionado),
                new SqlParameter("@SegundoApellido",
                    string.IsNullOrWhiteSpace(segundoApellidoSeleccionado) ? (object)DBNull.Value : segundoApellidoSeleccionado),
                new SqlParameter("@FechaReferencia", DtpFecha.Value.Date)
            };
            return ConexionDB.EjecutarConsulta("sp_ObtenerVisitasPorSemana_Visitante", parametros);
        }

        private DataTable ConsultarPorMes()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", nombreSeleccionado),
                new SqlParameter("@PrimerApellido", primerApellidoSeleccionado),
                new SqlParameter("@SegundoApellido",
                    string.IsNullOrWhiteSpace(segundoApellidoSeleccionado) ? (object)DBNull.Value : segundoApellidoSeleccionado),
                new SqlParameter("@Anio", (int)NumAnio.Value),
                new SqlParameter("@Mes", CbxMes.SelectedIndex + 1)
            };
            return ConexionDB.EjecutarConsulta("sp_ObtenerVisitasPorMes_Visitante", parametros);
        }

        private DataTable ConsultarPorAnio()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Nombre", nombreSeleccionado),
                new SqlParameter("@PrimerApellido", primerApellidoSeleccionado),
                new SqlParameter("@SegundoApellido",
                    string.IsNullOrWhiteSpace(segundoApellidoSeleccionado) ? (object)DBNull.Value : segundoApellidoSeleccionado),
                new SqlParameter("@Anio", (int)NumAnio.Value)
            };
            return ConexionDB.EjecutarConsulta("sp_ObtenerVisitasPorAnio_Visitante", parametros);
        }

        private DateTime ObtenerInicioSemana(DateTime fecha)
        {
            int diff = (7 + (fecha.DayOfWeek - DayOfWeek.Monday)) % 7;
            return fecha.AddDays(-1 * diff).Date;
        }

        private void BtnGenerarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreSeleccionado))
                {
                    MessageBox.Show(
                        "⚠️ Por favor, selecciona primero un visitante de la lista izquierda.",
                        "Visitante No Seleccionado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (DgvVisitas.DataSource == null || DgvVisitas.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "⚠️ No hay datos para exportar.\n\nPor favor, realiza una consulta primero haciendo clic en 'CONSULTAR VISITAS'.",
                        "Sin Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DataTable datos = (DataTable)DgvVisitas.DataSource;
                string nombreArchivo = "";
                string tituloReporte = "";
                string carpetaTipo = "";
                string nombreVisitante = LblVisitanteSeleccionado.Text;
                string tipoReporteAuditoria = "";
                string detallesReporte = "";

                if (RbHistorico.Checked)
                {
                    carpetaTipo = "Historico";
                    nombreArchivo = string.Format("Reporte_Historico_{0}.xlsx", nombreVisitante.Replace(" ", "_"));
                    tituloReporte = string.Format("Reporte Histórico de Visitas - {0}", nombreVisitante);
                    tipoReporteAuditoria = "Generación de Reporte Visitante - Histórico";
                    detallesReporte = "Período: Histórico completo";
                }
                else if (RbPorDia.Checked)
                {
                    DateTime fecha = DtpFecha.Value.Date;
                    carpetaTipo = "Dia";
                    nombreArchivo = string.Format("Reporte_Dia_{0:dd-MM-yyyy}_{1}.xlsx", fecha, nombreVisitante.Replace(" ", "_"));
                    tituloReporte = string.Format("Reporte de Visitas - {0:dd/MM/yyyy} - {1}", fecha, nombreVisitante);
                    tipoReporteAuditoria = "Generación de Reporte Visitante - Día";
                    detallesReporte = $"Fecha: {fecha:yyyy-MM-dd}";
                }
                else if (RbPorSemana.Checked)
                {
                    DateTime fechaSeleccionada = DtpFecha.Value.Date;
                    int diasHastaLunes = ((int)fechaSeleccionada.DayOfWeek - 1 + 7) % 7;
                    DateTime lunes = fechaSeleccionada.AddDays(-diasHastaLunes);
                    DateTime domingo = lunes.AddDays(6);

                    carpetaTipo = "Semana";
                    nombreArchivo = string.Format("Reporte_Semana_{0:dd-MM-yyyy}_a_{1:dd-MM-yyyy}_{2}.xlsx",
                        lunes, domingo, nombreVisitante.Replace(" ", "_"));
                    tituloReporte = string.Format("Reporte de Visitas - Semana del {0:dd/MM/yyyy} al {1:dd/MM/yyyy} - {2}",
                        lunes, domingo, nombreVisitante);
                    tipoReporteAuditoria = "Generación de Reporte Visitante - Semana";
                    detallesReporte = $"Fecha Inicio: {lunes:yyyy-MM-dd}, Fecha Fin: {domingo:yyyy-MM-dd}";
                }
                else if (RbPorMes.Checked)
                {
                    int anio = (int)NumAnio.Value;
                    int mes = CbxMes.SelectedIndex + 1;
                    carpetaTipo = "Mes";
                    nombreArchivo = string.Format("Reporte_Mes_{0}_{1}_{2}.xlsx",
                        CbxMes.Text, anio, nombreVisitante.Replace(" ", "_"));
                    tituloReporte = string.Format("Reporte de Visitas - {0} {1} - {2}",
                        CbxMes.Text, anio, nombreVisitante);
                    tipoReporteAuditoria = "Generación de Reporte Visitante - Mes";
                    detallesReporte = $"Mes: {mes}, Año: {anio}, Período: {CbxMes.Text} {anio}";
                }
                else if (RbPorAnio.Checked)
                {
                    int anio = (int)NumAnio.Value;
                    carpetaTipo = "Año";
                    nombreArchivo = string.Format("Reporte_Anio_{0}_{1}.xlsx", anio, nombreVisitante.Replace(" ", "_"));
                    tituloReporte = string.Format("Reporte de Visitas - Año {0} - {1}", anio, nombreVisitante);
                    tipoReporteAuditoria = "Generación de Reporte Visitante - Año";
                    detallesReporte = $"Año: {anio}";
                }

                string rutaCompleta = CrearEstructuraCarpetas(carpetaTipo, nombreVisitante, nombreArchivo);

                bool archivoExiste = File.Exists(rutaCompleta);
                if (archivoExiste)
                {
                    DialogResult confirmacion = MessageBox.Show(
                        string.Format("⚠️ REPORTE EXISTENTE\n\n" +
                        "Ya existe un reporte para este visitante y período:\n\n{0}\n\n" +
                        "¿Deseas REEMPLAZARLO con los datos actualizados?\n\n" +
                        "• SÍ: El reporte anterior será reemplazado\n" +
                        "• NO: Se abrirá un diálogo para guardar con otro nombre",
                        nombreArchivo),
                        "Reporte Existente",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmacion == DialogResult.No)
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                        saveDialog.FileName = nombreArchivo;
                        saveDialog.InitialDirectory = Path.GetDirectoryName(rutaCompleta);
                        saveDialog.Title = "Guardar Reporte con Otro Nombre";

                        if (saveDialog.ShowDialog() != DialogResult.OK)
                            return;

                        rutaCompleta = saveDialog.FileName;
                    }
                    else
                    {
                        try
                        {
                            File.Delete(rutaCompleta);
                        }
                        catch (Exception exDelete)
                        {
                            MessageBox.Show(
                                "⚠️ No se pudo eliminar el archivo anterior.\n\n" +
                                "Asegúrate de que no esté abierto en Excel.\n\n" +
                                "Error: " + exDelete.Message,
                                "Error al Reemplazar",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }
                }

                GenerarArchivoExcelProtegido(datos, rutaCompleta, tituloReporte, nombreArchivo, nombreVisitante,
                    tipoReporteAuditoria, detallesReporte);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al generar reporte:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string CrearEstructuraCarpetas(string tipoReporte, string nombreVisitante, string nombreArchivo)
        {
            try
            {
                string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string carpetaPrincipal = Path.Combine(rutaDocumentos, "Reportes Estadisticas Visitantes");

                if (!Directory.Exists(carpetaPrincipal))
                {
                    Directory.CreateDirectory(carpetaPrincipal);
                }

                string carpetaTipo = Path.Combine(carpetaPrincipal, tipoReporte);
                if (!Directory.Exists(carpetaTipo))
                {
                    Directory.CreateDirectory(carpetaTipo);
                }

                string carpetaVisitante = Path.Combine(carpetaTipo, nombreVisitante);
                if (!Directory.Exists(carpetaVisitante))
                {
                    Directory.CreateDirectory(carpetaVisitante);
                }

                return Path.Combine(carpetaVisitante, nombreArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al crear estructura de carpetas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                return Path.Combine(escritorio, nombreArchivo);
            }
        }

        private void GenerarArchivoExcelProtegido(DataTable datos, string rutaArchivo, string titulo,
            string nombreArchivo, string nombreVisitante, string tipoReporteAuditoria, string detallesReporte)
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte de Visitas");

                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(3).Height = 40;

                    try
                    {
                        System.Drawing.Image logo = Properties.Resources.LogoConsejo;

                        if (logo != null)
                        {
                            var picture = worksheet.Drawings.AddPicture("Logo", logo);
                            picture.SetPosition(0, 10, 0, 10);
                            picture.SetSize(100, 100);
                        }
                    }
                    catch { }

                    worksheet.Cells["A1"].Value = "CONSEJO CIUDADANO";
                    worksheet.Cells["A1:G1"].Merge = true;
                    worksheet.Cells["A1"].Style.Font.Size = 20;
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["A1"].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(111, 18, 64));
                    worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells["A2"].Value = titulo;
                    worksheet.Cells["A2:G2"].Merge = true;
                    worksheet.Cells["A2"].Style.Font.Size = 14;
                    worksheet.Cells["A2"].Style.Font.Bold = true;
                    worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells["A3"].Value = string.Format("Generado el: {0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    worksheet.Cells["A3:G3"].Merge = true;
                    worksheet.Cells["A3"].Style.Font.Size = 10;
                    worksheet.Cells["A3"].Style.Font.Italic = true;
                    worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells["A3"].Style.Font.Color.SetColor(System.Drawing.Color.Gray);

                    int filaEncabezados = 4;
                    for (int i = 0; i < datos.Columns.Count; i++)
                    {
                        worksheet.Cells[filaEncabezados, i + 1].Value = datos.Columns[i].ColumnName;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        worksheet.Cells[filaEncabezados, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(111, 18, 64));
                        worksheet.Cells[filaEncabezados, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[filaEncabezados, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                    worksheet.Row(filaEncabezados).Height = 25;

                    int filaActual = filaEncabezados + 1;
                    foreach (DataRow row in datos.Rows)
                    {
                        for (int i = 0; i < datos.Columns.Count; i++)
                        {
                            worksheet.Cells[filaActual, i + 1].Value = row[i].ToString();
                            worksheet.Cells[filaActual, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            worksheet.Cells[filaActual, i + 1].Style.WrapText = false;

                            if (filaActual % 2 == 0)
                            {
                                worksheet.Cells[filaActual, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[filaActual, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(253, 246, 249));
                            }

                            if (datos.Columns[i].ColumnName == "Estado")
                            {
                                if (row[i].ToString() == "En curso")
                                {
                                    worksheet.Cells[filaActual, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(247, 148, 29));
                                    worksheet.Cells[filaActual, i + 1].Style.Font.Bold = true;
                                }
                                else if (row[i].ToString() == "Finalizada")
                                {
                                    worksheet.Cells[filaActual, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(40, 167, 69));
                                    worksheet.Cells[filaActual, i + 1].Style.Font.Bold = true;
                                }
                            }
                        }
                        filaActual++;
                    }

                    filaActual++;

                    worksheet.Cells[filaActual + 1, 1].Value = "TOTAL DE VISITAS:";
                    worksheet.Cells[filaActual + 1, 1].Style.Font.Bold = true;
                    worksheet.Cells[filaActual + 1, 1].Style.Font.Size = 12;
                    worksheet.Cells[filaActual + 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    worksheet.Cells[filaActual + 1, 2].Value = datos.Rows.Count;
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Bold = true;
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Size = 12;
                    worksheet.Cells[filaActual + 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[filaActual + 1, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(247, 148, 29));
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells[filaActual + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Row(filaActual + 1).Height = 25;

                    worksheet.Column(1).Width = 8;
                    worksheet.Column(2).Width = 18;
                    worksheet.Column(3).Width = 18;
                    worksheet.Column(4).Width = 25;
                    worksheet.Column(5).Width = 30;
                    worksheet.Column(6).Width = 35;
                    worksheet.Column(7).Width = 14;

                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                    worksheet.PrinterSettings.FitToPage = true;
                    worksheet.PrinterSettings.FitToWidth = 1;
                    worksheet.PrinterSettings.FitToHeight = 0;

                    worksheet.PrinterSettings.LeftMargin = 0.25M;
                    worksheet.PrinterSettings.RightMargin = 0.25M;
                    worksheet.PrinterSettings.TopMargin = 0.5M;
                    worksheet.PrinterSettings.BottomMargin = 0.5M;
                    worksheet.PrinterSettings.HeaderMargin = 0.3M;
                    worksheet.PrinterSettings.FooterMargin = 0.3M;

                    worksheet.PrinterSettings.HorizontalCentered = true;
                    worksheet.PrinterSettings.VerticalCentered = false;

                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[worksheet.Dimension.Address];
                    worksheet.PrinterSettings.RepeatRows = worksheet.Cells["$4:$4"];

                    worksheet.PrinterSettings.BlackAndWhite = false;
                    worksheet.PrinterSettings.Draft = false;

                    worksheet.Protection.IsProtected = true;
                    worksheet.Protection.AllowSelectLockedCells = true;
                    worksheet.Protection.AllowSelectUnlockedCells = true;

                    string rangoCompleto = worksheet.Dimension.Address;
                    worksheet.Cells[rangoCompleto].Style.Locked = true;

                    System.IO.FileInfo file = new System.IO.FileInfo(rutaArchivo);
                    package.SaveAs(file);
                }

                // ✅ REGISTRAR GENERACIÓN DE REPORTE CON DETALLES MEJORADOS
                RegistrarGeneracionReporte(titulo, datos.Rows.Count, nombreArchivo, nombreVisitante,
                    tipoReporteAuditoria, detallesReporte, rutaArchivo);

                string carpetaContenedora = Path.GetDirectoryName(rutaArchivo);
                MessageBox.Show(
                    string.Format("✅ REPORTE GENERADO EXITOSAMENTE\n\n" +
                    "📁 Ubicación:\n{0}\n\n" +
                    "📊 Total de visitas: {1}\n\n" +
                    "🔒 Archivo protegido contra modificaciones\n" +
                    "🖨️ Configurado para impresión en 1 hoja horizontal\n\n" +
                    "💡 Puedes encontrarlo fácilmente en:\n" +
                    "Este equipo → Documentos → Reportes Estadisticas Visitantes → {2} → {3}",
                    rutaArchivo, datos.Rows.Count, Path.GetFileName(Path.GetDirectoryName(rutaArchivo)), nombreVisitante),
                    "Reporte Generado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                DialogResult abrir = MessageBox.Show(
                    "¿Deseas abrir el archivo ahora?",
                    "Abrir Archivo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (abrir == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(rutaArchivo);
                }

                DialogResult abrirCarpeta = MessageBox.Show(
                    "¿Deseas abrir la carpeta donde se guardó el reporte?",
                    "Abrir Carpeta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (abrirCarpeta == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(carpetaContenedora);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al crear archivo Excel:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODO MEJORADO: REGISTRAR GENERACIÓN DE REPORTE
        // ═════════════════════════════════════════════════════════
        private void RegistrarGeneracionReporte(string tituloReporte, int totalRegistros, string nombreArchivo,
            string nombreVisitante, string tipoReporteAuditoria, string detallesReporte, string rutaCompleta)
        {
            try
            {
                string descripcion = $"Usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) generó {tipoReporteAuditoria.ToLower()} para el visitante {nombreVisitante} con {totalRegistros} registro(s)";

                string datosNuevos = $"Visitante: {nombreVisitante}, Archivo: {nombreArchivo}, Ruta: {rutaCompleta}, Total registros: {totalRegistros}, {detallesReporte}, Generado: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdUsuario", SesionActual.IdUsuario),
                    new SqlParameter("@NombreUsuario", SesionActual.NombreUsuario),
                    new SqlParameter("@TipoAccion", tipoReporteAuditoria),
                    new SqlParameter("@TablaAfectada", "Visitas"),
                    new SqlParameter("@IdRegistroAfectado", DBNull.Value),
                    new SqlParameter("@DatosAnteriores", DBNull.Value),
                    new SqlParameter("@DatosNuevos", datosNuevos),
                    new SqlParameter("@Descripcion", descripcion),
                    new SqlParameter("@DireccionIP", SesionActual.DireccionIP),
                    new SqlParameter("@NombreMaquina", SesionActual.NombreMaquina)
                };

                ConexionDB.EjecutarComando("sp_RegistrarHistorialCambio", parametros);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar auditoría de reporte: {ex.Message}");
            }
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}