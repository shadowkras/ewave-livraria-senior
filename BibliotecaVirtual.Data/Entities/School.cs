using System.Collections.Generic;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de instituição de ensino.
    /// </summary>
    public class School : IDatabaseEntity
    {
        /// <summary>
        /// Identificador da instituição de ensino.
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// Nome da instituição.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Documento de identificação da instituição.
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// Telefone de contato da instituição.
        /// </summary>
        public string Telefone { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// Construtor da entidade School, necessário para o EntityFramework.
        /// </summary>
        public School()
        { }

        /// <summary>
        /// Construtor com as informações requeridas de uma instituição..
        /// </summary>
        /// <param name="name">Nome da instituição.</param>
        public School(string name)
        {
            Name = name;
        }
    }
}
