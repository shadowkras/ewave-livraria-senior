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

        /// <summary>
        ///     Checks whether a string can be converted to the Boolean (true/false) type.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result);
        }

        /// <summary>
        ///     Converts a string to a Boolean (true/false) value.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static bool AsBool(this string value)
        {
            return value.AsBool(false);
        }

        /// <summary>
        ///     Converts a string to a Boolean (true/false) value and specifies a default value.
        /// </summary>
        /// <returns>
        ///     The converted value.
        /// </returns>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The value to return if <paramref name="value" /> is null or is an invalid value.</param>
        public static bool AsBool(this string value, bool defaultValue)
        {
            if (!bool.TryParse(value, out bool result))
                return defaultValue;
            return result;
        }

        /// <summary>
        ///     Checks whether a string can be converted to the <see cref="T:System.DateTime" /> type.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="value" /> can be converted to the specified type; otherwise, false.
        /// </returns>
        /// <param name="value">The string value to test.</param>
        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }
    }
}
