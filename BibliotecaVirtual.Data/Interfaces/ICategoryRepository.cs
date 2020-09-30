using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>, IDisposable
    { }
}
