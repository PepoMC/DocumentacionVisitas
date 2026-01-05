using System;
using System.Data;
using System.Data.SqlClient;

namespace Visitas
{
    /// <summary>
    /// Clase estática para mantener información de la sesión del usuario actual
    /// Esta información está disponible en toda la aplicación mientras el usuario esté logueado
    /// </summary>
    public static class SesionActual
    {
        // =============================================
        // PROPIEDADES DE LA SESIÓN
        // =============================================

        /// <summary>
        /// ID del usuario logueado en la base de datos
        /// </summary>
        public static int IdUsuario { get; set; }

        /// <summary>
        /// Nombre de usuario (login)
        /// </summary>
        public static string NombreUsuario { get; set; }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        public static string NombreCompleto { get; set; }

        /// <summary>
        /// Rol del usuario: "Administrador" o "Usuario"
        /// </summary>
        public static string Rol { get; set; }

        /// <summary>
        /// Token único de la sesión (generado al hacer login)
        /// </summary>
        public static string TokenSesion { get; set; }

        /// <summary>
        /// Fecha y hora en que el usuario hizo login
        /// </summary>
        public static DateTime FechaHoraLogin { get; set; }

        /// <summary>
        /// Dirección IP desde donde se conectó el usuario
        /// </summary>
        public static string DireccionIP { get; set; }

        /// <summary>
        /// Nombre de la máquina desde donde se conectó
        /// </summary>
        public static string NombreMaquina { get; set; }

        /// <summary>
        /// Indica si hay una sesión activa
        /// </summary>
        public static bool SesionActiva { get; set; }

        // =============================================
        // MÉTODOS DE LA SESIÓN
        // =============================================

        /// <summary>
        /// Inicializa una nueva sesión con los datos del usuario
        /// </summary>
        public static void IniciarSesion(
            int idUsuario,
            string nombreUsuario,
            string nombreCompleto,
            string rol)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            NombreCompleto = nombreCompleto;
            Rol = rol;
            TokenSesion = SeguridadHelper.GenerarTokenSesion();
            FechaHoraLogin = DateTime.Now;
            DireccionIP = SeguridadHelper.ObtenerDireccionIP();
            NombreMaquina = SeguridadHelper.ObtenerNombreMaquina();
            SesionActiva = true;

            // ✅ REGISTRAR INICIO DE SESIÓN EN AUDITORÍA
            RegistrarAuditoria(
                "Inicio de Sesión",
                "SesionesActivas",
                idUsuario,
                $"Usuario {nombreCompleto} ({nombreUsuario}) inició sesión en el sistema con rol {rol}",
                null,
                $"Token: {TokenSesion.Substring(0, 8)}..., IP: {DireccionIP}, Máquina: {NombreMaquina}, Fecha: {FechaHoraLogin:yyyy-MM-dd HH:mm:ss}"
            );
        }

