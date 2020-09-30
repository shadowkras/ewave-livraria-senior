using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public CategoryRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
