using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Interfaces
{
    /// <summary>
    /// Interface para envio de e-mail da aplicação.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Envia um e-email utilizando as configurações da aplicação.
        /// </summary>
        /// <param name="email">E-mail do destinatário.</param>
        /// <param name="subject">Assunto do e-mail.</param>
        /// <param name="message">Corpo da mensagem do e-mail.</param>
        /// <returns></returns>
        Task SendEmail(string email, string subject, string message);
    }
}
