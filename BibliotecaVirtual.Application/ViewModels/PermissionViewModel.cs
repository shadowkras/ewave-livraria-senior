using BibliotecaVirtual.Application.Resources;
using System.ComponentModel.DataAnnotations;

namespace Vigente.Web.Application.ViewModels
{
    public class PermissionViewModel
    {
        [Display(Name = "Id")]
        public long UserId { get; set; }

        public string IdentityId { get; set; }

        [Display(Name = "Nome completo")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }        

        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
        [Display(Name = "Perfil")]
        public string Role { get; set; }

        [Display(Name = "Inativo")]
        public bool Inactive { get; set; }

        public PermissionViewModel()
        { }
    }
}
