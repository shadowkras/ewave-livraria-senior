using BibliotecaVirtual.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Interfaces
{
    public interface IUserService : IBaseService, IDisposable
    {
        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<UserViewModel> AddUser(UserViewModel viewModel);

        /// <summary>
        /// Altera informações de uma usuário cadastrada.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da usuário.</param>
        /// <returns></returns>
        Task<UserViewModel> UpdateUser(UserViewModel viewModel);

        /// <summary>
        /// Altera a foto do usuário logado.
        /// </summary>
        /// <param name="imagemFoto">String base64 com a imagem da foto do usuário.</param>
        /// <returns></returns>
        Task<bool> UpdateUserProfilePicture(string imagemFoto);

        /// <summary>
        /// Remove uma usuário.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da usuário.</param>
        /// <returns></returns>
        Task<bool> DeleteUser(UserViewModel viewModel);

        /// <summary>
        /// Deleta uma usuário cadastrada.
        /// </summary>
        /// <param name="UserId">Identificador da usuário.</param>
        /// <returns></returns>
        Task<bool> DeleteUser(int UserId);

        /// <summary>
        /// Retorna o nome do usuário.
        /// </summary>
        /// <returns></returns>
        Task<string> ObtainUserName();

        /// <summary>
        /// Retorna o nome completo do usuário.
        /// </summary>
        /// <returns></returns>
        Task<string> ObtainUserFullName();

        /// <summary>
        /// Retorna o e-mail cadastrado para o usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<string> ObtainUserEmail();

        /// <summary>
        /// Retorna o email cadastrado para o usuário informado.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <returns></returns>
        Task<string> ObtainUserEmail(long usuarioId);

        /// <summary>
        /// Retorna a foto do usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<string> ObtainUserProfilePicture();

        /// <summary>
        /// Obtém uma lista com os usuárioes cadastrados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserViewModel>> ObtainUsers(string filtro);

        /// <summary>
        /// Obtém uma usuário a partir do seu identificador.
        /// </summary>
        /// <param name="UserId">Identificador da usuário.</param>
        /// <returns></returns>
        Task<UserViewModel> ObtainUser(int UserId);

        /// <summary>
        /// Verifica se o usuário possui a permissão especificada.
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        bool UserHasClaim(string claim);

        /// <summary>
        /// Verifica se o usuário está no perfil especificado.
        /// </summary>
        /// <param name="role">Nome do perfil.</param>
        /// <returns></returns>
        bool UserIsInRole(string role);
    }
}
