using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVirtual.Data
{
    public class IdentityFrameworkDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityFrameworkDbContext(DbContextOptions<IdentityFrameworkDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<IdentityUser>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