        /// <summary>
        /// Cierra la sesión actual y limpia toda la información
        /// </summary>
        public static void CerrarSesion()
        {
            if (SesionActiva)
            {
                // ✅ REGISTRAR CIERRE DE SESIÓN EN AUDITORÍA ANTES DE LIMPIAR
                TimeSpan duracionSesion = DateTime.Now - FechaHoraLogin;
                RegistrarAuditoria(
                    "Cierre de Sesión",
                    "SesionesActivas",
                    IdUsuario,
                    $"Usuario {NombreCompleto} ({NombreUsuario}) cerró sesión del sistema",
                    $"Sesión iniciada: {FechaHoraLogin:yyyy-MM-dd HH:mm:ss}",
                    $"Duración de sesión: {FormatearTiempo(duracionSesion)}, Hora cierre: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                );
            }

            IdUsuario = 0;
            NombreUsuario = null;
            NombreCompleto = null;
            Rol = null;
            TokenSesion = null;
            FechaHoraLogin = DateTime.MinValue;
            DireccionIP = null;
            NombreMaquina = null;
            SesionActiva = false;
        }

        /// <summary>
        /// Verifica si el usuario actual es administrador
        /// </summary>
        /// <returns>True si es admin, False si no</returns>
        public static bool EsAdministrador()
        {
            return SesionActiva && Rol == "Administrador";
        }

        /// <summary>
        /// Verifica si el usuario actual es usuario normal
        /// </summary>
        /// <returns>True si es usuario normal, False si no</returns>
        public static bool EsUsuarioNormal()
        {
            return SesionActiva && Rol == "Usuario";
        }

        /// <summary>
        /// Obtiene el tiempo que lleva la sesión activa
        /// </summary>
        /// <returns>TimeSpan con la duración de la sesión</returns>
        public static TimeSpan TiempoSesion()
        {
            if (!SesionActiva)
                return TimeSpan.Zero;

            return DateTime.Now - FechaHoraLogin;
        }

        /// <summary>
        /// Obtiene un resumen de la sesión actual
        /// </summary>
        /// <returns>String con información de la sesión</returns>
        public static string ObtenerResumenSesion()
        {
            if (!SesionActiva)
                return "No hay sesión activa";

            return string.Format(
                "Usuario: {0} ({1})\n" +
                "Rol: {2}\n" +
                "Sesión iniciada: {3:dd/MM/yyyy HH:mm}\n" +
                "Duración: {4}\n" +
                "Máquina: {5}\n" +
                "IP: {6}",
                NombreCompleto,
                NombreUsuario,
                Rol,
                FechaHoraLogin,
                FormatearTiempo(TiempoSesion()),
                NombreMaquina,
                DireccionIP
            );
        }

        /// <summary>
        /// Formatea un TimeSpan a formato legible
        /// </summary>
        private static string FormatearTiempo(TimeSpan tiempo)
        {
            if (tiempo.TotalSeconds < 60)
                return string.Format("{0} segundos", (int)tiempo.TotalSeconds);

            if (tiempo.TotalMinutes < 60)
                return string.Format("{0} minutos", (int)tiempo.TotalMinutes);

            if (tiempo.TotalHours < 24)
                return string.Format("{0} horas, {1} minutos", tiempo.Hours, tiempo.Minutes);

            return string.Format("{0} días, {1} horas", tiempo.Days, tiempo.Hours);
        }

        /// <summary>
        /// Valida que haya una sesión activa, si no, lanza excepción
        /// </summary>
        public static void ValidarSesionActiva()
        {
            if (!SesionActiva)
            {
                throw new InvalidOperationException("No hay una sesión activa. Debes iniciar sesión primero.");
            }
        }

        // =============================================
        // MÉTODO: REGISTRAR AUDITORÍA
        // =============================================
        /// <summary>
        /// Registra una acción en el historial de cambios
        /// </summary>
        private static void RegistrarAuditoria(string tipoAccion, string tabla, int idRegistro,
            string descripcion, string datosAnteriores = null, string datosNuevos = null)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerConexion();
                if (conexion == null) return;

                SqlCommand cmd = new SqlCommand("sp_RegistrarHistorialCambio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario ?? "Sistema");
                cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                cmd.Parameters.AddWithValue("@TablaAfectada", tabla);
                cmd.Parameters.AddWithValue("@IdRegistroAfectado", idRegistro);

                cmd.Parameters.AddWithValue("@DatosAnteriores",
                    string.IsNullOrEmpty(datosAnteriores) ? (object)DBNull.Value : datosAnteriores);
                cmd.Parameters.AddWithValue("@DatosNuevos",
                    string.IsNullOrEmpty(datosNuevos) ? (object)DBNull.Value : datosNuevos);

                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@DireccionIP", DireccionIP ?? "DESCONOCIDA");
                cmd.Parameters.AddWithValue("@NombreMaquina", NombreMaquina ?? "DESCONOCIDO");

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
    }
}