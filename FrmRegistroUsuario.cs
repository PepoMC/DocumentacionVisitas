using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmRegistroUsuario : BaseForm
    {
        private int nivelFortaleza = 0;

        public FrmRegistroUsuario()
        {
            InitializeComponent();
        }

        // =============================================
        // EVENTO LOAD DEL FORMULARIO
        // =============================================
        private void FrmRegistroUsuario_Load(object sender, EventArgs e)
        {
            PanelDatosUsuario.Enabled = true;
            BtnValidarCodigo.Visible = false;
            LblInfoCodigo.Visible = false;
            TxtCodigoActivacion.Focus();
            ActualizarIndicadorFortaleza(0);
        }

        // =============================================
        // BOTÓN: REGISTRAR USUARIO
        // =============================================
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // PASO 1: VALIDAR QUE HAYA CÓDIGO
                string codigo = TxtCodigoActivacion.Text.Trim();
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    MessageBox.Show("⚠️ Por favor ingresa el código de activación.",
                        "Código requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCodigoActivacion.Focus();
                    return;
                }

                // PASO 2: VALIDAR EL CÓDIGO EN LA BASE DE DATOS
                if (!ValidarCodigoActivacion(codigo))
                {
                    MessageBox.Show(
                        "❌ CÓDIGO INVÁLIDO O YA USADO\n\n" +
                        "El código ingresado no es válido. Verifica con tu administrador.",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // ❌ REGISTRAR INTENTO DE REGISTRO CON CÓDIGO INVÁLIDO
                    RegistrarIntentoRegistro(
                        "Intento de Registro - Código Inválido",
                        $"Se intentó registrar un usuario con el código de activación inválido '{codigo}'"
                    );

                    TxtCodigoActivacion.SelectAll();
                    TxtCodigoActivacion.Focus();
                    return;
                }

                // PASO 3: VALIDAR EL RESTO DE LOS CAMPOS
                if (!ValidarCampos())
                    return;

                // PASO 4: CONFIRMAR Y REGISTRAR
                DialogResult resultado = MessageBox.Show(
                    $"¿Confirmas que deseas crear la cuenta con estos datos?\n\n" +
                    $"Usuario: {TxtNombreUsuario.Text}\n" +
                    $"Nombre completo: {TxtNombreCompleto.Text}\n" +
                    $"Rol: Usuario",
                    "Confirmar registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    RegistrarUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ ERROR INESPERADO\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================
        // MÉTODO: VALIDAR CÓDIGO DE ACTIVACIÓN
        // =============================================
        private bool ValidarCodigoActivacion(string codigo)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return false;

                SqlCommand cmd = new SqlCommand("sp_ValidarCodigoActivacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar código: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: VALIDAR CAMPOS DEL FORMULARIO
        // =============================================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(TxtNombreUsuario.Text))
            {
                MessageBox.Show("⚠️ Ingresa tu nombre de usuario", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNombreUsuario.Focus();
                return false;
            }

            if (TxtNombreUsuario.Text.Trim().Length < 4)
            {
                MessageBox.Show("⚠️ El nombre de usuario debe tener al menos 4 caracteres", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNombreUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtNombreCompleto.Text))
            {
                MessageBox.Show("⚠️ Ingresa tu nombre completo", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNombreCompleto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtContrasena.Text))
            {
                MessageBox.Show("⚠️ Ingresa una contraseña", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtContrasena.Focus();
                return false;
            }

            string mensajeError;
            if (!SeguridadHelper.ValidarFortalezaContrasena(TxtContrasena.Text, out mensajeError))
            {
                MessageBox.Show("⚠️ CONTRASEÑA NO CUMPLE REQUISITOS\n\n" + mensajeError, "Contraseña débil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtContrasena.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtConfirmarContrasena.Text))
            {
                MessageBox.Show("⚠️ Confirma tu contraseña", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtConfirmarContrasena.Focus();
                return false;
            }

            if (TxtContrasena.Text != TxtConfirmarContrasena.Text)
            {
                MessageBox.Show("❌ LAS CONTRASEÑAS NO COINCIDEN", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtConfirmarContrasena.Clear();
                TxtConfirmarContrasena.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtPreguntaSeguridad.Text))
            {
                MessageBox.Show("⚠️ Ingresa una pregunta de seguridad", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtPreguntaSeguridad.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtRespuestaSeguridad.Text))
            {
                MessageBox.Show("⚠️ Ingresa la respuesta de seguridad", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtRespuestaSeguridad.Focus();
                return false;
            }

            return true;
        }

        // =============================================
        // MÉTODO: REGISTRAR USUARIO
        // =============================================
        private void RegistrarUsuario()
        {
            SqlConnection conexion = null;
            try
            {
                BtnRegistrar.Enabled = false;
                BtnRegistrar.Text = "Registrando...";
                this.Cursor = Cursors.WaitCursor;

                string nombreUsuario = SeguridadHelper.SanitizarInput(TxtNombreUsuario.Text.Trim());
                string nombreCompleto = TxtNombreCompleto.Text.Trim();
                string contrasena = TxtContrasena.Text;
                string preguntaSeguridad = TxtPreguntaSeguridad.Text.Trim();
                string respuestaSeguridad = TxtRespuestaSeguridad.Text.Trim();
                string codigoActivacion = TxtCodigoActivacion.Text.Trim();

                string hashContrasena = SeguridadHelper.HashearContrasena(contrasena);

                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null)
                {
                    MessageBox.Show("Error al conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@HashContrasena", hashContrasena);
                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Rol", "Usuario");
                cmd.Parameters.AddWithValue("@PreguntaSeguridad", preguntaSeguridad);
                cmd.Parameters.AddWithValue("@RespuestaSeguridad", respuestaSeguridad);

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
                    // ✅ REGISTRO EXITOSO
                    MarcarCodigoUsado(codigoActivacion, nombreUsuario);

                    // ✅ REGISTRAR CREACIÓN DE USUARIO EN AUDITORÍA
                    RegistrarCreacionUsuario(
                        nombreUsuario,
                        nombreCompleto,
                        codigoActivacion,
                        "Creación de Usuario",
                        $"Se creó el usuario {nombreCompleto} ({nombreUsuario}) con rol Usuario mediante código de activación"
                    );

                    MessageBox.Show(
                        "✅ REGISTRO EXITOSO\n\n" + mensaje + "\n\nAhora puedes iniciar sesión.",
                        "Cuenta creada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ ERROR AL REGISTRAR\n\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ ERROR INESPERADO\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BtnRegistrar.Enabled = true;
                BtnRegistrar.Text = "✅ Registrar";
                this.Cursor = Cursors.Default;
                if (conexion != null && conexion.State == ConnectionState.Open) conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: MARCAR CÓDIGO COMO USADO
        // =============================================
        private void MarcarCodigoUsado(string codigo, string usuadoPor)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_MarcarCodigoUsado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@UsadoPor", usuadoPor);
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open) conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR CREACIÓN DE USUARIO
        // =============================================
        private void RegistrarCreacionUsuario(string nombreUsuario, string nombreCompleto,
            string codigoUsado, string tipoAccion, string descripcion)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreUsuario", "Sistema");
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                cmd.Parameters.AddWithValue("@DatosNuevos",
                    $"Usuario: {nombreUsuario}, Nombre: {nombreCompleto}, Rol: Usuario, Código: {codigoUsado}");
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", SeguridadHelper.ObtenerDireccionIP());
                cmd.Parameters.AddWithValue("@NombreMaquina", SeguridadHelper.ObtenerNombreMaquina());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar creación de usuario: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR INTENTO DE REGISTRO
        // =============================================
        private void RegistrarIntentoRegistro(string tipoAccion, string descripcion)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreUsuario", "Anónimo");
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", "CodigosActivacion");
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
                System.Diagnostics.Debug.WriteLine("Error al registrar intento: " + ex.Message);
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
        private void BtnValidarCodigo_Click(object sender, EventArgs e)
        {
            // Vacío - La validación se hace en BtnRegistrar_Click
        }

        private void TxtContrasena_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtContrasena.Text))
            {
                nivelFortaleza = 0;
                ActualizarIndicadorFortaleza(0);
                return;
            }
            nivelFortaleza = SeguridadHelper.ObtenerNivelFortaleza(TxtContrasena.Text);
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
            TxtContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
            TxtConfirmarContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar el registro?",
                "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}