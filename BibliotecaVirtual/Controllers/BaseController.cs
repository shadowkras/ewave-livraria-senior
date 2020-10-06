using System;
using System.Collections.Generic;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Propriedade que informa ao controler se o método de request é um método de POST.
        /// </summary>
        protected bool MetodoPOST => (HttpContext.Request.Method == "POST");

        /// <summary>
        /// Retorna o nome da controller sem o texto "Controller".
        /// </summary>
        protected string ControllerName => this.GetType().Name.Replace("Controller", string.Empty);

        #region Construtor

        protected BaseController()
        { }

        #endregion

        #region Adicionar erro de Model

        /// <summary>
        /// Adiciona uma mensagem de erro ao model state da view model.
        /// </summary>
        /// <param name="message">Mensgaem de erro da model.</param>
        public void AddModelError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        /// <summary>
        /// Adiciona uma mensagem de erro ao model state da view model.
        /// </summary>
        /// <param name="messages">Lista de mensagens de erros da model.</param>
        public void AddModelError(IList<string> messages)
        {
            foreach (var message in messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }
        }
        #endregion

        #region Retorno de Json (API)

        /// <summary>
        /// Retorno em Json para métodos de API.
        /// </summary>
        /// <param name="sucesso">Operação executada com sucesso ou não.</param>
        /// <param name="mensagem">Mensagem de retorno para o usuário.</param>
        /// <param name="dados">Objeto de retorno para o usuário.</param>
        /// <returns></returns>
        public IActionResult ReturnApi(bool sucesso = false, string mensagem = "", object dados = null)
        {
            return new ViewModels.RetornoApiViewModel(sucesso, mensagem, dados).RetornoJson();
        }

        #endregion

        #region Retorna Mensagem de Erro (API)

        /// <summary>
        /// Retorna uma mensagem de erro como API em formato JSON.
        /// </summary>
        /// <param name="ex">Exception gerado.</param>
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
