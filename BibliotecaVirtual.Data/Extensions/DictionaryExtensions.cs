using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaVirtual.Data.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Retorna se um dicionário é vazio.
        /// </summary>
        /// <param name="dictionary">Objeto do tipo IDictionary.</param>
        /// <returns></returns>
        public static bool IsEmpty(this IDictionary dictionary)
        {
            return dictionary.Count == 0;
        }

        /// <summary>
        /// Retorna se uma lista é vazia.
        /// </summary>
        /// <param name="list">Objeto do tipo IList.</param>
        /// <returns></returns>
        public static bool IsEmpty(this IList list)
        {
            return list.Count == 0;
        }

        /// <summary>
        /// Retorna se uma coleção é vazia.
        /// </summary>
        /// <param name="collection">Objeto do tipo ICollection.</param>
        /// <returns></returns>
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Extensão utilizada para descobrir se uma coleção de objetos possui itens.
        /// <para>Exemplo: if(MyCollection.IsEmpty() == true)</para>
        /// </summary>
        /// <param name="objeto">Objeto do tipo Lista.</param>
        /// <returns>
        /// Retorna se a lista está vazia.
        /// </returns>        
        public static bool IsEmpty<T>(this ICollection<T> objeto)
        {
            if (objeto == null)
                return true;
            else
                return (objeto.Count == 0);
        }

        /// <summary>
        /// Extensão utilizada para descobrir se uma coleção de objetos possui itens.
        /// <para>Exemplo: if(MyEnumerable.IsEmpty() == true)</para>
        /// </summary>
        /// <param name="objeto">Objeto do tipo IEnumerable.</param>
        /// <returns>
        /// Retorna se a lista está vazia.
        /// </returns>        
        public static bool IsEmpty<T>(this IEnumerable<T> objeto)
        {
            if (objeto == null)
                return true;
            else
                return (objeto.Any() == false);
        }
    }
}
