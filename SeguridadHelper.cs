using System;
using System.Security.Cryptography;
using System.Text;

namespace Visitas
{
    /// <summary>
    /// Clase para manejar la seguridad: hashing de contraseñas, generación de tokens, etc.
    /// Utiliza BCrypt para el hashing de contraseñas (algoritmo más seguro que SHA256)
    /// </summary>
    public static class SeguridadHelper
    {
        // =============================================
        // HASHING DE CONTRASEÑAS CON BCRYPT
        // =============================================

        /// <summary>
        /// Genera un hash seguro de una contraseña usando BCrypt
        /// </summary>
        /// <param name="contrasena">Contraseña en texto plano</param>
        /// <returns>Hash de la contraseña (incluye el salt automáticamente)</returns>
        public static string HashearContrasena(string contrasena)
        {
            // BCrypt.Net.BCrypt.HashPassword genera automáticamente:
            // - Un salt único y aleatorio
            // - El hash de la contraseña
            // - Todo empaquetado en un string
            return BCrypt.Net.BCrypt.HashPassword(contrasena, workFactor: 12);
        }

        /// <summary>
        /// Verifica si una contraseña coincide con un hash
        /// </summary>
        /// <param name="contrasena">Contraseña en texto plano</param>
        /// <param name="hashAlmacenado">Hash almacenado en la base de datos</param>
        /// <returns>True si la contraseña es correcta, False si no</returns>
        public static bool VerificarContrasena(string contrasena, string hashAlmacenado)
        {
            try
            {
                // BCrypt.Net.BCrypt.Verify compara la contraseña con el hash
                // Extrae automáticamente el salt del hash y lo usa para verificar
                return BCrypt.Net.BCrypt.Verify(contrasena, hashAlmacenado);
            }
            catch (Exception)
            {
                // Si hay algún error (hash inválido, formato incorrecto, etc.)
                return false;
            }
        }

        // =============================================
        // GENERACIÓN DE TOKENS DE SESIÓN
        // =============================================

        /// <summary>
        /// Genera un token único para identificar una sesión
        /// </summary>
        /// <returns>Token único de 64 caracteres</returns>
        public static string GenerarTokenSesion()
        {
            // Genera un token aleatorio usando criptografía segura
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32]; // 32 bytes = 256 bits
                rng.GetBytes(tokenData);

