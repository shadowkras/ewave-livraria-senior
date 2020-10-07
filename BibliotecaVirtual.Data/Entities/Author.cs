using System.Collections.Generic;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de autor de livros.
    /// </summary>
    public class Author : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do autor.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Nome do autor.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// Construtor da entidade Author, necessário para o EntityFramework.
        /// </summary>
        public Author()
        { }

        /// <summary>
        /// Construtor com as informações requeridas de um autor.
        /// </summary>
        /// <param name="name"></param>
        public Author(string name)
        {
            Name = name;
        }
    }
}
