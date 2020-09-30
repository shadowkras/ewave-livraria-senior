using BibliotecaVirtual.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class BookCategoryViewModel : IAutoMappleable
    {
        [Display(Name = "Livro")]
        public int BookId { get; set; }

        [Display(Name = "G�nero")]
        public int CategoryId { get; set; }        
    }
}