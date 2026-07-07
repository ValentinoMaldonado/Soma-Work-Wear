using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        /// <summary>
        /// Convierte una cadena a SHA256
        /// </summary>
        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Genera una clave temporal aleatoria
        /// </summary>
        public static string GenerarClaveAleatoria(int longitud = 12)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                sb.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Envía un correo de bienvenida con clave temporal
        /// </summary>
        public static bool EnviarCorreoBienvenida(string correoDestino, string nombre, string clavesTemporal)
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["SmtpServer"] ?? "smtp.gmail.com";
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"] ?? "";
                string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"] ?? "";

                using (SmtpClient cliente = new SmtpClient(smtpServer, smtpPort))
                {
                    cliente.EnableSsl = true;
                    cliente.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    cliente.Timeout = 10000;

                    MailMessage mensaje = new MailMessage();
                    mensaje.From = new MailAddress(smtpUser, "SomaWorkWear - Admin");
                    mensaje.To.Add(correoDestino);
                    mensaje.Subject = "Bienvenida a SomaWorkWear - Clave Temporal";
                    mensaje.Body = $@"
                        <html>
                            <head>
                                <style>
                                    body {{ font-family: Arial, sans-serif; background-color: #f5f5f5; }}
                                    .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 8px; }}
                                    .header {{ background: linear-gradient(135deg, #0D6EFD 0%, #003d99 100%); color: white; padding: 20px; text-align: center; border-radius: 8px; }}
                                    .content {{ padding: 20px 0; }}
                                    .clave-box {{ background-color: #f0f0f0; padding: 15px; border-left: 4px solid #198754; margin: 15px 0; font-family: monospace; font-size: 16px; }}
                                    .footer {{ color: #999; font-size: 12px; text-align: center; margin-top: 20px; }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <div class='header'>
                                        <h1>¡Bienvenida a SomaWorkWear!</h1>
                                    </div>
                                    <div class='content'>
                                        <p>Hola <strong>{nombre}</strong>,</p>
                                        <p>Tu cuenta ha sido creada exitosamente en el panel de administración de SomaWorkWear.</p>
                                        <p><strong>Tu clave temporal es:</strong></p>
                                        <div class='clave-box'>{clavesTemporal}</div>
                                        <p>Por razones de seguridad, esta clave es temporal y deberás cambiarla en tu primer acceso.</p>
                                        <p style='color: red;'><strong>⚠️ IMPORTANTE:</strong> No compartas esta clave con nadie.</p>
                                        <p>Si no solicitaste esta cuenta, por favor contacta al administrador.</p>
                                    </div>
                                    <div class='footer'>
                                        <p>© {DateTime.Now.Year} SomaWorkWear - Todos los derechos reservados</p>
                                    </div>
                                </div>
                            </body>
                        </html>";
                    mensaje.IsBodyHtml = true;

                    cliente.Send(mensaje);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}
