using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;

#pragma warning disable CS0618

namespace Visitas
{
    public partial class FrmReportes : BaseForm
    {
        public FrmReportes()
        {
            try
            {
                InitializeComponent();

                NumAnio.Value = System.DateTime.Now.Year;
                CbxMes.SelectedIndex = System.DateTime.Now.Month - 1;

                RbDia.CheckedChanged += RbTipoReporte_CheckedChanged;
                RbSemana.CheckedChanged += RbTipoReporte_CheckedChanged;
                RbMes.CheckedChanged += RbTipoReporte_CheckedChanged;
                RbAnio.CheckedChanged += RbTipoReporte_CheckedChanged;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "ERROR DETALLADO AL INICIALIZAR FORMULARIO:\n\n" +
                    "Mensaje: " + ex.Message + "\n\n" +
                    "Tipo: " + ex.GetType().Name + "\n\n" +
                    "Pila: " + ex.StackTrace,
                    "Error Detallado",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
                throw;
            }
        }

        private void RbTipoReporte_CheckedChanged(object sender, EventArgs e)
        {
            LblFecha.Visible = false;
            DtpFecha.Visible = false;
            LblAnio.Visible = false;
            NumAnio.Visible = false;
            LblMes.Visible = false;
            CbxMes.Visible = false;

            if (RbDia.Checked)
            {
                LblFecha.Text = "Selecciona el día:";
                LblFecha.Visible = true;
                DtpFecha.Visible = true;
            }
            else if (RbSemana.Checked)
            {
                LblFecha.Text = "Selecciona una fecha (semana completa):";
                LblFecha.Visible = true;
                DtpFecha.Visible = true;
            }
            else if (RbMes.Checked)
            {
                LblAnio.Visible = true;
                NumAnio.Visible = true;
                LblMes.Visible = true;
                CbxMes.Visible = true;
            }
            else if (RbAnio.Checked)
            {
                LblAnio.Visible = true;
                NumAnio.Visible = true;
            }
        }

        private void BtnGenerarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = null;
                string nombreArchivo = "";
                string tituloReporte = "";
                string carpetaTipo = "";
                string tipoReporteAuditoria = "";
                string detallesReporte = "";

