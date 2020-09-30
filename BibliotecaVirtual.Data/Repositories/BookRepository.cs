using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public BookRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
