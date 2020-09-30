using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Classe de extensões para entidades.
    /// </summary>
    public static class TEntityExtensions
    {
        #region Dettach do entity framework

        /// <summary>
        /// Desacompla uma entidade do seu DBContext, desativando o lazy loading.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
        /// <param name="entity">Instância da entidade.</param>
        /// <param name="context">DbContext que controla a entidade.</param>
        /// <returns></returns>
        internal static TEntity Detach<TEntity>(this TEntity entity, DbContext context) where TEntity : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        #endregion

        #region Auto-mapear

        /// <summary>
        /// Realiza o mapeamento as propriedades da classe de origem utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de Origem com as informações das propriedades.</param>
        /// <param name="predicado">Predicado com o construtor do objeto de destino.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
        public static TDestination AutoMapear<TDestination, TSource>(this TSource source, Expression<Func<TSource, TDestination>> predicado) where TSource : class, IAutoMappleable
                                                                                                                                             where TDestination : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ConstructUsing(predicado);
            });

            return configuracao.CreateMapper().Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Realiza o mapeamento as propriedades da classe de origem utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de origem com as informações das propriedades.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
        public static TDestination AutoMapear<TSource, TDestination>(this TSource source) where TSource : class, IAutoMappleable
                                                                                          where TDestination : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Realiza o mapeamento de uma lista de objetos utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Tipo da entidade de destino.</typeparam>
        /// <typeparam name="TSource">Tipo da entidade de origem.</typeparam>
        /// <param name="source">Objeto de origem com as informações das propriedades.</param>
        /// <returns>Objeto do tipo TEntity.</returns>
        public static IEnumerable<TDestination> AutoMapearLista<TSource, TDestination>(this IEnumerable<TSource> source) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            return configuracao.CreateMapper().Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }        

        #endregion

        #region Mapear Predicado

        /// <summary>
        /// Realiza o mapeamento de um predicado da classe de origem para a classe de destino utilizando o AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination">Objeto de destino.</typeparam>
        /// <typeparam name="TSource">Objeto de origem.</typeparam>
        /// <param name="predicado">Predicado do objeto de origem.</param>
        /// <returns>Retorna um predicado com o objeto de destino.</returns>
        public static Expression<Func<TDestination, bool>> MapearPredicado<TSource, TDestination>(this Expression<Func<TSource, bool>> predicado) where TSource : class, IAutoMappleable
        {
            var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TDestination, TSource>();
            });

            return configuracao.CreateMapper().Map<Expression<Func<TSource, bool>>, Expression<Func<TDestination, bool>>>(predicado);
        }

        #endregion
    }
}
