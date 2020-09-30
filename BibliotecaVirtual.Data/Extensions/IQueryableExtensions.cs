using System;
using System.Linq;
using System.Linq.Expressions;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Extensões para objetos com a interface IQueryable.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Permite aplicar um predicado nulo a uma expressão linq sem causar erros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Lista de entidades com a interface IQueryable.</param>
        /// <param name="predicate">Expressão de predicado (permite nulo).</param>
        /// <returns></returns>
        public static IQueryable<T> WhereNullSafe<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? source : source.Where(predicate);
        }
    }
}
