using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using SystemsIntegration.Api.Models.Entities;

namespace SystemsIntegration.Api.CrossCutting
{
    public static class Shared
    {
        public static string EncryptSha256Key(string message, string secret)
        {
            Encoding encoding = Encoding.UTF8;
            using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(secret)))
            {
                var msg = encoding.GetBytes(message);
                var hash = hmac.ComputeHash(msg);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }

        public static void EnviarCorreo(List<NotificacionDestinoEntity> destinatarios, string mensaje)
        {
            try
            {
                string body = $"<html><body><h1>El sistema de integración informa lo siguiente: </h1><p>{mensaje}</p><body></html>";

                SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("admin@unilene.com", "T3cnologia.INF$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("admin@unilene.com", "Alerta Unishop");

                foreach(NotificacionDestinoEntity destino in destinatarios)
                {
                    if(destino.Adicional1 == "Para")
                        mail.To.Add(new MailAddress(destino.Destino));
                    else
                        if (destino.Adicional1 == "CC")
                            mail.CC.Add(new MailAddress(destino.Destino));
                }

                mail.Subject = "Mensaje del sistema de integración con Unishop";
                mail.IsBodyHtml = true;
                mail.Body = body;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
