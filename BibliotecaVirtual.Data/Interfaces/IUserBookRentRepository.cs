using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IUserBookRentRepository : IBaseRepository<UserBookRent>, IDisposable
    { }
}
