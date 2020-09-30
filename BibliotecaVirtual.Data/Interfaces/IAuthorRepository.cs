using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>, IDisposable
    { }
}
