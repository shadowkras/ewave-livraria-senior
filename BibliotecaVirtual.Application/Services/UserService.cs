using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _repository;

        #region Construtor

        public UserService(UserManager<IdentityUser> userManager,
                           RoleManager<IdentityRole> roleManager, 
                           IUserRepository repositorio,
                           IApplicationUnitOfWork uow)
            : base(uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repositorio;
        }

        #endregion

        #region Adicionar, Alterar e Excluir

        /// <summary>
        /// Cadastra uma nova usuário.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da usuário.</param>
        /// <returns></returns>
        public async Task<UserViewModel> AddUser(UserViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Nome == viewModel.Nome && p.Sobrenome == viewModel.Sobrenome))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrado_0, "Usuário");
                return viewModel;
            }

            #endregion

            var user = viewModel.AutoMapear<UserViewModel, User>();
            _repository.Insert(user);
            await Commit();

            //Recuperando o valor recebido pelo UserId.
            viewModel.UserId = user.UserId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de uma usuário cadastrada.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da usuário.</param>
        /// <returns></returns>
        public async Task<UserViewModel> UpdateUser(UserViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Nome == viewModel.Nome && p.Sobrenome == viewModel.Sobrenome && p.UserId != viewModel.UserId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outro usuário com este nome.");
                return viewModel;
            }

            #endregion

            var user = viewModel.AutoMapear<UserViewModel, User>();
            _repository.Update(user);
            await Commit();

            return viewModel;
        }

        /// <summary>
        /// Altera a foto do usuário logado.
        /// </summary>
        /// <param name="imagemFoto">String base64 com a imagem da foto do usuário.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserProfilePicture(string imagemFoto)
        {
            var usuarioId = await _repository.ObterUsuarioId();
            var usuario = await _repository.Select(p => p.UserId == usuarioId.Value);

            byte[] imgResized = imagemFoto.AsStringBase64();
            usuario.ProfilePicture = imgResized;

            _repository.Update(usuario, p => p.ProfilePicture);
            await Commit();

            return IsSuccessful();
        }

        /// <summary>
        /// Remove uma usuário.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da usuário.</param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(UserViewModel viewModel)
        {
            var user = viewModel.AutoMapear<UserViewModel, User>();
            _repository.Delete(user);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta uma usuário cadastrado.
        /// </summary>
        /// <param name="UserId">Identificador da usuário.</param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int UserId)
        {
            _repository.Delete(p => p.UserId == UserId);
            await Commit();

            return OperationSuccesful;
        }

        #endregion

        #region Consultas

        /// <summary>
        /// Retorna o nome do usuário.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObtainUserName()
        {
            return await _repository.ObterUsuarioNome();
        }

        /// <summary>
        /// Retorna o nome completo do usuário.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObtainUserFullName()
        {
            return await _repository.ObterUsuarioNomeCompleto();
        }

        /// <summary>
        /// Retorna o e-mail cadastrado para o usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObtainUserEmail()
        {
            var usuarioIdentityId = _repository.ObterIdentityUserId();
            var userIdentity = await _userManager.FindByIdAsync(usuarioIdentityId);
            return userIdentity.Email;
        }

        /// <summary>
        /// Retorna o email cadastrado para o usuário informado.
        /// </summary>
        /// <param name="usuarioId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<string> ObtainUserEmail(long usuarioId)
        {
            var usuarioIdentityId = await _repository.Select(p => p.UserId == usuarioId, p => p.UsuarioIdentityId);
            var userIdentity = await _userManager.FindByIdAsync(usuarioIdentityId);
            return userIdentity.Email;
        }

        /// <summary>
        /// Retorna a foto do usuário logado.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObtainUserProfilePicture()
        {
            var usuarioId = await _repository.ObterUsuarioId();
            var usuarioFoto = await _repository.Select(p => p.UserId == usuarioId,
                                                       p => p.ProfilePicture);

            return usuarioFoto.AsStringBase64();
        }

        /// <summary>
        /// Obtém uma lista com os usuárioes cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserViewModel>> ObtainUsers(string filtro)
        {
            #region Predicados com os filtros de pesquisa

            Expression<Func<User, bool>> predicate = null;

            if (string.IsNullOrEmpty(filtro) == false)
            {
                predicate = p => p.Nome.Contains(filtro) || p.Sobrenome.Contains(filtro);
            }

            #endregion

            var users = await _repository.SelectList(predicate);
            var viewModel = users.AutoMapearLista<User, UserViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um usuário a partir do seu identificador.
        /// </summary>
        /// <param name="UserId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<UserViewModel> ObtainUser(int UserId)
        {
            var users = await _repository.Select(p => p.UserId == UserId);
            var viewModel = users.AutoMapear<User, UserViewModel>();
            return viewModel;
        }

        #endregion

        #region User Claims & Roles

        /// <summary>
        /// Verifica se o usuário possui a permissão especificada.
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public bool UserHasClaim(string claim)
        {
            return ClaimsPrincipal.Current.HasClaim(claim, "true");
        }

        /// <summary>
        /// Verifica se o usuário está no perfil especificado.
        /// </summary>
        /// <param name="role">Nome do perfil.</param>
        /// <returns></returns>
        public bool UserIsInRole(string role)
        {
            return ClaimsPrincipal.Current.IsInRole(role);
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
