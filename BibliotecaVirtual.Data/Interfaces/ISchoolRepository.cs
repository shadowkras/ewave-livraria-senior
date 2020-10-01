using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface ISchoolRepository : IBaseRepository<School>, IDisposable
    { }
}
