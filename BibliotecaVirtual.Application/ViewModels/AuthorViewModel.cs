using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class AuthorViewModel : IAutoMappleable
    {
        [Display(Name="Autor(a)")]
        public int AuthorId { get; set; }

        [Display(Name = "Nome")]
        //[Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<BookViewModel> Books { get; set; }

        public AuthorViewModel()
        {
            Books = new HashSet<BookViewModel>();
        }        
    }
}