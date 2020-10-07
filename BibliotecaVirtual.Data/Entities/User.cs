using System;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade complementar do usuário.
    /// </summary>
    public class User : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome do usuário.
        /// </summary>
        public string Sobrenome { get; set; }

        /// <summary>
        /// Documento pessoal do usuário.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Telefone de contato do usuário.
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Identificador do usuário do UserIdentity (chave estrangeira com IdentityUser).
        /// </summary>
        public string UsuarioIdentityId { get; set; }

        /// <summary>
        /// Data que o usuário se cadastrou.
        /// </summary>
        public DateTime RegisterDate { get; set; } = System.DateTime.Now;

        /// <summary>
        /// Foto do usuário.
        /// </summary>
        public byte[] ProfilePicture { get; set; }

        /// <summary>
        /// Status da conta do usuário (ativo ou inativo).
        /// </summary>
        public bool Inactive { get; set; }

        /// <summary>
        /// Construtor da entidade User, necessário para o EntityFramework.
        /// </summary>
        public User()
        { }

        /// <summary>
        /// Define a imagem de perfil do usuário.
        /// </summary>
        /// <param name="profilePicture">Imagem do usuário.</param>
        public void SetProfilePicture(byte[] profilePicture)
        {
            if(profilePicture.Length > 0)
                ProfilePicture = profilePicture;
        }

        /// <summary>
        /// Define a imagem de perfil do usuário.
        /// </summary>
        /// <param name="profilePicture">Imagem do usuário.</param>
        public void SetProfilePicture(string profilePicture)
        {
            if (string.IsNullOrEmpty(profilePicture) == false)
                ProfilePicture = profilePicture.AsStringBase64();
        }
    }
}