                // Convierte a string hexadecimal
                return BitConverter.ToString(tokenData).Replace("-", "").ToLower();
            }
        }

        // =============================================
        // VALIDACIÓN DE CONTRASEÑAS
        // =============================================

        /// <summary>
        /// Valida que una contraseña cumpla con los requisitos mínimos de seguridad
        /// </summary>
        /// <param name="contrasena">Contraseña a validar</param>
        /// <param name="mensajeError">Mensaje de error si no cumple (output)</param>
        /// <returns>True si es válida, False si no</returns>
        public static bool ValidarFortalezaContrasena(string contrasena, out string mensajeError)
        {
            mensajeError = "";

            // Requisito 1: Longitud mínima
            if (contrasena.Length < 8)
            {
                mensajeError = "La contraseña debe tener al menos 8 caracteres";
                return false;
            }

            // Requisito 2: Al menos una letra mayúscula
            bool tieneMayuscula = false;
            foreach (char c in contrasena)
            {
                if (char.IsUpper(c))
                {
                    tieneMayuscula = true;
                    break;
                }
            }

            if (!tieneMayuscula)
            {
                mensajeError = "La contraseña debe contener al menos una letra MAYÚSCULA";
                return false;
            }

            // Requisito 3: Al menos una letra minúscula
            bool tieneMinuscula = false;
            foreach (char c in contrasena)
            {
                if (char.IsLower(c))
                {
                    tieneMinuscula = true;
                    break;
                }
            }

            if (!tieneMinuscula)
            {
                mensajeError = "La contraseña debe contener al menos una letra minúscula";
                return false;
            }

            // Requisito 4: Al menos un número
            bool tieneNumero = false;
            foreach (char c in contrasena)
            {
                if (char.IsDigit(c))
                {
                    tieneNumero = true;
                    break;
                }
            }

            if (!tieneNumero)
            {
                mensajeError = "La contraseña debe contener al menos un NÚMERO";
                return false;
            }

            // Requisito 5: Al menos un carácter especial
            string caracteresEspeciales = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            bool tieneEspecial = false;
            foreach (char c in contrasena)
            {
                if (caracteresEspeciales.Contains(c.ToString()))
                {
                    tieneEspecial = true;
                    break;
                }
            }

            if (!tieneEspecial)
            {
                mensajeError = "La contraseña debe contener al menos un carácter especial (!@#$%^&*()_+-=[]{}|;:,.<>?)";
                return false;
            }

            // Todas las validaciones pasaron
            mensajeError = "";
            return true;
        }

        /// <summary>
        /// Obtiene el nivel de fortaleza de una contraseña (0-5)
        /// </summary>
        /// <param name="contrasena">Contraseña a evaluar</param>
        /// <returns>0=Muy débil, 1=Débil, 2=Regular, 3=Buena, 4=Fuerte, 5=Muy fuerte</returns>
        public static int ObtenerNivelFortaleza(string contrasena)
        {
            int puntos = 0;

            if (contrasena.Length >= 8) puntos++;
            if (contrasena.Length >= 12) puntos++;

            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;
            bool tieneEspecial = false;

            foreach (char c in contrasena)
            {
                if (char.IsUpper(c)) tieneMayuscula = true;
                if (char.IsLower(c)) tieneMinuscula = true;
                if (char.IsDigit(c)) tieneNumero = true;
                if ("!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c.ToString())) tieneEspecial = true;
            }

            if (tieneMayuscula) puntos++;
            if (tieneMinuscula) puntos++;
            if (tieneNumero) puntos++;
            if (tieneEspecial) puntos++;

            // Máximo 6 puntos, retornar entre 0-5
            return Math.Min(puntos, 5);
        }

        // =============================================
        // SANITIZACIÓN DE INPUTS
        // =============================================

        /// <summary>
        /// Limpia un string de caracteres peligrosos para SQL Injection
        /// NOTA: Esto es una medida adicional, NO reemplaza el uso de SqlParameters
        /// </summary>
        /// <param name="input">String a limpiar</param>
        /// <returns>String limpio</returns>
        public static string SanitizarInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remover caracteres peligrosos
            input = input.Replace("'", "");
            input = input.Replace("\"", "");
            input = input.Replace("--", "");
            input = input.Replace(";", "");
            input = input.Replace("/*", "");
            input = input.Replace("*/", "");
            input = input.Replace("xp_", "");
            input = input.Replace("sp_", "");

            return input.Trim();
        }

        // =============================================
        // INFORMACIÓN DEL SISTEMA
        // =============================================

        /// <summary>
        /// Obtiene la dirección IP de la máquina local
        /// </summary>
        /// <returns>Dirección IP o "DESCONOCIDA"</returns>
        public static string ObtenerDireccionIP()
        {
            try
            {
                string hostName = System.Net.Dns.GetHostName();
                System.Net.IPAddress[] addresses = System.Net.Dns.GetHostAddresses(hostName);

                foreach (System.Net.IPAddress address in addresses)
                {
                    // Buscar IPv4
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }

                return "127.0.0.1"; // Localhost por defecto
            }
            catch
            {
                return "DESCONOCIDA";
            }
        }

        /// <summary>
        /// Obtiene el nombre de la máquina
        /// </summary>
        /// <returns>Nombre de la máquina o "DESCONOCIDO"</returns>
        public static string ObtenerNombreMaquina()
        {
            try
            {
                return Environment.MachineName;
            }
            catch
            {
                return "DESCONOCIDO";
            }
        }
    }
}