using BibliotecaVirtual.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Interfaces
{
    public interface IBookService : IBaseService, IDisposable
    {
        /// <summary>
        /// Cadastra um novo livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        Task<BookViewModel> AddBook(BookViewModel viewModel);

        /// <summary>
        /// Altera informações de um livro cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        Task<BookViewModel> UpdateBook(BookViewModel viewModel);

        /// <summary>
        /// Remove um livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        Task<bool> DeleteBook(BookViewModel viewModel);

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        Task<bool> DeleteBook(int bookId);

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        Task<bool> RentBook(int bookId);

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BookViewModel>> ObtainBooks();

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <param name="filtro">Filtro de pesquisa pelo titulo ou autor do livro.</param>
        /// <returns></returns>
        Task<IEnumerable<BookViewModel>> ObtainBooks(string filtro);

        /// <summary>
        /// Obtém um livro a partir do seu identificador.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        Task<BookViewModel> ObtainBook(int bookId);
    }
}
