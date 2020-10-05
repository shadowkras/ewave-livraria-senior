namespace BibliotecaVirtual.Application.Helpers
{
    /// <summary>
    /// Classe helper para salvar as configurações de email da aplicação.
    /// </summary>
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
