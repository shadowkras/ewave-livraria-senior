using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IBaseRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    { }
}
