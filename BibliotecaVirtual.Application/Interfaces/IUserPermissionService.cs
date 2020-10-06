using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vigente.Web.Application.ViewModels;

namespace BibliotecaVirtual.Application.Interfaces
{
    public interface IUserPermissionService : IBaseService, IDisposable
    {
        /// <summary>
        /// Realiza uma pesquisa por usuários.
        /// </summary>
        /// <param name="filtro">Filtro da pesquisa.</param>
        /// <returns></returns>
        Task<IEnumerable<PermissionViewModel>> ObterUsersPermissions(string filtro);

        /// <summary>
        /// Criar os roles e claims padrões da aplicação.
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateClaims();

        /// <summary>
        /// Altera o perfil atual do usuário.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <param name="userRole">Nome do novo perfil do usuário.</param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(int usuarioId, string userRole);

        /// <summary>
        /// Remove o usuário de todos os perfis do sistema.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <param name="userRole">Nome do novo perfil do usuário.</param>
        /// <returns></returns>
        Task<bool> RemoveUserRole(int usuarioId, string userRole);
    }
}
