using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    public partial class FrmCambiarContrasena : BaseForm
    {
        private bool esObligatorio = false;
        private int nivelFortaleza = 0;

        // Constructor normal (cambio voluntario)
        public FrmCambiarContrasena()
        {
            InitializeComponent();
            esObligatorio = false;
        }

        // Constructor con parámetro (cambio obligatorio)
        public FrmCambiarContrasena(bool obligatorio)
        {
            InitializeComponent();
            esObligatorio = obligatorio;
        }

        // =============================================
        // EVENTO LOAD DEL FORMULARIO
        // =============================================
        private void FrmCambiarContrasena_Load(object sender, EventArgs e)
        {
            if (!SesionActual.SesionActiva)
            {
                MessageBox.Show(
                    "⚠️ No hay una sesión activa.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LblUsuario.Text = $"Usuario: {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario})";

            if (esObligatorio)
            {
                BtnCancelar.Enabled = false;
                LblAdvertencia.Visible = true;
                LblAdvertencia.Text = "⚠️ CAMBIO OBLIGATORIO: Debes cambiar tu contraseña temporal antes de continuar";
                this.ControlBox = false;
            }
            else
            {
                LblAdvertencia.Visible = false;
            }

            TxtContrasenaActual.Focus();
            ActualizarIndicadorFortaleza(0);
        }

        // =============================================
        // BOTÓN: CAMBIAR CONTRASEÑA
        // =============================================
        private void BtnCambiar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            DialogResult resultado = MessageBox.Show(
                "¿Confirmas que deseas cambiar tu contraseña?",
                "Confirmar cambio",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                CambiarContrasena();
            }
        }

        // =============================================
        // MÉTODO: VALIDAR CAMPOS
        // =============================================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(TxtContrasenaActual.Text))
            {
                MessageBox.Show(
                    "⚠️ Ingresa tu contraseña actual",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtContrasenaActual.Focus();
                return false;
            }

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

            if (TxtContrasenaActual.Text == TxtNuevaContrasena.Text)
            {
                MessageBox.Show(
                    "⚠️ La nueva contraseña debe ser diferente a la actual",
                    "Contraseñas iguales",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtNuevaContrasena.Clear();
                TxtNuevaContrasena.Focus();
                return false;
            }

            string mensajeError;
            if (!SeguridadHelper.ValidarFortalezaContrasena(TxtNuevaContrasena.Text, out mensajeError))
            {
                MessageBox.Show(
                    "⚠️ LA NUEVA CONTRASEÑA NO CUMPLE REQUISITOS\n\n" + mensajeError,
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
                BtnCambiar.Enabled = false;
                BtnCambiar.Text = "Cambiando...";
                this.Cursor = Cursors.WaitCursor;

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

                // ============================================
                // PASO 1: VALIDAR CONTRASEÑA ACTUAL
                // ============================================
                SqlCommand cmdValidar = new SqlCommand("sp_ValidarLogin", conexion);
                cmdValidar.CommandType = CommandType.StoredProcedure;
                cmdValidar.Parameters.AddWithValue("@NombreUsuario", SesionActual.NombreUsuario);

                SqlDataAdapter da = new SqlDataAdapter(cmdValidar);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Error al validar usuario",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                string hashActual = dt.Rows[0]["HashContrasena"].ToString();

                if (!SeguridadHelper.VerificarContrasena(TxtContrasenaActual.Text, hashActual))
                {
                    // ❌ REGISTRAR INTENTO FALLIDO - CONTRASEÑA INCORRECTA
                    RegistrarCambioContrasena(
                        "Cambio de Contraseña - Contraseña Actual Incorrecta",
                        $"El usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) intentó cambiar su contraseña pero ingresó incorrectamente la contraseña actual",
                        false
                    );

                    MessageBox.Show(
                        "❌ CONTRASEÑA ACTUAL INCORRECTA\n\n" +
                        "La contraseña actual que ingresaste no es correcta.",
                        "Error de autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    TxtContrasenaActual.Clear();
                    TxtContrasenaActual.Focus();
                    return;
                }

                // ============================================
                // PASO 2: GENERAR NUEVO HASH
                // ============================================
                string nuevoHash = SeguridadHelper.HashearContrasena(TxtNuevaContrasena.Text);

                // ============================================
                // PASO 3: ACTUALIZAR EN BASE DE DATOS
                // ============================================
                SqlCommand cmdCambiar = new SqlCommand("sp_CambiarContrasena", conexion);
                cmdCambiar.CommandType = CommandType.StoredProcedure;
                cmdCambiar.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                cmdCambiar.Parameters.AddWithValue("@NuevoHash", nuevoHash);
                cmdCambiar.ExecuteNonQuery();

                // ============================================
                // PASO 4: REGISTRAR EN AUDITORÍA
                // ============================================
                RegistrarCambioContrasena(
                    esObligatorio ? "Cambio de Contraseña Obligatorio" : "Cambio de Contraseña",
                    esObligatorio
                        ? $"Usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) cambió su contraseña temporal (cambio obligatorio)"
                        : $"Usuario {SesionActual.NombreCompleto} ({SesionActual.NombreUsuario}) cambió su contraseña de forma voluntaria",
                    true
                );

                // ============================================
                // PASO 5: MOSTRAR ÉXITO
                // ============================================
                MessageBox.Show(
                    "✅ CONTRASEÑA CAMBIADA EXITOSAMENTE\n\n" +
                    "Tu contraseña ha sido actualizada correctamente.",
                    "Cambio exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
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
                BtnCambiar.Enabled = true;
                BtnCambiar.Text = "✅ Cambiar Contraseña";
                this.Cursor = Cursors.Default;

                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR CAMBIO EN AUDITORÍA
        // =============================================
        private void RegistrarCambioContrasena(string tipoAccion, string descripcion, bool exitoso)
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
                cmd.Parameters.AddWithValue("@TablaAfectada", "Usuarios");
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", SesionActual.IdUsuario);

                if (exitoso)
                {
                    cmd.Parameters.AddWithValue("@DatosAnteriores", "Contraseña anterior (hash)");
                    cmd.Parameters.AddWithValue("@DatosNuevos", "Contraseña nueva (hash)");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DatosAnteriores", DBNull.Value);
                    cmd.Parameters.AddWithValue("@DatosNuevos", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", SesionActual.DireccionIP);
                cmd.Parameters.AddWithValue("@NombreMaquina", SesionActual.NombreMaquina);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar cambio de contraseña: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        // =============================================
        // EVENTO: CAMBIO EN NUEVA CONTRASEÑA
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

        // =============================================
        // MÉTODO: ACTUALIZAR INDICADOR DE FORTALEZA
        // =============================================
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

        // =============================================
        // CHECKBOX: MOSTRAR CONTRASEÑAS
        // =============================================
        private void ChkMostrarContrasenas_CheckedChanged(object sender, EventArgs e)
        {
            TxtContrasenaActual.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
            TxtNuevaContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
            TxtConfirmarContrasena.UseSystemPasswordChar = !ChkMostrarContrasenas.Checked;
        }

        // =============================================
        // BOTÓN: CANCELAR
        // =============================================
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (esObligatorio)
            {
                MessageBox.Show(
                    "⚠️ No puedes cancelar el cambio de contraseña.\n" +
                    "Es obligatorio cambiar tu contraseña temporal.",
                    "Cambio obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar?",
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