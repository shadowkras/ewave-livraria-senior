using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Classe de métodos de extensão para enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retorna Display (Name) da propriedade do enumerador.
        /// </summary>
        /// <param name="enumerador">Enumerador a ser recuperado.</param>
        /// <returns></returns>
        public static string DescricaoEnum(this Enum enumerador)
        {
            return enumerador.GetType()
                             .GetMember(enumerador.ToString())
                             .FirstOrDefault()
                             ?.GetCustomAttribute<DisplayAttribute>(false)
                             ?.Name
                             ?? enumerador.ToString();
        }

        /// <summary>
        /// Retorna os valores de um enum como uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo do Enum.</typeparam>
        public static IList<T> GetValues<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Values;
        }

        /// <summary>
        /// Retorna uma array com os nomes dos itens em um enum.
        /// </summary>
        /// <typeparam name="T">Tipo do Enum.</typeparam>
        /// <returns></returns>
        public static string[] GetNamesArray<T>() where T : struct, IEnumConstraint
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
