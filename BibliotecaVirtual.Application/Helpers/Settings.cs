namespace BibliotecaVirtual.Application.Helpers
{
    /// <summary>
    ///     Provides access to the current application's configuration file.
    /// </summary>
    public class Settings
    {
        /// <summary>
        ///     Retrieves the entry value for the following composed key: "config:Company" as a string.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        ///     Retrieves the entry value for the following composed key: "config:Project" as a string.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        ///     Retrieves the entry value for the following composed key: "config:AplicacaoVersao" as a string.
        /// </summary>
        public string AplicacaoVersao { get; set; }
    }
}
