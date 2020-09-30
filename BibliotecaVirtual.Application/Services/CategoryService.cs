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
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        #region Construtor

        public CategoryService(ICategoryRepository repositorio,
                               IApplicationUnitOfWork uow)
            : base(uow)
        {
            _repository = repositorio;
        }

        #endregion

        /// <summary>
        /// Cadastra um novo gênero.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> AddCategory(CategoryViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p=> p.Description == viewModel.Description))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Gênero");
                return viewModel;
            }

            #endregion

            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Insert(Category);
            await Commit();

            //Recuperando o valor recebido pelo CategoryId.
            viewModel.CategoryId = Category.CategoryId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Description == viewModel.Description && p.CategoryId != viewModel.CategoryId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro Gênero com esta descrição.");
                return viewModel;
            }

            #endregion

            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Update(Category);
            await Commit();

            return viewModel;
        }

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCategory(CategoryViewModel viewModel)
        {
            var Category = viewModel.AutoMapear<CategoryViewModel, Category>();
            _repository.Delete(Category);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        public async Task<bool> DeleteCategory(int CategoryId)
        {
            _repository.Delete(p=> p.CategoryId == CategoryId);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os gêneroes cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> ObtainCategories()
        {
            var Categorys = await _repository.SelectAll();
            var viewModel = Categorys.AutoMapearLista<Category, CategoryViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um gênero a partir do seu identificador.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        public async Task<CategoryViewModel> ObtainCategory(int CategoryId)
        {
            var Categorys = await _repository.Select(p=> p.CategoryId == CategoryId);
            var viewModel = Categorys.AutoMapear<Category, CategoryViewModel>();
            return viewModel;
        }
    }
}
