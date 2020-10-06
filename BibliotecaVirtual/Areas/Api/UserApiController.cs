using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Controllers;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Application.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Areas.Api
{
    public class UserApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserPermissionService _userPermissaoService;

        #region Construtor

        public UserApiController(IUserService servico,
                                 IUserPermissionService UserPermissaoService)
        {
            _userService = servico;
            _userPermissaoService = UserPermissaoService;
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Valida a extensão da imagem de usuário enviada.
        /// </summary>
        /// <param name="extensao">Extensão do arquivo da imagem.</param>
        /// <returns></returns>
        private bool ValidarExtensaoImagem(string extensao)
        {
            if (extensao == ".png" || extensao == ".jpg" || extensao == ".jpeg")
            {
                return true;
            }
            return false;
        }

        #endregion

        [Route("api/pesquisausers")]
        public async Task<IActionResult> ObterUsers(string filtro)
        {
            var users = await _userService.ObtainUsers(filtro);
            return RetornoApi(true, string.Empty, users);
        }

        [Route("api/obterusernome")]
        [ResponseCache(CacheProfileName = "UserHourCache")]
        [HttpGet]
        public async Task<IActionResult> ObterUserNome()
        {
            var resultado = await _userService.ObtainUserFullName();
            if (resultado.IsEmpty() == false)
            {
                return RetornoApi(true, string.Empty, resultado);
            }
            else
            {
                return RetornoApi(false, string.Empty);
            }
        }

        [HttpPost]
        [Route("api/uploadfotoperfil")]
        [ValidateAntiForgeryToken]
        public async Task<object> UploadFotoPerfil(string imagem)
        {
            if (imagem.IsEmpty() == false)
            {
                var result = await _userService.UpdateUserProfilePicture(imagem);
                if (result == true)
                {
                    return RetornoApi(true, "Foto cadastrada com sucesso.");
                }
                else
                {
                    return RetornoApi(false, string.Format(Erros.Possivel_0, "salvar a foto do usuário."));
                }
            }
            else
            {
                return RetornoApi(false, "É preciso informar uma imagem para a sua foto.");
            }
        }

        [HttpGet]
        [Route("api/imagemuserlogado")]
        [ResponseCache(CacheProfileName = "UserMinutoCache")]
        public async Task<string> ImagemUserLoagado()
        {
            var UserFoto = await _userService.ObtainUserProfilePicture();
            return UserFoto;
        }

        [Route("api/createclaims")]
        public async Task<bool> CriarPerfis()
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return await _userPermissaoService.CreateClaims();
            }
            else
            {
                return false;
            }
        }
    }
}
