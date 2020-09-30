using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class BookViewModel : IAutoMappleable
    {
        [Display(Name="Livro")]
        public int BookId { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Description { get; set; }

        [Display(Name = "Sinopse")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Sinopsis { get; set; }

        [Display(Name = "Páginas")]
        [Range(1, ushort.MaxValue, ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public int Pages { get; set; }

        [Display(Name = "Data de publicação")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public DateTime PublishDate { get; set; } = System.DateTime.Now;

        [Display(Name = "Capa")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string CoverImageBase64 { get; set; }

        [Display(Name = "Autor(a)")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        [Range(0, ushort.MaxValue, ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public int AuthorId { get; set; }

        [Display(Name = "Autor(a)")]
        public string AuthorName { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        [Range(0, ushort.MaxValue, ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public int PublisherId { get; set; }

        [Display(Name = "Editora")]
        public string PublisherName { get; set; }

        [Display(Name = "Gêneros")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        [JsonIgnore]
        public ICollection<BookCategoryViewModel> Categories { get; set; }

        [Display(Name = "Gêneros")]
        public List<int> CategoriesList { get; set; } = new List<int>();

        [Display(Name = "Autor")]
        [JsonIgnore]
        public AuthorViewModel Author { get; set; }

        [Display(Name = "Editora")]
        [JsonIgnore]
        public PublisherViewModel Publisher { get; set; }
    }
}