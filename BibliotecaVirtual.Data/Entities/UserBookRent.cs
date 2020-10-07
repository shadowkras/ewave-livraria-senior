using BibliotecaVirtual.Data.Interfaces;
using System;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade para registro de livros alugados por usuários
    /// </summary>
    public class UserBookRent : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do registro.
        /// </summary>
        public int UserBookRentId { get; set; }

        /// <summary>
        /// Identificador do livro.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public int UserId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Construtor da entidade BookCategory, necessário para o EntityFramework.
        /// </summary>
        public UserBookRent()
        { }

        /// <summary>
        ///  Construtor com as informações requeridas para criar o relacionamento.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <param name="userId">Identificador do usuário.</param>
        public UserBookRent(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}
