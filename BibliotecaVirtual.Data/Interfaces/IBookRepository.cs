using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>, IDisposable
    { }
}
