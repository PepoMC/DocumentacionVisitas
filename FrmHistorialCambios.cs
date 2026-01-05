using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Visitas
{
    public partial class FrmHistorialCambios : BaseForm
    {
        public FrmHistorialCambios()
        {
            InitializeComponent();
        }

        // =============================================
        // EVENTO LOAD DEL FORMULARIO
        // =============================================
        private void FrmHistorialCambios_Load(object sender, EventArgs e)
        {
            // Verificar que sea administrador
            if (!SesionActual.EsAdministrador())
            {
                MessageBox.Show(
                    "⛔ ACCESO DENEGADO\n\n" +
                    "Solo los administradores pueden ver el historial de cambios.",
                    "Permisos insuficientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // ✅ REGISTRAR ACCESO AL VISOR DE HISTORIAL
            RegistrarAccesoHistorial();

            // Configurar filtros
            ConfigurarFiltros();

            // Cargar historial inicial (últimos 7 días)
            DtpFechaInicio.Value = DateTime.Now.AddDays(-7);
            DtpFechaFin.Value = DateTime.Now;

            CargarHistorial();
        }

        // =============================================
        // CONFIGURAR FILTROS - CARGA DINÁMICA DESDE BD
        // =============================================
        private void ConfigurarFiltros()
        {
            SqlConnection conexion = null;
            try
            {
                // Cargar tipos de acción DIRECTAMENTE desde la base de datos
                conexion = ConexionDB.ObtenerConexion();
                if (conexion != null)
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT DISTINCT TipoAccion FROM HistorialCambios ORDER BY TipoAccion",
                        conexion);

                    CmbTipoAccion.Items.Clear();
                    CmbTipoAccion.Items.Add("(Todas las acciones)");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string tipoAccion = reader.GetString(0);

                            // ✅ FILTRAR tipos de acción antiguos/incorrectos
                            // NO mostrar tipos en mayúsculas con guiones bajos
                            if (!string.IsNullOrWhiteSpace(tipoAccion) &&
                                !tipoAccion.Contains("_") &&
                                !tipoAccion.Equals(tipoAccion.ToUpper()))
                            {
                                CmbTipoAccion.Items.Add(tipoAccion);
                            }
                        }
                    }
                    reader.Close();
                }

                CmbTipoAccion.SelectedIndex = 0;

                // Cargar lista de usuarios para el filtro
                CargarUsuariosFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtros: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // En caso de error, usar valores por defecto
                CmbTipoAccion.Items.Clear();
                CmbTipoAccion.Items.Add("(Todas las acciones)");
                CmbTipoAccion.SelectedIndex = 0;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // CARGAR USUARIOS PARA FILTRO
        // =============================================
        private void CargarUsuariosFiltro()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand(
                    "SELECT DISTINCT NombreUsuario FROM HistorialCambios " +
                    "WHERE NombreUsuario IS NOT NULL AND NombreUsuario <> '' " +
                    "ORDER BY NombreUsuario", conexion);

                CmbUsuario.Items.Clear();
                CmbUsuario.Items.Add("(Todos los usuarios)");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        CmbUsuario.Items.Add(reader.GetString(0));
                }
                reader.Close();

                CmbUsuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // CARGAR HISTORIAL
        // =============================================
        private void CargarHistorial()
        {
            SqlConnection conexion = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                string query = @"
                    SELECT 
                        IdCambio, 
                        FechaHora,
                        NombreUsuario,
                        TipoAccion,
                        TablaAfectada,
                        IdRegistroAfectado,
                        Descripcion,
                        DireccionIP,
                        NombreMaquina
                    FROM HistorialCambios
                    WHERE FechaHora BETWEEN @FechaInicio AND @FechaFin";

                // Filtro por usuario
                if (CmbUsuario.SelectedIndex > 0)
                {
                    query += " AND NombreUsuario = @Usuario";
                }

                // Filtro por tipo de acción
                if (CmbTipoAccion.SelectedIndex > 0)
                {
                    query += " AND TipoAccion = @TipoAccion";
                }

                // Filtro por búsqueda de texto
                if (!string.IsNullOrWhiteSpace(TxtBuscar.Text))
                {
                    query += " AND (Descripcion LIKE @Buscar OR TablaAfectada LIKE @Buscar OR NombreUsuario LIKE @Buscar)";
                }

                query += " ORDER BY FechaHora DESC";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@FechaInicio", DtpFechaInicio.Value.Date);
                cmd.Parameters.AddWithValue("@FechaFin", DtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1));

                if (CmbUsuario.SelectedIndex > 0)
                    cmd.Parameters.AddWithValue("@Usuario", CmbUsuario.SelectedItem.ToString());

                if (CmbTipoAccion.SelectedIndex > 0)
                    cmd.Parameters.AddWithValue("@TipoAccion", CmbTipoAccion.SelectedItem.ToString());

                if (!string.IsNullOrWhiteSpace(TxtBuscar.Text))
                    cmd.Parameters.AddWithValue("@Buscar", "%" + TxtBuscar.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DgvHistorial.DataSource = dt;
                ConfigurarColumnasHistorial();

                LblTotalRegistros.Text = $"Total de registros: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // CONFIGURAR COLUMNAS DEL DATAGRIDVIEW
        // =============================================
        private void ConfigurarColumnasHistorial()
        {
            if (DgvHistorial.Columns.Count == 0) return;

            if (DgvHistorial.Columns.Contains("IdCambio"))
            {
                DgvHistorial.Columns["IdCambio"].HeaderText = "ID";
                DgvHistorial.Columns["IdCambio"].Width = 60;
            }

            DgvHistorial.Columns["FechaHora"].HeaderText = "Fecha y Hora";
            DgvHistorial.Columns["FechaHora"].Width = 140;
            DgvHistorial.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            DgvHistorial.Columns["NombreUsuario"].HeaderText = "Usuario";
            DgvHistorial.Columns["NombreUsuario"].Width = 120;

            DgvHistorial.Columns["TipoAccion"].HeaderText = "Acción";
            DgvHistorial.Columns["TipoAccion"].Width = 180;

            DgvHistorial.Columns["TablaAfectada"].HeaderText = "Tabla";
            DgvHistorial.Columns["TablaAfectada"].Width = 120;

            DgvHistorial.Columns["IdRegistroAfectado"].HeaderText = "ID Registro";
            DgvHistorial.Columns["IdRegistroAfectado"].Width = 90;

            DgvHistorial.Columns["Descripcion"].HeaderText = "Descripción";
            DgvHistorial.Columns["Descripcion"].Width = 300;
            DgvHistorial.Columns["Descripcion"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            DgvHistorial.Columns["DireccionIP"].HeaderText = "IP";
            DgvHistorial.Columns["DireccionIP"].Width = 120;

            DgvHistorial.Columns["NombreMaquina"].HeaderText = "Máquina";
            DgvHistorial.Columns["NombreMaquina"].Width = 120;

            // Ajustar altura de filas para texto largo
            DgvHistorial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        // =============================================
        // BOTÓN: BUSCAR / APLICAR FILTROS
        // =============================================
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            // Validar rango de fechas
            if (DtpFechaInicio.Value > DtpFechaFin.Value)
            {
                MessageBox.Show(
                    "⚠️ La fecha de inicio no puede ser mayor que la fecha fin",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            CargarHistorial();

            // ✅ REGISTRAR CONSULTA DE HISTORIAL
            RegistrarConsultaHistorial();
        }

        // =============================================
        // BOTÓN: LIMPIAR FILTROS
        // =============================================
        private void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            DtpFechaInicio.Value = DateTime.Now.AddDays(-7);
            DtpFechaFin.Value = DateTime.Now;
            CmbUsuario.SelectedIndex = 0;
            CmbTipoAccion.SelectedIndex = 0;
            TxtBuscar.Clear();

            CargarHistorial();
        }

        // ═════════════════════════════════════════════════════════
        // BOTÓN: EXPORTAR A EXCEL (PROFESIONAL)
        // ═════════════════════════════════════════════════════════
        private void BtnExportar_Click(object sender, EventArgs e)
        {
            if (DgvHistorial.Rows.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ No hay datos para exportar.\n\nPrimero realiza una consulta con filtros.",
                    "Sin Datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener datos del DataGridView
                DataTable datos = (DataTable)DgvHistorial.DataSource;

                // Construir información del reporte
                string filtrosAplicados = ConstruirDescripcionFiltros();
                string nombreArchivo = GenerarNombreArchivo();
                string tituloReporte = GenerarTituloReporte();

                // Crear estructura de carpetas
                string rutaCompleta = CrearEstructuraCarpetas(nombreArchivo);

                // Verificar si el archivo ya existe
                if (File.Exists(rutaCompleta))
                {
                    DialogResult confirmacion = MessageBox.Show(
                        $"⚠️ ARCHIVO EXISTENTE\n\n" +
                        $"Ya existe un archivo con este nombre:\n\n{nombreArchivo}\n\n" +
                        $"¿Deseas REEMPLAZARLO?\n\n" +
                        $"• SÍ: El archivo anterior será reemplazado\n" +
                        $"• NO: Se abrirá un diálogo para guardar con otro nombre",
                        "Archivo Existente",
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
                                $"⚠️ No se pudo eliminar el archivo anterior.\n\n" +
                                $"Asegúrate de que no esté abierto en Excel.\n\n" +
                                $"Error: {exDelete.Message}",
                                "Error al Reemplazar",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }
                }

                // Generar el archivo Excel
                GenerarArchivoExcelProfesional(datos, rutaCompleta, tituloReporte, nombreArchivo, filtrosAplicados);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error al exportar historial:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════
        // CREAR ESTRUCTURA DE CARPETAS ORGANIZADA
        // ═════════════════════════════════════════════════════════
        private string CrearEstructuraCarpetas(string nombreArchivo)
        {
            try
            {
                string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string carpetaPrincipal = Path.Combine(rutaDocumentos, "Reporte Historial Cambios");

                // Crear carpeta principal si no existe
                if (!Directory.Exists(carpetaPrincipal))
                {
                    Directory.CreateDirectory(carpetaPrincipal);
                }

                // Determinar subcarpeta según filtros aplicados
                string subcarpeta = DeterminarSubcarpeta();
                string rutaFinal = Path.Combine(carpetaPrincipal, subcarpeta);

                // Crear subcarpeta si no existe
                if (!Directory.Exists(rutaFinal))
                {
                    Directory.CreateDirectory(rutaFinal);
                }

                return Path.Combine(rutaFinal, nombreArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error al crear estructura de carpetas:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // En caso de error, usar escritorio
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                return Path.Combine(escritorio, nombreArchivo);
            }
        }

        // ═════════════════════════════════════════════════════════
        // DETERMINAR SUBCARPETA SEGÚN FILTROS
        // ═════════════════════════════════════════════════════════
        private string DeterminarSubcarpeta()
        {
            bool tieneUsuario = CmbUsuario.SelectedIndex > 0;
            bool tieneTipo = CmbTipoAccion.SelectedIndex > 0;

            if (tieneUsuario && tieneTipo)
            {
                // Filtrado por USUARIO + TIPO
                string usuario = CmbUsuario.SelectedItem.ToString();
                string tipo = CmbTipoAccion.SelectedItem.ToString();
                string tipoLimpio = LimpiarNombreCarpeta(tipo);
                return Path.Combine("Combinado", $"{usuario}_{tipoLimpio}");
            }
            else if (tieneUsuario)
            {
                // Filtrado solo por USUARIO
                string usuario = CmbUsuario.SelectedItem.ToString();
                return Path.Combine("Por Usuario", usuario);
            }
            else if (tieneTipo)
            {
                // Filtrado solo por TIPO
                string tipo = CmbTipoAccion.SelectedItem.ToString();
                string tipoLimpio = LimpiarNombreCarpeta(tipo);
                return Path.Combine("Por Tipo Accion", tipoLimpio);
            }
            else
            {
                // Sin filtros específicos
                return "General";
            }
        }

        // ═════════════════════════════════════════════════════════
        // LIMPIAR NOMBRE DE CARPETA (remover caracteres inválidos)
        // ═════════════════════════════════════════════════════════
        private string LimpiarNombreCarpeta(string nombre)
        {
            char[] caracteresInvalidos = Path.GetInvalidFileNameChars();
            foreach (char c in caracteresInvalidos)
            {
                nombre = nombre.Replace(c, '_');
            }
            return nombre;
        }

        // ═════════════════════════════════════════════════════════
        // GENERAR NOMBRE DE ARCHIVO
        // ═════════════════════════════════════════════════════════
        private string GenerarNombreArchivo()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fechas = $"{DtpFechaInicio.Value:ddMMyyyy}_a_{DtpFechaFin.Value:ddMMyyyy}";

            bool tieneUsuario = CmbUsuario.SelectedIndex > 0;
            bool tieneTipo = CmbTipoAccion.SelectedIndex > 0;

            if (tieneUsuario && tieneTipo)
            {
                string usuario = CmbUsuario.SelectedItem.ToString();
                string tipo = LimpiarNombreCarpeta(CmbTipoAccion.SelectedItem.ToString());
                return $"Historial_{usuario}_{tipo}_{fechas}_{timestamp}.xlsx";
            }
            else if (tieneUsuario)
            {
                string usuario = CmbUsuario.SelectedItem.ToString();
                return $"Historial_{usuario}_{fechas}_{timestamp}.xlsx";
            }
            else if (tieneTipo)
            {
                string tipo = LimpiarNombreCarpeta(CmbTipoAccion.SelectedItem.ToString());
                return $"Historial_{tipo}_{fechas}_{timestamp}.xlsx";
            }
            else
            {
                return $"Historial_General_{fechas}_{timestamp}.xlsx";
            }
        }

        // ═════════════════════════════════════════════════════════
        // GENERAR TÍTULO DEL REPORTE
        // ═════════════════════════════════════════════════════════
        private string GenerarTituloReporte()
        {
            string titulo = "Historial de Cambios del Sistema";

            bool tieneUsuario = CmbUsuario.SelectedIndex > 0;
            bool tieneTipo = CmbTipoAccion.SelectedIndex > 0;

            if (tieneUsuario)
            {
                titulo += $" - Usuario: {CmbUsuario.SelectedItem}";
            }

            if (tieneTipo)
            {
                titulo += $" - Tipo: {CmbTipoAccion.SelectedItem}";
            }

            return titulo;
        }

        // ═════════════════════════════════════════════════════════
        // CONSTRUIR DESCRIPCIÓN DE FILTROS
        // ═════════════════════════════════════════════════════════
        private string ConstruirDescripcionFiltros()
        {
            string filtros = $"Período: {DtpFechaInicio.Value:dd/MM/yyyy} - {DtpFechaFin.Value:dd/MM/yyyy}";

            if (CmbUsuario.SelectedIndex > 0)
                filtros += $", Usuario: {CmbUsuario.SelectedItem}";

            if (CmbTipoAccion.SelectedIndex > 0)
                filtros += $", Tipo: {CmbTipoAccion.SelectedItem}";

            if (!string.IsNullOrWhiteSpace(TxtBuscar.Text))
                filtros += $", Búsqueda: '{TxtBuscar.Text}'";

            return filtros;
        }

        // ═════════════════════════════════════════════════════════
        // GENERAR ARCHIVO EXCEL PROFESIONAL
        // ═════════════════════════════════════════════════════════
        private void GenerarArchivoExcelProfesional(DataTable datos, string rutaArchivo,
            string titulo, string nombreArchivo, string filtrosAplicados)
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Historial de Cambios");

                    // Ajustar alturas de filas de encabezado
                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(3).Height = 40;
                    worksheet.Row(4).Height = 30;

                    // Intentar agregar logo
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

                    // Título principal
                    worksheet.Cells["A1"].Value = "CONSEJO CIUDADANO";
                    worksheet.Cells["A1:I1"].Merge = true;
                    worksheet.Cells["A1"].Style.Font.Size = 20;
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["A1"].Style.Font.Color.SetColor(Color.FromArgb(111, 18, 64));
                    worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Título del reporte
                    worksheet.Cells["A2"].Value = titulo;
                    worksheet.Cells["A2:I2"].Merge = true;
                    worksheet.Cells["A2"].Style.Font.Size = 14;
                    worksheet.Cells["A2"].Style.Font.Bold = true;
                    worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Información de generación
                    worksheet.Cells["A3"].Value = $"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss} por {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario})";
                    worksheet.Cells["A3:I3"].Merge = true;
                    worksheet.Cells["A3"].Style.Font.Size = 10;
                    worksheet.Cells["A3"].Style.Font.Italic = true;
                    worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells["A3"].Style.Font.Color.SetColor(Color.Gray);

                    // Filtros aplicados
                    worksheet.Cells["A4"].Value = $"Filtros aplicados: {filtrosAplicados}";
                    worksheet.Cells["A4:I4"].Merge = true;
                    worksheet.Cells["A4"].Style.Font.Size = 9;
                    worksheet.Cells["A4"].Style.Font.Italic = true;
                    worksheet.Cells["A4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells["A4"].Style.Font.Color.SetColor(Color.DarkGray);

                    // Encabezados de columnas
                    int filaEncabezados = 5;
                    for (int i = 0; i < datos.Columns.Count; i++)
                    {
                        worksheet.Cells[filaEncabezados, i + 1].Value = datos.Columns[i].ColumnName;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Font.Color.SetColor(Color.White);
                        worksheet.Cells[filaEncabezados, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(111, 18, 64));
                        worksheet.Cells[filaEncabezados, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[filaEncabezados, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[filaEncabezados, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                    worksheet.Row(filaEncabezados).Height = 25;

                    // Datos
                    int filaActual = filaEncabezados + 1;
                    foreach (DataRow row in datos.Rows)
                    {
                        for (int i = 0; i < datos.Columns.Count; i++)
                        {
                            worksheet.Cells[filaActual, i + 1].Value = row[i].ToString();
                            worksheet.Cells[filaActual, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            worksheet.Cells[filaActual, i + 1].Style.WrapText = true;

                            // Filas intercaladas con color
                            if (filaActual % 2 == 0)
                            {
                                worksheet.Cells[filaActual, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[filaActual, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(253, 246, 249));
                            }
                        }
                        filaActual++;
                    }

                    filaActual++;

                    // Total de registros
                    worksheet.Cells[filaActual + 1, 1].Value = "TOTAL DE REGISTROS:";
                    worksheet.Cells[filaActual + 1, 1].Style.Font.Bold = true;
                    worksheet.Cells[filaActual + 1, 1].Style.Font.Size = 12;
                    worksheet.Cells[filaActual + 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    worksheet.Cells[filaActual + 1, 2].Value = datos.Rows.Count;
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Bold = true;
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Size = 12;
                    worksheet.Cells[filaActual + 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[filaActual + 1, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(247, 148, 29));
                    worksheet.Cells[filaActual + 1, 2].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[filaActual + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Row(filaActual + 1).Height = 25;

                    // Ajustar anchos de columnas
                    worksheet.Column(1).Width = 10;  // ID
                    worksheet.Column(2).Width = 20;  // Fecha y Hora
                    worksheet.Column(3).Width = 15;  // Usuario
                    worksheet.Column(4).Width = 30;  // Acción
                    worksheet.Column(5).Width = 15;  // Tabla
                    worksheet.Column(6).Width = 12;  // ID Registro
                    worksheet.Column(7).Width = 50;  // Descripción
                    worksheet.Column(8).Width = 15;  // IP
                    worksheet.Column(9).Width = 20;  // Máquina

                    // Configuración de impresión
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                    worksheet.PrinterSettings.FitToPage = true;
                    worksheet.PrinterSettings.FitToWidth = 1;
                    worksheet.PrinterSettings.FitToHeight = 0;

                    worksheet.PrinterSettings.LeftMargin = 0.25M;
                    worksheet.PrinterSettings.RightMargin = 0.25M;
                    worksheet.PrinterSettings.TopMargin = 0.5M;
                    worksheet.PrinterSettings.BottomMargin = 0.5M;

                    worksheet.PrinterSettings.HorizontalCentered = true;
                    worksheet.PrinterSettings.VerticalCentered = false;

                    worksheet.PrinterSettings.RepeatRows = worksheet.Cells["$5:$5"];

                    // Protección del archivo
                    worksheet.Protection.IsProtected = true;
                    worksheet.Protection.AllowSelectLockedCells = true;
                    worksheet.Protection.AllowSelectUnlockedCells = true;

                    string rangoCompleto = worksheet.Dimension.Address;
                    worksheet.Cells[rangoCompleto].Style.Locked = true;

                    // Guardar archivo
                    FileInfo file = new FileInfo(rutaArchivo);
                    package.SaveAs(file);
                }

                // ✅ REGISTRAR EXPORTACIÓN
                RegistrarExportacionHistorial(rutaArchivo, datos.Rows.Count);

                // Mensaje de éxito
                string carpetaContenedora = Path.GetDirectoryName(rutaArchivo);
                string carpetaRelativa = carpetaContenedora.Replace(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Documentos");

                MessageBox.Show(
                    $"✅ REPORTE EXPORTADO EXITOSAMENTE\n\n" +
                    $"📁 Ubicación:\n{rutaArchivo}\n\n" +
                    $"📊 Total de registros: {datos.Rows.Count}\n\n" +
                    $"🔒 Archivo protegido contra modificaciones\n" +
                    $"🖨️ Configurado para impresión en 1 hoja horizontal\n\n" +
                    $"💡 Puedes encontrarlo fácilmente en:\n" +
                    $"{carpetaRelativa}",
                    "Exportación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Preguntar si desea abrir
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

                // Preguntar si desea abrir carpeta
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
                    $"❌ Error al crear archivo Excel:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =============================================
        // BOTÓN: VER DETALLES
        // =============================================
        private void BtnVerDetalles_Click(object sender, EventArgs e)
        {
            if (DgvHistorial.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecciona un registro para ver sus detalles",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MostrarDetalles();
        }

        // =============================================
        // DOBLE CLIC EN FILA
        // =============================================
        private void DgvHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MostrarDetalles();
            }
        }

        // =============================================
        // MOSTRAR DETALLES DEL REGISTRO
        // =============================================
        private void MostrarDetalles()
        {
            SqlConnection conexion = null;
            try
            {
                int idCambio = Convert.ToInt32(DgvHistorial.SelectedRows[0].Cells["IdCambio"].Value);

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM HistorialCambios WHERE IdCambio = @Id", conexion);
                cmd.Parameters.AddWithValue("@Id", idCambio);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string detalles = "═══════════════════════════════════════\n";
                    detalles += "  DETALLES DEL REGISTRO\n";
                    detalles += "═══════════════════════════════════════\n\n";

                    detalles += $"📅 Fecha y Hora: {reader["FechaHora"]}\n";
                    detalles += $"👤 Usuario: {reader["NombreUsuario"]}\n";
                    detalles += $"🔧 Acción: {reader["TipoAccion"]}\n";
                    detalles += $"📋 Tabla Afectada: {reader["TablaAfectada"]}\n";
                    detalles += $"🆔 ID Registro: {reader["IdRegistroAfectado"]}\n\n";

                    detalles += "📝 Descripción:\n";
                    detalles += reader["Descripcion"].ToString() + "\n\n";

                    if (!reader.IsDBNull(reader.GetOrdinal("DatosAnteriores")))
                    {
                        detalles += "📊 Datos Anteriores:\n";
                        detalles += reader["DatosAnteriores"].ToString() + "\n\n";
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("DatosNuevos")))
                    {
                        detalles += "📊 Datos Nuevos:\n";
                        detalles += reader["DatosNuevos"].ToString() + "\n\n";
                    }

                    detalles += "═══════════════════════════════════════\n";
                    detalles += "  INFORMACIÓN DE CONEXIÓN\n";
                    detalles += "═══════════════════════════════════════\n\n";

                    detalles += $"🌐 Dirección IP: {reader["DireccionIP"]}\n";
                    detalles += $"💻 Nombre Máquina: {reader["NombreMaquina"]}\n";

                    MessageBox.Show(
                        detalles,
                        "Detalles del Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // BOTÓN: REFRESCAR
        // =============================================
        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            // Recargar los filtros (puede haber nuevos tipos de acción)
            ConfigurarFiltros();

            CargarHistorial();

            MessageBox.Show("Historial actualizado", "Actualizado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =============================================
        // ENTER EN TEXTBOX BUSCAR
        // =============================================
        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnBuscar_Click(sender, e);
                e.Handled = true;
            }
        }

        // =============================================
        // BOTÓN: CERRAR
        // =============================================
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODOS DE AUDITORÍA DE SEGURIDAD
        // ═════════════════════════════════════════════════════════

        /// <summary>
        /// Registra el acceso al visor de historial (seguridad)
        /// </summary>
        private void RegistrarAccesoHistorial()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", SesionActual.NombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Acceso a Visor de Historial");
                cmd.Parameters.AddWithValue("@TablaAfectada", "HistorialCambios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos", DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) accedió al visor de historial de cambios del sistema");
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar acceso a historial: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra una consulta/filtrado del historial (seguridad)
        /// </summary>
        private void RegistrarConsultaHistorial()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                string filtrosAplicados = ConstruirDescripcionFiltros();
                int totalRegistros = DgvHistorial.Rows.Count;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", SesionActual.NombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Consulta de Historial");
                cmd.Parameters.AddWithValue("@TablaAfectada", "HistorialCambios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos", $"Filtros: {filtrosAplicados}, Resultados: {totalRegistros} registros");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) consultó el historial con filtros: {filtrosAplicados}. Se obtuvieron {totalRegistros} registro(s)");
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar consulta de historial: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra la exportación del historial a Excel (seguridad)
        /// </summary>
        private void RegistrarExportacionHistorial(string rutaArchivo, int totalRegistros)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                string nombreArchivo = Path.GetFileName(rutaArchivo);
                string filtrosAplicados = ConstruirDescripcionFiltros();

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", SesionActual.NombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Exportación de Historial a Excel");
                cmd.Parameters.AddWithValue("@TablaAfectada", "HistorialCambios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos",
                    $"Archivo: {nombreArchivo}, Ruta: {rutaArchivo}, Registros exportados: {totalRegistros}, Filtros: {filtrosAplicados}");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) exportó {totalRegistros} registro(s) del historial a Excel. Archivo: {nombreArchivo}. Filtros aplicados: {filtrosAplicados}");
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar exportación de historial: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }
    }
}