using BibliotecaVirtual.Data.Entities;
using System;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>, IDisposable
    {
        /// <summary>
        /// Obter o IdentityId do Identity do usuário.
        /// </summary>
        /// <returns></returns>
        string ObterIdentityUserId();

        /// <summary>
        /// Obter o UserIdName do Identity do usuário.
        /// </summary>
        /// <returns></returns>
        string ObterIdentityUserName();

        /// <summary>
        /// Retorna o UsuarioId do usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<long?> ObterUsuarioId();

        /// <summary>
        /// Retorna o Nome do usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<string> ObterUsuarioNome();

        /// <summary>
        /// Retorna o Email do usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<string> ObterUsuarioEmail();

        /// <summary>
        /// Retorna o Nome Completo do usuário logado.
        /// </summary>
        /// <returns></returns>
        Task<string> ObterUsuarioNomeCompleto();

        /// <summary>
        /// Retorna se existe um usuário autenticado no request atual.
        /// </summary>
        /// <returns></returns>
        bool ExisteUsuarioAutenticado();
    }
}
