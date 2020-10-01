using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Repositories
{
    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public SchoolRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
