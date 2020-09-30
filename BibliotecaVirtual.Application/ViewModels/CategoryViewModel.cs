using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class CategoryViewModel : IAutoMappleable
    {
        [Display(Name="G�nero")]
        public int CategoryId { get; set; }

        [Display(Name = "Descri��o")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Display(Name = "Sobre")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
        [Url]
        public string AboutUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookCategoryViewModel> Books { get; set; }
    }
}