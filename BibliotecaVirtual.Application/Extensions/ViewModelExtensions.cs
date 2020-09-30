using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BibliotecaVirtual.Application.Extensions
{
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Retorna o valor da propriedade Name do atributo Display de uma entidade.
        /// </summary>
        /// <param name="instancia">Objeto com a propriedade.</param>
        /// <param name="propriedade">Nome da propriedade a ser buscada.</param>
        /// <returns></returns>
        public static string DisplayName<TEntity>(this TEntity instancia, string propriedade) where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(propriedade.Trim()) == true)
            {
                throw new Exception($"Nenhuma propriedade informada para o método ExtensaoColunaGrade.DisplayName().");
            }

            MemberInfo property = (instancia.GetType()).GetProperty(propriedade.Trim());

            if (property == null)
                throw new Exception($"Nenhuma propriedade com o nome {propriedade} encontrada na classe {instancia.ToString()}.");

            if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute dd)
            {
                return dd.Name ?? property.Name;
            }
            else
            {
                return property.Name;
            }
        }
    }
}
