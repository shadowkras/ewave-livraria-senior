using System.Collections.Generic;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de gênero dos livros.
    /// </summary>
    public class Category : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do gênero.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Descrição do gênero.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Url externa com informações sobre o gênero.
        /// </summary>
        public string AboutUrl { get; set; }

        public virtual ICollection<BookCategory> Books { get; set; }

        /// <summary>
        /// Construtor da entidade Category, necessário para o EntityFramework.
        /// </summary>
        public Category()
        { }

        /// <summary>
        /// Construtor com as informações requeridas de um gênero de livros.
        /// </summary>
        /// <param name="description">Descrição do gênero.</param>
        /// <param name="aboutUrl">Url externa com informações sobre o gênero.</param>
        public Category(string description, string aboutUrl)
        {
            Description = description;
            AboutUrl = aboutUrl;
        }
    }
}
