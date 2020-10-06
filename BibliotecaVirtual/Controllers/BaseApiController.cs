using BibliotecaVirtual.Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BibliotecaVirtual.Controllers
{
    /// <summary>
    /// Classe com métodos padrões para controllers do tipo API.
    /// </summary>
    public class BaseApiController
    {
        #region Construtor

        protected BaseApiController()
        { }

        #endregion

        #region Retorno padrão de API

        /// <summary>
        /// Retorno em Json para métodos de API.
        /// </summary>
        /// <param name="sucesso">Operação executada com sucesso ou não.</param>
        /// <param name="mensagem">Mensagem de retorno para o usuário.</param>
        /// <param name="dados">Objeto de retorno para o usuário.</param>
        /// <param name="returnNotFound">Indica se a API deve retornar o erro 404 quando não tiver sucesso.</param>
        /// <returns></returns>
        public IActionResult RetornoApi(bool sucesso = false, string mensagem = "", object dados = null, bool returnNotFound = false)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel(sucesso, mensagem, dados, returnNotFound).RetornoJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel(sucesso, mensagem, dados, returnNotFound).RetornoJson();
            }
        }

        #endregion

        #region Retorna Mensagem de Erro (API)

        /// <summary>
        /// Retorna uma resposta em JSON com a mensagem da exception gerada.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public IActionResult RetornaErrorApi(Exception ex, string mensagem = null)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = false,
                    Mensagem = mensagem ?? "Ocorreu um erro.",
                    Dados = ex.GetMessageList(),
                }.RetornoJson();
            }
            else
            {
                return new ViewModels.RetornoApiViewModel
                {
                    Sucesso = false,
                    Mensagem = mensagem ?? "Ocorreu um erro.",
                }.RetornoJson();
            }
        }

        #endregion
    }
}
