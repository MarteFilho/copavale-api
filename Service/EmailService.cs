using System;
using System.Net;
using System.Net.Mail;

namespace CopaVale.Service
{
    public class EmailService
    {
        public string sendMail(string to, string asunto, string body)
        {
            string msge = "Erro ao enviar o e-mail, favor verifique os dados digitados!";
            string from = "suporte@copavale.org";
            string displayName = "Copa Vale - Ticket Criado com Sucesso!";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("mail.copavale.org", 587);
                client.Credentials = new NetworkCredential(from, "copavale@321");


                client.Send(mail);
                Console.WriteLine("funcionou");
                msge = "¡Correo enviado exitosamente! Pronto te contactaremos.";

            }
            catch
            {
                return ". Por favor verifica tu conexión a internet y que tus datos sean correctos e intenta nuevamente.";
            }

            return msge;
        }
    }
}
