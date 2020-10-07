using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _accessor;
        public UserRepository(ApplicationDbContext dbContext,
                              IHttpContextAccessor accessor)
            : base(dbContext)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Retorna a informação se o usuário esta logado.
        /// </summary>
        private bool UsuarioAutenticado
        {
            get
            {
                return _accessor?.HttpContext.User.Identity.IsAuthenticated ?? false;
            }
        }

        /// <summary>
        /// Retorna se existe um usuário autenticado no request atual.
        /// </summary>
        /// <returns></returns>
        public bool ExisteUsuarioAutenticado()
        {
            return UsuarioAutenticado;
        }        

        /// <summary>
        /// Obter o IdentityId do Identity do usuário.
        /// </summary>
        /// <returns></returns>
        public string ObterIdentityUserId()
        {
            if (UsuarioAutenticado == true)
            {
                var usuarioIdentity = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return usuarioIdentity;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obter o UserName do Identity do usuário.
        /// </summary>
        /// <returns></returns>
        public string ObterIdentityUserName()
        {
            if (UsuarioAutenticado == true)
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna o UsuarioId do usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<int?> ObterUsuarioId()
        {
            if (UsuarioAutenticado == true)
            {
                var usuarioIdentity = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var usuarioId = await (from p in DbSet.AsQueryable()
                                       where p.UsuarioIdentityId == usuarioIdentity
                                       select p.UserId).FirstOrDefaultAsync();

                return usuarioId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna o Email do usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObterUsuarioEmail()
        {
            if (UsuarioAutenticado == true)
            {
                var usuarioIdentityEmail = _accessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                return usuarioIdentityEmail;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna o Nome do usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObterUsuarioNome()
        {
            var usuarioId = await ObterUsuarioId();
            var nomeUsuario = await DbSet.Where(p => p.UserId == usuarioId)
                                          .Select(p => p.Nome).FirstOrDefaultAsync();
            return nomeUsuario;
        }

        /// <summary>
        /// Retorna o Nome Completo do usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObterUsuarioNomeCompleto()
        {
            var usuarioId = await ObterUsuarioId();
            var nomeUsuario = await DbSet.Where(p => p.UserId == usuarioId)
                                          .Select(p => new { p.Nome, p.Sobrenome }).FirstOrDefaultAsync();
            return $"{nomeUsuario.Nome} {nomeUsuario.Sobrenome}";
        }
    }
}
