using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de relacionamento dos gêneros com os livros.
    /// </summary>
    public class BookCategory : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do livro.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Identificador do gênero.
        /// </summary>
        public int CategoryId { get; set; }  
        
        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }

        /// <summary>
        /// Construtor da entidade BookCategory, necessário para o EntityFramework.
        /// </summary>
        public BookCategory()
        { }

        /// <summary>
        ///  Construtor com as informações requeridas para criar o relacionamento.
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="categoryId"></param>
        public BookCategory(int bookId, int categoryId)
        {
            BookId = bookId;
            CategoryId = categoryId;            
        }
    }
}
