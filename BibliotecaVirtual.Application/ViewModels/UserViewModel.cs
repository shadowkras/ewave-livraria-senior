using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class UserViewModel : IAutoMappleable
    {
        [Display(Name="Usuário")]
        public int UserId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required]
        public string Sobrenome { get; set; }

        [Display(Name = "CPF")]
        [Required]
        public string CPF { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Display(Name = "Data Cadastro")]
        public DateTime RegisterDate { get; set; } = System.DateTime.Now;

        [Display(Name = "Foto de perfil")]
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Inativo")]
        public bool Inactive { get; set; }

        public string UsuarioIdentityId { get; set; }

        /// <summary>
        /// Construtor da entidade User, necessário para o EntityFramework.
        /// </summary>
        public UserViewModel()
        { }
    }
}