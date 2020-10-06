using Microsoft.AspNetCore.Mvc;
using BibliotecaVirtual.Extensions;

namespace BibliotecaVirtual.ViewModels
{
    /// <summary>
    /// Classe de retorno de Api.
    /// </summary>
    public class RetornoApiViewModel
    {
        #region Propriedades Públicas

        /// <summary>
        /// Retorna se a operação foi realizada com sucesso.
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagem de retorno para o usuário.
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Dados de retorno da operação.
        /// </summary>
        public object Dados { get; set; }

        /// <summary>
        /// Indica se a API deve retornar erro 404 quando não obtiver sucesso.
        /// </summary>
        private bool? ReturnNotFound { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor da classe de retorno de Api.
        /// </summary>
        /// <param name="sucesso">Indica se a operação foi concluída com sucesso.</param>
        /// <param name="mensagem">Mensagem de retorno para o usuário.</param>
        /// <param name="dados">Dados de retorno da operação.</param>
        public RetornoApiViewModel(bool sucesso = false, string mensagem = "", object dados = null, bool? returnNotFound = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
            ReturnNotFound = returnNotFound;
        }

        #endregion

        #region Métodos de Retorno

        /// <summary>
        /// Retorna a classe em formato JSON.
        /// </summary>
        /// <param name="displayNulls">Converter objetos nulos e vazios.</param>
        /// <returns></returns>
        public ContentResult RetornoJson(bool displayNulls = false)
        {
            return new ContentResult
            {
                StatusCode = this.ReturnNotFound == true ? 404 : 200,
                Content = this.SerializarJSON(displayNulls),
                ContentType = "application/json"
            };
        }

        #endregion
    }
}
