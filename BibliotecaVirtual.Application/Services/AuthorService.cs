using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IAuthorRepository _repository;

        #region Construtor

        public AuthorService(IAuthorRepository repositorio,
                             IApplicationUnitOfWork uow)
            : base(uow)
        {
            _repository = repositorio;
        }

        /// <summary>
        /// Cadastra um novo autor.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> AddAuthor(AuthorViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Autor(a)");
                return viewModel;
            }

            #endregion

            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Insert(author);
            await Commit();

            //Recuperando o valor recebido pelo AuthorId.
            viewModel.AuthorId = author.AuthorId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um autor cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> UpdateAuthor(AuthorViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Name == viewModel.Name && p.AuthorId != viewModel.AuthorId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro(a) Autor(a) com este nome.");
                return viewModel;
            }

            #endregion

            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Update(author);
            await Commit();

            return viewModel;
        }

        /// <summary>
        /// Deleta um autor cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do autor.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthor(AuthorViewModel viewModel)
        {
            var author = viewModel.AutoMapear<AuthorViewModel, Author>();
            _repository.Delete(author);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um autor cadastrado.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAuthor(int authorId)
        {
            _repository.Delete(p => p.AuthorId == authorId);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os autores cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorViewModel>> ObtainAuthors()
        {
            var authors = await _repository.SelectAll();
            var viewModel = authors.AutoMapearLista<Author, AuthorViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um autor a partir do seu identificador.
        /// </summary>
        /// <param name="authorId">Identificador do autor.</param>
        /// <returns></returns>
        public async Task<AuthorViewModel> ObtainAuthor(int authorId)
        {
            var authors = await _repository.Select(p => p.AuthorId == authorId);
            var viewModel = authors.AutoMapear<Author, AuthorViewModel>();
            return viewModel;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Liberar recursos da memória.
        /// </summary>
        public void Dispose()
        {
            _repository.Dispose();
        }

        #endregion
    }
}
