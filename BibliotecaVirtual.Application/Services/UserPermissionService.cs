using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using System.Linq.Expressions;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Application.Enums;
using Vigente.Web.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Application.Interfaces;

namespace BibliotecaVirtual.Application.Services
{
    public class UserPermissionService : BaseService, IUserPermissionService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IUserRepository _repositorio;

        private readonly List<string> DefaultUserRoles = new List<string> {
                UserRoles.Administrator,
                UserRoles.Moderator,
            };

        public UserPermissionService(UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IUserRepository repositorio,
                              IApplicationUnitOfWork uow)
            : base(uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repositorio = repositorio;
        }

        #region Métodos privados

        /// <summary>
        /// Atualiza as informações de perfil e e-mail dos usuários.
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        private async Task UpdateIdentityData(ICollection<PermissionViewModel> Users)
        {
            foreach (var User in Users)
            {
                var UserIdentity = await _userManager.FindByIdAsync(User.IdentityId);
                User.Email = UserIdentity.Email;

                var roles = await _userManager.GetRolesAsync(UserIdentity);
                User.Role = string.Join(",", roles);
            }
        }

        /// <summary>
        /// Seleciona as informações de usuário que serão utilizadas na viewmodel.
        /// </summary>
        /// <returns></returns>
        private static Expression<System.Func<User, PermissionViewModel>> SelectUsers()
        {
            return p => new PermissionViewModel
            {
                UserId = p.UserId,
                IdentityId = p.UsuarioIdentityId,
                FullName = $"{p.Nome} {p.Sobrenome}",
                Inactive = p.Inactive
            };
        }

        #endregion

        #region Consultas

        /// <summary>
        /// Realiza uma pesquisa por usuários.
        /// </summary>
        /// <param name="filtro">Filtro da pesquisa.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PermissionViewModel>> ObterUsersPermissions(string filtro)
        {
            var Users = new List<PermissionViewModel>();

            if (long.TryParse(filtro, out long chave))
            {
                var dados = await _repositorio.SelectList(p => p.UserId == chave, SelectUsers());

                Users = dados.ToList();
            }
            else if (filtro.IsEmpty() == false)
            {
                var dados = await _repositorio.SelectList(0, 10, SelectUsers(),
                                                          p => p.Nome.ToLower().Contains(filtro.ToLower()) ||
                                                               p.Sobrenome.ToLower().Contains(filtro.ToLower()));


                Users = dados.ToList();
            }
            else
            {
                var dados = await _repositorio.SelectList(0, 200, SelectUsers());

                Users = dados.ToList();
            }

            await UpdateIdentityData(Users);

            return Users;
        }

        #endregion

        #region User Claims & Roles

        /// <summary>
        /// Criar os roles e claims padrões da aplicação.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateClaims()
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                if (await _roleManager.RoleExistsAsync(UserRoles.Administrator) == false)
                {
                    #region Administrador

                    var identityRole = new IdentityRole(UserRoles.Administrator);
                    await _roleManager.CreateAsync(identityRole);
                    await _roleManager.AddClaimAsync(identityRole, new Claim(UserClaims.AccessAdministrativeTools, "true"));
                    await _roleManager.AddClaimAsync(identityRole, new Claim(UserClaims.AccessModerationTools, "true"));

                    #endregion
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Moderator) == false)
                {
                    #region Moderador

                    var identityRole = new IdentityRole(UserRoles.Moderator);
                    await _roleManager.CreateAsync(identityRole);
                    await _roleManager.AddClaimAsync(identityRole, new Claim(UserClaims.AccessModerationTools, "true"));

                    #endregion
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Altera o perfil atual do usuário.
        /// </summary>
        /// <param name="UserId">Identificador do usuário.</param>
        /// <param name="userRole">Nome do novo perfil do usuário.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserRole(int UserId, string userRole)
        {
            var UserIdentityId = await _repositorio.Select(p => p.UserId == UserId,
                                                              p => p.UsuarioIdentityId);
            if (UserIdentityId.IsEmpty() == false)
            {
                var otherRoles = DefaultUserRoles.Where(p => p != userRole);
                var UserIdentity = await _userManager.FindByIdAsync(UserIdentityId);
                foreach (var item in otherRoles)
                {
                    await _userManager.RemoveFromRoleAsync(UserIdentity, item);
                }

                var result = await _userManager.AddToRoleAsync(UserIdentity, userRole);
                return result.Succeeded;
            }

            return false;
        }

        /// <summary>
        /// Remove o usuário de todos os perfis do sistema.
        /// </summary>
        /// <param name="UserId">Identificador do usuário.</param>
        /// <param name="userRole">Nome do novo perfil do usuário.</param>
        /// <returns></returns>
        public async Task<bool> RemoveUserRole(int UserId, string userRole)
        {
            var UserIdentityId = await _repositorio.Select(p => p.UserId == UserId,
                                                           p => p.UsuarioIdentityId);
            if (UserIdentityId.IsEmpty() == false)
            {
                var UserIdentity = await _userManager.FindByIdAsync(UserIdentityId);
                var removeResult = await _userManager.RemoveFromRoleAsync(UserIdentity, userRole);
                //forçando ele a excluir outras possíveis permissoes  
                var roles = await _userManager.GetRolesAsync(UserIdentity);
                foreach (var item in roles)
                {
                    removeResult = await _userManager.RemoveFromRoleAsync(UserIdentity, item);
                }

                return removeResult.Succeeded;
            }

            return false;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _repositorio.Dispose();
        }

        #endregion
    }
}
