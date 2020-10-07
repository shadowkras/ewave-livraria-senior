using System.Collections.Generic;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de endereço.
    /// </summary>
    public class Address : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do endereço
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Descrição do endereço.
        /// </summary>
        public string Endereco { get; set; }

        /// <summary>
        /// Número do endereço.
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Bairro do endereço.
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Cidade do endereço.
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Estado do endereço.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Complemento do endereço.
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Construtor da entidade Address, necessário para o EntityFramework.
        /// </summary>
        public Address()
        { }

        /// <summary>
        /// Construtor com as informações requeridas de um endereço.
        /// </summary>
        /// <param name="endereco">Descrição do endereço.</param>
        public Address(string endereco)
        {
            Endereco = endereco;
        }
    }
}
