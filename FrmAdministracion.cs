using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmAdministracion : BaseForm
    {
        private string nombreUsuario;
        private int usuarioId;

        public FrmAdministracion(string usuario, int idUsuario)
        {
            InitializeComponent();
            this.nombreUsuario = usuario;
            this.usuarioId = idUsuario;
        }

        private void FrmAdministracion_Load(object sender, EventArgs e)
        {
            // ✅ REGISTRAR ACCESO AL PANEL DE ADMINISTRACIÓN
            RegistrarAccesoAdministracion();

            LblUsuarioActual.Text = $"Administrador: {nombreUsuario}";
            CargarUsuarios();
            CargarCodigos();
            CargarEstadisticas();
        }

        // =============================================
        // GESTIÓN DE USUARIOS
        // =============================================

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return;

                    string query = @"
                        SELECT 
                            IdUsuario,
                            NombreUsuario,
                            NombreCompleto,
                            Rol,
                            CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado,
                            FechaCreacion
                        FROM Usuarios
                        ORDER BY FechaCreacion DESC";

                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    DgvUsuarios.DataSource = tabla;

                    DgvUsuarios.Columns["IdUsuario"].HeaderText = "ID";
                    DgvUsuarios.Columns["IdUsuario"].Width = 60;
                    DgvUsuarios.Columns["NombreUsuario"].HeaderText = "Nombre de Usuario";
                    DgvUsuarios.Columns["NombreUsuario"].Width = 150;
                    DgvUsuarios.Columns["NombreCompleto"].HeaderText = "Nombre Completo";
                    DgvUsuarios.Columns["NombreCompleto"].Width = 200;
                    DgvUsuarios.Columns["Rol"].HeaderText = "Rol";
                    DgvUsuarios.Columns["Rol"].Width = 120;
                    DgvUsuarios.Columns["Estado"].HeaderText = "Estado";
                    DgvUsuarios.Columns["Estado"].Width = 100;
                    DgvUsuarios.Columns["FechaCreacion"].HeaderText = "Fecha de Creación";
                    DgvUsuarios.Columns["FechaCreacion"].Width = 150;

                    LblTotalUsuarios.Text = $"Total de usuarios: {tabla.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescarUsuarios_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            MessageBox.Show("Lista de usuarios actualizada.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnActivarUsuario_Click(object sender, EventArgs e)
        {
            if (DgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int usuarioIdSeleccionado = Convert.ToInt32(DgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);
            string nombreSeleccionado = DgvUsuarios.SelectedRows[0].Cells["NombreUsuario"].Value.ToString();
            string nombreCompletoSeleccionado = DgvUsuarios.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();
            string estadoActual = DgvUsuarios.SelectedRows[0].Cells["Estado"].Value.ToString();

            if (estadoActual == "Activo")
            {
                MessageBox.Show($"El usuario '{nombreSeleccionado}' ya está activo.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de activar al usuario '{nombreSeleccionado}'?\n\n" +
                $"Nombre completo: {nombreCompletoSeleccionado}\n\n" +
                "Esto le permitirá iniciar sesión en el sistema.",
                "Confirmar Activación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                    {
                        if (conexion == null) return;

                        string query = "UPDATE Usuarios SET Activo = 1 WHERE IdUsuario = @IdUsuario";
                        SqlCommand cmd = new SqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@IdUsuario", usuarioIdSeleccionado);

                        cmd.ExecuteNonQuery();
                    }

                    // ✅ REGISTRAR ACTIVACIÓN DE USUARIO
                    RegistrarActivacionUsuario(usuarioIdSeleccionado, nombreSeleccionado, nombreCompletoSeleccionado);

                    MessageBox.Show(
                        $"✅ Usuario '{nombreSeleccionado}' activado correctamente.\n\n" +
                        "Ahora puede iniciar sesión en el sistema.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
                    CargarEstadisticas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al activar usuario: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            if (DgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int usuarioIdSeleccionado = Convert.ToInt32(DgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);
            string nombreSeleccionado = DgvUsuarios.SelectedRows[0].Cells["NombreUsuario"].Value.ToString();
            string nombreCompletoSeleccionado = DgvUsuarios.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();
            string estadoActual = DgvUsuarios.SelectedRows[0].Cells["Estado"].Value.ToString();

            if (usuarioIdSeleccionado == this.usuarioId)
            {
                MessageBox.Show("No puede desactivar su propia cuenta.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estadoActual == "Inactivo")
            {
                MessageBox.Show($"El usuario '{nombreSeleccionado}' ya está inactivo.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de desactivar al usuario '{nombreSeleccionado}'?\n\n" +
                $"Nombre completo: {nombreCompletoSeleccionado}\n\n" +
                "Esta acción impedirá que el usuario inicie sesión en el sistema.\n" +
                "Puede reactivar el usuario en cualquier momento.",
                "Confirmar Desactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                    {
                        if (conexion == null) return;

                        string query = "UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @IdUsuario";
                        SqlCommand cmd = new SqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@IdUsuario", usuarioIdSeleccionado);

                        cmd.ExecuteNonQuery();
                    }

                    // ✅ REGISTRAR DESACTIVACIÓN DE USUARIO
                    RegistrarDesactivacionUsuario(usuarioIdSeleccionado, nombreSeleccionado, nombreCompletoSeleccionado);

                    MessageBox.Show(
                        $"✅ Usuario '{nombreSeleccionado}' desactivado correctamente.\n\n" +
                        "El usuario ya no podrá iniciar sesión.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
                    CargarEstadisticas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al desactivar usuario: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =============================================
        // CÓDIGOS DE ACTIVACIÓN
        // =============================================

        private void CargarCodigos()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return;

                    string query = @"
                        SELECT 
                            IdCodigo,
                            Codigo,
                            FechaGeneracion,
                            FechaExpiracion,
                            CASE 
                                WHEN Usado = 1 THEN 'Usado' 
                                WHEN FechaExpiracion < GETDATE() THEN 'Vencido'
                                ELSE 'Disponible' 
                            END as Estado,
                            CASE WHEN Usado = 1 THEN u.NombreUsuario ELSE 'N/A' END as UsadoPor
                        FROM CodigosActivacion ca
                        LEFT JOIN Usuarios u ON ca.UsadoPor = u.IdUsuario
                        ORDER BY FechaGeneracion DESC";

                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    DgvCodigos.DataSource = tabla;

                    DgvCodigos.Columns["IdCodigo"].HeaderText = "ID";
                    DgvCodigos.Columns["IdCodigo"].Width = 60;
                    DgvCodigos.Columns["Codigo"].HeaderText = "Código";
                    DgvCodigos.Columns["FechaGeneracion"].HeaderText = "Fecha Generación";
                    DgvCodigos.Columns["FechaExpiracion"].HeaderText = "Fecha Expiración";
                    DgvCodigos.Columns["Estado"].HeaderText = "Estado";
                    DgvCodigos.Columns["UsadoPor"].HeaderText = "Usado Por";

                    foreach (DataGridViewRow row in DgvCodigos.Rows)
                    {
                        string estado = row.Cells["Estado"].Value.ToString();
                        if (estado == "Vencido")
                        {
                            row.DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else if (estado == "Usado")
                        {
                            row.DefaultCellStyle.ForeColor = Color.Gray;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = Color.Green;
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                    }

                    int totalCodigos = tabla.Rows.Count;
                    int disponibles = 0;

                    foreach (DataRow fila in tabla.Rows)
                    {
                        if (fila["Estado"].ToString() == "Disponible")
                            disponibles++;
                    }

                    LblTotalCodigos.Text = $"Total códigos: {totalCodigos} | Disponibles: {disponibles}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar códigos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerarCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                int diasValidez = (int)NumDiasValidez.Value;
                string codigoGenerado = GenerarCodigoUnico();

                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return;

                    string query = @"
                        INSERT INTO CodigosActivacion (Codigo, GeneradoPor, FechaGeneracion, FechaExpiracion, Usado)
                        VALUES (@Codigo, @GeneradoPor, GETDATE(), DATEADD(DAY, @DiasValidez, GETDATE()), 0)";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Codigo", codigoGenerado);
                    cmd.Parameters.AddWithValue("@GeneradoPor", this.usuarioId);
                    cmd.Parameters.AddWithValue("@DiasValidez", diasValidez);

                    cmd.ExecuteNonQuery();
                }

                TxtCodigoGenerado.Text = codigoGenerado;

                // ✅ REGISTRAR GENERACIÓN DE CÓDIGO
                RegistrarGeneracionCodigo(codigoGenerado, diasValidez);

                MessageBox.Show(
                    $"✅ Código generado exitosamente.\n\n" +
                    $"Código: {codigoGenerado}\n" +
                    $"Válido por {diasValidez} días.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarCodigos();
                CargarEstadisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar código: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarCodigoUnico()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string codigo;

            do
            {
                char[] codigoArray = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    codigoArray[i] = caracteres[random.Next(caracteres.Length)];
                }
                codigo = new string(codigoArray);
            }
            while (ExisteCodigoEnBD(codigo));

            return codigo;
        }

        private bool ExisteCodigoEnBD(string codigo)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return false;

                    string query = "SELECT COUNT(*) FROM CodigosActivacion WHERE Codigo = @Codigo";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void BtnCopiarCodigo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCodigoGenerado.Text))
            {
                Clipboard.SetText(TxtCodigoGenerado.Text);
                MessageBox.Show("Código copiado al portapapeles.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay código para copiar. Genere un código primero.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefrescarCodigos_Click(object sender, EventArgs e)
        {
            CargarCodigos();
            MessageBox.Show("Lista de códigos actualizada.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =============================================
        // ESTADÍSTICAS
        // =============================================

        private void CargarEstadisticas()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return;

                    string queryTotalUsuarios = "SELECT COUNT(*) FROM Usuarios";
                    SqlCommand cmdTotal = new SqlCommand(queryTotalUsuarios, conexion);
                    int totalUsuarios = (int)cmdTotal.ExecuteScalar();
                    LblEstTotalUsuarios.Text = $"📊 Total de Cuentas Registradas: {totalUsuarios}";

                    string queryActivos = "SELECT COUNT(*) FROM Usuarios WHERE Activo = 1";
                    SqlCommand cmdActivos = new SqlCommand(queryActivos, conexion);
                    int usuariosActivos = (int)cmdActivos.ExecuteScalar();
                    LblEstUsuariosActivos.Text = $"✅ Cuentas Habilitadas: {usuariosActivos}";

                    string queryAdmins = "SELECT COUNT(*) FROM Usuarios WHERE Rol = 'Administrador' AND Activo = 1";
                    SqlCommand cmdAdmins = new SqlCommand(queryAdmins, conexion);
                    int administradores = (int)cmdAdmins.ExecuteScalar();
                    LblEstAdministradores.Text = $"👑 Administradores: {administradores}";

                    string queryCodigos = "SELECT COUNT(*) FROM CodigosActivacion WHERE Usado = 0 AND FechaExpiracion > GETDATE()";
                    SqlCommand cmdCodigos = new SqlCommand(queryCodigos, conexion);
                    int codigosDisponibles = (int)cmdCodigos.ExecuteScalar();
                    LblEstCodigosDisponibles.Text = $"🎫 Códigos Válidos Disponibles: {codigosDisponibles}";

                    string queryUsuariosHoy = @"
                        SELECT COUNT(DISTINCT sa.IdUsuario) 
                        FROM SesionesActivas sa
                        INNER JOIN Usuarios u ON sa.IdUsuario = u.IdUsuario
                        WHERE CAST(sa.FechaInicio AS DATE) = CAST(GETDATE() AS DATE)
                        AND u.Rol = 'Usuario'";

                    SqlCommand cmdUsuariosHoy = new SqlCommand(queryUsuariosHoy, conexion);
                    object resultUsuariosHoy = cmdUsuariosHoy.ExecuteScalar();
                    int usuariosHoy = (resultUsuariosHoy == DBNull.Value) ? 0 : Convert.ToInt32(resultUsuariosHoy);

                    if (usuariosHoy == 0)
                    {
                        LblEstUsuariosHoy.Text = $"👤 Usuarios que entraron hoy: 0";
                        LblEstUsuariosHoy.ForeColor = Color.Gray;
                    }
                    else
                    {
                        LblEstUsuariosHoy.Text = $"👤 Usuarios que entraron hoy: {usuariosHoy}";
                        LblEstUsuariosHoy.ForeColor = Color.FromArgb(23, 162, 184);
                    }

                    string queryAdminsHoy = @"
                        SELECT COUNT(DISTINCT sa.IdUsuario) 
                        FROM SesionesActivas sa
                        INNER JOIN Usuarios u ON sa.IdUsuario = u.IdUsuario
                        WHERE CAST(sa.FechaInicio AS DATE) = CAST(GETDATE() AS DATE)
                        AND u.Rol = 'Administrador'";

                    SqlCommand cmdAdminsHoy = new SqlCommand(queryAdminsHoy, conexion);
                    object resultAdminsHoy = cmdAdminsHoy.ExecuteScalar();
                    int adminsHoy = (resultAdminsHoy == DBNull.Value) ? 0 : Convert.ToInt32(resultAdminsHoy);

                    if (adminsHoy == 0)
                    {
                        LblEstAdministradoresHoy.Text = $"👑 Administradores que entraron hoy: 0";
                        LblEstAdministradoresHoy.ForeColor = Color.Gray;
                    }
                    else
                    {
                        LblEstAdministradoresHoy.Text = $"👑 Administradores que entraron hoy: {adminsHoy}";
                        LblEstAdministradoresHoy.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescarEstadisticas_Click(object sender, EventArgs e)
        {
            CargarEstadisticas();
            MessageBox.Show("Estadísticas actualizadas.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =============================================
        // MÉTODOS DE REGISTRO EN AUDITORÍA
        // =============================================

        /// <summary>
        /// Registra el acceso al panel de administración
        /// </summary>
        private void RegistrarAccesoAdministracion()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", this.usuarioId);
                cmd.Parameters.AddWithValue("@NombreUsuario", this.nombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Acceso a Panel de Administración");
                cmd.Parameters.AddWithValue("@TablaAfectada", "Sistema");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos", DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {SesionActual.NombreCompleto} ({this.nombreUsuario}) accedió al panel de administración del sistema");
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar acceso: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra la activación de un usuario
        /// </summary>
        private void RegistrarActivacionUsuario(int usuarioAfectado, string nombreUsuarioAfectado, string nombreCompletoAfectado)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", this.usuarioId);
                cmd.Parameters.AddWithValue("@NombreUsuario", this.nombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Activación de Usuario");
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", usuarioAfectado);
                cmd.Parameters.AddWithValue("@DatosAnteriores", "Estado: Inactivo");
                cmd.Parameters.AddWithValue("@DatosNuevos", "Estado: Activo");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {this.nombreUsuario} activó al usuario {nombreCompletoAfectado} ({nombreUsuarioAfectado}). El usuario ahora puede iniciar sesión en el sistema");
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar activación: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra la desactivación de un usuario
        /// </summary>
        private void RegistrarDesactivacionUsuario(int usuarioAfectado, string nombreUsuarioAfectado, string nombreCompletoAfectado)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", this.usuarioId);
                cmd.Parameters.AddWithValue("@NombreUsuario", this.nombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Desactivación de Usuario");
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", usuarioAfectado);
                cmd.Parameters.AddWithValue("@DatosAnteriores", "Estado: Activo");
                cmd.Parameters.AddWithValue("@DatosNuevos", "Estado: Inactivo");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {this.nombreUsuario} desactivó al usuario {nombreCompletoAfectado} ({nombreUsuarioAfectado}). El usuario ya no puede iniciar sesión en el sistema");
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar desactivación: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /// <summary>
        /// Registra la generación de un código de activación
        /// </summary>
        private void RegistrarGeneracionCodigo(string codigo, int diasValidez)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", this.usuarioId);
                cmd.Parameters.AddWithValue("@NombreUsuario", this.nombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", "Generación de Código de Activación");
                cmd.Parameters.AddWithValue("@TablaAfectada", "CodigosActivacion");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos",
                    $"Código: {codigo}, Validez: {diasValidez} días, Expira: {DateTime.Now.AddDays(diasValidez):yyyy-MM-dd}");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"El administrador {this.nombreUsuario} generó el código de activación '{codigo}' con validez de {diasValidez} días");
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar generación de código: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // CIERRE
        // =============================================

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}