                if (RbDia.Checked)
                {
                    DateTime fecha = DtpFecha.Value.Date;

                    if (fecha > DateTime.Now.Date)
                    {
                        MessageBox.Show(
                            "⚠️ No puedes generar un reporte de un día futuro.\n\n" +
                            "Selecciona una fecha igual o anterior a hoy.",
                            "Fecha Inválida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    datos = ObtenerReportePorDia(fecha);
                    nombreArchivo = string.Format("Reporte_Dia_{0:dd-MM-yyyy}.xlsx", fecha);
                    tituloReporte = string.Format("Reporte de Visitas - {0:dd/MM/yyyy}", fecha);
                    carpetaTipo = "Día";
                    tipoReporteAuditoria = "Generación de Reporte - Día";
                    detallesReporte = $"Fecha: {fecha:yyyy-MM-dd}";
                }
                else if (RbSemana.Checked)
                {
                    DateTime fechaSeleccionada = DtpFecha.Value.Date;

                    int diasHastaLunes = ((int)fechaSeleccionada.DayOfWeek - 1 + 7) % 7;
                    DateTime lunes = fechaSeleccionada.AddDays(-diasHastaLunes);
                    DateTime domingo = lunes.AddDays(6);

                    if (lunes > DateTime.Now.Date)
                    {
                        MessageBox.Show(
                            "⚠️ No puedes generar un reporte de una semana futura.\n\n" +
                            "Selecciona una fecha de una semana que ya haya comenzado.",
                            "Fecha Inválida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (domingo > DateTime.Now.Date)
                    {
                        DialogResult resultado = MessageBox.Show(
                            string.Format("⚠️ SEMANA EN CURSO\n\n" +
                            "La semana seleccionada ({0:dd/MM/yyyy} - {1:dd/MM/yyyy}) aún no ha terminado.\n\n" +
                            "¿Deseas generar un REPORTE PARCIAL con los datos disponibles hasta hoy?", lunes, domingo),
                            "Semana Incompleta",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (resultado == DialogResult.No)
                            return;

                        domingo = DateTime.Now.Date;
                    }

                    datos = ObtenerReportePorSemana(lunes, domingo);
                    nombreArchivo = string.Format("Reporte_Semana_{0:dd-MM-yyyy}_a_{1:dd-MM-yyyy}.xlsx", lunes, domingo);
                    tituloReporte = string.Format("Reporte de Visitas - Semana del {0:dd/MM/yyyy} al {1:dd/MM/yyyy}", lunes, domingo);
                    carpetaTipo = "Semana";
                    tipoReporteAuditoria = "Generación de Reporte - Semana";
                    detallesReporte = $"Fecha Inicio: {lunes:yyyy-MM-dd}, Fecha Fin: {domingo:yyyy-MM-dd}";
                }
                else if (RbMes.Checked)
                {
                    int anio = (int)NumAnio.Value;
                    int mes = CbxMes.SelectedIndex + 1;

                    DateTime primerDiaMes = new DateTime(anio, mes, 1);
                    if (primerDiaMes > DateTime.Now.Date)
                    {
                        MessageBox.Show(
                            "⚠️ No puedes generar un reporte de un mes futuro.\n\n" +
                            "Selecciona un mes que ya haya comenzado.",
                            "Fecha Inválida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    DateTime ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);
                    if (ultimoDiaMes > DateTime.Now.Date && primerDiaMes <= DateTime.Now.Date)
                    {
                        DialogResult resultado = MessageBox.Show(
                            string.Format("⚠️ MES EN CURSO\n\n" +
                            "El mes de {0} {1} aún no ha terminado.\n\n" +
                            "¿Deseas generar un REPORTE PARCIAL con los datos disponibles hasta hoy?", CbxMes.Text, anio),
                            "Mes Incompleto",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (resultado == DialogResult.No)
                            return;
                    }

                    datos = ObtenerReportePorMes(anio, mes);
                    nombreArchivo = string.Format("Reporte_Mes_{0}_{1}.xlsx", CbxMes.Text, anio);
                    tituloReporte = string.Format("Reporte de Visitas - {0} {1}", CbxMes.Text, anio);
                    carpetaTipo = "Mes";
                    tipoReporteAuditoria = "Generación de Reporte - Mes";
                    detallesReporte = $"Mes: {mes}, Año: {anio}, Período: {CbxMes.Text} {anio}";
                }
                else if (RbAnio.Checked)
                {
                    int anio = (int)NumAnio.Value;

                    if (anio > DateTime.Now.Year)
                    {
                        MessageBox.Show(
                            "⚠️ No puedes generar un reporte de un año futuro.\n\n" +
                            "Selecciona un año que ya haya comenzado.",
                            "Fecha Inválida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (anio == DateTime.Now.Year)
                    {
                        DialogResult resultado = MessageBox.Show(
                            string.Format("⚠️ AÑO EN CURSO\n\n" +
                            "El año {0} aún no ha terminado.\n\n" +
                            "¿Deseas generar un REPORTE PARCIAL con los datos disponibles hasta hoy?", anio),
                            "Año Incompleto",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (resultado == DialogResult.No)
                            return;
                    }

                    datos = ObtenerReportePorAnio(anio);
                    nombreArchivo = string.Format("Reporte_Anio_{0}.xlsx", anio);
                    tituloReporte = string.Format("Reporte de Visitas - Año {0}", anio);
                    carpetaTipo = "Año";
                    tipoReporteAuditoria = "Generación de Reporte - Año";
                    detallesReporte = $"Año: {anio}";
                }

                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "ℹ️ No hay visitas registradas para el período seleccionado.\n\n" +
                        "No se puede generar el reporte.",
                        "Sin Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                string rutaCompleta = CrearEstructuraCarpetas(carpetaTipo, nombreArchivo);

                bool archivoExiste = File.Exists(rutaCompleta);
                if (archivoExiste)
                {
                    DialogResult confirmacion = MessageBox.Show(
                        string.Format("⚠️ REPORTE EXISTENTE\n\n" +
                        "Ya existe un reporte para este período:\n\n{0}\n\n" +
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

                GenerarArchivoExcelProtegido(datos, rutaCompleta, tituloReporte, nombreArchivo, tipoReporteAuditoria, detallesReporte);
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

        private string CrearEstructuraCarpetas(string tipoReporte, string nombreArchivo)
        {
            try
            {
                string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string carpetaPrincipal = Path.Combine(rutaDocumentos, "Reportes de Visitas");

                if (!Directory.Exists(carpetaPrincipal))
                {
                    Directory.CreateDirectory(carpetaPrincipal);
                }

                string carpetaTipo = Path.Combine(carpetaPrincipal, tipoReporte);
                if (!Directory.Exists(carpetaTipo))
                {
                    Directory.CreateDirectory(carpetaTipo);
                }

                return Path.Combine(carpetaTipo, nombreArchivo);
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

        private DataTable ObtenerReportePorDia(DateTime fecha)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Fecha", fecha)
            };
            return ConexionDB.EjecutarConsulta("sp_ReportePorDia", parametros);
        }

        private DataTable ObtenerReportePorSemana(DateTime inicio, DateTime fin)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@FechaInicio", inicio),
                new SqlParameter("@FechaFin", fin)
            };
            return ConexionDB.EjecutarConsulta("sp_ReportePorSemana", parametros);
        }

        private DataTable ObtenerReportePorMes(int anio, int mes)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Anio", anio),
                new SqlParameter("@Mes", mes)
            };
            return ConexionDB.EjecutarConsulta("sp_ReportePorMes", parametros);
        }

        private DataTable ObtenerReportePorAnio(int anio)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Anio", anio)
            };
            return ConexionDB.EjecutarConsulta("sp_ReportePorAnio", parametros);
        }

        private void GenerarArchivoExcelProtegido(DataTable datos, string rutaArchivo, string titulo,
            string nombreArchivo, string tipoReporteAuditoria, string detallesReporte)
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
                    worksheet.Cells["A1:I1"].Merge = true;
                    worksheet.Cells["A1"].Style.Font.Size = 20;
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["A1"].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(111, 18, 64));
                    worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells["A2"].Value = titulo;
                    worksheet.Cells["A2:I2"].Merge = true;
                    worksheet.Cells["A2"].Style.Font.Size = 14;
                    worksheet.Cells["A2"].Style.Font.Bold = true;
                    worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells["A3"].Value = string.Format("Generado el: {0:dd/MM/yyyy HH:mm}", DateTime.Now);
                    worksheet.Cells["A3:I3"].Merge = true;
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
                        worksheet.Cells[filaEncabezados, i + 1].Style.WrapText = false;
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

                    for (int i = 1; i <= datos.Columns.Count; i++)
                    {
                        worksheet.Column(i).AutoFit();

                        if (worksheet.Column(i).Width < 8)
                            worksheet.Column(i).Width = 8;

                        if (worksheet.Column(i).Width > 50)
                            worksheet.Column(i).Width = 50;
                    }

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
                RegistrarGeneracionReporte(titulo, datos.Rows.Count, nombreArchivo, tipoReporteAuditoria, detallesReporte, rutaArchivo);

                string carpetaContenedora = Path.GetDirectoryName(rutaArchivo);
                MessageBox.Show(
                    string.Format("✅ REPORTE GENERADO EXITOSAMENTE\n\n" +
                    "📁 Ubicación:\n{0}\n\n" +
                    "📊 Total de visitas: {1}\n\n" +
                    "🔒 Archivo protegido contra modificaciones\n" +
                    "🖨️ Configurado para impresión en 1 hoja horizontal\n" +
                    "📏 Columnas ajustadas automáticamente\n\n" +
                    "💡 Puedes encontrarlo fácilmente en:\n" +
                    "Este equipo → Documentos → Reportes de Visitas",
                    rutaArchivo, datos.Rows.Count),
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
            string tipoReporteAuditoria, string detallesReporte, string rutaCompleta)
        {
            try
            {
                string descripcion = $"Usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) generó {tipoReporteAuditoria.ToLower()}: '{tituloReporte}' con {totalRegistros} registro(s)";

                string datosNuevos = $"Archivo: {nombreArchivo}, Ruta: {rutaCompleta}, Total registros: {totalRegistros}, {detallesReporte}, Generado: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

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

        private void PanelContenido_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}












