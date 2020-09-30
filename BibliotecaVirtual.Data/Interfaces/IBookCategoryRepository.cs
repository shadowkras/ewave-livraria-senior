﻿using BibliotecaVirtual.Data.Entities;
using System;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IBookCategoryRepository : IBaseRepository<BookCategory>, IDisposable
    { }
}