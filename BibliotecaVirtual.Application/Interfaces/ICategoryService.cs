using BibliotecaVirtual.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Interfaces
{
    public interface ICategoryService : IBaseService
    {
        /// <summary>
        /// Cadastra um novo gênero.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<CategoryViewModel> AddCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Altera informações de um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        Task<CategoryViewModel> UpdateCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações do gênero.</param>
        /// <returns></returns>
        Task<bool> DeleteCategory(CategoryViewModel viewModel);

        /// <summary>
        /// Deleta um gênero cadastrado.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        Task<bool> DeleteCategory(int CategoryId);

        /// <summary>
        /// Obtém uma lista com os gênero cadastrados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoryViewModel>> ObtainCategories();

        /// <summary>
        /// Obtém um gênero a partir do seu identificador.
        /// </summary>
        /// <param name="CategoryId">Identificador do gênero.</param>
        /// <returns></returns>
        Task<CategoryViewModel> ObtainCategory(int CategoryId);
    }
}
