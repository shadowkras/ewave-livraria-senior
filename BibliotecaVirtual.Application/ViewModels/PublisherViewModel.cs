using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class PublisherViewModel : IAutoMappleable
    {
        [Display(Name="Editora")]
        public int PublisherId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookViewModel> Books { get; set; }

        /// <summary>
        /// Construtor da entidade Publisher, necessário para o EntityFramework.
        /// </summary>
        public PublisherViewModel()
        { }
    }
}