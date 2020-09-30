using System;
using System.Text.RegularExpressions;

namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Extensões para objetos do tipo string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converte uma string em uma array de bytes com base 64.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static byte[] AsStringBase64(this string value)
        {
            byte[] result = null;

            if (value != null)
            {
                value = Regex.Replace(value, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                result = Convert.FromBase64String(value);
            }

            return result;
        }
    }
}
