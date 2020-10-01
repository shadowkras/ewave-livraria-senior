using BibliotecaVirtual.Data.Contexts;
using BibliotecaVirtual.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BibliotecaVirtual.Data
{
    public class ApplicationDbContext : GenericContext<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {

        }

        #region DbSet

        public virtual DbSet<IdentityUser> IdentityUser { get; set; }
        public virtual DbSet<User> Usuario { get; set; }

        #endregion

        #region Abstract Classes

        public override void DatabaseConfig(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("database.json", optional: false, reloadOnChange: true)
                       .Build();

            var database = config["Database"];
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void IgnoreEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IdentityUser>();
        }

        #endregion
    }
}
