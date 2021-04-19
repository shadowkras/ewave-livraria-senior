using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Application.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserBookRentRepository _userBookRentRepository;

        #region Construtor

        public BookService(IBookRepository bookRepositorio,
                           IBookCategoryRepository bookCategoryRepository,
                           IUserRepository userRepository,
                           IUserBookRentRepository userBookRentRepository,
                           IApplicationUnitOfWork uow)
            : base(uow)
        {
            _bookRepository = bookRepositorio;
            _bookCategoryRepository = bookCategoryRepository;
            _userRepository = userRepository;
            _userBookRentRepository = userBookRentRepository;
        }

        #endregion

        #region Adicionar, alterar, excluir

        /// <summary>
        /// Cadastra um novo livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> AddBook(BookViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _bookRepository.Exists(p => p.Title == viewModel.Title))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Livro");
                return viewModel;
            }
            else if (viewModel.CategoriesList.Count == 0)
            {
                ModelError = "Nenhum gênero informado para o livro.";
                return viewModel;
            }

            #endregion

            var book = viewModel.AutoMapear<BookViewModel, Book>();

            //Convertendo a imagem de string para byte[].
            book.SetCoverImage(viewModel.CoverImageBase64);

            _bookRepository.Insert(book);

            #region Inserindo categorias

            if (viewModel.CategoriesList.Count > 0)
            {
                var bookId = _bookRepository.GetInsertCurrentKey(book, p => p.BookId);

                foreach (var categoryId in viewModel.CategoriesList)
                {
                    var bookCategory = new BookCategory(bookId, categoryId);
                    _bookCategoryRepository.Insert(bookCategory);
                }
            }

            #endregion

            await Commit();

            //Recuperando o valor recebido pelo BookId.
            viewModel.BookId = book.BookId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um livro cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> UpdateBook(BookViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _bookRepository.Exists(p => p.Title == viewModel.Title && p.BookId != viewModel.BookId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro Livro com este título.");
                return viewModel;
            }
            else if (viewModel.CategoriesList.Count == 0)
            {
                ModelError = "Nenhum gênero informado para o livro.";
                return viewModel;
            }

            #endregion

            var book = viewModel.AutoMapear<BookViewModel, Book>();

            //Convertendo a imagem de string para byte[].
            book.SetCoverImage(viewModel.CoverImageBase64);

            _bookRepository.Update(book);

            #region Inserindo categorias

            _bookCategoryRepository.Delete(p => p.BookId == book.BookId);

            if (viewModel.CategoriesList.Count > 0)
            {
                foreach (var categoryId in viewModel.CategoriesList)
                {
                    var bookCategory = new BookCategory(book.BookId, categoryId);
                    _bookCategoryRepository.Insert(bookCategory);
                }
            }

            #endregion

            await Commit();

            return viewModel;
        }

        /// <summary>
        /// Remove um livro.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do livro.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(BookViewModel viewModel)
        {
            var book = viewModel.AutoMapear<BookViewModel, Book>();
            _bookRepository.Delete(book);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(int bookId)
        {
            _bookRepository.Delete(p => p.BookId == bookId);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um livro cadastrado.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<bool> RentBook(int bookId)
        {
            var userId = await _userRepository.ObterUsuarioId();
            if (userId.HasValue == true)
            {
                if (await _userBookRentRepository.Exists(p => p.UserId == userId.Value && p.BookId == bookId && p.ReturnedDate.HasValue == false) == true)
                {
                    AddModelError("Usuário já está com uma cópia do livro informado. Só é permitido uma cópia por usuário");
                    return false;
                }
                if (await _userBookRentRepository.Count(p => p.UserId == userId.Value && p.ReturnedDate.HasValue == false) >= 2)
                {
                    AddModelError("Usuário já está com 2 livros alugados. Não é permitido alugar mais livros antes de devolver alguns.");
                    return false;
                }
                else if (await _userBookRentRepository.Exists(p => p.ReturnedDate.Value > p.ReturnDate) == true)
                {
                    var bookRentalInfraction = await _userBookRentRepository.Select(p => p.ReturnedDate.Value > p.ReturnDate);

                    if (bookRentalInfraction.ReturnedDate.Value.AddDays(30).Date > System.DateTime.Now.Date)
                    {
                        var infractionDaysLeft = bookRentalInfraction.ReturnedDate.Value.AddDays(30).Date - System.DateTime.Now.Date;
                        AddModelError($"Usuário está com uma restrição de aluguel por mais {infractionDaysLeft.TotalDays} dias devido atrasos na devolução.");
                        return false;
                    }
                }

                var bookRental = new UserBookRent(bookId, userId.Value)
                {
                    RentalDate = System.DateTime.Now,
                    ReturnDate = System.DateTime.Now.AddDays(30),
                };

                _userBookRentRepository.Insert(bookRental);
                await Commit();

                return OperationSuccesful;
            }
            else
            {
                AddModelError("Usuário não encontrado ou não logado na aplicação. Faça o login e tente novamente.");
                return false;
            }

        }

        #endregion

        #region Consultas

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookViewModel>> ObtainBooks()
        {
            var books = await _bookRepository.SelectList(null, p => new BookViewModel()
            {
                BookId = p.BookId,
                Title = p.Title,
                AuthorId = p.AuthorId,
                AuthorName = p.Author.Name, //Obtendo o nome do autor utilizando a propridade de navegação.
                PublishDate = p.PublishDate,
            });

            return books;
        }

        /// <summary>
        /// Obtém uma lista com os livros cadastrados.
        /// </summary>
        /// <param name="filtro">Filtro de pesquisa pelo titulo ou autor do livro.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BookViewModel>> ObtainBooks(string filtro)
        {
            #region Predicados com os filtros de pesquisa

            Expression<Func<Book, bool>> predicate = null;

            if (string.IsNullOrEmpty(filtro) == false)
            {
                predicate = p => p.Title.Contains(filtro) || p.Author.Name.Contains(filtro);
            }

            #endregion

            var books = await _bookRepository.SelectList(predicate,
                p => new BookViewModel()
                {
                    BookId = p.BookId,
                    Title = p.Title,
                    AuthorId = p.AuthorId,
                    AuthorName = p.Author.Name, //Obtendo o nome do autor utilizando a propridade de navegação.
                    PublishDate = p.PublishDate,
                });

            return books;
        }

        /// <summary>
        /// Obtém um livro a partir do seu identificador.
        /// </summary>
        /// <param name="bookId">Identificador do livro.</param>
        /// <returns></returns>
        public async Task<BookViewModel> ObtainBook(int bookId)
        {
            var books = await _bookRepository.Select(p => p.BookId == bookId);
            var categories = await _bookCategoryRepository.SelectList(p => p.BookId == bookId);

            var viewModel = books.AutoMapear<Book, BookViewModel>();

            //Criando uma lista de categorias separadas por virgula para levar até a view.
            foreach (var category in categories)
            {
                viewModel.CategoriesList.Add(category.CategoryId);
            }

            //Convertendo a imagem de  byte[] para string.
            viewModel.CoverImageBase64 = books.CoverImage.AsStringBase64();

            return viewModel;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Liberar recursos da memória.
        /// </summary>
        public void Dispose()
        {
            _bookRepository.Dispose();
            _bookCategoryRepository.Dispose();
            _userRepository.Dispose();
            _userBookRentRepository.Dispose();
        }

        #endregion
    }
}
