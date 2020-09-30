using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Classe de métodos de extensão para exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Looks up for all exception messages, including InnerExceptions.
        /// </summary>
        /// <param name="source">Exception class.</param>
        /// <returns>Returns a new line separated string with all exception messages found.</returns>
        public static string GetMessages(this Exception source)
        {
            if (source == null)
            {
                throw new Exception($"Exceção não encontrada para {source.ToString()}.");
            }
            else
            {
                if (string.IsNullOrEmpty(source.Message) == true)
                {
                    throw new Exception($"Nenhuma mensagem de exceção encontrada para {source.ToString()}.");
                }
                else
                {
                    var mensagem = source.Message;

                    if (source.InnerException != null)
                    {
                        mensagem += Environment.NewLine + InnerExceptionMessage(source.InnerException);
                    }

                    return mensagem;
                }
            }
        }

        /// <summary>
        /// Looks up for all exception messages, including InnerExceptions.
        /// </summary>
        /// <param name="source">Exception class.</param>
        /// <returns>A list of exception messages.</returns>
        public static List<string> GetMessageList(this Exception source)
        {
            var lista = new List<string>();

            if (source == null)
            {
                throw new Exception($"Exceção não encontrada para {source.ToString()}.");
            }
            else
            {
                if (string.IsNullOrEmpty(source.Message) == true)
                {
                    throw new Exception($"Nenhuma mensagem de exceção encontrada para {source.ToString()}.");
                }
                else
                {
                    var mensagem = source.Message;

                    if (source.InnerException != null)
                    {
                        mensagem += Environment.NewLine + InnerExceptionMessage(source.InnerException);
                    }

                    lista.Add(mensagem);

                    return lista;
                }
            }
        }

        /// <summary>
        /// Retorna um novo hash como identificador do exception.
        /// </summary>
        /// <param name="source">Classe de exceção.</param>
        /// <returns></returns>
        public static string GetHashId(this Exception source)
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                var stackTrace = source.StackTrace ?? (source.InnerException?.StackTrace);
                if (string.IsNullOrEmpty(stackTrace) == false)
                {
                    byte[] textData = System.Text.Encoding.UTF8.GetBytes(stackTrace);
                    byte[] hash = sha.ComputeHash(textData);
                    return BitConverter.ToString(hash).Replace("-", String.Empty);
                }
                else
                {
                    byte[] textData = System.Text.Encoding.UTF8.GetBytes(source.GetMessages());
                    byte[] hash = sha.ComputeHash(textData);
                    return BitConverter.ToString(hash).Replace("-", String.Empty);
                }
            }
        }

        /// <summary>
        /// Retorna o caminho dos arquivos para a origem do exception.
        /// </summary>
        /// <param name="source">Classe de exceção.</param>
        /// <returns></returns>
        public static string GetStackPathToString(this Exception source)
        {
            var stackPath = GetStackPath(source).ToList();
            if (stackPath.Count > 0)
            {
                return stackPath.Aggregate((a, b) => $"{a} > {b}");
            }
            else if (source.InnerException != null)
            {
                stackPath = GetStackPath(source.InnerException).ToList();
                if (stackPath.Count > 0)
                {
                    return stackPath.Aggregate((a, b) => $"{a} > {b}");
                }
                else
                {
                    return source.Source ?? "Origem desconhecida";
                }
            }
            else
            {
                return source.Source ?? "Origem desconhecida";
            }
        }

        /// <summary>
        /// Retorna uma lista dos arquivos no caminho para o exception.
        /// </summary>
        /// <param name="source">Classe de exceção.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetStackPath(this Exception source)
        {
            var result = new List<string>();
            var stackTrace = new System.Diagnostics.StackTrace(source, true);
            var stackFrames = stackTrace.GetFrames();

            if (stackFrames != null)
            {
                foreach (var sf in stackFrames.Reverse())
                {
                    var fileName = sf.GetFileName();
                    if (string.IsNullOrEmpty(fileName) == false)
                    {
                        //result.Add((sf.GetMethod() as System.Reflection.MethodInfo)?.ToString() ?? string.Empty);
                        result.Add(System.IO.Path.GetFileName(sf.GetFileName()));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Retorna o StackTrace completo do exception como string.
        /// </summary>
        /// <param name="source">Classe de exceção.</param>
        /// <returns></returns>
        public static string GetStackTrace(this Exception source)
        {
            string stackTrace = string.Empty;

            if (source == null)
            {
                stackTrace = $"Exceção não encontrada para {source.ToString()}.";
            }
            else
            {
                if (string.IsNullOrEmpty(source.StackTrace) == false)
                {
                    stackTrace = source.StackTrace;
                }
                else if (source.InnerException != null)
                {
                    stackTrace = GetStackTrace(source.InnerException);
                }
                else
                {
                    stackTrace = $"Exceção {source.ToString()} sem StackTrace.";
                }
            }

            return stackTrace;
        }

        /// <summary>
        /// Busca a mensagem dos InnerExceptions da exceção.
        /// </summary>
        /// <param name="exception">Classe de exceção.</param>
        /// <returns></returns>
        private static string InnerExceptionMessage(Exception exception)
        {
            if (exception.InnerException == null)
            {
                return exception.Message;
            }
            else
            {
                return exception.Message + Environment.NewLine + InnerExceptionMessage(exception.InnerException);
            }
        }
    }
}
