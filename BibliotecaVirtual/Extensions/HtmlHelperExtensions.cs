using BibliotecaVirtual.Data.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaVirtual.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Retorna o nome da Action que invocou a view.
        /// </summary>
        /// <param name="htmlHelper">Classe de HtmlHelper.</param>
        /// <returns></returns>
        public static string ActionName(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
            {
                return (string)routeValues["action"];
            }
            else
            {
                return string.Empty;
            }
        }

        #region Obter Url de Retorno

        /// <summary>
        /// Retorna a url que chamou o request atual, para criar uma "url de retorno".
        /// </summary>
        /// <param name="htmlHelper">Classe de HtmlHelper.</param>
        /// <returns></returns>
        public static string GetReturnUrl(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext?.HttpContext != null &&
                htmlHelper.ViewContext?.HttpContext.Request.Headers.IsEmpty() == false &&
                htmlHelper.ViewContext?.HttpContext.Request.Headers["Referer"].IsEmpty() == false)
            {
                return htmlHelper.ViewContext?.HttpContext.Request.Headers["Referer"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Obter SelectList de enumeradores

        /// <summary>
        /// Retorna uma SelectList com os itens de um enum.
        /// </summary>
        /// <param name="modelValue">Valor do objeto, para a seleção padrão.</param>
        /// <param name="toUpper"> Define se a informação será maiuscula ou minuscula.</param>
        /// <returns></returns>
        public static SelectList GetEnumSelectList<TEnum>(this IHtmlHelper htmlHelper, object modelValue) where TEnum : struct
        {
            System.Collections.IEnumerable items = ListaEnum(typeof(TEnum));

            if (modelValue != null)
                return new SelectList(items, "Value", "Text", selectedValue: modelValue);
            else
                return new SelectList(items, "Value", "Text");
        }

        /// <summary>
        /// Retorna Lista de Display's (Name) e valor das propriedade do enumerador.
        /// </summary>
        /// <param name="source">Type deve ser do tipo Enum (enumerador).</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> ListaEnum(this Enum source)
        {
            return ListaEnum(source.GetType());
        }

        /// <summary>
        /// Retorna Lista de Display's (Name) e valor das propriedade do enumerador.
        /// </summary>
        /// <param name="source">Type deve ser do tipo Enum (enumerador).</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> ListaEnum(Type source)
        {
            var type = source.GetType();
            return from Enum p in Enum.GetValues(source)
                   select new SelectListItem
                   {
                       Selected = p.Equals(type),
                       Text = p.DescricaoEnum(),
                       Value = Convert.ToInt32(p).ToString()
                   };
        }

        #endregion

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
