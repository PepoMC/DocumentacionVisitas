using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmRegistrarVisita : BaseForm
    {
        // ═════════════════════════════════════════════════════════
        // VARIABLES DE CONTROL
        // ═════════════════════════════════════════════════════════
        private int? idVisitanteFrecuenteSeleccionado = null;
        private int? idPersonaVisitadaFrecuenteSeleccionada = null;
        private int? idVisitaSeleccionada = null;
        private string estadoVisitaSeleccionada = null;
        private bool visitaIniciada = false;

        private bool modoCorreccion = false;
        private int? idVisitaEnCorreccion = null;

        public FrmRegistrarVisita()
        {
            InitializeComponent();
            this.BtnGuardarVisita.Click += new System.EventHandler(this.BtnGuardarVisita_Click);
            ConfigurarEventosComboBox();
            VerificarYCerrarVisitasAntiguas();
        }

        private void VerificarYCerrarVisitasAntiguas()
        {
            try
            {
                object resultado = ConexionDB.EjecutarEscalar("sp_VerificarVisitasPendientesAnteriores", null);

                if (resultado != null)
                {
                    int cantidadPendientes = Convert.ToInt32(resultado);

                    if (cantidadPendientes > 0)
                    {
                        DataTable dt = ConexionDB.EjecutarConsulta("sp_ObtenerVisitasPendientesAnteriores", null);

                        string mensaje = string.Format("⚠️ VISITAS PENDIENTES DETECTADAS\n\n" +
                                       "Se encontraron {0} visita(s) de días anteriores\n" +
                                       "que NO fueron finalizadas correctamente.\n\n", cantidadPendientes);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            mensaje += "Detalles:\n";
                            foreach (DataRow row in dt.Rows)
                            {
                                string visitante = row["Visitante"].ToString();
                                DateTime fechaEntrada = Convert.ToDateTime(row["FechaHoraEntrada"]);
                                int dias = Convert.ToInt32(row["DiasTranscurridos"]);
                                mensaje += string.Format("• {0} - Entrada: {1:dd/MM/yyyy HH:mm} (Hace {2} día(s))\n",
                                    visitante, fechaEntrada, dias);
                            }
                        }

                        mensaje += "\n¿Deseas finalizar automáticamente estas visitas?\n\n" +
                                  "Se marcarán como finalizadas a las 23:59 del día que entraron.";

                        DialogResult respuesta = MessageBox.Show(
                            mensaje,
                            "Visitas Pendientes de Días Anteriores",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (respuesta == DialogResult.Yes)
                        {
                            object resultadoFinalizar = ConexionDB.EjecutarEscalar("sp_FinalizarVisitasAntiguasAutomaticamente", null);

                            if (resultadoFinalizar != null)
                            {
                                int visitasFinalizadas = Convert.ToInt32(resultadoFinalizar);

                                // ✅ REGISTRAR FINALIZACIÓN AUTOMÁTICA DE VISITAS ANTIGUAS
                                RegistrarFinalizacionAutomatica(visitasFinalizadas);

                                MessageBox.Show(
                                    string.Format("✅ Se finalizaron correctamente {0} visita(s) pendiente(s).\n\n" +
                                    "Ahora puedes registrar nuevas visitas sin problemas.", visitasFinalizadas),
                                    "Visitas Finalizadas",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al verificar visitas antiguas: " + ex.Message);
            }
        }

        private void ConfigurarEventosComboBox()
        {
            CbxVisitantesFrecuentes.TextChanged += CbxVisitantesFrecuentes_TextChanged;
            CbxVisitantesFrecuentes.DropDownStyle = ComboBoxStyle.DropDown;
            CbxVisitantesFrecuentes.AutoCompleteMode = AutoCompleteMode.None;

            CbxPersonasVisitadasFrecuentes.TextChanged += CbxPersonasVisitadasFrecuentes_TextChanged;
            CbxPersonasVisitadasFrecuentes.DropDownStyle = ComboBoxStyle.DropDown;
            CbxPersonasVisitadasFrecuentes.AutoCompleteMode = AutoCompleteMode.None;

            CbxCompania.Enter += CbxCompania_Enter;
            CbxCompania.Leave += CbxCompania_Leave;

            BtnBuscar.Click += BtnBuscar_Click;
            TxtBuscarVisitante.TextChanged += TxtBuscarVisitante_TextChanged;
            TxtBuscarVisitante.KeyPress += TxtBuscarVisitante_KeyPress;
        }

        private void FrmRegistrarVisita_Load(object sender, EventArgs e)
        {
            try
            {
                if (!ConexionDB.ProbarConexion())
                {
                    MessageBox.Show(
                        "❌ No se puede conectar a la base de datos.\n\n" +
                        "Verifica que SQL Server esté ejecutándose.",
                        "Error de Conexión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                CargarVisitantesFrecuentes();
                CargarPersonasVisitadasFrecuentes();
                CargarCompanias();
                CargarVisitasActivas();
                ActualizarContadorVisitas();

                BloquearCampos();

                BtnIniciarVisita.Enabled = true;
                BtnGuardarVisita.Enabled = false;
                BtnLimpiar.Enabled = false;
                BtnCorregirDatos.Enabled = false;
                BtnCancelarVisita.Enabled = false;
                BtnTerminarVisita.Enabled = true;
                BtnRegresar.Enabled = true;
                BtnBuscar.Enabled = true;
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

        private void BloquearCampos()
        {
            CbxVisitantesFrecuentes.Enabled = false;
            TxtNombreV.Enabled = false;
            TxtAPV.Enabled = false;
            TxtAMV.Enabled = false;
            CbxCompania.Enabled = false;
            CkbGuardarVisFrec.Enabled = false;

            CbxPersonasVisitadasFrecuentes.Enabled = false;
            TxtNombreE.Enabled = false;
            TxtAPE.Enabled = false;
            TxtAME.Enabled = false;
            CbxDepartamento.Enabled = false;
            CkbGuardarPerVisFrec.Enabled = false;

            Color colorBloqueado = Color.FromArgb(240, 240, 240);
            TxtNombreV.BackColor = colorBloqueado;
            TxtAPV.BackColor = colorBloqueado;
            TxtAMV.BackColor = colorBloqueado;
            TxtNombreE.BackColor = colorBloqueado;
            TxtAPE.BackColor = colorBloqueado;
            TxtAME.BackColor = colorBloqueado;
        }

        private void DesbloquearCampos()
        {
            CbxVisitantesFrecuentes.Enabled = true;
            TxtNombreV.Enabled = true;
            TxtAPV.Enabled = true;
            TxtAMV.Enabled = true;
            CbxCompania.Enabled = true;
            CkbGuardarVisFrec.Enabled = true;

            CbxPersonasVisitadasFrecuentes.Enabled = true;
            TxtNombreE.Enabled = true;
            TxtAPE.Enabled = true;
            TxtAME.Enabled = true;
            CbxDepartamento.Enabled = true;
            CkbGuardarPerVisFrec.Enabled = true;

            Color colorActivo = Color.White;
            TxtNombreV.BackColor = colorActivo;
            TxtAPV.BackColor = colorActivo;
            TxtAMV.BackColor = colorActivo;
            TxtNombreE.BackColor = colorActivo;
            TxtAPE.BackColor = colorActivo;
            TxtAME.BackColor = colorActivo;
        }

        private void ActualizarContadorVisitas()
        {
            try
            {
                object resultado = ConexionDB.EjecutarEscalar("sp_ContarVisitasDelDia", null);

                if (resultado != null)
                {
                    int totalVisitas = Convert.ToInt32(resultado);
                    LblContadorVisitas.Text = totalVisitas.ToString();
                }
                else
                {
                    LblContadorVisitas.Text = "0";
                }
            }
            catch
            {
                LblContadorVisitas.Text = "?";
            }
        }

        private void CargarVisitantesFrecuentes(string filtro = "")
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@TextoBusqueda", string.IsNullOrWhiteSpace(filtro) ? "" : filtro.Trim())
                };

                DataTable dt = ConexionDB.EjecutarConsulta("sp_BuscarVisitantesFrecuentesAutoComplete", parametros);

                CbxVisitantesFrecuentes.Items.Clear();
                CbxVisitantesFrecuentes.Items.Add("-- Seleccionar visitante frecuente --");

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CbxVisitantesFrecuentes.Items.Add(new ComboBoxItem
                        {
                            Value = Convert.ToInt32(row["IdVisitanteFrecuente"]),
                            Text = row["NombreCompleto"].ToString(),
                            Nombre = row["Nombre"].ToString(),
                            PrimerApellido = row["PrimerApellido"].ToString(),
                            SegundoApellido = row["SegundoApellido"] != DBNull.Value ? row["SegundoApellido"].ToString() : "",
                            Extra = row["NombreCompania"] != DBNull.Value ? row["NombreCompania"].ToString() : "N/A"
                        });
                    }
                }

                if (string.IsNullOrWhiteSpace(filtro))
                    CbxVisitantesFrecuentes.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar visitantes frecuentes:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarPersonasVisitadasFrecuentes(string filtro = "")
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@TextoBusqueda", string.IsNullOrWhiteSpace(filtro) ? "" : filtro.Trim())
                };

                DataTable dt = ConexionDB.EjecutarConsulta("sp_BuscarPersonasVisitadasFrecuentesAutoComplete", parametros);

                CbxPersonasVisitadasFrecuentes.Items.Clear();
                CbxPersonasVisitadasFrecuentes.Items.Add("-- Seleccionar persona visitada frecuente --");

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CbxPersonasVisitadasFrecuentes.Items.Add(new ComboBoxItem
                        {
                            Value = Convert.ToInt32(row["IdPersonaVisitada"]),
                            Text = row["NombreCompleto"].ToString(),
                            Nombre = row["Nombre"].ToString(),
                            PrimerApellido = row["PrimerApellido"].ToString(),
                            SegundoApellido = row["SegundoApellido"] != DBNull.Value ? row["SegundoApellido"].ToString() : "",
                            Extra = row["NombreDepartamento"] != DBNull.Value ? row["NombreDepartamento"].ToString() : ""
                        });
                    }
                }

                if (string.IsNullOrWhiteSpace(filtro))
                    CbxPersonasVisitadasFrecuentes.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar personas visitadas frecuentes:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarCompanias()
        {
            try
            {
                string query = "SELECT NombreCompania FROM Companias WHERE Activo = 1 ORDER BY NombreCompania";
                DataTable dt = ConexionDB.EjecutarConsultaDirecta(query);

                CbxCompania.Items.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CbxCompania.Items.Add(row["NombreCompania"].ToString());
                    }
                }

                if (CbxCompania.Items.Contains("N/A"))
                {
                    CbxCompania.SelectedItem = "N/A";
                }
                else if (CbxCompania.Items.Count > 0)
                {
                    CbxCompania.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar compañías:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarVisitasActivas(string filtro = "")
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@TextoBusqueda", string.IsNullOrWhiteSpace(filtro) ? "" : filtro.Trim())
                };

                DataTable dt = ConexionDB.EjecutarConsulta("sp_BuscarVisitasDelDia", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("HoraEntradaTexto", typeof(string));
                    dt.Columns.Add("HoraSalidaTexto", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Hora Entrada"] != DBNull.Value)
                        {
                            DateTime horaEntrada = Convert.ToDateTime(row["Hora Entrada"]);
                            row["HoraEntradaTexto"] = horaEntrada.ToString("dd/MM/yyyy HH:mm");
                        }
                        else
                        {
                            row["HoraEntradaTexto"] = "";
                        }

                        if (row["Hora Salida"] == DBNull.Value)
                        {
                            row["HoraSalidaTexto"] = "En curso";
                        }
                        else
                        {
                            DateTime horaSalida = Convert.ToDateTime(row["Hora Salida"]);
                            row["HoraSalidaTexto"] = horaSalida.ToString("dd/MM/yyyy HH:mm");
                        }
                    }

                    dt.Columns.Remove("Hora Entrada");
                    dt.Columns.Remove("Hora Salida");

                    dt.Columns["HoraEntradaTexto"].ColumnName = "Hora Entrada";
                    dt.Columns["HoraSalidaTexto"].ColumnName = "Hora Salida";

                    DgvVisitas.DataSource = dt;

                    if (DgvVisitas.Columns.Count > 0)
                    {
                        DgvVisitas.Columns["ID"].Width = 60;
                        DgvVisitas.Columns["Visitante"].Width = 180;
                        DgvVisitas.Columns["Compañía"].Width = 130;
                        DgvVisitas.Columns["Persona Visitada"].Width = 180;
                        DgvVisitas.Columns["Departamento"].Width = 130;
                        DgvVisitas.Columns["Hora Entrada"].Width = 130;
                        DgvVisitas.Columns["Hora Salida"].Width = 130;
                        DgvVisitas.Columns["Estado"].Width = 100;

                        foreach (DataGridViewRow gridRow in DgvVisitas.Rows)
                        {
                            string estado = gridRow.Cells["Estado"].Value?.ToString();
                            string horaSalida = gridRow.Cells["Hora Salida"].Value?.ToString();

                            if (estado == "En curso" || horaSalida == "En curso")
                            {
                                gridRow.Cells["Hora Salida"].Style.ForeColor = Color.FromArgb(247, 148, 29);
                                gridRow.Cells["Hora Salida"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                                gridRow.Cells["Estado"].Style.ForeColor = Color.FromArgb(247, 148, 29);
                                gridRow.Cells["Estado"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                            }
                            else if (estado == "Finalizada")
                            {
                                gridRow.Cells["Estado"].Style.ForeColor = Color.FromArgb(40, 167, 69);
                                gridRow.Cells["Estado"].Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                            }
                        }
                    }

                    DgvVisitas.Refresh();
                }
                else
                {
                    DgvVisitas.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar visitas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CbxVisitantesFrecuentes_TextChanged(object sender, EventArgs e)
        {
            if (!CbxVisitantesFrecuentes.Enabled) return;

            string texto = CbxVisitantesFrecuentes.Text;

            if (string.IsNullOrWhiteSpace(texto) || texto.StartsWith("--")) return;

            if (CbxVisitantesFrecuentes.SelectedIndex > 0) return;

            try
            {
                int cursorPosition = CbxVisitantesFrecuentes.SelectionStart;

                CargarVisitantesFrecuentes(texto);

                CbxVisitantesFrecuentes.Text = texto;
                CbxVisitantesFrecuentes.SelectionStart = cursorPosition;
                CbxVisitantesFrecuentes.SelectionLength = 0;

                if (CbxVisitantesFrecuentes.Items.Count > 1)
                {
                    CbxVisitantesFrecuentes.DroppedDown = true;
                }
            }
            catch
            {
            }
        }

        private void CbxPersonasVisitadasFrecuentes_TextChanged(object sender, EventArgs e)
        {
            if (!CbxPersonasVisitadasFrecuentes.Enabled) return;

            string texto = CbxPersonasVisitadasFrecuentes.Text;

            if (string.IsNullOrWhiteSpace(texto) || texto.StartsWith("--")) return;

            if (CbxPersonasVisitadasFrecuentes.SelectedIndex > 0) return;

            try
            {
                int cursorPosition = CbxPersonasVisitadasFrecuentes.SelectionStart;

                CargarPersonasVisitadasFrecuentes(texto);

                CbxPersonasVisitadasFrecuentes.Text = texto;
                CbxPersonasVisitadasFrecuentes.SelectionStart = cursorPosition;
                CbxPersonasVisitadasFrecuentes.SelectionLength = 0;

                if (CbxPersonasVisitadasFrecuentes.Items.Count > 1)
                {
                    CbxPersonasVisitadasFrecuentes.DroppedDown = true;
                }
            }
            catch
            {
            }
        }

        private void CbxVisitantesFrecuentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CbxVisitantesFrecuentes.SelectedIndex <= 0)
                {
                    idVisitanteFrecuenteSeleccionado = null;
                    return;
                }

                if (CbxVisitantesFrecuentes.SelectedItem is ComboBoxItem item)
                {
                    idVisitanteFrecuenteSeleccionado = item.Value;

                    TxtNombreV.Text = item.Nombre;
                    TxtAPV.Text = item.PrimerApellido;
                    TxtAMV.Text = item.SegundoApellido ?? "";

                    if (!string.IsNullOrWhiteSpace(item.Extra))
                    {
                        CbxCompania.Text = item.Extra;
                    }

                    TxtNombreE.Focus();
                }
                else
                {
                    idVisitanteFrecuenteSeleccionado = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del visitante:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxPersonasVisitadasFrecuentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CbxPersonasVisitadasFrecuentes.SelectedIndex <= 0)
                {
                    idPersonaVisitadaFrecuenteSeleccionada = null;
                    return;
                }

                if (CbxPersonasVisitadasFrecuentes.SelectedItem is ComboBoxItem item)
                {
                    idPersonaVisitadaFrecuenteSeleccionada = item.Value;

                    TxtNombreE.Text = item.Nombre;
                    TxtAPE.Text = item.PrimerApellido;
                    TxtAME.Text = item.SegundoApellido ?? "";

                    if (!string.IsNullOrWhiteSpace(item.Extra))
                    {
                        CbxDepartamento.Text = item.Extra;
                    }

                    BtnGuardarVisita.Focus();
                }
                else
                {
                    idPersonaVisitadaFrecuenteSeleccionada = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de la persona visitada:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxCompania_Enter(object sender, EventArgs e)
        {
            if (CbxCompania.Text == "N/A")
            {
                CbxCompania.Text = "";
            }
        }

        private void CbxCompania_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CbxCompania.Text))
            {
                CbxCompania.Text = "N/A";
            }
        }

        private void BtnIniciarVisita_Click(object sender, EventArgs e)
        {
            try
            {
                DesbloquearCampos();
                BtnGuardarVisita.Enabled = true;
                BtnLimpiar.Enabled = true;
                BtnCancelarVisita.Enabled = true;
                BtnIniciarVisita.Enabled = false;
                TxtNombreV.Focus();
                visitaIniciada = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar visita:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODO GUARDAR VISITA - CON AUDITORÍA COMPLETA Y ETIQUETA
        // ═════════════════════════════════════════════════════════
        private void BtnGuardarVisita_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoCorreccion && idVisitaEnCorreccion.HasValue)
                {
                    GuardarCorreccionVisita();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtNombreV.Text) || string.IsNullOrWhiteSpace(TxtAPV.Text))
                {
                    MessageBox.Show(
                        "⚠️ Debes completar al menos:\n\n• Nombre del Visitante\n• Primer Apellido del Visitante",
                        "Datos del Visitante Incompletos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    TxtNombreV.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtNombreE.Text) || string.IsNullOrWhiteSpace(TxtAPE.Text))
                {
                    MessageBox.Show(
                        "⚠️ Debes completar al menos:\n\n• Nombre de la Persona Visitada\n• Primer Apellido de la Persona Visitada",
                        "Datos de Persona Visitada Incompletos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    TxtNombreE.Focus();
                    return;
                }

                

                

                if (VisitanteYaTieneVisitaActiva(TxtNombreV.Text.Trim(), TxtAPV.Text.Trim(), TxtAMV.Text.Trim()))
                {
                    MessageBox.Show(
                        string.Format("❌ VISITA DUPLICADA DETECTADA\n\n" +
                        "El visitante {0} {1} {2}\n" +
                        "ya tiene una visita EN CURSO (no ha registrado su salida).\n\n" +
                        "Primero debe finalizar su visita anterior antes de registrar una nueva entrada.",
                        TxtNombreV.Text, TxtAPV.Text, TxtAMV.Text),
                        "Visitante con Visita Activa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                int idCompania = ObtenerOCrearCompania(CbxCompania.Text);
                int idDepartamento = ObtenerOCrearDepartamento(CbxDepartamento.Text);

                bool visitanteActualizado = false;
                bool visitanteNuevoCreado = false;

                if (CkbGuardarVisFrec.Checked)
                {
                    DataTable dtVisitanteExistente = BuscarVisitanteFrecuentePorNombre(
                        TxtNombreV.Text.Trim(),
                        TxtAPV.Text.Trim(),
                        TxtAMV.Text.Trim()
                    );

                    if (dtVisitanteExistente != null && dtVisitanteExistente.Rows.Count > 0)
                    {
                        DataRow rowExistente = dtVisitanteExistente.Rows[0];
                        int idVisitanteFrecuenteExistente = Convert.ToInt32(rowExistente["IdVisitanteFrecuente"]);
                        int idCompaniaAnterior = Convert.ToInt32(rowExistente["IdCompania"]);

                        if (idCompaniaAnterior != idCompania)
                        {
                            ActualizarCompaniaVisitanteFrecuente(idVisitanteFrecuenteExistente, idCompania);
                            idVisitanteFrecuenteSeleccionado = idVisitanteFrecuenteExistente;
                            visitanteActualizado = true;
                        }
                        else
                        {
                            idVisitanteFrecuenteSeleccionado = idVisitanteFrecuenteExistente;
                        }
                    }
                    else
                    {
                        idVisitanteFrecuenteSeleccionado = RegistrarVisitanteFrecuente(
                            TxtNombreV.Text.Trim(),
                            TxtAPV.Text.Trim(),
                            TxtAMV.Text.Trim(),
                            idCompania
                        );
                        visitanteNuevoCreado = idVisitanteFrecuenteSeleccionado.HasValue;
                    }
                }

                bool personaVisitadaActualizada = false;
                bool personaVisitadaNuevaCreada = false;

                if (CkbGuardarPerVisFrec.Checked)
                {
                    DataTable dtPersonaExistente = BuscarPersonaVisitadaFrecuentePorNombre(
                        TxtNombreE.Text.Trim(),
                        TxtAPE.Text.Trim(),
                        TxtAME.Text.Trim()
                    );

                    if (dtPersonaExistente != null && dtPersonaExistente.Rows.Count > 0)
                    {
                        DataRow rowExistente = dtPersonaExistente.Rows[0];
                        int idPersonaVisitadaExistente = Convert.ToInt32(rowExistente["IdPersonaVisitada"]);
                        int idDepartamentoAnterior = Convert.ToInt32(rowExistente["IdDepartamento"]);

                        if (idDepartamentoAnterior != idDepartamento)
                        {
                            ActualizarDepartamentoPersonaVisitadaFrecuente(idPersonaVisitadaExistente, idDepartamento);
                            idPersonaVisitadaFrecuenteSeleccionada = idPersonaVisitadaExistente;
                            personaVisitadaActualizada = true;
                        }
                        else
                        {
                            idPersonaVisitadaFrecuenteSeleccionada = idPersonaVisitadaExistente;
                        }
                    }
                    else
                    {
                        idPersonaVisitadaFrecuenteSeleccionada = RegistrarPersonaVisitadaFrecuente(
                            TxtNombreE.Text.Trim(),
                            TxtAPE.Text.Trim(),
                            TxtAME.Text.Trim(),
                            idDepartamento
                        );
                        personaVisitadaNuevaCreada = idPersonaVisitadaFrecuenteSeleccionada.HasValue;
                    }
                }

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@NombreVisitante", TxtNombreV.Text.Trim()),
                    new SqlParameter("@PrimerApellidoVisitante", TxtAPV.Text.Trim()),
                    new SqlParameter("@SegundoApellidoVisitante", string.IsNullOrWhiteSpace(TxtAMV.Text) ? (object)DBNull.Value : TxtAMV.Text.Trim()),
                    new SqlParameter("@IdCompania", idCompania),
                    new SqlParameter("@IdVisitanteFrecuente", idVisitanteFrecuenteSeleccionado.HasValue ? (object)idVisitanteFrecuenteSeleccionado.Value : DBNull.Value),
                    new SqlParameter("@NombrePersonaVisitada", TxtNombreE.Text.Trim()),
                    new SqlParameter("@PrimerApellidoPersonaVisitada", TxtAPE.Text.Trim()),
                    new SqlParameter("@SegundoApellidoPersonaVisitada", string.IsNullOrWhiteSpace(TxtAME.Text) ? (object)DBNull.Value : TxtAME.Text.Trim()),
                    new SqlParameter("@IdDepartamento", idDepartamento),
                    new SqlParameter("@IdPersonaVisitadaFrecuente", idPersonaVisitadaFrecuenteSeleccionada.HasValue ? (object)idPersonaVisitadaFrecuenteSeleccionada.Value : DBNull.Value)
                };

                object resultado = ConexionDB.EjecutarEscalar("sp_RegistrarVisita", parametros);

                if (resultado != null)
                {
                    int idVisita = Convert.ToInt32(resultado);

                    // ✅ REGISTRAR EN AUDITORÍA
                    string nombreCompletoVisitante = $"{TxtNombreV.Text} {TxtAPV.Text} {TxtAMV.Text}".Trim();
                    string nombreCompletoVisitado = $"{TxtNombreE.Text} {TxtAPE.Text} {TxtAME.Text}".Trim();

                    RegistrarAuditoria(
                        "Registro de Visita",
                        "Visitas",
                        idVisita,
                        $"Se registró el ingreso del visitante {nombreCompletoVisitante} de {CbxCompania.Text} para visitar a {nombreCompletoVisitado} del área de {CbxDepartamento.Text}",
                        null,
                        $"ID Visita: {idVisita}, Visitante: {nombreCompletoVisitante}, Compañía: {CbxCompania.Text}, Persona Visitada: {nombreCompletoVisitado}, Departamento: {CbxDepartamento.Text}, Hora Entrada: {DateTime.Now:yyyy-MM-dd HH:mm:ss}, Estado: En curso"
                    );

                    // ═════════════════════════════════════════════════════════
                    // 🏷️ IMPRIMIR ETIQUETA DYMO
                    // ═════════════════════════════════════════════════════════
                    try
                    {
                        // Preparar los datos para la etiqueta
                        string companiaParaEtiqueta = (CbxCompania.Text == "N/A" || string.IsNullOrWhiteSpace(CbxCompania.Text))
                            ? null
                            : CbxCompania.Text;

                        // Crear e imprimir la etiqueta CON DEPARTAMENTO
                        EtiquetaDymo etiqueta = new EtiquetaDymo(
                            nombreCompletoVisitante,
                            nombreCompletoVisitado,
                            companiaParaEtiqueta,
                            CbxDepartamento.Text
                        );

                        etiqueta.Imprimir();
                    }
                    catch (Exception exEtiqueta)
                    {
                        // Si falla la impresión, mostrar error pero continuar
                        MessageBox.Show(
                            "⚠️ La visita se registró correctamente, pero hubo un error al imprimir la etiqueta:\n\n" +
                            exEtiqueta.Message,
                            "Error de Impresión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    // ═════════════════════════════════════════════════════════

                    string mensajeCompleto = string.Format("✅ ¡VISITA REGISTRADA EXITOSAMENTE!\n\n" +
                        "ID de Visita: {0}\n" +
                        "Visitante: {1}\n" +
                        "Visita a: {2}\n\n" +
                        "Hora de entrada: {3:dd/MM/yyyy HH:mm}\n\n" +
                        "🏷️ Etiqueta impresa exitosamente",
                        idVisita,
                        nombreCompletoVisitante,
                        nombreCompletoVisitado,
                        DateTime.Now);

                    if (visitanteNuevoCreado)
                    {
                        mensajeCompleto += string.Format("\n\n✅ {0}\nguardado en Visitantes Frecuentes",
                            nombreCompletoVisitante);
                    }
                    else if (visitanteActualizado)
                    {
                        mensajeCompleto += string.Format("\n\n🔄 {0}\nactualizado en Visitantes Frecuentes (nueva compañía: {1})",
                            nombreCompletoVisitante, CbxCompania.Text);
                    }

                    if (personaVisitadaNuevaCreada)
                    {
                        mensajeCompleto += string.Format("\n\n✅ {0}\nguardado en Personas Visitadas Frecuentes",
                            nombreCompletoVisitado);
                    }
                    else if (personaVisitadaActualizada)
                    {
                        mensajeCompleto += string.Format("\n\n🔄 {0}\nactualizado en Personas Visitadas Frecuentes (nuevo departamento: {1})",
                            nombreCompletoVisitado, CbxDepartamento.Text);
                    }

                    MessageBox.Show(
                        mensajeCompleto,
                        "Visita Guardada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CargarVisitasActivas();
                    CargarVisitantesFrecuentes();
                    CargarPersonasVisitadasFrecuentes();
                    CargarCompanias();
                    ActualizarContadorVisitas();

                    LimpiarCampos();
                    BloquearCampos();
                    BtnIniciarVisita.Enabled = true;
                    BtnGuardarVisita.Enabled = false;
                    BtnLimpiar.Enabled = false;
                    BtnCancelarVisita.Enabled = false;
                    visitaIniciada = false;
                }
                else
                {
                    MessageBox.Show(
                        "⚠️ No se pudo registrar la visita.\nVerifica la conexión a la base de datos.",
                        "Error al Guardar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al guardar la visita:\n\n" + ex.Message + "\n\n" + ex.StackTrace,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (!visitaIniciada)
            {
                MessageBox.Show("ℹ️ No hay campos para limpiar.\n\nPrimero inicia una visita.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "🔄 ¿Limpiar campos?\n\n" +
                "Los datos que hayas escrito se borrarán,\n" +
                "pero SEGUIRÁS REGISTRANDO la visita.\n\n" +
                "Usa esto si te equivocaste al escribir.",
                "Limpiar Campos",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                LimpiarCampos();
                TxtNombreV.Focus();
            }
        }

        private void BtnCancelarVisita_Click(object sender, EventArgs e)
        {
            if (modoCorreccion)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "⚠️ ¿CANCELAR CORRECCIÓN?\n\n" +
                    "Se descartarán todos los cambios realizados.\n\n" +
                    "¿Estás seguro?",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    SalirModoCorreccion();
                }
                return;
            }

            if (!visitaIniciada)
            {
                MessageBox.Show(
                    "ℹ️ No hay visita iniciada para cancelar.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            DialogResult confirmacion2 = MessageBox.Show(
                "⚠️ ¿CANCELAR VISITA COMPLETAMENTE?\n\n" +
                "Esta acción:\n" +
                "❌ Borrará todos los datos ingresados\n" +
                "❌ NO se registrará ninguna visita\n" +
                "🔄 Volverás al estado inicial\n\n" +
                "Usa esto si el visitante se fue sin registrarse.\n\n" +
                "¿Estás seguro de CANCELAR?",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion2 == DialogResult.Yes)
            {
                LimpiarCampos();
                BloquearCampos();

                BtnIniciarVisita.Enabled = true;
                BtnGuardarVisita.Enabled = false;
                BtnLimpiar.Enabled = false;
                BtnCancelarVisita.Enabled = false;

                visitaIniciada = false;
            }
        }

        private void LimpiarCampos()
        {
            TxtNombreV.Clear();
            TxtAPV.Clear();
            TxtAMV.Clear();
            CbxCompania.Text = "N/A";
            CkbGuardarVisFrec.Checked = false;
            if (CbxVisitantesFrecuentes.Items.Count > 0)
                CbxVisitantesFrecuentes.SelectedIndex = 0;

            TxtNombreE.Clear();
            TxtAPE.Clear();
            TxtAME.Clear();
            if (CbxDepartamento.Items.Count > 0)
                CbxDepartamento.SelectedIndex = -1;
            CkbGuardarPerVisFrec.Checked = false;
            if (CbxPersonasVisitadasFrecuentes.Items.Count > 0)
                CbxPersonasVisitadasFrecuentes.SelectedIndex = 0;

            idVisitanteFrecuenteSeleccionado = null;
            idPersonaVisitadaFrecuenteSeleccionada = null;
            idVisitaSeleccionada = null;
            estadoVisitaSeleccionada = null;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = TxtBuscarVisitante.Text.Trim();
            CargarVisitasActivas(textoBusqueda);
        }

        private void TxtBuscarVisitante_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscarVisitante.Text))
            {
                CargarVisitasActivas("");
            }
        }

        private void TxtBuscarVisitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                string textoBusqueda = TxtBuscarVisitante.Text.Trim();
                CargarVisitasActivas(textoBusqueda);
            }
        }

        // ═════════════════════════════════════════════════════════
        // TERMINAR VISITA - CON AUDITORÍA COMPLETA
        // ═════════════════════════════════════════════════════════
        private void BtnTerminarVisita_Click(object sender, EventArgs e)
        {
            try
            {
                if (idVisitaSeleccionada == null)
                {
                    MessageBox.Show(
                        "⚠️ Debes seleccionar una visita de la tabla.\n\nHaz clic en la fila de la persona que va a salir.",
                        "Ninguna Visita Seleccionada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    string.Format("¿Confirmas que deseas FINALIZAR la visita ID {0}?\n\nSe registrará la hora de salida.",
                    idVisitaSeleccionada),
                    "Confirmar Salida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    SqlParameter[] parametros = new SqlParameter[]
                    {
                        new SqlParameter("@IdVisita", idVisitaSeleccionada.Value)
                    };

                    bool exito = ConexionDB.EjecutarComando("sp_FinalizarVisita", parametros);

                    if (exito)
                    {
                        // ✅ REGISTRAR EN AUDITORÍA
                        string visitante = "";
                        string personaVisitada = "";
                        DateTime horaEntrada = DateTime.Now;

                        if (DgvVisitas.SelectedRows.Count > 0)
                        {
                            DataGridViewRow fila = DgvVisitas.SelectedRows[0];
                            visitante = fila.Cells["Visitante"].Value?.ToString() ?? "Desconocido";
                            personaVisitada = fila.Cells["Persona Visitada"].Value?.ToString() ?? "Desconocido";
                            if (fila.Cells["Hora Entrada"].Value != null && DateTime.TryParse(fila.Cells["Hora Entrada"].Value.ToString(), out DateTime entrada))
                            {
                                horaEntrada = entrada;
                            }
                        }

                        TimeSpan duracion = DateTime.Now - horaEntrada;

                        RegistrarAuditoria(
                            "Finalización de Visita",
                            "Visitas",
                            idVisitaSeleccionada.Value,
                            $"Se finalizó la visita de {visitante} quien visitó a {personaVisitada}. Duración total: {FormatearDuracionVisita(duracion)}",
                            $"Estado: En curso, Hora Salida: NULL, Visita Activa: 1",
                            $"Estado: Finalizada, Hora Salida: {DateTime.Now:yyyy-MM-dd HH:mm:ss}, Visita Activa: 0, Duración: {FormatearDuracionVisita(duracion)}"
                        );

                        MessageBox.Show(
                            string.Format("✅ Visita {0} finalizada correctamente.\n\n" +
                            "Visitante: {1}\n" +
                            "Hora de salida: {2:dd/MM/yyyy HH:mm}\n" +
                            "Duración: {3}",
                            idVisitaSeleccionada, visitante, DateTime.Now, FormatearDuracionVisita(duracion)),
                            "Salida Registrada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        CargarVisitasActivas();
                        ActualizarContadorVisitas();
                        idVisitaSeleccionada = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al finalizar visita:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Seleccionar(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow filaSeleccionada = DgvVisitas.Rows[e.RowIndex];
                    idVisitaSeleccionada = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);

                    estadoVisitaSeleccionada = filaSeleccionada.Cells["Estado"].Value?.ToString();

                    DgvVisitas.ClearSelection();
                    filaSeleccionada.Selected = true;

                    if (estadoVisitaSeleccionada == "En curso")
                    {
                        BtnCorregirDatos.Enabled = true;
                    }
                    else
                    {
                        BtnCorregirDatos.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar visita:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Deseas regresar al menú principal?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnCorregirDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (idVisitaSeleccionada == null)
                {
                    MessageBox.Show(
                        "⚠️ Debes seleccionar una visita de la tabla para corregir sus datos.",
                        "Ninguna Visita Seleccionada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (estadoVisitaSeleccionada != "En curso")
                {
                    MessageBox.Show(
                        "⚠️ NO SE PUEDE CORREGIR ESTA VISITA\n\n" +
                        "Solo puedes corregir visitas que estén EN CURSO.\n\n" +
                        "Esta visita ya ha sido FINALIZADA, por lo tanto\n" +
                        "sus datos NO pueden ser modificados.\n\n" +
                        "💡 Solo las visitas activas (sin hora de salida)\n" +
                        "pueden ser corregidas.",
                        "Visita Finalizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (visitaIniciada && !modoCorreccion)
                {
                    DialogResult resultado = MessageBox.Show(
                        "⚠️ HAY UNA VISITA EN PROCESO DE REGISTRO\n\n" +
                        "Estás registrando una nueva visita. Si continúas con la corrección,\n" +
                        "se cancelará el registro actual.\n\n" +
                        "¿Deseas cancelar el registro y proceder con la corrección?",
                        "Visita en Proceso",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (resultado == DialogResult.No)
                        return;

                    LimpiarCampos();
                    BloquearCampos();
                    visitaIniciada = false;
                    BtnIniciarVisita.Enabled = true;
                    BtnGuardarVisita.Enabled = false;
                    BtnLimpiar.Enabled = false;
                    BtnCancelarVisita.Enabled = false;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "✏️ MODO CORRECCIÓN DE DATOS\n\n" +
                    string.Format("¿Deseas corregir los datos de la visita ID {0}?\n\n", idVisitaSeleccionada) +
                    "Podrás modificar:\n" +
                    "• Nombres y apellidos del visitante\n" +
                    "• Compañía\n" +
                    "• Nombres y apellidos de la persona visitada\n" +
                    "• Departamento\n\n" +
                    "NO se modificarán las horas de entrada y salida.",
                    "Confirmar Corrección",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                CargarDatosVisitaParaCorreccion(idVisitaSeleccionada.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al iniciar corrección:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarDatosVisitaParaCorreccion(int idVisita)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdVisita", idVisita)
                };

                DataTable dt = ConexionDB.EjecutarConsulta("sp_ObtenerDatosVisitaParaEdicion", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    modoCorreccion = true;
                    idVisitaEnCorreccion = idVisita;

                    DesbloquearCampos();

                    TxtNombreV.Text = row["NombreVisitante"].ToString();
                    TxtAPV.Text = row["PrimerApellidoVisitante"].ToString();
                    TxtAMV.Text = row["SegundoApellidoVisitante"] != DBNull.Value
                        ? row["SegundoApellidoVisitante"].ToString()
                        : "";
                    CbxCompania.Text = row["NombreCompania"].ToString();

                    TxtNombreE.Text = row["NombrePersonaVisitada"].ToString();
                    TxtAPE.Text = row["PrimerApellidoPersonaVisitada"].ToString();
                    TxtAME.Text = row["SegundoApellidoPersonaVisitada"] != DBNull.Value
                        ? row["SegundoApellidoPersonaVisitada"].ToString()
                        : "";
                    CbxDepartamento.Text = row["NombreDepartamento"].ToString();

                    if (row["IdVisitanteFrecuente"] != DBNull.Value)
                        idVisitanteFrecuenteSeleccionado = Convert.ToInt32(row["IdVisitanteFrecuente"]);
                    else
                        idVisitanteFrecuenteSeleccionado = null;

                    if (row["IdPersonaVisitadaFrecuente"] != DBNull.Value)
                        idPersonaVisitadaFrecuenteSeleccionada = Convert.ToInt32(row["IdPersonaVisitadaFrecuente"]);
                    else
                        idPersonaVisitadaFrecuenteSeleccionada = null;

                    CbxVisitantesFrecuentes.Enabled = false;
                    CbxPersonasVisitadasFrecuentes.Enabled = false;

                    CkbGuardarVisFrec.Enabled = true;
                    CkbGuardarPerVisFrec.Enabled = true;

                    BtnIniciarVisita.Enabled = false;
                    BtnGuardarVisita.Enabled = true;
                    BtnGuardarVisita.Text = "💾 GUARDAR CORRECCIÓN";
                    BtnGuardarVisita.BackColor = Color.FromArgb(40, 167, 69);
                    BtnLimpiar.Enabled = false;
                    BtnCancelarVisita.Enabled = true;
                    BtnCancelarVisita.Text = "❌ CANCELAR CORRECCIÓN";
                    BtnCorregirDatos.Enabled = false;
                    BtnTerminarVisita.Enabled = false;

                    TxtNombreV.Focus();

                    MessageBox.Show(
                        "✅ DATOS CARGADOS PARA CORRECCIÓN\n\n" +
                        "Puedes modificar los campos necesarios.\n\n" +
                        "Cuando termines, presiona 'GUARDAR CORRECCIÓN'.",
                        "Modo Corrección Activado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "⚠️ No se pudieron cargar los datos de la visita.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al cargar datos para corrección:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ═════════════════════════════════════════════════════════
        // GUARDAR CORRECCIÓN - CON AUDITORÍA COMPLETA
        // ═════════════════════════════════════════════════════════
        private void GuardarCorreccionVisita()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtNombreV.Text) || string.IsNullOrWhiteSpace(TxtAPV.Text))
                {
                    MessageBox.Show(
                        "⚠️ Debes completar al menos:\n\n• Nombre del Visitante\n• Primer Apellido del Visitante",
                        "Datos Incompletos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                

                DialogResult confirmacion = MessageBox.Show(
                    "💾 ¿CONFIRMAR CORRECCIÓN DE DATOS?\n\n" +
                    "Se actualizarán los datos de la visita sin modificar las horas.\n\n" +
                    "¿Estás seguro?",
                    "Confirmar Corrección",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                int idCompania = ObtenerOCrearCompania(CbxCompania.Text);
                int idDepartamento = ObtenerOCrearDepartamento(CbxDepartamento.Text);

                if (idVisitanteFrecuenteSeleccionado.HasValue && CkbGuardarVisFrec.Checked)
                {
                    ActualizarCompaniaVisitanteFrecuente(idVisitanteFrecuenteSeleccionado.Value, idCompania);
                }

                if (idPersonaVisitadaFrecuenteSeleccionada.HasValue && CkbGuardarPerVisFrec.Checked)
                {
                    ActualizarDepartamentoPersonaVisitadaFrecuente(idPersonaVisitadaFrecuenteSeleccionada.Value, idDepartamento);
                }

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdVisita", idVisitaEnCorreccion.Value),
                    new SqlParameter("@NombreVisitante", TxtNombreV.Text.Trim()),
                    new SqlParameter("@PrimerApellidoVisitante", TxtAPV.Text.Trim()),
                    new SqlParameter("@SegundoApellidoVisitante", string.IsNullOrWhiteSpace(TxtAMV.Text) ? (object)DBNull.Value : TxtAMV.Text.Trim()),
                    new SqlParameter("@IdCompania", idCompania),
                    new SqlParameter("@NombrePersonaVisitada", TxtNombreE.Text.Trim()),
                    new SqlParameter("@PrimerApellidoPersonaVisitada", TxtAPE.Text.Trim()),
                    new SqlParameter("@SegundoApellidoPersonaVisitada", string.IsNullOrWhiteSpace(TxtAME.Text) ? (object)DBNull.Value : TxtAME.Text.Trim()),
                    new SqlParameter("@IdDepartamento", idDepartamento)
                };

                object resultado = ConexionDB.EjecutarEscalar("sp_ActualizarDatosVisita", parametros);

                if (resultado != null)
                {
                    // ✅ REGISTRAR EN AUDITORÍA
                    string nombreCompletoVisitante = $"{TxtNombreV.Text} {TxtAPV.Text} {TxtAMV.Text}".Trim();
                    string nombreCompletoVisitado = $"{TxtNombreE.Text} {TxtAPE.Text} {TxtAME.Text}".Trim();

                    RegistrarAuditoria(
                        "Corrección de Datos de Visita",
                        "Visitas",
                        idVisitaEnCorreccion.Value,
                        $"Se corrigieron los datos de la visita ID {idVisitaEnCorreccion.Value}. Visitante actualizado: {nombreCompletoVisitante}, Persona visitada actualizada: {nombreCompletoVisitado}",
                        "Datos anteriores modificados",
                        $"Visitante: {nombreCompletoVisitante}, Compañía: {CbxCompania.Text}, Persona Visitada: {nombreCompletoVisitado}, Departamento: {CbxDepartamento.Text}"
                    );

                    MessageBox.Show(
                        "✅ CORRECCIÓN GUARDADA EXITOSAMENTE\n\n" +
                        string.Format("Los datos de la visita ID {0} han sido actualizados.", idVisitaEnCorreccion.Value),
                        "Corrección Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    SalirModoCorreccion();

                    CargarVisitasActivas();
                    CargarVisitantesFrecuentes();
                    CargarPersonasVisitadasFrecuentes();
                    ActualizarContadorVisitas();
                }
                else
                {
                    MessageBox.Show(
                        "⚠️ No se pudo guardar la corrección.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Error al guardar corrección:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SalirModoCorreccion()
        {
            modoCorreccion = false;
            idVisitaEnCorreccion = null;
            idVisitaSeleccionada = null;

            LimpiarCampos();
            BloquearCampos();

            BtnIniciarVisita.Enabled = true;
            BtnGuardarVisita.Enabled = false;
            BtnGuardarVisita.Text = "💾 GUARDAR VISITA";
            BtnGuardarVisita.BackColor = Color.FromArgb(0, 123, 255);
            BtnLimpiar.Enabled = false;
            BtnCancelarVisita.Enabled = false;
            BtnCancelarVisita.Text = "❌ CANCELAR VISITA";
            BtnCorregirDatos.Enabled = false;
            BtnTerminarVisita.Enabled = true;

            CbxVisitantesFrecuentes.Enabled = true;
            CbxPersonasVisitadasFrecuentes.Enabled = true;
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODOS AUXILIARES
        // ═════════════════════════════════════════════════════════

        private DataTable BuscarVisitanteFrecuentePorNombre(string nombre, string primerApellido, string segundoApellido)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@PrimerApellido", primerApellido),
                    new SqlParameter("@SegundoApellido", string.IsNullOrWhiteSpace(segundoApellido) ? (object)DBNull.Value : segundoApellido)
                };

                return ConexionDB.EjecutarConsulta("sp_BuscarVisitanteFrecuentePorNombre", parametros);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar visitante frecuente:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null;
            }
        }

        private DataTable BuscarPersonaVisitadaFrecuentePorNombre(string nombre, string primerApellido, string segundoApellido)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@PrimerApellido", primerApellido),
                    new SqlParameter("@SegundoApellido", string.IsNullOrWhiteSpace(segundoApellido) ? (object)DBNull.Value : segundoApellido)
                };

                return ConexionDB.EjecutarConsulta("sp_BuscarPersonaVisitadaFrecuentePorNombre", parametros);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar persona visitada frecuente:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null;
            }
        }

        private void ActualizarCompaniaVisitanteFrecuente(int idVisitanteFrecuente, int idCompania)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdVisitanteFrecuente", idVisitanteFrecuente),
                    new SqlParameter("@IdCompania", idCompania)
                };

                ConexionDB.EjecutarComando("sp_ActualizarVisitanteFrecuente", parametros);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar visitante frecuente:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ActualizarDepartamentoPersonaVisitadaFrecuente(int idPersonaVisitada, int idDepartamento)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdPersonaVisitadaFrecuente", idPersonaVisitada),
                    new SqlParameter("@IdDepartamento", idDepartamento)
                };

                ConexionDB.EjecutarComando("sp_ActualizarPersonaVisitadaFrecuente", parametros);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar persona visitada frecuente:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool VisitanteYaTieneVisitaActiva(string nombre, string primerApellido, string segundoApellido)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@PrimerApellido", primerApellido),
                    new SqlParameter("@SegundoApellido", string.IsNullOrWhiteSpace(segundoApellido) ? (object)DBNull.Value : segundoApellido)
                };

                object resultado = ConexionDB.EjecutarEscalar("sp_VerificarVisitanteActivo", parametros);

                if (resultado != null)
                {
                    int visitasActivas = Convert.ToInt32(resultado);
                    return visitasActivas > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al verificar visitas activas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        private int ObtenerOCrearCompania(string nombreCompania)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[] { new SqlParameter("@NombreCompania", nombreCompania.Trim()) };
                object resultado = ConexionDB.EjecutarEscalar("sp_ObtenerOCrearCompania", parametros);
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener/crear compañía:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        private int ObtenerOCrearDepartamento(string nombreDepartamento)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[] { new SqlParameter("@NombreDepartamento", nombreDepartamento.Trim()) };
                object resultado = ConexionDB.EjecutarEscalar("sp_ObtenerOCrearDepartamento", parametros);
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener/crear departamento:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        private int? RegistrarVisitanteFrecuente(string nombre, string primerApellido, string segundoApellido, int idCompania)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@PrimerApellido", primerApellido),
                    new SqlParameter("@SegundoApellido", string.IsNullOrWhiteSpace(segundoApellido) ? (object)DBNull.Value : segundoApellido),
                    new SqlParameter("@IdCompania", idCompania)
                };
                object resultado = ConexionDB.EjecutarEscalar("sp_RegistrarVisitanteFrecuente", parametros);
                return resultado != null ? Convert.ToInt32(resultado) : (int?)null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar visitante frecuente:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private int? RegistrarPersonaVisitadaFrecuente(string nombre, string primerApellido, string segundoApellido, int idDepartamento)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@PrimerApellido", primerApellido),
                    new SqlParameter("@SegundoApellido", string.IsNullOrWhiteSpace(segundoApellido) ? (object)DBNull.Value : segundoApellido),
                    new SqlParameter("@IdDepartamento", idDepartamento)
                };
                object resultado = ConexionDB.EjecutarEscalar("sp_RegistrarPersonaVisitadaFrecuente", parametros);
                return resultado != null ? Convert.ToInt32(resultado) : (int?)null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar persona visitada frecuente:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODO: FORMATEAR DURACIÓN DE VISITA
        // ═════════════════════════════════════════════════════════
        private string FormatearDuracionVisita(TimeSpan duracion)
        {
            if (duracion.TotalMinutes < 1)
                return "menos de 1 minuto";
            if (duracion.TotalHours < 1)
                return $"{(int)duracion.TotalMinutes} minutos";
            if (duracion.TotalDays < 1)
                return $"{duracion.Hours} horas y {duracion.Minutes} minutos";
            return $"{duracion.Days} días, {duracion.Hours} horas";
        }

        // ═════════════════════════════════════════════════════════
        // MÉTODO: REGISTRAR AUDITORÍA
        // ═════════════════════════════════════════════════════════
        private void RegistrarAuditoria(string tipoAccion, string tabla, int idRegistro,
            string descripcion, string datosAnteriores = null, string datosNuevos = null)
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
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", tabla);
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", idRegistro);

                cmd.Parameters.AddWithValue("@DatosAnteriores",
                    string.IsNullOrEmpty(datosAnteriores) ? (object)DBNull.Value : datosAnteriores);
                cmd.Parameters.AddWithValue("@DatosNuevos",
                    string.IsNullOrEmpty(datosNuevos) ? (object)DBNull.Value : datosNuevos);

                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar auditoría: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra la finalización automática de visitas antiguas
        /// </summary>
        private void RegistrarFinalizacionAutomatica(int cantidadVisitas)
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
                cmd.Parameters.AddWithValue("@TipoAccion", "Finalización Automática de Visitas Antiguas");
                cmd.Parameters.AddWithValue("@TablaAfectada", "Visitas");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", $"{cantidadVisitas} visita(s) pendiente(s) de días anteriores");
                cmd.Parameters.AddWithValue("@DatosNuevos", $"{cantidadVisitas} visita(s) finalizadas automáticamente");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"Se finalizaron automáticamente {cantidadVisitas} visita(s) pendiente(s) de días anteriores por el usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario})");
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar finalización automática: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void CkbEsEmpleado_CheckedChanged(object sender, EventArgs e) { }

        public class ComboBoxItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
            public string Nombre { get; set; }
            public string PrimerApellido { get; set; }
            public string SegundoApellido { get; set; }
            public string Extra { get; set; }
            public override string ToString() { return Text; }
        }
    }
}