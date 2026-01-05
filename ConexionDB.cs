using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Visitas
{
    /// <summary>
    /// Clase mejorada para manejar la conexión a la base de datos SQL Server.
    /// Lee la cadena de conexión desde App.config para mayor flexibilidad.
    /// Crea conexiones nuevas por solicitud para evitar conflictos de objetos desechados.
    /// </summary>
    public class ConexionDB
    {
        // =============================================
        // CADENA DE CONEXIÓN DESDE App.config
        // =============================================
        /// <summary>
        /// Obtiene la cadena de conexión desde App.config
        /// Si no existe, usa una cadena por defecto para LocalDB
        /// </summary>
        private static string ObtenerCadenaConexion()
        {
            try
            {
                // Intentar leer desde App.config
                string cadena = ConfigurationManager.ConnectionStrings["ConexionSQL"]?.ConnectionString;

                if (string.IsNullOrEmpty(cadena))
                {
                    // Si no existe en config, usar cadena por defecto (LocalDB)
                    return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControlVisitas;Integrated Security=True;Connect Timeout=30;";
                }

                return cadena;
            }
            catch
            {
                // Si hay error leyendo config, usar cadena por defecto
                return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControlVisitas;Integrated Security=True;Connect Timeout=30;";
            }
        }

        /// <summary>
        /// Obtiene una NUEVA conexión abierta a la base de datos
        /// </summary>
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                // Obtener la cadena de conexión
                string cadenaConexion = ObtenerCadenaConexion();

                // Crear y abrir una nueva conexión
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al conectar con la base de datos:\n\n" + ex.Message +
                    "\n\nVerifique que SQL Server esté instalado y ejecutándose.\n" +
                    "Si el problema persiste, revise la cadena de conexión en el archivo de configuración.",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado al crear conexión:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null;
            }
        }

        /// <summary>
        /// Prueba la conexión a la base de datos
        /// </summary>
        public static bool ProbarConexion()
        {
            try
            {
                string cadenaConexion = ObtenerCadenaConexion();
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene la cadena de conexión actual (útil para diagnóstico)
        /// </summary>
        public static string ObtenerCadenaActual()
        {
            return ObtenerCadenaConexion();
        }

        /// <summary>
        /// Ejecuta un comando (INSERT, UPDATE, DELETE) asegurando cerrar la conexión al final.
        /// </summary>
        public static bool EjecutarComando(string nombreProcedimiento, SqlParameter[] parametros = null)
        {
            // Usamos 'using' para asegurar que la conexión se cierre y elimine correctamente
            using (SqlConnection conn = ObtenerConexion())
            {
                if (conn == null) return false;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar comando: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Ejecuta una consulta y devuelve un DataTable, cerrando la conexión al final.
        /// </summary>
        public static DataTable EjecutarConsulta(string nombreProcedimiento, SqlParameter[] parametros = null)
        {
            using (SqlConnection conn = ObtenerConexion())
            {
                if (conn == null) return null;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Ejecuta escalar (COUNT, ID, etc) cerrando la conexión al final.
        /// </summary>
        public static object EjecutarEscalar(string nombreProcedimiento, SqlParameter[] parametros = null)
        {
            using (SqlConnection conn = ObtenerConexion())
            {
                if (conn == null) return null;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error escalar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Ejecuta consulta directa (Query) cerrando la conexión al final.
        /// </summary>
        public static DataTable EjecutarConsultaDirecta(string query)
        {
            using (SqlConnection conn = ObtenerConexion())
            {
                if (conn == null) return null;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error consulta directa: " + ex.Message);
                    return null;
                }
            }
        }
    }
}


