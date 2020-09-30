using System.Collections.Generic;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de editora de livros.
    /// </summary>
    public class Publisher : IAutoMappleable
    {
        /// <summary>
        /// Identificador da editora.
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Nome da editora.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// Construtor da entidade Publisher, necessário para o EntityFramework.
        /// </summary>
        protected Publisher()
        { }

        /// <summary>
        /// Construtor com as informações requeridas de um autor.
        /// </summary>
        /// <param name="name"></param>
        public Publisher(string name)
        {
            Name = name;
        }
    }
}
