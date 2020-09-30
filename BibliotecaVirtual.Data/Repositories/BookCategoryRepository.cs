using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Repositories
{
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public BookCategoryRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
