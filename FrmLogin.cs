using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmLogin : BaseForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            TxtUsuario.Focus();
        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Text.Trim();
            string contrasena = TxtContrasena.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingresa usuario y contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    if (conexion == null) return;

                    string query = "SELECT IdUsuario, NombreUsuario, HashContrasena, Rol, Activo, NombreCompleto FROM Usuarios WHERE NombreUsuario = @Usuario";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Usuario existe en la base de datos
                            int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            string nombreUsuarioDB = reader["NombreUsuario"].ToString();
                            string nombreCompleto = reader["NombreCompleto"].ToString();
                            string rol = reader["Rol"].ToString();
                            bool activo = Convert.ToBoolean(reader["Activo"]);
                            string hashGuardado = reader["HashContrasena"].ToString();

                            // Verificar si el usuario está activo
                            if (!activo)
                            {
                                reader.Close();

                                // ❌ REGISTRAR INTENTO DE LOGIN CON USUARIO DESACTIVADO
                                RegistrarIntentoLogin(
                                    idUsuario,
                                    nombreUsuarioDB,
                                    nombreCompleto,
                                    "Intento de Login - Usuario Desactivado",
                                    $"El usuario {nombreCompleto} ({nombreUsuarioDB}) intentó iniciar sesión pero su cuenta está desactivada"
                                );

                                MessageBox.Show("Usuario desactivado.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Verificar contraseña
                            if (SeguridadHelper.VerificarContrasena(contrasena, hashGuardado))
                            {
                                reader.Close();

                                // ✅ LOGIN EXITOSO
                                RegistrarSesionActiva(idUsuario, conexion);
                                SesionActual.IniciarSesion(idUsuario, nombreUsuarioDB, nombreCompleto, rol);

                                this.Hide();

                                FrmMenu menu = new FrmMenu();

                                menu.FormClosed += (s, args) =>
                                {
                                    if (!SesionActual.SesionActiva)
                                    {
                                        this.Show();
                                        this.TxtContrasena.Clear();
                                        this.TxtUsuario.Focus();
                                    }
                                    else
                                    {
                                        this.Close();
                                    }
                                };

                                menu.Show();
                            }
                            else
                            {
                                reader.Close();

                                // ❌ REGISTRAR INTENTO DE LOGIN CON CONTRASEÑA INCORRECTA
                                RegistrarIntentoLogin(
                                    idUsuario,
                                    nombreUsuarioDB,
                                    nombreCompleto,
                                    "Intento de Login Fallido - Contraseña Incorrecta",
                                    $"Se intentó iniciar sesión con el usuario {nombreCompleto} ({nombreUsuarioDB}) pero la contraseña ingresada es incorrecta"
                                );

                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // ❌ Usuario no existe
                            reader.Close();

                            // REGISTRAR INTENTO DE LOGIN CON USUARIO INEXISTENTE
                            RegistrarIntentoLoginUsuarioInexistente(
                                usuario,
                                "Intento de Login Fallido - Usuario Inexistente",
                                $"Se intentó iniciar sesión con el usuario '{usuario}' que no existe en el sistema"
                            );

                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR SESIÓN ACTIVA
        // =============================================
        private void RegistrarSesionActiva(int idUsuario, SqlConnection conexion)
        {
            try
            {
                string token = Guid.NewGuid().ToString();
                string ip = SeguridadHelper.ObtenerDireccionIP();
                string nombreMaquina = SeguridadHelper.ObtenerNombreMaquina();

                string query = @"
                    INSERT INTO SesionesActivas (IdUsuario, Token, FechaInicio, UltimaActividad, DireccionIP, NombreMaquina)
                    VALUES (@IdUsuario, @Token, GETDATE(), GETDATE(), @IP, @NombreMaquina)";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@IP", ip);
                cmd.Parameters.AddWithValue("@NombreMaquina", nombreMaquina);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR INTENTO DE LOGIN
        // =============================================
        private void RegistrarIntentoLogin(int idUsuario, string nombreUsuario, string nombreCompleto,
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
                System.Diagnostics.Debug.WriteLine("Error al registrar intento de login: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR INTENTO CON USUARIO INEXISTENTE
        // =============================================
        private void RegistrarIntentoLoginUsuarioInexistente(string nombreUsuarioIntentado,
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
                System.Diagnostics.Debug.WriteLine("Error al registrar intento de login: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void LnkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FrmRegistroUsuario frmRegistro = new FrmRegistroUsuario();
            DialogResult resultado = frmRegistro.ShowDialog();
            this.Show();

            if (resultado == DialogResult.OK)
            {
                TxtUsuario.Focus();
            }
        }

        private void LnkOlvidasteContrasena_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FrmRecuperarContrasena frmRecuperar = new FrmRecuperarContrasena();
            DialogResult resultado = frmRecuperar.ShowDialog();
            this.Show();

            if (resultado == DialogResult.OK)
            {
                TxtContrasena.Clear();
                TxtUsuario.Focus();
            }
        }

        private void ChkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            TxtContrasena.UseSystemPasswordChar = !ChkMostrarContrasena.Checked;
        }

        private void TxtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnIniciarSesion.PerformClick();
            }
        }
    }
}