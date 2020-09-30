using BibliotecaVirtual.Data.Contexts;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.UnitOfWork
{
    /// <summary>
    /// Classe de unit of work da aplicação.
    /// </summary>
    public class ApplicationUnitOfWork : GenericUnitOfWork<ApplicationDbContext>, IApplicationUnitOfWork
    {
        #region Instâncias

        protected readonly new ApplicationDbContext _dbContext;

        #endregion

        #region Construtor

        public ApplicationUnitOfWork(ApplicationDbContext dbContext)
            : base(dbContext as GenericContext<ApplicationDbContext>)
        {
            _dbContext = dbContext;
        }

        #endregion
    }
}
