using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.Repositories
{
    public class UserBookRentRepository : BaseRepository<UserBookRent>, IUserBookRentRepository
    {
        private readonly IHttpContextAccessor _accessor;
        public UserBookRentRepository(ApplicationDbContext dbContext,
                              IHttpContextAccessor accessor)
            : base(dbContext)
        {
            _accessor = accessor;
        }
    }
}
