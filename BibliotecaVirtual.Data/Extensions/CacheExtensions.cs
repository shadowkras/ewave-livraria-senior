using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Classe de exntesões de métodos do IMemoryCache da Microsoft.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Salva uma resposta no memory cache da aplicação e retorna o resultado como string.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
        /// <param name="cache">Instância do IMemoryCache.</param>
        /// <param name="chave">Chave do chave (necessita ser única).</param>
        /// <param name="metodo">Método a ser executado para obter os dados.</param>
        /// <param name="condicao">Condição do método.</param>
        /// <returns></returns>
        public static async Task<string> RespostaCacheString<TEntity>(this IMemoryCache cache, string chave,
                                                                      Func<Expression<Func<TEntity, bool>>, Task<string>> metodo,
                                                                      Expression<Func<TEntity, bool>> condicao = null)
        {
            if (cache.TryGetValue(chave, out string cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                var dados = await metodo(condicao);
                cacheEntry = dados;

                cache.Set(chave, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        /// <summary>
        /// Salva uma resposta no memory cache da aplicação e retorna o resultado como objeto.
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
        /// <param name="cache">Instância do IMemoryCache.</param>
        /// <param name="chave">Chave do chave (necessita ser única).</param>
        /// <param name="metodo">Método a ser executado para obter os dados.</param>
        /// <param name="condicao">Condição do método.</param>
        public static async Task<object> RespostaCacheObject<TEntity>(this IMemoryCache cache, string chave,
                                                                      Func<Expression<Func<TEntity, bool>>, Task<object>> metodo,
                                                                      Expression<Func<TEntity, bool>> condicao = null)
        {
            if (cache.TryGetValue(chave, out object cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                var dados = await metodo(condicao);
                cacheEntry = dados;

                cache.Set(chave, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        /// <summary>
        /// Retorna um objeto do cache de memória da aplicação.
        /// </summary>
        /// <param name="cache">Instância do IMemoryCache.</param>
        /// <param name="chave">Chave do chave (necessita ser única).</param>
        /// <returns></returns>
        public static object ObterCache(this IMemoryCache cache, string chave)
        {
            cache.TryGetValue(chave, out object cacheEntry);
            return cacheEntry;
        }

        /// <summary>
        /// Salva um objeto no cache de memória da aplicação.
        /// </summary>
        /// <param name="cache">Instância do IMemoryCache.</param>
        /// <param name="chave">Chave do chave (necessita ser única).</param>
        /// <param name="objeto">Objeto a ser salvo.</param>
        /// <param name="duracao">Duração do cache, em segundos.</param>
        public static void SalvarCache(this IMemoryCache cache, string chave, object objeto, int duracao = 60)
        {
            if (cache.TryGetValue(chave, out object cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(duracao));
                cache.Set(chave, objeto, cacheEntryOptions);
            }
        }
    }
}
