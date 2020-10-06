using System;
using BibliotecaVirtual.Data.Extensions;
using Newtonsoft.Json;

namespace BibliotecaVirtual.Extensions
{
    /// <summary>
    /// Métodos de extensão para uso de JSON.
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Converte um objeto do tipo TEntity para uma string em Json.
        /// </summary>
        /// <param name="source">Objeto do tipo TEntity.</param>
        /// <param name="carregarNulos">Converter objetos nulos e vazios no objecto.</param>
        /// <returns></returns>
        public static string SerializarJSON<TEntity>(this TEntity source, bool carregarNulos = false)
        {
            string json = string.Empty;

            try
            {
                if (carregarNulos == true)
                {
                    json = JsonConvert.SerializeObject(source, Helpers.JsonConverters.DefaultSettingsWithNulls);
                }
                else
                {
                    json = JsonConvert.SerializeObject(source, Helpers.JsonConverters.DefaultSettings);
                }
            }
            catch (JsonSerializationException Ex)
            {
                throw new Exception("Erro ao serializar o objeto em JSON: " + Ex.GetMessages());
            }
            catch (Exception Ex)
            {
                throw new Exception("Erro durante a serialização do objeto " + source.GetType() + ": " + Ex.GetMessages());
            }

            return json;
        }

        /// <summary>
        /// Converte uma string em JSON para um objeto do tipo informado.
        /// </summary>
        /// <param name="source">String em JSON.</param>
        /// <param name="type">Tipo do objeto a ser convertido.</param>
        /// <param name="ignorarNulos">Converter objetos nulos e vazios.</param>
        /// <returns></returns>
        public static TEntity DeserializarJSON<TEntity>(this string source, bool ignorarNulos = false)
        {
            if (source == null)
            {
                throw new Exception("Uma string JSON nula não pode ser deserializada.");
            }

            try
            {
                if (ignorarNulos == true)
                {
                    var json = JsonConvert.DeserializeObject(source, typeof(TEntity), Helpers.JsonConverters.DefaultSettingsWithNulls);
                    return (TEntity)json;
                }
                else
                {
                    var json = JsonConvert.DeserializeObject(source, typeof(TEntity), Helpers.JsonConverters.DefaultSettings);
                    return (TEntity)json;
                }
            }
            catch (JsonSerializationException Ex)
            {
                throw new Exception("Erro ao deserializar o arquivo JSON: " + Ex.GetMessages());
            }
            catch (Exception Ex)
            {
                throw new Exception("Erro ao inicializar o arquivo JSON: " + Ex.GetMessages());
            }
        }
    }
}
