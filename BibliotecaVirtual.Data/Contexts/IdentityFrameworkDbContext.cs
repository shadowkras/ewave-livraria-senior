using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BibliotecaVirtual.Data
{
    public class IdentityFrameworkDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityFrameworkDbContext(DbContextOptions<IdentityFrameworkDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("database.json", optional: false, reloadOnChange: true)
                       .Build();

            var database = config["Database"];
            var connectionString = config["ConnectionStrings:DefaultConnection"];

            Console.WriteLine("String de conexão (EF): " + connectionString);

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<IdentityUser>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
