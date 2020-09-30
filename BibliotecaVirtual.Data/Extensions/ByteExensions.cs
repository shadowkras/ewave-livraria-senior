namespace BibliotecaVirtual.Data.Extensions
{
    /// <summary>
    /// Extensões para objetos do tipo byte[].
    /// </summary>
    public static class ByteExensions
    {
        /// <summary>
        /// Converte um array de bytes em uma string UTF8.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static string AsString(this byte[] value)
        {
            var result = string.Empty;

            if (value != null)
                result = System.Text.Encoding.UTF8.GetString(value);

            return result;
        }

        /// <summary>
        /// Converte um array de bytes em uma string de base 64.
        /// </summary>
        /// <param name="value">String de caracteres.</param>
        /// <returns>
        /// O valor convertido.
        /// </returns>
        public static string AsStringBase64(this byte[] value)
        {
            var result = string.Empty;

            if (value != null && value.Length > 0)
                result = string.Format("data:image/png;base64,{0}", System.Convert.ToBase64String(value));

            return result;
        }
    }
}
