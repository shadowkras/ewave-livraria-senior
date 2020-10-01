using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="dbContext">Contexto do banco para a entidade.</param>
        public AddressRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
