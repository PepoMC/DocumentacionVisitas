using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmRecuperarContrasena : BaseForm
    {
        // Variables para almacenar datos del usuario durante el proceso
        private int idUsuarioRecuperar = 0;
        private string nombreUsuarioRecuperar = "";
        private string nombreCompletoRecuperar = "";
        private string rolUsuarioRecuperar = "";
        private int nivelFortaleza = 0;

        public FrmRecuperarContrasena()
        {
            InitializeComponent();
        }

        // =============================================
        // EVENTO LOAD DEL FORMULARIO
        // =============================================
        private void FrmRecuperarContrasena_Load(object sender, EventArgs e)
        {
            GrpPaso1.Enabled = true;
            GrpPaso2.Enabled = false;
            GrpPaso3.Enabled = false;

            TxtUsuario.Focus();
            ActualizarIndicadorFortaleza(0);
        }

        // =============================================
        // PASO 1: BUSCAR USUARIO
        // =============================================
        private void BtnBuscarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
            {
                MessageBox.Show(
                    "⚠️ Por favor ingresa tu nombre de usuario",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtUsuario.Focus();
                return;
            }

            BuscarUsuario();
        }

        // =============================================
        // MÉTODO: BUSCAR USUARIO Y OBTENER PREGUNTA
        // =============================================
        private void BuscarUsuario()
        {
            SqlConnection conexion = null;
            try
            {
                BtnBuscarUsuario.Enabled = false;
                BtnBuscarUsuario.Text = "Buscando...";
                this.Cursor = Cursors.WaitCursor;

                string nombreUsuario = SeguridadHelper.SanitizarInput(TxtUsuario.Text.Trim());

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null)
                {
                    MessageBox.Show(
                        "Error al conectar a la base de datos",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_ObtenerPreguntaSeguridad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                SqlParameter paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramResultado);

                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramMensaje);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int resultado = Convert.ToInt32(paramResultado.Value);
                string mensaje = paramMensaje.Value.ToString();

                if (resultado == 1 && dt.Rows.Count > 0)
                {
                    // ✅ Usuario encontrado - Guardar datos
                    DataRow row = dt.Rows[0];
                    idUsuarioRecuperar = Convert.ToInt32(row["IdUsuario"]);
                    nombreUsuarioRecuperar = row["NombreUsuario"].ToString();
                    nombreCompletoRecuperar = row["NombreCompleto"].ToString();
                    rolUsuarioRecuperar = row["Rol"].ToString();
                    string preguntaSeguridad = row["PreguntaSeguridad"].ToString();

                    TxtPreguntaMostrada.Text = preguntaSeguridad;

                    GrpPaso1.Enabled = false;
                    GrpPaso2.Enabled = true;
                    TxtRespuesta.Focus();

                    // ✅ REGISTRAR BÚSQUEDA DE USUARIO PARA RECUPERACIÓN
                    RegistrarIntentoRecuperacion(
                        idUsuarioRecuperar,
                        nombreUsuarioRecuperar,
                        nombreCompletoRecuperar,
                        "Intento de Recuperación de Contraseña - Usuario Encontrado",
                        $"El usuario {nombreCompletoRecuperar} ({nombreUsuarioRecuperar}) inició el proceso de recuperación de contraseña"
                    );

                    MessageBox.Show(
                        $"✅ Usuario encontrado: {nombreCompletoRecuperar}\n\n" +
                        "Ahora responde tu pregunta de seguridad.",
                        "Usuario válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // ❌ REGISTRAR INTENTO CON USUARIO NO ENCONTRADO
                    RegistrarIntentoRecuperacionFallido(
                        nombreUsuario,
                        "Intento de Recuperación - Usuario No Encontrado",
                        $"Se intentó recuperar la contraseña del usuario '{nombreUsuario}' que no existe en el sistema"
                    );

                    MessageBox.Show(
                        $"❌ {mensaje}\n\n" +
                        "Verifica el nombre de usuario e intenta nuevamente.",
                        "Usuario no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    TxtUsuario.SelectAll();
                    TxtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ ERROR AL BUSCAR USUARIO\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                BtnBuscarUsuario.Enabled = true;
                BtnBuscarUsuario.Text = "🔍 Buscar";
                this.Cursor = Cursors.Default;

                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // PASO 2: VALIDAR RESPUESTA DE SEGURIDAD
        // =============================================
        private void BtnValidarRespuesta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtRespuesta.Text))
            {
                MessageBox.Show(
                    "⚠️ Por favor ingresa tu respuesta de seguridad",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtRespuesta.Focus();
                return;
            }

            ValidarRespuesta();
        }

        // =============================================
        // MÉTODO: VALIDAR RESPUESTA DE SEGURIDAD
        // =============================================
        private void ValidarRespuesta()
        {
            SqlConnection conexion = null;
            try
            {
                BtnValidarRespuesta.Enabled = false;
                BtnValidarRespuesta.Text = "Validando...";
                this.Cursor = Cursors.WaitCursor;

                string respuesta = TxtRespuesta.Text.Trim();

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null)
                {
                    MessageBox.Show(
                        "Error al conectar a la base de datos",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_ValidarRespuestaSeguridad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@RespuestaSeguridad", respuesta);

                SqlParameter paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramResultado);

                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramMensaje);

                cmd.ExecuteNonQuery();

                int resultado = Convert.ToInt32(paramResultado.Value);
                string mensaje = paramMensaje.Value.ToString();

                if (resultado == 1)
                {
                    // ✅ Respuesta correcta - Activar paso 3
                    GrpPaso2.Enabled = false;
                    GrpPaso3.Enabled = true;
                    TxtNuevaContrasena.Focus();

                    // ✅ REGISTRAR VALIDACIÓN CORRECTA
                    RegistrarIntentoRecuperacion(
                        idUsuarioRecuperar,
                        nombreUsuarioRecuperar,
                        nombreCompletoRecuperar,
                        "Recuperación de Contraseña - Respuesta Correcta",
                        $"El usuario {nombreCompletoRecuperar} ({nombreUsuarioRecuperar}) respondió correctamente la pregunta de seguridad"
                    );

                    MessageBox.Show(
                        "✅ RESPUESTA CORRECTA\n\n" +
                        "Ahora puedes ingresar tu nueva contraseña.",
                        "Verificación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // ❌ REGISTRAR RESPUESTA INCORRECTA
                    RegistrarIntentoRecuperacion(
                        idUsuarioRecuperar,
                        nombreUsuarioRecuperar,
                        nombreCompletoRecuperar,
                        "Recuperación de Contraseña - Respuesta Incorrecta",
                        $"El usuario {nombreCompletoRecuperar} ({nombreUsuarioRecuperar}) respondió incorrectamente la pregunta de seguridad"
                    );

                    MessageBox.Show(
                        $"❌ {mensaje}\n\n" +
                        "La respuesta no coincide con la registrada en tu cuenta.",
                        "Respuesta incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    TxtRespuesta.Clear();
                    TxtRespuesta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ ERROR AL VALIDAR RESPUESTA\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                BtnValidarRespuesta.Enabled = true;
                BtnValidarRespuesta.Text = "✅ Validar";
                this.Cursor = Cursors.Default;

                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // PASO 3: CAMBIAR CONTRASEÑA
        // =============================================
        private void BtnCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposContrasena())
                return;

            DialogResult resultado = MessageBox.Show(
                "¿Confirmas que deseas cambiar tu contraseña?\n\n" +
                "Después del cambio, deberás iniciar sesión con la nueva contraseña.",
                "Confirmar cambio",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                CambiarContrasena();
            }
        }

        // =============================================
        // MÉTODO: VALIDAR CAMPOS DE CONTRASEÑA
        // =============================================
        private bool ValidarCamposContrasena()
        {
            if (string.IsNullOrWhiteSpace(TxtNuevaContrasena.Text))
            {
                MessageBox.Show(
                    "⚠️ Ingresa la nueva contraseña",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtNuevaContrasena.Focus();
                return false;
            }

            string mensajeError;
            if (!SeguridadHelper.ValidarFortalezaContrasena(TxtNuevaContrasena.Text, out mensajeError))
            {
                MessageBox.Show(
                    "⚠️ LA CONTRASEÑA NO CUMPLE REQUISITOS\n\n" + mensajeError,
                    "Contraseña débil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtNuevaContrasena.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtConfirmarContrasena.Text))
            {
                MessageBox.Show(
                    "⚠️ Confirma la nueva contraseña",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtConfirmarContrasena.Focus();
                return false;
            }

            if (TxtNuevaContrasena.Text != TxtConfirmarContrasena.Text)
            {
                MessageBox.Show(
                    "❌ LAS CONTRASEÑAS NO COINCIDEN\n\n" +
                    "La nueva contraseña y su confirmación deben ser iguales.",
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                TxtConfirmarContrasena.Clear();
                TxtConfirmarContrasena.Focus();
                return false;
            }

            return true;
        }

        // =============================================
        // MÉTODO: CAMBIAR CONTRASEÑA
        // =============================================
        private void CambiarContrasena()
        {
            SqlConnection conexion = null;
            try
            {
                BtnCambiarContrasena.Enabled = false;
                BtnCambiarContrasena.Text = "Cambiando...";
                this.Cursor = Cursors.WaitCursor;

                string nuevoHash = SeguridadHelper.HashearContrasena(TxtNuevaContrasena.Text);

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null)
                {
                    MessageBox.Show(
                        "Error al conectar a la base de datos",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_CambiarContrasena", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@NuevoHash", nuevoHash);
                cmd.ExecuteNonQuery();

                // ✅ REGISTRAR RECUPERACIÓN EXITOSA EN AUDITORÍA
                RegistrarRecuperacionExitosa();

                DialogResult iniciarSesion = MessageBox.Show(
                    "✅ CONTRASEÑA CAMBIADA EXITOSAMENTE\n\n" +
                    "Tu contraseña ha sido actualizada correctamente.\n\n" +
                    "¿Deseas iniciar sesión ahora?",
                    "Cambio exitoso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (iniciarSesion == DialogResult.Yes)
                {
                    IniciarSesionYAbrirMenu();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ ERROR AL CAMBIAR CONTRASEÑA\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                BtnCambiarContrasena.Enabled = true;
                BtnCambiarContrasena.Text = "🔒 Cambiar Contraseña";
                this.Cursor = Cursors.Default;

                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR RECUPERACIÓN EXITOSA
        // =============================================
        private void RegistrarRecuperacionExitosa()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@TipoAccion", "Recuperación de Contraseña");
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", idUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@DatosAnteriores", "Contraseña anterior (hash)");
                cmd.Parameters.AddWithValue("@DatosNuevos", "Contraseña nueva (hash)");
                cmd.Parameters.AddWithValue("@Descripcion",
                    $"Usuario {nombreCompletoRecuperar} ({nombreUsuarioRecuperar}) recuperó su contraseña exitosamente mediante pregunta de seguridad");
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR INTENTO DE RECUPERACIÓN
        // =============================================
        private void RegistrarIntentoRecuperacion(int idUsuario, string nombreUsuario, string nombreCompleto,
            string tipoAccion, string descripcion)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", idUsuario);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos", DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar intento recuperación: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR INTENTO FALLIDO
        // =============================================
        private void RegistrarIntentoRecuperacionFallido(string nombreUsuarioIntentado,
            string tipoAccion, string descripcion)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuarioIntentado);
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos", DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar intento fallido: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: INICIAR SESIÓN Y ABRIR MENÚ
        // =============================================
        private void IniciarSesionYAbrirMenu()
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                string token = Guid.NewGuid().ToString();
                string ip = SeguridadHelper.ObtenerDireccionIP();
                string nombreMaquina = SeguridadHelper.ObtenerNombreMaquina();

                string query = @"
                    INSERT INTO SesionesActivas (IdUsuario, Token, FechaInicio, UltimaActividad, DireccionIP, NombreMaquina)
                    VALUES (@IdUsuario, @Token, GETDATE(), GETDATE(), @IP, @NombreMaquina)";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioRecuperar);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@IP", ip);
                cmd.Parameters.AddWithValue("@NombreMaquina", nombreMaquina);
                cmd.ExecuteNonQuery();

                SesionActual.IniciarSesion(
                    idUsuarioRecuperar,
                    nombreUsuarioRecuperar,
                    nombreCompletoRecuperar,
                    rolUsuarioRecuperar);

                this.DialogResult = DialogResult.OK;
                this.Hide();

                FrmMenu menu = new FrmMenu();
                menu.ShowDialog();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al iniciar sesión: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // EVENTOS DE INTERFAZ
        // =============================================
        private void TxtNuevaContrasena_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNuevaContrasena.Text))
            {
                nivelFortaleza = 0;
                ActualizarIndicadorFortaleza(0);
                return;
            }

            nivelFortaleza = SeguridadHelper.ObtenerNivelFortaleza(TxtNuevaContrasena.Text);
            ActualizarIndicadorFortaleza(nivelFortaleza);
        }

        private void ActualizarIndicadorFortaleza(int nivel)
        {
            switch (nivel)
            {
                case 0:
                    ProgressFortaleza.Value = 0;
                    LblFortaleza.Text = "Sin contraseña";
                    LblFortaleza.ForeColor = Color.Gray;
                    break;
                case 1:
                case 2:
                    ProgressFortaleza.Value = 20;
                    LblFortaleza.Text = "⚠️ Muy débil";
                    LblFortaleza.ForeColor = Color.Red;
                    break;
                case 3:
                    ProgressFortaleza.Value = 40;
                    LblFortaleza.Text = "⚠️ Débil";
                    LblFortaleza.ForeColor = Color.OrangeRed;
                    break;
                case 4:
                    ProgressFortaleza.Value = 60;
                    LblFortaleza.Text = "⚡ Aceptable";
                    LblFortaleza.ForeColor = Color.Orange;
                    break;
                case 5:
                    ProgressFortaleza.Value = 80;
                    LblFortaleza.Text = "✅ Buena";
                    LblFortaleza.ForeColor = Color.YellowGreen;
                    break;
                case 6:
                    ProgressFortaleza.Value = 100;
                    LblFortaleza.Text = "✅ Muy fuerte";
                    LblFortaleza.ForeColor = Color.Green;
                    break;
            }
        }

        private void ChkMostrarContrasenas_CheckedChanged(object sender, EventArgs e)
        {
            TxtNuevaContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
            TxtConfirmarContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar la recuperación?\n\n" +
                "Se perderá todo el progreso.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}