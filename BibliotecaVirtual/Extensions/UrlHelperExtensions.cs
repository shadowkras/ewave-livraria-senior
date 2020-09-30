using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Extensions
{
    public static class UrlHelperExtensions
    {
        #region Absolute Action

        /// <summary>
        /// Retorna uma string com a rota com o caminho da controller e action desejada.
        /// </summary>
        /// <param name="url">Classe de UrlHelper.</param>
        /// <param name="actionName">Nome da Action que será invocada.</param>
        /// <param name="controllerName">Nome da Controller que será acessada.</param>
        /// <param name="routeValues">Valores das rotas para chegar ao destino.</param>
        /// <returns>A URL absoluta.</returns>
        public static string AbsoluteAction(this UrlHelper url, string controllerName, string actionName, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        #endregion

        #region Absolute Content

        /// <summary>
        /// Retorna uma string com a URL do caminho para o conteúdo especificado.
        /// <para>Converte um caminho virtual (relativo) para o caminho absoluto da aplicação.</para>
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteContent(this IUrlHelper url, string contentPath)
        {
            HttpRequest request = url.ActionContext.HttpContext.Request;
            return new Uri(new Uri(request.Scheme + "://" + request.Host.Value), url.Content(contentPath)).ToString();
        }

        #endregion

        #region Absolute Route Url

        /// <summary>
        /// Cria uma URL absoluta "fully qualified" para a rota especifica usando o nome da rota e valores.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteRouteUrl(this IUrlHelper url, string routeName, object routeValues = null)
        {
            return url.RouteUrl(routeName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
        }

        #endregion

        #region Absolute Url

        /// <summary>
        /// Cria uma url absoluta "fully qualified" para uma action de uma controller, a area atual.
        /// </summary>
        /// <param name="url">Classe de UrlHelper.</param>
        /// <param name="controller">Nome da controler com a action</param>
        /// <param name="action">Nome da action que será invocada.</param>
        /// <param name="routeValues">Valores das rotas para chegar ao destino.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this UrlHelper url, string controller, string action, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            var uri = url.Action(action, controller, routeValues, scheme);

            return uri;
        }

        /// <summary>
        /// Cria uma url absoluta "fully qualified" para uma action de uma controller.
        /// </summary>
        /// <param name="url">Classe de UrlHelper.</param>
        /// <param name="areaName">Área onde a controller se localiza.</param>
        /// <param name="subAreaName">Sub-área onde a controller se localiza.</param>
        /// <param name="controllerName">Nome da controler com a action</param>
        /// <param name="actionName">Nome da action que será invocada.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this UrlHelper url, string areaName,
                                            string controllerName, string actionName)
        {
            var uri = GetAbsoluteUrl(url, controllerName, actionName, new { area = areaName });
            return uri;
        }

        /// <summary>
        /// Cria uma url absoluta "fully qualified" para uma action, assumindo a area e controller atual.
        /// </summary>
        /// <param name="url">Classe de UrlHelper.</param>
        /// <param name="action">Nome da action que será invocada.</param>
        /// <param name="routeValues">Valores das rotas para chegar ao destino.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string action, object routeValues = null)
        {
            var urlHelper = new UrlHelper(url.ActionContext);
            var values = urlHelper.ActionContext.RouteData.Values;
            var controller = values["controller"].ToString();

            return GetAbsoluteUrl(url, controller, action, routeValues);
        }

        /// <summary>
        /// Cria uma url absoluta "fully qualified" para uma action de uma controller, a area atual.
        /// </summary>
        /// <param name="url">Interface da classe de UrlHelper.</param>
        /// <param name="controller">Nome da controler com a action</param>
        /// <param name="action">Nome da action que será invocada.</param>
        /// <param name="routeValues">Valores das rotas para chegar ao destino.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string controller, string action, object routeValues = null)
        {
            string scheme = url.ActionContext.HttpContext.Request.Scheme;
            return url.Action(action, controller, routeValues, scheme);
        }

        /// <summary>
        /// Cria uma url absoluta "fully qualified" para uma action de uma controller.
        /// </summary>
        /// <param name="url">Interface da classe de UrlHelper.</param>
        /// <param name="areaName">Área onde a controller se localiza.</param>
        /// <param name="subAreaName">Sub-área onde a controller se localiza.</param>
        /// <param name="controllerName">Nome da controler com a action</param>
        /// <param name="actionName">Nome da action que será invocada.</param>
        /// <returns>A URL absoluta.</returns>
        public static string GetAbsoluteUrl(this IUrlHelper url, string areaName,
                                            string controllerName, string actionName)
        {
            var uri = GetAbsoluteUrl(url, controllerName, actionName, new { area = areaName });
            return uri;
        }

        #endregion
    }
}
