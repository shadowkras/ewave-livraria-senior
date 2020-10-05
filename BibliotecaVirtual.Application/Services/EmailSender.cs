using BibliotecaVirtual.Application.Helpers;
using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// Realiza a configuração do e-mail.
        /// </summary>
        /// <returns></returns>
        private bool ConfigurarEmail()
        {
            try
            {
                var config = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .Build();

                var emailSettings = new EmailSettings();
                config.GetSection(nameof(EmailSettings)).Bind(emailSettings);

                _emailSettings = emailSettings;

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                //TODO Logar o erro.
                return false;
            }
        }

        /// <summary>
        /// Envia um e-email utilizando as configurações da aplicação.
        /// </summary>
        /// <param name="email">E-mail do destinatário.</param>
        /// <param name="subject">Assunto do e-mail.</param>
        /// <param name="message">Corpo da mensagem do e-mail.</param>
        /// <returns></returns>
        public async Task SendEmail(string email, string subject, string message)
        {
            if (_emailSettings.Sender.IsEmpty() == true)
            {
                var sucesso = ConfigurarEmail();
                if (sucesso == false)
                {
                    return;
                }
            }

            try
            {
                // Credentials
                var credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                // Smtp client
                using (SmtpClient client = new SmtpClient())
                {
                    client.Port = _emailSettings.MailPort;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = _emailSettings.MailServer;
                    client.EnableSsl = _emailSettings.EnableSsl;
                    client.Credentials = credentials;

                    try
                    {
                        await client.SendMailAsync(mail);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debugger.Break();
                        //TODO Logar o erro.
                    }
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
