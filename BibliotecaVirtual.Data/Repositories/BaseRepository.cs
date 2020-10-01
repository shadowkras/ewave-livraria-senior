using System;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Data.Contexts;

namespace BibliotecaVirtual.Data.Repositories
{
    /// <summary>
    /// Classe com os métodos padrões dos repositórios.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : GenericRepository<TEntity, ApplicationDbContext>, IBaseRepository<TEntity>, IDisposable where TEntity : class
    {
        protected readonly new ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
            : base(dbContext as GenericContext<ApplicationDbContext>)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
        }
    }
}
